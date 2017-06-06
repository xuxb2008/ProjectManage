using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class SupervisorDAO
    {
       /// <summary>
        /// 监理评价列表
        /// Created:20170328(ChengMengjia)
       /// </summary>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <param name="PID"></param>
       /// <returns></returns>
        public GridData GetJLPJList(int PageIndex, int PageSize, string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            string sqlHead = " select r.id,r.Name,r.Content,strftime('%Y-%m-%d',r.JudgeDate)JudgeDate ";
            StringBuilder sqlBody = new StringBuilder();
            sqlBody.Append(" from SupervisorJudge r ");
            sqlBody.Append(" where r.PID=@PID  and r.status=1 order by r.updated desc,r.created asc");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.GetGridData(PageIndex, PageSize, sqlHead, sqlBody.ToString(), qf);
        }
    }
}
