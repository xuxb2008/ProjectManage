using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
   /// <summary>
    /// 可以持久到数据库的类
    /// </summary>
    [Serializable]
    public abstract class PersistenceEntity
    {
        public virtual string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 1 正常状态 0 废弃状态
        /// </summary>
        public virtual int? Status
        {
            get;
            set;
        }

        public virtual DateTime CREATED
        {
            get;
            set;
        }

       public virtual DateTime? UPDATED
        {
            get;
            set;
        }
    }
}


