using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// 沟通方式管理
    /// Author:ZHUGUANJUN
    /// AT:2017/03/24
    /// </summary>
    public class CommunicationBLL
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="communication"></param>
        /// <returns></returns>
        public JsonResult SaveCommunication(Communication communication)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(communication.ID))
                    new Repository<Communication>().Insert(communication, true, out _id);
                else
                    new Repository<Communication>().Update(communication, true, out _id);
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

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            new Repository<Cost>().Delete(id);
        }

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        public List<Cost> GetAll()
        {
            List<Cost> li = new Repository<Cost>().GetAll() as List<Cost>;
            return li;
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        public List<Communication> GetPageList(int pageSize, int pageIndex, string PID, out int recordCount)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Asc };

            List<Communication> li = new Repository<Communication>().GetList(pageSize, pageIndex, qf, sf, out recordCount) as List<Communication>;
            return li;
        }
    }
}
