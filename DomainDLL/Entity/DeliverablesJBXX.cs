using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 交付物的基本信息
    /// </summary>
    public class DeliverablesJBXX : PersistenceEntity
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public virtual string NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 交付物名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 交付物描述
        /// </summary>
        public virtual string Desc
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarteDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 工作量（天）
        /// </summary>
        public virtual decimal? Workload
        {
            get;
            set;
        }
        /// <summary>
        /// 权值
        /// </summary>
        public virtual int? Weight
        {
            get;
            set;
        }

    }
}