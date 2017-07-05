using DataAccessDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 成员贡献率Bll
    /// 2017/06/07(zhuguanjun)
    /// </summary>
    public class ReportMemberRateBLL
    {
        /// <summary>
        /// 获取成员贡献率
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public DataTable GetMemberRate(string projectId,DateTime s,DateTime e,int finishStatus)
        {
            return new ReportMemberRateDao().GetMemberRate(projectId,s,e, finishStatus);
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
