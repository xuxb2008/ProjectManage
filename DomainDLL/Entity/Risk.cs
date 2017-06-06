using System;
using System.Collections;

namespace DomainDLL
{

    /// <summary>
    ///风险信息
    /// </summary>
    public class Risk : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 风险来源
        /// </summary>
        public virtual string Source
        {
            get;
            set;
        }
        /// <summary>
        /// 风险名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 风险描述
        /// </summary>
        public virtual string Desc
        {
            get;
            set;
        }
        /// <summary>
        /// 识别时间
        /// </summary>
        public virtual DateTime? FindDate
        {
            get;
            set;
        }
        /// <summary>
        /// 评定等级
        /// </summary>
        public virtual int? Level
        {
            get;
            set;
        }
        /// <summary>
        /// 时限
        /// </summary>
        public virtual string CostTime
        {
            get;
            set;
        }
        /// <summary>
        /// 依赖关系
        /// </summary>
        public virtual string Dependency
        {
            get;
            set;
        }
        /// <summary>
        /// 概率
        /// </summary>
        public virtual decimal? Probability
        {
            get;
            set;
        }
        /// <summary>
        /// 评估时间
        /// </summary>
        public virtual DateTime? AssessDate
        {
            get;
            set;
        }
        /// <summary>
        /// 评估描述
        /// </summary>
        public virtual string AssessDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 应对策略
        /// </summary>
        public virtual int? HandleType
        {
            get;
            set;
        }
        /// <summary>
        /// 应对描述
        /// </summary>
        public virtual string HandleDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 应对时间
        /// </summary>
        public virtual DateTime? HandleDate
        {
            get;
            set;
        }
    }
}