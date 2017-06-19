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
    /// 类名：项目问题数据处理类
    /// Created：2017.04.06(xuxb)
    /// </summary>
    public class TroubleDAO : BaseDao
    {
        /// <summary>
        /// 新增问题
        ///  Created:20170601(zhuguanjun)
        ///  Updated:20170607(ChengMengjia) 添加作为节点插入
        /// </summary>
        /// <param name="entity">问题实体</param>
        /// <param name="listWork">责任人列表</param>
        public virtual void AddTrouble(Trouble entity, PNode node, List<TroubleWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Save(entity);
                if (node != null)
                    s.Save(node);
                if (listWork != null)
                    foreach (TroubleWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.TroubleID = entity.ID.Substring(0, 36);
                        s.Save(item);
                    }
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

        /// <summary>
        /// 问题工作
        /// Created:20170601(zhuguanjun)
        /// Updated:20170607(ChengMengjia) 更新节点插入
        /// </summary>
        /// <param name="entity">问题实体</param>
        /// <param name="listWork">负责人列表</param>
        public virtual void UpdateTrouble(Trouble newEntity, Trouble oldEntity, PNode newNode, PNode oldNode, List<TroubleWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Update(oldEntity);
                s.Save(newEntity);
                if (newNode != null)
                    s.Save(newNode);
                if (oldNode != null)
                    s.Update(oldNode);

                //删除责任人
                s.CreateQuery("delete from TroubleWork where TroubleID='" + oldEntity.ID.Substring(0, 36) + "';").ExecuteUpdate();

                //保存新的责任人
                if (listWork != null)
                    foreach (TroubleWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.TroubleID = newEntity.ID.Substring(0, 36);
                        s.Save(item);
                    }
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

        /// <summary>
        /// 项目问题查询
        /// Created:2017.04.06(xuxb)
        /// Updated:20170607(ChengMengjia)增加状态判断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, string key)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,r.Desc,r.HandleResult,strftime('%Y-%m-%d',r.StarteDate) as StartDate, ");
            sql.Append(" strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,d1.Name as Level, ");

            //完成状态判断 参加PNode的Entity中FinishStatus说明
            sql.Append(" case when r.HandleStatus=3 then 1   ");
            sql.Append(" when r.EndDate<date('now') and (r.HandleStatus is null or r.HandleStatus<>3) then 3 ");
            sql.Append(" when r.StarteDate>=date('now','+1 day') and (r.HandleStatus is null or r.HandleStatus<>3) then 0 else 2 end FinishType ");

            sql.Append(" from Trouble r inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
            sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
            sql.Append(" left join DictItem d1 on r.Level = d1.No and d1.DictNo = " + (int)CommonDLL.DictCategory.TroubleLevel);
            sql.Append(" where r.status = 1 and p.PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });

            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(r.StarteDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }

            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and date(r.EndDate) <= date(@endDate) )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            //关键字
            if (!string.IsNullOrEmpty(key))
            {
                sql.Append(" and (r.Name like '%' || @key || '%' or r.Desc like '%' || @key || '%' or r.DealResult like '%' || @key || '%') ");
                qf.Add(new QueryField() { Name = "key", Type = QueryFieldType.String, Value = key });
            }

            sql.Append(" order by r.StarteDate Desc  ");

            return NHHelper.ExecuteDataTable(sql.ToString(), qf);
        }

        /// <summary>
        /// 项目问题查询
        /// Created:2017.04.21(ChengMengjia)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, int? Status)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,p.Name NodeName,r.Desc,r.HandleResult,s.Name HandleMan,strftime('%Y-%m-%d',r.HandleDate) as HandleDate, ");
            sql.Append(" strftime('%Y-%m-%d',r.StarteDate) as StartDate,strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,d1.Name as Level from Trouble r ");
            sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
            sql.Append(" left join Stakeholders s on r.HandleMan = substr(s.ID,1,36) ");
            sql.Append(" left join DictItem d on r.HandleStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.TroubleHandleStatus);
            sql.Append(" left join DictItem d1 on r.Level = d1.No and d1.DictNo = " + (int)CommonDLL.DictCategory.TroubleLevel);
            sql.Append(" where r.status = 1 and p.PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });

            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(r.HandleDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }

            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and date(r.HandleDate) <= date(@endDate) )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            //解决状态
            if (Status != null)
            {
                sql.Append(" and r.HandleStatus =@status ");
                qf.Add(new QueryField() { Name = "status", Type = QueryFieldType.Numeric, Value = Status });
            }

            sql.Append(" order by r.StarteDate Desc  ");
            return NHHelper.ExecuteDataTable(sql.ToString(), qf);
        }

        /// <summary>
        /// 加载责任人列表
        /// 2017/06/06(zhuguanjun)
        /// </summary>
        /// <param name="TroubleID"></param>
        /// <returns></returns>
        public DataTable GetTroubleWorkList(string TroubleID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "TROUBLEID", Type = QueryFieldType.String, Value = TroubleID });
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT r.*,s.Name as ManagerName FROM TROUBLEWORK r");
            sql.Append(" LEFT JOIN STAKEHOLDERS s ON r.Manager= substr(s.Id,1,36) and s.status=1 ");
            sql.Append(" WHERE r.TROUBLEID =@TROUBLEID ");


            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return new DataTable();
            }
        }



       /// <summary>
        /// 通过NodeID获取附件列表
        /// Created:20170612 (ChengMengjia)
       /// </summary>
       /// <param name="NodeID"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        public List<TroubleFiles> GetFilesByNodeID(string NodeID, int? type)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.*  FROM TroubleFiles a");
            sql.Append(" LEFT JOIN Trouble b ON a.TroubleID= substr(b.ID,1,36) and b.Status=1 ");
            sql.Append(" WHERE a.Status =1 and b.NodeID=@NodeID ");
            if (type != null)
            {
                sql.Append(" and a.Type=@Type ");
                qf.Add(new QueryField() { Name = "Type", Type = QueryFieldType.Numeric, Value = type });
            }
            qf.Add(new QueryField() { Name = "NodeID", Type = QueryFieldType.String, Value = NodeID.Substring(0, 36) });

            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);
            if (ds != null && ds.Tables.Count > 0)
            {
                return JsonHelper.TableToList<TroubleFiles>(ds.Tables[0]);
            }
            else
            {
                return new List<TroubleFiles>();
            }
        }
    }
}
