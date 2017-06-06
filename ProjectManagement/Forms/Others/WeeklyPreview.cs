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
using System.IO;
using DomainDLL;


namespace ProjectManagement.Forms.Others
{
    public partial class WeeklyPreview : BaseForm
    {
        #region 业务类初期化
        ReportBLL bll = new ReportBLL();
        #endregion

        #region 画面变量
        DataTable dtThisRoutine;//本周计划完成工作
        DataTable dtNextRoutine;//本周计划完成工作
        DataTable dtTrouble;//存在的问题
        Setting setting;//周报配置
        List<int> addRow = new List<int>() { 0, 0, 0 };//每个数据表对模板中增删的行数
        #endregion

        #region 事件

        /// <summary>


        public WeeklyPreview()
        {
            InitializeComponent();
            DateTime now = DateTime.Now;  //当前时间  
            DateTime startWeek = now.AddDays(1 - Convert.ToInt32(now.DayOfWeek.ToString("d")));  //本周周一  
            DateTime endWeek = startWeek.AddDays(6);  //本周周日  
            DateTime nextWeek = endWeek.AddDays(1);  //下周一  
            DateTime nextEndWeek = nextWeek.AddDays(6);  //下周日  

            setting = new SettingBLL().GetSetting(ProjectId);//获取周报设置
            List<string> listContent = string.IsNullOrEmpty(setting.WeeklyCheck) ? new List<string> { "0", "0", "0", "0", "0", "0", "0", "0" } : setting.WeeklyCheck.Split(',').ToList();
            if (listContent.Count < 8)
                listContent = new List<string> { "0", "0", "0", "0", "0", "0", "0", "0" };

            #region 本周计划完成工作
            dtThisRoutine = new ReportBLL().GetFinishedWork(ProjectId, startWeek, endWeek, listContent[0].Equals("1"), listContent[2].Equals("1"), listContent[4].Equals("1"));
            dtThisRoutine.Rows.RemoveAt(0);
            DataHelper.AddNoCloumn(dtThisRoutine);
            gridThisWork.PrimaryGrid.DataSource = dtThisRoutine;
            #endregion

            #region 下周计划完成工作
            dtNextRoutine = new ReportBLL().GetUnFinishWork(ProjectId, nextWeek, nextEndWeek, listContent[1].Equals("1"), listContent[3].Equals("1"), listContent[5].Equals("1"));
            dtNextRoutine.Rows.RemoveAt(0);
            DataHelper.AddNoCloumn(dtNextRoutine);
            gridNextWork.PrimaryGrid.DataSource = dtNextRoutine;
            #endregion

            #region 存在的问题
            dtTrouble = new ReportBLL().GetTroubleList(ProjectId, startWeek, listContent[6].Equals("1"), listContent[7].Equals("1"));
            dtTrouble.Rows.RemoveAt(0);
            DataHelper.AddNoCloumn(dtTrouble);
            gridTrouble.PrimaryGrid.DataSource = dtTrouble;
            #endregion

        }

        /// <summary>
        /// 生成模板
        /// Created:2017.04.21(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CreateMail_Click(object sender, EventArgs e)
        {
            string addr_model = FileHelper.GetUploadPath(UploadType.WeeklyModel, "", "") + ConstHelper.Config_WeeklyFileName;
            string addr_save = FileHelper.GetFilePath(UploadType.Report_Weekly, ProjectId, "", "")
                + DateTime.Now.ToString("yyyy年MM月") + "第" +CommonHelper.getWeekNumInMonth(DateTime.Now) + "周\\" ;
            string name_save = "项目周报.xlsx";
            if (!Directory.Exists(addr_save))
            {
                Directory.CreateDirectory(addr_save);
            }
            addr_save += name_save;

            //SaveFileDialog saveDialog = new SaveFileDialog();
            //saveDialog.DefaultExt = "xls";
            //saveDialog.Filter = "Excel文件|*.xls";
            //saveDialog.FileName = saveFileName;
            //saveDialog.ShowDialog();
            //addr_save = saveDialog.FileName;
            //if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            dynamic xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            ExcelHelper excel = new ExcelHelper(addr_model, addr_save);
            #region 标题信息
            excel.SetCells(1, 1, ProjectName + "-项目周报");
            excel.SetCells(2, 3, DateTime.Now.ToShortDateString());
            excel.SetCells(2, 7, DateTime.Now.ToShortDateString());//当前日期
            excel.SetCells(3, 7, CommonHelper.GetConfigValue(ConstHelper.Config_UserName));//当前用户
            #endregion
            #region 内容
            List<string> columns;
            //本周完成工作
            columns = new List<string>() { "Type", "Name", "Desc", "Result", "Person", "Status" };
            Export(dtThisRoutine, excel, 0, columns);
            // 下周计划完成工作
            Export(dtThisRoutine, excel, 1, columns);
            // 存在的问题
            columns = new List<string>() { "Name", "Desc", "Result", "HandleDate", "Person", "Status" };
            Export(dtTrouble, excel, 2, columns);
            #endregion
            excel.SaveAsFile();//文件保存
            excel.Dispose();

            MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
            WeeklyHistory fm = new WeeklyHistory(new Report_WeeklyFiles() { RowNo = 1, Name = name_save, Path = addr_save });
            mainForm.ShowChildForm(fm);

        }
        #endregion

        #region 方法

        /// <summary>
        /// 模板数据格式化导入
        /// Created:2017.04.21(ChengMengjia)
        /// </summary>
        /// <param name="dt">数据表格</param>
        /// <param name="excel"></param>
        /// <param name="No">表格序号</param>
        /// <param name="ColumnNames">列名集合</param>
        private void Export(DataTable dt, ExcelHelper excel, int No, List<string> ColumnNames)
        {
            //首先计算之前增删的行数
            int count = 0;
            for (int i = 0; i <= No; i++)
                count += addRow[i];

            if (dt != null)
            {
                if (dt.Rows.Count > 3)
                {
                    //数据行数大于3，需要添加行
                    addRow[No] = dt.Rows.Count - 3;
                    excel.InsertRows(9 + No * 5 + count, addRow[No]);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int row = 6 + No * 5 + count + i;
                    excel.SetCells(row, 1, (1 + i).ToString());//序号
                    for (int s = 0; s < ColumnNames.Count; s++)
                        excel.SetCells(row, s + 2, dt.Rows[i][ColumnNames[s]].ToString());
                }
            }
            else
            {
                //不需要此数据表格，根据表格序号删除
                addRow[No] = -5;
                excel.DeleteRows(4 + No * 5 + count, 5);
            }
        }

        #endregion


    }

}
