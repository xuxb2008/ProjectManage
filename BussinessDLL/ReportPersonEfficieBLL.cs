using DataAccessDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 人员执行效率
    /// 2017/07/06(zhuguanjun)
    /// </summary>
    public class ReportPersonEfficieBLL
    {
        /// <summary>
        /// 获取成员执行效率
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataTable GetPersonEfficiency(string projectId, DateTime s, DateTime e, int finishStatus)
        {
            return new ReportPersonEfficiencyDao().GetPersonEfficiency(projectId, s, e, finishStatus);
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
