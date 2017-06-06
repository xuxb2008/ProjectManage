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
            SuperGridControl obj = (SuperGridControl)sender;
            if (obj.ActiveCell.ColumnIndex == 4)
            {
                string WorkType = e.GridCell.GridRow.Cells["WorkType"].Value.ToString();
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                
                if (WorkType == "日常工作")
                {
                    Forms.Others.Routine form = new Forms.Others.Routine();
                    form.WorkId = e.GridCell.GridRow.Cells["Id"].Value.ToString();
                    mainForm.ShowChildForm(form);
                }
                else
                {
                    Forms.Others.Trouble form = new Forms.Others.Trouble();
                    form.TroubleId = e.GridCell.GridRow.Cells["Id"].Value.ToString();
                    mainForm.ShowChildForm(form);
                }
                
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
            if(dt != null && dt.Rows.Count > 0){
                double totalWork = double.Parse(dt.Rows[0]["TotalWork"].ToString());
                double completeWork = double.Parse(dt.Rows[0]["CompleteWork"].ToString());
                //项目总工作量
                list = new List<double>();
                list.Add(completeWork);
                list.Add(totalWork - completeWork);
                chartResult.Series[0].Points.DataBindY(list);
                chartResult.Series[0].Points[0].Label = "已完成(" + completeWork + ")";
                chartResult.Series[0].Points[1].Label = "未完成(" + (totalWork - completeWork) + ")";
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

            if (dt != null && dt.Rows.Count > 0)
            {
                Series s1 = new Series();
                s1.ChartType = SeriesChartType.Column;
                s1.Color = System.Drawing.Color.LightGreen;
                s1.LegendText = "可用金额";
                Series s2 = new Series();
                s2.ChartType = SeriesChartType.StackedColumn;
                s2.Color = System.Drawing.Color.Yellow;
                s2.LegendText = "已用金额";
                Series s3 = new Series();
                s3.ChartType = SeriesChartType.StackedColumn;
                s3.Color = System.Drawing.Color.LightSalmon;
                s3.LegendText = "在途金额";
                Series s4 = new Series();
                s4.ChartType = SeriesChartType.Column;
                s4.Color = System.Drawing.Color.LightSkyBlue;
                s4.LegendText = "剩余金额";
                Random r = new Random();
                int y = 0;
                for (int i = 1; i < 12; i = i + 2)
                {
                    if (dt.Rows.Count > y)
                    {
                        //可用金额
                        if (dt.Rows[y]["Total"] != null && !string.IsNullOrEmpty(dt.Rows[y]["Total"].ToString()))
                        {
                            s1.Points.AddXY(i, double.Parse(dt.Rows[y]["Total"].ToString()), double.Parse(dt.Rows[y]["Total"].ToString()));
                            if (double.Parse(dt.Rows[y]["Total"].ToString()) > 0)
                            {
                                s1.Points[y].Label = dt.Rows[y]["Total"].ToString();
                            }
                        }

                        //剩余金额
                        if (dt.Rows[y]["Remaining"] != null && !string.IsNullOrEmpty(dt.Rows[y]["Remaining"].ToString()))
                        {

                            if (double.Parse(dt.Rows[y]["Remaining"].ToString()) > 0)
                            {
                                s4.Points.AddXY(i + 0.4, double.Parse(dt.Rows[y]["Remaining"].ToString()));
                                s4.Points[y].Label = dt.Rows[y]["Remaining"].ToString();
                            }
                            else
                            {
                                s4.Points.AddXY(i + 0.4, 0);
                            }
                        }

                        //已用金额
                        if (dt.Rows[y]["Used"] != null && !string.IsNullOrEmpty(dt.Rows[y]["Used"].ToString()))
                        {
                            s2.Points.AddXY(i + 0.8, double.Parse(dt.Rows[y]["Used"].ToString()));
                            if (double.Parse(dt.Rows[y]["Used"].ToString()) > 0)
                            {
                                s2.Points[y].Label = dt.Rows[y]["Used"].ToString();
                            }
                        }
                        //在途金额
                        if (dt.Rows[y]["Transit"] != null && !string.IsNullOrEmpty(dt.Rows[y]["Transit"].ToString()))
                        {
                            s3.Points.AddXY(i + 0.8, double.Parse(dt.Rows[y]["Transit"].ToString()));
                            if (double.Parse(dt.Rows[y]["Transit"].ToString()) > 0)
                            {
                                s3.Points[y].Label = dt.Rows[y]["Transit"].ToString();
                            }
                        }
                    }
                    y++;
                }

                chartCost.Series.Add(s1);
                chartCost.Series.Add(s4);
                chartCost.Series.Add(s2);
                chartCost.Series.Add(s3);
                chartCost.Series[0]["PointWidth"] = "0.4";
                chartCost.Series[1]["PointWidth"] = "0.4";
                chartCost.Series[2]["PointWidth"] = "0.2";
                chartCost.Series[3]["PointWidth"] = "0.2";
                chartCost.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartCost.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
                chartCost.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;

                int x = 1;
                for (int i = 1; i < 25; i++)
                {
                    if (i % 4 == 1)
                    {
                        CustomLabel label = new CustomLabel();
                        label.Text = "成本" + x;
                        label.ToPosition = i + 2;
                        chartCost.ChartAreas[0].AxisX.CustomLabels.Add(label);
                        label.GridTicks = GridTickTypes.None;
                        x = x + 1;
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
                chartTrouble.Series[0].Points[0].YValues = new double[] { total };
                chartTrouble.Series[0].Points[1].YValues = new double[] { handle };
                chartTrouble.Series[0].Points[2].YValues = new double[] { leave };
                chartTrouble.Series[0].Points[3].YValues = new double[] { rest };
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
    }
}
