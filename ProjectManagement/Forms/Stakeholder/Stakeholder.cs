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
using ProjectManagement.Common;
using DevComponents.DotNetBar.SuperGrid;
using CommonDLL;
using DomainDLL;
using DevComponents.Editors;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace ProjectManagement.Forms.Stakeholder
{

    /// <summary>
    /// author:zhuguanjun
    /// at:2017/03/27
    /// </summary>
    public partial class Stakeholder : BaseForm
    {
        #region 业务逻辑类
        private StakeholdersBLL bll = new StakeholdersBLL();
        #endregion

        #region 变量
        private string ID = null;
        private DateTime CREATED = DateTime.Now;
        
        #endregion

        #region 事件
        public Stakeholder()
        {
            InitializeComponent();
            DataBind(null,null);
            dtiCreated.Value = DateTime.Now;
            LoadDropList();
            DataHelper.LoadDictItems(cmbType, DictCategory.StakehoderType); //加载干系人类型
            pagerControl1.OnPageChanged += new EventHandler(DataBind);
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind(object sender, EventArgs e)
        {
            GridData list = bll.GetGridData(pagerControl1.PageSize, pagerControl1.PageIndex, ProjectId);
            superGridControl1.PrimaryGrid.DataSource = list.data;
            pagerControl1.DrawControl(list.count);
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDuty.Clear();
            txtEmail.Clear();
            txtName.Clear();
            txtPosition.Clear();
            txtQQ.Clear();
            txtTel.Clear();
            txtWechat.Clear();
            dtiCreated.Value = DateTime.Now;
            cbIspublic.CheckValue = false;
            cmbSendType.SelectedIndex = -1;
            ID = null;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(txtName.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "姓名");
                return;
            }
            int flag = 0;//没有选项目经理
            string flagid = string.Empty;//项目经理id
            if (superGridControl1.PrimaryGrid.Rows.Count != 0)
            {
                foreach (var item in superGridControl1.PrimaryGrid.Rows)
                {
                    string s = item.ToString();
                    s = s.Replace("{", ",");
                    s = s.Replace("}", ",");
                    string[] listS = s.Split(',');
                    if (int.Parse(listS[13].Trim()) != 0) {
                        flag = 1;
                        flagid = listS[18].Trim();
                    }
                }
            }
            if (flag != 0 && cbIspublic.Checked && flagid !=ID)
            {
                MessageBox.Show("不能存在多个项目经理");
                return;
            }
            #endregion

            Stakeholders stakeholders = new Stakeholders();
            stakeholders.CompanyName = txtCompanyName.Text.ToString();
            stakeholders.Duty = txtDuty.Text.ToString();
            stakeholders.Email = txtEmail.Text.ToString();
            stakeholders.Position = txtPosition.Text.ToString();
            stakeholders.QQ = txtQQ.Text.ToString();
            stakeholders.Tel = txtTel.Text.ToString();
            stakeholders.Wechat = txtWechat.Text.ToString();
            //分类
            ComboItem cbi = (ComboItem)cmbType.SelectedItem;
            if (cbi != null)
                stakeholders.Type = Convert.ToInt32(cbi.Value);
            stakeholders.Name = txtName.Text.ToString();
            //项目经理
            stakeholders.IsPublic = cbIspublic.Checked ? 1 : 0;
            //发送方式
            ComboItem cbisend = (ComboItem)cmbSendType.SelectedItem;
            if (cbisend != null)
                stakeholders.SendType = Convert.ToInt32(cbisend.Value);
            stakeholders.Name = txtName.Text.ToString();
            stakeholders.CREATED = CREATED;
            stakeholders.ID = ID;
            stakeholders.PID = ProjectId;

            string _id ="";
            JsonResult json = bll.SaveStakehoders(stakeholders,out _id);
            //失败提示
            if (!json.result)
                MessageHelper.ShowRstMsg(json.result);
            btnClear_Click(null, null);
            DataBind(null,null);
        }

        /// <summary>
        /// 选中一行
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
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtCompanyName.Text = row.Cells["CompanyName"].Value.ToString();
            txtDuty.Text = row.Cells["Duty"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtPosition.Text = row.Cells["Position"].Value.ToString();
            txtQQ.Text = row.Cells["QQ"].Value.ToString();
            txtTel.Text = row.Cells["Tel"].Value.ToString();
            txtWechat.Text = row.Cells["Wechat"].Value.ToString();
            cmbSendType.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            string select = string.IsNullOrEmpty(row.Cells["Type"].Value.ToString()) ? "0" : row.Cells["Type"].Value.ToString();
            string select1 = string.IsNullOrEmpty(row.Cells["SendType"].Value.ToString()) ? "0" : row.Cells["SendType"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByValue(cmbType, select);
            DataHelper.SetComboBoxSelectItemByValue(cmbSendType, select1);
            cbIspublic.CheckValue = Convert.ToInt32(string.IsNullOrEmpty(row.Cells["IsPublic"].Value.ToString()) ? "0" : row.Cells["IsPublic"].Value.ToString());
            ID = row.Cells["ID"].Value.ToString();
            dtiCreated.Value = string.IsNullOrEmpty(ID) ? DateTime.Now : Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
            CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载下拉框
        /// </summary>
        private void LoadDropList()
        {
            //updated2017/05/25 because of the 'projectteam' is cost need
            //类型下拉框
            //DataHelper.LoadDictItems(cmbType, DictCategory.StakehoderType);
            //cmbType.SelectedIndex = 0;

            //发送类型下拉框
            DataHelper.LoadDictItems(cmbSendType, DictCategory.SendType);
            cmbType.SelectedIndex = -1;
        }
        #endregion

        /// <summary>
        /// 数据绑定完成设置项目经理背景色
        /// 2017/06/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            List<GridElement> listRow = superGridControl1.PrimaryGrid.Rows.ToList();
            int type = 0;
            foreach (GridElement obj in listRow)
            {
                GridRow row = (GridRow)obj;
                type = int.Parse(row.GetCell("IsPublic").Value.ToString());
                if (type != 0)
                {
                    CellVisualStyles style = new CellVisualStyles();
                    style.Default.Background.Color1 = Color.CornflowerBlue;
                    row.CellStyles = style;
                }
            }
        }

    }
}
