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
using System.IO;

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
        //日常工作对应的NodeID
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

        RoutineBLL routineBLL = new RoutineBLL();

        #endregion

        #region 事件


        /// <summary>
        /// 画面初期化
        /// </summary>
        public Routine(string nodeID)
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
        private void Routine_Load(object sender, EventArgs e)
        {
            init();
        }

        /// <summary>
        /// 日常工作清空按钮按下时
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkClear_Click(object sender, EventArgs e)
        {
            ClearWork();
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
            #region 保存前检查
            //项目ID是否存在
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            //结点是否选择
            //if (string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "结点");
            //    return
            //}
            //工作名称是否输入
            if (string.IsNullOrEmpty(txtWorkName.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "工作名称");
                return;
            }
            //完成情况是否选择
            if (cmbResultStatus.SelectedItem != null && string.IsNullOrEmpty(((ComboItem)cmbResultStatus.SelectedItem).Value.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "完成情况");
                return;
            }
            #endregion
            List<RoutineWork> listWork = new List<RoutineWork>();
            if (!GetEditManager(ref listWork, true)) return;//如果填写无误

            DomainDLL.Routine obj = new DomainDLL.Routine();
            obj.ID = WorkId;//日常工作ID
            #region 赋值
            //节点(如果关联结点未选择时，默认设定为项目结点)
            if (cmbNode.SelectedIndex < 0 || string.IsNullOrEmpty(cmbNode.SelectedNode.Name))
                obj.NodeID = DataHelper.GetNodeIdByProjectId(ProjectId);
            else
                obj.NodeID = cmbNode.SelectedNode.Name.Substring(0, 36);
            //工作名称
            obj.Name = txtWorkName.Text;
            //工作描述
            obj.Desc = txtDesc.Text;
            //处理结果
            obj.DealResult = txtResult.Text;
            //预期工作量
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
            #endregion

            bool IsEdit = false;
            string oldPath = "";//旧的文件存放路径
            // 判断是否为修改状态 节点是否改变
            if (!string.IsNullOrEmpty(_nodeID))
            {
                IsEdit = true;
                oldPath = FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Routine, ProjectId, _nodeID);
            }

            JsonResult result = routineBLL.SaveRoutine(ProjectId, obj, listWork);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                //一览重新加载
                Search();
                //清空编辑
                ClearWork();
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectTroubleList();
                #region  结点改变时，移动文件到新的节点
                if (IsEdit)
                    FileHelper.MoveFloder(oldPath, FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Routine, ProjectId, _nodeID));
                #endregion
            }
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
            file.RoutineID = WorkId.Substring(0, 36);
            //文件路径
            file.Name = txtFileName.Text;
            //文件描述
            file.Desc = txtFileDesc.Text;

            DomainDLL.Routine routine = routineBLL.GetRoutineObject(WorkId, "");//需要获取日常工作作为节点的ID

            //上传文件名
            if (_fileSelectFlg)
            {
                file.Path = FileHelper.UploadFile(txtFilePath.Text, UploadType.Routine, ProjectId, routine.NodeID);
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
                LoadFileList();
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
        /// 文件行单击事件
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
            ClearWork();
            WorkId = e.GridCell.GridRow.Cells["ID"].Value.ToString();
            if (!string.IsNullOrEmpty(WorkId))
            {
                LoadContent();
                LoadFileList();//加载日常工作文件列表
            }
        }
        /// <summary>
        /// 工作日常列表加载时选择颜色
        ///  Created：20160607(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridRoutine_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listRow = gridRoutine.PrimaryGrid.Rows.ToList();
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
        /// 开始或结束时间值变化
        /// Created:20170605(ChengMengjia)
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
        /// 添加责任人
        /// Created：20170531(zhugaunjun)
        /// Updated：20170606(ChengMengjia)
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
                List<RoutineWork> listWork = new List<RoutineWork>();
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
                    listWork.Add(new RoutineWork()
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

        #region 方法

        /// <summary>
        /// 页面初始化
        /// Created：20170606(ChengMengjia)
        /// </summary>
        public void init()
        {
            DataHelper.SetComboxTreeData(this.cmbNode, ProjectId);//加载WBS结点下拉列表
            DataHelper.LoadDictItems(cmbResultStatus, DictCategory.WorkHandleStatus);//加载完成情况下拉列表
            Search(); //加载日常工作列表
            if (string.IsNullOrEmpty(WorkId) && string.IsNullOrEmpty(_nodeID))
            {
                ClearWork();//清空
            }
            else
            {
                LoadContent();//加载日常工作内容
                LoadFileList();//加载日常工作文件列表
            }
        }
        /// <summary>
        /// 加载日常工作内容
        /// Created：20170606(ChengMengjia)
        /// </summary>
        public void LoadContent(string NodeID)
        {
            _nodeID = NodeID;
            DomainDLL.Routine obj = routineBLL.GetRoutineObject("", _nodeID);
            PNode parentNode = new WBSBLL().GetParentNode(obj.NodeID); //日常工作挂靠的节点
            DataHelper.SetComboxTreeSelectByValue(cmbNode, parentNode.ID);
            WorkId = obj.ID;
            txtWorkName.Text = obj.Name;
            txtDesc.Text = obj.Desc;//工作描述
            txtResult.Text = obj.DealResult;//处理结果
            if (obj.StartDate.HasValue)//开始日期
                txtStartDate.Text = obj.StartDate.Value.ToShortDateString();
            if (obj.EndDate.HasValue)//结束日期
                txtEndDate.Text = obj.EndDate.Value.ToShortDateString();
            DataHelper.SetComboBoxSelectItemByValue(cmbResultStatus, obj.FinishStatus.ToString());//完成情况
            txtCreateDate.Text = obj.CREATED.ToShortDateString();//添加日期
            intWorkload.Value = (int)obj.Workload;//工作量

            //加载责任人列表
            var list = routineBLL.GetRoutinWorkList(obj.ID);
            gridManager.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 加载日常工作内容
        /// Created：20170606(ChengMengjia)
        /// </summary>
        private void LoadContent()
        {
            DomainDLL.Routine obj = routineBLL.GetRoutineObject(WorkId, _nodeID);
            PNode parentNode = new WBSBLL().GetParentNode(obj.NodeID); //日常工作挂靠的节点
            DataHelper.SetComboxTreeSelectByValue(cmbNode, parentNode.ID);
            _nodeID = obj.NodeID;
            WorkId = obj.ID;
            txtWorkName.Text = obj.Name;
            txtDesc.Text = obj.Desc;//工作描述
            txtResult.Text = obj.DealResult;//处理结果
            if (obj.StartDate.HasValue)//开始日期
                txtStartDate.Text = obj.StartDate.Value.ToShortDateString();
            if (obj.EndDate.HasValue)//结束日期
                txtEndDate.Text = obj.EndDate.Value.ToShortDateString();
            DataHelper.SetComboBoxSelectItemByValue(cmbResultStatus, obj.FinishStatus.ToString());//完成情况
            txtCreateDate.Text = obj.CREATED.ToShortDateString();//添加日期
            intWorkload.Value = (int)obj.Workload;//工作量

            //加载责任人列表
            var list = routineBLL.GetRoutinWorkList(obj.ID);
            gridManager.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 加载文件列表
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        /// <param name="routineId"></param>
        private void LoadFileList()
        {
            //日常工作文件取得
            List<RoutineFiles> list = routineBLL.GetRoutineFiles(WorkId);
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
        /// 加载日常工作列表
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        private void Search()
        {
            gridRoutine.PrimaryGrid.DataSource = routineBLL.GetRoutinList(ProjectId, this.txtSearchStart.Text, this.txtSearchEnd.Text, txtSearchKey.Text);
        }

        /// <summary>
        /// 获取编辑的责任人列表
        /// Created:20170531(zhuguanjun)
        /// Updated:20160606(ChengMengjia)
        /// </summary>
        /// <returns></returns>
        bool GetEditManager(ref List<RoutineWork> listWork, bool NeedCheck)
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
                listWork.Add(new RoutineWork() { Manager = cells[0], ManagerName = cells[1], Workload = int.Parse(cells[2]), ActualWorkload = int.Parse(cells[3]) });
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
        /// 日常工作清空
        /// Created：2017.03.31(Xuxb)
        /// </summary>
        private void ClearWork()
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
            txtStartDate.Value = DateTime.Now;
            //结束日期
            txtEndDate.Value = DateTime.Now;
            //工作量
            intWorkload.Value = 1;
            //完成情况
            cmbResultStatus.SelectedIndex = -1;
            //添加日期
            txtCreateDate.Text = DateTime.Now.ToShortDateString();
            //清空责任人
            gridManager.PrimaryGrid.DataSource = null;

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
        }
        #endregion

    }
}
