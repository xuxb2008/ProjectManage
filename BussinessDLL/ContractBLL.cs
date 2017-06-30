using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BussinessDLL
{

    /// <summary>
    /// 分包合同管理BLL
    /// 2017/4/11(zhuguanjun)
    /// </summary>
    public class ContractBLL
    {
        /// <summary>
        /// 保存分包信息
        /// 2017/04/12(zhuguanjun)
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="dicFile"></param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public JsonResult SaveSubContract(SubContract contract, Dictionary<int, string> dicFile, out string _id)
        {
            return new ContractDao().SaveSubContract(contract, dicFile, out _id);
        }

        /// <summary>
        /// 根据分包合同和合同类型获取合同文件(附件)
        /// 2017/04/12(zhuguanjun)
        /// </summary>
        /// <param name="SubID">分包合同主表ID(版本id)</param>
        /// <param name="Type">分包合同类型</param>
        /// <returns></returns>
        public List<SubContractFiles> GetFiles(string SubID, int? Type)
        {
            if (string.IsNullOrEmpty(SubID))
                return new List<SubContractFiles>();
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "SubID", Type = QueryFieldType.String, Comparison = QueryFieldComparison.eq, Value = SubID.Substring(0, 36) });
            if (Type != null)
                qf.Add(new QueryField() { Name = "Type", Comparison = QueryFieldComparison.eq, Type = QueryFieldType.Numeric, Value = (int)Type });
            qf.Add(new QueryField() { Name = "Status", Comparison = QueryFieldComparison.eq, Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Desc };
            List<SubContractFiles> list = new Repository<SubContractFiles>().GetList(qf, sf) as List<SubContractFiles>;
            return list;
        }

        /// <summary>
        /// 获取分包合同列表信息
        /// 2017/04/12
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetContractList(string PID)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            return new ContractDao().GetContractList(qlist);
        }

        /// <summary>
        /// 获取分包合同信息、附件信息、里程碑信息、付款信息
        /// 2017/04/13
        /// </summary>
        /// <param name="id">分包合同主表ID</param>
        /// <param name="subContract">返回合同信息</param>
        /// <param name="files">返回附件集合</param>
        /// <param name="LCB">返回里程碑集合</param>
        /// <param name="SKXX">返回付款信息集合</param>
        public void GetSubContractAll(string id, out SubContract subContract, out DataTable files, out DataTable LCB, out DataTable SKXX)
        {
            new ContractDao().GetSubContractAll(id, out subContract, out files, out LCB, out SKXX);
        }

        /// <summary>
        /// 保存里程碑
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="lcb"></param>
        /// <returns></returns>
        public JsonResult SaveLCB(SubContractLCB lcb)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(lcb.ID))
                    new Repository<SubContractLCB>().Insert(lcb, true, out _id);
                else
                    new Repository<SubContractLCB>().Update(lcb, true, out _id);
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
        /// 保存付款信息
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="skxx"></param>
        /// <returns></returns>
        public JsonResult SaveSKXX(SubContractSKXX skxx)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                jsonreslut.result = false;
                string _id;
                if (string.IsNullOrEmpty(skxx.ID))
                    new Repository<SubContractSKXX>().Insert(skxx, true, out _id);
                else
                    new Repository<SubContractSKXX>().Update(skxx, true, out _id);
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
        /// 获取里程碑集合
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="SubID"></param>
        /// <returns></returns>
        public DataTable GetLCBList(string SubID)
        {
            return new ContractDao().GetLCBList(SubID);
        }

        /// <summary>
        /// 获取收款信息集合
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="SubID"></param>
        /// <returns></returns>
        public DataTable GetSKXXList(string SubID)
        {
            return new ContractDao().GetSKXXList(SubID);
        }

        /// <summary>
        /// 获取合作商列表
        /// 2017/05/03(zhuguanjun)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public List<Supplier> GetSupplierList(string PID)
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            var dt = new ContractDao().GetSupplierList(qlist);
            if (dt != null && dt.Rows != null && dt.Rows.Count != 0)
            {
                return JsonHelper.TableToList<Supplier>(dt);
            }
            return null;
        }

        /// <summary>
        /// 文件保存
        ///  2017/6/13(zhuguanjun)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult SaveFile(SubContractFiles entity)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<SubContractFiles>().Insert(entity, true, out _id);
                else
                    new Repository<SubContractFiles>().Update(entity, true, out _id);
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
        /// 文件保存
        ///  2017/6/13(zhuguanjun)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ReUpload"></param>
        /// <returns></returns>
        public JsonResult SaveFile(SubContractFiles entity, bool ReUpload)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (entity.Type != 0)
                {
                    //不是其他文件
                    List<SubContractFiles> listOld = GetFiles(entity.SubID, entity.Type);
                    entity.ID = listOld.Count > 0 ? listOld[0].ID : "";
                }
                if (string.IsNullOrEmpty(entity.ID))
                    new Repository<SubContractFiles>().Insert(entity, true, out _id);
                else
                {
                    SubContractFiles old = new Repository<SubContractFiles>().Get(entity.ID);
                    old.Name = entity.Name;
                    old.Path = ReUpload ? entity.Path : old.Path;
                    new Repository<SubContractFiles>().Update(old, true, out _id);
                }
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

    }
}
