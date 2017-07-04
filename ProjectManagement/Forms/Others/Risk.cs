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
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using DevComponents.DotNetBar.Controls;
using DevComponents.AdvTree;

namespace ProjectManagement.Forms.Others
{
    /// <summary>
    /// 风险管理
    /// 2017/4/5(zhuguanjun)
    /// </summary>
    public partial class Risk : BaseForm
    {
        #region 业务类初始化
        RiskBLL bll = new RiskBLL();
        #endregion

        #region 变量
        private string ID = null;
        private DateTime CREATED = DateTime.MinValue;
        #endregion

        #region 事件
        public Risk()
        {
            InitializeComponent();
            dtiAccessDesc.Value = DateTime.Today;
            dtiFindDate.Value = DateTime.Today;
            dtiHandleDate.Value = DateTime.Today;
        }

        /// <summary>
        /// Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Risk_Load(object sender, EventArgs e)
        {
            DataBind(null, null);
            pagerControl1.OnPageChanged += new EventHandler(DataBind);
            //加载结点下拉列表
            DataHelper.SetComboxTreeData(this.cmbtSource, ProjectId);
            //加载结点下拉列表 （这个不支持多选）
            //DataHelper.SetComboxTreeData(this.cmbtDepenency, ProjectId);
            DataHelper.SetAdvTreeData(this.advTree1, ProjectId, 0, null);
            //加载等级下拉列表
            DataHelper.LoadDictItems(cmbLevel, DictCategory.Level);
            //加载策略下拉列表
            DataHelper.LoadDictItems(cmbHandleType, DictCategory.HandType);
            //加载概率下拉列表
            DataHelper.LoadDictItems(cmbProbability, DictCategory.Probability);                       
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBind(object sender, EventArgs e)
        {
            GridData gd = bll.GetGridData(pagerControl1.PageIndex, this.pagerControl1.PageSize, ProjectId);
            superGridControl1.PrimaryGrid.DataSource = gd.data;
            pagerControl1.DrawControl(gd.count);
            //this.treeListLookUpEdit1TreeList.DataSource = gd.data;
            //this.treeListLookUpEdit1TreeList.OptionsView.ShowCheckBoxes = true;
        }

        /// <summary>
        /// 识别风险点击保存
        /// Updated：20170414（Xuxb）保存后刷新首页风险图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave1_Click(object sender, EventArgs e)
        {
            DomainDLL.Risk risk = bll.Get(ID);
            risk.Source = cmbtSource.SelectedNode.Name;
            risk.Name = txtName.Text.ToString();
            risk.Desc = txtDesc.Text.ToString();
            risk.FindDate = dtiFindDate.Value;
            risk.ID = ID;
            risk.PID = ProjectId;
            JsonResult json = bll.SaveRisk(risk, out ID);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClear1_Click(null, null);
            DataBind(null, null);

            //重新加载首页的风险列表
            startPage.LoadProjectRisk();
        }

        /// <summary>
        /// 评估风险保存按钮点击事件
        /// Updated：20170414（Xuxb）保存后刷新首页风险图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            if (ID == null)
            {
                MessageBox.Show("请先保存识别风险！");
                return;
            }
            DomainDLL.Risk risk = bll.Get(ID);
            ComboItem cbi = (ComboItem)cmbLevel.SelectedItem;
            if (cbi != null)
                risk.Level = Convert.ToInt32(cbi.Value);
            risk.CostTime = txtCostTime.Text.ToString();
            //risk.Dependency = cmbtDepenency.SelectedNode.Name;
            risk.Dependency = txtDependency.Tag.ToString();
            ComboItem cbi1 = (ComboItem)cmbProbability.SelectedItem;
            if (cbi != null)
                risk.Probability = Convert.ToInt32(cbi1.Value);
            risk.AssessDesc = txtAcessDesc.Text.ToString();
            risk.AssessDate = dtiAccessDesc.Value;
            risk.ID = ID;
            JsonResult json = bll.SaveRisk(risk, out ID);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClear2_Click(null, null);
            DataBind(null, null);
            //重新加载首页的风险列表
            startPage.LoadProjectRisk();
        }

        /// <summary>
        /// 应对风险保存按钮
        /// Updated：20170414（Xuxb）保存后刷新首页风险图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave3_Click(object sender, EventArgs e)
        {
            if (ID == null)
            {
                MessageBox.Show("请先保存识别风险！");
                return;
            }
            DomainDLL.Risk risk = bll.Get(ID);
            ComboItem cbi = (ComboItem)cmbHandleType.SelectedItem;
            if (cbi != null)
                risk.HandleType = Convert.ToInt32(cbi.Value);
            risk.HandleDesc = txtHandleDesc.Text.ToString();
            risk.HandleDate = dtiHandleDate.Value;
            JsonResult json = bll.SaveRisk(risk, out ID);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClear3_Click(null, null);
            DataBind(null, null);
            
            //重新加载首页的风险列表
            startPage.LoadProjectRisk();
        }

        /// <summary>
        /// 识别风险清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear1_Click(object sender, EventArgs e)
        {
            ID = null;
            cmbtSource.SelectedIndex = -1;
            txtName.Clear();
            txtDesc.Clear();
            dtiFindDate.Value = DateTime.Today;
        }

        /// <summary>
        /// 评估风险清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtCostTime.Clear();
            cmbtDepenency.SelectedIndex = -1;
            txtDependency.Clear();
            foreach (var node in advTree1.CheckedNodes)
            {
                node.Checked = false;
            }
            cmbLevel.SelectedIndex = -1;
            cmbProbability.SelectedIndex = -1;
            txtAcessDesc.Clear();
            dtiAccessDesc.Value = DateTime.Today;
        }

        /// <summary>
        /// 应对风险清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear3_Click(object sender, EventArgs e)
        {
            cmbHandleType.SelectedIndex = -1;
            txtHandleDesc.Clear();
            dtiHandleDate.Value = DateTime.Today;
        }

        /// <summary>
        /// 选中一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            txtDependency.Clear();
            foreach (var node in advTree1.CheckedNodes)
            {
                node.Checked = false;
            }
            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl1.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            cmbtSource.SelectedIndex = -1;
            cmbtDepenency.SelectedIndex = -1;

            //识别风险
            //来源
            if (!string.IsNullOrEmpty(row.Cells["Source"].Value.ToString()))
                DataHelper.SetTreeSelectByValue(cmbtSource.AdvTree, row.Cells["Source"].Value.ToString());
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtDesc.Text = row.Cells["Desc"].Value.ToString();
            if (!string.IsNullOrEmpty(row.Cells["FindDate"].Value.ToString()))
                dtiFindDate.Value = Convert.ToDateTime(row.Cells["FindDate"].Value.ToString());

            //评估风险
            txtCostTime.Text = row.Cells["CostTime"].Value.ToString();
            string select = string.IsNullOrEmpty(row.Cells["Probability2"].Value.ToString()) ? "0" : row.Cells["Probability2"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByValue(cmbProbability, select);
            string select1 = string.IsNullOrEmpty(row.Cells["Level2"].Value.ToString()) ? "0" : row.Cells["Level2"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByValue(cmbLevel, select1);
            //依赖关系
            if (!string.IsNullOrEmpty(row.Cells["Dependency"].Value.ToString()))
            {
                string[] str = row.Cells["Dependency"].Value.ToString().Split(',');
                DataHelper.SetAdvTreeData(this.advTree1, ProjectId, 0, str);
                string text = "";//显示值
                string vall = "";//实际值
                txtDependency.Clear();
                foreach (dynamic node in advTree1.Nodes)
                {
                    if (node.Checked)
                    {
                        text += node.Text + ",";
                        vall += node.Name + ",";

                    }
                    nodetext(node, text, vall);

                }
                this.txtDependency.Text = text;
                this.txtDependency.Tag = vall;
            }
            //DataHelper.SetTreeSelectByValue(cmbtDepenency.AdvTree, row.Cells["Dependency"].Value.ToString());

            txtAcessDesc.Text = row.Cells["AssessDesc"].Value.ToString();
            if (!string.IsNullOrEmpty(row.Cells["AssessDate"].Value.ToString()))
                dtiAccessDesc.Value = Convert.ToDateTime(row.Cells["AssessDate"].Value.ToString());

            //应对风险
            string select2 = string.IsNullOrEmpty(row.Cells["HandleType2"].Value.ToString()) ? "0" : row.Cells["HandleType2"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByValue(cmbHandleType, select2);
            txtHandleDesc.Text = row.Cells["HandleDesc"].Value.ToString();
            if (!string.IsNullOrEmpty(row.Cells["HandleDate"].Value.ToString()))
                dtiHandleDate.Value = Convert.ToDateTime(row.Cells["HandleDate"].Value.ToString());

            ID = row.Cells["ID"].Value.ToString();
            if (!string.IsNullOrEmpty(row.Cells["CREATED"].Value.ToString()))
                CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
        }
        #endregion

        #region 方法
        /// <summary>
        /// 为下拉树赋值
        /// 2017/05/03(zhuguanjun)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="Value"></param>
        private void SetCMBT(ComboTree tree, string Value)
        {
            foreach (Node item in tree.Nodes)
            {
                if (item.Name == Value)
                {
                    item.Checked = true;
                }
                item.Checked = true;
            }
        }

        #endregion

        /// <summary>
        /// 依赖关系文本框点击与取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDependency_Click(object sender, EventArgs e)
        {
            advTree1.Visible = !advTree1.Visible;
        }

        /// <summary>
        /// node点击
        /// 2015/05/22(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advTree1_AfterCheck(object sender, AdvTreeCellEventArgs e)
        {
            string text = "";//显示值
            string vall = "";//实际值
            txtDependency.Clear();
            foreach (dynamic node in advTree1.Nodes)
            {
                if (node.Checked)
                {
                    text += node.Text + ",";
                    vall += node.Name + ",";

                }
                nodetext(node,  text,  vall);
                 
            }
            this.txtDependency.Text = text;
            this.txtDependency.Tag = vall;
        }

        private void nodetext(dynamic pnode,  string text, string vall)
        {
            var nodes = pnode.Nodes;
            foreach (dynamic node in nodes)
            {
                if (node.Checked)
                {
                    text += node.Text + ",";
                    vall += node.Name + ",";
                }
                nodetext(node,  text,  vall);
            }
        }


        /// <summary>
        /// panel点击隐藏node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupPanel3_Click(object sender, EventArgs e)
        {
            this.advTree1.Visible = false;
        }

        /// <summary>
        /// panel点击隐藏node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupPanel4_Click(object sender, EventArgs e)
        {
            this.advTree1.Visible = false;
        }

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            BindComplete();
        }

        private void BindComplete()
        {
            foreach (var item in superGridControl1.PrimaryGrid.Rows)
            {
                var str = ((GridRow)item).Cells["Dependency"].Value.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    string[] strlist = str.Split(',');
                    List<PNode> listNode = new WBSBLL().GetNodes(ProjectId, 0);
                    foreach (var node in listNode)
                    {
                        if (strlist.Contains(node.ID))
                        {
                            ((GridRow)item)["DependencyName"].Value += node.Name + ",";
                        }
                    }
                }
            }
        }
    }
}
