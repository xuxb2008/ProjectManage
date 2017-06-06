using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonDLL
{
    /// <summary>
    /// 枚举描述标签
    /// Created：20170327(ChengMengjia)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumDescAttribute : Attribute
    {
        public EnumDescAttribute(String Description_in)
        {
            this.description = Description_in;
        }
        protected String description;
        public String Description
        {
            get
            {
                return this.description;
            }
        }
    }

    /// <summary>
    /// 基础数据项
    /// Created：20170327(ChengMengjia)
    /// </summary>
    public enum DictCategory
    {
        /// <summary>
        /// 项目里程碑完成情况
        /// </summary>
        [EnumDesc("项目里程碑完成情况")]
        Milestones_FinshStatus = 0,
        /// <summary>
        /// 收款批次
        /// </summary>
        [EnumDesc("收款批次")]
        Receivables_BatchNo = 1,
        /// <summary>
        /// 责任人
        /// </summary>
        [EnumDesc("责任人")]
        WBS_Manager = 2,
        /// <summary>
        /// 收款完成情况
        /// </summary>
        [EnumDesc("收款完成情况")]
        Receivables_FinshStatus = 3,
        /// <summary>
        /// 干系人类型
        /// </summary>
        [EnumDesc("干系人类型")]
        StakehoderType = 4,
        /// <summary>
        /// 日常工作完成情况
        /// </summary>
        [EnumDesc("日常工作完成情况")]
        WorkHandleStatus = 5,
        /// <summary>
        /// 日常工作完成情况
        /// </summary>
        [EnumDesc("问题处理情况")]
        TroubleHandleStatus = 6,
        /// <summary>
        /// 沟通频率
        /// </summary>
        [EnumDesc("沟通频率")]
        Frequency = 7,
        /// <summary>
        /// 邮箱配置类型
        /// </summary>
        [EnumDesc("邮箱配置类型")]
        Pub_EmailType = 8,
        /// <summary>
        /// 风险等级
        /// </summary>
        [EnumDesc("风险等级")]
        Level = 9,
        /// <summary>
        /// 概率
        /// </summary>
        [EnumDesc("概率")]
        Probability = 10,
        /// <summary>
        /// 对应策略
        /// </summary>
        [EnumDesc("对应策略")]
        HandType = 11,
        /// <summary>
        /// 监理方式
        /// </summary>
        [EnumDesc("监理方式")]
        Supervisor_Way = 12,
        /// <summary>
        /// 问题级别
        /// </summary>
        [EnumDesc("问题级别")]
        TroubleLevel = 13,
        /// <summary>
        /// 发送方式
        /// </summary>
        [EnumDesc("发送方式")]
        SendType = 14,
        /// <summary>
        /// 收入计划完成情况
        /// </summary>
        [EnumDesc("收入计划完成情况")]
        EarningStatus = 15,
        /// <summary>
        /// 分包合同-里程碑完成情况
        /// </summary>
        [EnumDesc("分包合同-里程碑完成情况")]
        LCBFinishStatus = 16,
        /// <summary>
        /// 分包合同-收款信息完成情况
        /// </summary>
        [EnumDesc("分包合同-收款信息完成情况")]
        SKXXFnishStatus = 17,
        /// <summary>
        /// 分包合同-收款信息收款批次
        /// </summary>
        [EnumDesc("分包合同-收款信息收款批次")]
        SKXXBatchNo = 18,
        /// <summary>
        /// 项目计划报表-完成情况
        /// </summary>
        [EnumDesc("计划报表-完成情况")]
        PlanFinishStatus = 19,

        #region WBS代码
        /// <summary>
        /// WBS代码-序列
        /// </summary>
        [EnumDesc("WBS代码-序列")]
        WBSCodeOrder = 20,
        /// <summary>
        /// WBS代码-长度
        /// </summary>
        [EnumDesc("WBS代码-长度")]
        WBSCodeLength = 21,
        /// <summary>
        /// WBS代码-分割符
        /// </summary>
        [EnumDesc("WBS代码-分割符")]
        WBSCodeBreak = 22
        #endregion
    }

    public class EnumsHelper
    {
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            try
            {
                string description = value.ToString();
                FieldInfo fieldInfo = value.GetType().GetField(description);
                EnumDescAttribute[] attributes = (EnumDescAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    description = attributes[0].Description;
                }
                return description;
            }
            catch
            {
                return "";
            }
        }

    }

    #region 枚举类
    /// <summary>
    /// 变更类型
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// 时间变更
        /// </summary>
        [EnumDesc("时间变更")]
        Date = 1,
        /// <summary>
        /// 需求变更
        /// </summary>
        [EnumDesc("需求变更")]
        Need = 2,
        /// <summary>
        /// 范围变更
        /// </summary>
        [EnumDesc("范围变更")]
        Range = 3
    }

    /// <summary>
    /// wbs代码-序列
    /// </summary>
    public enum WBSCodeOrder
    {
        /// <summary>
        /// 大写字母
        /// </summary>
        [EnumDesc("大写字母")]
        Upper = 1,
        /// <summary>
        /// 小写字母
        /// </summary>
        [EnumDesc("小写字母")]
        Lower = 2,
        /// <summary>
        /// 数字
        /// </summary>
        [EnumDesc("数字")]
        Number = 3
    }

    /// <summary>
    /// 更新进度
    /// </summary>
    public enum ProgressType
    {
        /// <summary>
        /// 0%
        /// </summary>
        [EnumDesc("0%")]
        Type1 = 1,
        /// <summary>
        /// 25%
        /// </summary>
        [EnumDesc("25%")]
        Type2 = 2,
        /// <summary>
        /// 50%
        /// </summary>
        [EnumDesc("50%")]
        Type3 = 3,
        /// <summary>
        /// 75%
        /// </summary>
        [EnumDesc("75%")]
        Type4 = 4,
        /// <summary>
        /// 100%
        /// </summary>
        [EnumDesc("100%")]
        Type5 = 5
    }

    /// <summary>
    /// wbs代码-序列
    /// </summary>
    public enum WBSPType
    {
        /// <summary>
        /// 普通节点
        /// </summary>
        [EnumDesc("普通节点")]
        PType0 = 0,
        /// <summary>
        /// 交付物
        /// </summary>
        [EnumDesc("交付物")]
        PType1= 1,
        /// <summary>
        /// 日常工作
        /// </summary>
        [EnumDesc("日常")]
        PType2 = 2,
        /// <summary>
        /// 问题
        /// </summary>
        [EnumDesc("问题")]
        PType3 = 3
    }
    #endregion
}
