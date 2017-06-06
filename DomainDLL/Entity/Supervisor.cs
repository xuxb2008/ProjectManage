using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 监理信息表
    /// </summary>
    public class Supervisor : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 公司名
        /// </summary>
        public virtual string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// 监理费用
        /// </summary>
        public virtual string Cost
        {
            get;
            set;
        }
        /// <summary>
        /// 监理方式
        /// </summary>
        public virtual string Way
        {
            get;
            set;
        }
        /// <summary>
        /// 监理姓名
        /// </summary>
        public virtual string ManagerA
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string A_Tel
        {
            get;
            set;
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public virtual string A_QQ
        {
            get;
            set;
        }
        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string A_Wechat
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱号
        /// </summary>
        public virtual string A_Email
        {
            get;
            set;
        }
        /// <summary>
        /// 总监理姓名
        /// </summary>
        public virtual string ManagerB
        {
            get;
            set;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string B_Tel
        {
            get;
            set;
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public virtual string B_QQ
        {
            get;
            set;
        }
        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string B_Wechat
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱号
        /// </summary>
        public virtual string B_Email
        {
            get;
            set;
        }
    }
}
