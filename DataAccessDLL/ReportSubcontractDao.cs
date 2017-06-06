using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 分包合同报表DAO
    /// </summary>
    public class ReportSubcontractDao
    {
        /// <summary>
        /// 获取分包合同报表
        /// 2015/05/16(zhuguanjun)
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable GetSubcontract(List<string> pids,Dictionary<string,string> dic)
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
            //string sqlstr1 = "";//分包字段
            //string sqlstr2 = "";//项目字段
            //if (dic!=null&&dic.Count>0)
            //{
            //    foreach (var item in dic)
            //    {
            //        sqlstr1 += "s." + item.Key + ",";
            //        sqlstr2 += "null as " + item.Key + ",";
            //    }
            //    sqlstr1 =sqlstr1.TrimEnd(',');
            //    sqlstr2 = sqlstr2.TrimEnd(',');               
            //}
            
            #endregion

            StringBuilder sql = new StringBuilder();
            //最外层
            sql.Append(" select * from (");
            //查询分包合同
            sql.Append(" select s.ID as KeyFieldName,s.PID as ParentFieldName,s.B_Name,d.Name as SupplierName,");
            sql.Append(" s.B_No,s.A_No,s.A_Name,s.CompanyName,s.Amount,s.SignDate,s.Desc");
            sql.AppendFormat(@" from Subcontract s left join Project p on s.PID = p.ID  
                          left join Supplier d on substr(d.ID,1,36) = s.CompanyName and d.Status=@Status  
                          where s.Status = @Status and p.ID is not null");
            //交合
            sql.Append(" union");
            //查询项目
            sql.Append(" select distinct(p.ID) as KeyFieldName,p.ID as ParentFieldName,p.Name as B_Name,null as SupplierName");
            sql.Append(" ,null as B_No,null as A_No,null as A_Name,null as CompanyName,null as Amount,null as SignDate,null as Desc");
            sql.Append(@" from Project p left join Subcontract c on c.PID = p.ID                          
                          group by p.ID");
            //最外层
            sql.Append(" )");

            sql.Append(" where ParentFieldName in (" + PIDList + ")");
            //排序
            sql.Append(" order by ParentFieldName, B_Name");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }
    }
}
