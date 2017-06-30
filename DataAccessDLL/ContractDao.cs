using CommonDLL;
using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    /// <summary>
    /// 合同分包管理Dao
    /// 2017/4/11(zhuguanjun)
    /// </summary>
    public class ContractDao : BaseDao
    {
        /// <summary>
        /// 获取分包集合(无版本号的ID和有版本号的Name)
        /// 2017/04/12(zhuguanjun)
        /// </summary>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public DataTable GetContractList(List<QueryField> qf)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select c1.ID,c2.A_Name||'('||c2.A_No||')' as A,c2.B_Name as B from SubContract c1, SubContract c2");
            sql.Append(" where substr(c1.ID, 38) = '1' and substr(c1.ID, 1, 37) = substr(c2.ID, 1, 37)");
            sql.Append(" and c2.PID=@PID  and c2.status=@Status order by c2.A_No ");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            return dt;
        }

        /// <summary>
        /// 保存分包信息
        /// 2017/04/13(zhuguanjun)
        /// </summary>
        /// <param name="entity">分包信息</param>
        /// <param name="dicFile">上传文件集合</param>
        /// <param name="_id"></param>
        /// <returns></returns>
        public JsonResult SaveSubContract(SubContract entity, Dictionary<int, string> dicFile, out string _id)
        {
            _id = string.Empty;//实际id
            string SubID = string.Empty;//版本id
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();

                #region 保存分包信息
                if (string.IsNullOrEmpty(entity.ID))
                {
                    if (entity.Status == null)
                        entity.Status = 1;
                    entity.ID = Guid.NewGuid().ToString() + "-1";
                    entity.CREATED = DateTime.Now;
                    SubID = entity.ID;//原始版本id
                    _id = entity.ID;//实际id
                    s.Save(entity);
                }
                else
                {
                    SubContract old = new Repository<SubContract>().Get(entity.ID);
                    old.UPDATED = DateTime.Now;
                    old.Status = 0;
                    s.Update(old);
                    string hisNo = entity.ID.Substring(37);
                    entity.ID = entity.ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                    _id = entity.ID;//实际id
                    SubID = entity.ID.Substring(0, 36) + "-1";//原始版本id
                    entity.Status = 1;
                    entity.CREATED = old.CREATED;
                    s.Save(entity);
                }
                #endregion

                #region 保存上传的附件信息
                foreach (var item in dicFile)
                {
                    SubContractFiles file = new SubContractFiles();
                    switch (item.Key)
                    {
                        case 1:
                            file.Name = "合同扫描件";
                            break;
                        case 2:
                            file.Name = "合同电子档";
                            break;
                        case 3:
                            file.Name = "工作说明书扫描件";
                            break;
                        case 4:
                            file.Name = "工作说明书电子档";
                            break;
                        case 5:
                            file.Name = "其他";
                            break;
                    }
                    file.Path = item.Value;
                    file.SubID = SubID;
                    file.Type = item.Key;
                    List<QueryField> qlist = new List<QueryField>();
                    qlist.Add(new QueryField() { Name = "SubID", Type = QueryFieldType.String, Value = SubID });//版本id
                    qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                    qlist.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = item.Key });//附件类型
                    SubContractFiles old = new Repository<SubContractFiles>().FindSingle(qlist);
                    if (old != null && !string.IsNullOrEmpty(old.ID))
                    {
                        old.Path = file.Path;
                        old.UPDATED = DateTime.Now;
                        s.Update(old);
                    }
                    else
                    {
                        file.CREATED = DateTime.Now;
                        file.ID = Guid.NewGuid().ToString();
                        file.Status = 1;
                        s.Save(file);
                    }
                }
                #endregion

                s.Transaction.Commit();
                s.Close();
                return new JsonResult { result = true, msg = "保存成功", data = SubID };
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("保存实体失败", ex);
            }
        }

        /// <summary>
        /// 获取分包合同信息、附件信息、里程碑信息、付款信息
        /// 2017/04/13
        /// </summary>
        /// <param name="SubID">分包合同主表ID(版本id)</param>
        /// <param name="subContract">返回合同信息</param>
        /// <param name="files">返回附件集合</param>
        /// <param name="LCB">返回里程碑集合</param>
        /// <param name="SKXX">返回付款信息集合</param>
        public void GetSubContractAll(string SubID, out SubContract subContract, out DataTable files, out DataTable LCB, out DataTable SKXX)
        {
            StringBuilder sqlSub = new StringBuilder();
            StringBuilder sqlFile = new StringBuilder();
            StringBuilder sqlLCB = new StringBuilder();
            StringBuilder sqlSKXX = new StringBuilder();
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField() { Name = "SubID", Type = QueryFieldType.String, Value = SubID });
            
            #region  分包合同
            sqlSub.Append(" select * from SubContract s");
            //sqlSub.Append(" where Status=@status and substr(s.Id,1,37)||'1'=@SubID");
            sqlSub.Append(" where Status=@status and substr(s.Id,1,36)=@SubID");
            sqlSub.Append(" order by s.CREATED");
            DataTable Sub = NHHelper.ExecuteDataTable(sqlSub.ToString(), qlist);
            subContract = Sub == null ? new SubContract() : JsonHelper.TableToEntity<SubContract>(Sub);
            #endregion

            #region 附件
            sqlFile.Append(" select * from SubContractFiles");
            sqlFile.Append(" where SubID=@SubID and Status=@Status");
            sqlFile.Append(" order by s.CREATED");
            files = NHHelper.ExecuteDataTable(sqlFile.ToString(), qlist);
            #endregion

            #region 里程碑
            sqlLCB.Append(" select s.*,d1.Name as FinishStatusName from SubContractLCB s");
            sqlLCB.Append(" left join DictItem d1 on s.FinishStatus = d1.No and d1.DictNo=" + (int)DictCategory.Milestones_FinshStatus);
            sqlLCB.Append(" where s.SubID=@SubID and s.Status=@Status");
            sqlLCB.Append(" order by s.CREATED");
            LCB = NHHelper.ExecuteDataTable(sqlLCB.ToString(), qlist);
            #endregion

            #region 收款信息
            //sqlSKXX.Append(" select s.*,d1.Name as FinishStatusName,d2.Name as BatchNoName from SubContractSKXX s");
            sqlSKXX.Append(" select s.*,d1.Name as FinishStatusName from SubContractSKXX s");
            sqlSKXX.Append(" left join DictItem d1 on s.FinishStatus = d1.No and d1.DictNo=" + (int)DictCategory.Receivables_FinshStatus);
            //sqlSKXX.Append(" left join DictItem d2 on s.BatchNo = d2.No and d2.DictNo=" + (int)DictCategory.Receivables_BatchNo);
            sqlSKXX.Append(" where SubID=@SubID and Status=@Status");
            sqlSKXX.Append(" order by s.CREATED");
            SKXX = NHHelper.ExecuteDataTable(sqlSKXX.ToString(), qlist);
            #endregion

        }

        /// <summary>
        /// 获取里程碑集合
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="SubID"></param>
        /// <returns></returns>
        public DataTable GetLCBList(string SubID)
        {
            StringBuilder sqlLCB = new StringBuilder();
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField() { Name = "SubID", Type = QueryFieldType.String, Value = SubID });
            sqlLCB.Append(" select s.*,d1.Name as FinishStatusName from SubContractLCB s");
            sqlLCB.Append(" left join DictItem d1 on s.FinishStatus = d1.No and d1.DictNo=" + (int)DictCategory.Milestones_FinshStatus);
            sqlLCB.Append(" where s.SubID=@SubID and s.Status=@Status");
            return NHHelper.ExecuteDataTable(sqlLCB.ToString(), qlist);
        }

        /// <summary>
        /// 获取收款信息集合
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="SubID"></param>
        /// <returns></returns>
        public DataTable GetSKXXList(string SubID)
        {
            StringBuilder sqlSKXX = new StringBuilder();
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            qlist.Add(new QueryField() { Name = "SubID", Type = QueryFieldType.String, Value = SubID });
            //sqlSKXX.Append(" select s.*,d1.Name as FinishStatusName,d2.Name as BatchNoName from SubContractSKXX s");
            sqlSKXX.Append(" select s.*,d1.Name as FinishStatusName from SubContractSKXX s");
            sqlSKXX.Append(" left join DictItem d1 on s.FinishStatus = d1.No and d1.DictNo=" + (int)DictCategory.Receivables_FinshStatus);
            //sqlSKXX.Append(" left join DictItem d2 on s.BatchNo = d2.No and d2.DictNo=" + (int)DictCategory.Receivables_BatchNo);
            sqlSKXX.Append(" where SubID=@SubID and Status=@Status");
            return NHHelper.ExecuteDataTable(sqlSKXX.ToString(), qlist);
        }

        /// <summary>
        /// 获取供应商无版本号的ID和有版本号的Name(用于下拉框)
        /// </summary>
        /// <param name="qlist"></param>
        /// <returns></returns>
        public DataTable GetSupplierList(List<QueryField> qlist)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select substr(s.ID, 1,36) as ID,s.Name  from Supplier s");
            sql.Append(" where s.PID=@PID  and s.status=@Status order by s.Name ");
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qlist);
            return dt;
        }


        /// <summary>
        /// 更新付款信息和里程碑
        /// Created：20170612(ChengMengjia)
        /// </summary>
        /// <param name="entity">日常工作实体</param>
        /// <param name="listWork">负责人列表</param>
        public void UpdateEntities(SubContractLCB newlcb, SubContractLCB oldlcb, SubContractSKXX newskxx, SubContractSKXX oldskxx)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                if (newlcb != null)
                    s.Save(newlcb);
                if (oldlcb != null)
                    s.Update(oldlcb);
                if (newskxx != null)
                    s.Save(newskxx);
                if (oldskxx != null)
                    s.Update(oldskxx);
                UpdateProject(s);//更新项目时间
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("插入实体失败", ex);
            }
        }

    }
}
