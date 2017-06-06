using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    /// 供应商
    /// </summary>
    public class Supplier : PersistenceEntity
    {

        public virtual string PID
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
        /// 法人
        /// </summary>
        public virtual string LegalMan
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
        /// 联系电话
        /// </summary>
        public virtual string Tel
        {
            get;
            set;
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public virtual string Addr
        {
            get;
            set;
        }
        /// <summary>
        /// 营业执照
        /// </summary>
        public virtual string PathYYZZ
        {
            get;
            set;
        }
        /// <summary>
        /// 一般纳税人资格证
        /// </summary>
        public virtual string PathZGZ
        {
            get;
            set;
        }
        /// <summary>
        /// 组织机构代码证
        /// </summary>
        public virtual string PathDMZ
        {
            get;
            set;
        }

    }
}