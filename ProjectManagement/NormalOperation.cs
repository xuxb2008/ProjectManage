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
using CommonDLL;
using DevComponents.Editors;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace ProjectManagement
{
    /// <summary>
    /// 画面名：当前操作
    /// Created:20170329(ChengMengjia)
    /// </summary>
    public partial class NormalOperation : BaseForm
    {
        #region 业务类初期化
        WBSBLL bll = new WBSBLL();
        #endregion

        #region 画面变量
        DeliverablesJBXX _jbxx;
        NodeProgress _progress;

        List<DeliverablesWork> listWork;//责任人列表
        List<DeliverablesFiles> EmailFiles;
        #endregion

        #region 事件

        public NormalOperation()
        {
            InitializeComponent();
            init();
        }

        #region 节点
        /// <summary>
        /// 节点信息-清空
        /// Created：20170329（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearNode_Click(object sender, EventArgs e)
        {
            LoadNodeInfo();
        }

        /// <summary>
        /// 节点信息-保存
        /// Created：20170329（ChengMengjia）
        /// </summary>
        private void btnSaveNode_Click(object sender, EventArgs e)
        {
            CurrentNode.Name = txtNode.Text;
            #region 检查
            if (string.IsNullOrEmpty(CurrentNode.PID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(CurrentNode.ParentID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "上级节点");
                return;
            }
            if (string.IsNullOrEmpty(CurrentNode.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "节点名称");
                return;
            }
            #endregion
            JsonResult result = new WBSBLL().SaveNode(CurrentNode);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                txtNode2.Text = CurrentNode.Name;
                FileHelper.WBSMoveFloder(UploadType.WBS,CurrentNode.ID);//迁移文件夹
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
            }
            else
                CurrentNode = bll.GetNode(CurrentNode.ID);//恢复原有数据
        }
        #endregion

        #region 交付物基本信息
        /// <summary>
        /// 权值变化触发事件
        /// Created:20170329(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sdWeight_ValueChanged(object sender, EventArgs e)
        {
            sdWeight.Text = sdWeight.Value.ToString();
        }
        /// <summary>
        /// 开始或结束时间值变化
        /// Created:20170606(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dt_ValueChanged(object sender, EventArgs e)
        {
            if (dtStart.Value != null && dtEnd.Value != null)
            {
                if (dtEnd.Value < dtStart.Value)
                    dtEnd.Value = dtStart.Value;
                intWorkload.Value = DateHelper.ComputeWorkDays(dtStart.Value, dtEnd.Value);
            }

        }
        /// <summary>
        /// 交付物基本信息-保存
        /// Created：20170329（ChengMengjia）
        /// Updated：20170414（Xuxb）保存后刷新首页成果图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveJBXX_Click(object sender, EventArgs e)
        {
            if (GetEditManager(true))//如果填写无误
            {
                if (CurrentNode.PType == 1)
                    UpdateJBXX(listWork);
                else
                    AddJBXX(listWork);
            }

        }

        /// <summary>
        /// 交付物基本信息-清空
        /// Created：20170329（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearJBXX_Click(object sender, EventArgs e)
        {
            if (CurrentNode.PType == 1)
                LoadJBXX();
            else
                ClearJBXX();

        }

        #endregion

        #region  交付物附件
        /// <summary>
        /// 文件-选择
        /// Created：20170329（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = dialog.FileName;
                    txtFilePath.Tag = 1;
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtFileName.Text = temp[0];
                }
            }
        }

        /// <summary>
        /// 文件-编辑清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFile_Click(object sender, EventArgs e)
        {
            txtFilePath.Tag = 0;
            txtFilePath.Clear();
            txtFileName.Clear();
            txtFileName.Tag = "";
            txtFileDesc.Clear();
            gridFile.GetSelectedRows().Select(false);//取消选择
        }

        /// <summary>
        /// 文件-编辑保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            DeliverablesFiles entity = new DeliverablesFiles();
            entity.ID = txtFileName.Tag == null ? "" : txtFileName.Tag.ToString();
            entity.NodeID = CurrentNode.ID;
            entity.Path = txtFilePath.Text;
            entity.Name = txtFileName.Text;
            entity.Desc = txtFileDesc.Text;
            #region 检查
            if (string.IsNullOrEmpty(entity.Path))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "附件");
                return;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "附件名称");
                return;
            }
            #endregion

            #region 文件上传
            //判断是否有选择文件上传
            bool ReUpload = txtFilePath.Tag != null && txtFilePath.Tag.Equals(1);
            if (ReUpload)
                entity.Path = FileHelper.UploadFile(entity.Path, UploadType.WBS, ProjectId, CurrentNode.ID);
            #endregion

            if (string.IsNullOrEmpty(entity.Path))
                MessageHelper.ShowRstMsg(false);
            else
            {
                JsonResult result = bll.SaveFile(entity, ReUpload);
                MessageHelper.ShowRstMsg(result.result);
                if (result.result)
                {
                    btnClearFile_Click(sender, e);
                    LoadFile(false);

                    entity.Path = FileHelper.GetFilePath(UploadType.WBS, ProjectId, CurrentNode.ID, "") + entity.Path;
                    EmailFiles.Add(entity);
                    LoadEmailFile();
                }
            }
        }

        /// <summary>
        /// 附件列表的单元格点击触发事件
        /// Created:20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDownload")
            {
                string fileName = e.GridCell.GridRow.GetCell("Path").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }
                //文件下载
                FileHelper.DownLoadFile(UploadType.WBS, ProjectId, CurrentNode.ID, fileName);
            }
        }

        /// <summary>
        /// 附件列表行单击事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridFile_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            DevComponents.DotNetBar.SuperGrid.GridElement list = gridFile.GetSelectedRows()[0];
            string s = list.ToString();
            s = s.Replace("{", ",");
            s = s.Replace("}", ",");
            string[] listS = s.Split(',');
            txtFileName.Text = listS[2].Trim();
            txtFileDesc.Text = listS[3].Trim();
            txtFileName.Tag = listS[5].Trim();
            txtFilePath.Text = listS[6].Trim();
            txtFilePath.Tag = 0;
        }
        #endregion

        #region 进度
        /// <summary>
        ///  进度更改触发事件
        ///  Created：20170330(ChengMengjia)
        ///  Updated：20170414（Xuxb）保存后刷新首页成果图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProgress_Click(object sender, EventArgs e)
        {
            ButtonItem item = (ButtonItem)sender;
            if (!item.Checked)
                SaveProgress(int.Parse(item.Tag.ToString()));
            //重新加载首页的成果列表
            startPage.LoadProjectResult();
        }

        /// <summary>
        /// 清空进度说明
        /// Created：20170602（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearProgress_Click(object sender, EventArgs e)
        {
            txtDesc.Text = _progress == null ? "" : _progress.Desc;
        }

        /// <summary>
        /// 保存进度说明
        /// Created：20170602（ChengMengjia）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveProgress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProgressDesc.Text))
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "更新说明");
            else
                SaveProgress(null);
        }
        #endregion

        #region 邮件
        /// <summary>
        ///  发布信息-收件人新增
        /// Created：20170410(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSend_Click(object sender, EventArgs e)
        {
            EnterInfo dlg = new EnterInfo();
            dlg.StartPosition = FormStartPosition.CenterParent;
            DialogResult dr = dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                string Addr = dlg.GetVaule().Trim();
                if (CommonHelper.CheckEmail(Addr))
                    txtPSend.Text += Addr + ";";
            }
        }
        /// <summary>
        ///  发布信息-抄送人新增
        /// Created：20170410(ChengMengjia)
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
                    txtPCC.Text += Addr + ";";
            }
        }

        /// <summary>
        /// 邮件附件列表单元格点击事件
        /// Created：20170411(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridPFile_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Del")
            {
                EmailFiles = EmailFiles.Where(t => !(e.GridCell.GridRow.GetCell("ID").Value).ToString().Equals(t.ID)).ToList();
                LoadEmailFile();
            }
        }

        /// <summary>
        /// 邮件-添加附件
        /// Created：20170411(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPAdd_Click(object sender, EventArgs e)
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
        /// 邮件发送
        /// Created：20170411(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPSend_Click(object sender, EventArgs e)
        {
            try
            {
                #region 检查为空
                if (string.IsNullOrEmpty(txtPSend.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "收件人");
                    return;
                }
                if (string.IsNullOrEmpty(txtPTitle.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "主题");
                    return;
                }
                if (string.IsNullOrEmpty(txtPContent.Text))
                {
                    MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "内容");
                    return;
                }
                #endregion
                //添加附件
                List<Attachment> listA = new List<Attachment>();
                foreach (DeliverablesFiles obj in EmailFiles)
                {
                    string pathFileName = obj.Path;
                    string extName = Path.GetExtension(pathFileName).ToLower(); //获取扩展名
                    listA.Add((extName == ".rar" || extName == ".zip")
                        ? new Attachment(pathFileName, MediaTypeNames.Application.Zip)
                        : new Attachment(pathFileName, MediaTypeNames.Application.Octet));
                }
                EmailHelper email = new EmailHelper(txtPSend.Text, txtPCC.Text, null, txtPTitle.Text, false, txtPContent.Text, listA);
                email.Send();
                MessageBox.Show("发送成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！失败原因：" + ex.Message);
            }
        }
        #endregion

        #region 责任人

        /// <summary>
        /// 添加责任人
        /// Created：20170526(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddManager_Click(object sender, EventArgs e)
        {
            #region 计算剩余工作量
            int tmp = intWorkload.Value;//工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                tmp = tmp - int.Parse(cells[2]);
            }
            #endregion
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager("", tmp < 0 ? 0 : tmp, tmp < 0 ? 0 : tmp);
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                GetEditManager(false);//更新责任人列表
                bool IsExist = false;//是否列表中存在该责任人
                #region 查找该责任人 存在即修改
                foreach (var t in listWork)
                {
                    if (t.Manager.Equals(fmNewManager.ReturnValue.Manager.Substring(0, 36)))
                    {
                        IsExist = true;
                        t.ManagerName = fmNewManager.ReturnValue.ManagerName;
                        t.Workload += fmNewManager.ReturnValue.Workload;
                        t.ActualWorkload += fmNewManager.ReturnValue.ActualWorkload;
                        break;
                    }
                }
                #endregion
                #region 不存在即新增
                if (!IsExist)
                {
                    listWork.Add(new DeliverablesWork()
                    {
                        Manager = fmNewManager.ReturnValue.Manager.Substring(0, 36),
                        ManagerName = fmNewManager.ReturnValue.ManagerName,
                        Workload = fmNewManager.ReturnValue.Workload,
                        ActualWorkload = fmNewManager.ReturnValue.ActualWorkload
                    });
                }
                #endregion
                gridManager.PrimaryGrid.DataSource = listWork;
            }
        }

        /// <summary>
        /// 责任人-单元格点击事件
        ///  Created：20170526(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_CellClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "RowDel")
            {
                GetEditManager(false);//更新责任人列表
                listWork = listWork.Where(t => t.Manager != e.GridCell.GridRow.GetCell("Manager").Value.ToString()).ToList();
                gridManager.PrimaryGrid.DataSource = listWork;
            }
        }

        /// <summary>
        /// 责任人双击-修改
        ///  Created：20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridManager_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            string tmp = e.GridRow.ToString();
            tmp = tmp.Substring(tmp.LastIndexOf("{") + 1);
            tmp = tmp.Substring(0, tmp.LastIndexOf("}"));
            string[] cells = tmp.Trim().Split(',');
            ProjectManagement.Forms.WBS.NewManager fmNewManager = new Forms.WBS.NewManager(cells[0], int.Parse(cells[2]), int.Parse(cells[3]));
            if (fmNewManager.ShowDialog() == DialogResult.OK)
            {
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 0).Value = fmNewManager.ReturnValue.Manager;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 1).Value = fmNewManager.ReturnValue.ManagerName;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 2).Value = fmNewManager.ReturnValue.Workload;
                gridManager.PrimaryGrid.GetCell(e.GridRow.RowIndex, 3).Value = fmNewManager.ReturnValue.ActualWorkload;
                GetEditManager(false);
            }
        }

        #endregion

        #endregion

        #region 方法

        /// <summary>
        ///  初始化
        /// Created：20170525（ChengMengjia）
        /// </summary>
        public void init()
        {
            LoadNodeInfo();//当前节点信息加载
            SetProgress();//当前节点进度
            if (CurrentNode.PType == 1)
            {
                //是交付物节点
                panelNode.Enabled = false;
                panelContent.Enabled = true;
                panelPub.Enabled = true;
                LoadJBXX();
                LoadFile(true);
                LoadPubInfo();
            }
            else
            {
                //panelJFW.Enabled = false;
                //LoadManager();
                panelNode.Enabled = true;
                panelContent.Enabled = false;
                panelPub.Enabled = false;
                ClearJBXX();
            }
        }


        /// <summary>
        /// 根据选择设置文本框内显示值
        /// Created：20170410（ChengMengjia）
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
                names += d.Value + ",";
                ids += d.Key + ",";
            }
            names = names.Length > 0 ? names.Substring(0, names.Length - 1) : names;
            ids = ids.Length > 0 ? ids.Substring(0, ids.Length - 1) : ids;
            txtbox.Text = names;
            txtbox.Tag = ids;
        }

        /// <summary>
        /// 节点信息-加载
        /// Created：20170329（ChengMengjia）
        /// </summary>
        private void LoadNodeInfo()
        {
            txtParent.Text = bll.GetNode(CurrentNode.ParentID).Name;
            txtNode.Text = CurrentNode.Name;
            txtNode2.Text = CurrentNode.Name;
        }

        #region 交付物基本信息
        /// <summary>
        /// 交付物基本信息-加载
        /// Created：20170329（ChengMengjia）
        /// Updated：20170526（ChengMengjia） 责任人变为列表加载
        /// </summary>
        private void LoadJBXX()
        {
            _jbxx = bll.GetJBXX(CurrentNode.ID);
            txtNode2.Text = bll.GetNode(CurrentNode.ParentID).Name;
            txtNode2.Tag = _jbxx.ID;
            txtJFW.Text = _jbxx.Name;
            dtStart.Value = _jbxx.StarteDate == null ? DateTime.Now : (DateTime)_jbxx.StarteDate;
            dtEnd.Value = _jbxx.EndDate == null ? DateTime.Now : (DateTime)_jbxx.EndDate;
            intWorkload.Value = _jbxx.Workload == null ? 1 : (int)_jbxx.Workload;
            txtDesc.Text = _jbxx.Desc;
            //责任人
            listWork = bll.GetManagerWorks(_jbxx.ID);
            gridManager.PrimaryGrid.DataSource = listWork;
        }

        /// <summary>
        /// 交付物基本信息-新增
        /// Created：20170329（ChengMengjia）
        /// Updated：20170414（Xuxb）保存后刷新首页成果图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddJBXX(List<DeliverablesWork> listWork)
        {
            //新增交付物节点
            PNode node = new PNode()
            {
                PID = ProjectId,
                Name = txtJFW.Text,
                ParentID = CurrentNode.ID,
                PType = 1
            };
            DeliverablesJBXX entity = new DeliverablesJBXX()
            {
                Name = txtJFW.Text,
                StarteDate = dtStart.Value,
                EndDate = dtEnd.Value,
                Workload = intWorkload.Value,
                Weight = sdWeight.Value
            };

            #region 检查
            if (string.IsNullOrEmpty(node.PID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(node.ParentID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "所属节点");
                return;
            }
            if (string.IsNullOrEmpty(entity.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "交付物名称");
                return;
            }
            #endregion

            JsonResult result = new WBSBLL().AddJFWNode(node, entity, listWork);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                ClearJBXX();
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectResult();
            }
        }

        /// <summary>
        /// 交付物基本信息-更新
        /// Created：20170329（ChengMengjia）
        /// Updated：20170414（Xuxb）保存后刷新首页成果图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateJBXX(List<DeliverablesWork> listWork)
        {
            //交付物节点的修改
            _jbxx.Name = txtJFW.Text;
            _jbxx.StarteDate = dtStart.Value;
            _jbxx.EndDate = dtEnd.Value;
            _jbxx.Workload = intWorkload.Value;
            _jbxx.Desc = txtDesc.Text;
            _jbxx.Weight = sdWeight.Value;
            #region 检查
            if (string.IsNullOrEmpty(_jbxx.Name))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "交付物名称");
                return;
            }
            #endregion
            JsonResult result = bll.UpdateJBXX(_jbxx, listWork);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                CurrentNode.Name = _jbxx.Name;
                CurrentNode.ID = result.data.ToString();
                txtNode.Text = _jbxx.Name;
                FileHelper.WBSMoveFloder(UploadType.WBS,CurrentNode.ID);//迁移文件夹
                //主框更新
                MainFrame mainForm = (MainFrame)this.Parent.TopLevelControl;
                mainForm.RelaodTree();
                //重新加载首页的成果列表
                startPage.LoadProjectResult();
            }
            else
                _jbxx = bll.GetJBXX(CurrentNode.ID);//恢复原有数据
        }

        /// <summary>
        /// 交付物基本信息-清空
        /// Created：20170329（ChengMengjia）
        /// </summary>
        private void ClearJBXX()
        {
            txtJFW.Clear();
            dtStart.Value = DateTime.Parse("0001/1/1 0:00:00");
            dtEnd.Value = DateTime.Parse("0001/1/1 0:00:00");
            intWorkload.Value = 1;
            sdWeight.Value = 1;
            txtDesc.Clear();
            listWork = null;
            gridManager.PrimaryGrid.DataSource = null;
        }
#endregion

        #region 责任人
        /// <summary>
        /// 获取编辑的责任人列表
        /// Created:20170526(ChengMengjia)
        /// </summary>
        /// <param name="NeedCheck">是否需要检查工作量大小</param>
        /// <returns></returns>
        bool GetEditManager(bool NeedCheck)
        {
            listWork = new List<DeliverablesWork>();
            //责任人
            int totalWork = 0;//总的工作量
            List<DevComponents.DotNetBar.SuperGrid.GridElement> listManager = gridManager.PrimaryGrid.Rows.ToList();
            foreach (DevComponents.DotNetBar.SuperGrid.GridElement row in listManager)
            {
                string s = row.ToString();
                s = s.Substring(s.LastIndexOf("{") + 1);
                s = s.Substring(0, s.LastIndexOf("}"));
                string[] cells = s.Trim().Split(',');
                listWork.Add(new DeliverablesWork() { Manager = cells[0], ManagerName = cells[1], Workload = int.Parse(cells[2]), ActualWorkload = int.Parse(cells[3]) });
                totalWork += int.Parse(cells[2]);
            }
            if (NeedCheck)
            {
                if (totalWork > intWorkload.Value)
                {
                    MessageBox.Show("超过设置的总工作量，请检查！");
                    return false;
                }
                else if (totalWork < intWorkload.Value)
                {
                    MessageBox.Show("小于设置的总工作量，请检查！");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 进度
        /// <summary>
        /// 进度设置
        /// Created:20170330(ChengMengjia)
        /// </summary>
        void SetProgress()
        {
            btnProgress1.Checked = false;
            btnProgress2.Checked = false;
            btnProgress3.Checked = false;
            btnProgress4.Checked = false;
            btnProgress5.Checked = false;
            ButtonItem item;
            _progress = bll.GetProgress(CurrentNode.ID);
            if (_progress.PType != null)
            {
                item = (ButtonItem)bar1.GetItem("btnProgress" + _progress.PType.ToString());
                item.Checked = true;
            }
            txtProgressDesc.Text = _progress.Desc;
        }

        /// <summary>
        /// 进度更新保存
        ///  Created：20170330(ChengMengjia)
        /// </summary>
        /// <param name="Type"></param>
        private void SaveProgress(int? Type)
        {
            NodeProgress entity = _progress;
            entity.NodeID = CurrentNode.ID;
            if (Type != null)
                entity.PType = Type;
            entity.Desc = txtProgressDesc.Text;
            JsonResult result = bll.SaveProgress(entity);
            MessageBox.Show(result.msg);
            if (result.result)
            {
                _progress = entity;
                btnProgress1.Checked = false;
                btnProgress2.Checked = false;
                btnProgress3.Checked = false;
                btnProgress4.Checked = false;
                btnProgress5.Checked = false;
                ButtonItem item = (ButtonItem)bar1.GetItem("btnProgress" + (entity.PType == null ? 1 : (int)entity.PType));
                item.Checked = true;
            }
        }
        #endregion

        #region 交付物附件
        /// <summary>
        /// 交付物附件-列表加载
        /// Created：20170328(ChengMengjia)
        /// </summary>
        /// <param name="IsFirst"></param>
        private void LoadFile(bool IsFirst)
        {
            List<DeliverablesFiles> list = bll.GetFiles(CurrentNode.ID);
            int? i = 1;
            foreach (DeliverablesFiles file in list)
            {
                file.RowNo = i;
                i++;
            }
            gridFile.PrimaryGrid.DataSource = list;
            if (IsFirst)
            {
                //第一次加载 
                EmailFiles = list;
                string headPath = FileHelper.GetFilePath(UploadType.WBS, ProjectId, CurrentNode.ID, "");
                EmailFiles.ForEach(t => t.Path = headPath + t.Path);
                gridPFile.PrimaryGrid.DataSource = EmailFiles;
            }
        }
        #endregion

        #region 邮件
        /// <summary>
        /// 邮件附件-列表加载
        /// Created：20170411(ChengMengjia)
        /// </summary>
        private void LoadEmailFile()
        {
            int? i = 1;
            foreach (DeliverablesFiles file in EmailFiles)
            {
                file.RowNo = i;
                i++;
            }
            gridPFile.PrimaryGrid.DataSource = EmailFiles;
        }


        /// <summary>
        ///  发布信息-配置加载
        /// Created：20170410(ChengMengjia)
        /// </summary>
        private void LoadPubInfo()
        {
            #region 发送
            txtPSend.Text = "";
            IList<dynamic> listSendTo = new SettingBLL().GetSendToList(ProjectId);//配置里的发送人
            if (listSendTo != null)
                foreach (var member in listSendTo)
                {
                    if (string.IsNullOrEmpty(member.Email))
                        continue;
                    txtPSend.Text += member.Email + "(" + member.Name + ")" + ";";
                }
            #endregion
            #region 抄送
            txtPCC.Text = "";
            IList<dynamic> listCC = new SettingBLL().GetCCList(ProjectId);//配置里的抄送人
            if (listCC != null)
                foreach (var member in listCC)
                {
                    if (string.IsNullOrEmpty(member.Email))
                        continue;
                    txtPCC.Text += member.Email + "(" + member.Name + ")" + ";";
                }
            #endregion
        }
        #endregion

        #endregion







    }


}
