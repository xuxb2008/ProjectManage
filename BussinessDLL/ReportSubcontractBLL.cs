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
    /// 分包合同报表
    /// 2017/05/11(zhuguanjun)
    /// </summary>
    public class ReportSubcontractBLL
    {
        /// <summary>
        /// 获取项目列表
        /// 2017/05/11(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProject()
        {
            return new Repository<Project>().GetList(null, null) as List<Project>;
        }

        /// <summary>
        /// 获取分包合同
        /// 2015/05/15(zhuguanjun)
        /// </summary>
        /// <param name="pids"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable GetSubcontract(List<string> pids,Dictionary<string,string> dic)
        {
            return new ReportSubcontractDao().GetSubcontract(pids,dic);
        }

    }
}
