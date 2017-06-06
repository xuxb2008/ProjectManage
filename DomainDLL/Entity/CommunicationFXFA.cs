using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 沟通分析方案
    /// </summary>
    public class CommunicationFXFA : PersistenceEntity
    {
        /// <summary>
        /// 沟通方式Communication的ID
        /// </summary>
        public virtual string CID
        {
            get;
            set;
        }
        /// <summary>
        /// 干系人Stakeholder的ID
        /// </summary>
        public virtual string SID
        {
            get;
            set;
        }
        /// <summary>
        /// 沟通频率
        /// </summary>
        public virtual string Frequency
        {
            get;
            set;
        }
        /// <summary>
        /// 沟通时间
        /// </summary>
        public virtual string CommunicateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 沟通地点
        /// </summary>
        public virtual string Addr
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Content
        {
            get;
            set;
        }

    }
}
