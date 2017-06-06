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
    /// 风险管理BLL
    /// 017.03.31(zhuguanjun)
    /// </summary>
    public class RiskBLL
    {
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Risk Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new Risk();
            return new Repository<Risk>().Get(id);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="risk"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public JsonResult SaveRisk(Risk risk, out string _id)
        {
            _id = "";
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(risk.ID))
                    new Repository<Risk>().Insert(risk, true, out _id);
                else
                    new Repository<Risk>().Update(risk, true, out _id);
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
        /// 获取列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public GridData GetGridData(int pageIndex, int pageSize, string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });

            return new RiskDao().GetGridData(pageIndex, pageSize, qf);
        }
    }
}
