using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ProjectManagement.Common;

namespace ProjectManagement.Forms.Warning
{
    /// <summary>
    /// 画面名：预警一览
    /// Created：2017.04.17(Xuxb)
    /// </summary>
    public partial class WarningList : BaseForm
    {
        #region 事件

        public WarningList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面加载时
        /// Created：2017.04.17(Xuxb)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WarningList_Load(object sender, EventArgs e)
        {
            DataTable dt = DataHelper.GetWarnningData(ProjectId);
            DataHelper.AddNoCloumn(dt);
            superGridWarning.PrimaryGrid.DataSource = dt;
        }

        #endregion

        #region 方法

        #endregion
    }
}
