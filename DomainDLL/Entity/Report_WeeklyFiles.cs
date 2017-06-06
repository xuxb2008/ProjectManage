using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    public class Report_WeeklyFiles : PersistenceEntity
    {

        public virtual string ReportID
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Path
        {
            get;
            set;
        }

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
