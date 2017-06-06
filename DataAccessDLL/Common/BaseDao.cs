using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    public class BaseDao
    {
        /// <summary>
        /// 项目更新时间
        /// Created:20170526(ChengMengjia)
        /// </summary>
        protected void UpdateProject(ISession s)
        {
            string pid = CacheHelper.GetProjectID();
            if (string.IsNullOrEmpty(pid))
                return;
            Project project = new Repository<Project>().Get(pid);
            if (project == null)
                return;
            project.ProjectLastUpdate = DateTime.Now;
            s.Update(project);
        }
    }
}
