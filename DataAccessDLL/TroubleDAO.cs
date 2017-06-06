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
    public class TroubleDAO
    {
        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="entity">问题实体</param>
        /// <param name="listWork">责任人列表</param>
        public virtual void AddTrouble(Trouble entity, List<TroubleWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                entity.ID = Guid.NewGuid().ToString() + "-1";
                s.Save(entity);
                if (listWork != null)
                    foreach (TroubleWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.TroubleID = entity.ID.Substring(0, 36);
                        s.Save(item);
                    }
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
        /// 2017/06/01(zhuguanjun)
        /// </summary>
        /// <param name="entity">问题实体</param>
        /// <param name="listWork">负责人列表</param>
        public virtual void UpdateTrouble(Trouble entity, List<TroubleWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();

                //删除问题
                var oldTrouble = s.Get<Trouble>(entity.ID);
                oldTrouble.Status = 0;//假删除
                s.Update(oldTrouble);

                //删除责任人
                s.CreateQuery("delete from TroubleWork where TroubleID='" + entity.ID.Substring(0, 36) + "';").ExecuteUpdate();

                //保存已经问题
                string hisNo = entity.ID.Substring(37);
                entity.ID = entity.ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                s.Save(entity);

                //保存新的责任人
                if (listWork != null)
                    foreach (TroubleWork item in listWork)
                    {
                        item.ID = Guid.NewGuid().ToString();
                        item.Status = 1;
                        item.CREATED = DateTime.Now;
                        item.TroubleID = entity.ID.Substring(0, 36);
                        s.Save(item);
                    }
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
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetTroubleList(string PID, string startDate, string endDate, string key)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,r.Desc,r.HandleResult, ");
            sql.Append(" strftime('%Y-%m-%d',r.StarteDate) as StartDate,strftime('%Y-%m-%d',r.EndDate) as EndDate,d.Name as HandleStatus,d1.Name as Level from Trouble r ");
            sql.Append(" inner join PNode p on r.NodeID = substr(p.ID,1,36) and p.Status = 1");
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

            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);

            if (ds != null && ds.Tables.Count > 0)
            {
                return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
            }
            else
            {
                return new DataTable();
            }
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
            sql.Append(" LEFT JOIN STAKEHOLDERS s ON r.Manager= substr(s.Id,1,36)");
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
    }
}
