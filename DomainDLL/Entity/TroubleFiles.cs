using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    ///  问题附件
    /// </summary>
    public class TroubleFiles : PersistenceEntity
    {

        public virtual string TroubleID
        {
            get;
            set;
        }
        /// <summary>
        /// 附件类型
        /// 1：问题原因
        /// 2：问题分析
        /// 3：解决方案
        /// </summary>
        public virtual int? Type
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