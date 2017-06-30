using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 分包合同收款信息
    /// </summary>
    public class SubContractSKXX : PersistenceEntity
    {
        /// <summary>
        /// 分包合同FBContract的ID
        /// </summary>
        public virtual string SubID
        {
            get;
            set;
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 收款比例
        /// </summary>
        public virtual decimal? Ratio
        {
            get;
            set;
        }
        /// <summary>
        /// 完成情况
        /// </summary>
        public virtual int? FinishStatus
        {
            get;
            set;
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public virtual decimal? Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 条件
        /// </summary>
        public virtual string Condition
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 收款日期
        /// </summary>
        public virtual DateTime? InDate
        {
            get;
            set;
        }

    }
}