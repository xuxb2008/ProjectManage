using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonDLL
{
    public class CommonHelper
    {
        #region 取和设置Config.Xml文件里值
        /// <summary>
        /// 按name属性名称取Config.Xml文件里值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        public static string GetConfigValue(string name)
        {
            XmlHelper.XmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Config.xml";
            return XmlHelper.GetProValueByValue("configuration", "Config", "name", name);
        }

        /// <summary>
        /// 按name属性名称取Config.Xml文件里属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="outName">返回属性名</param>
        /// <returns></returns>
        public static string GetConfigProValue(string name, string outName)
        {
            XmlHelper.XmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Config.xml";
            return XmlHelper.GetProValueByProName("configuration", "Config", "name", name, outName);
        }

        /// <summary>
        /// 按name属性名称设置Config.Xml文件里值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool SetConfigValue(string name, string value)
        {
            XmlHelper.XmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Config.xml";
            return XmlHelper.SetProValueByValue("configuration", "Config", "name", name, value);
        }
        #endregion

        #region 文件处理
        /// 获取当前日期处于当月第几周
        ///  Created:2017.05.08(ChengMengjia)
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        public static int getWeekNumInMonth(DateTime daytime)
        {

            int dayInMonth = daytime.Day;
            //本月第一天
            DateTime firstDay = daytime.AddDays(1 - daytime.Day);
            //本月第一天是周几
            int weekday = (int)firstDay.DayOfWeek == 0 ? 7 : (int)firstDay.DayOfWeek;
            //本月第一周有几天
            int firstWeekEndDay = 7 - (weekday - 1);
            //当前日期和第一周之差
            int diffday = dayInMonth - firstWeekEndDay;
            diffday = diffday > 0 ? diffday : 1;
            //当前是第几周,如果整除7就减一天
            int WeekNumInMonth = ((diffday % 7) == 0 ? (diffday / 7 - 1) : (diffday / 7)) + 1 + (dayInMonth > firstWeekEndDay ? 1 : 0);
            return WeekNumInMonth;

        }


        #endregion

        #region 正则表达式
        /// <summary>
        /// 邮箱验证
        /// Created:20170410(ChengMengjia)
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static bool CheckEmail(string addr)
        {
            //正则表达式字符串
            string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";
            //邮箱正则表达式对象
            Regex emailReg = new Regex(emailStr);
            return emailReg.IsMatch(addr);

        }
        #endregion
    }
}
