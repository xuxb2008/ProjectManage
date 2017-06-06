using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 分包合同里程碑
    /// </summary>
    public class SubContractLCB : PersistenceEntity
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
        /// 里程碑名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 完成日期
        /// </summary>
        public virtual DateTime? FinishDate
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
        /// 完成依据
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

    }
}