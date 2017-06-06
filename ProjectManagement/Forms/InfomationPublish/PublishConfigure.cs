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
using DevComponents.Editors;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Net.Mime;
using System.Threading;

namespace ProjectManagement.Forms.InfomationPublish
{
    // <summary>
    /// 画面名：信息发布配置
    /// Created：20170401(ChengMengjia)
    /// </summary>
    public partial class PublishConfigure : Office2007RibbonForm
    {
        #region 业务类初期化
        #endregion

        #region 画面变量
        #endregion

        #region 事件
        public PublishConfigure()
        {
            InitializeComponent();
            DataHelper.LoadDictItems(cbEmailType, DictCategory.Pub_EmailType);//邮箱类型
            LoadSetting();
        }
        /// <summary>
        /// 设置保存
        /// Created:20170401 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSet_Click(object sender, EventArgs e)
        {
            ComboItem item = (ComboItem)cbEmailType.SelectedItem;

            string[] txtNames = { "邮箱类型", "邮箱地址", "个人签名", "QQ配置", "微信配置", "短信配置" };
            string[] values ={
                                item == null? "":item.Value.ToString(),
                                txtEmail.Text,
                                txtSelfInfo.Text,
                                txtSelfInfo.Text,
                                txtQQ.Text,
                                txtWechat.Text,
                                txtTel.Text
                            };
            string[] ConfigNames = {ConstHelper.Pub_EmailType,ConstHelper.Pub_Email, ConstHelper.Pub_SelfInfo, 
                                       ConstHelper.Pub_QQ,ConstHelper.Pub_WeChat,ConstHelper.Pub_Tel};
            SaveSetting(txtNames, values, ConfigNames);
        }
        /// <summary>
        /// 设置清空
        /// Created:20170401 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSet_Click(object sender, EventArgs e)
        {
            LoadSetting();
        }
        /// <summary>
        /// 发送邮件
        /// Created:20170410 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            progressBarX1.Enabled = true;
            try
            {
                if (string.IsNullOrEmpty(txtESend.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收件人");
                    return;
                }
                progressBarX1.Value = 20;
                if (string.IsNullOrEmpty(txtEContent.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "内容");
                    return;
                }
                progressBarX1.Value = 40;

                //添加附件
                List<Attachment> listA = new List<Attachment>();
                foreach (var obj in listFile.Items)
                {
                    ListBoxItem item = (ListBoxItem)obj;
                    string pathFileName = item.Tag.ToString();
                    string extName = Path.GetExtension(pathFileName).ToLower(); //获取扩展名
                    listA.Add((extName == ".rar" || extName == ".zip")
                        ? new Attachment(pathFileName, MediaTypeNames.Application.Zip)
                        : new Attachment(pathFileName, MediaTypeNames.Application.Octet));
                }
                progressBarX1.Value = 60;
                EmailHelper email = new EmailHelper(txtESend.Text, txtECopy.Text, null, "发布配置测试", false, txtEContent.Text, listA);
                progressBarX1.Value = 70;
                email.Send();
                progressBarX1.Value = 100;
                MessageBox.Show("发送成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！失败原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 邮件清空
        /// Created:20170410 (ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearEmail_Click(object sender, EventArgs e)
        {
            txtESend.Clear();
            txtECopy.Clear();
            txtEContent.Clear();
            listFile.Items.Clear();
            progressBarX1.Value = 0;
        }

        /// <summary>
        /// 附件添加
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ListBoxItem item = new ListBoxItem();
                    string[] temp = dialog.SafeFileName.Split('.');
                    item.Text = temp[0];
                    item.Tag = dialog.FileName;
                    item.MouseDown += FileItem_MouseDown;
                    listFile.Items.Add(item);
                }
            }
        }
        /// <summary>
        /// 附件右键点击
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileItem_MouseDown(object sender, MouseEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point point = listFile.PointToClient(Cursor.Position);
                this.DelFileMenu.Tag = item.Tag;
                this.DelFileMenu.Show(listFile, point);

            }
            item.IsSelected = true;
        }

        /// <summary>
        /// 附件删除
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDel_Click(object sender, EventArgs e)
        {
            ListBoxItem item = (ListBoxItem)listFile.SelectedItem;
            listFile.Items.Remove(item);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置加载
        ///  Created:20170401 (ChengMengjia)
        /// </summary>
        void LoadSetting()
        {
            DataHelper.SetComboBoxSelectItemByValue(cbEmailType, CommonHelper.GetConfigValue(ConstHelper.Pub_EmailType));
            txtEmail.Text = CommonHelper.GetConfigValue(ConstHelper.Pub_Email);
            txtSelfInfo.Text = CommonHelper.GetConfigValue(ConstHelper.Pub_SelfInfo);
            txtQQ.Text = CommonHelper.GetConfigValue(ConstHelper.Pub_QQ);
            txtWechat.Text = CommonHelper.GetConfigValue(ConstHelper.Pub_WeChat);
            txtTel.Text = CommonHelper.GetConfigValue(ConstHelper.Pub_Tel);

        }
        /// <summary>
        /// 设置保存
        ///  Created：20170401（ChengMengjia）
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="txtValue"></param>
        /// <param name="ConfigName"></param>
        void SaveSetting(string[] txtNames, string[] txtValues, string[] ConfigNames)
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
        #endregion


    }
}
