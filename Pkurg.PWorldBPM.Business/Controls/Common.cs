using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Pkurg.PWorldBPM.Business.Controls
{
    public static class Common
    {
        /// <summary>
        /// DataTableToList:目前仅支持几种简单类型属性
        /// star
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable dt)
            where T : class,new()
        {
            List<T> infos = new List<T>();
            if (dt == null)
            {
                return infos;
            }
            PropertyInfo[] pInfos = typeof(T).GetProperties();
            foreach (DataRow item in dt.Rows)
            {
                T info = new T();
                foreach (PropertyInfo pItem in pInfos)
                {
                    if (dt.Columns.Contains(pItem.Name)&& item[pItem.Name] != null)
                    {
                        object value = SetValue(pItem, item[pItem.Name].ToString());
                        if (value != null)
                        {
                            pItem.SetValue(info, value, null);
                        }
                    }
                }
                infos.Add(info);
            }
            return infos;
        }

        private static object SetValue(PropertyInfo pItem, string value)
        {
            switch (pItem.PropertyType.Name)
            {
                case "String":
                    return value;
                case "DateTime":
                    return ParseConvert<DateTime>(DateTime.Parse, value);
                case "Int32":
                    return ParseConvert<int>(int.Parse, value);
                default:
                    break;
            }
            return "";
        }
        private static object ParseConvert<T>(Func<string, T> parseFunc, string value)
        {
            try
            {
                return parseFunc(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
