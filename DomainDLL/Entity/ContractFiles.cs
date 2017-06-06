using System;
using System.Collections;

namespace DomainDLL
{
    /// <summary>
    /// 项目合同的附件表
    /// </summary>
    public class ContractFiles : PersistenceEntity
    {

        public virtual string PID
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// 1:合同扫描件
        /// 2:合同电子档
        /// 3:工作说明书扫描件
        /// 4:工作说明书电子档
        /// 5:招标文件电子档
        /// 6:投标文件电子档
        /// </summary>
        public virtual int? Type
        {
            get;
            set;
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 存放路径
        /// </summary>
        public virtual string Path
        {
            get;
            set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Desc
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
