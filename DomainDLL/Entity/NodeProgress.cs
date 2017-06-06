using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 节点交付物进度更新
    /// </summary>
    public class NodeProgress : PersistenceEntity
    {
         /// <summary>
        /// 节点编号
        /// </summary>
        public virtual string NodeID
        {
            get;
            set;
        }
        /// <summary>
        /// 进度
        /// </summary>
        public virtual int? PType
        {
            get;
            set;
        }
        /// <summary>
        /// 说明
        /// </summary>
        public virtual string Desc
        {
            get;
            set;
        }
    }
}