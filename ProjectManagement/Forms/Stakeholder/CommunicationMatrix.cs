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
using BussinessDLL;
using DomainDLL;
using DevComponents.Editors;
using CommonDLL;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace ProjectManagement.Forms.Stakeholder
{
    public partial class CommunicationMatrix : BaseForm
    {
        #region 业务类初始化
        CommunicationMatrixBLL bll = new CommunicationMatrixBLL();
        #endregion

        #region 画面变量
        private int? type = null;
        private int? sendtype = null;
        private string ID = null;
        private string FXFAID1 = null;
        private string FXFAID2 = null;
        private string FXFAID3 = null;
        private DateTime CREATED = DateTime.MinValue;
        #endregion

        #region 事件

        public CommunicationMatrix()
        {
            InitializeComponent();
            BindListBox();
        }

        /// <summary>
        /// 选择干系人事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAdv1_ItemClick(object sender, EventArgs e)
        {
            Stakeholders stakeholders = new Stakeholders();
            List<CommunicationFXFA> list = new List<CommunicationFXFA>();
            bll.GetCommunicationMatix(ProjectId, listBoxAdv1.SelectedValue.ToString(), out stakeholders, out list);
            if (stakeholders != null)
            {
                txtCompanyName.Text = stakeholders.CompanyName;
                txtDuty.Text = stakeholders.Duty;
                txtEmail.Text = stakeholders.Email;
                txtName.Text = stakeholders.Name;
                txtPosition.Text = stakeholders.Position;
                txtQQ.Text = stakeholders.QQ;
                txtTel.Text = stakeholders.Tel;
                txtWechat.Text = stakeholders.Wechat;
                cbIspublic.CheckValue = stakeholders.IsPublic;
                dtiCREATED.Value = stakeholders.CREATED;
                type = stakeholders.Type;
                sendtype = stakeholders.SendType;
                ID = stakeholders.ID;
                CREATED = stakeholders.CREATED;
            }
            if (list != null && list.Count > 0)
            {
                txtAddress1.Text = list[0].Addr;
                txtContent1.Text = list[0].Content;
                txtCommunicateDate1.Text = list[0].CommunicateDate;
                txtFrenquence1.Text = list[0].Frequency;
                DataHelper.SetComboBoxSelectItemByValue(cmbCommunication1, list[0].CID);
                FXFAID1 = list[0].ID;
                if (list.Count > 1)
                {
                    txtAddress2.Text = list[1].Addr;
                    txtContent2.Text = list[1].Content;
                    txtConmunicateDate2.Text = list[0].CommunicateDate;
                    txtFrenquence2.Text = list[0].Frequency;
                    DataHelper.SetComboBoxSelectItemByValue(cmbCommunication2, list[1].CID);
                    FXFAID2 = list[1].ID;
                }
                if (list.Count > 2)
                {
                    txtAddress3.Text = list[2].Addr;
                    txtContent3.Text = list[2].Content;
                    txtConmunicateDate3.Text = list[0].CommunicateDate;
                    txtFrenquence3.Text = list[0].Frequency;
                    DataHelper.SetComboBoxSelectItemByValue(cmbCommunication3, list[2].CID);
                    FXFAID3 = list[2].ID;
                }
            }

        }

        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl1.PrimaryGrid.ClearSelectedColumns();
                return;
            }

            //清空
            ClearAll();

            GridRow row = (GridRow)rows[0];
            Stakeholders stakeholders = new Stakeholders();
            List<CommunicationFXFA> list = new List<CommunicationFXFA>();
            bll.GetCommunicationMatix(ProjectId, row.Cells["ID"].Value.ToString(), out stakeholders, out list);
            if (stakeholders != null)
            {
                txtCompanyName.Text = stakeholders.CompanyName;
                txtDuty.Text = stakeholders.Duty;
                txtEmail.Text = stakeholders.Email;
                txtName.Text = stakeholders.Name;
                txtPosition.Text = stakeholders.Position;
                txtQQ.Text = stakeholders.QQ;
                txtTel.Text = stakeholders.Tel;
                txtWechat.Text = stakeholders.Wechat;
                cbIspublic.CheckValue = stakeholders.IsPublic;
                dtiCREATED.Value = stakeholders.CREATED;
                type = stakeholders.Type;
                sendtype = stakeholders.SendType;
                ID = stakeholders.ID;
                CREATED = stakeholders.CREATED;
            }
            if (list != null && list.Count > 0)
            {
                txtAddress1.Text = list[0].Addr;
                txtContent1.Text = list[0].Content;
                txtCommunicateDate1.Text = list[0].CommunicateDate;
                txtFrenquence1.Text = list[0].Frequency;
                DataHelper.SetComboBoxSelectItemByValue(cmbCommunication1, list[0].CID);
                FXFAID1 = list[0].ID;
                if (list.Count > 1)
                {
                    txtAddress2.Text = list[1].Addr;
                    txtContent2.Text = list[1].Content;
                    txtConmunicateDate2.Text = list[0].CommunicateDate;
                    txtFrenquence2.Text = list[0].Frequency;
                    DataHelper.SetComboBoxSelectItemByValue(cmbCommunication2, list[1].CID);
                    FXFAID2 = list[1].ID;
                }
                if (list.Count > 2)
                {
                    txtAddress3.Text = list[2].Addr;
                    txtContent3.Text = list[2].Content;
                    txtConmunicateDate3.Text = list[0].CommunicateDate;
                    txtFrenquence3.Text = list[0].Frequency;
                    DataHelper.SetComboBoxSelectItemByValue(cmbCommunication3, list[2].CID);
                    FXFAID3 = list[2].ID;
                }
            }
        }

        /// <summary>
        /// 保存干系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveStakeholders_Click(object sender, EventArgs e)
        {
            #region 检查
            int flag = 0;//没有选项目经理
            string flagid = string.Empty;//项目经理id
            if (superGridControl1.PrimaryGrid.Rows.Count != 0)
            {
                foreach (var item in superGridControl1.PrimaryGrid.Rows)
                {
                    string s = item.ToString();
                    s = s.Replace("{", ",");
                    s = s.Replace("}", ",");
                    string[] listS = s.Split(',');
                    if (int.Parse(listS[4].Trim()) != 0)
                    {
                        flag = 1;
                        flagid = listS[3].Trim();
                    }
                }
            }
            if (flag != 0 && cbIspublic.Checked && flagid != ID)
            {
                MessageBox.Show("不能存在多个项目经理");
                return;
            }

            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("未选择干系人");
                return;
            }
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "姓名");
                return;
            }
            #endregion
            
            var stakeholders = new Stakeholders();
            stakeholders.ID = ID;
            stakeholders.CREATED = CREATED;
            stakeholders.CompanyName = txtCompanyName.Text.ToString();
            stakeholders.Duty = txtDuty.Text.ToString();
            stakeholders.Email = txtEmail.Text.ToString();
            stakeholders.IsPublic = cbIspublic.CheckValue.ToString() == "Y" ? 1 : 0;
            stakeholders.Name = txtName.Text.ToString();
            stakeholders.PID = ProjectId;
            stakeholders.Position = txtPosition.Text.ToString();
            stakeholders.QQ = txtQQ.Text.ToString();
            stakeholders.Tel = txtTel.Text.ToString();
            stakeholders.Wechat = txtWechat.Text.ToString();
            stakeholders.SendType = sendtype;
            stakeholders.Type = type;
            string _id = "";
            JsonResult json = new StakeholdersBLL().SaveStakehoders(stakeholders, out _id);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
            {
                ID = _id;
            }
        }

        /// <summary>
        /// 保存干系人沟通方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCommunicationFXFA_Click(object sender, EventArgs e)
        {
            var rows = superGridControl1.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一条干系人数据");
                return;
            }
            List<CommunicationFXFA> list = new List<CommunicationFXFA>();
            if (!string.IsNullOrEmpty(FXFAID1) || cmbCommunication1.SelectedItem != null)
            {
                CommunicationFXFA fxfa = new CommunicationFXFA
                {
                    Addr = txtAddress1.Text.ToString(),
                    CID = (ComboItem)cmbCommunication1.SelectedItem != null ? ((ComboItem)cmbCommunication1.SelectedItem).Value.ToString() : "",
                    Content = txtContent1.Text.ToString(),
                    SID = ID.Substring(0, 37) + "1",
                    CommunicateDate = txtCommunicateDate1.Text,
                    Frequency = txtFrenquence1.Text,
                    ID = FXFAID1
                };
                list.Add(fxfa);
            }
            if (!string.IsNullOrEmpty(FXFAID2) || cmbCommunication2.SelectedItem != null)
            {
                CommunicationFXFA fxfa = new CommunicationFXFA
                {
                    Addr = txtAddress2.Text.ToString(),
                    CID = (ComboItem)cmbCommunication2.SelectedItem != null ? ((ComboItem)cmbCommunication2.SelectedItem).Value.ToString() : "",
                    Content = txtContent2.Text.ToString(),
                    SID = ID.Substring(0, 37) + "1",
                    CommunicateDate = txtConmunicateDate2.Text,
                    Frequency = txtFrenquence2.Text,
                    ID = FXFAID2
                };
                list.Add(fxfa);
            }
            if (!string.IsNullOrEmpty(FXFAID3) || cmbCommunication3.SelectedItem != null)
            {
                CommunicationFXFA fxfa = new CommunicationFXFA
                {
                    Addr = txtAddress3.Text.ToString(),
                    CID = (ComboItem)cmbCommunication3.SelectedItem != null ? ((ComboItem)cmbCommunication3.SelectedItem).Value.ToString() : "",
                    Content = txtContent3.Text.ToString(),
                    SID = ID.Substring(0, 37) + "1",
                    CommunicateDate = txtConmunicateDate3.Text,
                    Frequency = txtFrenquence3.Text,
                    ID = FXFAID3
                };
                list.Add(fxfa);
            }
            JsonResult json = bll.SaveFXFA(list, out FXFAID1, out FXFAID2, out FXFAID3);
            MessageHelper.ShowRstMsg(json.result);
        }

        #endregion

        #region 方法
        /// <summary>
        /// 绑定listbox
        /// </summary>
        private void BindListBox()
        {
            var dt = bll.GetDataTable(ProjectId);
            superGridControl1.PrimaryGrid.DataSource = dt;
            LoadDropList();
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        private void LoadDropList()
        {
            ////频率下拉框
            //DataHelper.LoadDictItems(cmbFrequency1, DictCategory.Frequency);
            //DataHelper.LoadDictItems(cmbFrequency2, DictCategory.Frequency);
            //DataHelper.LoadDictItems(cmbFrequency3, DictCategory.Frequency);
            LoadCommunicationItems(cmbCommunication1, "");
            LoadCommunicationItems(cmbCommunication2, "");
            LoadCommunicationItems(cmbCommunication3, "");
        }
        /// <summary>
        /// 加载下拉框
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="Value"></param>
        private void LoadCommunicationItems(ComboBoxEx combobox,string Value)
        {
            var list = bll.GetCommunicationItems(ProjectId);
            foreach (DomainDLL.Communication c in list)
            {
                ComboItem item = new ComboItem();
                item.Text = c.Name;
                item.Value = c.ID;
                combobox.Items.Add(item);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void ClearAll()
        {
            txtCompanyName.Clear();
            txtDuty.Clear();
            txtEmail.Clear();
            txtName.Clear();
            txtPosition.Clear();
            txtQQ.Clear();
            txtTel.Clear();
            txtWechat.Clear();
            cbIspublic.Checked = false;
            dtiCREATED.Value = DateTime.MinValue;
            type = null;
            sendtype = null;
            ID = null;
            CREATED = DateTime.MinValue;

            txtAddress3.Clear();
            txtContent3.Clear();
            txtConmunicateDate3.Clear();
            cmbCommunication3.SelectedIndex = -1;
            txtFrenquence3.Clear();
            FXFAID3 = null;

            txtAddress2.Clear();
            txtContent2.Clear();
            txtFrenquence2.Clear();
            txtConmunicateDate2.Clear();
            cmbCommunication2.SelectedIndex = -1;
            FXFAID2 = null;

            txtAddress1.Clear();
            txtContent1.Clear();
            txtCommunicateDate1.Clear();
            txtFrenquence1.Clear();
            cmbCommunication1.SelectedIndex = -1;
            FXFAID1 = null;
        }
        #endregion

        /// <summary>
        /// 设置项目经理背景颜色
        /// 2017/06/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            List<GridElement> listRow = superGridControl1.PrimaryGrid.Rows.ToList();
            int type = 0;
            foreach (GridElement obj in listRow)
            {
                GridRow row = (GridRow)obj;
                type = int.Parse(row.GetCell("IsPublic").Value.ToString());
                if (type != 0)
                {
                    CellVisualStyles style = new CellVisualStyles();
                    style.Default.Background.Color1 = Color.Green;
                    row.CellStyles = style;
                }
            }
        }
    }
}
