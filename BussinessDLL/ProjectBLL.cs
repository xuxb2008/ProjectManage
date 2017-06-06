using DomainDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessDLL;
using CommonDLL;
using System.Data;


namespace BussinessDLL
{
    /// <summary>
    /// 页面:Forms-Project-AddProject
    /// Created:2017.3.24(ChengMengjia)
    /// </summary>
    public class ProjectBLL
    {
        ProjectDao dao = new ProjectDao();
        /// <summary>
        /// 项目 新增和修改
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public JsonResult SaveProject(string id, string name, string no)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                #region 检查重名
                if (!UniqueName(name))
                {
                    jsonreslut.result = false;
                    jsonreslut.msg = "项目重名，请更改！";
                    return jsonreslut;
                }
                #endregion
                Project project = new Project() { ID = id, Name = name, No = no };
                string _id;
                if (string.IsNullOrEmpty(project.ID))
                {
                    project.ID = Guid.NewGuid().ToString();
                    project.Status = 1;
                    project.CREATED = DateTime.Now;
                    project.ProjectLastUpdate = DateTime.Now;
                    dao.Add(project, null,null, out _id);
                }
                else
                {
                    Project old = new Repository<Project>().Get(id);
                    old.Name = name;
                    old.No = no;
                    dao.Update(old, out _id);
                }
                jsonreslut.data = _id;
                jsonreslut.result = true;
                jsonreslut.msg = "保存成功！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
            }
            return jsonreslut;
        }

        /// <summary>
        /// （模板导入）项目和节点新增
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="listPNode"></param>
        /// <returns></returns>
        public JsonResult SaveProject(Project project, List<PNode> listPNode, List<DeliverablesJBXX> listJbxx)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                project.ProjectLastUpdate = DateTime.Now;
                dao.Add(project, listPNode,listJbxx, out _id);
                jsonreslut.data = _id;
                jsonreslut.result = true;
                jsonreslut.msg = "保存成功！";
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.BussinessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
            }
            return jsonreslut;
        }

        /// <summary>
        /// 检查项目重名
        ///  Created:20170531(ChengMengjia)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UniqueName(string name)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "Name", Value = name, Type = QueryFieldType.String });
            return new Repository<Project>().GetList(qf, null).Count == 0;
        }


        /// <summary>
        /// 获得项目路径
        /// Created：20170328(xuxb)
        /// </summary>
        /// <param name="proj ectId"></param>
        /// <returns></returns>
        public string GetProjectPath(string projectId)
        {
            string projectName = new Repository<Project>().Get(projectId).Name;
            return projectName + "\\";
        }
    }
}
