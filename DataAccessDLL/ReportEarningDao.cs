using CommonDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 收入报表Dao
    /// 2017/04/25(zhuguanjun)
    /// </summary>
    public class ReportEarningDao
    {
        /// <summary>
        /// 获取收入表
        /// 2017/04/27(zhuguanjun)
        /// </summary>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public DataTable GetEarning(List<string> pids)
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
            sql.AppendFormat(@" select e.ID as KeyFieldName,e.PID as ParentFieldName,e.Step,e.Ratio,e.FinishTag,d.Name as FinishStatus,e.Remark  from Income e 
                          left join Project p on e.PID = P.ID 
                          left join DictItem d on d.No = e.FinishStatus and d.DictNo ={0} 
                          where e.Status = @Status and p.ID is not null", (int)DictCategory.EarningStatus);
            //交合
            sql.Append(" union");
            //查询项目
            sql.Append(@" select distinct(p1.ID) as KeyFieldName,p1.ID as ParentFieldName,Name as Step,null as Ratio,null as FinishTag,null as FinishStatus,null as Remark from Project p1 
                          left join Income e1 on e1.PID = P1.ID 
                          group by p1.ID");
            //最外层
            sql.Append(" )");

            sql.Append(" where ParentFieldName in (" + PIDList + ")");
            sql.Append(" order by ParentFieldName,Step");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }
    }
}
