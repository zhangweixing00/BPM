using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace K2Utility
{
    public class SubStringStyle
    {
        /// <summary>
        /// 根据字节得到显示的字符串样式
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <param name="length">字节长度</param>
        /// <param name="showState">是否显示...</param>
        /// <returns></returns>
        public static string GetSubString(string str, int length, bool showState)
        {
            string temp = str;
            //此处为了避免传递的参数为null，在此处做下判断--create by guochao
            if (string.IsNullOrEmpty(temp))
            {
                temp = "";
            }
            else
            {
                int j = 0;
                int k = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                    {
                        j += 2;
                    }
                    else
                    {
                        j += 1;
                    }
                    if (j <= length)
                    {
                        k += 1;
                    }
                    if (j >= length)
                    {
                        temp = temp.Substring(0, k);
                        break;
                    }
                }
                if (showState == true)
                {
                    if (System.Text.Encoding.Default.GetByteCount(str) > length)
                    {
                        temp += "...";
                    }
                }
            }
            return temp;
        }


        #region 部门名称拼接

        /// <summary>
        ///  根据大部门、中部门、小部门获取显示名称
        /// </summary>
        /// <param name="firstDept">大部门</param>
        /// <param name="secondDept">中部门</param>
        /// <param name="thridDept">小部门</param>
        /// <param name="mark">连接符</param>
        /// <returns></returns>
        public static string GetDeptName(object firstDept, object secondDept, object thridDept, string mark)
        {
            StringBuilder sb = new StringBuilder();
            if (firstDept != null && !string.IsNullOrEmpty(firstDept.ToString()))
            {
                sb.Append(firstDept.ToString().Trim());
            }
            if (secondDept != null && !string.IsNullOrEmpty(secondDept.ToString()))
            {
                sb.Append(mark);
                sb.Append(secondDept.ToString().Trim());
            }
            if (thridDept != null && !string.IsNullOrEmpty(thridDept.ToString()))
            {
                sb.Append(mark);
                sb.Append(thridDept.ToString().Trim());
            }
            return sb.ToString();
        }

        #endregion

        #region 拼接公司 大部门 中部门 小部门
        /// <summary>
        /// 拼接公司 大部门 中部门 小部门
        /// </summary>
        /// <param name="company">公司</param>
        /// <param name="firstDept">大部门</param>
        /// <param name="secondDept">中部门</param>
        /// <param name="thridDept">小部门</param>
        /// <param name="mark">分隔符</param>
        /// <returns></returns>
        public static string GetDeptName(string company, string firstDept, string secondDept, string thridDept, string mark)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(company))
            {
                sb.Append(company);
            }
            if (!string.IsNullOrEmpty(firstDept))
            {
                sb.Append(mark);
                sb.Append(firstDept);
            }
            if (!string.IsNullOrEmpty(secondDept))
            {
                sb.Append(mark);
                sb.Append(secondDept);
            }
            if (!string.IsNullOrEmpty(thridDept))
            {
                sb.Append(mark);
                sb.Append(thridDept);
            }
            return sb.ToString();
        }
        #endregion

        #region InterFaceState，状态是否已经完成
        /// <summary>
        /// 判断当前的状态是否已经完成
        /// </summary>
        /// <param name="interFaceState">interFaceState</param>
        /// <param name="currentState">当前状态</param>
        /// <returns>已经完成返回 true;未完成返回 false</returns>
        public static bool IsFinish(string interFaceState, int currentState)
        {
            if ((Convert.ToInt32(interFaceState) & currentState) == currentState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region  public static void EmployeeNameHelper(ref string employeeName)
        /// <summary>
        /// 去除名字中的-
        /// </summary>
        /// <param name="employeeName">employeeName</param>
        public static string EmployeeNameHelper(string employeeName)
        {
            string name = employeeName;
            if (employeeName.IndexOf("-") > -1)
            {
                name = employeeName.Substring(0, employeeName.IndexOf("-"));
            }
            return name;
        }
        #endregion
        /// <summary>   
        /// 判断是否是数字  
        /// add by liaohaibo
        /// </summary>   
        /// <param name="str">字符串</param>   
        /// <returns>bool</returns>   
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
