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

namespace ProjectManagement.Forms.WBS
{
    /// <summary>
    ///  画面名：节点重命名
    /// Created:20170406(ChengMengjia)
    /// </summary>
    public partial class ReName : BaseForm
    {
        #region 业务类初期化
        WBSBLL bll = new WBSBLL();
        #endregion

        #region 画面变量
        PNode _node;
        #endregion

        #region 事件
        public ReName(PNode node)
        {
            InitializeComponent();
            _node = node;
            txtOldName.Text = _node.Name;
            txtNewName.Focus();
        }

        /// <summary>
        /// 保存
        /// Created:20170406(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string Name = txtNewName.Text;
            if (string.IsNullOrEmpty(Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "新名称");
                txtNewName.Focus();
                return;
            }
            else if (_node.Name.Equals(Name))
            {
                MessageBox.Show("新名称和原名称一样！");
                txtNewName.Focus();
                return;
            }
            _node.Name = Name;
            JsonResult result;
            if (_node.PType==1)
            {
                //如果为交付物节点
                DeliverablesJBXX jbxx = bll.GetJBXX(_node.ID);
                jbxx.Name = Name;
                result = bll.UpdateJBXX(jbxx,null);
            }
            else
                result = bll.SaveNode(_node);
            if (result.result)
            {
                FileHelper.WBSMoveFloder(UploadType.WBS,result.data.ToString());//迁移文件夹
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageHelper.ShowRstMsg(false);

        }

        /// <summary>
        /// 取消
        /// Created:20170406(ChengMengjia)
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
