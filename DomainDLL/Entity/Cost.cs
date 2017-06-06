using System;
using System.Collections;

namespace DomainDLL
{
    /// <summary>
    /// 成本信息
    /// </summary>
    public class Cost : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 成本标记
        /// </summary>
        public virtual string Tag
        {
            get;
            set;
        }
        /// <summary>
        /// 成本说明
        /// </summary>
        public virtual string Explanation
        {
            get;
            set;
        }
        /// <summary>
        /// 可用金额
        /// </summary>
        public virtual decimal? Total
        {
            get;
            set;
        }
        /// <summary>
        /// 已用金额
        /// </summary>
        public virtual decimal? Used
        {
            get;
            set;
        }
        /// <summary>
        /// 在途金额
        /// </summary>
        public virtual decimal? Transit
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余费用
        /// </summary>
        public virtual decimal? Remaining
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

    }
}
