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
    /// 项目基本信息报表
    /// 2017/05/17(zhuguanjun)
    /// </summary>
    public class ReportProjectBLL
    {
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProjects()
        {
            List<QueryField> qf = new List<QueryField>();;
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<Project> list = new Repository<Project>().GetList(qf, sf) as List<Project>;
            return list;
        }
    }
}
