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
    /// 供应商报表BLL
    /// 2017/05/16(zhuguanjun)
    /// </summary>
    public class ReportSupplierBLL
    {
        /// <summary>
        /// 获取项目列表
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
        public DataTable GetSupplier(List<string> pids, Dictionary<string, string> dic)
        {
            return new ReportSupplierDao().GetSupplier(pids, dic);
        }
    }
}
