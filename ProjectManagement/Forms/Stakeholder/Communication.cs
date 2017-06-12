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
using DevComponents.DotNetBar.SuperGrid;
using DomainDLL;
using CommonDLL;

namespace ProjectManagement.Forms.Stakeholder
{
    public partial class Communication : BaseForm
    {
        #region 业务类初始化
        private CommunicationBLL bll = new CommunicationBLL();
        #endregion

        #region 变量
        private string ID = null;
        private DateTime CREATED = DateTime.Now;
        #endregion

        #region 事件
        public Communication()
        {
            InitializeComponent();
            DataBind(null, null);
            dateCreated.Value = CREATED;
            pagerControl1.OnPageChanged += new EventHandler(DataBind);
        }

        /// <summary>
        /// 顺番编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAsc(object sender, EventArgs e)
        {
            foreach (GridElement row in superGridControl1.PrimaryGrid.Rows)
            {
                ((GridRow)row).Cells["编号"].Value = ((GridRow)row).RowIndex + 1 + pagerControl1.PageSize * (pagerControl1.PageIndex - 1);
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataBind(object sender, EventArgs e)
        {
            int recordcount;
            List<DomainDLL.Communication> list = bll.GetPageList(pagerControl1.PageSize, pagerControl1.PageIndex, ProjectId, out recordcount);
            superGridControl1.PrimaryGrid.DataSource = list;
            superGridControl1.DataBindingComplete += DataAsc;
            pagerControl1.DrawControl(recordcount);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCotent.Clear();
            txtName.Clear();
            ID = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "沟通方式");
                return;
            }
            #endregion

            DomainDLL.Communication communication = new DomainDLL.Communication();
            communication.Content = txtCotent.Text.ToString();
            communication.Name = txtName.Text.ToString();
            communication.CREATED = CREATED;
            communication.UPDATED = DateTime.Now;
            communication.ID = ID;
            communication.PID = ProjectId;

            JsonResult json = bll.SaveCommunication(communication);
            if (!json.result)
                MessageHelper.ShowRstMsg(json.result);
            btnClear_Click(null, null);
            DataBind(null, null);
        }

        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl1.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtCotent.Text = row.Cells["Content"].Value.ToString();
            ID = row.Cells["ID"].Value.ToString();
            dateCreated.Value = string.IsNullOrEmpty(ID) ? DateTime.Now : Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
            CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
        }
        #endregion

        #region 方法
        #endregion
    }
}
