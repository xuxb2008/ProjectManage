using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;

namespace BussinessDLL
{
    /// <summary>
    /// 收入计划及完成情况
    /// Created By ZhuGJ
    /// Date 2017/03/24
    /// </summary>
    public class EarningBLL
    {
        //保存
        public JsonResult SaveIncome(Income income)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string id;
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(income.ID))
                    new Repository<Income>().Insert(income, true, out id);
                else
                    new Repository<Income>().Update(income, true, out id);
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
        /// <returns></returns>
        public List<Income> GetPageList(int pageSize, int pageIndex, List<QueryField> queryList, SortField sort, out int recordCount)
        {
            List<Income> li = new Repository<Income>().GetList(pageSize, pageIndex, queryList, sort, out recordCount) as List<Income>;
            return li;
        }

        /// <summary>
        /// 获取全部信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetIncomeList(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            StringBuilder sql = new StringBuilder();
            sql.Append(" select i.*,d.Name as FinishStatusName from Income i");
            sql.Append(" left join DictItem d on d.No = i.FinishStatus and d.DictNo = " + (int)DictCategory.EarningStatus);
            sql.Append(" where PID=@PID and Status=@Status");
            sql.Append(" order by Created");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return dt;
        }

    }
}
