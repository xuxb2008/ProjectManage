using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 日常工作责任人工作量分配
    /// </summary>
    public class RoutineWork : PersistenceEntity
    {
        /// <summary>
        /// 日常工作基本信息ID
        /// </summary>
        public virtual string RoutineID
        {
            get;
            set;
        }
        /// <summary>
        /// 工作量（天）
        /// </summary>
        public virtual int? Workload
        {
            get;
            set;
        }
        /// <summary>
        /// <summary>
        /// 实际工作量（天）
        /// </summary>
        public virtual int? ActualWorkload
        {
            get;
            set;
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string Manager
        {
            get;
            set;
        }

        /// <summary>
        /// 负责人
        /// *不存进数据库
        /// </summary>
        public virtual string ManagerName
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