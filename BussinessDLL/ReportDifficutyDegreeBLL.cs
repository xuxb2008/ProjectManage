using DataAccessDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 工作困难系数
    /// 2017/07/07(zhuguanjun)
    /// </summary>
    public class ReportDifficutyDegreeBLL
    {
        public DataTable GetDefficutyDegree(string projectId, DateTime s, DateTime e, int finishStatus)
        {
            return new ReportDefficutyDegreeDao().GetDefficutyDegree(projectId, s, e, finishStatus);
        }

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DomainDLL.Project GetProject(string projectId)
        {
            return new Repository<DomainDLL.Project>().Get(projectId);
        }
    }
}
