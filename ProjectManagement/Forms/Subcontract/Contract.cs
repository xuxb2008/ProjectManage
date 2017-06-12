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
using System.Linq;

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
        private string AName = "";//主合同名称
        private string ANo = "";//主合同编号

        private string SubID = null;//原始版本id
        private string _id = null;//实际id
        private DateTime CREATED;//分包合同创建时间
        private Dictionary<int, string> dicFile = new Dictionary<int, string>();

        private string LCBID = null;//里程碑id
        private DateTime LCBCREATED = DateTime.MinValue;//里程碑创建时间

        private string SKXXID = null;//付款信息id
        private DateTime SKXXCREATED = DateTime.MinValue;//付款信息创建时间
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
            LoadSpecFiles();
            DataHelper.LoadDictItems(cmbLCBFinishStatus, DictCategory.LCBFinishStatus);
            DataHelper.LoadDictItems(cmbSKXXFinishStatus, DictCategory.SKXXFnishStatus);
            DataHelper.LoadDictItems(cmbSKXXBatchNo, DictCategory.SKXXBatchNo);
            LoadSupplierItems(cmbCompanyName, "");
            dtiSignDate.Value = DateTime.Now;
            dtiLCBFinishDate.Value = DateTime.Now;//里程碑完成日期
            dtiSKXXInDate.Value = DateTime.Now;

            //主合同信息
            ContractJBXX entity = new ProjectInfoBLL().GetJBXX(ProjectId);
            AName = entity.Name;
            ANo = entity.No;
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
        /// Updated：20170612（ChengMengjia）分包合同名称不为空
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
        /// Updated:20170612(ChengMengjia) 文件清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearContract_Click(object sender, EventArgs e)
        {
            txtAmount.Clear();
            txtA_Name.Text = AName;
            txtA_No.Text = ANo;
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

            #region 文件清空
            btn1.Tag = null;
            lbl1.Visible = false;
            btnD1.Visible = false;
            btn2.Tag = null;
            lbl2.Visible = false;
            btnD2.Visible = false;
            btn3.Tag = null;
            lbl3.Visible = false;
            btnD3.Visible = false;
            btn4.Tag = null;
            lbl4.Visible = false;
            btnD4.Visible = false;
            btn5.Tag = null;
            lbl5.Visible = false;
            btnD5.Visible = false;
            #endregion
        }

        /// <summary>
        /// 文件点击打开事件
        ///  Created:20170612(ChengMengjia) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileName_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;
            FileHelper.OpenFile(UploadType.SubContract, ProjectId, "", link.Tag.ToString());
        }

        /// <summary>
        /// 文件上传事件
        /// 2017/04/11(ZhuGuanJun)
        /// Updated:20170612(ChengMengjia) 上传保存
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
                    SubContractFiles entity = new SubContractFiles();
                    entity.ID = button.Tag == null ? "" : button.Tag.ToString();
                    entity.SubID = SubID.Substring(0, 36);
                    entity.Path = FileHelper.UploadFile(dialog.FileName, UploadType.SubContract, ProjectId, "");
                    switch (Type)
                    {
                        case 1:
                            entity.Name = "合同电子档";
                            break;
                        case 2:
                            entity.Name = "合同扫描件";
                            break;
                        case 3:
                            entity.Name = "工作说明书电子档";
                            break;
                        case 4:
                            entity.Name = "工作说明书扫描件";
                            break;
                        case 5:
                            entity.Name = "其它附件";
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.Path))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {
                        entity.Type = Type;
                        JsonResult result = bll.SaveFile(entity);
                        MessageHelper.ShowRstMsg(result.result);
                        LoadSpecFiles();
                    }


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
        ///  Updated:20170612(ChengMengjia) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_DownLoad(object sender, EventArgs e)
        {
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(button.Name.Length - 1));
            List<SubContractFiles> list = bll.GetFiles(SubID, Type);
            if (list.Count <= 0)
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }
            FileHelper.DownLoadFile(UploadType.SubContract, ProjectId, "", list[0].Path);
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
            txtA_Name.Text = string.IsNullOrEmpty(sub.A_Name) ? AName : sub.A_Name;
            txtAmount.Text = sub.Amount;
            txtA_No.Text = string.IsNullOrEmpty(sub.A_No) ? ANo : sub.A_No;
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

            //分包合同附件
            LoadSpecFiles();

            #region 里程碑信息
            superGridControl1.PrimaryGrid.DataSource = lcb;
            #endregion

            #region 付款信息
            superGridControl2.PrimaryGrid.DataSource = skxx;
            #endregion
        }

        /// <summary>
        /// 保存里程碑
        /// 2017/04/14(zhuguanjun)
        /// Updated：20170612（ChengMengjia）里程碑名称不为空
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
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "里程碑名称");
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
        /// 保存付款信息
        /// 2017/04/14(zhuguanjun)
        /// Updated：20170612（ChengMengjia）批次号不为空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSKXX_Click(object sender, EventArgs e)
        {
            string btachNo = "";//批次号
            #region 检查
            if (string.IsNullOrEmpty(SubID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分包合同");
                return;
            }
            ComboItem item = (ComboItem)cmbSKXXBatchNo.SelectedItem;
            if (item != null)
                btachNo = item.Value.ToString();
            if (string.IsNullOrEmpty(btachNo))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "付款批次");
                return;
            }
            #endregion
            SubContractSKXX skxx = new SubContractSKXX();
            skxx.Amount = itiSKXXAmount.Value;
            skxx.BtachNo = btachNo;
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
        /// 清空付款信息
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
        /// 绑定付款信息
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
        /// 付款信息选择事件
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

        /// <summary>
        /// 加载分包下面文件
        /// Created:20170612(ChengMengjia)
        /// </summary>
        void LoadSpecFiles()
        {
            List<DomainDLL.SubContractFiles> list = bll.GetFiles(SubID, null);
            //合同电子档
            DomainDLL.SubContractFiles entity = list.Where(t => t.Type == 1).FirstOrDefault();
            bool IsExist = entity != null;
            if (IsExist)
            {
                lbl1.Text = entity.Name;
                lbl1.Tag = entity.Path;
                btn1.Tag = entity.ID;
            }
            else
                btn1.Tag = null;
            lbl1.Visible = IsExist;
            btnD1.Visible = IsExist;

            //合同扫描件
            entity = null;
            entity = list.Where(t => t.Type == 2).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl2.Text = entity.Name;
                lbl2.Tag = entity.Path;
                btn2.Tag = entity.ID;
            }
            else
                btn2.Tag = null;
            lbl2.Visible = IsExist;
            btnD2.Visible = IsExist;

            //工作说明书电子档
            entity = null;
            entity = list.Where(t => t.Type == 3).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl3.Text = entity.Name;
                lbl3.Tag = entity.Path;
                btn3.Tag = entity.ID;
            }
            else
                btn3.Tag = null;
            lbl3.Visible = IsExist;
            btnD3.Visible = IsExist;

            //工作说明书扫描件
            entity = null;
            entity = list.Where(t => t.Type == 4).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl4.Text = entity.Name;
                lbl4.Tag = entity.Path;
                btn4.Tag = entity.ID;
            }
            else
                btn4.Tag = null;
            lbl4.Visible = IsExist;
            btnD4.Visible = IsExist;

            //其他附件
            entity = null;
            entity = list.Where(t => t.Type == 5).FirstOrDefault();
            IsExist = entity != null;
            if (IsExist)
            {
                lbl5.Text = entity.Name;
                lbl5.Tag = entity.Path;
                btn5.Tag = entity.ID;
            }
            else
                btn5.Tag = null;
            lbl5.Visible = IsExist;
            btnD5.Visible = IsExist;
        }


        #endregion

    }
}
