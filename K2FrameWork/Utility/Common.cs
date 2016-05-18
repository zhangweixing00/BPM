using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
/*
	Feature:	Common
	Author:		Jason Yu
*/
namespace Utility
{
    public static class Common
    {
        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SafeString(string input)
        {
            return SafeString(input, 0);

        }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="maxLength">返回的最大长度</param>
        /// <returns>过滤后的字符串</returns>
        public static string SafeString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            input = input.Replace("'", "''");
            //others...
            if (maxLength > 0)
            {
                input = input.Substring(0, maxLength);
            }
            return input;
        }

        public static int SafeInt(string s)
        {
            return SafeInt(s, 0);
        }

        public static int SafeInt(string s, int defaultValue)
        {
            int result;
            return int.TryParse(s, out result) ? result : defaultValue;
        }

        public static string FormatBrackets(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return Regex.Replace(s, "\\(.*\\)", string.Empty);
        }

        public static List<T> MapDataTableToObjectList<T>(DataTable dt) where T : new()
        {
            System.Data.DataColumnCollection columns = dt.Columns;
            int iColumnCount = columns.Count;
            int i;
            int j;
            T ttype = new T();
            System.Reflection.PropertyInfo[] publicProperties = ttype.GetType().GetProperties();
            List<T> result = new List<T>();
            try
            {
                foreach (DataRow currentRow in dt.Rows)
                {
                    for (i = 0; i < iColumnCount; i++)
                    {
                        for (j = 0; j < publicProperties.Length; j++)
                        {
                            if (columns[i].ColumnName == publicProperties[j].Name)
                            {
                                publicProperties[j].SetValue(ttype, currentRow[i], null);
                            }
                        }
                    }
                    result.Add(ttype);
                    ttype = new T();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        #region 获取数据 -- 分页
        /// <summary>
        /// 获取数据 -- 分页 
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <param name="DataSource">数据源</param>
        /// <param name="BoundControl">GridView空间Id</param>
        /// <param name="CurrentPageIndex">当前页索引 从0开始</param>
        /// <param name="totalRecordCount">总页数</param>
        /// <param name="PageSize">一页多少条</param>

        public static void BindBoundControl<TSource>(IEnumerable<TSource> DataSource, GridView BoundControl, int CurrentPageIndex, int PageSize, int totalRecordCount)
        {

            //计算总页数
            int totalPageCount = 0;

            if (PageSize == 0)
            {
                PageSize = totalRecordCount;
            }
            if (totalRecordCount % PageSize == 0)
            {
                totalPageCount = totalRecordCount / PageSize;
            }
            else
            {
                totalPageCount = totalRecordCount / PageSize + 1;
            }

            BoundControl.PageSize = PageSize;
            BoundControl.PageIndex = CurrentPageIndex;

            //绑定数据源
            BoundControl.DataSource = DataSource;//.Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize)
            BoundControl.DataBind();
        }
        /// <summary>
        /// 获取数据 -- 分页 
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <param name="DataSource">数据源</param>
        /// <param name="BoundControl">GridView空间Id</param>
        /// <param name="CurrentPageIndex">当前页索引 从0开始</param>
        /// <param name="totalRecordCount">总页数</param>
        /// <param name="PageSize">一页多少条</param>

        public static void BindBoundControl(DataTable DataSource, GridView BoundControl, int CurrentPageIndex, int PageSize, int totalRecordCount)
        {

            //计算总页数
            int totalPageCount = 0;

            if (PageSize == 0)
            {
                PageSize = totalRecordCount;
            }
            if (totalRecordCount % PageSize == 0)
            {
                totalPageCount = totalRecordCount / PageSize;
            }
            else
            {
                totalPageCount = totalRecordCount / PageSize + 1;
            }

            BoundControl.PageSize = PageSize;
            BoundControl.PageIndex = CurrentPageIndex;

            //绑定数据源
            BoundControl.DataSource = DataSource;//.Skip((CurrentPageIndex - 1) * PageSize).Take(PageSize)
            BoundControl.DataBind();
        }
        #endregion

        #region 获取数据 -- 分页
        /// <summary>
        /// 获取数据 -- 分页 
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <param name="DataSource">数据源</param>
        /// <param name="BoundControl">Repeater控件Id</param>
        /// <param name="CurrentPageIndex">当前页索引 从1开始</param>
        /// <param name="totalRecordCount">总页数</param>
        /// <param name="PageSize">一页多少条</param>
        ///WuWeiMin 2011-12-04
        public static void BindBoundControl(DataTable DataSource, Repeater BoundControl, int CurrentPageIndex, int PageSize, int totalRecordCount)
        {

            //计算总页数
            int totalPageCount = 0;

            if (PageSize == 0)
            {
                PageSize = totalRecordCount;
            }
            if (totalRecordCount % PageSize == 0)
            {
                totalPageCount = totalRecordCount / PageSize;
            }
            else
            {
                totalPageCount = totalRecordCount / PageSize + 1;
            }
            //绑定数据源
            BoundControl.DataSource = DataSource;
            BoundControl.DataBind();
        }
        #endregion


        #region gridview 隐藏多余文字
        /// <summary>
        /// gridview 隐藏多余文字 
        /// </summary>
        /// <param name="gv">GridViewName</param>
        /// <param name="cells">隐藏文字所在的列数 从0开始</param>
        /// <param name="controlname">控件的名字</param>
        /// <param name="lenth">超多多长就开始隐藏</param>
        public static void Hide_extra(GridView gv, int cells, string controlname, int lenth)
        {
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                string strRemars = ((Label)(gv.Rows[i].Cells[cells].FindControl(controlname))).Text.Trim();
                gv.Rows[i].Cells[cells].ToolTip = strRemars;

                if (strRemars.Length > lenth)
                {
                    gv.Rows[i].Cells[cells].Text = strRemars.Substring(0, lenth) + "...";
                }
            }
        }
        #endregion

        #region 2011-12-23王红福  转实体列表
        /// <summary>
        /// DataTable 转实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
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
                            { pi.SetValue(entity, Guid.Parse(value.ToString()), null); }
                            break;
                        case "System.Nullable`1[System.Guid]":
                            if (value.ToString().Trim() != string.Empty)
                            { pi.SetValue(entity, Guid.Parse(value.ToString()), null); }
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
                    // pi.SetValue(info, Convert.ChangeType(dt.Rows[i][dc.ColumnName],pi.PropertyType), null);
                }
                list.Add(entity);
            }
            dt.Dispose(); dt = null;
            return list;
        }
        #endregion

        #region 2011-12-23王红福 实体列表转成DataTable
        /// <summary>
        /// 实体列表转成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public static DataTable ListToTable<T>(DataTable dataTable, List<T> modelList) where T : class,new()
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            if (dataTable == null)
            {
                dataTable = new DataTable(typeof(T).Name);
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataTable.Columns.Add(propertyInfo.Name);
                }
            }
            foreach (T model in modelList)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(model, null);
                }
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;

        }
        #endregion

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