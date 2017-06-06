using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    /// <summary>
    ///储存一个ISession
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        ///获得ISession 
        /// </summary>
        /// <returns></returns>
        ISession Get();

        /// <summary>
        /// 保存ISession
        /// </summary>
        /// <param name="value"></param>
        void Set(ISession value);
    }
}
