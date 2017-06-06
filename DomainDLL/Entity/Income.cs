using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 收入信息
    /// </summary>
    public class Income : PersistenceEntity
    {
        
        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 收入阶段
        /// </summary>
        public virtual string Step
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
        /// 完成比例
        /// </summary>
        public virtual decimal Ratio
        {
            get;
            set;
        }
        /// <summary>
        /// 完成标志
        /// </summary>
        public virtual string FinishTag
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
        /// 附件地址
        /// </summary>
        public virtual string FilePath
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