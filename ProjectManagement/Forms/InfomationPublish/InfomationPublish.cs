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
using CommonDLL;
using BussinessDLL;
using ProjectManagement.Common;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace ProjectManagement.Forms.InfomationPublish
{
    public partial class InfomationPublish : BaseForm
    {
        #region 业务类初期化
        WBSBLL wbsBll = new WBSBLL();
        PubInfoBLL pubBll = new PubInfoBLL();
        RoutineBLL routineBll = new RoutineBLL();
        TroubleBLL troubleBll = new TroubleBLL();
        #endregion

        #region 画面变量
        List<DeliverablesFiles> EmailFiles = new List<DeliverablesFiles>();//邮件附件
        #endregion

        #region 事件
        public InfomationPublish()
        {
            InitializeComponent();
            LoadTree();
            LoadPubInfo();
            LoadEmailFile();
        }

        /// <summary>
        /// 添加收件人
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSendTo_Click(object sender, EventArgs e)
        {
            EnterInfo dlg = new EnterInfo();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                string Addr = dlg.GetVaule().Trim();
                if (CommonHelper.CheckEmail(Addr))
                    txtSend.Text += Addr + ";";
            }
        }

        /// <summary>
        /// 添加抄送人
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCC_Click(object sender, EventArgs e)
        {
            EnterInfo dlg = new EnterInfo();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                string Addr = dlg.GetVaule().Trim();
                if (CommonHelper.CheckEmail(Addr))
                    txtCC.Text += Addr + ";";
            }
        }
        /// <summary>
        /// 添加邮件附件
        /// Created:20170527(ChengMengjia)
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
                    DeliverablesFiles entity = new DeliverablesFiles();
                    entity.ID = Guid.NewGuid().ToString();
                    entity.Path = dialog.FileName;
                    string[] temp = dialog.SafeFileName.Split('.');
                    entity.Name = temp[0];
                    EmailFiles.Add(entity);
                    LoadEmailFile();
                }
            }
        }

        /// <summary>
        /// 邮件附件列表单元格点击事件
        /// Created：20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Del")
            {
                EmailFiles = EmailFiles.Where(t => !(e.GridCell.GridRow.GetCell("ID").Value).ToString().Equals(t.ID)).ToList();
                LoadEmailFile();
            }
        }

        /// <summary>
        ///  邮件发送
        /// Created：20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                #region 检查
                if (string.IsNullOrEmpty(txtSend.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收件人");
                    return;
                }
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "主题");
                    return;
                }
                if (string.IsNullOrEmpty(txtContent.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "内容");
                    return;
                }
                #endregion
                #region 附件添加附件
                List<Attachment> listA = new List<Attachment>();
                foreach (DeliverablesFiles obj in EmailFiles)
                {
                    if (!string.IsNullOrEmpty(obj.NodeID))
                        obj.Path = FileHelper.GetWorkdir() + FileHelper.GetUploadPath(UploadType.WBS, ProjectId, obj.NodeID) + obj.Path;
                    string pathFileName = obj.Path;
                    string extName = Path.GetExtension(pathFileName).ToLower(); //获取扩展名
                    listA.Add((extName == ".rar" || extName == ".zip")
                        ? new Attachment(pathFileName, MediaTypeNames.Application.Zip)
                        : new Attachment(pathFileName, MediaTypeNames.Application.Octet));
                }
                #endregion
                EmailHelper email = new EmailHelper(txtSend.Text, txtCC.Text, null, txtTitle.Text, false, txtContent.Text, listA);
                email.Send();

                PubInfo entity = new PubInfo();
                entity.PID = ProjectId;
                entity.Title = txtTitle.Text;
                entity.SendTo = txtSend.Text;
                entity.CopyTo = txtCC.Text;
                entity.Content = txtContent.Text;
                JsonResult result = pubBll.SavePubInfo(entity, EmailFiles);
                if (result.result)
                    MessageBox.Show("发送成功！");
                else
                    MessageBox.Show(result.msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！失败原因：" + ex.Message);
            }
        }

        /// <summary>
        /// WBS节点选中/取消选中事件
        /// Created：20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wbsTree_AfterCheck(object sender, DevComponents.AdvTree.AdvTreeCellEventArgs e)
        {
            List<DevComponents.AdvTree.Node> checkedNodes = wbsTree.CheckedNodes;
            txtTitle.Text = checkedNodes.Count > 0 ? checkedNodes[0].Text : "";//以第一个选中节点的名字命名

            PNode node = JsonHelper.StringToEntity<PNode>(e.Cell.TagString);
            if (e.Cell.Checked)
            {
                switch (node.PType)
                {
                    case 1:
                        //交付物
                        List<DeliverablesFiles> listF = wbsBll.GetFiles(node.ID);
                        if (listF.Count > 0)
                        {
                            EmailFiles.AddRange(listF);
                            LoadEmailFile();
                        }
                        break;
                    case 2:
                        //日常工作
                        List<RoutineFiles> listR = routineBll.GetFilesByNodeID(node.ID);
                        if (listR.Count > 0)
                        {
                            foreach (var r in listR)
                                EmailFiles.Add(new DeliverablesFiles() { Name = r.Name, Path = r.Path, NodeID = node.ID.Substring(0, 36) });
                            LoadEmailFile();
                        }
                        break;
                    case 3:
                        //问题
                        List<TroubleFiles> listT = troubleBll.GetFilesByNodeID(node.ID, null);
                        if (listT.Count > 0)
                        {
                            foreach (var r in listT)
                                EmailFiles.Add(new DeliverablesFiles() { Name = r.Name, Path = r.Path, NodeID = node.ID.Substring(0, 36) });
                            LoadEmailFile();
                        }
                        break;

                }
            }
            else
            {
                int oldCount = EmailFiles.Count();
                //取消选中
                EmailFiles = EmailFiles.Where(t => !t.NodeID.Equals(node.ID.Substring(0, 36))).ToList();
                if (EmailFiles.Count != oldCount)
                    LoadEmailFile();
            }
        }

        /// <summary>
        ///关闭按钮点击事件
        /// Created：20170612(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
            mainForm.CloseTab(this.Name);
        }

        #endregion

        #region 方法
        /// <summary>
        /// 加载树节点
        /// Created:20170527(ChengMengjia)
        /// </summary>
        void LoadTree()
        {
            wbsTree.Nodes.Clear();
            List<PNode> listNode = wbsBll.GetNodes(ProjectId, null);
            IEnumerable<PNode> parentNode = null;
            parentNode = listNode.Where(t => string.IsNullOrEmpty(t.ParentID)).OrderBy(t => t.No);
            foreach (PNode parent in parentNode)
            {
                DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node()
                {
                    Name = parent.ID,
                    Text = parent.Name,
                    Tag = JsonHelper.EntityToString<PNode>(parent)
                };
                SetSubTreeData(listNode, parent, node);
                wbsTree.Nodes.Add(node);
            }
            wbsTree.ExpandAll();
        }

        /// <summary>
        /// 设定子节点
        /// Created:20170330(Xuxb)
        /// Updated:20170527(ChengMengjia) 增加Check框
        /// </summary>
        /// <param name="advTree1"></param>
        /// <param name="ProjectID"></param>
        void SetSubTreeData(IList<PNode> listNode, PNode parent, DevComponents.AdvTree.Node node)
        {
            string parentID = parent.ID.Substring(0, 36);
            IEnumerable<PNode> children = listNode.Where(t => t.ParentID == parentID).OrderBy(t => t.No);
            if (children.Count<PNode>() < 1)
            {
                return;
            }
            DevComponents.AdvTree.Node node2;
            foreach (PNode child in children)
            {
                node2 = new DevComponents.AdvTree.Node()
                {
                    CheckBoxVisible = true,
                    Name = child.ID,
                    Text = (child.PType == null || child.PType == 0) ? child.Name : child.Name + "(" + EnumsHelper.GetDescription((WBSPType)child.PType) + ")",
                    Tag = JsonHelper.EntityToString<PNode>(child)
                };
                SetSubTreeData(listNode, child, node2);
                node.Nodes.Add(node2);
            }
        }

        /// <summary>
        ///  发布信息-配置加载
        /// Created：20170527(ChengMengjia)
        /// </summary>
        void LoadPubInfo()
        {
            #region 发送
            IList<dynamic> listSendTo = new SettingBLL().GetSendToList(ProjectId);//配置里的发送人
            if (listSendTo != null)
                foreach (var member in listSendTo)
                {
                    if (string.IsNullOrEmpty(member.Email))
                        continue;
                    txtSend.Text += member.Email + "(" + member.Name + ")" + ";";
                }
            #endregion
            #region 抄送
            IList<dynamic> listCC = new SettingBLL().GetCCList(ProjectId);//配置里的抄送人
            if (listCC != null)
                foreach (var member in listCC)
                {
                    if (string.IsNullOrEmpty(member.Email))
                        continue;
                    txtCC.Text += member.Email + "(" + member.Name + ")" + ";";
                }
            #endregion
        }

        /// <summary>
        /// 邮件附件-列表加载
        /// Created：20170527(ChengMengjia)
        /// </summary>
        private void LoadEmailFile()
        {
            int? i = 1;
            foreach (DeliverablesFiles file in EmailFiles)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = EmailFiles;
        }
        #endregion
    }
}
