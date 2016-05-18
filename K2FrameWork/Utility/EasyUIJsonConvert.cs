using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Utility
{
    public class EasyUIJsonConvert
    {
        public static StringBuilder result = new StringBuilder();
        private static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 将DataTable中的数据转换成JSON格式
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="displayCount">是否输出数据总条数</param>
        /// <param name="totalcount">JSON中显示的数据总条数</param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt, bool displayCount, int totalcount)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null)
            {
                JsonString.Append("{ ");
                JsonString.Append("\"rows\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + dt.Rows[i][j].ToString().ToLower() + ",");
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\",");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\",");
                            }
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            if (dt.Columns[j].DataType == typeof(bool))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + dt.Rows[i][j].ToString().ToLower());
                            }
                            else if (dt.Columns[j].DataType == typeof(string))
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j].ToString().Replace("\"", "\\\"") + "\"");
                            }
                            else
                            {
                                JsonString.Append("\"JSON_" + dt.Columns[j].ColumnName.ToLower() + "\":" + "\"" + dt.Rows[i][j] + "\"");
                            }
                        }
                    }
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                if (displayCount)
                {
                    JsonString.Append(",");
                    JsonString.Append("\"total\":"); JsonString.Append(totalcount);
                }
                JsonString.Append("}");
                return JsonString.ToString().Replace("\n", "");
            }
            else { return null; }
        }

        /// <summary>
        /// 根据DataTable生成Json树结构
        /// </summary>
        /// <param name="tabel">数据源</param>
        /// <param name="idCol">ID列</param>
        /// <param name="txtCol">Text列</param>
        /// <param name="rela">关系字段</param>
        /// <param name="pId">父ID</param>
        public static void GetTreeJsonByTable(DataTable tabel, string idCol, string txtCol, string rela, object pId)
        {
            result.Append(sb.ToString());
            sb.Clear();
            if (tabel.Rows.Count > 0)
            {
                sb.Append("[");
                string filer = string.Format("{0}='{1}'", rela, pId);
                DataRow[] rows = tabel.Select(filer);
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        sb.Append("{\"id\":\"" + row[idCol] + "\",\"text\":\"" + row[txtCol] + "\",\"state\":\"open\"");
                        if (tabel.Select(string.Format("{0}='{1}'", rela, row[idCol])).Length > 0)
                        {
                            sb.Append(",\"children\":");
                            GetTreeJsonByTable(tabel, idCol, txtCol, rela, row[idCol]);
                            result.Append(sb.ToString());
                            sb.Clear();
                        }
                        result.Append(sb.ToString());
                        sb.Clear();
                        sb.Append("},");
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                result.Append(sb.ToString());
                sb.Clear();
            }
        }

        
        /// <summary>
        /// 根据DataTable值生成ComboBox数据源
        /// </summary>
        /// <param name="dt"></param>
        public static string GetComboBoxJson(DataTable dt, string id, string text)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null)
            {
                if (string.IsNullOrEmpty(id))
                    id = "ID";
                if (string.IsNullOrEmpty(text))
                    text = "CHName";

                JsonString.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    JsonString.Append("{");
                    JsonString.AppendFormat("'{0}':'{1}','{2}':'{3}'", "id", dr[id].ToString(), "text", dr[text].ToString());
                    JsonString.Append("},");
                }
                if (JsonString[JsonString.Length - 1] == ',')
                    JsonString.Remove(JsonString.Length - 1, 1);
                JsonString.Append("]");
            }

            return JsonString.ToString();
        }

        /// <summary>
        /// 根据DataTable值生成AutoComplete数据源
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetAutoCompleteJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null)
            {
                JsonString.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    JsonString.Append("{");
                    JsonString.AppendFormat("'{0}':'{1}','{2}':'{3}','{4}':'{5}'", "id", dr["ID"].ToString(), "name", dr["CHName"].ToString(), "adaccount", dr["ADAccount"].ToString().Replace("\\","\\\\"));
                    JsonString.Append("},");
                }
                if (JsonString[JsonString.Length - 1] == ',')
                    JsonString.Remove(JsonString.Length - 1, 1);
                JsonString.Append("]");
            }

            return JsonString.ToString();
        }

        /// <summary>
        /// 根据DataTable值生成AutoComplete数据源
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetAutoCompleteRoleJson(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();
            if (dt != null)
            {
                JsonString.Append("[");
                foreach (DataRow dr in dt.Rows)
                {
                    JsonString.Append("{");
                    JsonString.AppendFormat("'{0}':'{1}','{2}':'{3}'", "id", dr["ID"].ToString(), "name", dr["RoleName"].ToString());
                    JsonString.Append("},");
                }
                if (JsonString[JsonString.Length - 1] == ',')
                    JsonString.Remove(JsonString.Length - 1, 1);
                JsonString.Append("]");
            }

            return JsonString.ToString();
        }

        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Serialize<T>(T data)
        {
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        /// <summary>
        /// 反序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
