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
        #endregion

        public Report_MemberContributionRate()
        {
            InitializeComponent();
            DataBind();
        }

        private void DataBind() {
            var dt = bll.GetMemberRate(ProjectId);
            this.superGridControl1.PrimaryGrid.DataSource = dt;
        }
    }
}
