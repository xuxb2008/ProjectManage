using BussinessDLL;
using CommonDLL;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.Editors;
using DomainDLL;
using ProjectManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ProjectManagement.Forms.Template
{
    public partial class UpdateTemplate : BaseForm
    {
        #region 业务逻辑初始化
        TempletTypeBLL bll = new TempletTypeBLL();
        #endregion

        #region
        private string extension;//文件扩展名
        private string fullfilename;//文件全名
        private string ID;
        private DateTime CREATED;
        #endregion
        public UpdateTemplate()
        {
            InitializeComponent();
            DataBind();
            DataBindTemplet();
        }

        /// <summary>
        /// 添加分类
        /// 2017/05/18(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveType_Click(object sender, EventArgs e)
        {
            TempletType entity = new DomainDLL.TempletType();
            entity.Name = txtTypeName.Text;
            entity.Desc = txtTypeDesc.Text;
            var json = bll.SaveTempletType(entity);
            MessageHelper.ShowRstMsg(json.result);
            if (json.result)
                btnClearType_Click(null, null);
            DataBind();
        }

        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearType_Click(object sender, EventArgs e)
        {
            txtTypeDesc.Clear();
            txtTypeName.Clear();
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="Value"></param>
        private void LoadCommunicationItems(ComboBoxEx combobox, string Value)
        {
            combobox.Items.Clear();
            var list = bll.GetTempletTypeList();
            foreach (DomainDLL.TempletType c in list)
            {
                ComboItem item = new ComboItem();
                item.Text = c.Name;
                item.Value = c.ID;
                combobox.Items.Add(item);
            }
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fullfilename = dialog.FileName;
                        txtTemplateSaveName.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
                        extension = Path.GetExtension(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        throw (ex);
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            #region 检查
            if (string.IsNullOrEmpty(cmbType.Text))
            {
                MessageHelper.ShowMsg(MessageID.W000000002, MessageType.Alert, "模板类型");
                return;
            }
            if (string.IsNullOrEmpty(txtTemplateSaveName.Text.ToString()))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "模板名称");
                return;
            }

            #endregion
            Templet entity = new Templet();
            ComboItem item = (ComboItem)cmbType.SelectedItem;
            if (item != null)
                entity.TypeID = item.Value.ToString();
            entity.ID = ID;
            entity.CREATED = CREATED;
            entity.Name = txtTemplateSaveName.Text;
            entity.Desc = txtTemplateDesc.Text;
            if (!string.IsNullOrEmpty(fullfilename))
                entity.FilePath = FileHelper.UploadFile(fullfilename, UploadType.Templet, null, null);

            if (string.IsNullOrEmpty(entity.FilePath) && !string.IsNullOrEmpty(fullfilename))
                return;
            else
            {
                JsonResult json = bll.SaveTemplet(entity);
                MessageHelper.ShowRstMsg(json.result);
                if (json.result)
                    btnClearTemplate_Click(null, null);
                DataBindTemplet();
            }
        }

        /// <summary>
        /// 清空模板信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearTemplate_Click(object sender, EventArgs e)
        {
            txtTemplateDesc.Clear();
            txtTemplateSaveName.Clear();
            cmbType.SelectedIndex = -1;

        }

        /// <summary>
        /// 选中一行
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

            txtTemplateDesc.Text = row.Cells["Desc"].Value == null ? "" : row.Cells["Desc"].Value.ToString();
            txtTemplateSaveName.Text = row.Cells["Name"].Value == null ? "" : row.Cells["Name"].Value.ToString();
            DataHelper.SetComboBoxSelectItemByValue(cmbType, row.Cells["TypeID"].Value == null ? "-1" : row.Cells["TypeID"].Value.ToString());            
            ID = row.Cells["ID"].Value.ToString();
            CREATED = Convert.ToDateTime(row.Cells["CREATED"].Value.ToString());
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            var keywords = textBoxX1.Text;
            var list = bll.GetTempletList(keywords);
            superGridControl1.PrimaryGrid.DataSource = list;
            superGridControl1.DataBindingComplete += new EventHandler<GridDataBindingCompleteEventArgs>(DataAsc);
        }

        /// <summary>
        /// 数据绑定模板类型
        /// </summary>
        private void DataBind()
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            List<DomainDLL.TempletType> list = bll.GetTempletTypeList();
            //superGridControl1.PrimaryGrid.DataSource = list;
            listBoxAdv1.DataSource = list;
            listBoxAdv1.DisplayMember = "Name";
            listBoxAdv1.ValueMember = "ID";

            //绑定下拉框
            LoadCommunicationItems(cmbType, "");
        }

        /// <summary>
        /// 数据绑定模板
        /// </summary>
        private void DataBindTemplet()
        {
            List<QueryField> qlist = new List<QueryField>();
            qlist.Add(new QueryField() { Name = "Status", Type = QueryFieldType.Numeric, Value = 1 });
            List<Templet> list = bll.GetTempletList();
            superGridControl1.PrimaryGrid.DataSource = list;
            superGridControl1.DataBindingComplete += new EventHandler<GridDataBindingCompleteEventArgs>(DataAsc);
        }

        /// <summary>
        /// 顺番编号
        /// 2017/05/19
        /// </summary>
        private void DataAsc(object sender, EventArgs e)
        {
            foreach (GridElement row in superGridControl1.PrimaryGrid.Rows)
            {
                ((GridRow)row).Cells["RowNo"].Value = ((GridRow)row).RowIndex + 1;
            }
        }

        
    }
}
