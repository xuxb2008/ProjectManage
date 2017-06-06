using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainDLL
{
    /// <summary>
    /// 工作量类(非映射类)
    /// 2015/05/31(zhuguanjun)
    /// </summary>
    public class WorkloadEntity
    {
        /// <summary>
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
        /// </summary>
        public virtual string ManagerName
        {
            get;
            set;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StarteDate
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// 排序用的
        /// </summary>
        public virtual int? RowNo
        {
            get;
            set;
        }
    }
}
