using System;
using System.Collections;


namespace DomainDLL
{
    /// <summary>
    /// 项目合同的基本信息
    /// </summary>
    public class ContractJBXX : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string No
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 签订日期
        /// </summary>
        public virtual DateTime? SignDate
        {
            get;
            set;
        }
        /// <summary>
        /// 合同金额
        /// </summary>
        public virtual string Amount
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方名称
        /// </summary>
        public virtual string A_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方地址
        /// </summary>
        public virtual string A_Addr
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方负责人
        /// </summary>
        public virtual string A_Manager
        {
            get;
            set;
        }
        /// <summary>
        /// 甲方负责人手机号
        /// </summary>
        public virtual string A_ManagerTel
        {
            get;
            set;
        }

        /// <summary>
        /// 乙方名称
        /// </summary>
        public virtual string B_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方地址
        /// </summary>
        public virtual string B_Addr
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方项目经理
        /// </summary>
        public virtual string B_PManager
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方项目经理手机号
        /// </summary>
        public virtual string B_PManagerTel
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方负责人
        /// </summary>
        public virtual string B_Manager
        {
            get;
            set;
        }
        /// <summary>
        /// 乙方负责人手机号
        /// </summary>
        public virtual string B_Tel
        {
            get;
            set;
        }

    }
}
