using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDLL
{
    public static class StringExtention
    {
        /// <summary>
        /// 判断一个字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static T ConvertTo<T>(this IConvertible convertibleValue, object defaultValue = null)
        {
            string erroMsg;

            if (null == convertibleValue || convertibleValue.ToString().Trim().IsNullOrEmpty())
            {
                return defaultValue == null ? default(T) : (T)defaultValue;
            }

            erroMsg = string.Format("数值 \"{0}\" 从类型 \"{1}\" 转换到类型 \"{2}\"无效.",
                                    convertibleValue.ToString(), convertibleValue.GetType().FullName, typeof(T).FullName);
            try
            {
                if (!typeof(T).IsGenericType)
                {
                    //非泛型类型
                    return (T)Convert.ChangeType(convertibleValue, typeof(T));
                }
                else
                {
                    //泛型类型
                    Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
                    }

                    if (defaultValue != null)
                    {
                        return (T)defaultValue;
                    }
                    throw new InvalidCastException(erroMsg);
                }
            }
            catch
            {
                if (defaultValue != null)
                {
                    return (T)defaultValue;
                }
                throw new InvalidCastException(erroMsg);
            }

        }

        public static T ConvertTo<T>(this object obj, object defaultValue = null)
        {
            if (obj == null)
            {
                return defaultValue == null ? default(T) : (T)defaultValue;
            }

            return ConvertTo<T>(obj.ToString(), defaultValue);
        }
    }
}
