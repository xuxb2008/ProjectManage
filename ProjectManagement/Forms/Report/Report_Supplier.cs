using BussinessDLL;
using CommonDLL;
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
    public partial class Report_Supplier : Common.BaseForm
    {
        #region 业务逻辑初始化
        ReportSupplierBLL bll = new ReportSupplierBLL();
        #endregion
        #region 变量
        DataTable dt = new DataTable();
        //需要显示的字段集合
        Dictionary<string, string> Settings = new Dictionary<string, string>();
        #endregion
        public Report_Supplier()
        {
            InitializeComponent();
            ProjectBind();
            Init();
        }

        /// <summary>
        /// 全选
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
        /// 取消
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
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Report_Supplier_Setting form = new Report_Supplier_Setting(Settings);
            form.settingdelegate += SetDic;
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Close();
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
            SupplierBind(pids);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "供应商信息报表";
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
            ExcelHelper excel = new ExcelHelper(FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Report_Dynamic, "", "") + ConstHelper.Report_Dynamic, saveFileName);
            #region 初始化
            List<string> columns;
            columns = Settings.Keys.ToList<string>();
            //动态的生成列个数
            excel.InsertColumns(1, columns.Count - 1);
            #endregion

            #region 标题信息
            for (int i = 1; i <= Settings.Count; i++)
            {
                excel.SetCells(1, i, Settings.ElementAt(i - 1).Value);
            }
            excel.SetCellsBackColor(1, 1, 1, Settings.Count, ColorIndex.灰色25);//设置颜色
            excel.SetCellsAlign(1, 1, 1, Settings.Count, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);//设置对齐
            #endregion

            #region 分包合同
            Export(dt, excel, columns);
            #endregion

            #region 设置单元格居中
            //编号居中
            excel.SetCellsAlign(2, 1, dt.Rows.Count + 1, 1, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter);
            //居右
            excel.SetCellsAlign(2, 2, dt.Rows.Count + 1, columns.Count, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight);
            
            #endregion


            excel.SaveAsFile();//文件保存
            MessageBox.Show("操作成功！");
        }

        /// <summary>
        /// 绑定分包列表数据
        /// </summary>
        private void SupplierBind(List<string> pids)
        {
            dt = bll.GetSupplier(pids, Settings);
            this.treeList1.DataSource = dt;
            treeList1.KeyFieldName = "KeyFieldName"; ;
            treeList1.ParentFieldName = "ParentFieldName";
        }

        /// <summary>
        /// 绑定项目列表数据
        /// </summary>
        private void ProjectBind()
        {
            List<DomainDLL.Project> list = bll.GetProject();
            checkedListBox1.DataSource = list;
            checkedListBox1.DisplayMember = "Name";
            checkedListBox1.ValueMember = "ID";
        }

        /// <summary>
        /// 委托设置当前的显示字段
        /// </summary>
        /// <param name="settings"></param>
        private void SetDic(Dictionary<string, string> settings)
        {
            Settings = settings;
            foreach (DevExpress.XtraTreeList.Columns.TreeListColumn item in treeList1.Columns)
            {
                item.Visible = false;
                if (settings.ContainsKey(item.FieldName))
                    item.Visible = true;
                if (item.FieldName == "Name")
                    item.Visible = true;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            Settings.Add("LegalMan", "法人");
            Settings.Add("Manager", "负责人");
            Settings.Add("Addr", "地址");
            Settings.Add("Tel", "联系电话");            
        }

        /// <summary>
        /// 模板数据格式化导入
        /// Created:2017.05.15(zhuguanjun)
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
