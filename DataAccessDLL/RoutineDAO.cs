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
    /// 类名：日常工作数据处理类
    /// Created：2017.03.30(xuxb)
    /// </summary>
    public class RoutineDAO : BaseDao
    {
        /// <summary>
        /// 日常工作查询
        /// Created:2017.04.06(xuxb)
        /// Updated:20170607(ChengMengjia) 增加状态判断
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetRoutinList(string PID, string startDate, string endDate, string key)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,r.Desc,r.DealResult,p.Name PName,strftime('%Y-%m-%d',r.StartDate) as StartDate,");
            sql.Append("strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,");

            //完成状态判断 参加PNode的Entity中FinishStatus说明
            sql.Append(" case when r.FinishStatus=3 then 1   ");
            sql.Append(" when r.EndDate<date('now') and (r.FinishStatus is null or r.FinishStatus<>3) then 3 ");
            sql.Append(" when r.StartDate>=date('now','+1 day') and (r.FinishStatus is null or r.FinishStatus<>3) then 0 else 2 end FinishType ");

            sql.Append(" from Routine r inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
            sql.Append(" left join DictItem d on r.FinishStatus = d.No and d.DictNo = " + (int)CommonDLL.DictCategory.WorkHandleStatus);
            sql.Append(" where r.status = 1 and p.PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });

            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(r.StartDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }

            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and (date(r.EndDate) <= date(@endDate) or r.EndDate is null )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }

            //关键字
            if (!string.IsNullOrEmpty(key))
            {
                sql.Append(" and (r.Name like '%' || @key || '%' or r.Desc like '%' || @key || '%' or r.DealResult like '%' || @key || '%') ");
                qf.Add(new QueryField() { Name = "key", Type = QueryFieldType.String, Value = key });
            }

            sql.Append(" order by r.StartDate Desc  ");

            return NHHelper.ExecuteDataTable(sql.ToString(), qf);

        }

        /// <summary>
        /// 新增日常工作
        ///  Updated:20170605(ChengMengjia) 添加作为节点插入
        /// </summary>
        /// <param name="entity">日常工作实体</param>
        /// <param name="listWork">责任人列表</param>
        public virtual void AddRoutine(Routine entity, PNode node, List<RoutineWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Save(entity);
                if (node != null)
                    s.Save(node);
                if (listWork != null)
                    foreach (RoutineWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.RoutineID = entity.ID.Substring(0, 36);
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
        /// 更新日常工作
        /// 2017/06/01(zhuguanjun)
        /// </summary>
        /// <param name="entity">日常工作实体</param>
        /// <param name="listWork">负责人列表</param>
        public void UpdateRoutine(Routine newEntity, Routine oldeEntity, PNode newNode, PNode oldNode, List<RoutineWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Update(oldeEntity);
                s.Save(newEntity);
                if (newNode != null)
                    s.Save(newNode);
                if (oldNode != null)
                    s.Update(oldNode);

                //删除责任人
                s.CreateQuery("delete from RoutineWork where RoutineID='" + oldeEntity.ID.Substring(0, 36) + "';").ExecuteUpdate();
                //保存新的责任人
                if (listWork != null)
                    foreach (RoutineWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.RoutineID = newEntity.ID.Substring(0, 36);
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

        public DataTable GetRoutinWorkList(string RoutineID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "ROUTINEID", Type = QueryFieldType.String, Value = RoutineID });
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT r.*,s.Name as ManagerName FROM ROUTINEWORK r");
            sql.Append(" LEFT JOIN STAKEHOLDERS s ON r.Manager= substr(s.Id,0,37)  and s.status=1  ");
            sql.Append(" WHERE r.ROUTINEID =@ROUTINEID ");


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
        /// <returns></returns>
        public List<RoutineFiles> GetFilesByNodeID(string NodeID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "NodeID", Type = QueryFieldType.String, Value = NodeID.Substring(0,36) });
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.*  FROM RoutineFiles a");
            sql.Append(" LEFT JOIN Routine b ON a.RoutineID= substr(b.ID,1,36) and b.Status=1 ");
            sql.Append(" WHERE a.Status =1 and b.NodeID=@NodeID ");
            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);
            if (ds != null && ds.Tables.Count > 0)
            {
                return JsonHelper.TableToList<RoutineFiles>(ds.Tables[0]);
            }
            else
            {
                return new List<RoutineFiles>();
            }
        }

    }
}
