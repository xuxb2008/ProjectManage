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

        //日常工作ID
        public string TroubleId { get; set; }

        #endregion

        #region 变量

        //文件ID
        string _fileId;
        //文件是否重新选择标识
        bool _fileSelectFlg = false;
        //当前选择的文件名称
        string _filePath;
        //当前结点ID
        string _nodeID;
        //问题原因
        string _fileTroubleReson;
        //问题分析
        string _fileTroubleAnalyse;
        //解决方案
        string _fileTroubleSolution;

        #endregion

        #region 业务类初期化

        //项目问题业务操作类初期化
        TroubleBLL troubleBLL = new TroubleBLL();
        //干系人业务操作类初期化
        StakeholdersBLL stakeholderBLL = new StakeholdersBLL();

        #endregion


        #region 事件

        public Trouble()
        {
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

            //加载责任人列表
            //LoadOperatorList();

            //加载画面内容
            LoadPageData();
        }

        /// <summary>
        /// 项目问题清空按钮按下时
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
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

            List<RoutineWork> listWork = new List<RoutineWork>();
            if (!GetEditManager(ref listWork)) return;//如果填写无误

            DomainDLL.Trouble obj = new DomainDLL.Trouble();
            //项目问题ID
            obj.ID = TroubleId;
            //节点
            if (cmbNode.SelectedIndex < 0 || string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
                obj.NodeID = DataHelper.GetNodeIdByProjectId(ProjectId);
            else
                obj.NodeID = cmbNode.SelectedNode.Name.Substring(0, 36);
            //结点改变时，移动文件到新的节点
            if (obj.NodeID != _nodeID)
            {
                //List<TroubleFiles> list = troubleBLL.GetTroubleFiles(TroubleId);
                //foreach (TroubleFiles file in list)
                //{
                //    //取得当前的文件路径
                //    string filePath = FileHelper.GetFilePath(UploadType.Trouble, ProjectId, _nodeID, file.Path);
                //    //拷贝文件到新的结点
                //    if (!FileHelper.CopyFile(filePath, UploadType.Trouble, ProjectId, obj.NodeID, file.Path)) return;
                //}
            }

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

            #region
            //责任人
            //if (cmbOperate.SelectedIndex > -1)
            //    obj.HandleMan = ((ComboItem)cmbOperate.SelectedItem).Value.ToString();
            //处理日期
            //if (!string.IsNullOrEmpty(txtHandleDate.Text))
            //    obj.HandleDate = DateTime.Parse(txtHandleDate.Text);
            #endregion

            //问题级别
            if (cmbTroubleLevel.SelectedIndex > -1)
                obj.Level = int.Parse(((ComboItem)cmbTroubleLevel.SelectedItem).Value.ToString());
            //处理情况
            if (cmbHandleStatus.SelectedIndex > -1)
                obj.HandleStatus = int.Parse(((ComboItem)cmbHandleStatus.SelectedItem).Value.ToString());
            

            //保存
            JsonResult result = troubleBLL.SaveTrouble(obj);
            TroubleId = result.result ? (string)result.data : TroubleId;

            if (result.result)
            {
                _nodeID = obj.NodeID;
                //一览重新加载
                Search();
            }

            MessageHelper.ShowRstMsg(result.result);

            //重新加载首页的成果列表
            startPage.LoadProjectTrouble();
            startPage.LoadProjectTroubleList();
        }

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
                DialogResult drt = MessageHelper.ShowMsg(MessageID.I000000009, MessageType.Confirm, "项目问题");

                if (drt == DialogResult.OK)
                {
                    //加载画面内容
                    LoadPageData();
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
        /// 相关文件-上传
        /// Created：20170425(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_Upload(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(9, 1));
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    TroubleFiles entity = new TroubleFiles();
                    entity.TroubleID = TroubleId.Substring(0, 36);
                    switch (Type)
                    {
                        case 1:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.TroubleReason, ProjectId, null);
                            entity.Name = "问题原因";
                            _fileTroubleReson = entity.Path;
                            break;
                        case 2:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.TroubleAnalyse, ProjectId, null);
                            entity.Name = "问题分析";
                            _fileTroubleAnalyse = entity.Path;
                            break;
                        case 3:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.TroubleSolution, ProjectId, null);
                            entity.Name = "解决方案";
                            _fileTroubleSolution = entity.Path;
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.Path))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {
                        entity.Type = Type;
                        JsonResult result = troubleBLL.SaveTroubleFile(entity);
                        MessageHelper.ShowRstMsg(result.result);
                        //LoadFile();
                    }
                }
            }


        }

        /// <summary>
        /// 相关文件-打开
        /// Created：20170425(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFOpen_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;

            switch (link.Name)
            {
                case "lblFile1":
                    //FileHelper.OpenFile(UploadType.ContractHTSMJ, ProjectId, null, _fileContractHTSMJName);
                    break;
                case "lblFile2":
                    //FileHelper.OpenFile(UploadType.ContractHTDZD, ProjectId, null, _fileContractHTDZDName);
                    break;
                case "lblFile3":
                    //FileHelper.OpenFile(UploadType.ContractGZSMJ, ProjectId, null, _fileContractGZSMJName);
                    break;
            }

        }

        /// <summary>
        /// 相关文件-下载
        /// Created：20170425(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_DownLoad(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(7, 1));
            //List<ContractFiles> list = bll.GetFiles(ProjectId, Type);
            //if (list.Count <= 0)
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
            //}

            //取得上传文件类型
           // string fileName = list[0].Path;
            UploadType uploadType = UploadType.ContractHTSMJ;
            switch (Type)
            {
                case 1:
                    uploadType = UploadType.ContractHTSMJ;
                    break;
                case 2:
                    uploadType = UploadType.ContractHTDZD;
                    break;
                case 3:
                    uploadType = UploadType.ContractGZSMJ;
                    break;
            }

            //FileHelper.DownLoadFile(uploadType, ProjectId, null, fileName);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载画面数据
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        private void LoadPageData()
        {
            if (!string.IsNullOrEmpty(TroubleId))
            {
                //日常工作取得
                DomainDLL.Trouble obj = troubleBLL.GetTroubleObject(TroubleId);

                if (obj != null)
                {
                    //节点
                    DataHelper.SetComboxTreeSelectByValue(cmbNode, obj.NodeID);
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
                    //责任人
                    //DataHelper.SetComboBoxSelectItemByValue(cmbOperate, obj.HandleMan);
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

                    txtFilePath.Text = string.Empty;
                    txtFileName.Text = string.Empty;
                    txtFileDesc.Text = string.Empty;
                }
            }
            else
            {
                //添加日期
                txtCreated.Text = DateTime.Now.ToShortDateString();
            }
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
        /// Updated:2017.04.25(Xuxb) 追加文件类型
        /// </summary>
        /// <param name="routineId"></param>
        private void LoadFileList(string troubleId)
        {
            List<TroubleFiles> list = new List<TroubleFiles>();

            //加载项目合同扫描件
            list = troubleBLL.GetTroubleFiles(troubleId, 1);
            if (list.Count > 0)
            {
                _fileTroubleReson = list[0].Path;
                //lblFile1.Show();
                //btnDown1.Show();
            }
            else
            {
                //lblFile1.Hide();
                //btnDown1.Hide();
            }

            //加载项目合同电子档
            list = troubleBLL.GetTroubleFiles(troubleId, 2);
            if (list.Count > 0)
            {
                //_fileTroubleAnalysis = list[0].Path;
                //lblFile2.Show();
                //btnDown2.Show();
            }
            else
            {
                //lblFile2.Hide();
                //btnDown2.Hide();
            }

            //加载项目工作说明书扫描件
            list = troubleBLL.GetTroubleFiles(troubleId, 3);
            if (list.Count > 0)
            {
                //_fileTroubleResolve = list[0].Path;
                //lblFile3.Show();
                //btnDown3.Show();
            }
            else
            {
                //lblFile3.Hide();
                //btnDown3.Hide();
            }

            //项目问题文件取得
            list = troubleBLL.GetTroubleFiles(troubleId, 0);

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
        /// </summary>
        private void Search()
        {
            //取得数据
            DataTable dt = troubleBLL.GetTroubleList(ProjectId, this.txtSearchStart.Text, this.txtSearchEnd.Text,txtSearchKey.Text);

            //追加行号
            DataHelper.AddNoCloumn(dt);

            //绑定
            gridTrouble.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 加载负责人下拉框列表
        /// Created：2017.04.06(Xuxb)
        /// </summary>
        //private void LoadOperatorList()
        //{
        //    List<Stakeholders> list = stakeholderBLL.GetList(ProjectId, null);

        //    foreach (Stakeholders obj in list)
        //    {
        //        ComboItem item = new ComboItem();
        //        item.Text = obj.Name;
        //        item.Value = obj.ID.Substring(0,36).ToString();
        //        this.cmbOperate.Items.Add(item);
        //    }
        //}

        /// <summary>
        /// 添加责任人
        /// 201//05/31(zhuguanjun)
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
                gridManager.PrimaryGrid.Rows.Add(new DevComponents.DotNetBar.SuperGrid.GridRow(
                    fmNewManager.ReturnValue.Manager, fmNewManager.ReturnValue.ManagerName, fmNewManager.ReturnValue.Workload,
                    fmNewManager.ReturnValue.StarteDate.ToShortDateString(), fmNewManager.ReturnValue.EndDate.ToShortDateString(),
                    "删除"));
            }
        }

        /// <summary>
        /// 获取编辑的责任人列表
        /// Created:20170531(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        bool GetEditManager(ref List<RoutineWork> listWork)
        {
            //责任人
            decimal totalWork = 0;//总的工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                listWork.Add(new RoutineWork() { Manager = cells[0], Workload = Decimal.Parse(cells[2]) });
                totalWork += Decimal.Parse(cells[2]);
                if (totalWork > intWorkload.Value)
                {
                    MessageBox.Show("超过设置的总工作量，请检查！");
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
