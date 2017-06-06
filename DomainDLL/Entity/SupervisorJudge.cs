using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 监理评价
    /// </summary>
    public class SupervisorJudge : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 评价内容
        /// </summary>
        public virtual string Content
        {
            get;
            set;
        }
        /// <summary>
        /// 评价日期
        /// </summary>
        public virtual DateTime JudgeDate
        {
            get;
            set;
        }
    }
}
