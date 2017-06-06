using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
   public class ReportDAO
    {

        /// <summary>
        /// 周报发送记录新增
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="node"></param>
        /// <param name="id"></param>
        public JsonResult Add_RptWeekly(Report_Weekly entity, List<Report_WeeklyFiles> list)
        {
            JsonResult jsonreslut = new JsonResult();
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                entity.ID = Guid.NewGuid().ToString();
                entity.Status = 1;
                entity.CREATED = DateTime.Now;
                s.Save(entity);
                list.ForEach(t =>
                {
                    t.ID = Guid.NewGuid().ToString();
                    t.ReportID = entity.ID;
                    t.Status = 1;
                    t.CREATED = DateTime.Now;
                    s.Save(t);
                });

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
