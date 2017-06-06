using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CommonDLL;
using ProjectManagement.Common;
using DomainDLL;
using BussinessDLL;

namespace ProjectManagement
{
    /// <summary>
    ///  画面名：输入信息
    /// Created:20170410(ChengMengjia)
    /// </summary>
    public partial class EnterInfo : BaseForm
    {

        string val;

        #region 事件
        public EnterInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            val = txtResult.Text;
            this.Close();

        }

        public string GetVaule()
        {
            return val;
        }

        /// <summary>
        /// 取消
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
