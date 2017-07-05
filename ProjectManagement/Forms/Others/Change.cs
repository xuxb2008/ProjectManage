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
using CommonDLL;
using DomainDLL;
using DevComponents.DotNetBar.SuperGrid;

namespace ProjectManagement.Forms.Others
{
    public partial class Change : BaseForm
    {
        #region 业务初始化
        ChangeBLL bll = new ChangeBLL();
        #endregion

        #region 变量
        private string CHANGEDATEID = null;//版本ID
        private string _changedateid = null;//实际id

        private string CHANGENEEDID = null;//版本ID
        private string _changeneedid = null;//实际id

        private string CHANGERANGEID = null;//版本ID
        private string _changerangeid = null;//实际id

        private string _filedateid = null;//日期变更附件实际id
        private string _fileneedid = null;//需求变更附件实际id
        private string _filerangeid = null;//范围变更附件实际id
        #endregion

        #region 构造
        public Change()
        {
            InitializeComponent();
            BindData((int)ChangeType.Date);
            BindData((int)ChangeType.Need);
            BindData((int)ChangeType.Range);
        }
        #endregion

        #region 绑定变更
        /// <summary>
        /// 绑定数据
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        private void BindData(int Type)
        {
            var dt = bll.GetChangeList(Type, ProjectId);
            switch (Type)
            {
                case (int)ChangeType.Date:
                    superDate.PrimaryGrid.DataSource = dt;
                    break;
                case (int)ChangeType.Need:
                    superNeed.PrimaryGrid.DataSource = dt;
                    break;
                case (int)ChangeType.Range:
                    superRange.PrimaryGrid.DataSource = dt;
                    break;
                default:
                    break;
            } 
        }
        #endregion

        #region 绑定附件
        /// <summary>
        /// 绑定附件
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="Type"></param>
        private void BindFile(int Type)
        {
            DataTable dt = new DataTable();
            switch (Type)
            {
                case (int)ChangeType.Date:
                    dt = bll.GetChangeFilesList(Type, CHANGEDATEID);
                    superGridControl1.PrimaryGrid.DataSource = dt;
                    break;
                case (int)ChangeType.Need:
                    dt = bll.GetChangeFilesList(Type, CHANGENEEDID);
                    superGridControl2.PrimaryGrid.DataSource = dt;
                    break;
                case (int)ChangeType.Range:
                    dt = bll.GetChangeFilesList(Type, CHANGERANGEID);
                    superGridControl3.PrimaryGrid.DataSource = dt;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 时间变更

        #region 时间保存
        /// <summary>
        /// 时间变更保存事件
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveChange_Click(object sender, EventArgs e)
        {
            DomainDLL.Change entity = new DomainDLL.Change();
            entity.ID = _changedateid;
            entity.Type = (int)ChangeType.Date;
            entity.Name = txtName.Text;
            entity.Payment = txtPayment.Text;
            entity.PID = ProjectId;
            entity.Reason = txtReason.Text;
            entity.AfterInfo = dtiAfter1.Value.ToShortDateString() + "-" + dtiAfter2.Value.ToShortDateString();
            entity.BeforeInfo = dtiBefore1.Value.ToShortDateString() + "-" + dtiBefore2.Value.ToShortDateString();
            entity.Cost = txtCost.Text;
            var result = bll.Save(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                BindData((int)ChangeType.Date);
                _changedateid = result.data.ToString();//实际id
                CHANGEDATEID = _changedateid.Substring(0, 36) + "-1";//原始版本id
            }
            ClearDate(false);//只清除文本框
        }
        #endregion

        #region 时间清空 
        /// <summary>
        /// 时间变更清空事件
        /// 2017/04/17(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearChange_Click(object sender, EventArgs e)
        {
            ClearDate(true);//清空文本框和内存中的时间变更id

            ClearDateFile();//清除附件文本框
        }
        #endregion

        #region 时间清空方法
        /// <summary>
        /// 清空时间
        /// </summary>
        private void ClearDate(bool IsFlag)
        {
            txtCost.Clear();
            txtPayment.Clear();
            txtName.Clear();
            dtiAfter1.Value = DateTime.MinValue;
            dtiAfter2.Value = DateTime.MinValue;
            dtiBefore1.Value = DateTime.MinValue;
            dtiBefore2.Value = DateTime.MinValue;
            txtReason.Clear();
            superDate.PrimaryGrid.ClearSelectedRows();
            if (IsFlag)
            {
                _changedateid = string.Empty;
                CHANGEDATEID = string.Empty;
            }
        }

        /// <summary>
        /// 清空时间附件
        /// </summary>
        private void ClearDateFile()
        {
            superGridControl1.PrimaryGrid.DataSource = null;
            txtDateFileDesc.Clear();
            txtDateFileName.Clear();
            txtDateFilePath.Clear();
            _filedateid = null;
        }
        #endregion

        #region 时间附件保存
        /// <summary>
        /// 时间变更附件保存
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(CHANGEDATEID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "变更信息");
                return;
            }
            #endregion

            DomainDLL.ChangeFiles entity = new DomainDLL.ChangeFiles();
            entity.ChangeID = CHANGEDATEID;
            entity.ID = _filedateid;
            entity.Desc = txtDateFileDesc.Text;
            entity.Name = txtDateFileName.Text;
            entity.Path = txtDateFilePath.Text;
            var result = bll.SaveFile(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
                BindFile((int)ChangeType.Date);
            btnClearFile_Click(null, null);
        }
        #endregion

        #region 时间附件上传
        /// <summary>
        /// 时间附件上传
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDateFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtDateFileName.Text = temp[0];
                    string path = string.Empty;
                    if (!string.IsNullOrEmpty(dialog.FileName))
                        path = FileHelper.UploadFile(dialog.FileName, UploadType.Change, ProjectId, null);
                    if (!string.IsNullOrEmpty(path))
                        txtDateFilePath.Text = path;
                }
            }
        }
        #endregion

        #region 时间附件清空
        /// <summary>
        /// 时间变更附件清空事件
        /// 2017/04/19(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFile_Click(object sender, EventArgs e)
        {
            _filedateid = null;
            txtDateFileDesc.Clear();
            txtDateFileName.Clear();
            txtDateFilePath.Clear();
            superGridControl1.PrimaryGrid.ClearSelectedRows();
        }
        #endregion

        #region 时间列表点击
        /// <summary>
        /// 时间变更列表行点击事件
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superDate_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {
            var rows = superDate.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superDate.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            btnClearFile_Click(null, null);

            GridRow row = (GridRow)rows[0];
            DomainDLL.Change change = new DomainDLL.Change();
            DataTable files = new DataTable();
            bll.GetChangeInfo(row.Cells["ID"].Value.ToString(), out change, out files);
            CHANGEDATEID = row.Cells["ID"].Value.ToString();
            if (change != null)
            {
                txtCost.Text = change.Cost;
                txtName.Text = change.Name;
                txtPayment.Text = change.Payment;
                txtReason.Text = change.Reason;
                string[] temp = change.AfterInfo.Split('-');
                if (temp != null && temp.Count() == 2)
                {
                    dtiAfter1.Value = Convert.ToDateTime(temp[0]);
                    dtiAfter2.Value = Convert.ToDateTime(temp[1]);
                }
                string[] tempb = change.BeforeInfo.Split('-');
                if (tempb != null && tempb.Count() == 2)
                {
                    dtiBefore1.Value = Convert.ToDateTime(tempb[0]);
                    dtiBefore2.Value = Convert.ToDateTime(tempb[1]);
                }
                _changedateid = change.ID;//实际id
            }
            superGridControl1.PrimaryGrid.DataSource = files;
        }
        #endregion

        #region 时间附件行点击
        /// <summary>
        /// 时间变更附件行点击
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            txtDateFileDesc.Text = row.Cells["Desc"].Value.ToString();
            txtDateFileName.Text = row.Cells["Name"].Value.ToString();
            txtDateFilePath.Text = row.Cells["Path"].Value.ToString();
            _filedateid = row.Cells["ID"].Value.ToString();
        }
        #endregion

        #region 时间附件下载
        /// <summary>
        /// 时间变更附件下载点击
        /// 2017/04/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Down")
            {
                if (e.GridCell.GridRow.GetCell("Path").Value == null)
                {
                    MessageBox.Show("没有文件！");
                    return;
                }
                string fileName = e.GridCell.GridRow.GetCell("Path").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.Change, ProjectId, null, fileName);
            }
        }
        #endregion

        #endregion

        #region 需求变更

        #region 需求保存
        /// <summary>
        /// 需求变更保存事件
        /// 2017/04/19(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNeed_Click(object sender, EventArgs e)
        {
            DomainDLL.Change entity = new DomainDLL.Change();
            entity.ID = _changeneedid;
            entity.Type = (int)ChangeType.Need;
            entity.Name = txtNeedName.Text;
            entity.Payment = txtNeedPayment.Text;
            entity.PID = ProjectId;
            entity.Reason = txtNeedReason.Text;
            entity.AfterInfo = txtNeedAfter.Text;
            entity.BeforeInfo = txtNeedBefore.Text;
            entity.Cost = txtNeedCost.Text;
            var result = bll.Save(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                _changeneedid = result.data.ToString();//实际id
                CHANGENEEDID = _changeneedid.Substring(0, 36) + "-1";//原始版本id
                BindData((int)ChangeType.Need);
            }
            ClearNeed(false);//不会干涉附件信息
        }
        #endregion

        #region 需求清空
        /// <summary>
        /// 需求变更清空事件
        /// 2017/04/19(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearNeed_Click(object sender, EventArgs e)
        {
            ClearNeed(true);
            ClearNeedFile();
        }
        #endregion

        #region 需求清空方法
        /// <summary>
        /// 清空需求
        /// </summary>
        private void ClearNeed(bool IsFlag)
        {
            txtNeedCost.Clear();
            txtNeedPayment.Clear();
            txtNeedName.Clear();
            txtNeedAfter.Clear();
            txtNeedBefore.Clear();
            txtNeedReason.Clear();
            superNeed.PrimaryGrid.ClearSelectedRows();
            if (IsFlag)
            {
                _changeneedid = string.Empty;
                CHANGENEEDID = string.Empty;
            }
        }

        /// <summary>
        /// 清空需求附件
        /// </summary>
        private void ClearNeedFile()
        {
            superGridControl2.PrimaryGrid.DataSource = null;
            txtNeedFileDesc.Clear();
            txtNEEDFileName.Clear();
            txtNEEDFilePath.Clear();
            _fileneedid = null;
        }
        #endregion

        #region 需求附件保存
        /// <summary>
        /// 需求变更附件保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveNeedFile_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(CHANGENEEDID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "变更信息");
                return;
            }
            #endregion

            DomainDLL.ChangeFiles entity = new DomainDLL.ChangeFiles();
            entity.ChangeID = CHANGENEEDID;//变更版本id
            entity.ID = _fileneedid;//附件id
            entity.Desc = txtNeedFileDesc.Text;
            entity.Name = txtNEEDFileName.Text;
            entity.Path = txtNEEDFilePath.Text;
            var result = bll.SaveFile(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
                BindFile((int)ChangeType.Need);
            btnClearNeedFile_Click(null, null);
        }
        #endregion

        #region 需求附件上传
        /// <summary>
        /// 需求附件上传
        /// 2017/04/19（zhuguanjun0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNeedFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtNEEDFileName.Text = temp[0];
                    string path = string.Empty;
                    if (!string.IsNullOrEmpty(dialog.FileName))
                        path = FileHelper.UploadFile(dialog.FileName, UploadType.Change, ProjectId, null);
                    if (!string.IsNullOrEmpty(path))
                        txtNEEDFilePath.Text = path;
                }
            }
        }
        #endregion

        #region 需求附件清空
        /// <summary>
        /// 需求附件清空
        /// 2017/04/17(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearNeedFile_Click(object sender, EventArgs e)
        {
            txtNEEDFilePath.Clear();
            txtNEEDFileName.Clear();
            txtNeedFileDesc.Clear();
            _fileneedid = null;
            superGridControl2.PrimaryGrid.ClearSelectedRows(); ;
        }
        #endregion

        #region 需求列表点击
        /// <summary>
        /// 需求列表点击
        /// 2017/04/19(zhuagunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superNeed_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superNeed.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superNeed.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            btnClearNeedFile_Click(null, null);

            GridRow row = (GridRow)rows[0];
            DomainDLL.Change change = new DomainDLL.Change();
            DataTable files = new DataTable();
            bll.GetChangeInfo(row.Cells["ID"].Value.ToString(), out change, out files);
            CHANGENEEDID = row.Cells["ID"].Value.ToString();
            if (change != null)
            {
                txtNeedCost.Text = change.Cost;
                txtNeedName.Text = change.Name;
                txtNeedPayment.Text = change.Payment;
                txtNeedReason.Text = change.Reason;
                txtNeedAfter.Text = change.AfterInfo;
                txtNeedBefore.Text = change.BeforeInfo;
                _changeneedid = change.ID;//实际id
            }
            superGridControl2.PrimaryGrid.DataSource = files;
        }
        #endregion

        #region 需求附件列表点击
        /// <summary>
        /// 需求附件列表行点击
        /// 2017/04/19(zhugaunjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl2_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl2.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl2.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            txtNeedFileDesc.Text = row.Cells["Desc"].Value.ToString();
            txtNEEDFileName.Text = row.Cells["Name"].Value.ToString();
            txtNEEDFilePath.Text = row.Cells["Path"].Value.ToString();
            _fileneedid = row.Cells["ID"].Value.ToString();
        }
        #endregion

        #region 需求附件下载
        /// <summary>
        /// 需求附件列表单元格点击
        /// 2017/04/19(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl2_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Down")
            {
                if (e.GridCell.GridRow.GetCell("Path").Value == null)
                {
                    MessageBox.Show("没有文件！");
                    return;
                }
                string fileName = e.GridCell.GridRow.GetCell("Path").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.Change, ProjectId, null, fileName);
            }
        }
        #endregion

        #endregion

        #region 范围变更
        #region 需求列表点击
        /// <summary>
        /// 范围行点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superRange_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superRange.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superRange.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            btnClearRangeFile_Click(null, null);

            GridRow row = (GridRow)rows[0];
            DomainDLL.Change change = new DomainDLL.Change();
            DataTable files = new DataTable();
            bll.GetChangeInfo(row.Cells["ID"].Value.ToString(), out change, out files);
            CHANGERANGEID = row.Cells["ID"].Value.ToString();//版本id
            if (change != null)
            {
                txtRangeCost.Text = change.Cost;
                txtRangeName.Text = change.Name;
                txtRangePayment.Text = change.Payment;
                txtRangeReason.Text = change.Reason;
                txtRangeAfter.Text = change.AfterInfo;
                txtRangeBefore.Text = change.BeforeInfo;
                _changerangeid = change.ID;//实际id
            }
            superGridControl3.PrimaryGrid.DataSource = files;
        }
        #endregion

        #region 范围保存
        /// <summary>
        /// 范围保存
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveRange_Click(object sender, EventArgs e)
        {
            DomainDLL.Change entity = new DomainDLL.Change();
            entity.ID = _changerangeid;
            entity.Type = (int)ChangeType.Range;
            entity.Name = txtRangeName.Text;
            entity.Payment = txtRangePayment.Text;
            entity.PID = ProjectId;
            entity.Reason = txtRangeReason.Text;
            entity.AfterInfo = txtRangeAfter.Text;
            entity.BeforeInfo = txtRangeBefore.Text;
            entity.Cost = txtRangeCost.Text;
            var result = bll.Save(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
            {
                _changerangeid = result.data.ToString();//实际id
                CHANGERANGEID = _changerangeid.Substring(0, 36) + "-1";//原始版本id
                BindData((int)ChangeType.Range);
            }
            ClearRange(false);//不会干涉附件信息
        }
        #endregion

        #region 范围清空方法
        /// <summary>
        /// 清空范围
        /// </summary>
        private void ClearRange(bool IsFlag)
        {
            txtRangeCost.Clear();
            txtRangePayment.Clear();
            txtRangeName.Clear();
            txtRangeAfter.Clear();
            txtRangeBefore.Clear();
            txtRangeReason.Clear();
            superRange.PrimaryGrid.ClearSelectedRows();
            if (IsFlag)
            {
                _changerangeid = string.Empty;
                CHANGERANGEID = string.Empty;
            }
        }

        /// <summary>
        /// 清空范围附件
        /// </summary>
        private void ClearRangeFile()
        {
            superGridControl3.PrimaryGrid.DataSource = null;
            txtRangeFileDesc.Clear();
            txtRangeFileName.Clear();
            txtRangeFilePath.Clear();
            _filerangeid = null;
        }
        #endregion

        #region 范围清空
        /// <summary>
        /// 范围清空
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRange_Click(object sender, EventArgs e)
        {
            ClearRange(true);
            ClearRangeFile();
        }
        #endregion

        #region 范围附件上传
        /// <summary>
        /// 范围文件上传
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRangeFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string[] temp = dialog.SafeFileName.Split('.');
                    txtRangeFileName.Text = temp[0];
                    string path = string.Empty;
                    if (!string.IsNullOrEmpty(dialog.FileName))
                        path = FileHelper.UploadFile(dialog.FileName, UploadType.Change, ProjectId, null);
                    if (!string.IsNullOrEmpty(path))
                        txtRangeFilePath.Text = path;
                }
            }
        }
        #endregion

        #region 范围附件保存
        /// <summary>
        /// 范围附件保存
        /// 2017/04/20(zhuguanjuns)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveRangeFile_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(CHANGERANGEID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "变更信息");
                return;
            }
            #endregion

            DomainDLL.ChangeFiles entity = new DomainDLL.ChangeFiles();
            entity.ChangeID = CHANGERANGEID;
            entity.ID = _filerangeid;
            entity.Desc = txtRangeFileDesc.Text;
            entity.Name = txtRangeFileName.Text;
            entity.Path = txtRangeFilePath.Text;
            var result = bll.SaveFile(entity);
            MessageHelper.ShowRstMsg(result.result);
            if (result.result)
                BindFile((int)ChangeType.Range);
            btnClearRangeFile_Click(null, null);
        }
        #endregion

        #region 清空范围附件
        /// <summary>
        /// 清空范围附件
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearRangeFile_Click(object sender, EventArgs e)
        {
            _filerangeid = null;
            txtRangeFileDesc.Clear();
            txtRangeFileName.Clear();
            txtRangeFilePath.Clear();
            superGridControl3.PrimaryGrid.ClearSelectedRows();
        }
        #endregion

        #region 下载范围附件
        /// <summary>
        /// 下载范围
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl3_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (e.GridCell.GridColumn.Name == "Down")
            {
                if (e.GridCell.GridRow.GetCell("Path").Value == null)
                {
                    MessageBox.Show("没有文件！");
                    return;
                }
                string fileName = e.GridCell.GridRow.GetCell("Path").Value.ToString();
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                    return;
                }

                //文件下载
                FileHelper.DownLoadFile(UploadType.Change, ProjectId, null, fileName);
            }
        }
        #endregion

        #region 范围附件行点击
        /// <summary>
        /// 范围附件行点击
        /// 2017/04/20(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl3_RowClick(object sender, GridRowClickEventArgs e)
        {
            var rows = superGridControl3.PrimaryGrid.GetSelectedRows();
            if (rows.Count != 1)
            {
                MessageBox.Show("请选择一行");
                superGridControl3.PrimaryGrid.ClearSelectedColumns();
                return;
            }
            GridRow row = (GridRow)rows[0];
            txtRangeFileDesc.Text = row.Cells["Desc"].Value.ToString();
            txtRangeFileName.Text = row.Cells["Name"].Value.ToString();
            txtRangeFilePath.Text = row.Cells["Path"].Value.ToString();
            _filerangeid = row.Cells["ID"].Value.ToString();
        }
        #endregion

        #endregion

    }
}
