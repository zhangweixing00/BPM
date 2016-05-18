using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;

namespace WS.K2
{
    public class Common
    {
        public static List<T> TableToList<T>(DataTable dt) where T : class,new()
        {
            List<T> list = new List<T>();
            if (dt == null) return list;
            int len = dt.Rows.Count;

            for (int i = 0; i < len; i++)
            {
                T entity = new T();
                foreach (DataColumn dc in dt.Rows[i].Table.Columns)
                {
                    if (dt.Rows[i][dc.ColumnName] == null || string.IsNullOrEmpty(dt.Rows[i][dc.ColumnName].ToString())) continue;
                    System.Reflection.PropertyInfo pi = entity.GetType().GetProperty(dc.ColumnName);
                    if (pi == null) continue;
                    var value = dt.Rows[i][dc.ColumnName];
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.Nullable`1[System.DateTime]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, DateTime.Parse(value.ToString()), null); }
                            break;
                        case "System.DateTime":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, DateTime.Parse(value.ToString()), null); }
                            break;
                        case "System.Guid":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, new Guid(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Guid]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, new Guid(value.ToString()), null); }
                            break;
                        case "System.Double":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, Double.Parse(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Double]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, Double.Parse(value.ToString()), null); }
                            break;
                        case "System.Boolean":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, System.Boolean.Parse(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Boolean]":
                            bool result;
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, System.Boolean.TryParse(value.ToString(), out result), null); }
                            break;
                        case "System.Int32":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, System.Int32.Parse(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Int32]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, System.Int32.Parse(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Decimal]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, System.Decimal.Parse(value.ToString()), null); }
                            break;
                        default:
                            pi.SetValue(entity, Convert.ChangeType(value, pi.PropertyType), null);
                            break;

                    }
                }
                list.Add(entity);
            }
            dt.Dispose();
            dt = null;
            return list;
        }

        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                //从stream装载到XmlTextReader 
                writer = new XmlTextWriter(stream, Encoding.Unicode);

                //用WriteXml方法写入文件. 
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                UnicodeEncoding utf = new UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

    }
}