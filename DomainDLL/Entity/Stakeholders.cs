using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    ///干系人清单
    /// </summary>
    public class Stakeholders : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 分类
        /// 1 客户
        /// 2 项目组
        /// 3监理
        /// </summary>
        public virtual int? Type
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual string Position
        {
            get;
            set;
        }
        /// <summary>
        /// 职责
        /// </summary>
        public virtual string Duty
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Tel
        {
            get;
            set;
        }
        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string Wechat
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱号
        /// </summary>
        public virtual string Email
        {
            get;
            set;
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public virtual string QQ
        {
            get;
            set;
        }
        /// <summary>
        /// 是否默认发布
        /// 是否为项目经理
        /// </summary>
        public virtual int IsPublic
        {
            get;
            set;
        }
        /// <summary>
        /// 发送方式
        /// </summary>
        public virtual int? SendType
        {
            get;
            set;
        }

        #region 扩展字段
        /// <summary>
        /// 编号
        /// </summary>
        public virtual int RowNo
        {
            get;
            set;
        }
        /// <summary>
        /// 分类
        /// </summary>
        public virtual string TypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 发送方式
        /// </summary>
        public virtual string SendTypeName
        {
            get;
            set;
        }
        #endregion

    }
}