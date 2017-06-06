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
    /// 类名：日常工作业务类
    /// Created：2017.03.30(Xuxb)
    /// updated:2017/06/06(zhuguanjun) mark:日常工作添加责任人列表
    /// </summary>
    public class RoutineBLL
    {
        private RoutineDAO dao = new RoutineDAO();

        /// <summary>
        /// 日常工作保存
        /// 2017/06/01(zhuguanjun)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listWork"></param>
        /// <returns></returns>
        public JsonResult SaveRoutine(Routine entity, List<RoutineWork> listWork)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                //如果是新增
                if (string.IsNullOrEmpty(entity.ID))
                    dao.AddRoutine(entity, listWork);
                //编辑
                else
                {
                    dao.UpdateRoutine(entity, listWork);
                }
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
        /// 日常工作保存
        ///  Created:20170330(Xuxb)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveRoutine(Routine entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Routine>().Insert(entity, true, out _id);
                else
                    new Repository<Routine>().Update(entity, true, out _id);
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
        /// 日常工作文件保存
        ///  Created:20170330(Xuxb)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveRoutineFile(RoutineFiles entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<RoutineFiles>().Insert(entity, true, out _id);
                else
                    new Repository<RoutineFiles>().Update(entity, true, out _id);
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
        /// 根据ID获取-日常工作基本信息
        /// Created:20170330(Xuxb)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Routine GetRoutineObject(string ID)
        {
            Routine entity = new Routine();
            if (!string.IsNullOrEmpty(ID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "ID", Type = QueryFieldType.String, Value = ID.Substring(0, 36) + "%", Comparison = QueryFieldComparison.like });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                entity = new Repository<Routine>().FindSingle(qf) as Routine;
            }
            return entity;
        }

        /// <summary>
        /// 根据查询条件获取-日常工作列表
        /// Created:20170330(Xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetRoutinList(string PID, string startDate, string endDate, string key)
        {
            return dao.GetRoutinList(PID, startDate, endDate, key);
        }

        /// <summary>
        /// 根据RountineID获取-日常工作文件
        /// Created:20170330(Xuxb)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<RoutineFiles> GetRoutineFiles(string RountineID)
        {
            List<RoutineFiles> list = new List<RoutineFiles>();
            if (!string.IsNullOrEmpty(RountineID))
            {
                List<QueryField> qf = new List<QueryField>();
                qf.Add(new QueryField() { Name = "RoutineID", Type = QueryFieldType.String, Value = RountineID.Substring(0,36) });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
                list = new Repository<RoutineFiles>().GetList(qf, sf) as List<RoutineFiles>;
            }
            return list;
        }

        /// <summary>
        /// 获取日常工作负责人列表
        /// 2017/06/01(zhuguanjun)
        /// </summary>
        /// <param name="RoutineID"></param>
        /// <returns></returns>
        public DataTable GetRoutinWorkList(string RoutineID)
        {
            //List<QueryField> qf = new List<QueryField>();
            //qf.Add(new QueryField() { Name = "RoutineID", Type = QueryFieldType.String, Value = RoutineID.Substring(0, 36) });
            //return new Repository<RoutineWork>().GetList(qf, null);
            return new RoutineDAO().GetRoutinWorkList(RoutineID.Substring(0, 36));
        }
    }
}
