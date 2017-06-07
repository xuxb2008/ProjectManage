using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ProjectManagement.Common;
using BussinessDLL;
using DomainDLL;
using CommonDLL;
using Newtonsoft.Json.Linq;
using DevComponents.Editors;

namespace ProjectManagement.Forms.WBS
{
    /// <summary>
    /// 画面名：工作分解结构
    /// Created:20170324(ChengMengjia)
    /// </summary>
    public partial class WBS : BaseForm
    {
        #region 业务类初期化
        WBSBLL bll = new WBSBLL();
        #endregion

        #region 画面变量
        PNode _SelectNode;
        DeliverablesJBXX _SelectJBXX;
        List<DeliverablesWork> listWork;
        bool IsRightClick = false;//当前对左侧树是否为右击选择
        #endregion

        #region 事件
        public WBS()
        {
            InitializeComponent();

            DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
        }


        /// <summary>
        /// 节点拖拽放置前事件
        /// Created:20170406(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree1_BeforeNodeDrop(object sender, DevComponents.AdvTree.TreeDragDropEventArgs e)
        {
            //顶级节点或移到顶级时 取消放置
            if (e.OldParentNode == null || e.NewParentNode == null)
            {
                MessageBox.Show("不可移动此位置！");
                e.Cancel = true;
                return;
            }
            int newPosition = e.InsertPosition + 1;
            PNode node = JsonHelper.StringToEntity<PNode>(e.Node.TagString);
            JsonResult result = bll.SaveNode(node, newPosition, e.NewParentNode.Name);
            if (result.result)
            {
                _SelectNode = node;
                DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                DataHelper.SetTreeSelectByValue(advTree1, _SelectNode.ID);
                FileHelper.WBSMoveFloder(UploadType.WBS,_SelectNode.ID);//迁移文件夹
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();

            }
            else
            {
                e.Cancel = true;
                MessageBox.Show(result.msg);
            }
        }


        /// <summary>
        /// 节点清空
        /// Created:20170323(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearNode();
        }

        /// <summary>
        /// 节点保存
        /// Created:20170323(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            PNode node = new PNode()
            {
                PID = ProjectId,
                Name = txtNode.Text,
                ParentID = txtParent.Tag == null ? "" : txtParent.Tag.ToString(),
                PType = 0
            };

            #region 检查
            if (string.IsNullOrEmpty(node.PID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(node.ParentID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "上级节点");
                return;
            }
            if (string.IsNullOrEmpty(node.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "节点名称");
                return;
            }
            #endregion

            JsonResult result = bll.SaveNode(node);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                ClearNode();
                DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                DataHelper.SetTreeSelectByValue(advTree1, node.ParentID);//遍历绑上原来的上级节点
                txtNode.Focus();

                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();

            }
        }

        /// <summary>
        /// 交付物信息清空
        /// Created:20170329(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear2_Click(object sender, EventArgs e)
        {
            if (_SelectNode.PType == 1)
            {
                LoadJFW();
            }
            else
            {
                ClearJFW();
            }
        }

        /// <summary>
        /// 交付物信息保存
        /// Created:20170329(ChengMengjia)
        /// Updated：20170414（Xuxb）保存后刷新首页成果图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (GetEditManager(true))//如果填写无误
            {
                if (_SelectNode.PType == 1)
                    UpdateJFW(listWork);
                else
                    AddJFW(listWork);
            }
        }

        /// <summary>
        /// Node上下文菜单-上移一级
        /// Created:20170524 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolNodeUp_Click(object sender, EventArgs e)
        {
            PNode node = JsonHelper.StringToEntity<PNode>(EditNodeMenu.Tag.ToString());
            PNode ParentNode = bll.GetNode(node.ParentID);//获取父级节点
            if (string.IsNullOrEmpty(ParentNode.ParentID))
                //父级节点已经是顶级了
                MessageBox.Show("无法再向上移动！");
            else
            {
                node.ParentID = ParentNode.ParentID;
                JsonResult result = bll.SaveNode(node);
                if (result.result)
                {
                    _SelectNode = node;
                    DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                    DataHelper.SetTreeSelectByValue(advTree1, _SelectNode.ID);
                    FileHelper.WBSMoveFloder(UploadType.WBS,_SelectNode.ID);//迁移文件夹

                    //主框更新
                    MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                    mainForm.ReloadCurrentNode(node);
                    mainForm.RelaodTree();

                }
                else
                    MessageHelper.ShowRstMsg(false);
            }
        }

        /// <summary>
        ///  Node上下文菜单-节点类型转换
        /// Created:20170524 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolNodeExchange_Click(object sender, EventArgs e)
        {
            PNode node = JsonHelper.StringToEntity<PNode>(EditNodeMenu.Tag.ToString());
            #region 如果不是交付物 或有子节点
            if (node.PType == 0 && bll.GetChildren(node.ID).Count > 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000007, MessageType.Alert);
                return;
            }
            #endregion
            node.PType = node.PType == 0 ? 1 : 0;
            JsonResult result = bll.SaveNode(node);
            if (result.result)
            {
                _SelectNode = node;
                DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                DataHelper.SetTreeSelectByValue(advTree1, _SelectNode.ID);

                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.ReloadCurrentNode(node);
                mainForm.RelaodTree();
            }
            else
                MessageBox.Show(result.msg);
        }

        /// <summary>
        ///  Node上下文菜单-改名
        /// Created:20170524 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolNodeRename_Click(object sender, EventArgs e)
        {
            PNode node = JsonHelper.StringToEntity<PNode>(EditNodeMenu.Tag.ToString());
            Forms.WBS.ReName reName = new Forms.WBS.ReName(node);
            if (reName.ShowDialog() == DialogResult.OK)
            {
                reName.Close();
                _SelectNode = node;
                DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                DataHelper.SetTreeSelectByValue(advTree1, _SelectNode.ID);
                FileHelper.WBSMoveFloder(UploadType.WBS,_SelectNode.ID);//迁移文件夹

                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.ReloadCurrentNode(node);
                mainForm.RelaodTree();
            }
        }

        /// <summary>
        ///  WBS节点树鼠标点击事件
        /// Created:20170524 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree1_MouseDown(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            IsRightClick = e.Button == System.Windows.Forms.MouseButtons.Right;//是否为右击事件
            if (IsRightClick)
            {
                PNode node = JsonHelper.StringToEntity<PNode>(e.Node.Tag.ToString());
                if (string.IsNullOrEmpty(node.ParentID))
                    return;//顶级节点不能修改
                DataHelper.SetTreeSelectByValue(advTree1, node.ID);
                Point point = advTree1.PointToClient(Cursor.Position);
                this.toolNodeExchange.Visible = true;
                if (node.PType == 0)
                    this.toolNodeExchange.Text = "转为交付物";
                else if (node.PType == 1)
                    this.toolNodeExchange.Text = "转为节点";
                else
                    this.toolNodeExchange.Visible = false;
                this.EditNodeMenu.Tag = e.Node.Tag;
                this.EditNodeMenu.Show(advTree1, point);

            }
        }

        /// <summary>
        /// 当左侧树控制选择项改变时操作
        /// Updated:20170524(ChengMengjia) 增加判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree1_BeforeNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
        {
            //未选中节点 或者 选中的节点已经是选中状态了
            if (e.Node == null || (_SelectNode != null && _SelectNode.ID.Equals(e.Node.Name)))
                e.Cancel = true;
        }


        /// <summary>
        /// 上级节点选择后的触发事件
        /// Created:20170324(ChengMengjia)
        /// Updated:20170329(ChengMengjia) 增加判断是否里程碑、交付物，是即不能进行交付物添加 
        /// Updated:20170329(ChengMengjia) lixx提出交付物节点可以编辑修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            _SelectNode = JsonHelper.StringToEntity<PNode>(e.Node.Tag.ToString());
            _SelectJBXX = new DeliverablesJBXX();
            if (_SelectNode.PType == 1)
                LoadJFW();
            else
                LoadNode();
        }

        /// <summary>
        /// 责任人-单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDel")
            {
                listWork = listWork.Where(t => t.Manager != e.GridCell.GridRow.GetCell("Manager").Value.ToString()).ToList();
                gridManager.PrimaryGrid.DataSource = listWork;
            }
        }

        /// <summary>
        /// 权值变化触发事件
        /// Created:20170329(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sdWeight_ValueChanged(object sender, EventArgs e)
        {
            sdWeight.Text = sdWeight.Value.ToString();
        }
        /// <summary>
        /// 添加责任人
        /// Created：20170526(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddManager_Click(object sender, EventArgs e)
        {
            #region 计算剩余工作量
            int tmp = intWorkload.Value;//工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                tmp = tmp - int.Parse(cells[2]);
            }
            #endregion
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager("", tmp < 0 ? 0 : tmp, tmp < 0 ? 0 : tmp);
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                GetEditManager(false);//更新责任人列表
                bool IsExist = false;//是否列表中存在该责任人
                #region 查找该责任人 存在即修改
                foreach (var t in listWork)
                {
                    if (t.Manager.Equals(fmNewManager.ReturnValue.Manager.Substring(0, 36)))
                    {
                        IsExist = true;
                        t.ManagerName = fmNewManager.ReturnValue.ManagerName;
                        t.Workload += fmNewManager.ReturnValue.Workload;
                        t.ActualWorkload += fmNewManager.ReturnValue.ActualWorkload;
                        break;
                    }
                }
                #endregion
                #region 不存在即新增
                if (!IsExist)
                {
                    listWork.Add(new DeliverablesWork()
                    {
                        Manager = fmNewManager.ReturnValue.Manager.Substring(0, 36),
                        ManagerName = fmNewManager.ReturnValue.ManagerName,
                        Workload = fmNewManager.ReturnValue.Workload,
                        ActualWorkload = fmNewManager.ReturnValue.ActualWorkload
                    });
                }
                #endregion
                gridManager.PrimaryGrid.DataSource = listWork;
            }
        }

        /// <summary>
        /// 责任人双击-修改
        ///  Created：20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            string tmp = e.GridRow.ToString();
            tmp = tmp.Substring(tmp.LastIndexOf("{") + 1);
            tmp = tmp.Substring(0, tmp.LastIndexOf("}"));
            string[] cells = tmp.Trim().Split(',');
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager(cells[0], int.Parse(cells[2]), int.Parse(cells[3]));
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 0).Value = fmNewManager.ReturnValue.Manager;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 1).Value = fmNewManager.ReturnValue.ManagerName;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 2).Value = fmNewManager.ReturnValue.Workload;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 3).Value = fmNewManager.ReturnValue.ActualWorkload;
                GetEditManager(false);
            }
        }

        /// <summary>
        /// 开始或结束时间值变化
        /// Created:20170601(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dt_ValueChanged(object sender, EventArgs e)
        {
            if (dtStart.Value != null && dtEnd.Value != null)
            {
                if (dtEnd.Value < dtStart.Value)
                    dtEnd.Value = dtStart.Value;
                intWorkload.Value = DateHelper.ComputeWorkDays(dtStart.Value, dtEnd.Value);
            }
        }
        #endregion

        #region 方法


        /// <summary>
        /// 交付物信息加载（作为当前节点信息展示）
        /// Created:20170401(ChengMengjia)
        /// Updated：20170526（ChengMengjia） 责任人变为列表加载
        /// </summary>
        void LoadJFW()
        {
            DevComponents.AdvTree.Node JFW_Node = advTree1.SelectedNode;
            panelNode.Enabled = false;

            _SelectJBXX = new WBSBLL().GetJBXX(_SelectNode.ID);

            //上方节点信息
            txtParent.Text = JFW_Node.Parent.Text;
            txtParent.Tag = JFW_Node.Parent.Name;
            txtNode.Text = _SelectJBXX.Name;
            txtNode.Tag = _SelectJBXX.ID;

            //下方交付物基本信息
            txtJFWParent.Text = JFW_Node.Parent.Text;
            txtJFWParent.Tag = JFW_Node.Parent.Name;
            txtJFW.Text = _SelectJBXX.Name;
            txtDesc.Text = _SelectJBXX.Desc;
            dtStart.Value = (DateTime)_SelectJBXX.StarteDate;
            dtEnd.Value = (DateTime)_SelectJBXX.EndDate;
            intWorkload.Value = int.Parse(_SelectJBXX.Workload.ToString());

            //责任人
            listWork = bll.GetManagerWorks(_SelectJBXX.ID);
            gridManager.PrimaryGrid.DataSource = listWork;

        }

        /// <summary>
        /// 普通节点加载（作为上级节点信息显示）
        /// Created:20170401(ChengMengjia)
        /// </summary>
        void LoadNode()
        {
            ClearNode();
            ClearJFW();
            DevComponents.AdvTree.Node node = advTree1.SelectedNode;
            panelJFW.Enabled = true;
            panelNode.Enabled = true;

            txtParent.Text = node.Text;
            txtParent.Tag = node.Name;
            txtJFWParent.Text = node.Text;
            txtJFWParent.Tag = node.Name;
        }
        /// <summary>
        /// 上方节点信息清空
        /// Created:20170401(ChengMengjia)
        /// </summary>
        private void ClearNode()
        {
            txtNode.Clear();
            txtNode.Tag = "";
            //txtParent.Clear();
            //txtParent.Tag = "";
        }

        /// <summary>
        /// 下方交付物信息清空
        /// Created:20170401(ChengMengjia)
        /// </summary>
        private void ClearJFW()
        {
            //txtJFWParent.Clear();
            //txtJFWParent.Tag = "";
            txtJFW.Clear();
            txtJFW.Tag = "";
            txtDesc.Clear();
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            intWorkload.Value = 1;
            sdWeight.Value = 1;
            listWork = null;
            gridManager.PrimaryGrid.DataSource = listWork;
        }

        /// <summary>
        /// 添加交付物基本信息
        ///  Created:20170401(ChengMengjia)
        /// </summary>
        void AddJFW(List<DeliverablesWork> listWork)
        {
            PNode node = new PNode()
            {
                PID = ProjectId,
                Name = txtJFW.Text,
                ParentID = txtJFWParent.Tag == null ? "" : txtJFWParent.Tag.ToString(),
                PType = 1
            };
            DeliverablesJBXX entity = new DeliverablesJBXX()
            {
                ID = "",
                EndDate = dtEnd.Value,
                // Manager = cbManager.SelectedItem == null ? "" : ((ComboItem)cbManager.SelectedItem).Value.ToString(),
                StarteDate = dtStart.Value,
                Workload = intWorkload.Value,
                Weight = sdWeight.Value,
                Name = txtJFW.Text,
                Desc = txtDesc.Text
            };

            #region 检查
            if (string.IsNullOrEmpty(node.PID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(node.ParentID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "所属节点");
                return;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "交付物名称");
                return;
            }
            #endregion

            JsonResult result = bll.AddJFWNode(node, entity, listWork);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                ClearJFW();
                DataHelper.SetTreeDate(advTree1, ProjectId);//绑定树形数据
                DataHelper.SetTreeSelectByValue(advTree1, node.ParentID);//遍历绑上原来的上级节点
                txtJFW.Focus();

                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectResult();
            }
        }

        /// <summary>
        /// 更新交付物基本信息
        ///  Created:20170401(ChengMengjia)
        /// </summary>
        void UpdateJFW(List<DeliverablesWork> listWork)
        {
            string jbxxid = _SelectJBXX.ID;
            _SelectJBXX.EndDate = dtEnd.Value;
            _SelectJBXX.StarteDate = dtStart.Value;
            _SelectJBXX.Workload = intWorkload.Value;
            _SelectJBXX.Name = txtJFW.Text;
            _SelectJBXX.Desc = txtDesc.Text;
            _SelectJBXX.Weight = sdWeight.Value;
            #region 检查
            if (string.IsNullOrEmpty(_SelectJBXX.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "交付物名称");
                return;
            }
            #endregion
            JsonResult result = bll.UpdateJBXX(_SelectJBXX, listWork);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                _SelectNode.Name = _SelectJBXX.Name;
                _SelectNode.ID = result.data.ToString();
                txtNode.Text = _SelectNode.Name;
                advTree1.SelectedNode.Text = _SelectNode.WBSNo + "-" + _SelectNode.Name;
                advTree1.SelectedNode.Name = _SelectNode.ID;
                advTree1.SelectedNode.Tag = JsonHelper.EntityToString<PNode>(_SelectNode);
                FileHelper.WBSMoveFloder(UploadType.WBS,_SelectNode.ID);//迁移文件夹

                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectResult();
            }
            else
                _SelectJBXX = bll.GetJBXX(jbxxid);
        }

        /// <summary>
        /// 获取编辑的责任人列表
        /// Created:20170526(ChengMengjia)
        /// </summary>
        /// <param name="NeedCheck">是否需要检查工作量大小</param>
        /// <returns></returns>
        bool GetEditManager(bool NeedCheck)
        {
            listWork = new List<DeliverablesWork>();
            //责任人
            int totalWork = 0;//总的工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                listWork.Add(new DeliverablesWork() { Manager = cells[0], ManagerName = cells[1], Workload = int.Parse(cells[2]), ActualWorkload = int.Parse(cells[3]) });
                totalWork += int.Parse(cells[2]);
            }
            if (NeedCheck)
            {
                if (totalWork > intWorkload.Value)
                {
                    MessageBox.Show("超过设置的总工作量，请检查！");
                    return false;
                }
                else if (totalWork < intWorkload.Value)
                {
                    MessageBox.Show("小于设置的总工作量，请检查！");
                    return false;
                }
            }
            return true;
        }

        #endregion


    }
}
