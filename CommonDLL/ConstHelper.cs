using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDLL
{
    public static class ConstHelper
    {
        #region Config设置
        /// <summary>
        /// 工作目录
        /// </summary>
        public static string Config_WorkDir = "WorkDir";
        /// <summary>
        /// 用户名称
        /// </summary>
        public static string Config_UserName = "UserName";

        /// <summary>
        /// WBS模板
        /// </summary>
        public static string Config_WBSModel = "WBS模板.xlsx";

        #region 周报配置
        /// <summary>
        /// 模板文件名
        /// </summary>
        public static string Config_WeeklyFileName = "项目周报模板.xlsx";
        /// <summary>
        /// 周报-发送
        /// </summary>
        public static string Config_WeeklySendTo = "WeeklySendTo";
        /// <summary>
        /// 周报-抄送
        /// </summary>
        public static string Config_WeeklyCopyTo = "WeeklyCopyTo";
        /// <summary>
        /// 周报-标题
        /// </summary>
        public static string Config_WeeklyTitle = "WeeklyTitle";
        /// <summary>
        /// 周报-内容
        /// </summary>
        public static string Config_WeeklyContent = "WeeklyContent";
        /// <summary>
        /// 周报-内容选项
        /// </summary>
        public static string Config_WeeklyCheck = "WeeklyCheck";
        #endregion

        #region 发布配置
        /// <summary>
        /// 邮箱类型
        /// </summary>
        public static string Pub_EmailType = "PubEmailType";
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public static string Pub_Email = "PubEmail";
        /// <summary>
        /// 个性签名
        /// </summary>
        public static string Pub_SelfInfo = "PubSelfInfo";
        /// <summary>
        /// QQ号
        /// </summary>
        public static string Pub_QQ = "PubQQ";
        /// <summary>
        /// 微信号
        /// </summary>
        public static string Pub_WeChat = "PubWeChat";
        /// <summary>
        /// 短信号
        /// </summary>
        public static string Pub_Tel = "PubTel";
        #endregion

        #region 预警配置

        /// <summary>
        /// 成本超预算提醒开关
        /// </summary>
        public static string Warn_Cost = "WarnCost";
        /// <summary>
        /// 项目信息发布 天数判断
        /// </summary>
        public static string Warn_PubDay = "WarnPubDay";
        /// <summary>
        /// 项目信息发布 天数判断
        /// </summary>
        public static string Warn_UpdateDay = "WarnUpdateDay";
        /// <summary>
        /// 交付物 完成超时提醒开关
        /// </summary>
        public static string Warn_JFW1 = "WarnJFW1";
        /// <summary>
        /// 交付物 完成即将超时提醒开关
        /// </summary>
        public static string Warn_JFW2 = "WarnJFW2";
        /// <summary>
        /// 问题处理 超时提醒开关
        /// </summary>
        public static string Warn_Deal = "WarnDeal";
        #endregion

        #region 报表配置

        /// <summary>
        /// 项目计划模板
        /// </summary>
        public static string Report_Plan = "项目计划模板.xlsx";

        /// <summary>
        /// 收入情况模板
        /// </summary>
        public static string Report_Earning = "收入情况模板.xlsx";

        /// <summary>
        /// 成本分配模板
        /// </summary>
        public static string Report_Cost = "成本分配模板.xlsx";

        /// <summary>
        /// 收款情况模板
        /// </summary>
        public static string Report_Receivables = "收款情况模板.xlsx";

        /// <summary>
        /// 分包合同模板
        /// </summary>
        public static string Report_Dynamic = "动态报表模板.xlsx";
        #endregion

        #endregion
    }
}
