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

namespace ProjectManagement.Forms.Report
{
    public partial class Report_ProjectInfo : Common.BaseForm
    {
        #region 业务类初始化
        ReportProjectBLL bll = new ReportProjectBLL();
        #endregion
        public Report_ProjectInfo()
        {
            InitializeComponent();
            var list = bll.GetProjects();
            superGridControl1.PrimaryGrid.DataSource = list;
            
        }
    }
}
