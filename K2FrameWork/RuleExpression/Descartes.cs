using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RuleExpression
{
    /// <summary>
    /// 求笛卡儿积
    /// </summary>
    public class Descartes
    {
        /// <summary>
        /// 字符串求笛卡儿积
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <param name="result"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string StringDescartes(List<string[]> list, int count, List<string> result, string data)
        {
            string temp = data;
            //获取当前数组
            string[] astr = list[count];
            //循环当前数组
            foreach (var item in astr)
            {
                if (count + 1 < list.Count)
                {
                    temp += StringDescartes(list, count + 1, result, data + item);
                }
                else
                {
                    result.Add(data + item);
                }
            }
            return temp;
        }



        public static void TableDescarts(List<DataTable> dtList, DataTable result, int count, StringBuilder sb)
        {
            if (count >= dtList.Count)
            {
                count = 0;
                return;
            }
            else
            {
                int i = count;
                DataTable dt = dtList[count++];
                foreach (DataRow dr in dt.Rows)
                {
                    DataRow rd = result.NewRow();
                    for (int j = 0; j < dr.ItemArray.Length; j++)
                    {
                        if (i == 0)
                            rd[j] = dr[j];
                        else if (i > 0 && j == 1)
                        {
                            sb.Replace(" ", "");        //去掉空格
                            for (int k = 0; k < i * 4; k++)
                                sb.Append(" ");
                            rd[j] = sb.ToString() + dr[j];
                        }
                        else
                            rd[j] = dr[j];
                    }
                    result.Rows.Add(rd);        //添加到结果集
                    TableDescarts(dtList, result, count, sb);
                }
            }
        }
    }
}
