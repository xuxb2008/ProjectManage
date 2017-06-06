using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 里程碑
    /// </summary>
    public class Milestones : PersistenceEntity
    {

        public virtual string PID
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
        /// 条件
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