using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class PubInfoDAO:BaseDao
    {

        /// <summary>
        /// 周报发送记录新增
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="node"></param>
        /// <param name="id"></param>
        public JsonResult AddPubInfo(PubInfo entity, List<PubInfoFiles> list)
        {
            JsonResult jsonreslut = new JsonResult();
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Save(entity);
                list.ForEach(t =>
                {
                    s.Save(t);
                });
                UpdateProject(s);
                s.Transaction.Commit();
                s.Close();
                jsonreslut.result =true ;
                jsonreslut.msg = "操作成功！";
                jsonreslut.data = entity.ID;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.DataAccessDLL);
                jsonreslut.result = false;
                jsonreslut.msg = ex.Message;
                s.Transaction.Rollback();
                s.Close();
            }
            return jsonreslut;
        }
    }
}
