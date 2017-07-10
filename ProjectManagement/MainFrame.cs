using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using BussinessDLL;
using DomainDLL;
using Newtonsoft.Json.Linq;
using CommonDLL;
using ProjectManagement.Common;
using System.IO;

namespace ProjectManagement
{
    public partial class MainFrame : BaseForm
    {

        #region 业务类初期化
        MainFrameBLL bll = new MainFrameBLL();
        ProjectBLL proBLL = new ProjectBLL();
        WBSBLL wbsBll = new WBSBLL();
        #endregion

        #region 画面变量
        string _SelectedNodeID;//左侧wbs节点树所选NodeID
        bool IsRightClick = false;//当前对左侧树是否为右击选择

        private NormalOperation nodePage { get; set; }
        private ProjectManagement.Forms.Others.Routine nodeRoutine { get; set; }
        private ProjectManagement.Forms.Others.Trouble nodeTrouble { get; set; }


        string[] NodeTabs = { "NormalOperation" };//切换节点时所需关闭的页面
        #endregion

        #region 事件
        public MainFrame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化主窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Load(object sender, EventArgs e)
        {
            //MainSuperTabControl.Tabs.Clear();//初始化TabControl的Tabs
            //ShowChildForm(new StartPage());//创建启动窗体
            //ShowChildForm(new ProjectInfo());//显示项目的基本信息
            //MainSuperTabControl.SelectedTabIndex = 0;//首页做为第一个页面
            LoadProjects();//加载项目列表
        }

        /// <summary>
        /// 项目列表子项点击事件
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prolist_Item_Click(object sender, EventArgs e)
        {
            ButtonItem item = (ButtonItem)sender;
            if (item.Name == ProjectId)
                return;//点击的就是当前选中项目
            if (MessageHelper.ShowMsg(MessageID.I000000003, MessageType.Confirm) == DialogResult.OK)
            {
                ProjectId = item.Name;
                ProjectName = item.Text;
                ProjectNo = item.Tag.ToString();
                DataHelper.SetMainTreeDate(WbsTree, ProjectId);//绑定树形数据
                _SelectedNodeID = null;
                CurrentNode = null;
                MainSuperTabControl.Tabs.Clear();//初始化TabControl的Tabs
                ShowChildForm(new StartPage());//创建启动窗体
                ////ShowChildForm(new ProjectInfo());//显示项目的基本信息
                ////加载项目的基本信息tab
                //SuperTabItem tabItem = MainSuperTabControl.CreateTab("基本信息");
                //tabItem.Text = "基本信息";
                //tabItem.Name = "ProjectInfo";
                //MainSuperTabControl.SelectedTabIndex = 0;//首页做为第一个页面
            }
            CurrentNode = null;//当前无选中节点
        }


        #region 左侧WBS节点树

        /// <summary>
        /// 左侧WBS节点树上方刷新按钮点击事件
        ///  Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (MessageHelper.ShowMsg(MessageID.I000000005, MessageType.Confirm) == DialogResult.OK)
            {
                DataHelper.SetMainTreeDate(WbsTree, ProjectId);//绑定树形数据
                _SelectedNodeID = null;
                CurrentNode = null;
                CloseTabs(NodeTabs);//关闭对应窗体
            }
        }

        /// <summary>
        /// WBS节点树鼠标点击事件
        /// Created:20170406 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WbsTree_NodeMouseDown(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            IsRightClick = e.Button == System.Windows.Forms.MouseButtons.Right;//是否为右击事件
            if (IsRightClick)
            {
                PNode node = JsonHelper.StringToEntity<PNode>(e.Node.Tag.ToString());
                Point point = WbsTree.PointToClient(Cursor.Position);
                if (string.IsNullOrEmpty(node.ParentID))
                {
                    //顶级节点
                    this.EditProMenu.Show(WbsTree, point);
                }
                else
                {
                    if (node.PType == 0)
                        this.toolNodeExchange.Text = "转为交付物";
                    else if (node.PType == 1)
                        this.toolNodeExchange.Text = "转为节点";
                    else
                        return;
                    this.EditNodeMenu.Tag = e.Node.Tag;
                    this.EditNodeMenu.Show(WbsTree, point);
                }
            }
        }

        /// <summary>
        /// 当左侧树控制选择项改变时操作
        /// Updated:20170329(ChengMengjia) 增加判断
        /// Updated:20170401(ChengMengjia) liuxx提出去除confirm弹出框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WbsTree_BeforeNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeCancelEventArgs e)
        {
            if (IsRightClick || e.Node == null)
            {
                //右击选择或未选中节点
                e.Cancel = true;
                return;
            }
            string SelectedNodeID = e.Node.Name;
            if (string.IsNullOrEmpty(_SelectedNodeID) || !_SelectedNodeID.Equals(SelectedNodeID))
            {
                //普通节点或交付物
                CurrentNode = JsonHelper.StringToEntity<PNode>(e.Node.Tag.ToString());
                _SelectedNodeID = SelectedNodeID;
                switch (CurrentNode.PType)
                {
                    case (int)WBSPType.PType0:
                    case (int)WBSPType.PType1:
                        #region 打开当前操作
                        OpenNormalOperation();
                        if (nodeRoutine != null)
                            CloseTab(nodeRoutine.Name);
                        if (nodeTrouble != null)
                            CloseTab(nodeTrouble.Name);
                        #endregion
                        break;
                    case (int)WBSPType.PType2:
                        #region 打开日常工作
                        OpenRoutine();
                        if (nodePage != null)
                            CloseTab(nodePage.Name);
                        if (nodeTrouble != null)
                            CloseTab(nodeTrouble.Name);
                        #endregion
                        break;
                    case (int)WBSPType.PType3:
                        #region 打开问题
                        OpenTrouble();
                        if (nodePage != null)
                            CloseTab(nodePage.Name);
                        if (nodeRoutine != null)
                            CloseTab(nodeRoutine.Name);
                        #endregion
                        break;
                }
            }
            else e.Cancel = true;
        }

        /// <summary>
        /// Node上下文菜单-节点类型转换
        /// Created:20170406 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolNodeExchange_Click(object sender, EventArgs e)
        {
            PNode node = JsonHelper.StringToEntity<PNode>(EditNodeMenu.Tag.ToString());
            #region 如果不是交付物 检查是否有子节点
            if (node.PType == 0 && wbsBll.GetChildren(node.ID).Count > 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000007, MessageType.Alert);
                return;
            }
            #endregion
            node.PType = node.PType == 0 ? 1 : 0;
            JsonResult result = wbsBll.SaveNode(node);
            if (result.result)
            {
                ReloadCurrentNode(node);
                RelaodTree();
            }
            else
                MessageBox.Show(result.msg);

        }

        /// <summary>
        /// Node上下文菜单-改名
        /// Created:20170406 (ChengMengjia)
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
                ReloadCurrentNode(node);
                RelaodTree();
            }
        }

        /// <summary>
        /// Node上下文菜单-上移一级
        /// Created:20170406 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolNodeUp_Click(object sender, EventArgs e)
        {
            PNode node = JsonHelper.StringToEntity<PNode>(EditNodeMenu.Tag.ToString());
            PNode ParentNode = wbsBll.GetNode(node.ParentID);//获取父级节点
            if (string.IsNullOrEmpty(ParentNode.ParentID))
                //父级节点已经是顶级了
                MessageBox.Show("无法再向上移动！");
            else
            {
                node.ParentID = ParentNode.ParentID;
                JsonResult result = wbsBll.SaveNode(node);
                if (result.result)
                {
                    FileHelper.WBSMoveFloder(UploadType.WBS, node.ID);//迁移文件夹
                    ReloadCurrentNode(node);
                    RelaodTree();
                }
                else
                    MessageHelper.ShowRstMsg(false);
            }
        }

        #endregion

        /// <summary>
        /// 上方菜单tab行右击事件
        /// Created：20170410（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSuperTabControl_TabStripMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point point = MainSuperTabControl.PointToClient(Cursor.Position);
                this.CloseTabContextMenu.Show(MainSuperTabControl, point);
            }
        }



        #region 窗体事情，代码块

        /// <summary>
        /// 点击TabControl的关闭按钮时，首页和项目信息两个TAB不关闭，其它正常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superTabControl1_TabItemClose(object sender, SuperTabStripTabItemCloseEventArgs e)
        {
            if (e.Tab.Name == "StartPage")
            {
                e.Cancel = true;
            }
            if (e.Tab.Name == "NormalOperation")
            {
                nodePage = null;
            }
            if (e.Tab.Name == "Routine")
            {
                nodeRoutine = null;
            }
            if (e.Tab.Name == "Trouble")
            {
                nodeTrouble = null;
            }
        }

        /// <summary>
        /// 打开创建新项目的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_NewProject_Click(object sender, EventArgs e)
        {
            Forms.Project.NewProject newProject = new Forms.Project.NewProject(true);
            if (newProject.ShowDialog() == DialogResult.OK)
            {
                newProject.Close();
                LoadProjects();//加载项目列表
            }
        }
        /// <summary>
        /// 打开创建WBS界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_CreateWBS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectId))
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
            else
                ShowChildForm(new Forms.WBS.WBS());
        }
        /// <summary>
        /// 打开干系人清单界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Stakeholder_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Stakeholder.Stakeholder());
        }
        /// <summary>
        /// 打开沟通方式界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Communication_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Stakeholder.Communication());
        }
        /// <summary>
        /// 打开沟通分析矩阵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_CommunicationMatrix_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Stakeholder.CommunicationMatrix());
        }
        /// <summary>
        /// 打开供应商管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Supplier_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Subcontract.Supplier());
        }
        /// <summary>
        /// 打开分包合同的管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Contract_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Subcontract.Contract());
        }
        /// <summary>
        /// 打开预警配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_WarningConfigure_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Warning.WarningConfigure());
        }
        /// <summary>
        /// 打开预警信息界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_WarningList_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Warning.WarningList());
        }

        /// <summary>
        /// 打开信息发布的配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_PublishConfigure_Click(object sender, EventArgs e)
        {

            ShowChildForm(new Forms.InfomationPublish.PublishConfigure());

        }

        /// <summary>
        /// 开开信息发布历史记录界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_PublishHistory_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.InfomationPublish.PublishHistory());
        }
        /// <summary>
        /// 打开信息发布界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_InfomationPublish_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.InfomationPublish.InfomationPublish());
        }
        /// <summary>
        /// 打开成本管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Cost_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Income.Cost());
        }
        /// <summary>
        /// 打开收入管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Earning_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Income.Earning());
        }
        /// <summary>
        /// 打开风险管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Risk_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.Risk());
        }
        /// <summary>
        /// 打开变更管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Change_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.Change());
        }
        /// <summary>
        /// 打开日常工作界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Routine_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.Routine(""));
        }
        /// <summary>
        /// 打开问题管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Trouble_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.Trouble(""));
        }
        /// <summary>
        /// 打开生成周报界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Weekly_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.WeeklyPreview());
        }
        /// <summary>
        /// 打开基础配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Setting_Click(object sender, EventArgs e)
        {
            ShowChildForm(new FormSetting());
        }

        /// <summary>
        /// 打开周报发送历史界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_WeeklyHistory_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Others.WeeklyHistory(null));
        }

        /// <summary>
        /// 打开上传模板的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_UpdateTemplate_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Template.UpdateTemplate());
        }

        /// <summary>
        /// 打开定义WBS代码的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_WBSCode_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.WBS.WBSCode());
        }

        /// <summary>
        /// 打开成本使用情况报表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Repost_Cost_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Cost());
        }
        /// <summary>
        /// 打开收入情况报表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_Earning_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Earning());
        }
        /// <summary>
        /// 打开收款情况报表界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_Receivables_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Receivables());
        }

        /// <summary>
        /// 成员贡献率
        /// 2017/06/07(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MemberContributionRate_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_MemberContributionRate());
        }

        /// <summary>
        /// 关闭所有窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllWindows_Click(object sender, EventArgs e)
        {
            List<String> names = new List<String>();
            foreach (SuperTabItem item in MainSuperTabControl.Tabs)
            {
                if (item.Name != "StartPage" && item.Name != "ProjectInfo")
                {
                    names.Add(item.Name);
                }
            }
            CloseTabs(names.ToArray());
        }
        /// <summary>
        /// 关闭所有的窗体，除了自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllWindows_ExcludeSelf_Click(object sender, EventArgs e)
        {

            string currenttabname = MainSuperTabControl.SelectedTab.Name;

            //currenttabname = (sender as ContextMenuStrip).SourceControl.Name;

            List<string> names = new List<String>();
            foreach (SuperTabItem item in MainSuperTabControl.Tabs)
            {
                if (item.Name != "StartPage" && item.Name != "ProjectInfo" && item.Name != currenttabname)
                {
                    names.Add(item.Name);
                }
            }
            CloseTabs(names.ToArray());
        }



        /// <summary>
        /// 打开项目计划报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Report_Plan_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Plan());
        }

        /// <summary>
        /// 打开项目基本信息报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_ProjectInfo_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_ProjectInfo());
        }
        /// <summary>
        /// 打开分包信息报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_Subcontract_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Subcontract());
        }
        /// <summary>
        /// 打开近期工作报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_RecentWork_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Recentwork());
        }
        /// <summary>
        /// 打开待解决问题报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_ToBeSolvedTrouble_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_ToBeSolvedTrouble());
        }
        /// <summary>
        /// 打开周报报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_Weekly_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Weekly());
        }
        /// <summary>
        /// 打开项目基本信息界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_ProjectInfo_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Project.ProjectInfo());
        }
        /// <summary>
        /// 打开里程碑界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Milestone_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Project.FormMilestone());
        }
        /// <summary>
        /// 打开监理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Supervisor_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Project.FormSupervisor());
        }
        /// <summary>
        /// 打开收款界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RibbonBtn_Receivables_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Income.FormReceivables());
        }


        /// <summary>
        /// 打开供应商信息报表的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ribbon_Report_Supplier_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_Supplier());
        }
        /// <summary>
        /// 点击项目基本信息时
        /// Created:2017.3.28(xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainSuperTabControl_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            //if (e.NewValue.Name == "ProjectInfo")
            //{
            //    SuperTabItem item = (SuperTabItem)MainSuperTabControl.Tabs["ProjectInfo"];
            //    if (item.AttachedControl.Controls.Count == 0)
            //    {
            //        Office2007RibbonForm form = new ProjectInfo();
            //        //不存在，创建TabItem，并激活
            //        form.FormBorderStyle = FormBorderStyle.None;
            //        form.Dock = DockStyle.Fill;
            //        //开始 liuxuexian
            //        //为了适应不同屏幕的分辨率，设置最小出现滚动条的值
            //        form.AutoScroll = true;
            //        form.AutoScrollMinSize = new Size(1100, 800);
            //        //结束
            //        form.TopLevel = false;
            //        form.Visible = true;
            //        item.AttachedControl.Controls.Add(form);//显示项目的基本信息
            //    }
            //}
        }

        #endregion
        #endregion

        #region 方法

        /// <summary>
        /// 加载项目列表
        /// Created:20170324(ChengMengjia)
        /// Updated:20170414(Xuxb) 把首页设定到静态变量中
        /// </summary>
        private void LoadProjects()
        {
            icProlist.SubItems.Clear();
            List<Project> listPro = bll.GetProList();
            foreach (Project project in listPro)
            {
                ButtonItem item = new ButtonItem();
                item.Text = project.Name;
                item.Name = project.ID;
                item.Tag = project.No;
                item.Click += new System.EventHandler(Prolist_Item_Click);
                icProlist.SubItems.Add(item);
            }
            //默认选择第一个项目
            if (listPro.Count > 0)
            {
                ProjectId = icProlist.SubItems[0].Name;
                ProjectName = icProlist.SubItems[0].Text;
                ProjectNo = icProlist.SubItems[0].Tag.ToString();
                DataHelper.SetMainTreeDate(WbsTree, ProjectId);//绑定树形数据
                MainSuperTabControl.Tabs.Clear();//初始化TabControl的Tabs
                startPage = new StartPage();
                ShowChildForm(startPage);//创建启动窗体
                ////ShowChildForm(new ProjectInfo());//显示项目的基本信息
                ////加载项目的基本信息tab
                //SuperTabItem tabItem = MainSuperTabControl.CreateTab("基本信息");
                //tabItem.Text = "基本信息";
                //tabItem.Name = "ProjectInfo";
                //MainSuperTabControl.SelectedTabIndex = 0;//首页做为第一个页面
            }
        }

        #region 窗体-常用方法代码块
        /// <summary>
        /// 打开当前操作
        /// Created:20170409 (ChengMengjia)
        /// </summary>
        public void OpenNormalOperation()
        {
            if (nodePage == null)
                nodePage = new NormalOperation();
            else
                nodePage.init();
            ShowChildForm(nodePage);
        }

        /// <summary>
        /// 打开日常工作
        /// Created:20170409 (ChengMengjia)
        /// </summary>
        public void OpenRoutine()
        {
            if (nodeRoutine == null)
                nodeRoutine = new Forms.Others.Routine(CurrentNode.ID);
            else
                nodeRoutine.LoadContent(CurrentNode.ID);
            ShowChildForm(nodeRoutine);
        }

        /// <summary>
        /// 打开问题
        /// Created:20170409 (ChengMengjia)
        /// </summary>
        public void OpenTrouble()
        {
            if (nodeTrouble == null)
                nodeTrouble = new Forms.Others.Trouble(CurrentNode.ID);
            else
                nodeTrouble.LoadPageData(CurrentNode.ID);
            ShowChildForm(nodeTrouble);
        }



        /// <summary>
        /// 加载一个窗体到TabControl中
        /// </summary>
        /// <param name="form"></param>
        public void ShowChildForm(Office2007RibbonForm form)
        {
            //判断是否在，如果存在，直接激活为当前Tab         
            foreach (SuperTabItem it in MainSuperTabControl.Tabs)
            {
                if (it.Name == form.Name)
                {
                    MainSuperTabControl.SelectedTab = it;
                    return;
                }
            }
            OpenTab(form);
        }

        /// <summary>
        /// 加载一个窗体到TabControl中
        /// </summary>
        /// <param name="form"></param>
        public void ShowChildForm(Office2007RibbonForm form, string TagName)
        {
            //判断是否在，如果存在，直接激活为当前Tab         
            foreach (SuperTabItem it in MainSuperTabControl.Tabs)
            {
                if (it.Name == form.Name)
                {
                    MainSuperTabControl.SelectedTab = it;
                    return;
                }
            }
            //不存在，创建TabItem，并激活
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            //开始 liuxuexian
            //为了适应不同屏幕的分辨率，设置最小出现滚动条的值
            //form.AutoScroll = true;
            //form.AutoScrollMinSize = new Size(this.MainSuperTabControl.Width - 20, this.MainSuperTabControl.Height - 20);
            //结束
            form.TopLevel = false;
            form.Visible = true;
            SuperTabItem item = MainSuperTabControl.CreateTab(form.Text);
            item.Text = TagName;
            item.Name = form.Name;
            item.AttachedControl.Controls.Add(form);
            MainSuperTabControl.SelectedTab = item;
        }

        /// <summary>
        /// 打开对应页面
        /// Created：20170329(ChengMengjia)
        /// </summary>
        /// <param name="form"></param>
        void OpenTab(Office2007RibbonForm form)
        {
            //创建TabItem，并激活
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            //开始 liuxuexian
            //为了适应不同屏幕的分辨率，设置最小出现滚动条的值
            //form.AutoScroll = true;
            //form.AutoScrollMinSize = new Size(this.MainSuperTabControl.Width - 20, this.MainSuperTabControl.Height - 20);
            //结束
            form.TopLevel = false;
            form.Visible = true;
            SuperTabItem item = MainSuperTabControl.CreateTab(form.Text);
            item.Text = form.Text;
            item.Name = form.Name;

            item.AttachedControl.Controls.Add(form);
            MainSuperTabControl.SelectedTab = item;
        }

        /// <summary>
        /// 关闭后再打开对应页面
        /// Created：20170329(ChengMengjia)
        /// </summary>
        /// <param name="listForm"></param>
        void ReOpenTabs(List<Office2007RibbonForm> listForm)
        {
            foreach (Office2007RibbonForm form in listForm)
            {
                CloseTab(form.Name);//先关闭

                //不存在，创建TabItem，并激活
                form.FormBorderStyle = FormBorderStyle.None;
                form.Dock = DockStyle.Fill;
                //开始 liuxuexian
                //为了适应不同屏幕的分辨率，设置最小出现滚动条的值
                //form.AutoScroll = true;
                //form.AutoScrollMinSize = new Size(this.MainSuperTabControl.Width - 20, this.MainSuperTabControl.Height - 20);
                //结束
                form.TopLevel = false;
                form.Visible = true;
                SuperTabItem item = MainSuperTabControl.CreateTab(form.Text);
                item.Text = form.Text;
                item.Name = form.Name;

                item.AttachedControl.Controls.Add(form);
                MainSuperTabControl.SelectedTab = item;
            }
        }

        void CloseTabs(String[] names)
        {
            foreach (String str in names)
            {
                CloseTab(str);
            }
        }
        /// <summary>
        /// 删除TabControl里名字为tabname的tab
        /// </summary>
        /// <param name="tabname"></param>
        public void CloseTab(string tabname)
        {
            if (MainSuperTabControl.Tabs.Contains(tabname))
                MainSuperTabControl.Tabs.Remove(tabname);
        }
        #endregion

        /// <summary>
        ///  刷新左侧节点树
        /// Created:20170524(ChengMengjia)
        /// </summary>
        public void RelaodTree()
        {
            DataHelper.SetMainTreeDate(WbsTree, ProjectId);//绑定树形数据
            if (CurrentNode != null)
                DataHelper.SetTreeSelectByValue(WbsTree, CurrentNode.ID);
        }

        /// <summary>
        /// 当前节点更新
        /// Created:20170525 (ChengMengjia)
        /// </summary>
        /// <param name="node"></param>
        public void ReloadCurrentNode(PNode node)
        {
            if (CurrentNode != null && CurrentNode.ID.Contains(node.ID.Substring(0, 36)))
            {
                //当前有选中节点并且和修改的节点相同
                CurrentNode = node;
                _SelectedNodeID = node.ID;

                if (nodePage == null)
                {
                    nodePage = new NormalOperation();
                    OpenTab(nodePage);
                }
                else
                    nodePage.init();
            }
        }
        #endregion

        /// <summary>
        /// 项目节点-修改名称
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolProRename_Click(object sender, EventArgs e)
        {
            Forms.Project.NewProject newProject = new Forms.Project.NewProject(false);
            if (newProject.ShowDialog() == DialogResult.OK)
            {
                newProject.Close();
                LoadProjects();//加载项目列表
            }
        }

        /// <summary>
        /// 项目-模板导入
        /// Created:20170531(ChengMengjia)
        /// Updated:20170601(ChengMengjia) 添加交付物信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProIn_Click(object sender, EventArgs e)
        {
            string Filter = "Excel文件|*.xls;*.xlsx";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Excel文件";
            openFileDialog1.Filter = Filter;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            ExcelHelper excel = new ExcelHelper(openFileDialog1.FileName, 1);


            string proName = excel.ReadCell(1, 1).Split('-')[0];//项目名称
            if (!proBLL.UniqueName(proName))//检查重名
            {
                MessageBox.Show("项目重名，请更改！");
                return;
            }

            Project project = new Project();
            project.ID = Guid.NewGuid().ToString();
            project.Name = proName;
            project.No = "";
            project.ProjectLastUpdate = DateTime.Now;
            List<PNode> listPNode = new List<PNode>();
            PNode node;
            List<DeliverablesJBXX> listJbxx = new List<DeliverablesJBXX>();
            #region 顶级节点
            PNode topNode = new PNode()
            {
                ID = Guid.NewGuid().ToString(),
                PID = project.ID,
                WBSNo = "",
                No = 1,
                Name = proName,
                PType = 0,
                Status = 1,
                CREATED = DateTime.Now
            };
            listPNode.Add(topNode);
            #endregion
            for (int row = 3; row < 1000; row++)
            {
                try
                {
                    string no = excel.ReadCell(row, 1).Trim();//wbs编号
                    if (string.IsNullOrEmpty(no))
                        break;
                    string[] noArray = no.Split('.');
                    node = new PNode();
                    if (noArray.Length == 1)
                    {
                        #region 第一级节点
                        node.ID = Guid.NewGuid().ToString() + "-1";
                        node.PID = project.ID;
                        node.ParentID = topNode.ID.Substring(0, 36);
                        node.WBSNo = no;
                        node.No = int.Parse(no);
                        node.Name = excel.ReadCell(row, 2).Trim();//wbs名称
                        node.PType = 0;
                        node.Status = 1;
                        node.CREATED = DateTime.Now;
                        #endregion
                        listPNode.Add(node);
                        continue;
                    }
                    else
                    {
                        #region 非一级节点
                        //先找到父级节点 例如：1.2.3 就找编号为1.2的作为父级节点
                        PNode parent = listPNode.Where(t => t.WBSNo == no.Substring(0, no.Length - 1 - noArray[noArray.Length - 1].Length)).FirstOrDefault();
                        node = new PNode()
                        {
                            ID = Guid.NewGuid().ToString() + "-1",
                            PID = project.ID,
                            ParentID = parent.ID.Substring(0, 36),
                            WBSNo = no,
                            No = int.Parse(noArray[noArray.Length - 1]),
                            Name = excel.ReadCell(row, 2).Trim(),//wbs名称
                            PType = 0,
                            Status = 1,
                            CREATED = DateTime.Now
                        };
                        #endregion
                    }
                    if (excel.ReadCell(row, 3).Trim() == "是")
                    {
                        node.PType = 1;
                        #region 是交付物
                        DateTime dt1 = DateTime.Now;
                        DateTime dt2 = DateTime.Now;
                        int workload = 0;
                        int weight = 1;
                        DateTime.TryParse(excel.ReadCell(row, 4), out dt1);
                        DateTime.TryParse(excel.ReadCell(row, 5), out dt2);
                        int.TryParse(excel.ReadCell(row, 6), out workload);
                        int.TryParse(excel.ReadCell(row, 7), out weight);
                        listJbxx.Add(new DeliverablesJBXX()
                        {
                            ID = Guid.NewGuid().ToString() + "-1",
                            NodeID = node.ID.Substring(0, 36),
                            Name = node.Name,
                            StarteDate = dt1,
                            EndDate = dt2,
                            Workload = workload,
                            Weight = weight,
                            Status = 1,
                            CREATED = DateTime.Now
                        });
                        #endregion
                    }
                    listPNode.Add(node);
                }
                catch
                {
                    MessageBox.Show("第" + row + "行数据无法解析！");
                }
            }
            JsonResult result = proBLL.SaveProject(project, listPNode, listJbxx);
            if (result.result)
            {
                MessageBox.Show("导入成功！");
                LoadProjects();//加载项目列表
            }
            else
                MessageBox.Show(result.msg);
        }

        /// <summary>
        /// 项目-模板导出
        /// Created:20170531(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProOut_Click(object sender, EventArgs e)
        {
            try
            {
                string addr_model = FileHelper.GetUploadPath(UploadType.WeeklyModel, "", "") + ConstHelper.Config_WBSModel;//模板地址
                string addr_save = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + ProjectName + "-工作分解结构.xlsx";//用户选择存放地址
                //打开文件夹下载
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.CheckPathExists = true;//是否检查文件是否存在
                    dialog.Title = "Excel文件";
                    dialog.Filter = "Excel文件|*.xlsx;*.xls";
                    dialog.FileName = addr_save;
                    dialog.DefaultExt = "xlsx";
                    if (dialog.ShowDialog() == DialogResult.OK && dialog.FileName.Length > 0)
                    {
                        addr_save = dialog.FileName.ToString();
                        //用户自行更改扩展名
                        if (addr_save.Substring(addr_save.LastIndexOf(".") + 1).ToString() != "xlsx" && addr_save.Substring(addr_save.LastIndexOf(".") + 1).ToString() != "xls")
                        {
                            if (MessageBox.Show("扩展名不可更改，将恢复默认！", "警告") == DialogResult.OK)
                                addr_save = addr_save.Split('.')[0] + ".xlsx";
                            else
                                return;
                        }
                        ExcelHelper excel = new ExcelHelper(addr_model, addr_save);
                        #region 标题信息
                        excel.SetCells(1, 1, ProjectName + "-工作分解结构");
                        #endregion
                        #region 内容
                        int rowIndex = 3;
                        List<PNode> listNode = new WBSBLL().GetNodes(ProjectId);//所有普通和交付物节点
                        listNode = listNode.Where(t => t.PType == 0 || t.PType == 1).ToList();//普通节点和交付物节点
                        IEnumerable<PNode> parentNode = null;
                        PNode proNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No).FirstOrDefault();//顶级项目名节点
                        parentNode = listNode.Where(t => t.ParentID == proNode.ID).OrderBy(t => t.No);//第一级节点列表
                        foreach (PNode parent in parentNode)
                        {
                            excel.SetCells(rowIndex, 1, parent.WBSNo);
                            excel.SetCells(rowIndex, 2, parent.Name);
                            excel.SetCells(rowIndex, 3, "否");
                            //excel.SetCellsBackColor(rowIndex, 1, rowIndex, 4, ColorIndex.灰色25);
                            rowIndex += 1;
                            SetSubTreeData(listNode, parent, excel, ref rowIndex);
                        }
                        #endregion
                        excel.SaveAsFile();//文件保存
                        excel.Dispose();
                        MessageBox.Show("保存成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 项目-模板导出
        /// Created:20170531(ChengMengjia)
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        private void SetSubTreeData(IList<PNode> listNode, PNode parent, ExcelHelper excel, ref int rowIndex)
        {
            string parentID = parent.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
            if (children.Count<PNode>() < 1)
            {
                return;
            }
            foreach (PNode child in children)
            {
                excel.SetCells(rowIndex, 1, child.WBSNo);
                excel.SetCells(rowIndex, 2, child.Name);
                if (child.PType == 1)//是交付物
                {
                    excel.SetCells(rowIndex, 3, "是");
                    DeliverablesJBXX jbxx = wbsBll.GetJBXX(child.ID);
                    excel.SetCells(rowIndex, 4, jbxx.StarteDate == null ? "" : ((DateTime)jbxx.StarteDate).ToString("yyyy年MM月dd日"));
                    excel.SetCells(rowIndex, 5, jbxx.EndDate == null ? "" : ((DateTime)jbxx.EndDate).ToString("yyyy年MM月dd日"));
                    excel.SetCells(rowIndex, 6, jbxx.Workload == null ? "" : ((int)jbxx.Workload).ToString());
                    excel.SetCells(rowIndex, 7, jbxx.Weight == null ? "" : ((int)jbxx.Weight).ToString());
                }
                else
                    excel.SetCells(rowIndex, 3, "否");
                rowIndex += 1;
                SetSubTreeData(listNode, child, excel, ref rowIndex);
            }
        }

        /// <summary>
        /// 工作执行效率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem7_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_PersonnelExecutiveEfficiency());
        }

        /// <summary>
        /// 工作难度系数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DifficutyDegree_Click(object sender, EventArgs e)
        {
            ShowChildForm(new Forms.Report.Report_DifficutyDegreeNew());
        }
    }
}
