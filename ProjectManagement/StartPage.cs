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
using System.Windows.Forms.DataVisualization.Charting;
using DomainDLL;

namespace ProjectManagement
{
    /// <summary>
    /// 画面名：首页
    /// Created：2017.03.29(Xuxb)
    /// </summary>
    public partial class StartPage : BaseForm
    {
        #region 业务类初期化

        //初始化项目业务BLL
        ProjectInfoBLL projectBll = new ProjectInfoBLL();
        //初始化成本业务BLL
        CostBLL costBll = new CostBLL();

        #endregion

        #region 事件

        /// <summary>
        /// 画面初期化
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public StartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面加载时
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPage_Load(object sender, EventArgs e)
        {
            //加载项目成果
            LoadProjectResult();

            //加载项目成本
            LoadProjectCost();

            //加载项目风险
            LoadProjectRisk();

            //加载项目问题
            LoadProjectTrouble();

            //加载项目预警信息
            LoadProjectWarning();

            //加载项目问题一览
            LoadProjectTroubleList();
        }

        /// <summary>
        /// 点击近期工作和问题一览时
        /// Created：2017.03.29(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridLastWork_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Operate")
            {
                string WorkType = e.GridCell.GridRow.Cells["WorkType"].Value.ToString();
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;

                if (WorkType == "日常工作")
                {
                    Forms.Others.Routine form = new Forms.Others.Routine("");
                    form.WorkId = e.GridCell.GridRow.Cells["Id"].Value.ToString();
                    mainForm.ShowChildForm(form);
                }
                else
                {
                    Forms.Others.Trouble form = new Forms.Others.Trouble("");
                    form.TroubleId = e.GridCell.GridRow.Cells["Id"].Value.ToString();
                    mainForm.ShowChildForm(form);
                }

            }
        }

        /// <summary>
        /// 双击近期工作和问题一览时
        /// Created：20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridLastWork_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            GridCell cell = e.GridRow.SuperGrid.GetCell(e.GridRow.RowIndex, 5);
            if (cell != null)
            {
                string WorkType = e.GridRow.SuperGrid.GetCell(e.GridRow.RowIndex, 1).Value.ToString();
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;

                if (WorkType == "日常工作")
                {
                    Forms.Others.Routine form = new Forms.Others.Routine("");
                    form.WorkId = cell.Value.ToString();
                    mainForm.ShowChildForm(form);
                }
                else
                {
                    Forms.Others.Trouble form = new Forms.Others.Trouble("");
                    form.TroubleId = cell.Value.ToString();
                    mainForm.ShowChildForm(form);
                }

            }

        }


        /// <summary>
        /// 按F5时刷新首页
        /// Created：2017.06.07(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //加载项目成果
                LoadProjectResult();

                //加载项目成本
                LoadProjectCost();

                //加载项目风险
                LoadProjectRisk();

                //加载项目问题
                LoadProjectTrouble();

                //加载项目预警信息
                LoadProjectWarning();

                //加载项目问题一览
                LoadProjectTroubleList();
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载项目成果
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectResult()
        {
            List<double> list = new List<double>();
            DataTable dt = projectBll.GetProjectResult(ProjectId);
            if (dt != null && dt.Rows.Count > 0)
            {
                double totalWork = double.Parse(dt.Rows[0]["TotalWork"].ToString());
                double completeWork = double.Parse(dt.Rows[0]["CompleteWork"].ToString());
                //项目总工作量
                list = new List<double>();
                list.Add(completeWork);
                list.Add(totalWork - completeWork);
                chartResult.Series[0].Points.DataBindY(list);
                chartResult.Series[0].Points[0].Label = "已完成(" + completeWork + ")";
                chartResult.Series[0].Points[1].Label = "未完成(" + (totalWork - completeWork) + ")";
                chartResult.Titles[0].Text = "项目成果(总工作量：" + totalWork.ToString() + "人天)";
            }
            else
            {
                list.Add(0);
                chartResult.Series[0].Points.DataBindY(list);
            }
        }

        /// <summary>
        /// 加载项目成本
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectCost()
        {
            DataTable dt = costBll.GetCostList(ProjectId);

            double total = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Column;
                s1.Color = System.Drawing.Color.Blue;
                s1.LegendText = "预算金额";
                Series s2 = new Series();
                s2.ChartType = SeriesChartType.Column;
                s2.Color = System.Drawing.Color.Red;
                s2.LegendText = "已用金额";
                Series s4 = new Series();
                s4.ChartType = SeriesChartType.Column;
                s4.Color = System.Drawing.Color.LightGreen;
                s4.LegendText = "剩余金额";
                Random r = new Random();

                string[] tags = new string[dt.Rows.Count];
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    total = total + double.Parse(dt.Rows[i - 1]["Total"].ToString());
                    s1.Points.AddXY(i, double.Parse(dt.Rows[i - 1]["Total"].ToString()));
                    s1.Points[i - 1].Label = dt.Rows[i - 1]["Total"].ToString();
                    s2.Points.AddXY(i, double.Parse(dt.Rows[i - 1]["Used"].ToString()));
                    s2.Points[i - 1].Label = dt.Rows[i - 1]["Used"].ToString();
                    s4.Points.AddXY(i, double.Parse(dt.Rows[i - 1]["Remaining"].ToString()));
                    s4.Points[i - 1].Label = dt.Rows[i - 1]["Remaining"].ToString();
                    tags[i - 1] = dt.Rows[i - 1]["Tag"].ToString();
                }

                chartCost.Series.Clear();
                chartCost.Series.Add(s1);
                chartCost.Series.Add(s4);
                chartCost.Series.Add(s2);
                chartCost.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartCost.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
                chartCost.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
                chartCost.Titles[0].Text = "项目预算(总金额：" + total.ToString() + "元)";

                int x = 0;
                for (int i = 1; i < dt.Rows.Count * 2; i++)
                {
                    if (i % 2 == 1)
                    {
                        CustomLabel label = new CustomLabel();
                        label.Text = tags[x];
                        label.ToPosition = i + 1;
                        chartCost.ChartAreas[0].AxisX.CustomLabels.Add(label);
                        label.GridTicks = GridTickTypes.None;
                        x++;
                    }

                }
            }
        }

        /// <summary>
        /// 加载项目风险
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectRisk()
        {
            List<double> list = new List<double>();
            DataTable dt = projectBll.GetProjectRisk(ProjectId);
            if (dt != null && dt.Rows.Count > 0)
            {
                //已识别的风险
                double risk = double.Parse(dt.Rows[0]["riskFind"].ToString());
                //已评估的风险
                double risking = double.Parse(dt.Rows[0]["riskAssess"].ToString());
                //已应对的风险
                double risked = double.Parse(dt.Rows[0]["riskHandle"].ToString());
                chartRisk.Series[0].Points[0].YValues = new double[] { risk };
                chartRisk.Series[0].Points[1].YValues = new double[] { risking };
                chartRisk.Series[0].Points[2].YValues = new double[] { risked };
                chartRisk.Titles[0].Text = "项目风险(总风险数：" + risk.ToString() + "个)";
            }
            else
            {
                list.Add(0);
                chartRisk.Series[0].Points.DataBindY(list);
            }
        }

        /// <summary>
        /// 加载项目问题
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectTrouble()
        {
            List<double> list = new List<double>();
            DataTable dt = projectBll.GetProjectTrouble(ProjectId);
            if (dt != null && dt.Rows.Count > 0)
            {
                //总问题数量
                double total = double.Parse(dt.Rows[0]["TroubleTotal"].ToString());
                //已解决数量
                double handle = double.Parse(dt.Rows[0]["TroubleHandle"].ToString());
                //剩余数量
                double leave = double.Parse(dt.Rows[0]["TroubleLeave"].ToString());
                //超期数量
                double rest = double.Parse(dt.Rows[0]["TroubleRest"].ToString());
                chartTrouble.Series[0].Points[0].YValues = new double[] { handle };
                chartTrouble.Series[0].Points[1].YValues = new double[] { leave };
                chartTrouble.Series[0].Points[2].YValues = new double[] { rest };
                chartTrouble.Titles[0].Text = "项目问题(总问题数量：" + total.ToString() + "个)";
            }
            else
            {
                list.Add(0);
                chartTrouble.Series[0].Points.DataBindY(list);
            }
        }

        /// <summary>
        /// 加载项目预警信息
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectWarning()
        {
            DataTable dt = DataHelper.GetWarnningData(ProjectId);
            DataHelper.AddNoCloumn(dt);
            superGridWarning.PrimaryGrid.DataSource = dt;
        }

        /// <summary>
        /// 加载项目问题一览
        /// Created：2017.03.27(Xuxb)
        /// </summary>
        public void LoadProjectTroubleList()
        {
            DataTable dt = projectBll.GetProjectLastWorkList(ProjectId);
            DataHelper.AddNoCloumn(dt);
            superGridLastWork.PrimaryGrid.DataSource = dt;
        }

        #endregion


        /// <summary>
        /// 预警一览 行双击
        /// Created：20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridWarning_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            SuperGridControl obj = (SuperGridControl)sender;
            string WorkType = e.GridRow.SuperGrid.GetCell(e.GridRow.RowIndex, 1).Value.ToString();
            MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
            switch (WorkType)
            {
                case "项目交付物预警":
                    CurrentNode = new WBSBLL().GetNode(e.GridRow.SuperGrid.GetCell(e.GridRow.RowIndex, 4).Value.ToString());
                    mainForm.RelaodTree();
                    mainForm.OpenNormalOperation();
                    break;
                case "项目问题预警":
                    Forms.Others.Trouble form = new Forms.Others.Trouble("");
                    form.TroubleId = e.GridRow.SuperGrid.GetCell(e.GridRow.RowIndex, 4).Value.ToString();
                    mainForm.ShowChildForm(form);
                    break;
            }
        }
        /// <summary>
        /// 预警一览 单元格单击
        /// Created：20170609(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridWarning_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Operate")
            {
                string WorkType = e.GridCell.GridRow.Cells["WarnningName"].Value.ToString();
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                switch (WorkType)
                {
                    case "项目交付物预警":
                        CurrentNode = new WBSBLL().GetNode(e.GridCell.GridRow.Cells["Id"].Value.ToString());
                        mainForm.RelaodTree();
                        mainForm.OpenNormalOperation();
                        break;
                    case "项目问题预警":
                        Forms.Others.Trouble form = new Forms.Others.Trouble("");
                        form.TroubleId = e.GridCell.GridRow.Cells["Id"].Value.ToString();
                        mainForm.ShowChildForm(form);
                        break;
                }

            }
        }



    }
}
