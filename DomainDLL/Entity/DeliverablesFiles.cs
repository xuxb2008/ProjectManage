using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 交付物附件表
    /// </summary>
    public class DeliverablesFiles : PersistenceEntity
    {
        /// <summary>
        /// WBS节点PNode的ID
        /// </summary>
        public virtual string NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 附件名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 存放路径
        /// </summary>
        public virtual string Path
        {
            get;
            set;
        }
        /// <summary>
        /// 附件描述
        /// </summary>
        public virtual string Desc
        {
            get;
            set;
        }


        /// <summary>
        /// 排序用的，不存于数据库
        /// </summary>
        public virtual int? RowNo
        {
            get;
            set;
        }
    }
}