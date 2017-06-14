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
using ProjectManagement.Common;
using BussinessDLL;
using CommonDLL;
using DevComponents.DotNetBar.SuperGrid;

namespace ProjectManagement.Forms.Subcontract
{
    /// <summary>
    /// 供应商管理
    /// 2017/4/10(zhuguanjun)
    /// </summary>
    public partial class Supplier : BaseForm
    {
        #region 业务类初始化
        SupplierBLL bll = new SupplierBLL();
        #endregion

        #region 变量
        string YYZZ = null;//营业执照
        string ZGZ = null;//一般纳税人资格证
        string DMZ = null;//组织机构代码证
        string YYZZtext = null;//营业执照
        string ZGZtext = null;//一般纳税人资格证
        string DMZtext = null;//组织机构代码证
        string ID = null;
        DateTime CREATED = DateTime.Today;

        string _fileYYZZName;
        string _fileZGZName;
        string _fileDMZName;
        #endregion

        #region 事件
        public Supplier()
        {
            InitializeComponent();
            dtiCREATED.Value = DateTime.Today;
            DataBind();
            LoadFile();
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ProjectId))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "项目");
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "供应商名称");
                return;
            }
            #endregion

            string id;
            DomainDLL.Supplier entity = new DomainDLL.Supplier();
            entity.Addr = txtAddr.Text.ToString();
            entity.LegalMan = txtLegalMan.Text.ToString();
            entity.Manager = txtManager.Text.ToString();
            entity.Name = txtName.Text.ToString();
            entity.Tel = txtTel.Text.ToString();
            entity.CREATED = CREATED;
            entity.ID = ID;
            entity.PID = ProjectId;
            entity.PathDMZ = DMZ;
            entity.PathYYZZ = YYZZ;
            entity.PathZGZ = ZGZ;
            ////上传组织机构代码证
            //if (!string.IsNullOrEmpty(DMZtext))
            //    entity.PathDMZ = FileHelper.UploadFile(DMZtext, UploadType.Supplier, ProjectId, null);
            ////上传营业执照
            //if (!string.IsNullOrEmpty(YYZZtext))
            //    entity.PathYYZZ = FileHelper.UploadFile(YYZZtext, UploadType.Supplier, ProjectId, null);
            ////上传纳税人资格证
            //if (!string.IsNullOrEmpty(ZGZtext))
            //    entity.PathZGZ = FileHelper.UploadFile(ZGZtext, UploadType.Supplier, ProjectId, null);
            ////代码证上传失败
            //if (string.IsNullOrEmpty(entity.PathDMZ) && !string.IsNullOrEmpty(DMZtext))
            //    return;
            ////营业执照上传失败
            //else if (string.IsNullOrEmpty(entity.PathYYZZ) && !string.IsNullOrEmpty(YYZZtext))
            //    return;
            ////资格证上传失败
            //else if (string.IsNullOrEmpty(entity.PathZGZ) && !string.IsNullOrEmpty(ZGZtext))
            //    return;
            //else
            {
                JsonResult json = bll.SaveSupplier(entity, out id);
                MessageHelper.ShowRstMsg(json.result);
                if (json.result)
                    Clear_Click(null, null);
                DataBind();
            }
        }

        /// <summary>
        /// 营业执照上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpYYZZ_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    YYZZtext = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// 一般纳税人资格证上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpZGZ_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ZGZtext = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// 组织机构代码证上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpDMZ_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DMZtext = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e)
        {
            txtAddr.Clear();
            txtLegalMan.Clear();
            txtManager.Clear();
            txtName.Clear();
            txtTel.Clear();
            DMZtext = null;
            YYZZtext = null;
            ZGZtext = null;
            DMZ = null;
            YYZZ = null;
            ZGZ = null;
            ID = null;
            LoadFile();
            CREATED = DateTime.Today;
            superGridControl1.PrimaryGrid.ClearSelectedRows();
        }

        /// <summary>
        /// 营业执照下载点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownYYZZ_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(YYZZ))
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //文件下载
            FileHelper.DownLoadFile(UploadType.Supplier, ProjectId, null, YYZZ);
        }

        /// <summary>
        /// 资格证下载点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownZGZ_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ZGZ))
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //文件下载
            FileHelper.DownLoadFile(UploadType.Supplier, ProjectId, null, ZGZ);
        }

        /// <summary>
        /// 代码证下载点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownDMZ_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DMZ))
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //文件下载
            FileHelper.DownLoadFile(UploadType.Supplier, ProjectId, null, DMZ);
        }

        /// <summary>
        /// Grid点击事件
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
            GridRow row = (GridRow)rows[0];
            txtAddr.Text = row.Cells["Addr"].Value.ToString();
            txtLegalMan.Text = row.Cells["LegalMan"].Value.ToString();
            txtManager.Text = row.Cells["Manager"].Value.ToString();
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtTel.Text = row.Cells["Tel"].Value.ToString();
            ZGZ = row.Cells["PathZGZ"].Value.ToString();
            YYZZ = row.Cells["PathYYZZ"].Value.ToString();
            DMZ = row.Cells["PathDMZ"].Value.ToString();
            ID = row.Cells["ID"].Value.ToString();
            dtiCREATED.Value = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
            LoadFile();
        }


        private void File_Upload(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ID))
            {
                MessageBox.Show("请选择供应商");
                return;
            }
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(3, 1));
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DomainDLL.Supplier entity = new DomainDLL.Supplier();
                    entity = bll.GetSupplier(ID);
                    switch (Type)
                    {
                        case 1:
                            entity.PathYYZZ = FileHelper.UploadFile(dialog.FileName, UploadType.Supplier, ProjectId, null);
                            _fileYYZZName = entity.PathYYZZ;
                            break;
                        case 2:
                            entity.PathZGZ = FileHelper.UploadFile(dialog.FileName, UploadType.Supplier, ProjectId, null);
                            _fileZGZName = entity.PathZGZ;
                            break;
                        case 3:
                            entity.PathDMZ = FileHelper.UploadFile(dialog.FileName, UploadType.Supplier, ProjectId, null);
                            _fileDMZName = entity.PathDMZ;
                            break;
                    }
                    if (string.IsNullOrEmpty(entity.PathYYZZ) && string.IsNullOrEmpty(entity.PathDMZ) && string.IsNullOrEmpty(entity.PathZGZ))
                        MessageHelper.ShowRstMsg(false);
                    else
                    {                        
                        JsonResult json = bll.SaveSupplier(entity, out ID);
                        MessageHelper.ShowRstMsg(json.result);
                        LoadFile();
                    }
                }
            }
        }

        /// <summary>
        /// 文件下载事件
        /// 2017/04/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void File_DownLoad(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(ID))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "分包合同");
                return;
            }
            #endregion
            ButtonX button = (ButtonX)sender;
            int Type = int.Parse(button.Name.Substring(4, 1));
            DomainDLL.Supplier entity = bll.GetSupplier(ID);
            if (entity == null)
            {
                MessageHelper.ShowMsg(MessageID.W000000005, MessageType.Alert);
                return;
            }

            //取得上传文件类型
            string fileName = string.Empty;
            switch (Type)
            {
                case 1:fileName = entity.PathYYZZ;
                    break;
                case 2:fileName = entity.PathZGZ;
                    break;
                case 3:fileName = entity.PathDMZ;
                    break;
            }
            
            FileHelper.DownLoadFile(UploadType.Supplier, ProjectId, null, fileName);
        }

        /// <summary>
        /// 相关文件-打开
        /// Created：20170330(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFOpen_Click(object sender, EventArgs e)
        {
            LinkLabel link = (LinkLabel)sender;

            switch (link.Name)
            {
                case "lblFile1":
                    FileHelper.OpenFile(UploadType.Supplier, ProjectId, null, _fileYYZZName);
                    break;
                case "lblFile2":
                    FileHelper.OpenFile(UploadType.Supplier, ProjectId, null, _fileZGZName);
                    break;
                case "lblFile3":
                    FileHelper.OpenFile(UploadType.Supplier, ProjectId, null, _fileDMZName);
                    break;
            }

        }
        #endregion

        #region 方法
        private void DataBind()
        {
            DataTable list = bll.GetSupplierList(ProjectId);
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void LoadFile()
        {
            DomainDLL.Supplier entity = new DomainDLL.Supplier();

            //营业执照
            entity = bll.GetSupplier(ID);
            if (entity !=null &&!string.IsNullOrEmpty(entity.PathYYZZ))
            {
                _fileYYZZName = entity.PathYYZZ;
                lblFile1.Show();
                btnD1.Show();
            }
            else
            {
                lblFile1.Hide();
                btnD1.Hide();
            }

            //资格证
            if (entity != null && !string.IsNullOrEmpty(entity.PathZGZ))
            {
                _fileZGZName = entity.PathZGZ;
                lblFile2.Show();
                btnD2.Show();
            }
            else
            {
                lblFile2.Hide();
                btnD2.Hide();
            }

            //代码证
            if (entity != null && !string.IsNullOrEmpty(entity.PathDMZ))
            {
                _fileDMZName = entity.PathDMZ;
                lblFile3.Show();
                btnD3.Show();
            }
            else
            {
                lblFile3.Hide();
                btnD3.Hide();
            }

        }
        #endregion
    }
}
