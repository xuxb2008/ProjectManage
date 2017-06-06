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
    /// 变更管理BLL
    /// 2017/04/18(zhuguanjun)
    /// </summary>
    public class ChangeBLL
    {
        /// <summary>
        /// 获取变更列表
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetChangeList(int Type,string PID)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = Type });
            return new ChangeDao().GetDataTable(qlist);
        }

        /// <summary>
        /// 获取文件列表
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="Type">变更类型</param>
        /// <param name="CID">变更的版本ID</param>
        /// <returns></returns>
        public DataTable GetChangeFilesList(int Type,string CID)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = Type });
            qlist.Add(new QueryField() { Name = "CID", Type = QueryFieldType.String, Value = CID });
            return new ChangeDao().GetFilesList(qlist);
        }

        public void GetChangeInfo(string CID,out Change change,out DataTable files)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField { Name = "CID", Type = QueryFieldType.String, Value = CID });
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            change = new ChangeDao().GetChange(qlist);
            files = new ChangeDao().GetFilesList(qlist);
        }

        /// <summary>
        /// 保存变更信息
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult Save(Change entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Change>().Insert(entity, true, out _id);
                else
                    new Repository<Change>().Update(entity, true, out _id);
                jsonreslut.result = true;
                jsonreslut.data = _id;
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
        /// 保存变更附件
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveFile(ChangeFiles entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<ChangeFiles>().Insert(entity, true, out _id);
                else
                    new Repository<ChangeFiles>().Update(entity, true, out _id);
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
