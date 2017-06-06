using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainDLL
{
    public class WBSCode : PersistenceEntity
    {
        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 序列
        /// </summary>
        public virtual int Orderr
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public virtual int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 分割符
        /// </summary>
        public virtual int Breakk
        {
            get;
            set;
        }

        #region 扩展属性
        /// <summary>
        /// 实际长度
        /// </summary>
        public virtual int LengthName
        {
            get;
            set;
        }
        /// <summary>
        /// 实际分割符
        /// </summary>
        public virtual string BreakName
        {
            get;
            set;
        }
        #endregion
    }
}
