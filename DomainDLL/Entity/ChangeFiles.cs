using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDLL
{
    public class ChangeFiles : PersistenceEntity
    {

        public virtual string ChangeID
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

    }
}
