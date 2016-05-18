using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RuleExpression
{
    public class Expression
    {
        private StackSimple<int> theStack;
        public StringBuilder suffix = new StringBuilder();
        private XmlDocument tmpDoc = new XmlDocument();     //缓存Document

        /**
         * 检查该字符是否为括号
         */
        private bool isParen(string c)
        {
            if (c == "{" || c == "[" || c == "(" || c == ")" || c == "]" || c == "}")
                return true;
            else
                return false;
        }

        /**
         * 检查是否为左括号
         */
        private bool isLeftParen(string c)
        {
            if (c == "{" || c == "[" || c == "(")
                return true;
            else
                return false;
        }
        /**
         * 判断指定的操作符是否为 '+' 或者 '-'
         */
        private bool isAddOrSub(string oper)
        {
            return oper == "43" || oper == "45" || oper == ")" || oper == "(";
        }
        /**
         * 检查是否为数字
         */
        private bool isNumber(int ch)
        {
            return ch >= 48 && ch <= 57;
        }

        /**
         * 检查表达式是否合法
         */
        private bool checkExp(String exp)
        {
            //char[] ch = exp.ToCharArray();
            exp = exp.Replace(")", " ) ").Replace("(", " ( ").Replace("+", " + ").Replace("-", " - ").Replace("*", " * ").Replace("/", " / ");
            string[] ch = exp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StackSimple<int> theStackXInt = new StackSimple<int>(exp.Length / 2);
            /**
             * 遍历表达式,只处理括号部分
             */
            for (int i = 0; i < ch.Length; i++)
            {
                //char c = ch[i];
                string c = ch[i];
                if (isParen(c))
                {
                    /**
                     * 左括号进栈
                     */
                    if (isLeftParen(c))
                    {
                        char[] tmp = c.ToCharArray();
                        theStackXInt.Push(tmp[0]);
                        //theStackXInt.Push(c);
                    }
                    else
                    {
                        if (theStackXInt.IsEmpty())
                        {
                            return false;
                        }
                        int left = theStackXInt.Pop();
                        switch (c)
                        {
                            case "}":
                                if (left != '{')
                                    return false;
                                break;
                            case "]":
                                if (left != '[')
                                    return false;
                                break;
                            case ")":
                                if (left != '(')
                                    return false;
                                break;
                        }
                    }
                }
            }
            if (theStackXInt.IsEmpty())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /**
         * 根据当前操作符处理之前在栈中的操作符,当前操作符暂不处理,放入栈中
         */
        private void processOper(string oper)
        {
            while (!theStack.IsEmpty())
            {
                int currTop = theStack.Pop();
                /**
                 * 如果是左括号,则不予处理,将括号返回进栈
                 * 
                 * 若得到的操作符,则进一步判断
                 */
                if (currTop == '(')
                {
                    theStack.Push(currTop);
                    break;
                }
                else
                {
                    if (this.isAddOrSub(currTop.ToString()))
                    {
                        if (this.isAddOrSub(oper))
                        {
                            this.suffix.Append((char)currTop);
                        }
                        else
                        {
                            this.theStack.Push(currTop);
                            break;
                        }
                    }
                    else
                    {
                        this.suffix.Append((char)currTop);
                        break;
                    }
                }
            }
            /**
             * 当前操作符进栈
             */
            char[] tmp = oper.ToCharArray();
            theStack.Push(tmp[0]);
        }
        /**
         * 处理括弧.
         * 
         * 如果为左括弧,直接进栈;
         * 
         * 否则为右括弧,现将这一对括号中的操作符优先处理完
         */
        private void processParen(string paren)
        {
            if (this.isLeftParen(paren))
            {
                char[] tmp = paren.ToCharArray();
                this.theStack.Push(tmp[0]);
            }
            else
            {
                while (!theStack.IsEmpty())
                {
                    int chx = theStack.Pop();
                    if (chx == '(')
                        break;
                    else
                        suffix.Append((char)chx);
                }
            }
        }
        /**
         * 将正确的中缀表达式转为后缀表达式
         */
        private void doTrans(String exp)
        {
            theStack = new StackSimple<int>(exp.Length);
            if (this.checkExp(exp))
            {
                //char[] ch = exp.ToCharArray();
                exp = exp.Replace("(", " ( ").Replace(")", " ) ").Replace("+", " + ").Replace("-", " - ").Replace("*", " * ").Replace("/", " / ");
                string[] ch = exp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ch.Length; i++)
                {
                    //char c = ch[i];
                    string c = ch[i];
                    switch (c)
                    {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                            this.processOper(c);
                            break;
                        case "(":
                        case ")":
                            this.processParen(c);
                            break;
                        default:
                            this.suffix.Append(c);
                            this.suffix.Append(" ");
                            break;
                    }
                }
                while (!this.theStack.IsEmpty())
                {
                    this.suffix.Append((char)this.theStack.Pop());
                }
            }
        }

        /// <summary>
        /// 计算表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int clac(String exp, String xml, out String retXml)
        {
            string reXml = CreateRedundancyXML(xml);
            this.doTrans(exp);
            String suff = this.suffix.ToString();
            StackSimple<int> stack = new StackSimple<int>(suff.Length);

            if (suff == null || suff.Length == 0)
            {

            }
            else
            {
                //char[] ch = suff.ToCharArray();
                //suff = suff.Replace(")", " ) ").Replace("(", " ( ").Replace("+", " + ").Replace("-", " - ").Replace("*", " * ").Replace("/", " / ");
                string[] ch = suff.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ch.Length; i++)
                {
                    //char c = ch[i];
                    string c = ch[i];
                    if (StringHelper.isNumeric(c))
                    {
                        stack.Push(Convert.ToInt32(c));
                    }
                    else
                    {
                        int operand2 = stack.Pop();
                        int operand1 = stack.Pop();
                        switch (c)
                        {
                            case "+":
                                stack.Push(CalCollection(operand1, operand2, "+", ref reXml));
                                break;
                            case "-":
                                stack.Push(CalCollection(operand1, operand2, "-", ref reXml));
                                break;
                            case "*":
                                stack.Push(CalCollection(operand1, operand2, "*", ref reXml));
                                break;
                            case "/":
                                stack.Push(CalCollection(operand1, operand2, "-", ref reXml));
                                break;
                        }
                    }
                }
            }
            retXml = reXml;
            return stack.Peep();
        }

        /// <summary>
        /// 计算表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public int clac(String exp, DataTable dt, ref DataTable ret, ref DataTable resultModel)
        {
            this.doTrans(exp);
            String suff = this.suffix.ToString();
            StackSimple<int> stack = new StackSimple<int>(suff.Length);

            //创建唯一标识
            if (!dt.Columns.Contains("AppID"))
            {
                DataColumn dc = new DataColumn("AppID");
                dt.Columns.Add(dc);
                foreach (DataRow dr in dt.Rows)
                    dr["AppID"] = Guid.NewGuid();
                dt.AcceptChanges();
            }

            //创建父ID
            if (!dt.Columns.Contains("PID"))
            {
                DataColumn dc = new DataColumn("PID");
                dt.Columns.Add(dc);
                dt.AcceptChanges();
            }
            FindRoot(dt);       //创建父子关系
            resultModel = dt.Clone();
            ret = dt.Clone();  //赋给另一个Table

            /*-------------暂时注释-----------------
            //将结果表增加一列IsDelete标识是否是重复行
            DataColumn dcDelete = new DataColumn("IsDelete");
            resultModel.Columns.Add(dcDelete);
            foreach (DataRow d in resultModel.Rows)
                d["IsDelete"] = false;

            //过滤结果表
            FindRootRow(resultModel);
            DataTable tmp = resultModel.Copy();

            //删除需要过滤的记录
            foreach (DataRow d in tmp.Rows)
            {
                if (d["IsDelete"].ToString() == "True")
                {
                    DataRow[] tdr = resultModel.Select("AppID = '" + d["AppID"].ToString() + "'");
                    if (tdr != null && tdr.Length == 1)
                        resultModel.Rows.Remove(tdr[0]);
                }
            }

            -------------暂时注释-----------------*/

            //生成标准模板Table
            resultModel = CreatedModelTable(dt);

            if (suff == null || suff.Length == 0)
            {

            }
            else
            {
                //char[] ch = suff.ToCharArray();
                //suff = suff.Replace(")", " ) ").Replace("(", " ( ").Replace("+", " + ").Replace("-", " - ").Replace("*", " * ").Replace("/", " / ");
                string[] ch = suff.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < ch.Length; i++)
                {
                    //char c = ch[i];
                    string c = ch[i];
                    if (StringHelper.isNumeric(c))
                    {
                        //stack.Push(c - 48);
                        stack.Push(Convert.ToInt32(c));
                    }
                    else
                    {
                        int operand2 = stack.Pop();
                        int operand1 = stack.Pop();
                        switch (c)
                        {
                            case "+":
                                stack.Push(CalCollection(operand1, operand2, "+", dt, ret));
                                break;
                            case "-":
                                stack.Push(CalCollection(operand1, operand2, "-", dt, ret));
                                break;
                            case "*":
                                stack.Push(CalCollection(operand1, operand2, "*", dt, ret));
                                break;
                            case "/":
                                stack.Push(CalCollection(operand1, operand2, "-", dt, ret));
                                break;
                        }
                    }
                }
            }
            return stack.Peep();
        }

        /// <summary>
        /// 生成标准模板Table
        /// </summary>
        /// <param name="dt"></param>
        private DataTable CreatedModelTable(DataTable dt)
        {
            DataTable resultModel = dt.Clone();     //定义保存结果的Table
            List<DataRow> tmp = new List<DataRow>();    //临时列表
            StringBuilder sb = new StringBuilder();

            //循环取得OrderNo顺序
            foreach (DataRow dr in dt.Rows)
            {
                if (!sb.ToString().Contains(dr["OrderNo"].ToString()))
                {
                    sb.Append(dr["OrderNo"].ToString());
                    sb.Append(";");
                }
                else
                {
                    break;
                }
            }

            string[] orderArray = sb.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string order in orderArray)
            {
                //循环取得当前Order的行
                tmp.Clear();
                foreach (DataRow d in dt.Rows)
                {
                    if (d["OrderNo"].ToString() == order)
                        tmp.Add(d);
                }
                List<DataRow> t = deepCopy(tmp);
                FilterRepeat(t);        //过滤重复行

                if (resultModel.Rows.Count == 0)
                {
                    foreach (DataRow r in t)
                    {
                        r["PID"] = null;
                        resultModel.Rows.Add(r.ItemArray);
                    }
                }
                else
                {
                    int pos = -1;        //记录位置
                    DataTable tdt = resultModel.Copy();
                    foreach (DataRow wr in tdt.Rows)
                    {
                        ++pos;
                        //判断是否是末节点行
                        if (tdt.Select("PID = '" + wr["AppID"].ToString() + "'").Length == 0)
                        {
                            foreach (DataRow r in t)
                            {
                                ++pos;
                                if (tdt.Select("RequestNodeID = '" + r["RequestNodeID"].ToString() + "'").Length == 0)
                                {
                                    DataRow dr = resultModel.NewRow();
                                    dr.ItemArray = r.ItemArray;
                                    dr["PID"] = wr["AppID"].ToString();
                                    dr["AppID"] = Guid.NewGuid().ToString();
                                    resultModel.Rows.InsertAt(dr, pos);
                                }
                            }
                        }
                    }
                }
            }
            return resultModel;
        }

        /// <summary>
        /// 查找根节点
        /// </summary>
        /// <param name="dt"></param>
        private void FindRoot(DataTable dt)
        {
            List<DataRow> root = new List<DataRow>();
            //查找根节点
            foreach (DataRow d in dt.Rows)
            {
                if (!d["RequestNodeName"].ToString().StartsWith("&nbsp;&nbsp;&nbsp;&nbsp;"))
                {
                    d["PID"] = "NULL";
                    root.Add(d);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
            //foreach (DataRow dr in root)
            //{
            //    dr["PID"] = "NULL";     //根节点，无父ID
            //}

            FindChild(root, dt, sb);
        }

        /// <summary>
        /// 查找子节点
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dt"></param>
        /// <param name="sb"></param>
        private void FindChild(List<DataRow> dr, DataTable dt, StringBuilder sb)
        {
            List<DataRow> tmp = new List<DataRow>();
            foreach (DataRow t in dr)
                tmp.Add(t);

            DataTable childDt = dt.Copy();
            while (true)
            {
                //循环找子节点
                childDt.Rows.Clear();
                foreach (DataRow d in dt.Rows)
                {
                    if (d["RequestNodeName"].ToString().StartsWith(sb.ToString()) && !d["RequestNodeName"].ToString().StartsWith(sb.ToString() + "&nbsp;"))
                    {
                        childDt.Rows.Add(d.ItemArray);
                    }
                }

                if (childDt == null || childDt.Rows.Count == 0)
                    break;

                int inlineLoop = childDt.Rows.Count / tmp.Count;
                for (int i = 0; i < tmp.Count; i++)
                {
                    for (int j = 0; j < inlineLoop; j++)
                    {
                        childDt.Rows[(i * inlineLoop) + j]["PID"] = tmp[i]["AppID"].ToString();
                        DataRow[] drp = dt.Select("AppID = '" + childDt.Rows[(i * inlineLoop) + j]["AppID"].ToString() + "'");
                        if (drp != null && drp.Length == 1)
                            drp[0]["PID"] = tmp[i]["AppID"].ToString();
                    }
                }

                //调整参数值
                sb = sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;");
                //DataRow[] drp = new DataRow[childDt.Rows.Count];
                DataTable dcl = childDt.Copy();
                tmp.Clear();
                foreach (DataRow tp in dcl.Rows)
                    tmp.Add(tp);
            }
        }

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <returns></returns>
        private int CalCollection(Int32 operand1, Int32 operand2, String op, ref String xml)
        {
            //通过operand参数取得xml中的对象
            switch (op)
            {
                case "+":
                    CalAdd(operand1, operand2, ref xml);
                    break;
                case "-":
                    CalSub(operand1, operand2, ref xml);
                    break;
                case "/":
                    CalSub(operand1, operand2, ref xml);
                    break;
                case "*":
                    CalMul(operand1, operand2, ref xml);
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 集合计算
        /// </summary>
        /// <returns></returns>
        private int CalCollection(Int32 operand1, Int32 operand2, String op, DataTable dt, DataTable ret)
        {
            ////添加一列判断列
            //if (!dt.Columns.Contains("Isjudged"))
            //{
            //    DataColumn dc = new DataColumn("Isjudged");
            //    dt.Columns.Add(dc);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dr["Isjudged"] = false;
            //    }
            //    dt.AcceptChanges();
            //}

            //通过operand参数取得xml中的对象
            switch (op)
            {
                case "+":
                    CalAdd(operand1, operand2, dt, ret);
                    break;
                case "-":
                    CalSub(operand1, operand2, dt, ret);
                    break;
                case "/":
                    CalSub(operand1, operand2, dt, ret);
                    break;
                case "*":
                    CalMul(operand1, operand2, dt, ret);
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 创建冗余XML
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private string CreateRedundancyXML(string xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);

            //判断是否需要添加
            XmlNode judgeNode = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='0']");
            if (judgeNode != null)
                return xml;
            else
            {
                XmlNodeList nodeList = xDoc.SelectNodes("Rule/Group/RuleTable");
                if (nodeList != null && nodeList.Count > 0)
                {
                    XmlNode node = nodeList[nodeList.Count - 1];
                    XmlNode cloneNode = nodeList[0].CloneNode(true);

                    //创建冗余xml
                    node.ParentNode.AppendChild(cloneNode);
                    node.NextSibling.Attributes["OrderNo"].Value = "0";
                }
                return xDoc.InnerXml;
            }
        }

        /// <summary>
        /// 计算并集
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="xml"></param>
        private void CalAdd(Int32 operand1, Int32 operand2, ref String xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlNode nodeop1 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand1.ToString() + "']");
            XmlNode nodeop2 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand2.ToString() + "']");
            if (nodeop1 != null && nodeop2 != null)
            {
                XmlNodeList nodesop1 = nodeop1.SelectNodes("./Node");
                XmlNodeList nodesop2 = nodeop2.SelectNodes("./Node");
                if (nodesop1 != null && nodesop1.Count > 0 && nodesop2 != null && nodesop2.Count > 0 && nodesop1.Count == nodesop2.Count)
                {
                    foreach (XmlNode node in nodesop1)
                    {
                        XmlNode tmpNode = node.SelectSingleNode("./ProcessNodeID");
                        if (tmpNode != null)
                        {
                            foreach (XmlNode secNode in nodesop2)
                            {
                                XmlNode tmpNode2 = secNode.SelectSingleNode("./ProcessNodeID");
                                if (tmpNode2 != null)
                                {
                                    if (tmpNode.FirstChild.Value.ToLower() == tmpNode2.FirstChild.Value.ToLower())
                                    {

                                        //0找到IsApprove节点
                                        XmlNode nodeRec = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='0']");
                                        XmlNodeList listRec = nodeRec.SelectNodes("./Node/ProcessNodeID");
                                        foreach (XmlNode nodeApp in listRec)
                                        {
                                            if (nodeApp.FirstChild.Value.ToLower() == tmpNode.FirstChild.Value.ToLower())
                                            {
                                                if (tmpNode.NextSibling.FirstChild.Value == "False" && tmpNode2.NextSibling.FirstChild.Value == "False")
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "False";
                                                }
                                                else
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "True";
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    //抛出异常
                    throw new Exception("....");
                }
                xml = xDoc.InnerXml;
            }
        }

        /// <summary>
        /// 计算并集
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="dt"></param>
        private void CalAdd(Int32 operand1, Int32 operand2, DataTable dt, DataTable ret)
        {
            if (operand1 == 0)  //后续计算过程
            {
                List<DataRow> opr2 = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);   //从DT中取得operand2
                }

                FilterRepeat(opr2);     //去掉重复行

                //从ret中取出末节点
                List<DataRow> leafs = GetLeaf(ret);

                //判断opr2的RequestNodeID是否在ret中
                if (ret.Select("RequestNodeID = '" + opr2[0]["RequestNodeID"].ToString() + "'").Length > 0)
                {
                    //循环查询opr2所属的根节点
                    //先判断leafs是否与opr2一致
                    bool isLeaf = false;
                    foreach (DataRow dr in leafs)
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            if (op2["RequestNodeID"].ToString() == dr["RequestNodeID"].ToString())
                            {
                                isLeaf = true;
                                break;
                            }
                        }
                        if (isLeaf)
                            break;
                    }
                    if (isLeaf) //opr2是ret的末节点
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            DataRow[] leaf = ret.Select("RequestNodeID = '" + op2["RequestNodeID"].ToString() + "'");
                            if (leaf.Length > 0)
                            {
                                foreach (DataRow dr in leaf)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (op2[k].ToString() == "True" || dr[k].ToString() == "True")
                                            dr[k] = true;
                                        else
                                            dr[k] = false;
                                    }
                                }
                            }
                        }
                    }
                    else//opr2不是ret的末节点行
                    {
                        for (int i = 0; i < opr2.Count; i++)
                        {
                            //在leafs中查找所有是opr2子元素的行
                            foreach (DataRow dr in leafs)
                            {
                                bool isSame = false;        //标识是否找到记录
                                DataRow tmp = dr;
                                //查找每个leafs节点行的父节点行是否是opr2[i]
                                while (true)
                                {
                                    if (tmp["PID"] == DBNull.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        DataRow[] t = ret.Select("AppID = '" + tmp["PID"].ToString() + "'");
                                        if (t.Length == 1)
                                        {
                                            if (t[0]["RequestNodeID"].ToString() == opr2[i]["RequestNodeID"].ToString())
                                            {
                                                isSame = true;
                                                break;
                                            }
                                            else
                                            {
                                                tmp = t[0];
                                            }
                                        }
                                    }
                                }
                                if (isSame)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (opr2[i][k].ToString() == "True" || dr[k].ToString() == "True")
                                            dr[k] = true;
                                        else
                                            dr[k] = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else//opr2不在ret中
                {
                    //临时保存leafs节点
                    List<DataRow> tmpLeafs = deepCopy(leafs);
                    List<DataRow> tmp2Leafs = deepCopy(leafs);
                    FilterRepeat(tmp2Leafs);        //过滤重复行
                    int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo

                    //opr2不在ret中，删除ret的末节点行
                    foreach (DataRow dr in tmp2Leafs)
                    {
                        DataRow[] tmp = ret.Select("RequestNodeID = '" + dr["RequestNodeID"].ToString() + "'");
                        foreach (DataRow d in tmp)
                            d.Delete();
                    }

                    leafs = tmpLeafs;
                    if (ret.Rows.Count == 0)//说明当前ret中只有一级节点
                    {
                        //int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo
                        if (tmpOpr <= operand2)
                        {
                            List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                            foreach (DataRow d1 in leafs)
                            {
                                d1["PID"] = null;
                                ret.Rows.Add(d1.ItemArray);
                                for (int j = 0; j < opr2.Count; j++)
                                {
                                    for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                    {
                                        if (d1[i].ToString() == "True" || opr2[j][i].ToString() == "True")
                                            tmpd[j][i] = true;
                                        else
                                            tmpd[j][i] = false;
                                    }
                                    tmpd[j]["PID"] = d1["AppID"].ToString();
                                    //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                        else
                        {
                            List<DataRow> tmpd = deepCopy(leafs);
                            foreach (DataRow d2 in opr2)
                            {
                                d2["PID"] = null;
                                ret.Rows.Add(d2.ItemArray);
                                for (int j = 0; j < leafs.Count; j++)
                                {
                                    for (int i = 4; i < leafs[j].ItemArray.Length - 2; i++)
                                    {
                                        if (leafs[j][i].ToString() == "True" || d2[i].ToString() == "True")
                                            tmpd[j][i] = true;
                                        else
                                            tmpd[j][i] = false;
                                    }
                                    tmpd[j]["PID"] = d2["AppID"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                    }
                    else//ret中有超过一级节点
                    {
                        List<DataRow> parentLeafs = GetLeaf(ret);       //取得删除后的末节点行
                        if (tmpOpr <= operand2 || tmpOpr > operand2)
                        {
                            int loop = tmp2Leafs.Count;
                            List<DataRow> caList = deepCopy(leafs);     //临时保存
                            for (int k = 0; k < ret.Rows.Count; k++)
                            {
                                int times = -1;      //记录循环(parentLeafs循环)次数
                                foreach (DataRow pdr in parentLeafs)
                                {
                                    ++times;
                                    if (ret.Rows[k]["AppID"].ToString() == pdr["AppID"].ToString())
                                    {
                                        int pos = 0;
                                        foreach (DataRow dr in opr2)
                                        {
                                            ++pos;
                                            dr["PID"] = pdr["AppID"].ToString();    //修改PID值
                                            dr["AppID"] = Guid.NewGuid().ToString();
                                            DataRow tdr = ret.NewRow();
                                            tdr.ItemArray = dr.ItemArray;
                                            ret.Rows.InsertAt(tdr, k + pos);    //将opr2中的一行添加到ret表中

                                            //与现有的末节点行进行计算
                                            for (int t = times * loop; t < times * loop + loop; t++)
                                            {
                                                ++pos;
                                                caList[t]["PID"] = dr["AppID"].ToString();
                                                caList[t]["AppID"] = Guid.NewGuid().ToString();
                                                for (int i = 4; i < leafs[t].ItemArray.Length - 2; i++)
                                                {
                                                    if (leafs[t][i].ToString() == "True" || dr[i].ToString() == "True")
                                                    {
                                                        caList[t][i] = true;
                                                    }
                                                    else
                                                    {
                                                        caList[t][i] = false;
                                                    }
                                                }
                                                DataRow tdr1 = ret.NewRow();
                                                tdr1.ItemArray = caList[t].ItemArray;
                                                ret.Rows.InsertAt(tdr1, k + pos);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else//第一次计算过程
            {
                //在dt中查询opr1和opr2的行（顺序）
                List<DataRow> opr1 = new List<DataRow>();
                List<DataRow> opr2 = new List<DataRow>();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand1.ToString())
                        opr1.Add(dr);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);
                }

                //去掉重复的行
                FilterRepeat(opr1);
                FilterRepeat(opr2);

                //判断两个元素是否一致
                if (IsSame(opr1, opr2))
                {
                    if (operand1 <= operand2)
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr1[i][j].ToString() == "True" || opr2[i][j].ToString() == "True")
                                {
                                    opr2[i][j] = true;
                                }
                                else
                                {
                                    opr2[i][j] = false;
                                }
                            }
                            opr2[i]["PID"] = null;
                            ret.Rows.Add(opr2[i].ItemArray);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr1[i][j].ToString() == "True" || opr2[i][j].ToString() == "True")
                                {
                                    opr1[i][j] = true;
                                }
                                else
                                {
                                    opr1[i][j] = false;
                                }
                            }
                            opr1[i]["PID"] = null;
                            ret.Rows.Add(opr1[i].ItemArray);
                        }
                    }
                }
                else
                {
                    //比较大小
                    if (operand1 <= operand2)
                    {
                        List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                        foreach (DataRow d1 in opr1)
                        {
                            d1["PID"] = null;
                            ret.Rows.Add(d1.ItemArray);
                            for (int j = 0; j < opr2.Count; j++)
                            {
                                for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                {
                                    if (d1[i].ToString() == "True" || opr2[j][i].ToString() == "True")
                                        tmpd[j][i] = true;
                                    else
                                        tmpd[j][i] = false;
                                }
                                tmpd[j]["PID"] = d1["AppID"].ToString();
                                //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                    else
                    {
                        List<DataRow> tmpd = deepCopy(opr1);
                        foreach (DataRow d2 in opr2)
                        {
                            d2["PID"] = null;
                            ret.Rows.Add(d2.ItemArray);
                            for (int j = 0; j < opr1.Count; j++)
                            {
                                for (int i = 4; i < opr1[j].ItemArray.Length - 2; i++)
                                {
                                    if (opr1[j][i].ToString() == "True" || d2[i].ToString() == "True")
                                        tmpd[j][i] = true;
                                    else
                                        tmpd[j][i] = false;
                                }
                                tmpd[j]["PID"] = d2["AppID"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算补集合
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="xml"></param>
        private void CalSub(Int32 operand1, Int32 operand2, ref String xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlNode nodeop1 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand1.ToString() + "']");
            XmlNode nodeop2 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand2.ToString() + "']");
            if (nodeop1 != null && nodeop2 != null)
            {
                XmlNodeList nodesop1 = nodeop1.SelectNodes("./Node");
                XmlNodeList nodesop2 = nodeop2.SelectNodes("./Node");
                if (nodesop1 != null && nodesop1.Count > 0 && nodesop2 != null && nodesop2.Count > 0 && nodesop1.Count == nodesop2.Count)
                {
                    foreach (XmlNode node in nodesop1)
                    {
                        XmlNode tmpNode = node.SelectSingleNode("./ProcessNodeID");
                        if (tmpNode != null)
                        {
                            foreach (XmlNode secNode in nodesop2)
                            {
                                XmlNode tmpNode2 = secNode.SelectSingleNode("./ProcessNodeID");
                                if (tmpNode2 != null)
                                {
                                    if (tmpNode.FirstChild.Value.ToLower() == tmpNode2.FirstChild.Value.ToLower())
                                    {

                                        //0找到IsApprove节点
                                        XmlNode nodeRec = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='0']");
                                        XmlNodeList listRec = nodeRec.SelectNodes("./Node/ProcessNodeID");
                                        foreach (XmlNode nodeApp in listRec)
                                        {
                                            if (nodeApp.FirstChild.Value.ToLower() == tmpNode.FirstChild.Value.ToLower())
                                            {
                                                if (tmpNode.NextSibling.FirstChild.Value == "True" && tmpNode2.NextSibling.FirstChild.Value == "True")
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "False";
                                                }
                                                else
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "True";
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    //抛出异常
                    throw new Exception("....");
                }
                xml = xDoc.InnerXml;
            }
        }

        private void CalSub(Int32 operand1, Int32 operand2, DataTable dt, DataTable ret)
        {
            if (operand1 == 0)  //后续计算过程
            {
                List<DataRow> opr2 = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);   //从DT中取得operand2
                }

                FilterRepeat(opr2);     //去掉重复行

                //从ret中取出末节点
                List<DataRow> leafs = GetLeaf(ret);

                //判断opr2的RequestNodeID是否在ret中
                if (ret.Select("RequestNodeID = '" + opr2[0]["RequestNodeID"].ToString() + "'").Length > 0)
                {
                    //循环查询opr2所属的根节点
                    //先判断leafs是否与opr2一致
                    bool isLeaf = false;
                    foreach (DataRow dr in leafs)
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            if (op2["RequestNodeID"].ToString() == dr["RequestNodeID"].ToString())
                            {
                                isLeaf = true;
                                break;
                            }
                        }
                        if (isLeaf)
                            break;
                    }
                    if (isLeaf) //opr2是ret的末节点
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            DataRow[] leaf = ret.Select("RequestNodeID = '" + op2["RequestNodeID"].ToString() + "'");
                            if (leaf.Length > 0)
                            {
                                foreach (DataRow dr in leaf)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (op2[k].ToString() == "True")
                                            dr[k] = false;
                                        else
                                            dr[k] = dr[k];
                                    }
                                }
                            }
                        }
                    }
                    else//opr2不是ret的末节点行
                    {
                        for (int i = 0; i < opr2.Count; i++)
                        {
                            //在leafs中查找所有是opr2子元素的行
                            foreach (DataRow dr in leafs)
                            {
                                bool isSame = false;        //标识是否找到记录
                                DataRow tmp = dr;
                                //查找每个leafs节点行的父节点行是否是opr2[i]
                                while (true)
                                {
                                    if (tmp["PID"] == DBNull.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        DataRow[] t = ret.Select("AppID = '" + tmp["PID"].ToString() + "'");
                                        if (t.Length == 1)
                                        {
                                            if (t[0]["RequestNodeID"].ToString() == opr2[i]["RequestNodeID"].ToString())
                                            {
                                                isSame = true;
                                                break;
                                            }
                                            else
                                            {
                                                tmp = t[0];
                                            }
                                        }
                                    }
                                }
                                if (isSame)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (opr2[i][k].ToString() == "True")
                                            dr[k] = false;
                                        else
                                            dr[k] = dr[k];
                                    }
                                }
                            }
                        }
                    }
                }
                else//opr2不在ret中
                {
                    //临时保存leafs节点
                    List<DataRow> tmpLeafs = deepCopy(leafs);
                    List<DataRow> tmp2Leafs = deepCopy(leafs);
                    FilterRepeat(tmp2Leafs);        //过滤重复行
                    int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo

                    //opr2不在ret中，删除ret的末节点行
                    foreach (DataRow dr in tmp2Leafs)
                    {
                        DataRow[] tmp = ret.Select("RequestNodeID = '" + dr["RequestNodeID"].ToString() + "'");
                        foreach (DataRow d in tmp)
                            d.Delete();
                    }

                    leafs = tmpLeafs;
                    if (ret.Rows.Count == 0)//说明当前ret中只有一级节点
                    {
                        //int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo
                        if (tmpOpr <= operand2)
                        {
                            List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                            foreach (DataRow d1 in leafs)
                            {
                                d1["PID"] = null;
                                ret.Rows.Add(d1.ItemArray);
                                for (int j = 0; j < opr2.Count; j++)
                                {
                                    for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                    {
                                        if (opr2[j][i].ToString() == "True")
                                            tmpd[j][i] = false;
                                        else
                                            tmpd[j][i] = tmpd[j][i];
                                    }
                                    tmpd[j]["PID"] = d1["AppID"].ToString();
                                    //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                        else
                        {
                            List<DataRow> tmpd = deepCopy(leafs);
                            foreach (DataRow d2 in opr2)
                            {
                                d2["PID"] = null;
                                ret.Rows.Add(d2.ItemArray);
                                for (int j = 0; j < leafs.Count; j++)
                                {
                                    for (int i = 4; i < leafs[j].ItemArray.Length - 2; i++)
                                    {
                                        if (d2[i].ToString() == "True")
                                            tmpd[j][i] = false;
                                        else
                                            tmpd[j][i] = tmpd[j][i];
                                    }
                                    tmpd[j]["PID"] = d2["AppID"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                    }
                    else//ret中有超过一级节点
                    {
                        List<DataRow> parentLeafs = GetLeaf(ret);       //取得删除后的末节点行
                        if (tmpOpr <= operand2 || tmpOpr > operand2)
                        {
                            int loop = tmp2Leafs.Count;
                            List<DataRow> caList = deepCopy(leafs);     //临时保存
                            for (int k = 0; k < ret.Rows.Count; k++)
                            {
                                int times = -1;      //记录循环(parentLeafs循环)次数
                                foreach (DataRow pdr in parentLeafs)
                                {
                                    ++times;
                                    if (ret.Rows[k]["AppID"].ToString() == pdr["AppID"].ToString())
                                    {
                                        int pos = 0;
                                        foreach (DataRow dr in opr2)
                                        {
                                            ++pos;
                                            dr["PID"] = pdr["AppID"].ToString();    //修改PID值
                                            dr["AppID"] = Guid.NewGuid().ToString();
                                            DataRow tdr = ret.NewRow();
                                            tdr.ItemArray = dr.ItemArray;
                                            ret.Rows.InsertAt(tdr, k + pos);    //将opr2中的一行添加到ret表中

                                            //与现有的末节点行进行计算
                                            for (int t = times * loop; t < times * loop + loop; t++)
                                            {
                                                ++pos;
                                                caList[t]["PID"] = dr["AppID"].ToString();
                                                caList[t]["AppID"] = Guid.NewGuid().ToString();
                                                for (int i = 4; i < leafs[t].ItemArray.Length - 2; i++)
                                                {
                                                    if (dr[i].ToString() == "True")
                                                    {
                                                        caList[t][i] = false;
                                                    }
                                                    else
                                                    {
                                                        caList[t][i] = caList[t][i];
                                                    }
                                                }
                                                DataRow tdr1 = ret.NewRow();
                                                tdr1.ItemArray = caList[t].ItemArray;
                                                ret.Rows.InsertAt(tdr1, k + pos);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else//第一次计算过程
            {
                //在dt中查询opr1和opr2的行（顺序）
                List<DataRow> opr1 = new List<DataRow>();
                List<DataRow> opr2 = new List<DataRow>();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand1.ToString())
                        opr1.Add(dr);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);
                }

                //去掉重复的行
                FilterRepeat(opr1);
                FilterRepeat(opr2);

                //判断两个元素是否一致
                if (IsSame(opr1, opr2))
                {
                    if (operand1 <= operand2)
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr2[i][j].ToString() == "True")
                                {
                                    opr2[i][j] = false;
                                }
                                else
                                {
                                    opr2[i][j] = opr2[i][j];
                                }
                            }
                            opr2[i]["PID"] = null;
                            ret.Rows.Add(opr2[i].ItemArray);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr2[i][j].ToString() == "True")
                                {
                                    opr1[i][j] = false;
                                }
                                else
                                {
                                    opr1[i][j] = opr1[i][j];
                                }
                            }
                            opr1[i]["PID"] = null;
                            ret.Rows.Add(opr1[i].ItemArray);
                        }
                    }
                }
                else
                {
                    //比较大小
                    if (operand1 <= operand2)
                    {
                        List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                        foreach (DataRow d1 in opr1)
                        {
                            d1["PID"] = null;
                            ret.Rows.Add(d1.ItemArray);
                            for (int j = 0; j < opr2.Count; j++)
                            {
                                for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                {
                                    if (opr2[j][i].ToString() == "True")
                                        tmpd[j][i] = false;
                                    else
                                        tmpd[j][i] = tmpd[j][i];
                                }
                                tmpd[j]["PID"] = d1["AppID"].ToString();
                                //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                    else
                    {
                        List<DataRow> tmpd = deepCopy(opr1);
                        foreach (DataRow d2 in opr2)
                        {
                            d2["PID"] = null;
                            ret.Rows.Add(d2.ItemArray);
                            for (int j = 0; j < opr1.Count; j++)
                            {
                                for (int i = 4; i < opr1[j].ItemArray.Length - 2; i++)
                                {
                                    if (d2[i].ToString() == "True")
                                        tmpd[j][i] = false;
                                    else
                                        tmpd[j][i] = tmpd[j][i];
                                }
                                tmpd[j]["PID"] = d2["AppID"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算交集
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="xml"></param>
        private void CalMul(Int32 operand1, Int32 operand2, ref String xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlNode nodeop1 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand1.ToString() + "']");
            XmlNode nodeop2 = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='" + operand2.ToString() + "']");
            if (nodeop1 != null && nodeop2 != null)
            {
                XmlNodeList nodesop1 = nodeop1.SelectNodes("./Node");
                XmlNodeList nodesop2 = nodeop2.SelectNodes("./Node");
                if (nodesop1 != null && nodesop1.Count > 0 && nodesop2 != null && nodesop2.Count > 0 && nodesop1.Count == nodesop2.Count)
                {
                    foreach (XmlNode node in nodesop1)
                    {
                        XmlNode tmpNode = node.SelectSingleNode("./ProcessNodeID");
                        if (tmpNode != null)
                        {
                            foreach (XmlNode secNode in nodesop2)
                            {
                                XmlNode tmpNode2 = secNode.SelectSingleNode("./ProcessNodeID");
                                if (tmpNode2 != null)
                                {
                                    if (tmpNode.FirstChild.Value.ToLower() == tmpNode2.FirstChild.Value.ToLower())
                                    {

                                        //0找到IsApprove节点
                                        XmlNode nodeRec = xDoc.SelectSingleNode("Rule/Group/RuleTable[@OrderNo='0']");
                                        XmlNodeList listRec = nodeRec.SelectNodes("./Node/ProcessNodeID");
                                        foreach (XmlNode nodeApp in listRec)
                                        {
                                            if (nodeApp.FirstChild.Value.ToLower() == tmpNode.FirstChild.Value.ToLower())
                                            {
                                                if (tmpNode.NextSibling.FirstChild.Value == "True" && tmpNode2.NextSibling.FirstChild.Value == "True")
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "True";
                                                }
                                                else
                                                {
                                                    nodeApp.NextSibling.FirstChild.Value = "False";
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    //抛出异常
                    throw new Exception("....");
                }
                xml = xDoc.InnerXml;
            }
        }

        /// <summary>
        /// 计算交集
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="operand2"></param>
        /// <param name="dt"></param>
        /// <param name="ret">结果集</param>
        private void CalMul(Int32 operand1, Int32 operand2, DataTable dt, DataTable ret)
        {
            if (operand1 == 0)  //后续计算过程
            {
                List<DataRow> opr2 = new List<DataRow>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);   //从DT中取得operand2
                }

                FilterRepeat(opr2);     //去掉重复行

                //从ret中取出末节点
                List<DataRow> leafs = GetLeaf(ret);

                //判断opr2的RequestNodeID是否在ret中
                if (ret.Select("RequestNodeID = '" + opr2[0]["RequestNodeID"].ToString() + "'").Length > 0)
                {
                    //循环查询opr2所属的根节点
                    //先判断leafs是否与opr2一致
                    bool isLeaf = false;
                    foreach (DataRow dr in leafs)
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            if (op2["RequestNodeID"].ToString() == dr["RequestNodeID"].ToString())
                            {
                                isLeaf = true;
                                break;
                            }
                        }
                        if (isLeaf)
                            break;
                    }
                    if (isLeaf) //opr2是ret的末节点
                    {
                        foreach (DataRow op2 in opr2)
                        {
                            DataRow[] leaf = ret.Select("RequestNodeID = '" + op2["RequestNodeID"].ToString() + "'");
                            if (leaf.Length > 0)
                            {
                                foreach (DataRow dr in leaf)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (op2[k].ToString() == "True" && dr[k].ToString() == "True")
                                            dr[k] = true;
                                        else
                                            dr[k] = false;
                                    }
                                }
                            }
                        }
                    }
                    else//opr2不是ret的末节点行
                    {
                        for (int i = 0; i < opr2.Count; i++)
                        {
                            //在leafs中查找所有是opr2子元素的行
                            foreach (DataRow dr in leafs)
                            {
                                bool isSame = false;        //标识是否找到记录
                                DataRow tmp = dr;
                                //查找每个leafs节点行的父节点行是否是opr2[i]
                                while (true)
                                {
                                    if (tmp["PID"] == DBNull.Value)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        DataRow[] t = ret.Select("AppID = '" + tmp["PID"].ToString() + "'");
                                        if (t.Length == 1)
                                        {
                                            if (t[0]["RequestNodeID"].ToString() == opr2[i]["RequestNodeID"].ToString())
                                            {
                                                isSame = true;
                                                break;
                                            }
                                            else
                                            {
                                                tmp = t[0];
                                            }
                                        }
                                    }
                                }
                                if (isSame)
                                {
                                    for (int k = 4; k < dr.ItemArray.Length - 2; k++)
                                    {
                                        if (opr2[i][k].ToString() == "True" && dr[k].ToString() == "True")
                                            dr[k] = true;
                                        else
                                            dr[k] = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else//opr2不在ret中
                {
                    //临时保存leafs节点
                    List<DataRow> tmpLeafs = deepCopy(leafs);
                    List<DataRow> tmp2Leafs = deepCopy(leafs);
                    FilterRepeat(tmp2Leafs);        //过滤重复行
                    int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo

                    //opr2不在ret中，删除ret的末节点行
                    foreach (DataRow dr in tmp2Leafs)
                    {
                        DataRow[] tmp = ret.Select("RequestNodeID = '" + dr["RequestNodeID"].ToString() + "'");
                        foreach (DataRow d in tmp)
                            d.Delete();
                    }

                    leafs = tmpLeafs;
                    if (ret.Rows.Count == 0)//说明当前ret中只有一级节点
                    {
                        //int tmpOpr = Convert.ToInt32(leafs[0]["OrderNo"]);  //临时保存末节点OrderNo
                        if (tmpOpr <= operand2)
                        {
                            List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                            foreach (DataRow d1 in leafs)
                            {
                                d1["PID"] = null;
                                ret.Rows.Add(d1.ItemArray);
                                for (int j = 0; j < opr2.Count; j++)
                                {
                                    for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                    {
                                        if (d1[i].ToString() == "True" && opr2[j][i].ToString() == "True")
                                            tmpd[j][i] = true;
                                        else
                                            tmpd[j][i] = false;
                                    }
                                    tmpd[j]["PID"] = d1["AppID"].ToString();
                                    //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                        else
                        {
                            List<DataRow> tmpd = deepCopy(leafs);
                            foreach (DataRow d2 in opr2)
                            {
                                d2["PID"] = null;
                                ret.Rows.Add(d2.ItemArray);
                                for (int j = 0; j < leafs.Count; j++)
                                {
                                    for (int i = 4; i < leafs[j].ItemArray.Length - 2; i++)
                                    {
                                        if (leafs[j][i].ToString() == "True" && d2[i].ToString() == "True")
                                            tmpd[j][i] = true;
                                        else
                                            tmpd[j][i] = false;
                                    }
                                    tmpd[j]["PID"] = d2["AppID"].ToString();
                                    ret.Rows.Add(tmpd[j].ItemArray);
                                }
                            }
                        }
                    }
                    else//ret中有超过一级节点
                    {
                        List<DataRow> parentLeafs = GetLeaf(ret);       //取得删除后的末节点行
                        if (tmpOpr <= operand2 || tmpOpr > operand2)
                        {
                            int loop = tmp2Leafs.Count;
                            List<DataRow> caList = deepCopy(leafs);     //临时保存
                            for (int k = 0; k < ret.Rows.Count; k++)
                            {
                                int times = -1;      //记录循环(parentLeafs循环)次数
                                foreach (DataRow pdr in parentLeafs)
                                {
                                    ++times;
                                    if (ret.Rows[k]["AppID"].ToString() == pdr["AppID"].ToString())
                                    {
                                        int pos = 0;
                                        foreach (DataRow dr in opr2)
                                        {
                                            ++pos;
                                            dr["PID"] = pdr["AppID"].ToString();    //修改PID值
                                            dr["AppID"] = Guid.NewGuid().ToString();
                                            DataRow tdr = ret.NewRow();
                                            tdr.ItemArray = dr.ItemArray;
                                            ret.Rows.InsertAt(tdr, k + pos);    //将opr2中的一行添加到ret表中

                                            //与现有的末节点行进行计算
                                            for (int t = times * loop; t < times * loop + loop; t++)
                                            {
                                                ++pos;
                                                caList[t]["PID"] = dr["AppID"].ToString();
                                                caList[t]["AppID"] = Guid.NewGuid().ToString();
                                                for (int i = 4; i < leafs[t].ItemArray.Length - 2; i++)
                                                {
                                                    if (leafs[t][i].ToString() == "True" && dr[i].ToString() == "True")
                                                    {
                                                        caList[t][i] = true;
                                                    }
                                                    else
                                                    {
                                                        caList[t][i] = false;
                                                    }
                                                }
                                                DataRow tdr1 = ret.NewRow();
                                                tdr1.ItemArray = caList[t].ItemArray;
                                                ret.Rows.InsertAt(tdr1, k + pos);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    int loop = opr2.Count;
                        //    List<DataRow> caList = deepCopy(opr2);     //临时保存
                        //    for (int k = 0; k < ret.Rows.Count; k++)
                        //    {
                        //        //int times = -1;      //记录循环(parentLeafs循环)次数
                        //        foreach (DataRow pdr in parentLeafs)
                        //        {
                        //            //++times;
                        //            if (ret.Rows[k]["AppID"].ToString() == pdr["AppID"].ToString())
                        //            {
                        //                int pos = 0;
                        //                foreach (DataRow dr in leafs)
                        //                {
                        //                    ++pos;
                        //                    dr["PID"] = pdr["AppID"].ToString();    //修改PID值
                        //                    dr["AppID"] = Guid.NewGuid().ToString();
                        //                    DataRow tdr = ret.NewRow();
                        //                    tdr.ItemArray = dr.ItemArray;
                        //                    ret.Rows.InsertAt(tdr, k + pos);    //将opr2中的一行添加到ret表中
                        //                    //int t = times * loop;
                        //                    //int tpos = -1;
                        //                    for (int t = 0; t < opr2.Count; t++)
                        //                    {
                        //                        ++pos;
                        //                        caList[t]["PID"] = dr["AppID"].ToString();
                        //                        caList[t]["AppID"] = Guid.NewGuid().ToString();
                        //                        for (int i = 4; i < opr2[0].ItemArray.Length - 2; i++)
                        //                        {
                        //                            if (opr2[t][i].ToString() == "True" && leafs[i].ToString() == "True")
                        //                            {
                        //                                caList[t][i] = true;
                        //                            }
                        //                            else
                        //                            {
                        //                                caList[t][i] = false;
                        //                            }
                        //                        }
                        //                        DataRow tdr1 = ret.NewRow();
                        //                        tdr1.ItemArray = caList[t].ItemArray;
                        //                        ret.Rows.InsertAt(tdr1, k + pos);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
            }
            else//第一次计算过程
            {
                //在dt中查询opr1和opr2的行（顺序）
                List<DataRow> opr1 = new List<DataRow>();
                List<DataRow> opr2 = new List<DataRow>();

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand1.ToString())
                        opr1.Add(dr);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OrderNo"].ToString() == operand2.ToString())
                        opr2.Add(dr);
                }

                //去掉重复的行
                FilterRepeat(opr1);
                FilterRepeat(opr2);

                //判断两个元素是否一致
                if (IsSame(opr1, opr2))
                {
                    if (operand1 <= operand2)
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr1[i][j].ToString() == "True" && opr2[i][j].ToString() == "True")
                                {
                                    opr2[i][j] = true;
                                }
                                else
                                {
                                    opr2[i][j] = false;
                                }
                            }
                            opr2[i]["PID"] = null;
                            ret.Rows.Add(opr2[i].ItemArray);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < opr1.Count; i++)
                        {
                            for (int j = 4; j < opr1[i].ItemArray.Length - 2; j++)
                            {
                                if (opr1[i][j].ToString() == "True" && opr2[i][j].ToString() == "True")
                                {
                                    opr1[i][j] = true;
                                }
                                else
                                {
                                    opr1[i][j] = false;
                                }
                            }
                            opr1[i]["PID"] = null;
                            ret.Rows.Add(opr1[i].ItemArray);
                        }
                    }
                }
                else
                {
                    //比较大小
                    if (operand1 <= operand2)
                    {
                        List<DataRow> tmpd = deepCopy(opr2);        //将opr2拷贝到临时变量中
                        foreach (DataRow d1 in opr1)
                        {
                            d1["PID"] = null;
                            ret.Rows.Add(d1.ItemArray);
                            for (int j = 0; j < opr2.Count; j++)
                            {
                                for (int i = 4; i < opr2[j].ItemArray.Length - 2; i++)
                                {
                                    if (d1[i].ToString() == "True" && opr2[j][i].ToString() == "True")
                                        tmpd[j][i] = true;
                                    else
                                        tmpd[j][i] = false;
                                }
                                tmpd[j]["PID"] = d1["AppID"].ToString();
                                //d2["RequestNodeName"] = "&nbsp;&nbsp;&nbsp;&nbsp;" + d2["RequestNodeName"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                    else
                    {
                        List<DataRow> tmpd = deepCopy(opr1);
                        foreach (DataRow d2 in opr2)
                        {
                            d2["PID"] = null;
                            ret.Rows.Add(d2.ItemArray);
                            for (int j = 0; j < opr1.Count; j++)
                            {
                                for (int i = 4; i < opr1[j].ItemArray.Length - 2; i++)
                                {
                                    if (opr1[j][i].ToString() == "True" && d2[i].ToString() == "True")
                                        tmpd[j][i] = true;
                                    else
                                        tmpd[j][i] = false;
                                }
                                tmpd[j]["PID"] = d2["AppID"].ToString();
                                ret.Rows.Add(tmpd[j].ItemArray);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 过滤重复行
        /// </summary>
        /// <param name="dr"></param>
        private void FilterRepeat(DataRow[] dr)
        {
            //int k = 0;
            for (int i = 0; i < dr.Length; i++)
            {
                for (int j = i + 1; j < dr.Length; j++)
                {
                    if (dr[i] != null && dr[j] != null)
                    {
                        if (dr[i]["RequestNodeID"].ToString() == dr[j]["RequestNodeID"].ToString())
                        {
                            //++k;
                            dr[j].Delete();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 过滤重复的行
        /// </summary>
        /// <param name="drList"></param>
        private void FilterRepeat(List<DataRow> drList)
        {
            int len = drList.Count;
            for (int i = 0; i < len - 1; i++)
            {
                DataRow temp = drList[i];
                for (int j = i + 1; j < len; j++)
                {
                    if (temp["RequestNodeID"].ToString() == drList[j]["RequestNodeID"].ToString())
                    {
                        drList.RemoveAt(j);
                        j--;
                        len--;
                    }
                }
            }
        }

        /// <summary>
        /// 判断d1，d2是否一致
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        private bool IsSame(List<DataRow> d1, List<DataRow> d2)
        {
            if (d1[0]["OrderNo"].ToString() == d2[0]["OrderNo"].ToString())
                return true;
            else
            {
                if (d1.Count == d2.Count)
                {
                    for (int i = 0; i < d1.Count; i++)
                    {
                        if (d1[i]["RequestNodeID"].ToString() != d2[i]["RequestNodeID"].ToString())
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 判断operand是否是ret中的最末级叶子结点
        /// </summary>
        /// <param name="operand"></param>
        /// <param name="dt"></param>
        /// <param name="ret"></param>
        private bool IsLeaf(Int32 operand, DataTable dt, DataTable ret)
        {
            DataRow[] dr = dt.Select("OrderNo = " + operand.ToString());
            if (dr.Length > 0)
            {
                DataRow[] r = ret.Select("RequestNodeID = '" + dr[0]["RequestNodeID"].ToString() + "'");    //在结果表中查询相同的RequestNodeID行
                if (r.Length > 0)
                {
                    //在结果表中查找，r[0]行是否是末级子节点
                    DataRow[] leaf = ret.Select("PID = '" + r[0]["AppID"].ToString() + "'");
                    if (leaf.Length == 0)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 取出末子节点行
        /// </summary>
        /// <param name="dt"></param>
        private static List<DataRow> GetLeaf(DataTable dt)
        {
            List<DataRow> ldr = new List<DataRow>();

            foreach (DataRow d in dt.Rows)
            {
                DataRow[] tmp = dt.Select("PID = '" + d["AppID"].ToString() + "'");
                if (tmp.Length == 0)
                {
                    ldr.Add(d);
                }
            }

            return ldr;
        }

        ///// <summary>
        ///// 查找根节点
        ///// </summary>
        ///// <param name="dt"></param>
        //private void FindRootRow(DataTable dt)
        //{
        //    //查找根节点
        //    //List<DataRow> root = new List<DataRow>();
        //    //查找根节点
        //    foreach (DataRow d in dt.Rows)
        //    {
        //        if (d["PID"].ToString() == "NULL")
        //        {
        //            //root.Add(d);
        //            FindChildRow(d, dt);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 查找子节点
        ///// </summary>
        ///// <param name="dr"></param>
        ///// <param name="dt"></param>
        ///// <param name="sb"></param>
        //private void FindChildRow(DataRow dr, DataTable dt)
        //{
        //    //查找子节点
        //    DataRow[] childs = dt.Select("PID = '" + dr["AppID"].ToString() + "'");
        //    if (childs == null || childs.Length == 0)
        //        return;
        //    else
        //    {
        //        foreach (DataRow d in childs)
        //        {
        //            FindChildRow(d, dt);      //查找子节点
        //            FindParentRow(d, dt);     //查询父节点，并计算                    
        //        }
        //    }
        //}

        ///// <summary>
        ///// 查父节点，找到重复行并且修改DT中的IsDelete列的值
        ///// </summary>
        ///// <param name="dr"></param>
        ///// <param name="dt"></param>
        //private void FindParentRow(DataRow dr, DataTable dt)
        //{
        //    DataRow dtp = dr;       //保存当前要查找的节点
        //    while (dtp["PID"].ToString() != "NULL")
        //    {
        //        DataRow[] pRow = dt.Select("AppID = '" + dtp["PID"].ToString() + "'");
        //        if (pRow.Length == 1)
        //        {
        //            if (pRow[0]["RequestNodeID"].ToString() == dr["RequestNodeID"].ToString())
        //            {
        //                dr["IsDelete"] = true;
        //                //修改dt中与dr同级的行
        //                foreach (DataRow d in dt.Rows)
        //                {
        //                    if (d["PID"].ToString() == dr["PID"].ToString())
        //                        d["IsDelete"] = true;
        //                }
        //                dt.AcceptChanges();
        //                break;
        //            }
        //            else
        //            {
        //                dtp = pRow[0];
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 两个List深拷贝
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private List<DataRow> deepCopy(List<DataRow> source)
        {
            List<DataRow> result = new List<DataRow>();
            DataTable tmp = source.CopyToDataTable();
            //tmp.Copy()
            foreach (DataRow d in tmp.Rows)
            {
                result.Add(d);
            }
            return result;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="ldc"></param>
        /// <returns></returns>
        private object deepClone(object ldc)
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            Formatter.Serialize(stream, ldc);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }

        /// <summary>
        /// 找末节点行数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int endRows(DataTable dt)
        {
            DataTable tmp = dt.Copy();
            List<DataRow> endrow = GetLeaf(tmp);
            FilterRepeat(endrow);
            return endrow.Count;
        }

        /// <summary>
        /// 取得最终的结果集
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="result"></param>
        public static DataTable GetResultCollection(DataTable ret, DataTable result)
        {
            List<DataRow> retLeafs = GetLeaf(ret);
            List<DataRow> resultLeafs = GetLeaf(result);
            //StringBuilder sb = new StringBuilder();

            //将两个结果集合并
            foreach (DataRow d in resultLeafs)
            {
                string link = GetLinkString(d, result);
                string[] links = link.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); //拆分字符
                if (links.Length > 0)
                {
                    foreach (DataRow dr in retLeafs)
                    {
                        bool isOk = true;       //标识是否匹配
                        string retlink = GetLinkString(dr, ret);

                        foreach (string l in links)
                        {
                            if (!retlink.Contains(l))
                            {
                                isOk = false;
                                break;
                            }
                        }
                        if (isOk)
                        {
                            for (int i = 4; i < dr.ItemArray.Length - 2; i++)
                            {
                                d[i] = dr[i].ToString();
                            }
                        }
                    }
                }
            }

            //循环去掉多余的值
            bool isExists = false;      //标识是否需要保存
            foreach (DataRow dr in result.Rows)
            {
                isExists = false;
                foreach (DataRow d in resultLeafs)
                {
                    if (dr["AppID"].ToString() == d["AppID"].ToString())
                    {
                        isExists = true;
                        break;
                    }
                }
                if (!isExists)
                {
                    for (int i = 4; i < dr.ItemArray.Length - 2; i++)
                    {
                        dr[i] = DBNull.Value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 取得dr的父RequestNodeID链
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string GetLinkString(DataRow dr, DataTable dt)
        {
            //从result表的末节点行中依次取出每行，并生成父子RequestNodeID链
            StringBuilder sb = new StringBuilder();
            sb.Append(dr["RequestNodeID"].ToString());
            sb.Append(";");
            DataRow trow = dr;
            while (true)
            {
                DataRow[] t = dt.Select("AppID = '" + trow["PID"].ToString() + "'");
                if (t.Length == 1)
                {
                    sb.Append(t[0]["RequestNodeID"].ToString());
                    sb.Append(";");
                    trow = t[0];
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
    }
}
