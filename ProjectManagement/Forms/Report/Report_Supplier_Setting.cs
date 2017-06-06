using ProjectManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectManagement.Forms.Report
{
    public partial class Report_Supplier_Setting : BaseForm
    {
        #region 变量
        //委托变量
        public delegate void SettingDelegate(Dictionary<string, string> setting);
        public event SettingDelegate settingdelegate = null;
        #endregion

        public Report_Supplier_Setting(Dictionary<string, string> settting)
        {
            InitializeComponent();
            BindData(settting);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> Settings = new Dictionary<string, string>();
            Settings.Add("Name", "名称");
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                var value = checkedListBox1.CheckedItems[i].ToString();
                var key = GetKey(value);
                Settings.Add(key, value);
            }
            settingdelegate(Settings);
            this.Close();
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 根据中文name设置英文value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetKey(string value)
        {
            switch (value)
            {
                case "法人":
                    return "LegalMan";
                case "负责人":
                    return "Manager";
                case "地址":
                    return "Addr";
                case "联系电话":
                    return "Tel";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 加载时候绑定导出数据
        /// </summary>
        private void BindData(Dictionary<string, string> setting)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (setting.ContainsValue(checkedListBox1.Items[i].ToString()))
                    checkedListBox1.SetItemChecked(i, true);
                else
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

    }
}
