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
    /// 收入报表BLL
    /// 2017/04/25(zhuguanjun)
    /// </summary>
    public class ReportEarningBLL
    {
        /// <summary>
        /// 获取项目列表
        /// 2017/04/25(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProject()
        {
            return new Repository<Project>().GetList(null,null) as List<Project>;
        }

        /// <summary>
        /// 获取收入列表
        /// </summary>
        /// <param name="pids"></param>
        /// <returns></returns>
        public DataTable GetEarning(List<string> pids)
        {            
            return new ReportEarningDao().GetEarning(pids);
        }
    }
}
