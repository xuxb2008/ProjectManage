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
using CommonDLL;
using BussinessDLL;
using DomainDLL;
using DevComponents.Editors;

namespace ProjectManagement.Forms.Others
{
    /// <summary>
    /// 画面名：日常工作
    /// Created：2017.03.30(Xuxb)
    /// </summary>
    public partial class Routine : BaseForm
    {

        #region 画面属性

        //日常工作ID
        public string WorkId { get; set; }

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

        #endregion

        #region 业务类初期化

        RoutineBLL routineBLL = new RoutineBLL();

        #endregion


        #region 事件


        /// <summary>
        /// 画面初期化
        /// </summary>
        public Routine()
        {
            InitializeComponent();          
        }

        /// <summary>
        /// 画面加载时
        /// Created：2017.03.30(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Routine_Load(object sender, EventArgs e)
        {
            //加载结点下拉列表
            DataHelper.SetComboxTreeData(this.cmbNode, ProjectId);

            //加载完成情况下拉列表
            DataHelper.LoadDictItems(cmbResultStatus, DictCategory.WorkHandleStatus);

            //加载画面内容
            LoadPageData();
            Search();
        }

        /// <summary>
        /// 日常工作清空按钮按下时
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkClear_Click(object sender, EventArgs e)
        {
            //节点
            cmbNode.SelectedIndex = -1;
            //工作名称
            txtWorkName.Text = string.Empty;
            //工作描述
            txtDesc.Text = string.Empty;
            //处理结果
            txtResult.Text = string.Empty;
            //开始日期
            txtStartDate.Text = string.Empty;
            //结束日期
            txtEndDate.Text = string.Empty;
            //完成情况
            cmbResultStatus.SelectedIndex = -1;
            //添加日期
            txtCreateDate.Text = DateTime.Now.ToShortDateString();
            //附件列表加载
            gridFile.PrimaryGrid.DataSource = new List<RoutineFiles>();
            //选择附件
            txtFilePath.Text = string.Empty;
            //附件名称
            txtFileName.Text = string.Empty;
            //附件描述
            txtFileDesc.Text = string.Empty;
            //日常工作ID
            WorkId = string.Empty;
            //文件ID
            _fileId = string.Empty;
            //清空责任人
            gridManager.PrimaryGrid.DataSource = null;
        }

        /// <summary>
        /// 文件清空
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileClear_Click(object sender, EventArgs e)
        {
            //选择附件
            txtFilePath.Text = string.Empty;
            //附件名称
            txtFileName.Text = string.Empty;
            //附件描述
            txtFileDesc.Text = string.Empty;
            //文件ID
            _fileId = string.Empty;
        }

        /// <summary>
        /// 日常工作保存时
        /// Created：2017.03.31(Xuxb)
        /// Updated：20170414（Xuxb）保存后刷新首页问题列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkSave_Click(object sender, EventArgs e)
        {
            //保存前检查
            if (!RoutineCheck()) return;

            List<RoutineWork> listWork = new List<RoutineWork>();
            if (!GetEditManager(ref listWork)) return;//如果填写无误

            DomainDLL.Routine obj = new DomainDLL.Routine();
            //日常工作ID
            obj.ID = WorkId;
            //节点(如果关联结点未选择时，默认设定为项目结点)
            if (cmbNode.SelectedIndex < 0 || string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
                obj.NodeID = DataHelper.GetNodeIdByProjectId(ProjectId);
            else
                obj.NodeID = cmbNode.SelectedNode.Name.Substring(0, 36);
            
            //结点改变时，移动文件到新的节点
            if (obj.NodeID != _nodeID)
            {
                List<RoutineFiles> list = routineBLL.GetRoutineFiles(WorkId);
                foreach (RoutineFiles file in list)
                {
                    //取得当前的文件路径
                    string filePath = FileHelper.GetFilePath(UploadType.Routine, ProjectId, _nodeID, file.Path);
                    //拷贝文件到新的结点
                    if (!FileHelper.CopyFile(filePath, UploadType.Routine, ProjectId, obj.NodeID, file.Path)) return; 
                }
            }
            //状态
            obj.Status = 1;
            //工作名称
            obj.Name = txtWorkName.Text;
            //工作描述
            obj.Desc = txtDesc.Text;
            //处理结果
            obj.DealResult = txtResult.Text;
            //工作量
            obj.Workload = intWorkload.Value;
            //开始日期
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            obj.StartDate = DateTime.Parse(txtStartDate.Text);
            //结束日期
            if (!string.IsNullOrEmpty(txtEndDate.Text))
                obj.EndDate = DateTime.Parse(txtEndDate.Text);
            //完成情况
            if (cmbResultStatus.SelectedIndex > -1)
                obj.FinishStatus = int.Parse(((ComboItem)cmbResultStatus.SelectedItem).Value.ToString());

            //保存
            //JsonResult result = routineBLL.SaveRoutine(obj);
            JsonResult result = routineBLL.SaveRoutine(obj, listWork);
            WorkId = result.result ? (string)result.data : WorkId;

            if (result.result)
            {
                _nodeID = obj.NodeID;
                //一览重新加载
                Search();
            }

            MessageHelper.ShowRstMsg(result.result);

            //重新加载首页的成果列表
            startPage.LoadProjectTroubleList();
        }

        /// <summary>
        /// 文件保存时
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileSave_Click(object sender, EventArgs e)
        {
            //保存前检查
            if (!FileCheck()) return;

            RoutineFiles file = new RoutineFiles();

            //文件ID
            file.ID = _fileId;
            //日常工作ID
            file.RoutineID = WorkId.Substring(0,36);
            //文件路径
            file.Name = txtFileName.Text;
            //文件描述
            file.Desc = txtFileDesc.Text;

            //上传文件名
            if(_fileSelectFlg){
                file.Path = FileHelper.UploadFile(txtFilePath.Text, UploadType.Routine, ProjectId, _nodeID);
            }
            else
            {
                file.Path = _filePath;
            }

            //如果返回文件名为空，不保存数据库
            if (string.IsNullOrEmpty(file.Path)) return;

            //保存
            JsonResult result = routineBLL.SaveRoutineFile(file);
            _fileId = result.result ? (string)result.data : _fileId;
            if (result.result)
            {
                //附件列表加载
                LoadFileList(WorkId);
            }
            MessageHelper.ShowRstMsg(result.result);
        }

        /// <summary>
        /// 选择文件按钮按下时
        /// Created：2017.03.30(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX5_Click(object sender, EventArgs e)
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
        /// 行单击事件
        /// Created：2017.03.31(Xuxb)
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
                FileHelper.DownLoadFile(UploadType.Routine, ProjectId, _nodeID, fileName);
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
        /// 点击查询按钮时
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 日常管理
        /// Created：2017.04.01(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridRoutine_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            WorkId = e.GridCell.GridRow.Cells["ID"].Value.ToString(); 
            if (!string.IsNullOrEmpty(WorkId))
            {
                DialogResult drt = MessageHelper.ShowMsg(MessageID.I000000009, MessageType.Confirm, "日常工作");

                if (drt == DialogResult.OK)
                {
                    //加载画面内容
                    LoadPageData();
                    //加载责任人列表
                    var list = routineBLL.GetRoutinWorkList(WorkId);
                    gridManager.PrimaryGrid.DataSource = list;
                }
            }
        }

        /// <summary>
        /// 结点清空
        /// Created：2017.04.07(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.cmbNode.SelectedIndex = -1;
        }

        /// <summary>
        /// 添加责任人
        /// 201//05/31(zhuguanjun)
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
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager("", tmp < 0 ? 0 : tmp,0);
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                gridManager.PrimaryGrid.Rows.Add(new DevComponents.DotNetBar.SuperGrid.GridRow(
                    fmNewManager.ReturnValue.Manager, fmNewManager.ReturnValue.ManagerName, fmNewManager.ReturnValue.Workload,
                    fmNewManager.ReturnValue.StarteDate.ToShortDateString(), fmNewManager.ReturnValue.EndDate.ToShortDateString(),
                    "删除"));
            }
        }

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
        ///  Created：20170531(Czhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            string tmp = e.GridRow.ToString();
            tmp = tmp.Substring(tmp.LastIndexOf("{") + 1);
            tmp = tmp.Substring(0, tmp.LastIndexOf("}"));
            string[] cells = tmp.Trim().Split(',');
            int work = 0;
            int.TryParse(cells[2], out work);
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager(cells[0], work,1);
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 0).Value = fmNewManager.ReturnValue.Manager;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 1).Value = fmNewManager.ReturnValue.ManagerName;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 2).Value = fmNewManager.ReturnValue.Workload;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载画面数据
        /// Created：2017.03.30(Xuxb)
        /// </summary>
        private void LoadPageData()
        {
            if (!string.IsNullOrEmpty(WorkId))
            {
                //日常工作取得
                DomainDLL.Routine obj = routineBLL.GetRoutineObject(WorkId);

                if (obj != null)
                {
                    //节点
                    DataHelper.SetComboxTreeSelectByValue(cmbNode, obj.NodeID);
                    _nodeID = obj.NodeID;
                    //工作名称
                    txtWorkName.Text = obj.Name;
                    //工作描述
                    txtDesc.Text = obj.Desc;
                    //处理结果
                    txtResult.Text = obj.DealResult;
                    //开始日期
                    if (obj.StartDate.HasValue)
                        txtStartDate.Text = obj.StartDate.Value.ToShortDateString();
                    //结束日期
                    if (obj.EndDate.HasValue)
                        txtEndDate.Text = obj.EndDate.Value.ToShortDateString();
                    //完成情况
                    DataHelper.SetComboBoxSelectItemByValue(cmbResultStatus, obj.FinishStatus.ToString());
                    //添加日期
                    txtCreateDate.Text = obj.CREATED.ToShortDateString();

                    //附件列表加载
                    LoadFileList(obj.ID.Substring(0,36));

                    txtFilePath.Text = string.Empty;
                    txtFileName.Text = string.Empty;
                    txtFileDesc.Text = string.Empty;
                }
            }
            else
            {
                //添加日期
                txtCreateDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        /// <summary>
        /// 日常工作保存时检查
        /// Created：2017.03.30(Xuxb)
        /// </summary>
        /// <returns></returns>
        private bool RoutineCheck()
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
            //工作名称是否输入
            if (string.IsNullOrEmpty(txtWorkName.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "工作名称");
                return false;
            }
            //完成情况是否选择
            if (cmbResultStatus.SelectedItem != null && string.IsNullOrEmpty(((ComboItem)cmbResultStatus.SelectedItem).Value.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "完成情况");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 文件保存时检查
        /// Created：2017.03.31(Xuxb)
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
            //日常工作内容是否创建
            if (string.IsNullOrEmpty(WorkId))
            {
                MessageHelper.ShowMsg(MessageID.W000000006, MessageType.Alert, "日常工作内容");
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
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="routineId"></param>
        private void LoadFileList(string routineId)
        {
            //日常工作文件取得
            List<RoutineFiles> list = routineBLL.GetRoutineFiles(routineId);

            //附件列表加载
            int? i = 1;
            foreach (RoutineFiles file in list)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 查询按钮按下时
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        private void Search()
        {
            //取得数据
            DataTable dt = routineBLL.GetRoutinList(ProjectId, this.txtSearchStart.Text, this.txtSearchEnd.Text, txtSearchKey.Text);

            //追加行号
            DataHelper.AddNoCloumn(dt);

            //绑定
            gridRoutine.PrimaryGrid.DataSource = dt;
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
