using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 分包合同
    /// </summary>
    public class SubContract : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 主合同编号
        /// </summary>
        public virtual string A_No
        {
            get;
            set;
        }
        /// <summary>
        /// 主合同名称
        /// </summary>
        public virtual string A_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 分包合同编号
        /// </summary>
        public virtual string B_No
        {
            get;
            set;
        }
        /// <summary>
        /// 分包合同名称
        /// </summary>
        public virtual string B_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 分包合同金额
        /// </summary>
        public virtual string Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 签订日期
        /// </summary>
        public virtual DateTime? SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同内容描述
        /// </summary>
        public virtual string Desc
        {
            get;
            set;
        }

    }
}