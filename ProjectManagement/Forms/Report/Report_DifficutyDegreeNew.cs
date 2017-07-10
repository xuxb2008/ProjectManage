using BussinessDLL;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.Editors;
using ProjectManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManagement.Forms.Report
{
    public partial class Report_DifficutyDegreeNew : BaseForm
    {
        #region 业务初始化
        ReportDifficutyDegreeBLL bll = new ReportDifficutyDegreeBLL();
        DomainDLL.Project project = new DomainDLL.Project();
        #endregion

        #region 变量
        DataTable dt = new DataTable();
        #endregion
        public Report_DifficutyDegreeNew()
        {
            InitializeComponent();           
            DataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            dt = bll.GetDefficutyDegree(ProjectId, DateTime.MinValue, DateTime.MinValue, 0);
            this.superGridControl1.PrimaryGrid.DataSource = dt;
            project = bll.GetProject(ProjectId);
        }


        /// <summary>
        /// 设置背景色
        /// 2017/06/09(zhuguanjun)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static CellVisualStyles MatchRowColor(int? status)
        {
            CellVisualStyles style = new CellVisualStyles();
            switch (status)
            {
                case -1:
                    style.Default.Background.Color1 = Color.LightGray;
                    break;
                case 0:
                    style.Default.Background.Color1 = Color.LightGray;
                    break;
                default:
                    style.Default.Background.Color1 = Color.White;
                    break;
            }
            return style;
        }

        /// <summary>
        /// 数据绑定完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listRow = superGridControl1.PrimaryGrid.Rows.ToList();
            int type = 0;
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement obj in listRow)
            {
                DevComponents.DotNetBar.SuperGrid.GridRow row = (DevComponents.DotNetBar.SuperGrid.GridRow)obj;
                type = int.Parse(row.GetCell("type").Value.ToString());
                row.CellStyles = MatchRowColor(type);
                type = 0;
            }
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "工作难度系数";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = saveFileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消

            dynamic xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            ExcelHelper excel = new ExcelHelper(saveFileName);//创建excel

            #region 标题信息
            excel.SetCells(1, 1, "编号");
            excel.SetCells(1, 2, "来源");
            excel.SetCells(1, 3, "名称");
            excel.SetCells(1, 4, "描述");
            excel.SetCells(1, 5, "开始时间");
            excel.SetCells(1, 6, "结束时间");
            excel.SetCells(1, 7, "预计工作量");
            excel.SetCells(1, 8, "实际工作量");
            excel.SetCells(1, 9, "难度系数");
            excel.SetCellsBackColor(1, 1, 1, 9, ColorIndex.灰色25);//设置颜色
            excel.SetCellsAlign(1, 1, 1, 9, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);//设置对齐
            #endregion

            #region 项目计划
            List<string> columns;
            columns = new List<string>() { "RowNo", "source", "name", "desc", "startdate", "enddate", "workload", "actualworkload","degree" };
            Export(dt, excel, columns);
            #endregion

            #region 设置单元格格式
            //先设置单元格居右
            excel.SetCellsAlign(2, 1, dt.Rows.Count + 1, 9, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);
            int first = 1;
            //foreach (DataRow item in dt.Rows)
            //{
            //    rowno++;
            //    if (item["type"].ToString() == "-1")
            //    {
            //        last = rowno - 2;
            //        if (last > 2)
            //            excel.SetCellsBorder(first, 1, last, 8);//设置边框
            //        first = rowno;
            //        excel.SetCellsStyle(rowno, 1, rowno, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, true, ColorIndex.灰色25);//设置
            //        excel.SetCellsStyle(rowno + 1, 1, rowno + 1, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, false, ColorIndex.灰色25);//设置
            //    }

            //}
            excel.SetCellsBorder(first, 1, dt.Rows.Count, 9);//设置边框
            excel.SetCellsBorder(dt.Rows.Count, 8, dt.Rows.Count + 1, 9);//设置边框
            excel.InsertRows(1, 2);
            excel.MergeCells(1, 1, 2, 9, project.Name+"—工作难度系数");//项目名称
            excel.SetCellsStyle(1, 1, 1, 1, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, true, ColorIndex.无色);//项目名称粗体
            #endregion


            excel.SaveAsFile();//文件保存
            MessageBox.Show("操作成功！");
        }

        /// <summary>
        /// 模板数据格式化导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="excel"></param>
        /// <param name="ColumnNames"></param>
        private void Export(DataTable dt, ExcelHelper excel, List<string> ColumnNames)
        {
            if (dt != null)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    for (int s = 1; s <= ColumnNames.Count; s++)
                        excel.SetCells(i + 1, s, dt.Rows[i - 1][ColumnNames[s - 1]].ToString());
                }
            }
        }
    }
}
