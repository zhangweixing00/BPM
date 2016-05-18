using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RuleExpression
{
    /// <summary>
    /// 一些常用的文字处理
    /// CopyRight：2007~2008 services
    /// Author：Glen-wu
    /// </summary>
    public class StringHelper
    {

        private static string passWord;	//加密字符串

        /// <summary>
        /// 过滤输入信息
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>
        public static string InputText(string text, int maxLength)
        {
            #region
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            text = Regex.Replace(text, "[\\s]{2,}", " ");	//two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br>
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	//any other tags
            text = text.Replace("'", "''");
            return text;
            #endregion
        }

        public static string ExtractDoamin(string name)
        {
            int n = name.IndexOf("\\");

            //eg. ECCENTRIX\admin 
            if (n > 0)
                name = name.Substring(n + 1);

            return name;
        }

        public static string TitleOut(string title, string color)
        {
            if (color != null)
                return "<span style=\"color:" + color + "\">" + title + "</span>";
            else
                return title;
        }

        public static string TitleOut(string title, int count, string color)
        {
            if (color != null)
                return "<span style=\"color:" + color + "\">" + StrLeft(title, count) + "</span>";
            else
                return StrLeft(title, count);
        }

        /// <summary>
        /// 检察是否都是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            Regex reg = new Regex(@"^[+]?\d*$");
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool isNumeric(string ch)
        {
            Regex r = new Regex(@"^\d+(\.)?\d*$");
            return r.IsMatch(ch);
        }

        /// <summary>
        /// 检察是否正确的Email格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            Regex reg = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 检察是否正确的日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            //考虑到了4年一度的366天，还有特殊的2月的日期
            Regex reg = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$");
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 截断字符集
        /// </summary>
        /// <param name="str"></param>
        /// <param name="byteCount"></param>
        /// <returns></returns>
        public static string StrLeft(string str, int byteCount)
        {
            if (byteCount < 1) return str;

            if (Encoding.Default.GetByteCount(str) <= byteCount)
            {
                return str;
            }
            else
            {
                if (byteCount > 4)
                {
                    byte[] txtBytes = Encoding.Default.GetBytes(str);
                    byte[] newBytes = new byte[byteCount - 3];

                    for (int i = 0; i < byteCount - 3; i++)
                        newBytes[i] = txtBytes[i];

                    return Encoding.Default.GetString(newBytes) + "...";
                }
                else
                    return str;
            }
        }

        public static string StrLeft(string str, int byteCount, string leftstr)
        {
            if (byteCount < 1) return str;

            if (Encoding.Default.GetByteCount(str) <= byteCount)
            {
                return str;
            }
            else
            {

                if (byteCount > leftstr.Length)
                {
                    byte[] txtBytes = Encoding.Default.GetBytes(str);
                    byte[] newBytes = new byte[byteCount - leftstr.Length];
                    for (int i = 0; i < byteCount - leftstr.Length; i++)
                        newBytes[i] = txtBytes[i];

                    return Encoding.Default.GetString(newBytes) + leftstr;
                }
                else
                {

                    byte[] txtBytes = Encoding.Default.GetBytes(str);
                    byte[] newBytes = new byte[byteCount];
                    for (int i = 0; i < byteCount; i++)
                        newBytes[i] = txtBytes[i];

                    return Encoding.Default.GetString(newBytes);
                }
            }
        }

        
        /// <summary>
        /// 获取汉字第一个拼音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string getSpells(string input)
        {
            #region
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += getSpell(input.Substring(i, 1));
            }
            return reVal;
            #endregion
        }
        static public string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
            #endregion
        }


        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        static public string GetQuanJiao(string BJstr)
        {
            #region
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;

            #endregion
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        static public string GetBanJiao(string QJstr)
        {
            #region
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
            #endregion
        }

        #region 加密的类型
        /// <summary>
        /// 加密的类型
        /// </summary>
        public enum PasswordType
        {
            SHA1,
            MD5
        }
        #endregion

        /// <summary>
        /// 过滤HTML 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        static public string TipHTML(string html)
        {
            //return System.Text.RegularExpressions.Regex.Replace(html, @"<[^>]+>", "");
            return Regex.Replace(html, "<(.|\n)+?>", "");
        }

        public static string FilterScript(string content)
        {
            if (content == null || content == "")
            {
                return content;
            }
            string regexstr = @"(?i)<script([^>])*>(\w|\W)*</script([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<script([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</script>", string.Empty, RegexOptions.IgnoreCase);
        }
        public static string FilterIFrame(string content)
        {
            if (content == null || content == "")
            {
                return content;
            }
            string regexstr = @"(?i)<iframe([^>])*>(\w|\W)*</iframe([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<iframe([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</iframe>", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string CheckHtml(string content)
        {

            string regexfont1 = @"(?i)<font([^>])*>(\w|\W)*</font([^>])*>";//@"<script.*</script>";
            string regexfont2 = @"<font([^>])*>";
            if (Regex.IsMatch(content, regexfont2) && (!Regex.IsMatch(content, regexfont1)))
            {
                return Regex.Replace(content, regexfont2, string.Empty, RegexOptions.IgnoreCase);
            }
            return content;
        }
        public static string RemoveHtml(string content)
        {
            string newstr = FilterScript(content);
            string regexstr = @"<[^>]*>";
            return Regex.Replace(newstr, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }

        public static string RemoveHtmlTag(string content, string[] tags)
        {
            string regexstr1, regexstr2;
            foreach (string tag in tags)
            {
                if (tag != "")
                {
                    regexstr1 = string.Format(@"<{0}([^>])*>", tag);
                    regexstr2 = string.Format(@"</{0}([^>])*>", tag);
                    content = Regex.Replace(content, regexstr1, string.Empty, RegexOptions.IgnoreCase);
                    content = Regex.Replace(content, regexstr2, string.Empty, RegexOptions.IgnoreCase);
                }
            }
            return content;
        }

        public static string RemoveHtmlTag(string content, string tag)
        {
            string returnStr;
            string regexstr1 = string.Format(@"<{0}([^>])*>", tag);
            string regexstr2 = string.Format(@"</{0}([^>])*>", tag);
            returnStr = Regex.Replace(content, regexstr1, string.Empty, RegexOptions.IgnoreCase);
            returnStr = Regex.Replace(returnStr, regexstr2, string.Empty, RegexOptions.IgnoreCase);
            return returnStr;

        }
        /// <summary>
        /// 安全字符检查
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>检查结果  true：安全 ｜ false：不安全</returns>
        public static bool IsSafeCharacters(string str)
        {
            bool returnVal = true;
            //不安全字符定义
            char[] unSafeChar = { '&', ';', '\'', '\\', '"', '|', '*', '?', '~', '^', '<', '>', '(', ')', '[', ']', '{', '}', '$' };

            //逐个检查

            if (str.IndexOfAny(unSafeChar) != -1)
            {
                returnVal = false;
            }

            return returnVal;
        }

        public static string ConvertMoney(Decimal Money)
        {
            //金额转换程序
            string MoneyNum = "";//记录小写金额字符串[输入参数]
            string MoneyStr = "";//记录大写金额字符串[输出参数]
            string BNumStr = "零壹贰叁肆伍陆柒捌玖";//模
            string UnitStr = "万仟佰拾亿仟佰拾万仟佰拾圆角分";//模

            MoneyNum = ((long)(Money * 100)).ToString();
            for (int i = 0; i < MoneyNum.Length; i++)
            {
                string DVar = "";//记录生成的单个字符(大写)
                string UnitVar = "";//记录截取的单位
                for (int n = 0; n < 10; n++)
                {
                    //对比后生成单个字符(大写)
                    if (Convert.ToInt32(MoneyNum.Substring(i, 1)) == n)
                    {
                        DVar = BNumStr.Substring(n, 1);//取出单个大写字符
                        //给生成的单个大写字符加单位
                        UnitVar = UnitStr.Substring(15 - (MoneyNum.Length)).Substring(i, 1);
                        n = 10;//退出循环
                    }
                }
                //生成大写金额字符串
                MoneyStr = MoneyStr + DVar + UnitVar;
            }
            //二次处理大写金额字符串
            MoneyStr = MoneyStr + "整";
            while (MoneyStr.Contains("零分") || MoneyStr.Contains("零角") || MoneyStr.Contains("零佰") || MoneyStr.Contains("零仟")
                || MoneyStr.Contains("零万") || MoneyStr.Contains("零亿") || MoneyStr.Contains("零零") || MoneyStr.Contains("零圆")
                || MoneyStr.Contains("亿万") || MoneyStr.Contains("零整") || MoneyStr.Contains("分整"))
            {
                MoneyStr = MoneyStr.Replace("零分", "零");
                MoneyStr = MoneyStr.Replace("零角", "零");
                MoneyStr = MoneyStr.Replace("零拾", "零");
                MoneyStr = MoneyStr.Replace("零佰", "零");
                MoneyStr = MoneyStr.Replace("零仟", "零");
                MoneyStr = MoneyStr.Replace("零万", "万");
                MoneyStr = MoneyStr.Replace("零亿", "亿");
                MoneyStr = MoneyStr.Replace("亿万", "亿");
                MoneyStr = MoneyStr.Replace("零零", "零");
                MoneyStr = MoneyStr.Replace("零圆", "圆零");
                MoneyStr = MoneyStr.Replace("零整", "整");
                MoneyStr = MoneyStr.Replace("分整", "分");
            }
            if (MoneyStr == "整")
            {
                MoneyStr = "零元整";
            }
            return MoneyStr;
        }

        /// <summary> 
        /// 将字符串使用base64算法加密 
        /// </summary> 
        /// <param name="sourceString">待加密的字符串</param> 
        /// <param name="ens">System.Text.Encoding 对象，如创建中文编码集对象：System.Text.Encoding.GetEncoding(54936)</param> 
        /// <returns>加码后的文本字符串</returns> 
        public static string EncodingForString(string sourceString, System.Text.Encoding ens)
        {
            return Convert.ToBase64String(ens.GetBytes(sourceString));
        }


        /// <summary> 
        /// 将字符串使用base64算法加密 
        /// </summary> 
        /// <param name="sourceString">待加密的字符串</param> 
        /// <returns>加码后的文本字符串</returns> 
        public static string EncodingForString(string sourceString)
        {
            return EncodingForString(sourceString, System.Text.Encoding.GetEncoding(65001));
        }


        /// <summary> 
        /// 从base64编码的字符串中还原字符串，支持中文 
        /// </summary> 
        /// <param name="base64String">base64加密后的字符串</param> 
        /// <param name="ens">System.Text.Encoding 对象，如创建中文编码集对象：System.Text.Encoding.GetEncoding(54936)</param> 
        /// <returns>还原后的文本字符串</returns> 
        public static string DecodingForString(string base64String, System.Text.Encoding ens)
        {
            /** 
            * *********************************************************** 
            * 
            * 从base64String中取得的字节值为字符的机内码（ansi字符编码） 
            * 一般的，用机内码转换为汉字是公式： 
            * (char)(第一字节的二进制值*256+第二字节值) 
            * 而在c#中的char或string由于采用了unicode编码，就不能按照上面的公式计算了 
            * ansi的字节编和unicode编码不兼容 
            * 故利用.net类库提供的编码类实现从ansi编码到unicode代码的转换 
            * 
            * GetEncoding 方法依赖于基础平台支持大部分代码页。但是，对于下列情况提供系统支持：默认编码，即在执行此方法的计算机的区域设置中指定的编码；Little-Endian Unicode (UTF-16LE)；Big-Endian Unicode (UTF-16BE)；Windows 操作系统 (windows-1252)；UTF-7；UTF-8；ASCII 以及 GB18030（简体中文）。 
            * 
            *指定下表中列出的其中一个名称以获取具有对应代码页的系统支持的编码。 
            * 
            * 代码页 名称 
            * 1200 “UTF-16LE”、“utf-16”、“ucs-2”、“unicode”或“ISO-10646-UCS-2” 
            * 1201 “UTF-16BE”或“unicodeFFFE” 
            * 1252 “windows-1252” 
            * 65000 “utf-7”、“csUnicode11UTF7”、“unicode-1-1-utf-7”、“unicode-2-0-utf-7”、“x-unicode-1-1-utf-7”或“x-unicode-2-0-utf-7” 
            * 65001 “utf-8”、“unicode-1-1-utf-8”、“unicode-2-0-utf-8”、“x-unicode-1-1-utf-8”或“x-unicode-2-0-utf-8” 
            * 20127 “us-ascii”、“us”、“ascii”、“ANSI_X3.4-1968”、“ANSI_X3.4-1986”、“cp367”、“csASCII”、“IBM367”、“iso-ir-6”、“ISO646-US”或“ISO_646.irv:1991” 
            * 54936 “GB18030” 
            * 
            * 某些平台可能不支持特定的代码页。例如，Windows 98 的美国版本可能不支持日语 Shift-jis 代码页（代码页 932）。这种情况下，GetEncoding 方法将在执行下面的 C# 代码时引发 NotSupportedException： 
            * 
            * Encoding enc = Encoding.GetEncoding("shift-jis"); 
            * 
            * **************************************************************/
            //从base64String中得到原始字符 
            return ens.GetString(Convert.FromBase64String(base64String));
        }


        /// <summary> 
        /// 从base64编码的字符串中还原字符串，支持中文 
        /// </summary> 
        /// <param name="base64String">base64加密后的字符串</param> 
        /// <returns>还原后的文本字符串</returns> 
        public static string DecodingForString(string base64String)
        {
            return DecodingForString(base64String, System.Text.Encoding.GetEncoding(65001));
        }


        //-------------------------------------------------------------------------------------- 

        /// <summary> 
        /// 对任意类型的文件进行base64加码 
        /// </summary> 
        /// <param name="fileName">文件的路径和文件名</param> 
        /// <returns>对文件进行base64编码后的字符串</returns> 
        public static string EncodingForFile(string fileName)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(fileName);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);

            /*System.Byte[] b=new System.Byte[fs.Length]; 
            fs.Read(b,0,Convert.ToInt32(fs.Length));*/


            string base64String = Convert.ToBase64String(br.ReadBytes((int)fs.Length));


            br.Close();
            fs.Close();
            return base64String;
        }

        /// <summary> 
        /// 把经过base64编码的字符串保存为文件 
        /// </summary> 
        /// <param name="base64String">经base64加码后的字符串</param> 
        /// <param name="fileName">保存文件的路径和文件名</param> 
        /// <returns>保存文件是否成功</returns> 
        public static bool SaveDecodingToFile(string base64String, string fileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            bw.Write(Convert.FromBase64String(base64String));
            bw.Close();
            fs.Close();
            return true;
        }

        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
    }
}
