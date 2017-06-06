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
using ProjectManagement.Common;
using CommonDLL;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace ProjectManagement.Forms.Others
{
    public partial class WeeklyHistory : BaseForm
    {

        #region 业务类初期化
        ReportBLL bll = new ReportBLL();
        #endregion

        #region 画面变量
        List<Report_WeeklyFiles> listFile = new List<Report_WeeklyFiles>();
        string Title;
        string Content;
        string SendTo;
        string CopyTo;
        #endregion

        #region 事件
        public WeeklyHistory(Report_WeeklyFiles createFile)
        {
            InitializeComponent();
            #region 初次加载周报配置
            Setting setting = new SettingBLL().GetSetting(ProjectId);
            Title = ProjectName + "+" + DateTime.Now.ToString("yyyyMMdd") + "+周报" + CommonHelper.GetConfigValue(ConstHelper.Config_UserName);
            Content = setting.WeeklyContent;
            List<Stakeholders> listSendTo = new StakeholdersBLL().GetList(ProjectId, null);//所有可选人
            string configSendTo = setting.WeeklySend;//配置里的发送人
            configSendTo = string.IsNullOrEmpty(configSendTo) ? "" : configSendTo;
            foreach (Stakeholders member in listSendTo)
            {
                if (configSendTo.Contains(member.ID) && !string.IsNullOrEmpty(member.Email))
                    //设置里包含此人
                    SendTo += member.Email + ";";
            }
            List<Stakeholders> listCopyTo = new StakeholdersBLL().GetList(ProjectId, null);//所有可选人
            string configCopyTo = setting.WeeklyCC;//配置里的发送人
            configCopyTo = string.IsNullOrEmpty(configCopyTo) ? "" : configCopyTo;
            foreach (Stakeholders member in listCopyTo)
            {
                if (configCopyTo.Contains(member.ID) && !string.IsNullOrEmpty(member.Email))
                    //设置里包含此人
                    CopyTo += member.Email + ";";
            }
            #endregion
            LoadSetting();
            LoadHistory();
            if (createFile != null)
            {
                listFile.Add(createFile);
                gridFile.PrimaryGrid.DataSource = listFile;
            }
        }
        /// <summary>
        /// 发送
        /// Created:20170508(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Tag != null && txtTitle.Tag.ToString().Equals(txtTitle.Text))
                {
                    txtTitle.Text = Title;
                }

                #region 判断填写
                if (string.IsNullOrEmpty(txtSendTo.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收件人");
                    return;
                }
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "标题");
                    return;
                }
                //if (string.IsNullOrEmpty(txtContent.Text))
                //{
                //    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "内容");
                //    return;
                //}
                #endregion
                #region 邮件添加附件
                string addr_save = FileHelper.GetFilePath(UploadType.Report_Weekly, ProjectId, "", "")
                + DateTime.Now.ToString("yyyy年MM月") + "第" + CommonHelper.getWeekNumInMonth(DateTime.Now) + "周\\"; //周报文件夹路径
                if (!Directory.Exists(addr_save))
                    Directory.CreateDirectory(addr_save);

                List<Attachment> listA = new List<Attachment>();
                foreach (Report_WeeklyFiles obj in listFile)
                {
                    string dir = addr_save + Path.GetFileName(obj.Path);
                    if (!obj.Path.Equals(dir))
                    {
                        File.Copy(obj.Path, dir, true);//复制至项目周报文件夹
                        obj.Path = dir;
                    }
                    string extName = Path.GetExtension(obj.Path).ToLower(); //获取扩展名
                    listA.Add((extName == ".rar" || extName == ".zip")
                        ? new Attachment(obj.Path, MediaTypeNames.Application.Zip)
                        : new Attachment(obj.Path, MediaTypeNames.Application.Octet));
                }
                #endregion

                EmailHelper email = new EmailHelper(txtSendTo.Text, txtCopyTo.Text, null, txtTitle.Text, false, txtContent.Text, listA);
                email.Send();

                Report_Weekly entity = new Report_Weekly();
                entity.PID = ProjectId;
                entity.Title = txtTitle.Text;
                entity.SendTo = txtSendTo.Text;
                entity.CopyTo = txtCopyTo.Text;
                entity.Content = txtContent.Text;
                JsonResult result = bll.SaveWeeklyHistory(entity, listFile);
                if (result.result)
                {
                    MessageBox.Show("发送成功！");
                    LoadSetting();
                    LoadHistory();
                }
                else
                    MessageBox.Show(result.msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！失败原因：" + ex.Message);
            }
        }

        /// <summary>
        /// 查询按钮点击事件
        ///  Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }

        /// <summary>
        /// 添加文件按钮点击事件
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] temp = dialog.SafeFileName.Split('.');
                    listFile.Add(new Report_WeeklyFiles() { Name = temp[0], Path = dialog.FileName, RowNo = listFile.Count + 1 });
                    gridFile.PrimaryGrid.DataSource = listFile;
                }
            }
        }

        /// <summary>
        /// 附件列表单元格点击事件
        /// Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Del")
            {
                listFile = listFile.Where(t => !e.GridCell.GridRow.GetCell("Path").Value.ToString().Equals(t.Path)).ToList();
                for (int i = 0; i < listFile.Count; i++)
                {
                    listFile[i].RowNo = i + 1;
                }
                gridFile.PrimaryGrid.DataSource = listFile;
            }

        }
        #endregion

        #region 方法
        /// <summary>
        /// 清空邮件面板
        /// Created:20170508(ChengMengjia)
        /// </summary>
        private void LoadSetting()
        {
            txtContent.Text = Content;
            txtTitle.Text = Title;
            txtSendTo.Text = SendTo;
            txtCopyTo.Text = CopyTo;
            gridFile.PrimaryGrid.DataSource = null;
        }

        /// <summary>
        /// 加载历史
        /// Created:20170508(ChengMengjia)
        /// </summary>
        private void LoadHistory()
        {
            lboxHistory.Items.Clear();
            string start = dtStart.Text;
            string end = dtEnd.Text;
            string query = txtQuery.Text;
            DataTable dt = bll.getWeeklyHistory(ProjectId, start, end, query);
            foreach (DataRow row in dt.Rows)
            {
                ListBoxItem item = new ListBoxItem();
                item.Text = row["Title"].ToString();
                item.Tag = row["ID"].ToString();
                item.Click += HistoryItem_Click;
                lboxHistory.Items.Add(item);
            }
        }

        /// <summary>
        ///  周报历史选择
        ///  Created:20170509(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryItem_Click(object sender, EventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            Report_Weekly entity = bll.getWeeklyHistory(item.Tag.ToString());
            txtTitle.Text = entity.Title;
            txtTitle.Tag = entity.Title;
            txtContent.Text = entity.Content;
            txtCopyTo.Text = entity.CopyTo;
            txtSendTo.Text = entity.SendTo;
            listFile = bll.getWeeklyHistoryFiles(entity.ID);
            for (int i = 0; i < listFile.Count; i++)
            {
                listFile[i].RowNo = i + 1;
            }
            gridFile.PrimaryGrid.DataSource = listFile;

        }

        #endregion
    }
}
