using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 干系人矩阵Dao
    /// 2017.03.29(zhuguanjun)
    /// </summary>
    public class CommunicationMatrixDao
    {
        private ISession _session = null;
        protected virtual ISession Session
        {

            get
            {
                if (_session == null)
                {
                    return NHHelper.GetCurrentSession();
                }
                else
                {
                    return _session;
                }
            }
        }

        /// <summary>
        /// 获取干系人列表(无版本号的ID+有版本号的NAME)
        /// </summary>
        /// <param name="qf"></param>
        /// <returns></returns>
        public DataTable GetDataTable(List<QueryField> qf)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select s1.ID,s2.Name||'('||s2.CompanyName||')' as Name,s2.CompanyName || '-' || s2.Name as showName,s2.IsPublic from stakeholders s1, stakeholders s2");
            sql.Append(" where substr(s1.ID, 38) = '1' and substr(s1.ID, 1, 37) = substr(s2.ID, 1, 37)");
            sql.Append(" and s2.PID=@PID  and s2.status=@Status order by s2.updated desc,s2.created desc");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return dt;
        }

        /// <summary>
        /// 点击干系人列表加载干系人信息和干系人沟通方式列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="stakeholders">干系人实体</param>
        /// <param name="communication">沟通方式集合</param>
        public void GetCommunicationMatix(List<QueryField> qf, out Stakeholders stakeholders, out List<CommunicationFXFA> FXFAlist)
        {
            stakeholders = new Stakeholders();
            FXFAlist = new List<CommunicationFXFA>();

            #region 干系人
            StringBuilder sql = new StringBuilder();
            sql.Append(" select * from stakeholders s where s.PID=@PID and substr(s.Id,1,37)||'1'=@Id and s.Status=@Status");
            sql.Append(" order by s.UPDATED desc , s.CREATED desc");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            stakeholders = dt == null ? new Stakeholders() : JsonHelper.TableToEntity<Stakeholders>(dt);

            #endregion

            #region 沟通方式列表
            StringBuilder sql2 = new StringBuilder();
            sql2.Append(" select fxfa.*,c.Name as CNAME from communicationFXFA fxfa left join communication c on c.Id = fxfa.CID");
            sql2.Append(" where fxfa.SID=@ID and c.PID=@PID and c.Status=@Status and fxfa.Status=@Status");
            sql2.Append(" order by fxfa.CREATED");
            DataSet ds2 = NHHelper.ExecuteDataset(sql2.ToString(), qf);
            var dt2 = new DataTable();
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                dt2 = ds2.Tables[0];
            }
            FXFAlist = JsonHelper.TableToList<CommunicationFXFA>(dt2).ToList();
            #endregion
        }

        /// <summary>
        /// 保存FXFA
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        public JsonResult SaveFXFA(List<CommunicationFXFA> list, out string id1, out string id2, out string id3)
        {
            id1 = "";
            id2 = "";
            id3 = "";
            JsonResult jsonreslut = new JsonResult { result = false};
            try
            {
                ISession s = Session;
                s.BeginTransaction();
                if (list != null && list.Count > 0)
                {
                    if (string.IsNullOrEmpty(list[0].ID))
                    {
                        if (list[0].Status == null)
                            list[0].Status = 1;
                        list[0].ID = Guid.NewGuid().ToString() + "-1";
                        id1 = list[0].ID;
                        list[0].CREATED = DateTime.Now;
                        s.Save(list[0]); 
                    }
                    else
                    {
                        CommunicationFXFA old = Session.Get<CommunicationFXFA>(list[0].ID);
                        old.UPDATED = DateTime.Now;
                        old.Status = 0;
                        s.Update(old);
                        string hisNo = list[0].ID.Substring(37);
                        list[0].ID = list[0].ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                        list[0].Status = 1;
                        list[0].CREATED = old.CREATED;
                        s.Save(list[0]);
                    }
                    if (list.Count > 1)
                    {

                        if (string.IsNullOrEmpty(list[1].ID))
                        {
                            if (list[1].Status == null)
                                list[1].Status = 1;
                            list[1].ID = Guid.NewGuid().ToString() + "-1";
                            id1 = list[1].ID;
                            list[1].CREATED = DateTime.Now;
                            s.Save(list[1]);
                        }
                        else
                        {
                            CommunicationFXFA old = Session.Get<CommunicationFXFA>(list[1].ID);
                            old.UPDATED = DateTime.Now;
                            old.Status = 0;
                            s.Update(old);
                            string hisNo = list[1].ID.Substring(37);
                            list[1].ID = list[1].ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                            list[1].Status = 1;
                            list[1].CREATED = old.CREATED;
                            s.Save(list[1]);
                        }
                    }
                    if (list.Count > 2)
                    {

                        if (string.IsNullOrEmpty(list[2].ID))
                        {
                            if (list[2].Status == null)
                                list[2].Status = 1;
                            list[2].ID = Guid.NewGuid().ToString() + "-1";
                            id1 = list[2].ID;
                            list[2].CREATED = DateTime.Now;
                            s.Save(list[2]);
                        }
                        else
                        {
                            CommunicationFXFA old = Session.Get<CommunicationFXFA>(list[2].ID);
                            old.UPDATED = DateTime.Now;
                            old.Status = 0;
                            s.Update(old);
                            string hisNo = list[2].ID.Substring(37);
                            list[2].ID = list[2].ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                            list[2].Status = 1;
                            list[2].CREATED = old.CREATED;
                            s.Save(list[2]);
                        }
                    }
                }

                s.Transaction.Commit();
                s.Close();
                jsonreslut.result = true;
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
