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
using DevComponents.Editors;
using CommonDLL;
using DevComponents.DotNetBar.Controls;
using ProjectManagement.Common;

namespace ProjectManagement.Forms.Report
{
    public partial class Report_Plan : BaseForm
    {
        #region 业务逻辑初始化
        ReportPlanBLL bll = new ReportPlanBLL();
        #endregion

        #region 变量
        DataTable dt =new DataTable();
        #endregion

        #region 事件
        /// <summary>
        /// 构造方法
        /// </summary>
        public Report_Plan()
        {
            InitializeComponent();
            //完成状态下拉框
            DataHelper.LoadDictItems(cmbFinishStatus, DictCategory.PlanFinishStatus);
            cmbFinishStatus.SelectedIndex = 0;
            //负责人下拉框
            LoadStakeholderItems(cmbManager, null);
            //DataHelper.LoadDictItems(cmbManager, DictCategory.WBS_Manager);//责任人列表加载
            BindData();
        }

        /// <summary>
        /// 生成计划
        /// 2017/04/21(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 导出计划
        /// 2017/04/21(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "项目计划";
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
            excel.InsertColumns(1, 8);//创建8列
            #region 标题信息
            excel.SetCells(1, 1, "编号");
            excel.SetCells(1, 2, "WBS代码");
            excel.SetCells(1, 3, "任务名称");
            excel.SetCells(1, 4, "工作量");
            excel.SetCells(1, 5, "开始时间");
            excel.SetCells(1, 6, "结束时间");
            excel.SetCells(1, 7, "完成比例");
            excel.SetCells(1, 8, "负责人");
            excel.SetCellsBackColor(1, 1, 1, 8, ColorIndex.灰色25);//设置颜色
            excel.SetCellsAlign(1, 1, 1, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);//设置颜色
            #endregion

            #region 项目计划
            List<string> columns;
            columns = new List<string>() { "RowNo", "WBSNo", "Name", "Workload", "StarteDate", "EndDate", "Progress", "Manager" };
            Export(dt, excel, columns);
            #endregion

            #region 设置单元格居中
            //编号居中
            excel.SetCellsAlign(2, 1, dt.Rows.Count + 1, 1, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //代码/名称居左
            excel.SetCellsAlign(2, 2, dt.Rows.Count + 1, 3, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft);
            //其他的都居右
            excel.SetCellsAlign(2, 4, dt.Rows.Count + 1, 8, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);
            #endregion


            excel.SaveAsFile();//文件保存
            MessageBox.Show("操作成功！");
        }
        #endregion 

        #region 方法

        /// <summary>
        /// 绑定数据
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        private void BindData()
        {
            //完成比例
            int PType = 0;
            ComboItem item = (ComboItem)cmbFinishStatus.SelectedItem;
            if (item != null)
                PType = int.Parse(item.Value.ToString());

            //负责人
            string Manager = string.Empty;
            ComboItem manageritem = (ComboItem)cmbManager.SelectedItem;
            if (manageritem != null)
                Manager = manageritem.Value.ToString();

            dt = bll.GetPlan(dtiStartDate.Value, dtiEndDate.Value, PType, Manager,ProjectId);
            //superGridControl1.PrimaryGrid.DataSource = dt;
            this.treeList1.DataSource = dt;
            treeList1.KeyFieldName = "KeyFieldName"; ;
            treeList1.ParentFieldName = "ParentFieldName";
            treeList1.ExpandAll();
        }

        /// <summary>
        /// 模板数据格式化导入
        /// Created:2017.04.24(zhuguanjun)
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

        /// <summary>
        /// 加载负责人下拉框
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="Value"></param>
        private void LoadStakeholderItems(ComboBoxEx combobox, string Value)
        {
            var list = bll.GetStakeholderItems(ProjectId);
            ComboItem itemnull = new ComboItem();
            itemnull.Text = "请选择";
            itemnull.Value = string.Empty;
            combobox.Items.Add(itemnull);
            foreach (DomainDLL.Stakeholders c in list)
            {
                ComboItem item = new ComboItem();
                item.Text = c.Name;
                item.Value = c.ID;
                combobox.Items.Add(item);
            }
            combobox.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 清空按钮
        /// 2017/06/14(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtClear_Click(object sender, EventArgs e)
        {
            dtiStartDate.Value = DateTime.MinValue;
            dtiEndDate.Value = DateTime.MinValue;
            cmbFinishStatus.SelectedIndex = 0;
            cmbManager.SelectedIndex = 0;
        }
    }
}
