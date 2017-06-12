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
        #endregion

        #region 事件
        /// <summary>
        /// 构造函数
        /// 2017/04/11(ZhuGuanJun)
        /// </summary>
        public Contract()
        {
            InitializeComponent();
            DataBind();
            DataHelper.LoadDictItems(cmbLCBFinishStatus, DictCategory.LCBFinishStatus);
            DataHelper.LoadDictItems(cmbSKXXFinishStatus, DictCategory.SKXXFnishStatus);
            DataHelper.LoadDictItems(cmbSKXXBatchNo, DictCategory.SKXXBatchNo);
            LoadSupplierItems(cmbCompanyName, "");
            dtiSignDate.Value = DateTime.Now;
            dtiLCBFinishDate.Value = DateTime.Now;//里程碑完成日期
            dtiSKXXInDate.Value = DateTime.Now;
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
            //if (string.IsNullOrEmpty(txtB_Name.Text))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "分包合同名称");
            //    return;
            //}
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearContract_Click(object sender, EventArgs e)
        {
            txtAmount.Clear();
            txtA_Name.Clear();
            txtA_No.Clear();
            txtB_Name.Clear();
            txtB_No.Clear();
            cmbCompanyName.SelectedIndex = -1;
            txtDesc.Clear();
            dtiSignDate.Value = DateTime.Today;
            SubID = null;
            _id = null;
            superGridControl3.PrimaryGrid.ClearSelectedRows();

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
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(3, 1));
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = string.Empty;
                    if (!string.IsNullOrEmpty(dialog.FileName))
                        path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, null);
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (dicFile.ContainsKey(Type))
                        {
                            dicFile.Remove(Type);
                        }
                        dicFile.Add(Type, path);
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
            bll.GetSubContractAll(row.Cells["ID"].Value.ToString(), out sub, out file, out lcb, out skxx);

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
            SubID = sub.ID.Substring(0, 36) + "-1";//版本id
            #endregion

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
            skxx.Amount = itiSKXXAmount.Value;
            ComboItem item = (ComboItem)cmbSKXXBatchNo.SelectedItem;
            if (item != null)
                skxx.BtachNo = item.Value.ToString();
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
            itiSKXXAmount.Value = 0;
            cmbSKXXBatchNo.SelectedIndex = -1;
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
            DataHelper.SetComboBoxSelectItemByText(cmbSKXXBatchNo, row.Cells["BatchNoName"].Value.ToString());
            DataHelper.SetComboBoxSelectItemByText(cmbSKXXFinishStatus, row.Cells["FinishStatusName"].Value.ToString());
            itiSKXXAmount.Value = int.Parse(row.Cells["Amount"].Value.ToString());
            itiSKXXRatio.Value = int.Parse(row.Cells["Ratio"].Value.ToString());
            dtiSKXXInDate.Value = Convert.ToDateTime(row.Cells["InDate"].Value.ToString());
            SKXXID = row.Cells["ID"].Value.ToString();
        }
        #endregion

        #region 方法
        #endregion

    }
}
