using System;
using System.Collections;

namespace DomainDLL
{
    /// <summary>
    /// 项目合同的项目周期
    /// </summary>
    public class ContractXMZQ : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同开始日期
        /// </summary>
        public virtual DateTime? StartDate 
        {
            get;
            set;
        }
        /// <summary>
        /// 合同结束日期
        /// </summary>
        public virtual DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 实际开始日期
        /// </summary>
        public virtual DateTime? TStartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 实际结束日期
        /// </summary>
        public virtual DateTime? TEndDate
        {
            get;
            set;
        }

    }
}
