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
    public class SupervisorBLL
    {
        SupervisorDAO dao = new SupervisorDAO();

        /// <summary>
        /// 监理信息的新增和修改
        /// Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveJLXX(Supervisor entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Supervisor>().Insert(entity, true, out _id);
                else
                    new Repository<Supervisor>().Update(entity, true, out _id);
                jsonreslut.result = true;
                jsonreslut.data = _id;
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
        /// 监理评价的新增和修改
        /// Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveJLPJ(SupervisorJudge entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<SupervisorJudge>().Insert(entity, true);
                else
                    new Repository<SupervisorJudge>().Update(entity, true);
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
        /// 根据PID获取-监理信息
        /// Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public Supervisor GetJLXX(string PID)
        {
            Supervisor entity = new Supervisor();
            if (!string.IsNullOrEmpty(PID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                List<Supervisor> list = new Repository<Supervisor>().GetList(qf, sf) as List<Supervisor>;
                if (list.Count > 0)
                    entity = list[0];
            }
            return entity;
        }

       /// <summary>
        /// 监理评价列表
        /// Created:20170328(ChengMengjia)
       /// </summary>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <param name="PID"></param>
       /// <returns></returns>
        public GridData GetJLPJList(int PageIndex, int PageSize, string PID)
        {
            return dao.GetJLPJList(PageIndex,PageSize,PID);
        }
    }
}
