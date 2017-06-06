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
using DomainDLL;
using BussinessDLL;
using System.Collections;
using DevComponents.Editors;
using ProjectManagement.Common;

namespace ProjectManagement
{
    /// <summary>
    ///  画面名：基本信息配置
    ///  Created：20170324（ChengMengjia）
    /// </summary>
    public partial class FormSetting : BaseForm
    {
        #region 业务类初期化
        SettingBLL bll = new SettingBLL();
        #endregion

        #region 画面变量
        string _dictNo = "";
        List<string> listContent;
        Dictionary<string, string> dicSendTo = new Dictionary<string, string>();
        Dictionary<string, string> dicCopyTo = new Dictionary<string, string>();
        #endregion

        #region 事件
        public FormSetting()
        {
            InitializeComponent();

            #region 配置绑定
            LoadWorkDir();
            LoadWeekly();
            #endregion

            //加载基础数据项
            foreach (DictCategory category in Enum.GetValues(typeof(DictCategory)))
            {
                ComboItem item = new ComboItem();
                item.Text = EnumsHelper.GetDescription(category);
                item.Value = (int)category;
                cbDictCategory.Items.Add(item);
            }
            cbDictCategory.SelectedIndexChanged += new System.EventHandler(LoadDictItems);
        }

        /// <summary>
        /// 加载数据项的子项列表
        /// Created：20170324（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadDictItems(object sender, EventArgs e)
        {
            ComboItem item = (ComboItem)cbDictCategory.SelectedItem;
            if (_dictNo.Equals(item.Value.ToString()))
                return;//点击的就是当前选中项目
            _dictNo = item.Value.ToString();
            List<DictItem> listD = bll.GetDictItems((DictCategory)int.Parse(_dictNo));
            gridDictItem.PrimaryGrid.DataSource = listD;
        }

        /// <summary>
        /// 基础数据子项编辑-清空
        /// Created：20170324（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearItem_Click(object sender, EventArgs e)
        {
            txtItemName.Clear();
            txtItemRemark.Clear();
            txtItemName.Tag = "";
            gridDictItem.GetSelectedRows().Select(false);//取消选择
        }


        /// <summary>
        /// 基础数据子项编辑-保存
        /// Created：20170324（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            DictItem item = new DictItem();
            item.ID = txtItemName.Tag == null ? "" : txtItemName.Tag.ToString().Trim();
            item.Name = txtItemName.Text.Trim();
            item.DictNo = _dictNo.Trim();
            item.Remark = txtItemRemark.Text.Trim();
            #region 判断空
            if (string.IsNullOrEmpty(item.DictNo))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "数据项");
                return;
            }
            if (string.IsNullOrEmpty(item.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "内容");
                return;
            }
            #endregion
            JsonResult result = bll.SaveItem(item);
            if (result.result)
            {
                btnClearItem_Click(sender, e);
                List<DictItem> listD = bll.GetDictItems((DictCategory)int.Parse(item.DictNo));
                gridDictItem.PrimaryGrid.DataSource = listD;
            }
            else
                MessageBox.Show(result.msg);
        }

        /// <summary>
        /// 基础数据子项列表单击事件-修改
        /// Created：20170324（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridDictItem_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            DevComponents.DotNetBar.SuperGrid.GridElement list = gridDictItem.GetSelectedRows()[0];
            string s = list.ToString();
            s = s.Replace("{", ",");
            s = s.Replace("}", ",");
            string[] listS = s.Split(',');
            txtItemName.Tag = listS[1].Trim();
            txtItemName.Text = listS[4].Trim();
            txtItemRemark.Text = listS[5].Trim();
        }


        /// <summary>
        /// 工作目录和用户名称-保存
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string UserName = txtUserName.Text.Trim();
            string WorkDir = txtWorkDir.Text.Trim();
            #region 判断空
            if (string.IsNullOrEmpty(UserName))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "用户名称");
                return;
            }
            if (string.IsNullOrEmpty(WorkDir))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "工作目录");
                return;
            }
            #endregion
            bool result = CommonHelper.SetConfigValue(ConstHelper.Config_UserName, UserName) && CommonHelper.SetConfigValue(ConstHelper.Config_WorkDir, WorkDir);
            MessageHelper.ShowRstMsg(result);
        }

        /// <summary>
        /// 工作目录选择
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    txtWorkDir.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 周报发送-文本框点击事件
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSendTo_Click(object sender, EventArgs e)
        {
            panelSend.Visible = !panelSend.Visible;
            panelCopyTo.Visible = false;
        }
        /// <summary>
        /// 周报抄送-文本框点击事件
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCopyTo_MouseClick(object sender, MouseEventArgs e)
        {
            panelCopyTo.Visible = !panelCopyTo.Visible;
            panelSend.Visible = false;
        }

        /// <summary>
        /// 外部空白处-鼠标点击事件
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HidePanel(object sender, MouseEventArgs e)
        {
            panelSend.Visible = false;
            panelCopyTo.Visible = false;
        }

        /// <summary>
        /// 发送人选择
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SendItem_Click(object sender, EventArgs e)
        {
            ReSetMembers(dicSendTo, txtSendTo, (CheckBoxItem)sender);
        }

        /// <summary>
        /// 抄送人选择
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyToItem_Click(object sender, EventArgs e)
        {
            ReSetMembers(dicCopyTo, txtCopyTo, (CheckBoxItem)sender);
        }

        /// <summary>
        /// 周报内容勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckItem_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            Setting setting = bll.GetSetting(ProjectId);
            CheckBoxItem item = (CheckBoxItem)sender;
            int index = int.Parse(item.Tag.ToString()) - 1;
            listContent[index] = item.Checked ? "1" : "0";
            string result = "";
            for (int i = 0; i < 8; i++)
                result += listContent[i] + ",";
            setting.WeeklyCheck = result;
            bll.SaveSetting(setting);
        }

        /// <summary>
        /// 模板点击查看事件
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FileHelper.OpenFile(FileHelper.GetUploadPath(UploadType.WeeklyModel, "", ""), ConstHelper.Config_WeeklyFileName);
        }

        /// <summary>
        /// 周报配置清除
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearWeekly_Click(object sender, EventArgs e)
        {
            LoadWeekly();
        }

        /// <summary>
        /// 周报配置保存
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveWeekly_Click(object sender, EventArgs e)
        {
            Setting setting = bll.GetSetting(ProjectId);
            setting.WeeklySend = txtSendTo.Tag == null ? "" : txtSendTo.Tag.ToString().Trim();
            setting.WeeklyCC = txtCopyTo.Tag == null ? "" : txtCopyTo.Tag.ToString().Trim();
            setting.WeeklyTitle = txtTitle.Text.Trim();
            setting.WeeklyContent = txtContent.Text.Trim();
            JsonResult result = bll.SaveSetting(setting);
            if (!result.result)
                MessageBox.Show(result.msg);
        }


        #endregion

        #region 方法
        /// <summary>
        /// 工作目录配置加载
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        void LoadWorkDir()
        {
            txtUserName.Text = CommonHelper.GetConfigValue(ConstHelper.Config_UserName);
            txtWorkDir.Text = CommonHelper.GetConfigValue(ConstHelper.Config_WorkDir);
        }

        /// <summary>
        /// 周报配置加载
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        void LoadWeekly()
        {
            dicSendTo.Clear();
            dicCopyTo.Clear();
            txtSendTo.Clear();
            txtCopyTo.Clear();
            txtTitle.Clear();
            txtContent.Clear();

            Setting setting = bll.GetSetting(ProjectId);
            txtTitle.Text = setting.WeeklyTitle;
            txtContent.Text = setting.WeeklyContent;
            #region 内容设置选项
            listContent = string.IsNullOrEmpty(setting.WeeklyCheck) ? new List<string> { "0", "0", "0", "0", "0", "0", "0", "0" } : setting.WeeklyCheck.Split(',').ToList();
            if (listContent.Count < 8)
                listContent = new List<string> { "0", "0", "0", "0", "0", "0", "0", "0" };
            CheckBoxItem ckitem;
            for (int i = 1; i <= 8; i++)
            {
                ckitem = (CheckBoxItem)itemPanel1.GetItem("ckItem" + i);
                ckitem.Checked = listContent[i - 1].Equals("1");
            }
            #endregion
            #region 发送
            List<Stakeholders> listSendTo = new StakeholdersBLL().GetList(ProjectId, null);//所有可选人
            string configSendTo = setting.WeeklySend;//配置里的发送人
            configSendTo = string.IsNullOrEmpty(configSendTo) ? "" : configSendTo;
            foreach (Stakeholders member in listSendTo)
            {
                CheckBoxItem item = new CheckBoxItem();
                item.Text = member.Name;
                item.Tag = member.ID;
                item.Click += SendItem_Click;
                if (configSendTo.Contains(member.ID))
                {
                    //设置里包含此人
                    item.Checked = true;
                    txtSendTo.Text += member.Name + ";";
                    txtSendTo.Tag += member.ID + ";";
                    dicSendTo.Add(member.ID, member.Name);
                }
                panelSend.Items.Add(item);
            }
            #endregion
            #region 抄送
            List<Stakeholders> listCopyTo = new StakeholdersBLL().GetList(ProjectId, null);//所有可选人
            string configCopyTo = setting.WeeklyCC;//配置里的发送人
            configCopyTo = string.IsNullOrEmpty(configCopyTo) ? "" : configCopyTo;
            foreach (Stakeholders member in listCopyTo)
            {
                CheckBoxItem item = new CheckBoxItem();
                item.Text = member.Name;
                item.Tag = member.ID;
                item.Click += CopyToItem_Click;
                if (configCopyTo.Contains(member.ID))
                {
                    //设置里包含此人
                    item.Checked = true;
                    txtCopyTo.Text += member.Name + ";";
                    txtCopyTo.Tag += member.ID + ";";
                    dicCopyTo.Add(member.ID, member.Name);
                }
                panelCopyTo.Items.Add(item);
            }
            #endregion
        }

        /// <summary>
        /// 设置保存
        ///  Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="txtValue"></param>
        /// <param name="ConfigName"></param>
        void SaveWeekly(string[] txtNames, string[] txtValues, string[] ConfigNames)
        {
            for (int i = 0; i < txtNames.Length; i++)
            {
                if (!CommonHelper.SetConfigValue(ConfigNames[i], txtValues[i]))
                {
                    MessageBox.Show(txtNames[i] + "保存失败！");
                    return;
                }
            }
            MessageHelper.ShowRstMsg(true);
        }



        /// <summary>
        /// 根据选择设置文本框内显示值
        /// Created：20170331（ChengMengjia）
        /// </summary>
        /// <param name="list"></param>
        /// <param name="txtbox"></param>
        /// <param name="ckItem"></param>
        void ReSetMembers(Dictionary<string, string> list, TextBox txtbox, CheckBoxItem ckItem)
        {
            if (ckItem.Checked)
                //选中
                list.Add(ckItem.Tag.ToString(), ckItem.Text);
            else
                //取消选中
                list.Remove(ckItem.Tag.ToString());
            string names = "";
            string ids = "";
            foreach (var d in list)
            {
                names += d.Value + ";";
                ids += d.Key + ";";
            }
            names = names.Length > 0 ? names.Substring(0, names.Length - 1) : names;
            ids = ids.Length > 0 ? ids.Substring(0, ids.Length - 1) : ids;
            txtbox.Text = names;
            txtbox.Tag = ids;
        }
        #endregion


    }
}
