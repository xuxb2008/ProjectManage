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
using CommonDLL;

namespace ProjectManagement.Forms.Report
{
    

    public partial class Report_Earning : BaseForm
    {
        #region 业务初始化
        ReportEarningBLL bll = new ReportEarningBLL();
        DataTable dt = new DataTable();
        #endregion

        /// <summary>
        /// 构造方法
        /// 2017/04/25(zhuguanjun)
        /// </summary>
        public Report_Earning()
        {
            InitializeComponent();
            ProjectBind();
        }

        /// <summary>
        /// 绑定项目列表数据
        /// 2017/04/25(zhuguanjun)
        /// </summary>
        private void ProjectBind()
        {
            List<DomainDLL.Project> list = bll.GetProject();
            checkedListBox1.DataSource = list;
            checkedListBox1.DisplayMember = "Name";
            checkedListBox1.ValueMember = "ID";
        }

        /// <summary>
        /// 绑定收入列表数据
        /// </summary>
        private void EarningBind(List<string> pids)
        {
            dt = bll.GetEarning(pids);
            this.treeList1.DataSource = dt;
            treeList1.KeyFieldName = "KeyFieldName"; ;
            treeList1.ParentFieldName = "ParentFieldName";
        }

        /// <summary>
        /// 全选
        /// 2017/04/25(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// 取消全选
        /// 2017/04/25(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Click(object sender, EventArgs e)
        {
            List<string> pids = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
            {

                pids.Add(((DomainDLL.Project)item).ID);
            }
            EarningBind(pids);
        }

        /// <summary>
        /// 导出
        /// 2017/04/27(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "收入情况";
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
            ExcelHelper excel = new ExcelHelper(FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Report_Earning, "", "") + ConstHelper.Report_Earning, saveFileName);

            #region 标题信息
            excel.SetCells(1, 1, "项目阶段");
            excel.SetCells(1, 2, "完成比例");
            excel.SetCells(1, 3, "完成标志");
            excel.SetCells(1, 4, "完成情况");
            excel.SetCells(1, 5, "备注");
            excel.SetCellsBackColor(1, 1, 1, 5, ColorIndex.灰色25);//设置颜色
            excel.SetCellsAlign(1, 1, 1, 5, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);//设置对齐
            #endregion

            #region 项目计划
            List<string> columns;
            columns = new List<string>() { "Step", "Ratio", "FinishTag", "FinishStatus", "Remark" };
            Export(dt, excel, columns);
            #endregion

            #region 设置单元格居中
            //编号居中
            excel.SetCellsAlign(2, 1, dt.Rows.Count + 1, 1, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //代码/名称居右
            excel.SetCellsAlign(2, 2, dt.Rows.Count + 1, 3, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);
            //其他的都居左
            excel.SetCellsAlign(2, 4, dt.Rows.Count + 1, 5, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft);
            #endregion


            excel.SaveAsFile();//文件保存
            MessageBox.Show("操作成功！");
        }

        /// <summary>
        /// 模板数据格式化导入
        /// Created:2017.04.27(zhuguanjun)
        /// </summary>
        /// <param name="dt">数据表格</param>
        /// <param name="excel"></param>
        /// <param name="ColumnNames">列名集合</param>
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
