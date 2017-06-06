using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 收款信息
    /// </summary>
    public class Receivables : PersistenceEntity
    {
        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual string  BatchNo
        {
            get;
            set;
        }
        /// <summary>
        /// 收入说明
        /// </summary>
        public virtual string Explanation
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
        /// 情况
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
