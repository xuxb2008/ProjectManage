using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDLL
{
    /// <summary>
    /// 产生ISessionStorage的工厂
    /// </summary>
    public class ISessionStorageFactory
    {
        /// <summary>
        /// 获得ISessionStorage
        /// </summary>
        /// <returns></returns>
        public static ISessionStorage GetSessionStorage()
        {
            if (Config.SessionSourceType == "http")  //使用    
            {
                return new HttpSessionSource();
            }
            else if (Config.SessionSourceType == "threadStatic")
            {
                return new ThreadSessionSource();
            }
            else
            {
                throw new NotSupportedException("不支持的SessionSourceType！" + Config.SessionSourceType);
            }
        }
    }
}
