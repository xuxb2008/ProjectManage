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
    public partial class Report_Subcontract_Setting : BaseForm
    {
        #region 变量
        //委托变量
        public delegate void SettingDelegate(Dictionary<string,string> setting);
        public event SettingDelegate settingdelegate = null;
        #endregion
        public Report_Subcontract_Setting(Dictionary<string,string> settting)
        {           
            InitializeComponent();
            BindData(settting);
        }

        /// <summary>
        /// 全选
        /// 2017/05/12(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        /// <summary>
        /// 保存按钮
        /// 201//05/15(zhuguanjun)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> Settings = new Dictionary<string, string>();
            Settings.Add("B_Name", "分包合同名称");
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
        /// 根据中文name设置英文value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetKey(string value)
        {
            switch (value)
            {
                case "主合同编号":
                    return "A_No";
                case "主合同名称":
                    return "A_Name";
                case "分包合同编号":
                    return "B_No";
                //case "分包合同名称":
                //    return "B_Name";
                case "合作商":
                    return "SupplierName";
                case "分包合同金额":
                    return "Amount";
                case "签订日期":
                    return "SignDate";
                case "描述":
                    return "Desc";
                default:
                    return "";
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
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 加载时候绑定导出数据
        /// </summary>
        private void BindData(Dictionary<string,string> setting)
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
