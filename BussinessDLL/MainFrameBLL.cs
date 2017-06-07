using DataAccessDLL;
using DomainDLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 页面：MainFrame
    /// Created:2017.3.24(ChengMengjia)
    /// </summary>
    public class MainFrameBLL
    {
        /// <summary>
        /// 获取所有有效项目
        /// Created:2017.3.24(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProList()
        {
            List<QueryField> qf = new List<QueryField>();
            //qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "ProjectLastUpdate", Direction = SortDirection.Desc };
            return new Repository<Project>().GetList(qf, sf) as List<Project>;
        }


    }
}
