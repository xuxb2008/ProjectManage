using DataAccessDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 项目计划报表BLL
    /// 2017/04/20(zhuguanjun)
    /// </summary>
    public class ReportPlanBLL
    {
        /// <summary>
        /// 获取
        /// 2017/04/24(zhuguanjun)
        /// 2017/05/10(zhuguanjun)增加PID
        /// </summary>
        /// <param name="StarteDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="PType"></param>
        /// <param name="Manager"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataTable GetPlan(DateTime StarteDate, DateTime EndDate, int PType, string Manager,string projectId)
        {
            if (Manager != string.Empty)
            {
                Manager = Manager.Substring(0, 36);
            }
            return new ReportPlanDao().GetPlan(StarteDate, EndDate, PType, Manager, projectId);
        }

        /// <summary>
        /// 获取干系人
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public List<Stakeholders> GetStakeholderItems(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });

            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            return new Repository<Stakeholders>().GetList(qf, sf) as List<Stakeholders>;
        }
    }
}
