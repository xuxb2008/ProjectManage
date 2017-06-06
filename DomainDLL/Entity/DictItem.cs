using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 基础数据项列表
    /// </summary>
    public class DictItem : PersistenceEntity
    {
         /// <summary>
        /// 数据项编号
        /// </summary>
        public virtual string DictNo
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string No
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
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