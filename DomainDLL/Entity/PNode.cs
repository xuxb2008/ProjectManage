using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// WBS节点
    /// </summary>
    public class PNode : PersistenceEntity
    {
        /// <summary>
        /// 排序编号
        /// </summary>
        public virtual int? No
        {
            get;
            set;
        }

        /// <summary>
        /// 项目编码
        /// </summary>
        public virtual string WBSNo
        {
            get;
            set;
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 父级ID
        /// </summary>
        public virtual string ParentID
        {
            get;
            set;
        }
        /// <summary>
        /// 节点名
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为里程碑
        /// </summary>
        public virtual int? IsMilestone
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为交付物
        /// </summary>
        public virtual int? IsJFW
        {
            get;
            set;
        }
    }
}