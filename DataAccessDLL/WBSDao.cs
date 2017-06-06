using DomainDLL;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessDLL
{
    public class WBSDao : BaseDao
    {
        /// <summary>
        /// 节点移动后位置和父级修改保存
        /// Created:20170406(ChengMengjia)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="oldNo"></param>
        /// <param name="oldParentID"></param>
        public void UpdateNode(PNode node, PNode oldNode, string oldParentID)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                string sql;

                if (string.IsNullOrEmpty(oldParentID))
                {
                    //父节点不变
                    sql = string.Format("select WBSNo from PNode where substr(ID,1,36)='{0}'", node.ParentID);
                    string wbsno = s.CreateSQLQuery(sql).DynamicList().FirstOrDefault().WBSNo;
                    if (!string.IsNullOrEmpty(wbsno))
                        wbsno += ".";
                    if (node.No < oldNode.No)
                    {
                        #region 向上移动
                        sql = string.Format("Update PNode set WBSNo='{0}'||(No+1)  where ParentID='{1}' and No>={2} and No<{3}  ;",
                      wbsno, node.ParentID, node.No, oldNode.No);
                        s.CreateSQLQuery(sql).ExecuteUpdate();

                        sql = string.Format("Update PNode set No=No+1 where ParentID='{0}' and No>={1} and No<{2} ;",
                      node.ParentID, node.No, oldNode.No);
                        s.CreateSQLQuery(sql).ExecuteUpdate();
                        #endregion
                    }
                    else
                    {
                        #region 向下移动
                        sql = string.Format("Update PNode set No=No-1 where ParentID='{0}' and No>{1} and No<={2} ;",
                            node.ParentID, node.No, oldNode.No);
                        s.CreateSQLQuery(sql).ExecuteUpdate();
                        sql = string.Format("Update PNode set WBSNo='{0}'||(No-1) where ParentID='{0}' and No>{1} and No<={2} ;",
                     wbsno, node.ParentID, node.No, oldNode.No);
                        s.CreateSQLQuery(sql).ExecuteUpdate();
                        #endregion
                    }
                }
                else
                {
                    #region 更新新同级编号
                    sql = string.Format("select WBSNo from PNode where substr(ID,1,36)='{0}'", node.ParentID);
                    string wbsno = s.CreateSQLQuery(sql).DynamicList().FirstOrDefault().WBSNo;
                    if (!string.IsNullOrEmpty(wbsno))
                        wbsno += ".";
                    sql = string.Format("Update PNode set WBSNo='{0}'||(No+1) where ParentID='{1}'  and No>={2} ;",
                     wbsno, node.ParentID, node.No);
                    s.CreateSQLQuery(sql).ExecuteUpdate();
                    #endregion
                    sql = string.Format("Update PNode set No=No+1 where ParentID='{0}' and No>={1} ;",
                     node.ParentID, node.No);
                    s.CreateSQLQuery(sql).ExecuteUpdate();

                    #region 更新原同级编号
                    sql = string.Format("select WBSNo from PNode where substr(ID,1,36)='{0}'", oldParentID);
                    wbsno = s.CreateSQLQuery(sql).DynamicList().FirstOrDefault().WBSNo;
                    if (!string.IsNullOrEmpty(wbsno))
                        wbsno += ".";
                    sql = string.Format("Update PNode set WBSNo='{0}'||(No-1) where ParentID='{1}' and No>{2} ;",
                     wbsno, oldParentID, oldNode.No);
                    s.CreateSQLQuery(sql).ExecuteUpdate();
                    #endregion

                    sql = string.Format("Update PNode set No=No-1 where ParentID='{0}' and No>{1} ;",
                       oldParentID, oldNode.No);
                    s.CreateSQLQuery(sql).ExecuteUpdate();

                }
                s.Save(node);
                s.Update(oldNode);
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
        /// 交付物节点的添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        /// <param name="id"></param>
        public virtual void AddDeliverables(PNode node, DeliverablesJBXX jbxx, NodeProgress progress, List<DeliverablesWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Save(node);
                s.Save(jbxx);
                s.Save(progress);
                if (listWork != null)
                    foreach (DeliverablesWork entity in listWork)
                    {
                        entity.ID = Guid.NewGuid().ToString();
                        entity.Status = 1;
                        entity.CREATED = DateTime.Now;
                        entity.JBXXID = jbxx.ID.Substring(0, 36);
                        entity.Manager = entity.Manager.Substring(0, 36);
                        s.Save(entity);
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
        /// 交付物节点的修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="hisFlg"></param>
        /// <param name="id"></param>
        public virtual void UpdatedDeliverables(DeliverablesJBXX new_jbxx, DeliverablesJBXX old_jbxx, PNode new_node, PNode old_node, List<DeliverablesWork> listWork)
        {
            ISession s = NHHelper.GetCurrentSession();
            try
            {
                s.BeginTransaction();
                s.Save(new_jbxx);
                s.Save(new_node);
                s.Update(old_jbxx);
                s.Update(old_node);
                if (listWork != null)
                {
                    s.CreateQuery("delete from DeliverablesWork where JBXXID='" + new_jbxx.ID.Substring(0, 36) + "';").ExecuteUpdate();
                    foreach (DeliverablesWork entity in listWork)
                    {
                        entity.ID = Guid.NewGuid().ToString();
                        entity.CREATED = DateTime.Now;
                        entity.JBXXID = new_jbxx.ID.Substring(0, 36);
                        entity.Manager = entity.Manager.Substring(0, 36);
                        entity.Status = 1;
                        s.Save(entity);
                    }
                }
                UpdateProject(s);//更新项目时间
                s.Transaction.Commit();
                s.Close();
            }
            catch (Exception ex)
            {
                s.Transaction.Rollback();
                s.Close();
                throw new Exception("更新失败", ex);
            }
        }

        /// <summary>
        /// 根据项目ID取得项目结点ID
        /// Created:2017.04.07(Xuxb)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public string GetNodeIdByProjectId(string projectId)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select ID from PNode where ParentID is null and PID = @PID ");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = projectId });

            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return NHHelper.ExecuteDataset(sql.ToString(), qf).Tables[0].Rows[0]["ID"].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取交付物信息列表
        ///  Created:2017.04.21(ChengMengJia)
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="PID"></param>
        /// <returns></returns>
        public DataTable GetJFWList(string startDate, string endDate, string PID)
        {
            List<QueryField> qf = new List<QueryField>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select j.id,j.name,j.Desc,parent.Name NodeName,strftime('%Y-%m-%d',j.StarteDate)StarteDate,strftime('%Y-%m-%d',j.EndDate)EndDate,");
            sql.Append(" j.Workload,j.Manager ");
            sql.Append(" from DeliverablesJBXX j inner join PNode n on j.NodeID=substr(n.ID,1,36) and n.status=1");
            sql.Append(" left join PNode parent on n.ParentID=substr(parent.ID,1,36) and parent.status=1");
            sql.Append(" where n.PID=@PID  and j.status=1 ");
            //开始日期
            if (!string.IsNullOrEmpty(startDate))
            {
                sql.Append(" and date(j.StarteDate) >= date(@startDate)");
                qf.Add(new QueryField() { Name = "startDate", Type = QueryFieldType.String, Value = DateTime.Parse(startDate).ToString("yyyy-MM-dd") });
            }
            //结束日期
            if (!string.IsNullOrEmpty(endDate))
            {
                sql.Append(" and (date(j.EndDate) <= date(@endDate) or j.EndDate is null )");
                qf.Add(new QueryField() { Name = "endDate", Type = QueryFieldType.String, Value = DateTime.Parse(endDate).ToString("yyyy-MM-dd") });
            }
            sql.Append(" order by j.updated desc,j.created asc");
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = PID });
            return NHHelper.ExecuteDataTable(sql.ToString(), qf);
        }

    }
}
