using CommonDLL;
using DomainDLL;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    public class NHHelper
    {
        #region 私有静态变量

        private static object m_Locker = new object();
        private static Configuration m_Configuration = null;
        private static ISessionFactory m_SessionFactory = null;
        private static ISessionStorage m_Sessionsource;

        #endregion

        #region 静态构造函数

        static NHHelper()
        {
            m_Sessionsource = ISessionStorageFactory.GetSessionStorage();
        }

        #endregion

        #region 内部静态变量

        /// <summary>
        /// NHibernate配置对象
        /// </summary>
        public static Configuration Configuration
        {
            get
            {
                lock (m_Locker)
                {
                    if (m_Configuration == null)
                    {
                        CreateConfiguration();
                    }
                    return m_Configuration;
                }
            }
            set { m_Configuration = value; }
        }

        /// <summary>
        /// NHibernate的对象工厂
        /// </summary>
        internal static ISessionFactory SessionFactory
        {
            get
            {
                if (null == m_SessionFactory)
                {
                    if (m_Configuration == null)
                    {
                        CreateConfiguration();
                    }
                    lock (m_Locker)
                    {
                        m_SessionFactory = Configuration.BuildSessionFactory();
                    }
                }

                return m_SessionFactory;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 建立ISessionFactory的实例
        /// </summary>
        /// <returns></returns>
        public static ISession GetCurrentSession()
        {
            if (Config.UserSessionSource) //如果使用保存的ISession
            {
                ISession s = m_Sessionsource.Get();
                if (s == null)
                {
                    s = SessionFactory.OpenSession(new SQLWatcher());
                    //m_Sessionsource.Set(s);
                    //CurrentSessionContext.Bind(s);
                }
                return s;
            }
            else //如果使用新ISession
            {
                ISession s = SessionFactory.OpenSession();
                //CurrentSessionContext.Bind(s);
                return s;
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="s"></param>
        public static void BeginTransaction(ISession s)
        {
            s.BeginTransaction();
            CurrentSessionContext.Bind(s);
        }

        /// <summary>
        /// 分页一览（多表关联）查询
        /// Created:2017.3.28(xuxb)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public static DataTable ExecutePageDataTable(string sql, List<QueryField> qlist, int pageSize, int pageIndex)
        {
            ISession session = null;
            DataTable result = new DataTable();
            try
            {
                //设定分页条件
                string pageSql = " Limit " + pageSize * (pageIndex - 1) + "," + pageSize;
                session = GetCurrentSession();
                IDbCommand command = session.Connection.CreateCommand();
                command.CommandText = sql + pageSql;
                command.CommandType = CommandType.Text;
                foreach (QueryField field in qlist)
                {
                    SQLiteParameter param = new SQLiteParameter();
                    param.Value = field.Value;
                    param.ParameterName = field.Name;
                    param.DbType = ConvertDbType(field.Type);
                    command.Parameters.Add(param);
                }
                IDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string columnName = schemaTable.Rows[i][0].ToString();
                    result.Columns.Add(columnName);
                }
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    object[] values = new Object[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                    {
                        values[i] = reader.GetValue(i);
                    }
                    result.Rows.Add(values);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                return null;
            }
            return result;
        }

        /// <summary>
        /// 一览查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string sql, List<QueryField> qlist)
        {
            ISession session = null;
            DataSet ds = new DataSet();
            try
            {
                session = GetCurrentSession();
                IDbCommand command = session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                foreach (QueryField field in qlist)
                {
                    SQLiteParameter param = new SQLiteParameter();
                    param.Value = field.Value;
                    param.ParameterName = field.Name;
                    param.DbType = ConvertDbType(field.Type);
                    command.Parameters.Add(param);
                }
                IDataReader reader = command.ExecuteReader();
                DataTable result = new DataTable();
                DataTable schemaTable = reader.GetSchemaTable();
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string columnName = schemaTable.Rows[i][0].ToString();
                    Type t = (Type)schemaTable.Rows[i]["DataType"];
                    result.Columns.Add(columnName, t);
                }
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    object[] values = new Object[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                    {
                        values[i] = reader.GetValue(i);
                    }
                    result.Rows.Add(values);
                }
                ds.Tables.Add(result);
                reader.Close();
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                return null;
            }
            return ds;
        }


        /// <summary>
        /// 一览查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, List<QueryField> qlist)
        {
            ISession session = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                session = GetCurrentSession();
                IDbCommand command = session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                foreach (QueryField field in qlist)
                {
                    SQLiteParameter param = new SQLiteParameter();
                    param.Value = field.Value;
                    param.ParameterName = field.Name;
                    param.DbType = ConvertDbType(field.Type);
                    command.Parameters.Add(param);
                }
                IDataReader reader = command.ExecuteReader();
                DataTable result = new DataTable();
                DataTable schemaTable = reader.GetSchemaTable();
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string columnName = schemaTable.Rows[i][0].ToString();
                    //Type t = (Type)schemaTable.Rows[i]["DataType"];
                    //result.Columns.Add(columnName, t);
                    result.Columns.Add(columnName);
                }
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    object[] values = new Object[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                    {
                        values[i] = reader.GetValue(i);
                    }
                    result.Rows.Add(values);
                }
                ds.Tables.Add(result);
                reader.Close();
                dt = ds.Tables[0];
                dt.Columns.Add("RowNo");
                int no = 1;
                foreach (DataRow row in dt.Rows)
                {
                    row["RowNo"] = no;
                    no++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                return null;
            }
            return dt;
        }

        public static DataTable ExecuteDataTableNoRow(string sql, List<QueryField> qlist)
        {
            ISession session = null;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                session = GetCurrentSession();
                IDbCommand command = session.Connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                foreach (QueryField field in qlist)
                {
                    SQLiteParameter param = new SQLiteParameter();
                    param.Value = field.Value;
                    param.ParameterName = field.Name;
                    param.DbType = ConvertDbType(field.Type);
                    command.Parameters.Add(param);
                }
                IDataReader reader = command.ExecuteReader();
                DataTable result = new DataTable();
                DataTable schemaTable = reader.GetSchemaTable();
                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    string columnName = schemaTable.Rows[i][0].ToString();
                    result.Columns.Add(columnName);
                }
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    object[] values = new Object[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                    {
                        values[i] = reader.GetValue(i);
                    }
                    result.Rows.Add(values);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 返回分页数据
        /// Created:20170330(ChengMengjia)
        /// </summary>
        /// <param name="QueryHead"></param>
        /// <param name="QueryBody"></param>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public static GridData GetGridData(int PageIndex, int PageSize, string QueryHead, string QueryBody, List<QueryField> qlist)
        {
            GridData result = new GridData();
            result.count = 0;
            result.data = new DataTable();
            string sql = "select count(1) n " + QueryBody;
            DataSet ds = ExecuteDataset(sql, qlist);
            if (ds != null && ds.Tables.Count > 0)
            {
                result.count = int.Parse(ds.Tables[0].Rows[0]["n"].ToString());
                sql = QueryHead + QueryBody;
                DataTable dt = NHHelper.ExecutePageDataTable(sql, qlist, PageSize, PageIndex);
                if (dt != null)
                {
                    dt.Columns.Add("RowNo");
                    int i = 1;
                    if (PageIndex >= 1)
                        i += (PageIndex - 1) * PageSize;
                    foreach (DataRow row in dt.Rows)
                    {
                        row["RowNo"] = i;
                        i++;
                    }
                }
                result.data = dt;
            }
            return result;
        }

        public static List<dynamic> GetDynamicList(string sql, Dictionary<string, string> args)
        {
            ISession session = GetCurrentSession();
            try
            {
                ISQLQuery query = session.CreateSQLQuery(sql);
                foreach (var item in args)
                {
                    query.SetString(item.Key, item.Value);
                }
                return query.DynamicList().ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                return null;
            }
        }

        #endregion

        #region 私有方法

        private static void CreateConfiguration()
        {
            m_Configuration = new Configuration().Configure();
        }

        private static DbType ConvertDbType(QueryFieldType qfType)
        {
            switch (qfType)
            {
                case QueryFieldType.Boolean:
                    return DbType.Boolean;
                case QueryFieldType.DateTime:
                    return DbType.DateTime;
                case QueryFieldType.Numeric:
                    return DbType.Decimal;
                case QueryFieldType.String:
                    return DbType.String;
                default:
                    return DbType.String;
            }
        }

        #endregion

    }

    public class SQLWatcher : EmptyInterceptor
    {
        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            System.Diagnostics.Debug.WriteLine("sql语句:" + sql);
            return base.OnPrepareStatement(sql);
        }
    }
}
