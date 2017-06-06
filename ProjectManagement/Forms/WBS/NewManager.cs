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
using DevComponents.Editors;

namespace ProjectManagement.Forms.WBS
{
    /// <summary>
    ///  画面名：添加责任人
    /// Created:20170526(ChengMengjia)
    /// </summary>
    public partial class NewManager : BaseForm
    {
        #region 业务类初期化
        WBSBLL bll = new WBSBLL();
        #endregion

        #region 画面变量
        public WorkloadEntity ReturnValue { get; protected set; }
        #endregion

        #region 事件
        public NewManager(string ManagerID, int WorkLoad, int ActualWorkLoad)
        {
            InitializeComponent();
            LoadManager(ManagerID, WorkLoad, ActualWorkLoad);
            cbManager.Focus();
        }
        /// <summary>
        /// 预计工作量变化时间
        /// Created:20170605(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intWorkload_ValueChanged(object sender, EventArgs e)
        {
            intActualWorkload.Value = intWorkload.Value;
        }
        /// <summary>
        /// 保存
        /// Created:20170526(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ReturnValue = new WorkloadEntity();
            ReturnValue.Manager = cbManager.SelectedItem == null ? "" : ((ComboItem)cbManager.SelectedItem).Tag.ToString();
            ReturnValue.ManagerName = cbManager.SelectedItem == null ? "" : ((ComboItem)cbManager.SelectedItem).Text;
            ReturnValue.Workload = intWorkload.Value;
            ReturnValue.ActualWorkload = intActualWorkload.Value;
            if (string.IsNullOrEmpty(ReturnValue.Manager))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "责任人");
                cbManager.Focus();
                return;
            }
            if (ReturnValue.Workload == null)
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "预计工作量");
                intWorkload.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// Created:20170526(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion

        #region 方法

        /// <summary>
        /// 责任人加载
        ///  Created：20170526（ChengMengjia）
        /// </summary>
        void LoadManager(string ManagerID, int WorkLoad, int ActualWorkLoad)
        {
            int SelectedIndex = -1;
            intWorkload.Value = WorkLoad;
            intActualWorkload.Value = ActualWorkLoad;
            List<Stakeholders> list = new StakeholdersBLL().GetList(ProjectId, null);//所有可选人
            for (int i = 0; i < list.Count; i++)
            {
                ComboItem item = new ComboItem();
                item.Text = list[i].Name;
                item.Tag = list[i].ID.Substring(0, 36);
                if (string.IsNullOrEmpty(ManagerID))
                {
                    if (list[i].IsPublic == 1)
                        SelectedIndex = i;
                }
                else if (list[i].ID.Substring(0, 36).Equals(ManagerID))
                    SelectedIndex = i;
                cbManager.Items.Add(item);
            }
            cbManager.SelectedIndex = SelectedIndex;
        }
        #endregion

    }
}
