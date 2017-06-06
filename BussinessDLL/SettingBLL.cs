using CommonDLL;
using DomainDLL;
using DataAccessDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 页面：Setting
    /// Created:2017.03.28(ChengMengjia)
    /// </summary>
    public class SettingBLL
    {
        SettingDAO dao = new SettingDAO();

        /// <summary>
        /// 获取基础数据项的列表
        /// Created:2017.03.28(ChengMengjia)
        /// </summary>
        /// <param name="DictNo"></param>
        /// <returns></returns>
        public List<DictItem> GetDictItems(DictCategory DictNo)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "DictNo", Type = QueryFieldType.String, Value = (int)DictNo });
            SortField sf = new SortField() { Name = "No", Direction = SortDirection.Asc };
            return new Repository<DictItem>().GetList(qf, sf) as List<DictItem>;
        }


        /// <summary>
        /// 数据项子项的新增或修改
        /// Created:2017.03.28(ChengMengjia)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public JsonResult SaveItem(DictItem item)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "DictNo", Type = QueryFieldType.String, Value = item.DictNo });
                SortField sf = new SortField() { Name = "No", Direction = SortDirection.Asc };
                List<DictItem> listOld = new Repository<DictItem>().GetList(qf, sf) as List<DictItem>;
                if (listOld.Where(t => t.Name.Equals(item.Name)).Count() > 0)
                {
                    jsonreslut.result = false;
                    jsonreslut.msg = "内容重复，请修改！";
                }
                else
                {
                    if (string.IsNullOrEmpty(item.ID))
                    {
                        item.No = (listOld.Count() + 1).ToString();
                        new Repository<DictItem>().Insert(item);
                    }
                    else
                    {
                        DictItem old = new Repository<DictItem>().Get(item.ID);
                        old.Name = item.Name;
                        old.Remark = item.Remark;
                        new Repository<DictItem>().Update(old);
                    }
                    jsonreslut.result = true;
                    jsonreslut.msg = "保存成功！";
                }
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
        /// 获取周报收件人列表
        ///  Created:2017.04.10(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public IList<dynamic> GetSendToList(string ProjectID)
        {
            string configSendTo = GetSetting(ProjectID).WeeklySend;//配置里的发送人
            configSendTo = configSendTo == null ? "" : configSendTo;
            string QueryIDs = "";
            configSendTo.Split(';').ToList().ForEach(t => QueryIDs += string.IsNullOrEmpty(t) ? "" : "'" + t + "',");
            QueryIDs = QueryIDs.Length > 0 ? QueryIDs.Substring(0, QueryIDs.Length - 1) : "";
            if (string.IsNullOrEmpty(QueryIDs))
                return null;
            else return dao.GetMemberList(ProjectID, QueryIDs);
        }
        /// <summary>
        /// 获取周报抄送人列表
        ///  Created:2017.04.10(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public IList<dynamic> GetCCList(string ProjectID)
        {
            string configSendTo = GetSetting(ProjectID).WeeklyCC;//配置里的抄送人
            configSendTo = configSendTo == null ? "" : configSendTo;
            string QueryIDs = "";
            configSendTo.Split(';').ToList().ForEach(t => QueryIDs += string.IsNullOrEmpty(t) ? "" : "'" + t + "',");
            QueryIDs = QueryIDs.Length > 0 ? QueryIDs.Substring(0, QueryIDs.Length - 1) : "";
            if (string.IsNullOrEmpty(QueryIDs))
                return null;
            else return dao.GetMemberList(ProjectID, QueryIDs);
        }
        /// <summary>
        /// 获取项目配置
        ///  Created:2017.04.10(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public Setting GetSetting(string ProjectID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Asc };
            List<Setting> list = new Repository<Setting>().GetList(qf, sf) as List<Setting>;
            return list.Count > 0 ? list[0] : new Setting() { PID = ProjectID };
        }

        /// <summary>
        /// 保存项目配置
        ///  Created:2017.04.10(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveSetting(Setting entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Setting>().Insert(entity, false);
                else
                    new Repository<Setting>().Update(entity, false);
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
    }
}
