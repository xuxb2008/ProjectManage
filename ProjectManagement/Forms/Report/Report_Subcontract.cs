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
using CommonDLL;

namespace ProjectManagement.Forms.Report
{
    public partial class Report_Subcontract : Common.BaseForm
    {
        #region 业务逻辑初始化
        ReportSubcontractBLL bll = new ReportSubcontractBLL();
        #endregion
        #region 变量
        DataTable dt = new DataTable();
        //需要显示的字段集合
        Dictionary<string,string> Settings = new Dictionary<string, string>();
        #endregion

        /// <summary>
        /// 分包合同报表
        /// 2017/05/11(zhuguanjun)
        /// </summary>
        public Report_Subcontract()
        {
            InitializeComponent();
            Init();
            ProjectBind();
        }

        /// <summary>
        /// 绑定项目列表数据
        /// 2017/05/11(zhuguanjun)
        /// </summary>
        private void ProjectBind()
        {
            List<DomainDLL.Project> list = bll.GetProject();
            checkedListBox1.DataSource = list;
            checkedListBox1.DisplayMember = "Name";
            checkedListBox1.ValueMember = "ID";
        }

        /// <summary>
        /// 全选
        /// 2017/05/11(zhuguanjun)
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
        /// 2017/05/11(zhugaunjun)
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
        /// 2017/05/11(zhuguanjun)
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
            SubcontractBind(pids);
        }

        /// <summary>
        /// 设置报表显示字段
        /// 2017/05/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Report_Subcontract_Setting form = new Report_Subcontract_Setting(Settings);
            form.settingdelegate += SetDic;
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Close();
            }
        }

        /// <summary>
        /// 委托设置当前的显示字段
        /// </summary>
        /// <param name="settings"></param>
        private void SetDic(Dictionary<string,string> settings)
        {
            Settings = settings;
            foreach (DevExpress.XtraTreeList.Columns.TreeListColumn item in treeList1.Columns)
            {
                item.Visible = false;
                if (settings.ContainsKey(item.FieldName))
                    item.Visible = true;
                if (item.FieldName == "B_Name")
                    item.Visible = true;
            }            
        }

        /// <summary>
        /// 绑定分包列表数据
        /// </summary>
        private void SubcontractBind(List<string> pids)
        {
            dt = bll.GetSubcontract(pids,Settings);
            this.treeList1.DataSource = dt;
            treeList1.KeyFieldName = "KeyFieldName"; ;
            treeList1.ParentFieldName = "ParentFieldName";
            
        }

        /// <summary>
        /// 初始化
        /// 2015/05/15
        /// </summary>
        private void Init()
        {            
            Settings.Add("B_Name", "分包合同名称");
            Settings.Add("B_No", "分包合同编号");
            Settings.Add("A_No", "主合同名称");
            Settings.Add("A_Name", "主合同编号");
            Settings.Add("SupplierName", "合作商");
            Settings.Add("Amount", "分包合同金额");
            Settings.Add("SignDate", "签订日期");
            Settings.Add("Desc", "描述");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string saveFileName = "分包合同信息报表";
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
            #region
            ExcelHelper excel = new ExcelHelper(FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.Report_Dynamic, "", "") + ConstHelper.Report_Dynamic, saveFileName);
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

            #region 生成            
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
