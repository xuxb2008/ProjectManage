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

namespace ProjectManagement.Forms.Warning
{
    /// <summary>
    /// 画面：预警配置
    /// Created:20170401(ChengMengjia)
    /// </summary>
    public partial class WarningConfigure : Office2007RibbonForm
    {

        #region 事件
        public WarningConfigure()
        {
            InitializeComponent();
            LoadSetting();
        }

        /// <summary>
        /// 设置保存
        /// Created:20170401(ChengMengjia)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] txtNames = { "项目预警条件", "信息发布预警条件", "项目更新预警条件", 
                                    "交付物预警条件", "交付物预警条件", "问题处理预警条件" };
            string[] values ={
                                sbtnCost.Value?"1":"0",
                                intPub.Value.ToString(),
                                intUpdate.Value.ToString(),
                                sbtnJFW1.Value?"1":"0",
                                sbtnJFW2.Value?"1":"0",
                                sbtnDeal.Value?"1":"0"
                            };
            string[] ConfigNames = {ConstHelper.Warn_Cost,ConstHelper.Warn_PubDay, ConstHelper.Warn_UpdateDay, 
                                       ConstHelper.Warn_JFW1,ConstHelper.Warn_JFW2,ConstHelper.Warn_Deal};
            SaveSetting(txtNames, values, ConfigNames);
        }
        #endregion

        #region 方法

        /// <summary>
        /// 设置加载
        /// Created:20170401(ChengMengjia)
        /// </summary>
        void LoadSetting()
        {
            sbtnCost.Value = CommonHelper.GetConfigValue(ConstHelper.Warn_Cost).Equals("1");
            sbtnJFW1.Value = CommonHelper.GetConfigValue(ConstHelper.Warn_JFW1).Equals("1");
            sbtnJFW2.Value = CommonHelper.GetConfigValue(ConstHelper.Warn_JFW2).Equals("1");
            sbtnDeal.Value = CommonHelper.GetConfigValue(ConstHelper.Warn_Deal).Equals("1");
            string tmp = CommonHelper.GetConfigValue(ConstHelper.Warn_PubDay);
            intPub.Value = string.IsNullOrEmpty(tmp) ? 7 : int.Parse(tmp);
            tmp = CommonHelper.GetConfigValue(ConstHelper.Warn_UpdateDay);
            intUpdate.Value = string.IsNullOrEmpty(tmp) ? 7 : int.Parse(tmp);
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
