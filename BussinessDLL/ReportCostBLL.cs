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
    /// 成本分配报表BLL
    /// 2017/04/28(zhuguanjun)
    /// </summary>
    public class ReportCostBLL
    {
        /// <summary>
        /// 获取项目列表
        /// 2017/04/28(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProject()
        {
            return new Repository<Project>().GetList(null, null) as List<Project>;
        }

        /// <summary>
        /// 获取成本列表
        /// 2017/04/28(zhuguanjun)
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public DataTable GetEarning(List<string> pids)
        {
            return new ReportCostDao().GetCost(pids);
        }

    }
}
