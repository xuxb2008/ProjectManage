using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using BussinessDLL;
using DomainDLL;
using DevComponents.DotNetBar.SuperGrid;
using ProjectManagement.Common;
using CommonDLL;
using System.Threading;
using System.Data;
using ProjectManagement.Control;

namespace ProjectManagement.Forms.Income
{
    /// <summary>
    /// 项目成本管理
    /// Author:ZhuGJ
    /// Created:2017.03.23
    /// </summary>
    public partial class Cost : BaseForm
    {
        #region 业务逻辑类
        private CostBLL bll = new CostBLL();
        #endregion

        #region 变量
        private string ID = null;
        private DateTime CREATED = DateTime.Now;
        #endregion

        #region 事件
        public Cost()
        {
            InitializeComponent();
            
        }

        private void Cost_Load(object sender, EventArgs e)
        {            
            DataBind();           
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            DataTable dt = bll.GetCostList(ProjectId);
            superGridControl1.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ID = null;
            txtExplanation.Clear();
            txtRemaining.Clear();
            txtRemark.Clear();
            txtTag.Clear();
            //清除预算金额，如果不清除的话，txtRemaining的值不会自动计算出来。还注销了txtRemaining.Text = "0";此行代码
            //liuxuexian 2017/6/30
            txtTotal.Clear();
            //结束
            var amount = GetAmount();
            txtTotal.Text = amount < 0 ? "0" : amount.ToString();
            txtTransit.Text = "0";
            txtUsed.Text = "0";
            //txtRemaining.Text = "0";
            superGridControl1.PrimaryGrid.ClearSelectedRows();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }            
            try
            {
                var total = Convert.ToDecimal(txtTotal.Text.ToString());
                var used = Convert.ToDecimal(txtUsed.Text.ToString());
                var transit = Convert.ToDecimal(txtTransit.Text.ToString());
                if (total < 0 || used < 0 || transit < 0)
                {
                    MessageBox.Show("输入金额不能低于0！");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("输入的金额不符合规范！");
                return;
            }
            //判断一下是修改还是新增，如果是修改，则不执行下面的代码
            //liuxuexian 2017/6/30
            #region
            if (ID.IsNullOrEmpty())
            {
                decimal amount = GetAmount();
                amount = amount - Convert.ToDecimal(txtTotal.Text.ToString());
                if (amount < 0)
                {
                    MessageBox.Show("超出合同金额");
                    return;
                }
            }
            #endregion
            #endregion

            DomainDLL.Cost cost = new DomainDLL.Cost();
            cost.ID = ID;
            cost.CREATED = CREATED;
            cost.Explanation = txtExplanation.Text.ToString();
            cost.Remaining = Convert.ToDecimal(string.IsNullOrEmpty(txtRemaining.Text.ToString()) ? "0" : txtRemaining.Text.ToString());
            cost.Remark = txtRemark.Text.ToString();
            cost.Tag = txtTag.Text.ToString();
            cost.Total = Convert.ToDecimal(string.IsNullOrEmpty(txtTotal.Text.ToString()) ? "0" : txtTotal.Text.ToString());
            cost.Transit = Convert.ToDecimal(string.IsNullOrEmpty(txtTransit.Text.ToString()) ? "0" : txtTransit.Text.ToString());
            cost.Used = Convert.ToDecimal(string.IsNullOrEmpty(txtUsed.Text.ToString())?"0":txtUsed.Text.ToString());
            cost.PID = ProjectId;
            cost.UPDATED = DateTime.Now;

            JsonResult json = bll.SaveCost(cost);
            if (!json.result)
                MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                ClearButton_Click(null, null);
            DataBind();
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

            txtExplanation.Text = row.Cells["Explanation"].Value == null ? "" : row.Cells["Explanation"].Value.ToString();
            txtRemaining.Text = row.Cells["Remaining"].Value.ToString();
            txtRemark.Text = row.Cells["Remark"].Value == null ? "" : row.Cells["Remark"].Value.ToString();
            txtTag.Text = row.Cells["Tag"].Value.ToString();
            txtTotal.Text = row.Cells["Total"].Value.ToString();
            txtTransit.Text = row.Cells["Transit"].Value.ToString();
            txtUsed.Text = row.Cells["Used"].Value.ToString();
            ID = row.Cells["ID"].Value.ToString();
            CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
        }

        /// <summary>
        /// 可用金额文本框值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var total = string.IsNullOrEmpty(txtTotal.Text.ToString()) ? "0" : txtTotal.Text.ToString();
                var transit = string.IsNullOrEmpty(txtTransit.Text.ToString()) ? "0" : txtTransit.Text.ToString();
                var used = string.IsNullOrEmpty(txtUsed.Text.ToString()) ? "0" : txtUsed.Text.ToString();
                txtRemaining.Text = (Convert.ToInt32(total) - Convert.ToInt32(transit) - Convert.ToInt32(used)).ToString();
            }
            catch
            {
                MessageBox.Show("输入的格式不正确！");
                txtTotal.Clear();
            }
        }

        /// <summary>
        /// 已用金额文本框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsed_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var total = string.IsNullOrEmpty(txtTotal.Text.ToString()) ? "0" : txtTotal.Text.ToString();
                var transit = string.IsNullOrEmpty(txtTransit.Text.ToString()) ? "0" : txtTransit.Text.ToString();
                var used = string.IsNullOrEmpty(txtUsed.Text.ToString()) ? "0" : txtUsed.Text.ToString();
                txtRemaining.Text = (Convert.ToInt32(total) - Convert.ToInt32(transit) - Convert.ToInt32(used)).ToString();
            }
            catch
            {
                MessageBox.Show("输入的格式不正确！");
                txtUsed.Clear();
            }
        }

        /// <summary>
        /// 在途金额文本况改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTransit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var total = string.IsNullOrEmpty(txtTotal.Text.ToString()) ? "0" : txtTotal.Text.ToString();
                var transit = string.IsNullOrEmpty(txtTransit.Text.ToString()) ? "0" : txtTransit.Text.ToString();
                var used = string.IsNullOrEmpty(txtUsed.Text.ToString()) ? "0" : txtUsed.Text.ToString();
                txtRemaining.Text = (Convert.ToInt32(total) - Convert.ToInt32(transit) - Convert.ToInt32(used)).ToString();
            }
            catch
            {
                MessageBox.Show("输入的格式不正确！");
                txtTransit.Clear();
            }
        }

        /// <summary>
        /// 计算预算金额剩余金额
        /// 2017/6/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            decimal amount = GetAmount();
            txtTotal.Text = amount <= 0 ? "0" : amount.ToString();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 获取剩余的预算资金总额
        /// 2017/6/12(zhuguanjun)
        /// </summary>
        /// <returns></returns>
        private decimal GetAmount()
        {
            decimal amount = 0;
            ContractJBXX jbxx = new ProjectInfoBLL().GetJBXX(ProjectId);
            if (!string.IsNullOrEmpty(jbxx.Amount))
            {
                decimal.TryParse(jbxx.Amount,out amount);
            }
            if (superGridControl1.PrimaryGrid.Rows != null && superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                foreach (var item in superGridControl1.PrimaryGrid.Rows)
                {
                    GridRow row = (GridRow)item;
                    string amountstr = row.Cells["Total"].Value == null ? "" : row.Cells["Total"].Value.ToString();
                    if (!string.IsNullOrEmpty(amountstr))
                    {
                        decimal temp = 0;
                        decimal.TryParse(amountstr, out temp);
                        amount = amount - temp;
                    }
                }                
            }
            return amount;
        }
        #endregion

    }
}
