using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    /// <summary>
    /// 模板分类信息
    /// </summary>
    public class TempletType : PersistenceEntity
    {
        /// <summary>
        /// 分类名称
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
    }
}
