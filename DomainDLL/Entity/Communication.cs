using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 沟通方式
    /// </summary>
    public class Communication : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 沟通方式名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Content
        {
            get;
            set;
        }

    }
}
