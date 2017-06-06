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
using ProjectManagement.Common;
using DomainDLL;
using BussinessDLL;
using System.IO;

namespace ProjectManagement.Forms.Project
{
    /// <summary>
    ///  画面名：创建新项目
    /// Created:20170323(ChengMengjia)
    /// </summary>
    public partial class NewProject : BaseForm
    {
        #region 业务类初期化
        ProjectBLL bll = new ProjectBLL();
        bool IsAdd;//是否为新增项目
        #endregion

        #region 事件
        public NewProject(bool isAdd)
        {
            IsAdd = isAdd;
            InitializeComponent();
            if (IsAdd)
            {
                groupPanel1.Text = "创建新项目";
            }
            else
            {
                groupPanel1.Text = "项目信息修改";
                txtName.Text = ProjectName;
                txtNo.Text = ProjectNo;
            }
            txtName.Focus();
        }

        /// <summary>
        /// 保存
        /// Created:20170323(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            string newName = txtName.Text;
            string newNo = txtNo.Text;
            #region 检查空
            if (string.IsNullOrEmpty(newNo))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "项目编号");
                //开始，liuxxf
                //如果编号为空时，则编号文本框直接获取焦点
                txtNo.Focus();
                //结束
                return;
            }
            if (string.IsNullOrEmpty(newName))
            {
                MessageHelper.ShowMsg(MessageID.W000000001, MessageType.Alert, "项目名称");
                //开始，liuxxf
                //如果名字为空时，则名字文本框直接获取焦点
                txtName.Focus();
                //结束
                return;
            }
            #endregion
            if (IsAdd)
                AddProject(newName, newNo);
            else
                UpdateProject(newName, newNo);

        }

        /// <summary>
        /// 取消
        /// Created:20170323(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region 方法

        /// <summary>
        /// 新增项目
        /// Created:20170527(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AddProject(string newName, string newNo)
        {
            JsonResult result = bll.SaveProject("", newName, newNo);
            if (result.result)
            {
                ProjectId = (string)result.data;
                ProjectName = newName;
                ProjectNo = newNo;
                MessageBox.Show("项目新增成功！");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageHelper.ShowRstMsg(result.result);
        }

        /// <summary>
        /// 修改项目
        /// Created:20170527(ChengMengjia)
        /// </summary>
        void UpdateProject(string newName, string newNo)
        {
            string oldName = ProjectName;
            if (newName.Equals(ProjectName) && newNo.Equals(ProjectNo))
            {
                //没有修改
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            JsonResult result = bll.SaveProject(ProjectId, newName, newNo);
            if (result.result)
            {
                ProjectName = newName;
                ProjectNo = newNo;

                #region 名称更改后，项目文件夹改名
                if (!oldName.Equals(newName))
                    try
                    {
                        string olddir = FileHelper.GetWorkdir() + oldName + "\\";
                        string newdir = FileHelper.GetWorkdir() + newName + "\\";
                        DirectoryInfo di = new DirectoryInfo(@olddir);
                        di.MoveTo(@newdir);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                #endregion
                MessageBox.Show("项目修改成功！");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageHelper.ShowRstMsg(result.result);
        }
        #endregion
    }
}
