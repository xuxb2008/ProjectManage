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
    public class ProjectDao
    {

        /// <summary>
        /// 项目新增
        /// Created:2017.3.24(ChengMengjia)
        /// Updated:2017.4.6(zhuguanjun)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="node"></param>
        /// <param name="id"></param>
        public virtual void Add(Project project, List<PNode> list, List<DeliverablesJBXX> listJbxx, out string id)
        {
            id = string.Empty;
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                if (list == null)
                {
                    #region 顶级节点
                    PNode node = new PNode();
                    node.No = 1;
                    node.ID = Guid.NewGuid().ToString();
                    node.PID = project.ID;
                    node.Name = project.Name;
                    node.PType = 0;
                    node.Status = 1;
                    node.CREATED = DateTime.Now;
                    s.Save(node);
                    #endregion
                }
                else
                {
                    foreach (PNode node in list)
                        s.Save(node);
                    foreach (DeliverablesJBXX jbxx in listJbxx)
                        s.Save(jbxx);
                }
                //成本、收入、收款取消6条
                //(2017/05/09)zhuguanjun
                #region 成本信息
                //for (int i = 1; i < 7; i++)
                //{
                //    Cost entityC = new Cost();
                //    entityC.ID = Guid.NewGuid().ToString() + "-1";
                //    entityC.PID = project.ID;
                //    entityC.Tag = "成本分配" + i;
                //    entityC.Status = 1;
                //    entityC.Total = 0;
                //    entityC.Transit = 0;
                //    entityC.Used = 0;
                //    entityC.Remaining = 0;
                //    entityC.CREATED = DateTime.Now.AddSeconds(i);
                //    s.Save(entityC);
                //}
                #endregion

                #region 收入信息
                //for (int i = 1; i < 7; i++)
                //{
                //    Income entityI = new Income();
                //    entityI.ID = Guid.NewGuid().ToString() + "-1";
                //    entityI.PID = project.ID;
                //    entityI.Status = 1;
                //    entityI.Step = "阶段" + i;
                //    entityI.CREATED = DateTime.Now;
                //    s.Save(entityI);
                //}
                #endregion

                #region 收款信息
                //for (int i = 1; i < 7; i++)
                //{
                //    Receivables entityR = new Receivables();
                //    entityR.ID = Guid.NewGuid().ToString() + "-1";
                //    entityR.PID = project.ID;
                //    entityR.Status = 1;
                //    entityR.BatchNo = "第" + i + "次收款";
                //    entityR.CREATED = DateTime.Now;
                //    s.Save(entityR);
                //}
                #endregion

                s.Save(project);
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("保存实体失败", ex);
            }
        }

        /// <summary>
        /// 项目修改
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="id"></param>
        public void Update(Project entity, out string id)
        {
            id = string.Empty;
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                id = entity.ID;
                entity.UPDATED = DateTime.Now;
                entity.ProjectLastUpdate = DateTime.Now;
                s.Update(entity);

                #region 顶级节点
                s.CreateSQLQuery("update PNode set Name=:name where PID=:pid and ParentID is null and Status=1; ")
                    .SetString("name", entity.Name).SetString("pid", entity.ID).ExecuteUpdate();
                #endregion

                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("保存实体失败", ex);
            }
        }

        /// <summary>
        /// 项目成果取得
        /// Created:20170328(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectResult(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select case when sum(d.Workload) is null then 0 else sum(d.Workload) end as TotalWork, ");
            sql.Append(" case when sum(d.WorkLoad * (np.PType - 1)/4) is null then 0 else sum(d.WorkLoad * (np.PType - 1)/4) end as CompleteWork  ");
            sql.Append(" from PNode p ");
            sql.Append(" left join DeliverablesJBXX d on substr(p.ID,1,36) = d.NodeID and d.Status = 1 ");
            sql.Append(" left join NodeProgress np on substr(p.ID,1,36) = np.NodeID and np.Status = 1 ");
            sql.Append(" where p.PID=@PID  and p.status=1");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        /// <summary>
        /// 项目风险取得
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectRisk(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select IFNULL(riskFind,0) as riskFind,IFNULL(riskAssess,0) as riskAssess,");
            sql.Append(" IFNULL(riskHandle,0) as riskHandle from (  ");
            sql.Append(" select sum( case when FindDate is null then 0 else 1 end) as riskFind,  ");
            sql.Append(" sum( case when AssessDate is null then 0 else 1 end) as riskAssess, ");
            sql.Append(" sum( case when HandleDate is null then 0 else 1 end) as riskHandle ");
            sql.Append(" from Risk r ");
            sql.Append(" where r.PID=@PID  and r.status=1)");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        /// <summary>
        /// 项目问题取得
        /// Created:20170329(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectTrouble(string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select IFNULL(TroubleTotal,0) as TroubleTotal,IFNULL(TroubleHandle,0) as TroubleHandle,");
            sql.Append(" IFNULL(TroubleLeave,0) as TroubleLeave,IFNULL(TroubleRest,0) as TroubleRest from (");
            sql.Append(" select count(1) as TroubleTotal,  ");
            sql.Append(" sum(case when HandleStatus = 3 then 1 else 0 end) as TroubleHandle, ");
            sql.Append(" sum( case when HandleStatus <> 3 then 1 else 0 end) as TroubleLeave, ");
            sql.Append(" sum( case when HandleStatus <> 3 and EndDate < date('now') then 1 else 0 end) as TroubleRest ");
            sql.Append(" from Trouble t ");
            sql.Append(" inner join PNode p on substr(p.ID,1,36) = t.NodeId and p.status = 1 ");
            sql.Append(" where p.PID=@PID  and t.status=1)");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        /// <summary>
        /// 项目近期工作和问题取得
        /// Created:20170329(xuxb)
        /// updated:20170706(zhugj)近期不显示已完成的
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetProjectLastWorkList(string PID, int days)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select r.ID,r.Name,r.Desc,'日常工作' as WorkType from Routine r ");
            sql.Append(" inner join PNode p on substr(p.ID,1,36) = r.NodeId and p.status = 1 ");
            sql.Append(" where r.StartDate < date('now','+' || @Days || ' day') and r.status = 1 and p.PID=@PID ");
            sql.Append(" and r.FinishStatus != 3");
            sql.Append(" union all ");
            sql.Append(" select t.ID,t.Name,t.Desc,'项目问题' as WorkType from Trouble t  ");
            sql.Append(" inner join PNode p on substr(p.ID,1,36) = t.NodeId and p.status = 1 ");
            sql.Append(" where t.StarteDate < date('now','+' || @Days || ' day') and t.status = 1 and p.PID=@PID");
            sql.Append(" and t.HandleStatus != 3");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Days", Type = QueryFieldType.Numeric, Value = days });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

        /// <summary>
        /// 预警内容取得
        ///  Created:20170414(xuxb)
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="openFlg"></param>
        /// <param name="projectUpdateDays"></param>
        /// <param name="publishUpdateDays"></param>
        /// <returns></returns>
        public DataTable GetProjectWarnning(string PID, bool[] openFlg, int projectUpdateDays, int publishUpdateDays)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            //项目更新预警
            sql.Append(" select '' Id,'项目更新预警' as WarnningName, @Days1 || '天内没有更新项目' as WarnningContent from Project ");
            sql.Append(" where IFNULL(ProjectLastUpdate,date('2017-01-01')) < date('now','-' || @Days1 || ' day') ");
            sql.Append(" and ID=@PID ");

            //项目发布更新预警
            //TODO

            //项目预警
            if (openFlg[0])
            {
                sql.Append(" union all ");
                sql.Append(" select '' Id,'项目预警' as WarnningName, Tag || '的成本超出预算' as WarnningContent from Cost where IFNULL(Remaining,0) < 0  ");
                sql.Append(" and status = 1 and PID=@PID ");
            }

            //交付物预警条件
            //期限内没有完成工作量
            if (openFlg[1])
            {
                sql.Append(" union all ");
                sql.Append(" select p.ID Id,'项目交付物预警' as WarnningName, d.Name || '在期限内没有完成工作量' as WarnningContent from DeliverablesJBXX d  ");
                sql.Append(" inner join PNode p on substr(p.ID,1,36) = d.NodeID ");
                sql.Append(" left join NodeProgress n on d.NodeID = n.NodeID ");
                sql.Append(" where IFNULL(d.EndDate,date('2017-01-01')) < date('now') and IFNULL(n.PType,0) < 4 ");
                sql.Append(" and d.status = 1 and n.status = 1 and p.status = 1 and p.PID=@PID ");
            }
            //时间过去2/3时，工作量没有完成时提醒
            if (openFlg[2])
            {
                sql.Append(" union all ");
                sql.Append(" select p.ID Id,'项目交付物预警' as WarnningName, d.Name || '的时间过去2/3,但工作量没有完成' as WarnningContent from DeliverablesJBXX d  ");
                sql.Append(" inner join PNode p on substr(p.ID,1,36) = d.NodeID ");
                sql.Append(" left join NodeProgress n on d.NodeID = n.NodeID ");
                sql.Append(" where (julianday(strftime('%Y-%m-%d','now'))-julianday(IFNULL(d.StarteDate,date('2017-01-01'))))/");
                sql.Append("(julianday(IFNULL(d.EndDate,strftime('%Y-%m-%d','now')))-julianday(IFNULL(d.StarteDate,date('2017-01-01'))))");
                sql.Append(" > 2/3 and IFNULL(n.PType,0) < 4 ");
                sql.Append(" and d.status = 1 and n.status = 1 and p.status = 1 and p.PID=@PID ");
            }

            //问题处理预警
            if (openFlg[3])
            {
                sql.Append(" union all ");
                sql.Append(" select d.ID Id,'项目问题预警' as WarnningName, '问题【' || d.Name || '】在期限内没有解决' as WarnningContent from Trouble d  ");
                sql.Append(" inner join PNode p on substr(p.ID,1,36) = d.NodeID ");
                sql.Append(" where IFNULL(d.EndDate,date('2017-01-01')) < date('now') and d.HandleStatus <> 3");
                sql.Append(" and d.status = 1 and p.status = 1 and p.PID=@PID ");
            }

            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            qf.Add(new QueryField() { Name = "Days1", Type = QueryFieldType.Numeric, Value = projectUpdateDays });
            return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0];
        }

    }
}
