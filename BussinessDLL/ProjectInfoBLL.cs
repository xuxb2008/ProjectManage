using CommonDLL;
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
    /// 页面：ProjectInfo
    /// Created:20170328(ChengMengjia)
    /// </summary>
    public class ProjectInfoBLL
    {
        ProjectDao dao = new ProjectDao();

        #region 项目合同

        /// <summary>
        /// 项目合同基本信息 新增或修改
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="jbxx"></param>
        /// <returns></returns>
        public JsonResult SaveJBXX(ContractJBXX entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                {
                    new Repository<ContractJBXX>().Insert(entity, true, out _id);

                    List<Stakeholders> list = new StakeholdersBLL().GetList(entity.PID, null);//所有干系人
                    //干系人中项目经理为空
                    if (!string.IsNullOrEmpty(entity.B_PManager) && list.Where(t => t.IsPublic == 1 && t.Status == 1).Count() <= 0)
                    {
                        Stakeholders person = new Stakeholders();
                        person.PID = entity.PID;
                        person.Name = entity.B_PManager;
                        person.Tel = entity.B_PManagerTel;
                        person.Type = 2;//项目组
                        person.IsPublic = 1;
                        new Repository<Stakeholders>().Insert(person, true);
                    }
                    //干系人中客户判断有无此人
                    if (!string.IsNullOrEmpty(entity.A_Manager) && list.Where(t => t.Type == 1 && t.Status == 1 && t.Name == entity.A_Manager).Count() <= 0 )
                    {
                        Stakeholders person = new Stakeholders();
                        person.PID = entity.PID;
                        person.Name = entity.A_Manager;
                        person.Tel = entity.A_ManagerTel;
                        person.Type = 1;//客户
                        person.IsPublic = 0;
                        new Repository<Stakeholders>().Insert(person, true);
                    }
                    //干系人中项目组判断有无此人
                    if (!string.IsNullOrEmpty(entity.B_Manager) && list.Where(t => t.Type == 2 && t.Status == 1 && t.Name == entity.B_Manager).Count() <= 0)
                    {
                        Stakeholders person = new Stakeholders();
                        person.PID = entity.PID;
                        person.Name = entity.B_Manager;
                        person.Tel = entity.B_Tel;
                        person.Type = 2;//项目组
                        person.IsPublic = 0;
                        new Repository<Stakeholders>().Insert(person, true);
                    }
                }
                else
                    new Repository<ContractJBXX>().Update(entity, true, out _id);
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
        /// 项目合同周期 新增或修改
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="jbxx"></param>
        /// <returns></returns>
        public JsonResult SaveXMZQ(ContractXMZQ entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<ContractXMZQ>().Insert(entity, true, out _id);
                else
                    new Repository<ContractXMZQ>().Update(entity, true, out _id);
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
        /// 项目合同周期 新增或修改
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="jbxx"></param>
        /// <returns></returns>
        public JsonResult SaveQKMS(ContractQKMS entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<ContractQKMS>().Insert(entity, true, out _id);
                else
                    new Repository<ContractQKMS>().Update(entity, true, out _id);
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
        /// 根据PID获取-基本信息
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public ContractJBXX GetJBXX(string PID)
        {
            ContractJBXX entity = new ContractJBXX();
            if (!string.IsNullOrEmpty(PID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                List<ContractJBXX> list = new Repository<ContractJBXX>().GetList(qf, sf) as List<ContractJBXX>;
                if (list.Count > 0)
                    entity = list[0];
            }
            return entity;
        }

        /// <summary>
        /// 根据PID获取-项目周期
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public ContractXMZQ GetXMZQ(string PID)
        {
            ContractXMZQ entity = new ContractXMZQ();
            if (!string.IsNullOrEmpty(PID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                List<ContractXMZQ> list = new Repository<ContractXMZQ>().GetList(qf, sf) as List<ContractXMZQ>;
                if (list.Count > 0)
                    entity = list[0];
            }
            return entity;
        }

        /// <summary>
        /// 根据PID获取-情况描述
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public ContractQKMS GetQKMS(string PID)
        {
            ContractQKMS entity = new ContractQKMS();
            if (!string.IsNullOrEmpty(PID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                List<ContractQKMS> list = new Repository<ContractQKMS>().GetList(qf, sf) as List<ContractQKMS>;
                if (list.Count > 0)
                    entity = list[0];
            }
            return entity;
        }

        /// <summary>
        /// 文件保存
        ///  Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveFile(ContractFiles entity, bool ReUpload)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (entity.Type != 0)
                {
                    //不是其他文件
                    List<ContractFiles> listOld = GetFiles(entity.PID, entity.Type);
                    entity.ID = listOld.Count > 0 ? listOld[0].ID : "";
                }
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<ContractFiles>().Insert(entity, true, out _id);
                else
                {
                    ContractFiles old = new Repository<ContractFiles>().Get(entity.ID);
                    old.Name = entity.Name;
                    old.Desc = entity.Desc;
                    old.Path = ReUpload ? entity.Path : old.Path;
                    new Repository<ContractFiles>().Update(old, true, out _id);
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
        /// 文件获取
        ///  Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<ContractFiles> GetFiles(string PID, int? Type)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Comparison = QueryFieldComparison.eq, Value = PID });
            if (Type != null)
                qf.Add(new QueryField() { Name = "Type", Comparison = QueryFieldComparison.eq, Type = QueryFieldType.Numeric, Value = (int)Type });
            qf.Add(new QueryField() { Name = "Status", Comparison = QueryFieldComparison.eq, Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<ContractFiles> list = new Repository<ContractFiles>().GetList(qf, sf) as List<ContractFiles>;
            return list;
        }

        #endregion




        /// <summary>
        /// 取得项目成果
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectResult(string PID)
        {
            return dao.GetProjectResult(PID);
        }

        /// <summary>
        /// 取得项目风险
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectRisk(string PID)
        {
            return dao.GetProjectRisk(PID);
        }

        /// <summary>
        /// 取得项目问题
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectTrouble(string PID)
        {
            return dao.GetProjectTrouble(PID);
        }

        /// <summary>
        /// 项目近期工作和问题取得
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectLastWorkList(string PID)
        {
            int days = 7;
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    days = days + 6;
                    break;
                case DayOfWeek.Tuesday:
                    days = days + 5;
                    break;
                case DayOfWeek.Wednesday:
                    days = days + 4;
                    break;
                case DayOfWeek.Thursday:
                    days = days + 3;
                    break;
                case DayOfWeek.Friday:
                    days = days + 2;
                    break;
                case DayOfWeek.Saturday:
                    days = days + 1;
                    break;
            }
            return dao.GetProjectLastWorkList(PID, days);
        }

        /// <summary>
        /// 预警内容取得
        /// Created:20170417(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="openFlg"></param>
        /// <param name="projectUpdateDays"></param>
        /// <param name="publishUpdateDays"></param>
        /// <returns></returns>
        public DataTable GetProjectWarnning(string PID, bool[] openFlg, int projectUpdateDays, int publishUpdateDays)
        {
            return dao.GetProjectWarnning(PID, openFlg, projectUpdateDays, publishUpdateDays);
        }
    }
}
