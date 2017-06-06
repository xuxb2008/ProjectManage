using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 变更信息
    /// </summary>
    public class Change : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 变更类别
        /// </summary>
        public virtual int Type
        {
            get;
            set;
        }
        /// <summary>
        /// 变更名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 变更前
        /// </summary>
        public virtual string BeforeInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 变更后
        /// </summary>
        public virtual string AfterInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 变更原因
        /// </summary>
        public virtual string Reason
        {
            get;
            set;
        }
        /// <summary>
        /// 费用价格
        /// </summary>
        public virtual string Cost
        {
            get;
            set;
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public virtual string Payment
        {
            get;
            set;
        }

    }
}
