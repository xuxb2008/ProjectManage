using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccessDLL
{
    /// <summary>
    /// 储存一个ISession <see cref="HttpContext.Items" /> 集合.
    /// </summary>
    public class HttpSessionSource : ISessionStorage
    {
        /// <summary>
        /// 获得ISession 
        /// </summary>
        /// <returns>获得的ISession</returns>
        public ISession Get()
        {
            return (ISession)HttpContext.Current.Items[Config.HttpSessionSourceItemName];
        }

        /// <summary>
        /// 保存ISession
        /// </summary>
        /// <param name="value">需要保存的ISession</param>
        public void Set(ISession value)
        {
            if (value != null)
            {
                HttpContext.Current.Items.Add(Config.HttpSessionSourceItemName, value);
            }
            else
            {
                HttpContext.Current.Items.Remove(Config.HttpSessionSourceItemName);
            }
        }
    }
}
