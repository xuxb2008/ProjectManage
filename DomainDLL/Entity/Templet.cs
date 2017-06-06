using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 模板信息
    /// </summary>
    public class Templet : PersistenceEntity
    {
        /// <summary>
        /// 模板分类
        /// </summary>
        public virtual string TypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public virtual string Name
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
        /// 文件地址
        /// </summary>
        public virtual string FilePath
        {
            get;
            set;
        }
    }
}
