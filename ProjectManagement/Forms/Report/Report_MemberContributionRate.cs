using BussinessDLL;
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
    public partial class Report_MemberContributionRate : BaseForm
    {
        #region 业务初始化
        ReportMemberRateBLL bll = new ReportMemberRateBLL();
        DomainDLL.Project project = new DomainDLL.Project();
        #endregion

        #region 变量
        DataTable dt = new DataTable();
        #endregion

        public Report_MemberContributionRate()
        {
            InitializeComponent();
            DataBind();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind() {
            dt = bll.GetMemberRate(ProjectId);
            this.superGridControl1.PrimaryGrid.DataSource = dt;
            project = bll.GetProject(ProjectId);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "成员贡献率";
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
            //ExcelHelper excel = new ExcelHelper(FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Report_Plan, "", "") + ConstHelper.Report_Plan, saveFileName);
            ExcelHelper excel = new ExcelHelper(saveFileName);//创建excel
            //excel.InsertColumns(1, 8);//创建8列

            #region 项目计划
            List<string> columns;
            columns = new List<string>() { "RowNo", "Source", "name", "desc", "startedate", "enddate", "workload", "zhanbi" };
            Export(dt, excel, columns);
            #endregion

            #region 设置单元格格式
            //先设置单元格居右
            excel.SetCellsAlign(2, 1, dt.Rows.Count + 1, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);
            int rowno = 1;
            int first = 1;
            int last = 1;
            foreach (DataRow item in dt.Rows)
            {
                rowno++;
                if (item["type"].ToString() == "-1")
                {
                    last = rowno - 2;
                    if (last > 2)
                        excel.SetCellsBorder(first, 1, last, 8);//设置边框
                    first = rowno;
                    excel.SetCellsStyle(rowno, 1, rowno, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, true, ColorIndex.灰色25);//设置
                    excel.SetCellsStyle(rowno+1, 1, rowno + 1, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter, false, ColorIndex.灰色25);//设置
                }
                
            }
            excel.SetCellsBorder(first, 1, dt.Rows.Count+1, 8);//设置边框
            excel.InsertRows(1, 1);
            excel.MergeCells(1, 1, 2, 8, project.Name);//项目名称
            excel.DeleteRows(4, 1);
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
