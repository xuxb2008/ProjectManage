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
using DomainDLL;
using BussinessDLL;
using DevComponents.DotNetBar.SuperGrid;
using CommonDLL;
using System.IO;
using DevComponents.Editors;

namespace ProjectManagement.Forms.Income
{
    public partial class Earning : BaseForm
    {
        #region 业务类初始化
        private EarningBLL bll = new EarningBLL();
        #endregion

        #region 变量
        private string ID = null;
        private DateTime CREATED = DateTime.Now;
        private string FilePath = null;
        private int recordcount = 0;
        #endregion

        #region 事件
        public Earning()
        {
            InitializeComponent();
            DataHelper.LoadDictItems(cbSFinishStatus, DictCategory.EarningStatus);
            DataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            DataTable list = bll.GetIncomeList(ProjectId);
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(txtStep.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收入阶段");
                return;
            }
            //if (string.IsNullOrEmpty(txtExplanation.Text.ToString()))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收入说明");
            //    return;
            //}
            //if (string.IsNullOrEmpty(iRatio.Text.ToString()))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "完成比例");
            //    return;
            //}
            #endregion

            DomainDLL.Income income = new DomainDLL.Income();
            income.ID = ID;
            income.CREATED = CREATED;
            income.Ratio = Convert.ToDecimal(string.IsNullOrEmpty(iRatio.Text.ToString()) ? "0" : iRatio.Text.ToString());
            income.Remark = txtRemark.Text.ToString();
            income.Step = txtStep.Text.ToString();
            income.Explanation = txtExplanation.Text.ToString();
            income.UPDATED = DateTime.Now;
            ComboItem item = (ComboItem)cbSFinishStatus.SelectedItem;
            if (item != null)
                income.FinishStatus = int.Parse(item.Value.ToString());
            income.FinishTag = txtFinishTag.Text.ToString();
            income.PID = ProjectId;
            income.FilePath = FilePath;
            if (!string.IsNullOrEmpty(txtFilePath.Text))
                income.FilePath = FileHelper.UploadFile(txtFilePath.Text, UploadType.InCome, ProjectId, null);

            if (string.IsNullOrEmpty(income.FilePath) && !string.IsNullOrEmpty(txtFilePath.Text.ToString()))
                return;
            else
            {
                JsonResult json = bll.SaveIncome(income);
                MessageHelper.ShowRstMsg(json.result);
                if (json.result)
                    btnClear_Click(null, null);
                DataBind();
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ID = null;
            FilePath = null;
            txtStep.Clear();
            txtFilePath.Clear();
            cbSFinishStatus.SelectedIndex = -1;
            txtFinishTag.Clear();
            txtRemark.Clear();
            txtUpload.Clear();
            txtExplanation.Clear();
            iRatio.Value = 0;
            superGridControl1.PrimaryGrid.ClearSelectedRows();
            this.txtStep.Clear();
        }

        /// <summary>
        /// 选择附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtFilePath.Text = dialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }

        /// <summary>
        /// 点击列表
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
            txtFinishTag.Text = row.Cells["FinishTag"].Value == null ?"":row.Cells["FinishTag"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByText(cbSFinishStatus, row.Cells["FinishStatus"].Value == null ? "-1" : row.Cells["FinishStatus"].Value.ToString());
            txtRemark.Text = row.Cells["FinishStatus"].Value == null ? "" : row.Cells["Remark"].Value.ToString();
            txtStep.Text = row.Cells["Step"].Value.ToString();
            txtExplanation.Text = row.Cells["Explanation"].Value.ToString();
            iRatio.Text = row.Cells["Ratio"].Value.ToString();
            ID = row.Cells["ID"].Value.ToString();
            CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
            FilePath = row.Cells["FilePath"].Value != null ? row.Cells["FilePath"].Value.ToString() : "";
        }

        /// <summary>
        /// 附件下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Download")
            {
                if (e.GridCell.GridRow.GetCell("FilePath").Value == null)
                {
                    MessageBox.Show("没有文件！");
                    return;
                }
                string fileName = e.GridCell.GridRow.GetCell("FilePath").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.InCome, ProjectId, null, fileName);
            }
        }
        #endregion

        #region 方法
        #endregion
    }
}
