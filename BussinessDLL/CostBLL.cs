using DomainDLL;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessDLL;
using CommonDLL;
using System.Data;

namespace BussinessDLL
{
    /// <summary>
    /// 项目成本管理
    /// Author:ZhuGJ
    /// Created:2017.03.23
    /// </summary>
    public class CostBLL
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public JsonResult SaveCost(Cost cost)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(cost.ID))
                    new Repository<Cost>().Insert(cost, true, out _id);
                else
                    new Repository<Cost>().Update(cost, true, out _id);
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
        /// 获取全部信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetCostList(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            StringBuilder sql = new StringBuilder();
            sql.Append(" select Tag, Total,(Used+Transit) as Used,transit ,Remaining,id,created,Explanation from Cost");
            sql.Append(" where PID=@PID and Status=@Status");
            sql.Append(" order by CREATED");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return dt;
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        public List<Cost> GetPageList(int pageSize, int pageIndex, List<QueryField> queryList, SortField sort, out int recordCount)
        {
            List<Cost> li = new Repository<Cost>().GetList(pageSize, pageIndex, queryList, sort, out recordCount) as List<Cost>;
            return li;
        }
    }
}
