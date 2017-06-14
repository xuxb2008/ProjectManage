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
    /// 供应商管理BLL
    /// 2017/4/10(zhuguanjun)
    /// </summary>
    public class SupplierBLL
    {
        //保存
        public JsonResult SaveSupplier(Supplier supperlier, out string _id)
        {
            _id = "";
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                if (string.IsNullOrEmpty(supperlier.ID))
                    new Repository<Supplier>().Insert(supperlier, true, out _id);
                else
                    new Repository<Supplier>().Update(supperlier, true, out _id);
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
        /// 获取供应商列表
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetSupplierList(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            return new SupplierDao().GetSupplierList(qf);
        }

        /// <summary>
        /// 获取供应商
        /// 2017/06/13(zhuguanjun)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DomainDLL.Supplier GetSupplier(string ID)
        {
            try
            {
                var supplier = new Repository<DomainDLL.Supplier>().Get(ID);
                return supplier;
            }
            catch
            {
                return null;
            }
        } 
    }
}
