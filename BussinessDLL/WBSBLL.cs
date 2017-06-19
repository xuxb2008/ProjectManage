using CommonDLL;
using DataAccessDLL;
using DomainDLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BussinessDLL
{
    /// <summary>
    /// Forms-WBS-WBS
    /// Created:2017.03.23(ChengMengjia)
    /// </summary>
    public partial class WBSBLL
    {
        WBSDao dao = new WBSDao();

        /// <summary>
        /// 获取父节点
        ///  Created:2017605(ChengMengJia)
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public PNode GetParentNode(string NodeID)
        {
            PNode node = GetNode(NodeID);
            return GetNode(node.ParentID);
        }

        /// <summary>
        /// WBS节点保存
        /// Created:2017.3.23(ChengMengjia)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public JsonResult SaveNode(PNode node)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                if (node.ParentID.Length > 36)
                    node.ParentID = node.ParentID.Substring(0, 36);
                if (string.IsNullOrEmpty(node.ID))
                {
                    List<PNode> list = GetChildren(node.ParentID).Where(t => t.PType == 0 || t.PType == 1).ToList();
                    node.No = list.Count() + 1;
                    node.WBSNo = GetWBSNo(node.ParentID) + node.No.ToString();
                    new Repository<PNode>().Insert(node, true, out _id);
                }
                else
                    new Repository<PNode>().Update(node, true, out _id);
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
        /// WBS节点编号遍历得出
        /// Created:20170606(ChengMengjia)
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        private string GetWBSNo(string NodeID)
        {
            PNode node = GetNode(NodeID);
            if (string.IsNullOrEmpty(node.ParentID))
            {
                return "";
            }
            string WBSNo = node.No.ToString();
            while (!string.IsNullOrEmpty(node.ParentID))
            {
                node = GetNode(node.ParentID);
                //如果顶级节点 退出
                if (string.IsNullOrEmpty(node.ParentID)) break;
                WBSNo = node.No.ToString() + "." + WBSNo;
            }
            WBSNo += ".";
            return WBSNo;
        }


        /// <summary>
        /// WBS节点移动后更新
        /// Created:20170406(ChengMengjia)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public JsonResult SaveNode(PNode node, int newNO, string newParentID)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                string _id;
                PNode oldNode = new Repository<PNode>().Get(node.ID);
                oldNode.UPDATED = DateTime.Now;
                oldNode.Status = 0;

                string hisNo = node.ID.Substring(37);
                node.ID = node.ID.Substring(0, 36) + "-" + (int.Parse(hisNo) + 1).ToString();
                _id = node.ID;
                node.Status = 1;
                node.CREATED = DateTime.Now;
                node.No = newNO + 1;
                node.ParentID = newParentID.Substring(0, 36);
                node.WBSNo = GetWBSNo(node.ParentID)+node.No.ToString();

                dao.UpdateNode(node, oldNode, node.ParentID.Equals(oldNode.ParentID) ? "" : oldNode.ParentID);
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
        /// WBS交付物节点新增保存
        /// Created:2017.3.29(ChengMengjia)
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public JsonResult AddJFWNode(PNode node, DeliverablesJBXX jbxx, List<DeliverablesWork> list)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                node.ID = Guid.NewGuid().ToString() + "-1";
                node.ParentID = node.ParentID.Substring(0, 36);
                node.No = GetChildren(node.ParentID).Count() + 1;
                node.WBSNo = GetWBSNo(node.ParentID) + node.No.ToString();
                node.Status = 1;
                node.CREATED = DateTime.Now;

                jbxx.ID = Guid.NewGuid().ToString() + "-1";
                jbxx.NodeID = node.ID.Substring(0, 36);
                jbxx.Status = 1;
                jbxx.CREATED = DateTime.Now;

                NodeProgress entity = new NodeProgress()
                {
                    ID = Guid.NewGuid().ToString() + "-1",
                    NodeID = jbxx.NodeID,
                    PType = 1,
                    Status = 1,
                    CREATED = DateTime.Now
                };

                dao.AddDeliverables(node, jbxx, entity, list);
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
        /// 交付物基本信息更新
        ///  Created:20170330(ChengMengjia)
        /// </summary>
        /// <param name="new_jbxx"></param>
        /// <returns></returns>
        public JsonResult UpdateJBXX(DeliverablesJBXX new_jbxx, List<DeliverablesWork> listWorkx)
        {
            JsonResult jsonreslut = new JsonResult();
            try
            {
                #region jbxx
                DeliverablesJBXX old_jbxx = new Repository<DeliverablesJBXX>().Get(new_jbxx.ID);
                new_jbxx.ID = new_jbxx.ID.Substring(0, 36) + "-" + (int.Parse(new_jbxx.ID.Substring(37)) + 1).ToString();
                new_jbxx.NodeID = new_jbxx.NodeID.Substring(0, 36);
                new_jbxx.Status = 1;
                new_jbxx.CREATED = DateTime.Now;
                old_jbxx.Status = 0;
                old_jbxx.UPDATED = DateTime.Now;
                #endregion
                #region pnode
                PNode new_node = GetNode(new_jbxx.NodeID);
                PNode old_node = GetNode(new_jbxx.NodeID);
                new_node.Name = new_jbxx.Name;
                new_node.Status = 1;
                new_node.CREATED = DateTime.Now;
                new_node.ID = new_node.ID.Substring(0, 36) + "-" + (int.Parse(new_node.ID.Substring(37)) + 1).ToString();
                old_node.Status = 0;
                old_node.UPDATED = DateTime.Now;
                #endregion
                dao.UpdatedDeliverables(new_jbxx, old_jbxx, new_node, old_node, listWorkx);
                jsonreslut.data = new_node.ID;
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
        /// 交付物责任人列表
        ///  Created:20170526(ChengMengjia)
        /// </summary>
        /// <param name="jbxxID"></param>
        /// <returns></returns>
        public List<DeliverablesWork> GetManagerWorks(string jbxxID)
        {
            if (string.IsNullOrEmpty(jbxxID))
                return new List<DeliverablesWork>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.*,b.Name ManagerName from DeliverablesWork a inner join Stakeholders b on substr(a.Manager,1,36)=substr(b.ID,1,36) and b.status=1  ");
            sql.Append(" where a.JBXXID=@JBXXID and a.Status=@Status");
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "JBXXID", Type = QueryFieldType.String, Value = jbxxID.Substring(0, 36) });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            DataTable dt = NHHelper.ExecuteDataTable(sql.ToString(), qf);
            List<DeliverablesWork> list = JsonHelper.TableToList<DeliverablesWork>(dt);
            return list;
        }

        /// <summary>
        /// WBS节点集合
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public List<PNode> GetNodes(string ProjectID, int? PType)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            if (PType != null)
                qf.Add(new QueryField() { Name = "PType", Type = QueryFieldType.Numeric, Value = PType });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Asc };
            List<PNode> list = new Repository<PNode>().GetList(qf, sf) as List<PNode>;
            return list;
        }

        /// <summary>
        /// WBS节点集合 普通节点和交付物节点
        /// Created:20170605(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public List<PNode> GetNodes(string ProjectID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Asc };
            List<PNode> list = new Repository<PNode>().GetList(qf, sf) as List<PNode>;
            list = list.Where(t => t.PType == 0 || t.PType == 1).ToList();
            return list;
        }


        /// <summary>
        /// WBS子节点集合
        /// Created:2017.4.6(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        public List<PNode> GetChildren(string ParentID)
        {
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "ParentID", Type = QueryFieldType.String, Value = ParentID.Substring(0, 36) + "%", Comparison = QueryFieldComparison.like });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            SortField sf = new SortField() { Name = "CREATED", Direction = SortDirection.Asc };
            List<PNode> list = new Repository<PNode>().GetList(qf, sf) as List<PNode>;
            return list;
        }

        /// <summary>
        /// 获得WBS文件路径(模糊查询)
        /// Created：2017.3.28(xuxb)
        /// Updated：2017.3.30(ChengMengjia) 循环中的 filePath += node.Name + "\\" 改为  filePath = node.Name + "\\" + filePath
        /// Updated：2017.4.7(xuxb) 判断结点ID是否为空，判断是否追加根节点的路径
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <param name="NeedLike">需要模糊取最新吗</param>
        /// <param name="baseNodeFlg"></param>
        /// <returns></returns>
        public string GetWBSPath(string projectId, string nodeId, bool baseNodeFlg = true)
        {
            string filePath = "";
            if (string.IsNullOrEmpty(nodeId))
            {
                return filePath;
            }

            List<string> aa = new List<string>();


            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "ID", Type = QueryFieldType.String, Value = nodeId.Substring(0, 36) + "%", Comparison = QueryFieldComparison.like });
            qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            PNode node = new Repository<PNode>().FindSingle(qf);
            //如果根节点为空，退出
            if (!baseNodeFlg && string.IsNullOrEmpty(node.ParentID)) return filePath;
            filePath += node.Name + "\\"; 

            while (!string.IsNullOrEmpty(node.ParentID))
            {
                qf.Clear();
                qf.Add(new QueryField() { Name = "ID", Type = QueryFieldType.String, Value = node.ParentID + "%", Comparison = QueryFieldComparison.like });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                node = new Repository<PNode>().FindSingle(qf);
                //如果根节点为空，退出
                if (!baseNodeFlg && string.IsNullOrEmpty(node.ParentID)) break;
                filePath = node.Name + "\\" + filePath;
            }

            return filePath;
        }

        /// <summary>
        /// 获得WBS文件路径 (非模糊查询)
        /// Created：2017.4.6(xuxb)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public string GetWBSPath(string nodeId)
        {
            string filePath = "";
            List<QueryField> qf = new List<QueryField>();
            PNode node = new Repository<PNode>().Get(nodeId);
            filePath += node.Name + "\\";

            while (!string.IsNullOrEmpty(node.ParentID))
            {
                qf.Clear();
                qf.Add(new QueryField() { Name = "ID", Type = QueryFieldType.String, Value = node.ParentID + "%", Comparison = QueryFieldComparison.like });
                qf.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
                node = new Repository<PNode>().FindSingle(qf);
                filePath = node.Name + "\\" + filePath;
            }

            return filePath;
        }

        /// <summary>
        /// 根据项目ID取得项目结点ID
        /// Created:2017.04.07(Xuxb)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public string GetNodeIdByProjectId(string projectId)
        {
            return dao.GetNodeIdByProjectId(projectId);
        }



        /// <summary>
        /// 获取底层节点（带状态）
        /// Created:20170605(ChengMengjia)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public List<PNode> GetNodesWithStatus(string ProjectID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  select a.*,CASE WHEN b.cc is Null then ( case a.PType  ");
            //交付物
            sql.Append(" when 1 then (case when d.PType=5 then 1 ");
            sql.Append(" when c.EndDate<date('now') and (d.PType is null or d.PType<>5) then 3 ");
            sql.Append(" when c.StarteDate>=date('now','+1 day') and (d.PType is null or d.PType<>5) then 0  else 2 end) ");
            //日常
            sql.Append(" when 2 then (case when e.FinishStatus=3 then 1 ");
            sql.Append(" when e.EndDate<date('now') and (e.FinishStatus is null or e.FinishStatus<>3) then 3 ");
            sql.Append(" when e.StartDate>=date('now','+1 day') and (e.FinishStatus is null or e.FinishStatus<>3) then 0 else 2 end ) ");
            //问题
            sql.Append(" when 3 then (case when f.HandleStatus=3 then 1 ");
            sql.Append(" when f.EndDate<date('now') and (f.HandleStatus is null or f.HandleStatus<>3) then 3  ");
            sql.Append(" when f.StarteDate>=date('now','+1 day') and (f.HandleStatus is null or f.HandleStatus<>3) then 0 else 2 end ) ");

            sql.Append(" else null end) else null end FinishStatus from pnode a ");
            sql.Append(" left join (select parentid,count(*)cc from pnode where status=1 and PID=@PID group by parentid)b on a.id=b.parentid   ");
            sql.Append(" left join DeliverablesJBXX c on c.NodeID=substr(a.ID,1,36) and c.Status=1 and a.PType=1 ");
            sql.Append(" left join NodeProgress d on d.NodeID=substr(a.ID,1,36) and d.Status=1 ");
            sql.Append(" left join Routine e on e.NodeID=substr(a.ID,1,36) and e.Status=1 and a.PType=2  ");
            sql.Append(" left join Trouble f on f.NodeID=substr(a.ID,1,36) and f.Status=1 and a.PType=3  ");
            sql.Append(" where  a.status=1 and a.PID=@PID ");
            List<QueryField> qf = new List<QueryField>();
            qf.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectID });
            DataSet ds = NHHelper.ExecuteDataset(sql.ToString(), qf);
            if (ds == null || ds.Tables.Count == 0)
                return new List<PNode>();
            else
                return JsonHelper.TableToList<PNode>(ds.Tables[0]);
        }


    }
}
