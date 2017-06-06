using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    public class QueryField
    {
        public QueryFieldType Type { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
        public QueryFieldComparison Comparison { get; set; }
    }

    public class SortField
    {
        public string Name { get; set; }
        public SortDirection Direction { get; set; }
    }


    public enum SortDirection
    {
        Asc,
        Desc
    }

    public enum QueryFieldType
    {
        String = 1,

        Numeric = 2,

        DateTime = 3,

        Boolean = 4
    }


    public enum QueryFieldComparison
    {
        /// <summary>
        /// 等于
        /// </summary>
        eq,
        /// <summary>
        /// 小于
        /// </summary>
        lt,
        /// <summary>
        /// 小于等于
        /// </summary>
        le,
        /// <summary>
        /// 大于
        /// </summary>
        gt,
        /// <summary>
        /// 大于等于
        /// </summary>
        ge,
        /// <summary>
        /// like
        /// </summary>
        like
    }
}
