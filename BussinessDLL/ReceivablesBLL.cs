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
    public class ReceivablesBLL
    {
        ReceivablesDAO dao = new ReceivablesDAO();

        /// <summary>
        /// 收款列表
        /// Created:20170327(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public GridData GetSKList(int PageIndex, int PageSize, string PID)
        {
            return dao.GetSKList(PageIndex, PageSize, PID);
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        public List<Receivables> GetPageList(int pageSize, int pageIndex, List<QueryField> queryList, SortField sort, out int recordCount)
        {
            List<Receivables> li = new Repository<Receivables>().GetList(pageSize, pageIndex, queryList, sort, out recordCount) as List<Receivables>;
            return li;
        }

        /// <summary>
        /// 收款的新增和修改
        /// Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveSK(Receivables entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<Receivables>().Insert(entity, true);
                else
                    new Repository<Receivables>().Update(entity, true);
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
    }
}
