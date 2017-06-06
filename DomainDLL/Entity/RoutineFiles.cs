using System;
using System.Collections;

namespace DomainDLL
{
    /// <summary>
    /// 日常工作附件
    /// </summary>
    public class RoutineFiles : PersistenceEntity
    {
        /// <summary>
        /// 日常工作DailyWork的ID
        /// </summary>
        public virtual string RoutineID
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
        /// 描述
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
