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
    /// 类名：项目问题业务类
    /// Created：2017.04.06(Xuxb)
    /// </summary>
    public class TroubleBLL
    {
        private TroubleDAO dao = new TroubleDAO();

        /// <summary>
        /// 项目问题保存
        ///  Created:2017.04.06(Xuxb)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveTrouble(Trouble entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Trouble>().Insert(entity, true, out _id);
                else
                    new Repository<Trouble>().Update(entity, true, out _id);
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listWork"></param>
        /// <returns></returns>
        //public JsonResult SaveTrouble(Routine entity, List<RoutineWork> listWork)
        //{
        //    JsonResult jsonreslut = new JsonResult();
        //    try
        //    {
        //        如果是新增
        //        if (string.IsNullOrEmpty(entity.ID))
        //            dao.AddRoutine(entity, listWork);
        //        编辑
        //        else
        //        {
        //            dao.UpdateRoutine(entity, listWork);
        //        }
        //        jsonreslut.result = true;
        //        jsonreslut.msg = "保存成功！";
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteException(ex, LogType.BussinessDLL);
        //        jsonreslut.result = false;
        //        jsonreslut.msg = ex.Message;
        //    }
        //    return jsonreslut;
        //}

        /// <summary>
        /// 项目问题文件保存
        ///  Created:2017.04.06(Xuxb)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveTroubleFile(TroubleFiles entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<TroubleFiles>().Insert(entity, true, out _id);
                else
                    new Repository<TroubleFiles>().Update(entity, true, out _id);
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
        /// 根据ID获取-项目问题基本信息
        /// Created:2017.04.06(Xuxb)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Trouble GetTroubleObject(string ID)
        {
            Trouble entity = new Trouble();
            if (!string.IsNullOrEmpty(ID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "ID", Type = QueryFieldType.String, Value = ID.Substring(0, 36) + "%", Comparison = QueryFieldComparison.like });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                entity = new Repository<Trouble>().FindSingle(qf) as Trouble;
            }
            return entity;
        }

        /// <summary>
        /// 根据查询条件获取-项目问题列表
        /// Created:2017.04.06(Xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, string key)
        {
            return dao.GetTroubleList(PID, startDate, endDate, key);
        }

        /// <summary>
        /// 根据查询条件获取-项目问题列表
        /// Created:2017.04.21(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, int? Status)
        {
            return dao.GetTroubleList(PID, startDate, endDate, Status);
        }

        /// <summary>
        /// 根据RountineID获取-项目问题文件
        /// Created:2017.04.06(Xuxb)
        /// Updated:2017.04.25(Xuxb) 追加文件类型
        /// </summary>
        /// <param name="TroubleID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<TroubleFiles> GetTroubleFiles(string TroubleID, int type)
        {
            List<TroubleFiles> list = new List<TroubleFiles>();
            if (!string.IsNullOrEmpty(TroubleID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "TroubleID", Type = QueryFieldType.String, Value = TroubleID.Substring(0, 36) });
                qf.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = type });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                list = new Repository<TroubleFiles>().GetList(qf, sf) as List<TroubleFiles>;
            }
            return list;
        }
    }
}
