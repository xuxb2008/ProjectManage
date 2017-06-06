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
    /// 干系人沟通方式矩阵
    /// 2017.03.28(zhuguanjun)
    /// </summary>
    public class CommunicationMatrixBLL
    {
        /// <summary>
        /// 获取干系人列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            DataTable dt = new CommunicationMatrixDao().GetDataTable(qf);
            return dt;
        }

        /// <summary>
        /// 获取干系人详情及交流方式
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="stakeholdersId"></param>
        /// <param name="stakeholders"></param>
        /// <param name="communicationList"></param>
        public void GetCommunicationMatix(string PID, string stakeholdersId, out Stakeholders stakeholders, out List<CommunicationFXFA> communicationList)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qf.Add(new QueryField() { Name = "Id", Type = QueryFieldType.String, Value = stakeholdersId });
            new CommunicationMatrixDao().GetCommunicationMatix(qf, out stakeholders, out communicationList);
        }

        /// <summary>
        /// 保存沟通方式
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="id3"></param>
        /// <returns></returns>
        public JsonResult SaveFXFA(List<CommunicationFXFA> list, out string id1,out string id2,out string id3)
        {
            id1 = "";
            id2 = "";
            id3 = "";
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut = new CommunicationMatrixDao().SaveFXFA(list, out id1, out id2, out id3);
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
        /// 获取沟通方式的列表
        /// Created:2017.03.30(Zhuguanjun)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public List<Communication> GetCommunicationItems(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });

            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            return new Repository<Communication>().GetList(qf, sf) as List<Communication>;
        }

    }
}
