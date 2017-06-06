using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDLL
{
    /// <summary>
    /// Log类型
    /// </summary>
    public enum LogType
    {
        BussinessDLL,
        CommonDLL,
        DataAccessDLL,
        DomainDLL,
        ProjectManagement
    }

    /// <summary>
    /// Log生成
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 写日志至文本文件中
        /// </summary>
        /// <param name="log"></param>
        /// <param name="logType"></param>
        public static void WriteLog(string log, LogType logType)
        {
            string fileName = logType.ToString() + "Log.txt";
            String logFilePath = AppDomain.CurrentDomain.BaseDirectory + fileName;

            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(logFilePath, FileMode.Append);
                if (fs.Length > 1000 * 1000 * 3)
                {
                    if (sw != null)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log/"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Log/");
                    }
                    File.Move(logFilePath,
                              AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.ToString("yyyyMMddmm") +
                              "_" + fileName);
                    fs = new FileStream(logFilePath, FileMode.Append);
                }
                sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " :    " + log);
            }
            catch (Exception ex)
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
                if (!log.StartsWith("writelog"))
                    WriteLog("writelog " + ex.Message + " " + ex.StackTrace, LogType.CommonDLL);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Flush();
                    sw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 写异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteException(Exception ex, LogType logType)
        {
            StringBuilder sb = new StringBuilder();
            string date = DateTime.Now.ToString();
            sb.AppendLine("--------------" + date + "--------------");
            sb.AppendLine("Message:" + ex.Message);
            sb.AppendLine("Source:" + ex.Source);
            sb.AppendLine("StackTrace:" + ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendLine("InnerException:" + ex.InnerException.Message);
            }
            WriteLog(sb.ToString(), logType);
        }
    }
}
