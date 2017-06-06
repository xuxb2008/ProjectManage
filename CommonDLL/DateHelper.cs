using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonDLL
{
    /// <summary>
    /// 日期计算帮助类
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// 计算两个日期间的工作日数
        /// Creatd:20170601(ChengMengjia)
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int ComputeWorkDays(DateTime date1, DateTime date2)
        {
            try
            {
                TimeSpan span = date2.Date - date1.Date;
                int delta = span.Days + 1;
                int weekEnds = 0;
                for (int i = 0; i < delta; i++)
                {
                    if ((int)date1.DayOfWeek == 0 || (int)date1.DayOfWeek == 6) weekEnds++;
                    date1 = date1.AddDays(1);
                }
                return delta - weekEnds;
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, LogType.CommonDLL);
                return 0;
            }
        }
    }
}
