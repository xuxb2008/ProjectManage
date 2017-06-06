using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 项目合同的整体情况描述
    /// </summary>
    public class ContractQKMS : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content
        {
            get;
            set;
        }


    }
}
