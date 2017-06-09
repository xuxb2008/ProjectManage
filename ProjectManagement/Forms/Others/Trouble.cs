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
using CommonDLL;
using DomainDLL;
using DevComponents.Editors;

namespace ProjectManagement.Forms.Others
{
    /// <summary>
    /// 画面名：问题管理
    /// Created：2017.03.30(Xuxb)
    /// </summary>
    public partial class Trouble : BaseForm
    {
        #region 画面属性

        //问题ID
        public string TroubleId { get; set; }
        //问题对应的NodeID
        private string _nodeID { get; set; }
        #endregion

        #region 变量

        //文件ID
        string _fileId;
        //文件是否重新选择标识
        bool _fileSelectFlg = false;
        //当前选择的文件名称
        string _filePath;

        #endregion

        #region 业务类初期化

        //项目问题业务操作类初期化
        TroubleBLL troubleBLL = new TroubleBLL();
        //干系人业务操作类初期化
        StakeholdersBLL stakeholderBLL = new StakeholdersBLL();

        #endregion

        #region 事件

        public Trouble(string nodeID)
        {
            _nodeID = nodeID;
            InitializeComponent();
        }

        /// <summary>
        /// 画面加载时
        /// Created：2017.03.30(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Trouble_Load(object sender, EventArgs e)
        {
            //加载结点下拉列表
            DataHelper.SetComboxTreeData(this.cmbNode, ProjectId);

            //加载问题级别下拉列表
            DataHelper.LoadDictItems(cmbTroubleLevel, DictCategory.TroubleLevel);

            //加载处理情况下拉列表
            DataHelper.LoadDictItems(cmbHandleStatus, DictCategory.TroubleHandleStatus);

            //加载画面内容
            LoadPageData();
            //加载问题列表
            Search();
        }

        #region 问题内容

        /// <summary>
        /// 查询按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        /// <summary>
        /// 问题列表加载时选择颜色
        ///  Created：20160607(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTrouble_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listRow = gridTrouble.PrimaryGrid.Rows.ToList();
            int type = 0;
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement obj in listRow)
            {
                DevComponents.DotNetBar.SuperGrid.GridRow row = (DevComponents.DotNetBar.SuperGrid.GridRow)obj;
                int.TryParse(row.GetCell("FinishType").Value.ToString(), out type);
                row.CellStyles = DataHelper.MatchRowColor(type);
                type = 0;
            }
        }
        /// <summary>
        /// 问题列表行点击时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridTrouble_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            TroubleId = e.GridCell.GridRow.Cells["ID"].Value.ToString();
            if (!string.IsNullOrEmpty(TroubleId))
            {
                //加载画面内容
                LoadPageData();
                //加载责任人列表
                var list = troubleBLL.GetTroubleWorkList(TroubleId);
                gridManager.PrimaryGrid.DataSource = list;
            }
        }

        /// <summary>
        /// 结点清空
        /// Created：2017.04.07(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX8_Click(object sender, EventArgs e)
        {
            this.cmbNode.SelectedIndex = -1;
        }
        /// <summary>
        /// 开始或结束时间值变化
        /// Created:20170607(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dt_ValueChanged(object sender, EventArgs e)
        {
            if (txtStartDate.Value != null && txtEndDate.Value != null)
            {
                if (txtEndDate.Value < txtStartDate.Value)
                    txtEndDate.Value = txtStartDate.Value;
                intWorkload.Value = DateHelper.ComputeWorkDays(txtStartDate.Value, txtEndDate.Value);
            }
        }
        /// <summary>
        /// 项目问题清空按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearTrouble();
        }


        /// <summary>
        /// 项目问题保存按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// Updated：20170414（Xuxb）保存后刷新首页问题图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //保存前检查
            if (!TroubleCheck()) return;

            List<TroubleWork> listWork = new List<TroubleWork>();
            if (!GetEditManager(ref listWork, true)) return;//责任人如果填写无误

            DomainDLL.Trouble obj = new DomainDLL.Trouble();

            #region 赋值
            //项目问题ID
            obj.ID = TroubleId;
            //NodeID
            //节点(如果关联结点未选择时，默认设定为项目结点)
            if (cmbNode.SelectedIndex < 0 || string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
                obj.NodeID = DataHelper.GetNodeIdByProjectId(ProjectId);
            else
                obj.NodeID = cmbNode.SelectedNode.Name.Substring(0, 36);
            //问题名称
            obj.Name = txtTroubleName.Text;
            //问题描述
            obj.Desc = txtTroubleDesc.Text;
            //处理结果
            obj.HandleResult = txtTroubleResult.Text;
            //开始日期
            if (!string.IsNullOrEmpty(txtStartDate.Text))
                obj.StarteDate = DateTime.Parse(txtStartDate.Text);
            //结束日期
            if (!string.IsNullOrEmpty(txtEndDate.Text))
                obj.EndDate = DateTime.Parse(txtEndDate.Text);
            //工作量
            obj.Workload = intWorkload.Value;
            //状态
            obj.Status = 1;

            //问题级别
            if (cmbTroubleLevel.SelectedIndex > -1)
                obj.Level = int.Parse(((ComboItem)cmbTroubleLevel.SelectedItem).Value.ToString());
            //处理情况
            if (cmbHandleStatus.SelectedIndex > -1)
                obj.HandleStatus = int.Parse(((ComboItem)cmbHandleStatus.SelectedItem).Value.ToString());
            #endregion

            bool IsEdit = false;
            string oldPath = "";//旧的文件存放路径
            // 判断是否为修改状态 节点是否改变
            if (!string.IsNullOrEmpty(_nodeID))
            {
                IsEdit = true;
                oldPath = FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Trouble, ProjectId, _nodeID);
            }

            //保存
            JsonResult result = troubleBLL.SaveTrouble(ProjectId, obj, listWork);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                //一览重新加载
                Search();
                //清空当前编辑
                ClearTrouble();
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectTrouble();
                startPage.LoadProjectTroubleList();
                #region  结点改变时，移动文件到新的节点
                if (IsEdit)
                    FileHelper.MoveFloder(oldPath, FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Trouble, ProjectId, _nodeID));
                #endregion
            }


        }

        #endregion

        #region 责任人

        /// <summary>
        /// 责任人-单元格点击事件
        ///  Created：20170531(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDel")
                gridManager.PrimaryGrid.Rows.Remove(e.GridCell.GridRow);//删除行
        }

        /// <summary>
        /// 责任人双击-修改
        ///  Created：20170531(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            string tmp = e.GridRow.ToString();
            tmp = tmp.Substring(tmp.LastIndexOf("{") + 1);
            tmp = tmp.Substring(0, tmp.LastIndexOf("}"));
            string[] cells = tmp.Trim().Split(',');
            int work = 0, acture = 0;
            int.TryParse(cells[2], out work);
            int.TryParse(cells[3], out acture);
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager(cells[0], work, acture);
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 0).Value = fmNewManager.ReturnValue.Manager;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 1).Value = fmNewManager.ReturnValue.ManagerName;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 2).Value = fmNewManager.ReturnValue.Workload;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 3).Value = fmNewManager.ReturnValue.ActualWorkload;
            }
        }

        #endregion

        #region 文件

        /// <summary>
        /// 项目问题文件保存按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSave_Click(object sender, EventArgs e)
        {
            //保存前检查
            if (!FileCheck()) return;

            TroubleFiles file = new TroubleFiles();

            //文件ID
            file.ID = _fileId;
            //项目问题ID
            file.TroubleID = TroubleId.Substring(0, 36);
            //文件路径
            file.Name = txtFileName.Text;
            //文件描述
            file.Desc = txtFileDesc.Text;
            //文件类型
            file.Type = 0;
            //上传文件名
            if (_fileSelectFlg)
            {
                file.Path = FileHelper.UploadFile(txtFilePath.Text, UploadType.Trouble, ProjectId, _nodeID);
            }
            else
            {
                file.Path = _filePath;
            }

            //如果返回文件名为空，不保存数据库
            if (string.IsNullOrEmpty(file.Path)) return;

            //保存
            JsonResult result = troubleBLL.SaveTroubleFile(file);
            _fileId = result.result ? (string)result.data : _fileId;
            if (result.result)
            {
                //附件列表加载
                LoadFileList(TroubleId);
            }
            MessageHelper.ShowRstMsg(result.result);
        }

        /// <summary>
        /// 项目问题文件清空按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileClear_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = string.Empty;
            txtFileName.Text = string.Empty;
            txtFileDesc.Text = string.Empty;

            //文件ID
            _fileId = string.Empty;
        }

        /// <summary>
        /// 选择文件按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = dialog.FileName;
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtFileName.Text = temp[0];
                    _fileSelectFlg = true;
                }
            }
        }


        /// <summary>
        /// 文件列表行点击时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDownLoad")
            {
                string fileName = e.GridCell.GridRow.Cells["Path"].Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.Trouble, ProjectId, _nodeID, fileName);
            }
            else
            {
                _fileId = e.GridCell.GridRow.Cells["ID"].Value.ToString();
                _filePath = e.GridCell.GridRow.Cells["Path"].Value.ToString();
                txtFileName.Text = e.GridCell.GridRow.Cells["Name"].Value.ToString();
                txtFileDesc.Text = e.GridCell.GridRow.Cells["Desc"].Value.ToString();
            }
        }

        #endregion

        #region 其他文件——原因、分析、解决方案


        /// <summary>
        /// 原因、分析、解决方案的下载
        /// Created:20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(button.Name.Length - 1));
            List<TroubleFiles> list = troubleBLL.GetTroubleFiles(TroubleId, Type);
            if (list.Count <= 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }
            FileHelper.DownLoadFile(UploadType.Trouble, ProjectId, _nodeID, list[0].Path);
        }

        /// <summary>
        /// 原因、分析、解决方案的打开
        /// Created:20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void lbl_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;
            FileHelper.OpenFile(UploadType.Trouble, ProjectId, _nodeID, link.Tag.ToString());
        }

        /// <summary>
        /// 原因、分析、解决方案的上传
        /// Created:20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(button.Name.Length - 1));
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DomainDLL.TroubleFiles entity = new DomainDLL.TroubleFiles();
                    entity.ID = button.Tag == null ? "" : button.Tag.ToString();
                    entity.TroubleID = TroubleId.Substring(0, 36);
                    entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.Trouble, ProjectId, _nodeID);
                    switch (Type)
                    {
                        case 1:
                            entity.Name = "原因";
                            break;
                        case 2:
                            entity.Name = "分析";
                            break;
                        case 3:
                            entity.Name = "解决方案";
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.Path))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {
                        entity.Type = Type;
                        JsonResult result = troubleBLL.SaveTroubleFile(entity);
                        MessageHelper.ShowRstMsg(result.result);
                        LoadSpecFiles(TroubleId);
                    }
                }
            }
        }
        #endregion


        #endregion

        #region 方法

        /// <summary>
        /// 加载画面数据
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        private void LoadPageData()
        {
            //日常工作取得
            DomainDLL.Trouble obj = troubleBLL.GetTroubleObject(TroubleId, _nodeID);
            if (!string.IsNullOrEmpty(obj.ID))
            {
                PNode parentNode = new WBSBLL().GetParentNode(obj.NodeID); //日常工作挂靠的节点
                //节点
                DataHelper.SetComboxTreeSelectByValue(cmbNode, parentNode.ID);
                TroubleId = obj.ID;
                _nodeID = obj.NodeID;
                //问题名称
                txtTroubleName.Text = obj.Name;
                //问题描述
                txtTroubleDesc.Text = obj.Desc;
                //处理结果
                txtTroubleResult.Text = obj.HandleResult;
                //开始日期
                if (obj.StarteDate.HasValue)
                    txtStartDate.Text = obj.StarteDate.Value.ToShortDateString();
                //结束日期
                if (obj.EndDate.HasValue)
                    txtEndDate.Text = obj.EndDate.Value.ToShortDateString();
                //工作量 
                intWorkload.Value = (int)obj.Workload;
                //加载责任人列表
                var list = troubleBLL.GetTroubleWorkList(obj.ID);
                gridManager.PrimaryGrid.DataSource = list;
                //问题级别
                DataHelper.SetComboBoxSelectItemByValue(cmbTroubleLevel, obj.Level.ToString());
                //处理情况
                DataHelper.SetComboBoxSelectItemByValue(cmbHandleStatus, obj.HandleStatus.ToString());
                //处理日期
                //if (obj.HandleDate.HasValue)
                //    txtHandleDate.Text = obj.HandleDate.Value.ToShortDateString();
                //添加日期
                txtCreated.Text = obj.CREATED.ToShortDateString();

                //附件列表加载
                LoadFileList(obj.ID.Substring(0, 36));
                LoadSpecFiles(obj.ID.Substring(0, 36));

                txtFilePath.Text = string.Empty;
                txtFileName.Text = string.Empty;
                txtFileDesc.Text = string.Empty;
            }
        }

        /// <summary>
        /// 加载画面数据
        /// Created：20170607(ChengMengjia)
        /// </summary>
        public void LoadPageData(string NodeID)
        {
            _nodeID = NodeID;
            //日常工作取得
            DomainDLL.Trouble obj = troubleBLL.GetTroubleObject("", _nodeID);
            TroubleId = obj.ID;
            PNode parentNode = new WBSBLL().GetParentNode(obj.NodeID); //日常工作挂靠的节点
            //节点
            DataHelper.SetComboxTreeSelectByValue(cmbNode, parentNode.ID);
            //问题名称
            txtTroubleName.Text = obj.Name;
            //问题描述
            txtTroubleDesc.Text = obj.Desc;
            //处理结果
            txtTroubleResult.Text = obj.HandleResult;
            //开始日期
            if (obj.StarteDate.HasValue)
                txtStartDate.Text = obj.StarteDate.Value.ToShortDateString();
            //结束日期
            if (obj.EndDate.HasValue)
                txtEndDate.Text = obj.EndDate.Value.ToShortDateString();
            //问题级别
            DataHelper.SetComboBoxSelectItemByValue(cmbTroubleLevel, obj.Level.ToString());
            //处理情况
            DataHelper.SetComboBoxSelectItemByValue(cmbHandleStatus, obj.HandleStatus.ToString());
            //处理日期
            //if (obj.HandleDate.HasValue)
            //    txtHandleDate.Text = obj.HandleDate.Value.ToShortDateString();
            //添加日期
            txtCreated.Text = obj.CREATED.ToShortDateString();
            //工作量 
            intWorkload.Value = (int)obj.Workload;
            //加载责任人列表
            var list = troubleBLL.GetTroubleWorkList(obj.ID);
            gridManager.PrimaryGrid.DataSource = list;
            //附件列表加载
            LoadFileList(obj.ID.Substring(0, 36));

            txtFilePath.Text = string.Empty;
            txtFileName.Text = string.Empty;
            txtFileDesc.Text = string.Empty;
        }

        /// <summary>
        /// 清空问题添加内容
        /// Created：20170607(ChengMengjia)
        /// </summary>
        private void ClearTrouble()
        {
            //节点
            cmbNode.SelectedIndex = -1;
            //问题名称
            txtTroubleName.Text = string.Empty;
            //问题描述
            txtTroubleDesc.Text = string.Empty;
            //处理结果
            txtTroubleResult.Text = string.Empty;
            //开始日期
            txtStartDate.Text = string.Empty;
            //结束日期
            txtEndDate.Text = string.Empty;
            //责任人
            //cmbOperate.SelectedIndex = -1 ;
            //问题级别
            cmbTroubleLevel.SelectedIndex = -1;
            //处理情况
            cmbHandleStatus.SelectedIndex = -1;
            //处理日期
            //txtHandleDate.Text = string.Empty;
            //添加日期
            txtCreated.Text = DateTime.Now.ToShortDateString();

            //附件列表加载
            gridFile.PrimaryGrid.DataSource = new List<TroubleFiles>();

            txtFilePath.Text = string.Empty;
            txtFileName.Text = string.Empty;
            txtFileDesc.Text = string.Empty;

            //日常工作ID
            TroubleId = string.Empty;
            //文件ID
            _fileId = string.Empty;

            //清空责任人
            gridManager.PrimaryGrid.DataSource = null;
        }

        /// <summary>
        /// 项目问题保存时检查
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <returns></returns>
        private bool TroubleCheck()
        {
            //项目ID是否存在
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return false;
            }
            //结点是否选择
            //if (string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "结点");
            //    return false;
            //}
            //问题名称是否输入
            if (string.IsNullOrEmpty(txtTroubleName.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "问题名称");
                return false;
            }
            //处理情况是否选择
            if (cmbHandleStatus.SelectedItem != null && string.IsNullOrEmpty(((ComboItem)cmbHandleStatus.SelectedItem).Value.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "处理情况");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 文件保存时检查
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <returns></returns>
        private bool FileCheck()
        {
            //项目ID是否存在
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return false;
            }
            //项目问题是否创建
            if (string.IsNullOrEmpty(TroubleId))
            {
                MessageHelper.ShowMsg(MessageID.W000000006, MessageType.Alert, "问题内容");
                return false;
            }
            //文件未选择
            if (string.IsNullOrEmpty(_fileId) && string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "文件");
                return false;
            }
            //文件名称未输入
            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "文件名称");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 加载文件列表
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="routineId"></param>
        private void LoadFileList(string troubleId)
        {
            //项目问题文件取得
            List<TroubleFiles> list = troubleBLL.GetTroubleFiles(troubleId, 0);

            //附件列表加载
            int? i = 1;
            foreach (TroubleFiles file in list)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 查询按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// Updated：20170607(ChengMengjia) 行号在查询时加入
        /// </summary>
        private void Search()
        {
            //取得数据
            DataTable dt = troubleBLL.GetTroubleList(ProjectId, this.txtSearchStart.Text, this.txtSearchEnd.Text, txtSearchKey.Text);
            gridTrouble.PrimaryGrid.DataSource = dt;
        }


        /// <summary>
        /// 添加责任人
        /// Created：20170531(zhuguanjun)
        /// Updated：20170607(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddManager_Click(object sender, EventArgs e)
        {
            #region 计算剩余工作量
            int tmp = (int)intWorkload.Value;//工作量
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
                List<TroubleWork> listWork = new List<TroubleWork>();
                GetEditManager(ref listWork, false);
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
                    listWork.Add(new TroubleWork()
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
        /// 获取编辑的责任人列表
        /// Created:20170531(zhuguanjun)
        /// Updated：20170607(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        bool GetEditManager(ref List<TroubleWork> listWork, bool NeedCheck)
        {
            //责任人
            int totalWork = 0;//总的工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                listWork.Add(new TroubleWork() { Manager = cells[0], ManagerName = cells[1], Workload = int.Parse(cells[2]), ActualWorkload = int.Parse(cells[3]) });
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

        /// <summary>
        /// 原因、分析、解决方案的加载
        /// Created:20170609(ChengMengjia)
        /// </summary>
        private void LoadSpecFiles(string troubleID)
        {
            List<TroubleFiles> list = troubleBLL.GetTroubleFiles(TroubleId, null);
            //原因
            TroubleFiles entity = list.Where(t => t.Type == 1).FirstOrDefault();
            bool IsExist = entity != null;
            if (IsExist)
            {
                lbl1.Text = entity.Name;
                lbl1.Tag = entity.Path;
                btnUp1.Tag = entity.ID;
            }
            else
                btnUp1.Tag = null;
            lbl1.Visible = IsExist;
            btnDown1.Visible = IsExist;

            //分析
            entity = null;
            entity = list.Where(t => t.Type == 2).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl2.Text = entity.Name;
                lbl2.Tag = entity.Path;
                btnUp2.Tag = entity.ID;
            }
            else
                btnUp2.Tag = null;
            lbl2.Visible = IsExist;
            btnDown2.Visible = IsExist;

            //解决方案
            entity = null;
            entity = list.Where(t => t.Type == 3).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl3.Text = entity.Name;
                lbl3.Tag = entity.Path;
                btnUp3.Tag = entity.ID;
            }
            else
                btnUp3.Tag = null;
            lbl3.Visible = IsExist;
            btnDown3.Visible = IsExist;
        }

        #endregion

    }
}
