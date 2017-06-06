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
    public class MilestoneBLL
    {
        MilestoneDAO dao = new MilestoneDAO();

       /// <summary>
        /// 里程碑列表
        /// Created:20170327(ChengMengjia)
       /// </summary>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <param name="PID"></param>
       /// <returns></returns>
        public GridData GetLCBList(int PageIndex, int PageSize,string PID)
        {
            return dao.GetLCBList(PageIndex,PageSize,PID);
        }

        public DataTable GetLCBList(string StartDate, string EndDate, string PID)
        {
            return dao.GetLCBList( StartDate, EndDate, PID);
        }


        /// <summary>
        /// 里程碑的新增和修改
        /// Created:20170327(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveLCB(Milestones entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Milestones>().Insert(entity, true);
                else
                    new Repository<Milestones>().Update(entity, true);
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
