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
    /// 收款报表
    /// 2017/04/28(zhuguanjun)
    /// </summary>
    public class ReportReceivablesDao
    {
        /// <summary>
        /// 获取收款报表信息
        /// 2017/04/28(zhuguanjun)
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public DataTable GetReceivables(List<string> pids)
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
            sql.AppendFormat(@" select e.ID as KeyFieldName,e.PID as ParentFieldName,e.BatchNo,e.Ratio,d.Name as FinishStatus,e.Amount,e.Condition,e.Remark,e.Indate from Receivables e
                          left join Project p on e.PID = P.ID 
                          left join DictItem d on d.No = e.FinishStatus and d.DictNo ={0} 
                          where e.Status = @Status and p.ID is not null", (int)DictCategory.Receivables_FinshStatus);
            //交合
            sql.Append(" union");
            //查询项目
            sql.Append(@" select distinct(p1.ID) as KeyFieldName,p1.ID as ParentFieldName,Name as BatchNo,null as Ratio,null as FinishStatus,null as Amount,null as Condition,null as Remark,null as Indate from Project p1 
                          left join Receivables e1 on e1.PID = P1.ID
                          group by p1.ID");
            //最外层
            sql.Append(" )");

            sql.Append(" where ParentFieldName in (" + PIDList + ")");
            sql.Append(" order by ParentFieldName,BatchNo");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }
    }
}
