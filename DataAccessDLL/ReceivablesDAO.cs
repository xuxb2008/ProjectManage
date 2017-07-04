using CommonDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class ReceivablesDAO
    {
        /// <summary>
        /// 收款列表
        /// Created:20170327(ChengMengjia)
        /// Updated:20170405(ChengMengjia) liuxx要求去除分页
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public GridData GetSKList(int PageIndex, int PageSize, string PID)
        {
            //List<QueryField> qf = new List<QueryField>();
            //StringBuilder sqlHead = new StringBuilder();
            //sqlHead.Append(" select r.id,r.BatchNo,r.Ratio,r.FinishStatus,r.Amount,r.Condition,r.Remark,");
            //sqlHead.Append(" strftime('%Y-%m-%d',r.InDate)InDate,d1.Name FinishStatusName ");
            //StringBuilder sqlBody = new StringBuilder();
            //sqlBody.Append(" from Receivables r ");
            //sqlBody.Append(" left join DictItem d1 on d1.DictNo=" + (int)DictCategory.Receivables_FinshStatus + " and r.FinishStatus=d1.No ");
            //sqlBody.Append(" where r.PID=@PID  and r.status=1 order by r.updated desc,r.created asc");
            //qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            //GridData result = NHHelper.GetGridData(PageIndex, PageSize, sqlHead.ToString(), sqlBody.ToString(), qf);
            //return result;
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.id,r.BatchNo,r.Ratio,r.FinishStatus,r.Amount,r.Condition,r.Remark,");
            sql.Append(" strftime('%Y-%m-%d',r.InDate)InDate,d1.Name FinishStatusName ");
            sql.Append(" from Receivables r ");
            sql.Append(" left join DictItem d1 on d1.DictNo=" + (int)DictCategory.Receivables_FinshStatus + " and r.FinishStatus=d1.No ");
            sql.Append(" where r.PID=@PID  and r.status=1 order by r.CREATED");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            GridData result = new GridData();
            result.data = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return result;
        }
    }
}
