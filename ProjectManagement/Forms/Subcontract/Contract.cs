using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ProjectManagement.Common;
using BussinessDLL;
using DomainDLL;
using CommonDLL;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using DevComponents.DotNetBar.Controls;

namespace ProjectManagement.Forms.Subcontract
{
    /// <summary>
    /// 分包合同管理
    /// 2017/4/11(zhuguanjun)
    /// </summary>
    public partial class Contract : BaseForm
    {
        #region 业务逻辑类初始化
        ContractBLL bll = new ContractBLL();
        SupplierBLL supplierbll = new SupplierBLL();

        string MainName, MainNo;//主合同编号、名称
        #endregion

        #region 变量
        private string SubID = null;//原始版本id
        private string _id = null;//实际id
        private DateTime CREATED;//分包合同创建时间
        private Dictionary<int, string> dicFile = new Dictionary<int, string>();

        private string LCBID = null;//里程碑id
        private DateTime LCBCREATED = DateTime.MinValue;//里程碑创建时间

        private string SKXXID = null;//收款信息id
        private DateTime SKXXCREATED = DateTime.MinValue;//收款信息创建时间

        string _fileContractHTSMJName;
        string _fileContractHTDZDName;
        string _fileContractGZSMJName;
        string _fileContractGZDZDName;
        #endregion

        #region 事件
        /// <summary>
        /// 构造函数
        /// 2017/04/11(ZhuGuanJun)
        /// Updated：20170619（ChengMengjia） 加载主合同编号、名称；里程碑完成情况绑定和付款完成情况绑定
        /// </summary>
        public Contract()
        {
            InitializeComponent();

            //主合同编号、名称
            ContractJBXX conJBXX = new ProjectInfoBLL().GetJBXX(ProjectId);
            MainName = conJBXX.Name;
            MainNo = conJBXX.No;
            txtA_Name.Text = MainName;
            txtA_No.Text = MainNo;

            DataBind();
            DataHelper.LoadDictItems(cmbLCBFinishStatus, DictCategory.Milestones_FinshStatus);
            DataHelper.LoadDictItems(cmbSKXXFinishStatus, DictCategory.Receivables_FinshStatus);
            //DataHelper.LoadDictItems(cmbSKXXBatchNo, DictCategory.Receivables_BatchNo);
            LoadSupplierItems(cmbCompanyName, "");
            dtiSignDate.Value = DateTime.Now;
            dtiLCBFinishDate.Value = DateTime.Now;//里程碑完成日期
            dtiSKXXInDate.Value = DateTime.Now;
            LoadFile();//合同文件

        }

        /// <summary>
        /// 供应商下拉框
        /// 2017/05/03(zhuguanjun)
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="Value"></param>
        private void LoadSupplierItems(ComboBoxEx combobox, string Value)
        {
            var list = bll.GetSupplierList(ProjectId);
            if (list == null)
                return;
            foreach (DomainDLL.Supplier c in list)
            {
                ComboItem item = new ComboItem();
                item.Text = c.Name;
                item.Value = c.ID;
                combobox.Items.Add(item);
            }
        }

        /// <summary>
        /// 绑定数据
        /// 2017/04/11(ZhuGuanJun)
        /// </summary>
        private void DataBind()
        {
            DataTable dt = bll.GetContractList(ProjectId);
            superGridControl3.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 保存分包合同点击事件
        /// 2017/04/11(ZhuGuanJun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveContrat_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            //if (string.IsNullOrEmpty(txtA_No.Text))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "主合同编号");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtA_Name.Text))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "主合同名称");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtB_No.Text))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "分包合同编号");
            //    return;
            //}
            if (string.IsNullOrEmpty(txtB_Name.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "分包合同名称");
                return;
            }
            #endregion

            #region 分包合同信息
            SubContract entity = new SubContract();
            entity.Desc = txtDesc.Text.ToString();
            entity.Amount = txtAmount.Text.ToString();
            entity.A_Name = txtA_Name.Text;
            entity.A_No = txtA_No.Text;
            entity.B_Name = txtB_Name.Text;
            entity.B_No = txtB_No.Text;
            entity.CompanyName = (ComboItem)cmbCompanyName.SelectedItem != null ? ((ComboItem)cmbCompanyName.SelectedItem).Value.ToString() : "";
            entity.PID = ProjectId;
            entity.SignDate = dtiSignDate.Value;
            entity.ID = _id;
            #endregion

            JsonResult json = bll.SaveSubContract(entity, dicFile, out _id);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                SubID = (string)json.data;//版本
            DataBind();
        }

        /// <summary>
        /// 分包合同清空事件
        /// 2017/04/11(zhugaunjun)
        /// Updated：20170619（ChengMengjia） 不清空主合同名称、编号；清空下方文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearContract_Click(object sender, EventArgs e)
        {
            SubID = null;
            _id = null;

            //分包内容信息清空
            txtAmount.Clear();
            txtB_Name.Clear();
            txtB_No.Clear();
            cmbCompanyName.SelectedIndex = -1;
            txtDesc.Clear();
            dtiSignDate.Value = DateTime.Today;
            //superGridControl3.PrimaryGrid.DataSource=null;

            //下方文件清空
            LoadFile();
            btnFClear_Click(null, null);
            gridFile.PrimaryGrid.DataSource = null;
             

            btnClearLCB_Click(null, null);
            btnClearSKXX_Click(null, null);
            superGridControl1.PrimaryGrid.DataSource = null;
            superGridControl2.PrimaryGrid.DataSource = null;
        }

        /// <summary>
        /// 文件上传事件
        /// 2017/04/11(ZhuGuanJun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_Upload(object sender, EventArgs e)
        {
            //ButtonX button = (ButtonX)sender;
            //int Type = int.Parse(button.Name.Substring(3, 1));
            //using (OpenFileDialog dialog = new OpenFileDialog())
            //{
            //    dialog.Multiselect = false;
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string path = string.Empty;
            //        if (!string.IsNullOrEmpty(dialog.FileName))
            //            path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
            //        if (!string.IsNullOrEmpty(path))
            //        {
            //            if (dicFile.ContainsKey(Type))
            //            {
            //                dicFile.Remove(Type);
            //            }
            //            dicFile.Add(Type, path);
            //        }                        
            //    }
            //}
            if (string.IsNullOrEmpty(SubID))
            {
                MessageBox.Show("请选择分包合同");
                return;
            }
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(3, 1));
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SubContractFiles entity = new SubContractFiles();
                    entity.SubID = SubID;
                    switch (Type)
                    {
                        case 1:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
                            entity.Name = "合同扫描件";
                            _fileContractHTSMJName = entity.Path;
                            break;
                        case 2:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
                            entity.Name = "合同电子档";
                            _fileContractHTDZDName = entity.Path;
                            break;
                        case 3:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
                            entity.Name = "工作说明书扫描件";
                            _fileContractGZSMJName = entity.Path;
                            break;
                        case 4:
                            entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
                            entity.Name = "工作说明书电子档";
                            _fileContractGZDZDName = entity.Path;
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.Path))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {
                        entity.Type = Type;
                        JsonResult result = bll.SaveFile(entity, true);
                        MessageHelper.ShowRstMsg(result.result);
                        LoadFile();
                    }
                }
            }
        }

        /// <summary>
        /// 文件下载事件
        /// 2017/04/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_DownLoad(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(SubID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分包合同");
                return;
            }
            #endregion
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(4, 1));
            List<SubContractFiles> list = bll.GetFiles(SubID, Type);
            if (list.Count <= 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //取得上传文件类型
            string fileName = list[0].Path;

            FileHelper.DownLoadFile(UploadType.SubContract, ProjectId, null, fileName);
        }

        /// <summary>
        /// 分包合同点击事件
        /// 2017/04/13(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl3_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl3.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl1.PrimaryGrid.ClearSelectedColumns();
                return;
            }

            btnClearSKXX_Click(null, null);
            btnClearLCB_Click(null, null);

            GridRow row = (GridRow)rows[0];
            SubContract Sub = new SubContract();
            DataTable file = new DataTable();
            DataTable lcb = new DataTable();
            DataTable skxx = new DataTable();
            SubContract sub = new SubContract();
            bll.GetSubContractAll(row.Cells["ID"].Value.ToString().Substring(0,36), out sub, out file, out lcb, out skxx);

            #region 合同信息
            txtA_Name.Text = sub.A_Name;
            txtAmount.Text = sub.Amount;
            txtA_No.Text = sub.A_No;
            txtB_Name.Text = sub.B_Name;
            txtB_No.Text = sub.B_No;
            cmbCompanyName.SelectedIndex = -1;
            DataHelper.SetComboBoxSelectItemByValue(cmbCompanyName, sub.CompanyName);
            //DataHelper.SetComboBoxSelectItemByValue(cmbCompanyName, sub.CompanyName);
            txtDesc.Text = sub.Desc;
            dtiSignDate.Value = sub.SignDate.Value;
            CREATED = sub.CREATED;
            _id = sub.ID;//实际id
            SubID = sub.ID.Substring(0, 36);//版本id
            #endregion

            //附件信息
            LoadFile();
            LoadFileList();

            #region 里程碑信息
            superGridControl1.PrimaryGrid.DataSource = lcb;
            #endregion

            #region 收款信息
            superGridControl2.PrimaryGrid.DataSource = skxx;
            #endregion
        }

        /// <summary>
        /// 保存里程碑
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveLCB_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(SubID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分包合同");
                return;
            }
            if (string.IsNullOrEmpty(txtLCBName.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "里程碑名称");
                return;
            }
            #endregion
            SubContractLCB lcb = new SubContractLCB();
            lcb.Condition = txtLCBCondition.Text;
            lcb.FinishDate = dtiLCBFinishDate.Value;
            ComboItem item = (ComboItem)cmbLCBFinishStatus.SelectedItem;
            if (item != null)
                lcb.FinishStatus = int.Parse(item.Value.ToString());
            lcb.Name = txtLCBName.Text;
            lcb.Remark = txtLCBRemark.Text;
            lcb.SubID = SubID;//版本id;
            lcb.ID = LCBID;
            lcb.CREATED = LCBCREATED;
            JsonResult json = bll.SaveLCB(lcb);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClearLCB_Click(null, null);
            BindLCB();
        }

        /// <summary>
        /// 清空里程碑
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearLCB_Click(object sender, EventArgs e)
        {
            txtLCBCondition.Clear();
            dtiLCBFinishDate.Value = DateTime.Today;
            cmbLCBFinishStatus.SelectedIndex = -1;
            txtLCBName.Clear();
            txtLCBRemark.Clear();
            LCBID = null;
            LCBCREATED = DateTime.Today;
            superGridControl1.PrimaryGrid.ClearSelectedRows();
        }

        /// <summary>
        /// 保存收款信息
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSKXX_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(SubID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分包合同");
                return;
            }
            ComboItem itemfkxx = (ComboItem)cmbSKXXFinishStatus.SelectedItem;
            if (itemfkxx == null || string.IsNullOrEmpty(itemfkxx.Value.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "付款信息");
                return;
            }
            #endregion
            SubContractSKXX skxx = new SubContractSKXX();
            skxx.Amount = int.Parse(SKXXAmount.Text);
            //ComboItem item = (ComboItem)cmbSKXXBatchNo.SelectedItem;
            //if (item != null)
            //    skxx.BatchNo = item.Value.ToString();
            skxx.BatchNo = SKXXBatchNo.Text;
            skxx.Condition = txtSKXXCondition.Text;
            ComboItem item1 = (ComboItem)cmbSKXXFinishStatus.SelectedItem;
            if (item1 != null)
                skxx.FinishStatus = int.Parse(item1.Value.ToString());
            skxx.InDate = dtiSKXXInDate.Value;
            skxx.Ratio = itiSKXXRatio.Value;
            skxx.Remark = txtSKXXRemark.Text;
            skxx.SubID = SubID;//版本id
            skxx.CREATED = SKXXCREATED;
            skxx.ID = SKXXID;
            JsonResult json = bll.SaveSKXX(skxx);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClearSKXX_Click(null, null);
            BindSKXX();
        }

        /// <summary>
        /// 清空收款信息
        /// 2017/04/13(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSKXX_Click(object sender, EventArgs e)
        {
            SKXXAmount.Clear();
            SKXXBatchNo.Clear();
            //cmbSKXXBatchNo.SelectedIndex = -1;
            txtSKXXCondition.Clear();
            cmbSKXXFinishStatus.SelectedIndex = -1;
            dtiSKXXInDate.Value = DateTime.Now;
            itiSKXXRatio.Value = 0;
            txtSKXXRemark.Clear();
            SKXXID = null;
            SKXXCREATED = DateTime.Now;
            superGridControl2.PrimaryGrid.ClearSelectedRows();
        }

        /// <summary>
        /// 绑定里程碑
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        public void BindLCB()
        {
            DataTable DT = bll.GetLCBList(SubID);
            superGridControl1.PrimaryGrid.DataSource = DT;
        }

        /// <summary>
        /// 绑定收款信息
        /// 2017/04/14(zhuguanjun)
        /// </summary>
        public void BindSKXX()
        {
            DataTable DT = bll.GetSKXXList(SubID);
            superGridControl2.PrimaryGrid.DataSource = DT;
        }

        /// <summary>
        /// 里程碑选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl1.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            txtLCBCondition.Text = row.Cells["Condition"].Value.ToString();
            txtLCBName.Text = row.Cells["Name"].Value.ToString();
            txtLCBRemark.Text = row.Cells["Remark"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByText(cmbLCBFinishStatus, row.Cells["FinishStatusName"].Value.ToString());
            dtiLCBFinishDate.Value = Convert.ToDateTime(row.Cells["FinishDate"].Value.ToString());
            LCBID = row.Cells["ID"].Value.ToString();
        }

        /// <summary>
        /// 收款信息选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl2_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl2.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl2.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            txtSKXXCondition.Text = row.Cells["Condition"].Value.ToString();
            txtSKXXRemark.Text = row.Cells["Remark"].Value.ToString();
            SKXXBatchNo.Text = row.Cells["BatchNo"].Value.ToString();
            //DataHelper.SetComboBoxSelectItemByText(cmbSKXXBatchNo, row.Cells["BatchNoName"].Value.ToString());
            DataHelper.SetComboBoxSelectItemByText(cmbSKXXFinishStatus, row.Cells["FinishStatusName"].Value.ToString());
            SKXXAmount.Text = row.Cells["Amount"].Value.ToString();
            itiSKXXRatio.Value = int.Parse(row.Cells["Ratio"].Value.ToString());
            dtiSKXXInDate.Value = Convert.ToDateTime(row.Cells["InDate"].Value.ToString());
            SKXXID = row.Cells["ID"].Value.ToString();
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
                    FileHelper.OpenFile(UploadType.SubContract, ProjectId, null, _fileContractHTSMJName);
                    break;
                case "lblFile2":
                    FileHelper.OpenFile(UploadType.SubContract, ProjectId, null, _fileContractHTDZDName);
                    break;
                case "lblFile3":
                    FileHelper.OpenFile(UploadType.SubContract, ProjectId, null, _fileContractGZSMJName);
                    break;
                case "lblFile4":
                    FileHelper.OpenFile(UploadType.SubContract, ProjectId, null, _fileContractGZDZDName);
                    break;
            }

        }
        #endregion

        #region 方法
        /// <summary>
        /// 主要文件加载
        /// 2017/06/13(zhuguanjun)
        /// </summary>
        private void LoadFile()
        {
            List<SubContractFiles> list = new List<SubContractFiles>();

            //加载项目合同扫描件
            list = bll.GetFiles(SubID, 1);
            if (list.Count > 0)
            {
                _fileContractHTSMJName = list[0].Path;
                lblFile1.Show();
                btnD1.Show();
            }
            else
            {
                lblFile1.Hide();
                btnD1.Hide();
            }

            //加载项目合同电子档
            list = bll.GetFiles(SubID, 2);
            if (list.Count > 0)
            {
                _fileContractHTDZDName = list[0].Path;
                lblFile2.Show();
                btnD2.Show();
            }
            else
            {
                lblFile2.Hide();
                btnD2.Hide();
            }

            //加载项目工作说明书扫描件
            list = bll.GetFiles(SubID, 3);
            if (list.Count > 0)
            {
                _fileContractGZSMJName = list[0].Path;
                lblFile3.Show();
                btnD3.Show();
            }
            else
            {
                lblFile3.Hide();
                btnD3.Hide();
            }

            //加载项目工作说明书电子档
            list = bll.GetFiles(SubID, 4);
            if (list.Count > 0)
            {
                _fileContractGZDZDName = list[0].Path;
                lblFile4.Show();
                btnD4.Show();
            }
            else
            {
                lblFile4.Hide();
                btnD4.Hide();
            }

        }
        #endregion

        /// <summary>
        /// 选择附件
        /// 2017/06/13(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFSelect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SubID))
            {
                MessageBox.Show("请选择分包合同");
                return;
            }
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
        /// 其他附件清空
        /// 2017/06/13(zhuguanjun)
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
        /// 其他附件编辑保存
        /// 2017/06/13(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFSave_Click(object sender, EventArgs e)
        {
            SubContractFiles entity = new SubContractFiles();
            entity.ID = txtFName.Tag == null ? "" : txtFName.Tag.ToString();
            entity.SubID = SubID;
            entity.Type = 0;
            entity.Path = txtFPath.Text;
            entity.Name = txtFName.Text;
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
        /// 其他附件文件下载
        /// 2017/06/13(zhuguanjun)
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
        /// 其他附件
        /// 2017/06/13(zhuguanjun)
        /// </summary>
        private void LoadFileList()
        {
            List<SubContractFiles> list = bll.GetFiles(SubID, 0);
            int? i = 1;
            foreach (SubContractFiles file in list)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = list;
        }

        private void itiSKXXRatio_ValueChanged(object sender, EventArgs e)
        {
            if(!txtAmount.Text.IsNullOrEmpty())
            {
                decimal temp = 0;

                if(decimal.TryParse(txtAmount.Text, out temp))
                {
                    SKXXAmount.Text = (temp * itiSKXXRatio.Value / 100).ToString();
                }
            }
        }
    }
}
