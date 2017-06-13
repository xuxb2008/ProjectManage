using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DomainDLL;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using BussinessDLL;
using ProjectManagement.Common;
using CommonDLL;
using DevComponents.Editors;
using System.IO;
using DevComponents.Editors.DateTimeAdv;

namespace ProjectManagement.Forms.Project
{
    /// <summary>
    /// 画面名：项目基本信息
    /// Created：20170324(ChengMengjia)
    /// </summary>
    public partial class ProjectInfo : BaseForm
    {
        #region 业务类初期化

        ProjectInfoBLL bll = new ProjectInfoBLL();

        #endregion

        #region 画面变量

        string _jbxxID;
        string _xmzqID;
        string _qkmsID;
        string _fileContractHTSMJName;
        string _fileContractHTDZDName;
        string _fileContractGZSMJName;
        string _fileContractGZDZDName;
        string _fileContractZBDZDName;
        string _fileContractTBDZDName;

        #endregion

        #region 事件
        public ProjectInfo()
        {
            InitializeComponent();

            LoadJBXX();//基本信息
            LoadXMZQ();//项目周期
            LoadQKMS();//情况描述
            LoadFile();//合同文件
            LoadFileList();//其他文件列表

        }

        /// <summary>
        /// 基本信息-保存
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveJBXX_Click(object sender, EventArgs e)
        {

            ContractJBXX entity = new ContractJBXX();
            entity.ID = _jbxxID;
            entity.PID = ProjectId;
            entity.No = txtNo.Text;
            entity.Name = txtName.Text;
            entity.SignDate = dtSignDate.Value;
            entity.Amount = txtAmount.Text;
            entity.A_Name = txtA_Name.Text;
            entity.A_Manager = txtA_Manager.Text;
            entity.A_ManagerTel = txtA_ManagerTel.Text;
            entity.B_Name = txtB_Name.Text;
            entity.B_PManager = txtB_PManager.Text;
            entity.B_PManagerTel = txtB_PManagerTel.Text;
            entity.B_Manager = txtB_Manager.Text;
            entity.B_Tel = txtB_Tel.Text;

            #region 判断是否填写完整
            //if (string.IsNullOrEmpty(entity.No))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "合同编号");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "合同名称");
            //    return;
            //}
            //if (entity.SignDate == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "签订日期");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.Amount))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "合同金额");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.A_Name))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "甲方名称");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.A_Manager))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "甲方负责人");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.A_ManagerTel))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "甲方负责人电话号码");
            //    return;
            //}
            // if (string.IsNullOrEmpty(entity.A_PManager))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "项目经理");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.B_Manager))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "乙方负责人");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.B_Tel))
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "乙方负责人电话号码");
            //    return;
            //}
            #endregion

            JsonResult result = bll.SaveJBXX(entity);
            _jbxxID = result.result ? (string)result.data : _jbxxID;
            MessageHelper.ShowRstMsg(result.result);
        }

        /// <summary>
        /// 项目周期保存
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveXMZQ_Click(object sender, EventArgs e)
        {
            ContractXMZQ entity = new ContractXMZQ();
            entity.ID = _xmzqID;
            entity.PID = ProjectId;
            entity.StartDate = dtStart.Value;
            entity.EndDate = dtEnd.Value;
            entity.TEndDate = dtTEnd.Value;
            entity.TStartDate = dtStart.Value;

            #region 判断是否填写完整
            //if (entity.StartDate == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "开始日期");
            //    return;
            //}
            //if (entity.EndDate == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "结束日期");
            //    return;
            //}
            //if (entity.TStartDate == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "实际开始日期");
            //    return;
            //}
            //if (entity.TEndDate == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "实际结束日期");
            //    return;
            //}
            #endregion

            JsonResult result = bll.SaveXMZQ(entity);
            _xmzqID = result.result ? (string)result.data : _xmzqID;
            MessageHelper.ShowRstMsg(result.result);
        }

        /// <summary>
        /// 情况描述保存
        /// Created:20170324(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveQKMS_Click(object sender, EventArgs e)
        {
            ContractQKMS entity = new ContractQKMS();
            entity.ID = _qkmsID;
            entity.PID = ProjectId;
            entity.Content = txtInfo.Text;
            #region 判断是否填写完整
            if (string.IsNullOrEmpty(entity.Content))
            {
                MessageHelper.ShowMsg(MessageID.E000000001, MessageType.Alert, "情况描述");
                return;
            }
            #endregion
            JsonResult result = bll.SaveQKMS(entity);
            _qkmsID = result.result ? (string)result.data : _qkmsID;
            MessageBox.Show(result.msg);
        }

        /// <summary>
        /// 相关文件-上传
        /// Created：20170328(ChengMengjia)
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
                    ContractFiles entity = new ContractFiles();
                    entity.PID = ProjectId;
                    switch (Type)
                    {
                        case 1:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractHTSMJ, ProjectId, null);
                            entity.Name = "合同扫描件";
                            _fileContractHTSMJName = entity.Path;
                            break;
                        case 2:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractHTDZD, ProjectId, null);
                            entity.Name = "合同电子档";
                            _fileContractHTDZDName = entity.Path;
                            break;
                        case 3:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractGZSMJ, ProjectId, null);
                            entity.Name = "工作说明书扫描件";
                            _fileContractGZSMJName = entity.Path;
                            break;
                        case 4:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractGZDZD, ProjectId, null);
                            entity.Name = "工作说明书电子档";
                            _fileContractGZDZDName = entity.Path;
                            break;
                        case 5:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractZBDZD, ProjectId, null);
                            entity.Name = "招标文件电子档";
                            _fileContractZBDZDName = entity.Path;
                            break;
                        case 6:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.ContractTBDZD, ProjectId, null);
                            entity.Name = "投标文件电子档";
                            _fileContractTBDZDName = entity.Path;
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.Path))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {
                        entity.Type = Type;
                        JsonResult result = bll.SaveFile(entity,true);
                        MessageHelper.ShowRstMsg(result.result);
                        LoadFile();
                    }
                }
            }


        }

        /// <summary>
        /// 相关文件-打开
        /// Created：20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFOpen_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;

            switch (link.Name)
            {
                case "lblFile1":
                    FileHelper.OpenFile(UploadType.ContractHTSMJ, ProjectId, null, _fileContractHTSMJName);
                    break;
                case "lblFile2":
                    FileHelper.OpenFile(UploadType.ContractHTDZD, ProjectId, null, _fileContractHTDZDName);
                    break;
                case "lblFile3":
                    FileHelper.OpenFile(UploadType.ContractGZSMJ, ProjectId, null, _fileContractGZSMJName);
                    break;
                case "lblFile4":
                    FileHelper.OpenFile(UploadType.ContractGZDZD, ProjectId, null, _fileContractGZDZDName);
                    break;
                case "lblFile5":
                    FileHelper.OpenFile(UploadType.ContractZBDZD, ProjectId, null, _fileContractZBDZDName);
                    break;
                case "lblFile6":
                    FileHelper.OpenFile(UploadType.ContractTBDZD, ProjectId, null, _fileContractTBDZDName);
                    break;
            }

        }

        /// <summary>
        /// 相关文件-下载
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_DownLoad(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(7, 1));
            List<ContractFiles> list = bll.GetFiles(ProjectId, Type);
            if (list.Count <= 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //取得上传文件类型
            string fileName = list[0].Path;
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
                case 4:
                    uploadType = UploadType.ContractGZDZD;
                    break;
                case 5:
                    uploadType = UploadType.ContractZBDZD;
                    break;
                case 6:
                    uploadType = UploadType.ContractTBDZD;
                    break;
            }

            FileHelper.DownLoadFile(uploadType, ProjectId, null, fileName);
        }

        /// <summary>
        /// 附件信息编辑-选择附件
        ///  Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFPath.Text = dialog.FileName;
                    txtFPath.Tag = 1;
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtFName.Text = temp[0];
                }
            }
        }

        /// <summary>
        /// 附件信息编辑-清空
        ///  Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFClear_Click(object sender, EventArgs e)
        {
            txtFPath.Tag = 0;
            txtFPath.Clear();
            txtFName.Tag = "";
            txtFName.Clear();
            txtFDesc.Clear();
            gridFile.GetSelectedRows().Select(false);//取消选择
        }


        /// <summary>
        /// 附件信息编辑-保存
        ///  Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFSave_Click(object sender, EventArgs e)
        {
            ContractFiles entity = new ContractFiles();
            entity.ID = txtFName.Tag == null ? "" : txtFName.Tag.ToString();
            entity.PID = ProjectId;
            entity.Type = 0;
            entity.Path = txtFPath.Text;
            entity.Name = txtFName.Text;
            entity.Desc = txtFDesc.Text;
            #region 填写判断
            if (string.IsNullOrEmpty(entity.Path))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "附件");
                return;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "附件名称");
                return;
            }
            #endregion

            #region 文件上传
            //判断是否有选择文件上传
            bool ReUpload = txtFPath.Tag != null && txtFPath.Tag.Equals(1);
            if (ReUpload)
                entity.Path = FileHelper.UploadFile(entity.Path, UploadType.ContractQTFJ, ProjectId, null);
            #endregion

            if (string.IsNullOrEmpty(entity.Path))
                MessageHelper.ShowRstMsg(false);
            else
            {
                JsonResult result = bll.SaveFile(entity, ReUpload);
                MessageHelper.ShowRstMsg(result.result);
                if (result.result)
                {
                    btnFClear_Click(sender, e);
                    LoadFileList();
                }
            }
        }

        /// <summary>
        /// 附件列表的下载按钮单击触发事件
        /// Created:20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDownLoad")
            {
                string fileName = e.GridCell.GridRow.GetCell("Path").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.ContractQTFJ, ProjectId, null, fileName);
            }

        }

        /// <summary>
        /// 附件列表行单击触发事件
        /// Created:20170331(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            DevComponents.DotNetBar.SuperGrid.GridElement list = gridFile.GetSelectedRows()[0];
            string s = list.ToString();
            s = s.Replace("{", ",");
            s = s.Replace("}", ",");
            string[] listS = s.Split(',');
            txtFName.Text = listS[2].Trim();
            txtFDesc.Text = listS[3].Trim();
            txtFName.Tag = listS[5].Trim();
            txtFPath.Text = listS[6].Trim();
            txtFPath.Tag = 0;
        }

        /// <summary>
        /// 签订日期值改变事件
        /// Created:20170414(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtSignDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimeInput dt = (DateTimeInput)sender;
            dtStart.Value = dt.Value;
            dtTStart.Value = dt.Value;
        }
        #endregion

        #region 方法

        /// <summary>
        /// 基本信息-加载
        /// Created:20170324(ChengMengjia)
        /// </summary>
        private void LoadJBXX()
        {
            ContractJBXX jbxx = bll.GetJBXX(ProjectId);
            if (!string.IsNullOrEmpty(jbxx.ID))
            {
                _jbxxID = jbxx.ID;
                txtNo.Text = jbxx.No;
                txtName.Text = jbxx.Name;
                dtSignDate.Value = (DateTime)jbxx.SignDate;
                txtAmount.Text = jbxx.Amount;
                txtA_Name.Text = jbxx.A_Name;
                txtA_Manager.Text = jbxx.A_Manager;
                txtA_ManagerTel.Text = jbxx.A_ManagerTel;
                txtB_Name.Text = jbxx.B_Name;
                txtB_PManager.Text = jbxx.B_PManager;
                txtB_PManagerTel.Text = jbxx.B_PManagerTel;
                txtB_Manager.Text = jbxx.B_Manager;
                txtB_Tel.Text = jbxx.B_Tel;
                dtCREATED.Value = jbxx.CREATED;
            }
            else
            {
                dtCREATED.Value = DateTime.Now;
                txtNo.Text = ProjectNo;
            }

            //项目经理为空
            if (string.IsNullOrEmpty(txtB_PManager.Text))
            {
                List<Stakeholders> list = new StakeholdersBLL().GetPMList(ProjectId);//所有项目经理
                txtB_PManager.Text = list.Count > 0 ? list[0].Name : "";
            }
        }

        /// <summary>
        /// 项目周期-加载
        /// Created:20170324(ChengMengjia)
        /// </summary>
        private void LoadXMZQ()
        {
            ContractXMZQ xmzq = bll.GetXMZQ(ProjectId);
            if (!string.IsNullOrEmpty(xmzq.ID))
            {
                _xmzqID = xmzq.ID;
                dtStart.Value = (DateTime)xmzq.StartDate;
                dtEnd.Value = (DateTime)xmzq.EndDate;
                dtTEnd.Value = (DateTime)xmzq.TEndDate;
                dtStart.Value = (DateTime)xmzq.TStartDate;
            }
        }

        /// <summary>
        /// 情况描述-加载
        /// Created:20170324(ChengMengjia)
        /// </summary>
        private void LoadQKMS()
        {
            ContractQKMS qkms = bll.GetQKMS(ProjectId);
            if (!string.IsNullOrEmpty(qkms.ID))
            {
                _qkmsID = qkms.ID;
                txtInfo.Text = qkms.Content;
            }
        }

        /// <summary>
        /// 主要文件加载
        /// Created：20170328(ChengMengjia)
        /// </summary>
        private void LoadFile()
        {
            List<ContractFiles> list = new List<ContractFiles>();

            //加载项目合同扫描件
            list = bll.GetFiles(ProjectId, 1);
            if (list.Count > 0)
            {
                _fileContractHTSMJName = list[0].Path;
                lblFile1.Show();
                btnDown1.Show();
            }
            else
            {
                lblFile1.Hide();
                btnDown1.Hide();
            }

            //加载项目合同电子档
            list = bll.GetFiles(ProjectId, 2);
            if (list.Count > 0)
            {
                _fileContractHTDZDName = list[0].Path;
                lblFile2.Show();
                btnDown2.Show();
            }
            else
            {
                lblFile2.Hide();
                btnDown2.Hide();
            }

            //加载项目工作说明书扫描件
            list = bll.GetFiles(ProjectId, 3);
            if (list.Count > 0)
            {
                _fileContractGZSMJName = list[0].Path;
                lblFile3.Show();
                btnDown3.Show();
            }
            else
            {
                lblFile3.Hide();
                btnDown3.Hide();
            }

            //加载项目工作说明书电子档
            list = bll.GetFiles(ProjectId, 4);
            if (list.Count > 0)
            {
                _fileContractGZDZDName = list[0].Path;
                lblFile4.Show();
                btnDown4.Show();
            }
            else
            {
                lblFile4.Hide();
                btnDown4.Hide();
            }
            //加载项目招标文件电子档
            list = bll.GetFiles(ProjectId, 5);
            if (list.Count > 0)
            {
                _fileContractZBDZDName = list[0].Path;
                lblFile5.Show();
                btnDown5.Show();
            }
            else
            {
                lblFile5.Hide();
                btnDown5.Hide();
            }

            //加载项目投标文件电子档
            list = bll.GetFiles(ProjectId, 6);
            if (list.Count > 0)
            {
                _fileContractTBDZDName = list[0].Path;
                lblFile6.Show();
                btnDown6.Show();
            }
            else
            {
                lblFile6.Hide();
                btnDown6.Hide();
            }

        }

        /// <summary>
        /// 项目其他文件列表加载
        /// Created：20170405(ChengMengjia)
        /// </summary>
        private void LoadFileList()
        {
            List<ContractFiles> list = bll.GetFiles(ProjectId, 0);
            int? i = 1;
            foreach (ContractFiles file in list)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = list;
        }
        #endregion





    }
}
