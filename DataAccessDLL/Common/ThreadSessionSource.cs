using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    /// <summary>
    /// 保存一个Session在一个thread-static的类成员中。
    /// </summary>
    public class ThreadSessionSource : ISessionStorage
    {
        [ThreadStatic]
        private static ISession m_Session;

        /// <summary>
        ///获得Session 
        /// </summary>
        /// <returns></returns>
        public ISession Get()
        {
            if (m_Session != null)
            {
                if (!m_Session.IsConnected)
                {
                    m_Session.Reconnect();
                }
            }
            return m_Session;
        }

        /// <summary>
        /// 保存Session
        /// </summary>
        /// <param name="value"></param>
        public void Set(ISession value)
        {
            if (value.IsConnected)
            {
                value.Disconnect();
            }
            m_Session = value;
        }
    }
}
