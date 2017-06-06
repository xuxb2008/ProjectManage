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
    /// 干系人列表
    /// Author:ZHUGUANJUN
    /// AT:2017/03/24
    public class StakeholdersBLL
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="stakehoders"></param>
        /// <returns></returns>
        public JsonResult SaveStakehoders(Stakeholders stakehoders, out string _id)
        {
            _id = "";
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(stakehoders.ID))
                    new Repository<Stakeholders>().Insert(stakehoders, true, out _id);
                else
                    new Repository<Stakeholders>().Update(stakehoders, true, out _id);
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
        /// 获取分页信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public GridData GetGridData(int pageSize, int pageIndex, string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });

            return new StakeholdersDao().GetGridData(pageSize, pageIndex, qf);
        }

        /// <summary>
        /// 干系人清单列表获取
        /// Created:20170331(ChengMengjia)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public List<Stakeholders> GetList(string ProjectID, int? SendType)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            if (SendType != null)
                qf.Add(new QueryField() { Name = "SendType", Type = QueryFieldType.Numeric, Value = SendType });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<Stakeholders> list = new Repository<Stakeholders>().GetList(qf, sf) as List<Stakeholders>;
            return list;

        }

        /// <summary>
        ///项目经理列表获取
        /// Created:20170525(ChengMengjia)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public List<Stakeholders> GetPMList(string ProjectID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            qf.Add(new QueryField() { Name = "IsPublic", Type = QueryFieldType.Numeric, Value = 1 });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<Stakeholders> list = new Repository<Stakeholders>().GetList(qf, sf) as List<Stakeholders>;
            return list;

        }

        /// <summary>
        /// 干系人清单列表获取
        /// Created:20170525(ChengMengjia)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public List<Stakeholders> GetListByType(string ProjectID, int Type)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            qf.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = Type });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<Stakeholders> list = new Repository<Stakeholders>().GetList(qf, sf) as List<Stakeholders>;
            return list;

        }
    }
}
