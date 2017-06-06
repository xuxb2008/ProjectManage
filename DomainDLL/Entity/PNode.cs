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
        /// 节点类型：0普通节点 1交付物节点 2日常工作 3问题
        /// </summary>
        public virtual int? PType
        {
            get;
            set;
        }

        /// <summary>
        /// 未开始（0），已完成（1），正在执行（2），超期（3）
        /// 未开始的没有背景色（值为0），已完成的为绿色（1），正在执行的为黄色（2），超期的为红色（3）
        /// 非数据库字段
        /// </summary>
        public virtual int? FinishStatus
        {
            get;
            set;
        }
    }
}