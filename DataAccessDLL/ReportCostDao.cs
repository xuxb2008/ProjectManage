using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 成本分配报表Dao
    /// 2017/04/28(zhuguanjun)
    /// </summary>
    public class ReportCostDao
    {
        /// <summary>
        /// 获取成本分配
        /// 2017/04/28(zhuguanjun)
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public DataTable GetCost(List<string> pids)
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
            //查询收入
            sql.AppendFormat(@" select e.ID as KeyFieldName,e.PID as ParentFieldName,e.Tag,e.Explanation,e.Total,e.Used,e.Transit,e.Remaining,e.Remark from Cost e
                          left join Project p on e.PID = P.ID
                          where e.Status = @Status and p.ID is not null");
            //交合
            sql.Append(" union");
            //查询项目
            sql.Append(@" select distinct(p1.ID) as KeyFieldName,p1.ID as ParentFieldName,Name as Tag,null as Explanation,null as Total,null as Used,null as Transit,null as Remaining,null as Remark from Project p1 
                          left join Cost e1 on e1.PID = P1.ID                          
                          group by p1.ID");
            //最外层
            sql.Append(" )");

            sql.Append(" where ParentFieldName in (" + PIDList + ")");
            sql.Append(" order by ParentFieldName,Tag");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }
    }
}
