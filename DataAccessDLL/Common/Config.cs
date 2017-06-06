using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    /// <summary>
    /// 配置信息帮助类
    /// </summary>
    public class Config
    {
        #region 私有成员

        private static object m_Locker = new object();
        private static string m_SessionSourceType = String.Empty;
        private static string m_HttpSessionSourceItemName = String.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// Session资源源类型;http,threadStatic
        /// </summary>
        public static string SessionSourceType
        {
            get
            {
                lock (m_Locker)
                {
                    if (m_SessionSourceType == String.Empty)
                    {
                        return ConfigurationManager.AppSettings["SessionSourceType"];
                    }
                    else
                    {
                        return m_SessionSourceType;
                    }
                }
            }
        }

        /// <summary>
        /// HttpSessionSource存放HttpContext.Current.Items的键值名
        /// </summary>
        public static string HttpSessionSourceItemName
        {
            get
            {
                lock (m_Locker)
                {
                    if (m_HttpSessionSourceItemName == String.Empty)
                    {
                        return ConfigurationManager.AppSettings["HttpSessionSourceItemName"];
                    }
                    else
                    {
                        return m_HttpSessionSourceItemName;
                    }
                }
            }

        }

        /// <summary>
        /// 是否使用Session资源源
        /// </summary>
        public static bool UserSessionSource
        {
            get
            {
                lock (m_Locker)
                {
                    return Convert.ToBoolean(ConfigurationManager.AppSettings["UserSessionSource"]);
                }
            }
        }

        #endregion
    }
}
