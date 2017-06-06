using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 项目信息
    /// </summary>
    public class Project : PersistenceEntity
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string No
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 项目最新更新日期
        /// </summary>
        public virtual DateTime? ProjectLastUpdate
        {
            get;
            set;
        }
    }
}