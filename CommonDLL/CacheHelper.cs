using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace CommonDLL
{
    /// <summary>
    /// 缓存类
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 设置-项目ID
        /// </summary>
        /// <param name="PID"></param>
        public static void SetProjectID(string PID)
        {
            ObjectCache oCache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(120);//取得或设定值，这个值会指定是否应该在指定期间过后清除
            oCache.Set("project_id", PID, policy);
        }

        /// <summary>
        /// 取出-项目ID
        /// </summary>
        /// <returns></returns>
        public static string GetProjectID()
        {
            ObjectCache oCache = MemoryCache.Default;
            return oCache["project_id"] as string;
        }
    }
}
