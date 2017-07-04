using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainDLL;
using BussinessDLL;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using CommonDLL;
using DevComponents.Editors;
using System.Data;
using System.Drawing;
using DevComponents.DotNetBar.SuperGrid.Style;
namespace ProjectManagement
{
    public static class DataHelper
    {

        /// <summary>
        /// 根据项目ID取得项目结点ID
        /// Created:2017.04.07(Xuxb)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static string GetNodeIdByProjectId(string projectId)
        {
            return new WBSBLL().GetNodeIdByProjectId(projectId);
        }


        #region 首页节点树 展示状态颜色
        /// <summary>
        /// WBS节点数加载
        /// Created:20170602(ChengMengjia)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        public static void SetMainTreeDate(DevComponents.AdvTree.AdvTree advTree1, string ProjectID)
        {
            advTree1.Nodes.Clear();
            List<PNode> listNode = new WBSBLL().GetNodesWithStatus(ProjectID);


            IEnumerable<PNode> parentNode = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No);
            foreach (PNode parent in parentNode)
            {
                DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node()
                {
                    Name = parent.ID,
                    Text = parent.Name,
                    Tag = JsonHelper.EntityToString<PNode>(parent),
                };
                SetMainSubTreeData(listNode, parent, node);
                node.Style = null;//项目节点
                advTree1.Nodes.Add(node);
            }
            advTree1.ExpandAll();
        }
        /// <summary>
        /// 设定子节点
        ///  Created:20170602(ChengMengjia)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        private static void SetMainSubTreeData(IList<PNode> listNode, PNode parent, DevComponents.AdvTree.Node node)
        {
            string parentID = parent.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No).OrderByDescending(t => t.No.HasValue);
            if (children.Count<PNode>() < 1)
            {
                parent.FinishStatus = parent.FinishStatus == null ? 0 : parent.FinishStatus;
                node.Style = MatchColor(parent.FinishStatus);
                return;
            }
            DevComponents.AdvTree.Node node2;
            foreach (PNode child in children)
            {
                node2 = new DevComponents.AdvTree.Node()
                {
                    Name = child.ID,
                    Text = string.IsNullOrEmpty(child.WBSNo) ? child.Name + "(" + EnumsHelper.GetDescription((WBSPType)child.PType) + ")" : child.WBSNo + "-" + child.Name,
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                #region 交付物需要检查完成情况

                #endregion
                SetMainSubTreeData(listNode, child, node2);
                node.Nodes.Add(node2);
            }
            parent.FinishStatus = (from p in children select p.FinishStatus).Max();
            node.Style = MatchColor(parent.FinishStatus);
        }

        /// <summary>
        /// 节点背景色
        /// Created:20170605(ChengMengjia)
        /// 未开始的没有背景色（值为0），已完成的为蓝色（1），正在执行的为黄色（2），超期的为红色（3）
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private static ElementStyle MatchColor(int? status)
        {
            switch (status)
            {
                case 1:
                    return new ElementStyle() { BackColor = Color.CornflowerBlue };
                case 2:
                    return new ElementStyle() { BackColor = Color.Orange };
                case 3:
                    return new ElementStyle() { BackColor = Color.Red, TextColor = Color.White };
                default:
                    return new ElementStyle() { BackColor = Color.Transparent };
            }
        }

        /// <summary>
        /// 节点背景色
        /// Created:20170607(ChengMengjia)
        /// 未开始的没有背景色（值为0），已完成的为蓝色（1），正在执行的为黄色（2），超期的为红色（3）
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static CellVisualStyles MatchRowColor(int? status)
        {
            CellVisualStyles style = new CellVisualStyles();
            switch (status)
            {
                case 1:
                    style.Default.Background.Color1 = Color.CornflowerBlue;
                    break;
                case 2:
                    style.Default.Background.Color1 = Color.Orange;
                    break;
                case 3:
                    style.Default.Background.Color1 = Color.Red;
                    style.Default.TextColor = Color.White;
                    break;
                default:
                    style.Default.Background.Color1 = Color.Transparent;
                    break;
            }
            return style;
        }
        #endregion

        #region WBS节点数 AdvTree
        /// <summary>
        /// WBS节点数加载
        /// Created:20170323(ChengMengjia)
        /// Updated:20170329(ChengMengjia) 在树节点的tag中存放PNode序列字符串
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        public static void SetTreeDate(DevComponents.AdvTree.AdvTree advTree1, string ProjectID)
        {
            advTree1.Nodes.Clear();
            List<PNode> listNode = new WBSBLL().GetNodes(ProjectID);
            IEnumerable<PNode> parentNode = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No);
            foreach (PNode parent in parentNode)
            {
                DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node()
                {
                    Name = parent.ID,
                    Text = parent.Name,
                    Tag = JsonHelper.EntityToString<PNode>(parent)
                };
                SetSubTreeData(listNode, parent, node);
                advTree1.Nodes.Add(node);
            }
            advTree1.ExpandAll();
        }
        /// <summary>
        /// 设定子节点
        /// Created:20170330(Xuxb)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        private static void SetSubTreeData(IList<PNode> listNode, PNode parent, DevComponents.AdvTree.Node node)
        {
            string parentID = parent.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
            if (children.Count<PNode>() < 1)
            {
                return;
            }
            DevComponents.AdvTree.Node node2;
            foreach (PNode child in children)
            {
                node2 = new DevComponents.AdvTree.Node()
                {
                    Name = child.ID,
                    Text = child.WBSNo + "-" + child.Name,
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                SetSubTreeData(listNode, child, node2);
                node.Nodes.Add(node2);
            }
        }

        /// <summary>
        /// 加载下拉结点列表(包含多选框)
        /// Created:2017/05/22(zhuguanjun)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        /// <param name="NodeType">节点类型</param>
        public static void SetAdvTreeData(DevComponents.AdvTree.AdvTree advTree1, string ProjectID, int? NodeType,string[] checks)
        {
            advTree1.Nodes.Clear();
            List<PNode> listNode = new WBSBLL().GetNodes(ProjectID, NodeType);
            IEnumerable<PNode> parentNode = null;
            DevComponents.AdvTree.Node node = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.CREATED);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentNode.First().ID).OrderBy(t => t.No);
            foreach (PNode child in children)
            {
                node = new DevComponents.AdvTree.Node()
                {
                    Checked = checks != null && checks.Contains(child.ID) ? true : false,
                    CheckBoxVisible = true,
                    Name = child.ID,
                    Text = child.Name,
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                SetSubTreeDataWithCheckBox(listNode, child, node,checks);
                advTree1.Nodes.Add(node);
            }
            advTree1.ExpandAll();
        }
        /// <summary>
        /// 根据CODE设定数的选定值
        /// Created:20170405(Xuxb)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="?"></param>
        /// <param name="value"></param>
        public static void SetTreeSelectByValue(DevComponents.AdvTree.AdvTree advTree1, string value)
        {
            int currentIndex = 0;
            bool catchFlg = false;
            for (int i = 0; i < advTree1.Nodes.Count; ++i)
            {
                DevComponents.AdvTree.Node dr = (DevComponents.AdvTree.Node)(advTree1.Nodes[i]);
                if (dr.Name.ToString().Substring(0, 36) == value.Substring(0, 36))
                {
                    advTree1.SelectedIndex = currentIndex;
                    catchFlg = true;
                    break;
                }
                else
                {
                    currentIndex = GetSubTreeData(dr, value.Substring(0, 36), currentIndex, out catchFlg);
                    if (catchFlg)
                    {
                        advTree1.SelectedIndex = currentIndex;
                        break;
                    }
                    else
                    {
                        currentIndex = currentIndex + 1;
                        continue;
                    }

                }
            }
        }
        #endregion

        #region 下拉节点树 ComboTree

        /// <summary>
        /// 加载下拉结点列表
        /// Created:20170323(ChengMengjia)
        /// Updated:20170605(ChengMengjia) 只需要普通节点PType=0 
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        public static void SetComboxTreeData(DevComponents.DotNetBar.Controls.ComboTree comboTree, string ProjectID)
        {
            comboTree.Nodes.Clear();
            List<PNode> listNode = new WBSBLL().GetNodes(ProjectID, 0);
            IEnumerable<PNode> parentNode = null;
            DevComponents.AdvTree.Node node = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.CREATED);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentNode.First().ID).OrderBy(t => t.No);
            foreach (PNode child in children)
            {
                node = new DevComponents.AdvTree.Node()
                {
                    Name = child.ID,
                    Text = child.Name,
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                SetSubTreeData(listNode, child, node);
                comboTree.Nodes.Add(node);
            }
            comboTree.AdvTree.ExpandAll();
        }

        /// <summary>
        /// 根据CODE设定下拉树的选定值
        /// Created:20170405(Xuxb)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="value"></param>
        public static void SetComboxTreeSelectByValue(DevComponents.DotNetBar.Controls.ComboTree advTree1, string value)
        {
            int currentIndex = 0;
            bool catchFlg = false;
            for (int i = 0; i < advTree1.Nodes.Count; ++i)
            {
                DevComponents.AdvTree.Node dr = (DevComponents.AdvTree.Node)(advTree1.Nodes[i]);
                if (dr.Name.ToString().Substring(0, 36) == value.Substring(0, 36))
                {
                    advTree1.SelectedIndex = currentIndex;
                    catchFlg = true;
                    break;
                }
                else
                {
                    currentIndex = GetSubTreeData(dr, value.Substring(0, 36), currentIndex, out catchFlg);
                    if (catchFlg)
                    {
                        advTree1.SelectedIndex = currentIndex;
                        break;
                    }
                    else
                    {
                        currentIndex = currentIndex + 1;
                        continue;
                    }

                }
            }
        }
        #endregion

        #region 绑定节点树

        /// <summary>
        /// 设定子节点(包含多选框)
        /// Created:2017/05/22(zhuguanjun)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        private static void SetSubTreeDataWithCheckBox(IList<PNode> listNode, PNode parent, DevComponents.AdvTree.Node node,string[] checks)
        {
            string parentID = parent.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
            if (children.Count<PNode>() < 1)
            {
                return;
            }
            DevComponents.AdvTree.Node node2;
            foreach (PNode child in children)
            {
                node2 = new DevComponents.AdvTree.Node()
                {
                    Checked = checks != null && checks.Contains(child.ID) ? true : false,
                    CheckBoxVisible = true,
                    Name = child.ID,
                    Text = child.Name,
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                SetSubTreeDataWithCheckBox(listNode, child, node2,checks);
                node.Nodes.Add(node2);
            }
        }


        /// <summary>
        /// 根据CODE设定下拉树的选定值
        /// Created:20170405(Xuxb)
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="value"></param>
        /// <param name="currentIndex"></param>
        /// <param name="catchFlg"></param>
        /// <returns></returns>
        private static int GetSubTreeData(DevComponents.AdvTree.Node dr, string value, int currentIndex, out bool catchFlg)
        {
            if (dr.Nodes.Count > 0)
            {
                for (int i = 0; i < dr.Nodes.Count; ++i)
                {
                    DevComponents.AdvTree.Node dr1 = (DevComponents.AdvTree.Node)(dr.Nodes[i]);

                    if (dr1.Name.ToString().Substring(0, 36) == value)
                    {
                        currentIndex = currentIndex + 1;
                        catchFlg = true;
                        return currentIndex;
                    }
                    else
                    {
                        currentIndex = GetSubTreeData(dr1, value, currentIndex, out catchFlg);
                        if (catchFlg)
                        {
                            currentIndex = currentIndex + 1;
                            return currentIndex;
                        }
                        else
                        {
                            currentIndex = currentIndex + 1;
                            continue;
                        }
                    }
                }
                catchFlg = false;
                return currentIndex;
            }
            else
            {
                catchFlg = false;
                return currentIndex;
            }
        }

        #endregion

        #region 下拉框操作
        /// <summary>
        /// 按下拉框值选中
        /// Created：20170327(xuxb)
        /// Updated：20170328(ChengMengjia) 将ComboxItem改为ComboItem
        /// </summary>
        /// <param name="cb">下拉框</param>
        /// <param name="Value">值</param>
        public static void SetComboBoxSelectItemByValue(ComboBoxEx cb, string Value)
        {
            for (int i = 0; i < cb.Items.Count; ++i)
            {
                ComboItem dr = (ComboItem)(cb.Items[i]);
                if (dr.Value.ToString() == Value)
                {
                    cb.SelectedIndex = i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// 按下拉框值选中
        /// Created：20170329(ChengMengjia)
        /// </summary>
        /// <param name="cb">下拉框</param>
        /// <param name="Value">值</param>
        public static void SetComboBoxSelectItemByText(ComboBoxEx cb, string Text)
        {
            for (int i = 0; i < cb.Items.Count; ++i)
            {
                ComboItem dr = (ComboItem)(cb.Items[i]);
                if (dr.Text.ToString() == Text)
                {
                    cb.SelectedIndex = i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        #endregion#region 基础数据列表加载

        #region Combobox基础数据列表加载
        /// <summary>
        /// 基础数据列表加载
        /// Created:2017.3.27(ChengMengjia)
        /// update:2017/6/12(zhuguanjun)加个‘空’选项
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="DictNo"></param>
        public static void LoadDictItems(ComboBoxEx combobox, DictCategory DictNo)
        {
            ComboItem itemnull = new ComboItem();
            itemnull.Text = "请选择";
            itemnull.Value = 0;
            combobox.Items.Add(itemnull);

            List<DictItem> listD = new SettingBLL().GetDictItems(DictNo);
            foreach (DictItem dictitem in listD)
            {
                ComboItem item = new ComboItem();
                item.Text = dictitem.Name;
                item.Value = dictitem.No.ToString();
                combobox.Items.Add(item);
            }
        }


        #endregion

        #region 数据表格操作
        /// <summary>
        /// 数据表加序号
        /// Created:20170328(ChengMengjia)
        /// </summary>
        /// <param name="dt"></param>
        public static void AddNoCloumn(DataTable dt)
        {
            dt.Columns.Add("RowNo");
            int i = 1;
            foreach (DataRow row in dt.Rows)
            {
                row["RowNo"] = i;
                i++;
            }
        }


        /// <summary>
        /// 数据表加序号 分页情况下
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        public static void AddNoCloumn(DataTable dt, int pageNo, int pageSize)
        {
            dt.Columns.Add("RowNo");
            int i = 1;
            if (pageNo >= 1)
                i += (pageNo - 1) * pageSize;
            foreach (DataRow row in dt.Rows)
            {
                row["RowNo"] = i;
                i++;
            }
        }

        #endregion

        #region 项目预警信息取得

        /// <summary>
        /// 取得项目预警信息
        /// Created:20170417(xuxb)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public static DataTable GetWarnningData(string projectID)
        {
            DataTable dt = new DataTable();

            //成本超预算提醒
            if (CommonHelper.GetConfigValue(ConstHelper.Warn_Cost).Equals("1"))
            {

            }

            //
            dt = (new ProjectInfoBLL()).GetProjectWarnning(projectID, new bool[] { true, true, true, true }, 7, 7);

            return dt;
        }

        #endregion

        #region

        #endregion
    }
}