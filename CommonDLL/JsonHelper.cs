using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDLL
{
    /// <summary>
    /// Json序列化和反序列化
    /// Created：20170329(ChengMengjia)
    /// </summary>
    public static class JsonHelper
    {

        public static string EntityToString<T>(T t)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd";
            return JsonConvert.SerializeObject(t, Formatting.Indented, timeFormat);
        }

        public static string ListToString<T>(List<T> tl)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd";
            return JsonConvert.SerializeObject(tl, Formatting.Indented, timeFormat);
        }
        public static string TableToString(DataTable dt)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd";
            return JsonConvert.SerializeObject(dt, Formatting.Indented, timeFormat);
        }

        public static List<T> StringToList<T>(string value)
        {
            return JsonConvert.DeserializeObject<List<T>>(value);
        }

        public static T StringToEntity<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T TableToEntity<T>(DataTable dt)
        {
            string temp = TableToString(dt);
            string value = temp.Substring(1, temp.Length - 2);
            T t = JsonConvert.DeserializeObject<T>(value);
            return t;
        }

        public static List<T> TableToList<T>(DataTable dt)
        {
            string temp = TableToString(dt);
            List<T> tl = JsonConvert.DeserializeObject<List<T>>(temp);
            return tl;
        }


    }
}
