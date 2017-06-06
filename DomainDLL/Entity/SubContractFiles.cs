using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    ///分包合同的附件表
    /// </summary>
    public class SubContractFiles : PersistenceEntity
    {
        /// <summary>
        /// 分包合同FBContract的ID
        /// </summary>
        public virtual string SubID
        {
            get;
            set;
        }
        /// <summary>
        /// 类别
        /// </summary>
        public virtual int? Type
        {
            get;
            set;
        }
        /// <summary>
        /// 文件名
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
        /// 排序用的，不存于数据库
        /// </summary>
        public virtual int? RowNo
        {
            get;
            set;
        }
    }
}