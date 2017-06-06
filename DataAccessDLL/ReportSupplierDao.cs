using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 供应商报表DAO
    /// 2017/05/16(zhuguanjun)
    /// </summary>
    public class ReportSupplierDao
    {
        /// <summary>
        /// 获取供应商报表
        /// 2017/05/16
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable GetSupplier(List<string> pids, Dictionary<string, string> dic)
        {
            #region 查询条件
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });

            string PIDList = "";
            if (pids != null && pids.Count() > 0)
            {
                foreach (var item in pids)
                {
                    PIDList += "'" + item + "',";
                }
                PIDList = PIDList.TrimEnd(new char[] { ',' });
            }

            #endregion

            StringBuilder sql = new StringBuilder();
            //最外层
            sql.Append(" select * from (");
            //查询分包合同
            sql.Append(" select s.ID as KeyFieldName,s.PID as ParentFieldName,s.Name,s.LegalMan,");
            sql.Append(" s.Manager,s.Tel,s.Addr");
            sql.AppendFormat(@" from Supplier s left join Project p on s.PID = p.ID  
                          where s.Status = @Status and p.ID is not null");
            //交合
            sql.Append(" union");
            //查询项目
            sql.Append(" select distinct(p.ID) as KeyFieldName,p.ID as ParentFieldName,p.Name,null as LegalMan");
            sql.Append(" ,null as Manager,null as Tel,null as Addr");
            sql.Append(@" from Project p left join Supplier c on c.PID = p.ID                          
                          group by p.ID");
            //最外层
            sql.Append(" )");

            sql.Append(" where ParentFieldName in (" + PIDList + ")");
            //排序
            sql.Append(" order by ParentFieldName,Name");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }
    }
}
