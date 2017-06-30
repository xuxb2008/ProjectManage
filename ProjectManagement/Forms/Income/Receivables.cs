using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using CommonDLL;
using BussinessDLL;
using DomainDLL;
using ProjectManagement.Control;
using DevComponents.DotNetBar.SuperGrid;

namespace ProjectManagement.Forms.Income
{
    /// <summary>
    /// 画面名：收款信息
    /// Created：20170327(ChengMengjia)
    /// </summary>
    public partial class FormReceivables : Common.BaseForm
    {
        #region 业务类初期化
        ReceivablesBLL bll = new ReceivablesBLL();
        #endregion

        #region 变量
        #endregion

        #region 事件
        public FormReceivables()
        {
            InitializeComponent();
            dtSInDate.Value = DateTime.Now;
            DataHelper.LoadDictItems(cbSFinishStatus, DictCategory.Receivables_FinshStatus);
            LoadSK();
        }

        /// <summary>
        /// 分页控件变化触发事件
        /// Created：20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageChanged(object sender, EventArgs e)
        {
            LoadSK();
        }

        /// <summary>
        /// 收款-列表行单击触发事件
        ///  Created：20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridSK_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            DevComponents.DotNetBar.SuperGrid.GridElement list = gridSK.GetSelectedRows()[0];
            string s = list.ToString();
            s = s.Replace("{", ",");
            s = s.Replace("}", ",");
            string[] listS = s.Split(',');
            txtSBatchNo.Tag = listS[1].Trim();
            txtSBatchNo.Text = listS[3].Trim();
            txtExplanation.Text = listS[4] == "<null>" ? "" : listS[4].Trim();
            intSRatio.Value = listS[5] == "<null>" ? 0 : int.Parse(listS[5].Trim());
            txtAmount.Text = listS[6] == "<null>" ? "0" : listS[6].Trim();
            txtSCondition.Text = listS[7] == "<null>" ? "" : listS[7].Trim();
            dtSInDate.Value = listS[9] == "<null>" ? DateTime.Now : DateTime.Parse(listS[9].Trim());
            DataHelper.SetComboBoxSelectItemByText(cbSFinishStatus, listS[8] == "<null>" ? "-1" : listS[8].Trim());
            txtSRemark.Text = listS[10] == "<null>" ? "" : listS[10].Trim();
        }

        /// <summary>
        /// 收款-保存
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Receivables entity = new Receivables();
            entity.ID = txtSBatchNo.Tag == null ? "" : txtSBatchNo.Tag.ToString();
            entity.PID = ProjectId;
            entity.BatchNo = txtSBatchNo.Text;
            ComboItem item = (ComboItem)cbSFinishStatus.SelectedItem;
            if (item != null)
                entity.FinishStatus = int.Parse(item.Value.ToString());
            entity.Ratio = intSRatio.Value;
            decimal temp = 0;
            decimal.TryParse(txtAmount.Text, out temp);
            entity.Amount = temp;
            entity.Condition = txtSCondition.Text;
            entity.Remark = txtSRemark.Text;
            entity.InDate = dtSInDate.Value;
            entity.Explanation = txtExplanation.Text.ToString();
            #region 判断空值
            if (string.IsNullOrEmpty(entity.BatchNo))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收款批次");
                return;
            }
            //if (entity.FinishStatus == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "完成情况");
            //    return;
            //}
            //if (entity.Ratio == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收款比例");
            //    return;
            //}
            //if (entity.Amount == null)
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收款金额");
            //    return;
            //}
            //if (string.IsNullOrEmpty(entity.Condition))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收款条件");
            //    return;
            //}
            //if (entity.InDate == null || entity.InDate == DateTime.Parse("0001/1/1 0:00:00"))
            //{
            //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收款日期");
            //    return;
            //}
            #endregion
            JsonResult result = bll.SaveSK(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                btnSClear_Click(sender, e);
                LoadSK();
            }
        }

        /// <summary>
        /// 收款-清空
        ///  Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSClear_Click(object sender, EventArgs e)
        {
            txtSBatchNo.Tag = null;
            txtSBatchNo.Clear();
            txtExplanation.Clear();
            cbSFinishStatus.SelectedIndex = -1;
            intSRatio.Value = 0;
            txtAmount.Text = "0";
            txtSCondition.Clear();
            dtSInDate.Value = DateTime.Now;
            txtSRemark.Clear();
            gridSK.GetSelectedRows().Select(false);//取消选择
            txtSBatchNo.Clear();
        }

        /// <summary>
        /// 比例改变，金额随之改变
        /// 2017/6/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intSRatio_ValueChanged(object sender, EventArgs e)
        {
            var jbxx = new ProjectInfoBLL().GetJBXX(ProjectId);
            decimal temp = 0;
            if (jbxx != null)
            {
                decimal.TryParse(jbxx.Amount, out temp);
            }
            txtAmount.Text = (temp * intSRatio.Value / 100).ToString();
        }

        #endregion

        #region 方法


        /// <summary>
        /// 收款-加载
        ///  Created：20170327(ChengMengjia)
        /// Updated:2017/05/09(zhuguanjun)
        /// </summary>
        private void LoadSK()
        {
            GridData gridData = bll.GetSKList(0, 0, ProjectId);
            gridSK.PrimaryGrid.DataSource = gridData.data;
            //int recordcount;
            //List<QueryField> qlist = new List<QueryField>();
            //qlist.Add(new QueryField() { Name = "PID", Type = QueryFieldType.String, Value = ProjectId });
            //qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            //List<DomainDLL.Receivables> list = bll.GetPageList(pagerControl1.PageSize, pagerControl1.PageIndex, qlist, null, out recordcount);
            //gridSK.PrimaryGrid.DataSource = list;
        }




        #endregion

    }
}
