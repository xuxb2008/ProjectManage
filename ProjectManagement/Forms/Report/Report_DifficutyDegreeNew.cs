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
    }
}
