using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 信息发布
    /// </summary>
    public class PubInfo : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }

        public virtual string Content
        {
            get;
            set;
        }

        public virtual string SendTo
        {
            get;
            set;
        }

        public virtual string CopyTo
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
