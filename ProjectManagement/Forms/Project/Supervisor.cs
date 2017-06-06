using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DomainDLL;
using BussinessDLL;
using CommonDLL;

namespace ProjectManagement.Forms.Project
{
    /// <summary>
    ///  画面名：监理信息
    /// Created：20170328(ChengMengjia)
    /// </summary>
    public partial class FormSupervisor : Common.BaseForm
    {
        #region 业务类初期化
        SupervisorBLL bll = new SupervisorBLL();
        #endregion

        #region 画面变量
        string _jlxxID;
        #endregion

        #region 事件
        public FormSupervisor()
        {
            InitializeComponent();
            gridPager.OnPageChanged += new EventHandler(PageChanged);
            DataHelper.LoadDictItems(cbJWay, DictCategory.Supervisor_Way);
            LoadJLXX();
            LoadJLPJ();
            dtJDate.Value = DateTime.Now;
            dtJDate.IsInputReadOnly = true;
        }

        /// <summary>
        /// 分页控件变化触发事件
        /// Created：20170331(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageChanged(object sender, EventArgs e)
        {
            LoadJLPJ();
        }

        /// <summary>
        /// 监理信息-清空
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJClear_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_jlxxID))
                LoadJLXX();
            else
            {
                txtJCName.Clear();
                txtJManagerA.Clear();
                txtJManagerB.Clear();
                txtJA_Email.Clear();
                txtJA_QQ.Clear();
                txtJA_Tel.Clear();
                txtJA_Wechat.Clear();
                txtJB_QQ.Clear();
                txtJB_Tel.Clear();
                txtJB_Email.Clear();
                txtJB_Wechat.Clear();
                txtJCost.Clear();
                cbJWay.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 监理信息-保存
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJSave_Click(object sender, EventArgs e)
        {
            Supervisor entity = new Supervisor();
            entity.ID = _jlxxID;
            entity.PID = ProjectId;
            entity.CompanyName = txtJCName.Text;
            entity.ManagerA = txtJManagerA.Text;
            entity.ManagerB = txtJManagerB.Text;
            entity.A_Email = txtJA_Email.Text;
            entity.A_QQ = txtJA_QQ.Text;
            entity.A_Tel = txtJA_Tel.Text;
            entity.A_Wechat = txtJA_Wechat.Text;
            entity.B_Email = txtJB_Email.Text;
            entity.B_QQ = txtJB_QQ.Text;
            entity.B_Tel = txtJB_Tel.Text;
            entity.B_Wechat = txtJB_Wechat.Text;
            entity.Cost = txtJCost.Text;
            entity.Way = cbJWay.Text;

            #region 判断空值
            if (string.IsNullOrEmpty(entity.CompanyName))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "监理公司名称");
                return;
            }
            if (string.IsNullOrEmpty(entity.ManagerA))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "监理姓名");
                return;
            }
            if (string.IsNullOrEmpty(entity.A_Tel))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "监理手机号码");
                return;
            }
            if (string.IsNullOrEmpty(entity.Cost))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "监理费用");
                return;
            }
            if (string.IsNullOrEmpty(entity.Way))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "监理方式");
                return;
            }
            #endregion
            JsonResult result = bll.SaveJLXX(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                _jlxxID = result.data.ToString();
            }
        }

        /// <summary>
        /// 监理评价内容-清空
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJPClear_Click(object sender, EventArgs e)
        {
            txtJName.Clear();
            txtJName.Tag = "";
            txtJContent.Clear();
            dtJDate.Value = DateTime.Now;
            gridJLPJ.GetSelectedRows().Select(false);//取消选择
            dtJDate.IsInputReadOnly = true;
        }

        /// <summary>
        /// 监理评价单击修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridJLPJ_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            dtJDate.IsInputReadOnly = false;
            DevComponents.DotNetBar.SuperGrid.GridElement list = gridJLPJ.GetSelectedRows()[0];
            string s = list.ToString();
            s = s.Replace("{", ",");
            s = s.Replace("}", ",");
            string[] listS = s.Split(',');
            txtJName.Tag = listS[5].Trim();
            txtJName.Text = listS[2].Trim();
            txtJContent.Text = listS[3].Trim();
            dtJDate.Value = DateTime.Parse(listS[4].Trim());
        }


        /// <summary>
        /// 监理评价内容-保存
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJPSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_jlxxID))
            {
                MessageBox.Show("请先完善左侧监理信息！");
                return;
            }

            SupervisorJudge entity = new SupervisorJudge();
            entity.ID = txtJName.Tag == null ? "" : txtJName.Tag.ToString();
            entity.PID = ProjectId;
            entity.Name = txtJName.Text;
            entity.Content = txtJContent.Text;
            entity.JudgeDate = dtJDate.Value;
            #region 判断空值
            if (string.IsNullOrEmpty(entity.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "姓名");
                return;
            }
            if (string.IsNullOrEmpty(entity.Content))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "评价内容");
                return;
            }
            if (entity.JudgeDate == null || entity.JudgeDate == DateTime.Parse("0001/1/1 0:00:00"))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "评价日期");
                return;
            }
            #endregion

            JsonResult result = bll.SaveJLPJ(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                btnJPClear_Click(sender, e);
                LoadJLPJ();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 监理信息-加载
        /// Created：20170328(ChengMengjia)
        /// </summary>
        private void LoadJLXX()
        {
            Supervisor entity = bll.GetJLXX(ProjectId);
            if (!string.IsNullOrEmpty(entity.ID))
            {
                _jlxxID = entity.ID;
                txtJCName.Text = entity.CompanyName;
                txtJManagerA.Text = entity.ManagerA;
                txtJManagerB.Text = entity.ManagerB;
                txtJA_Email.Text = entity.A_Email;
                txtJA_QQ.Text = entity.A_QQ;
                txtJA_Tel.Text = entity.A_Tel;
                txtJA_Wechat.Text = entity.A_Wechat;
                txtJB_QQ.Text = entity.B_QQ;
                txtJB_Tel.Text = entity.B_Tel;
                txtJB_Email.Text = entity.B_Email;
                txtJB_Wechat.Text = entity.B_Wechat;
                txtJCost.Text = entity.Cost;
                DataHelper.SetComboBoxSelectItemByText(cbJWay, entity.Way);
            }
            else
                btnJClear_Click(null, null);

        }


        /// <summary>
        /// 监理评价内容-加载
        /// Created：20170327(ChengMengjia)
        /// </summary>
        private void LoadJLPJ()
        {
            GridData gridData = bll.GetJLPJList(gridPager.PageIndex, gridPager.PageSize, ProjectId);
            gridJLPJ.PrimaryGrid.DataSource = gridData.data;
            gridPager.DrawControl(gridData.count);
        }


        #endregion

    }
}
