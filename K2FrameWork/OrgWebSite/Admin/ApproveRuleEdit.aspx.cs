using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Utility;
using Model;
using BLL;
using K2Utility;
using System.Data;

namespace OrgWebSite.Admin
{
    public partial class ApproveRuleEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["ProcessID"]))
                {
                    string processID = Request.QueryString["ProcessID"];
                    BindProcessType(processID);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["TableId"]))
                {
                    string tableId = Request.QueryString["TableId"];
                    InintApproveRule(tableId);
                }
            }
            else
            {
               
                 if (Request.Form["__EVENTTARGET"] == "lbReload1")
                {
                    lbReload_Click(null, null);
                }
            }
        }
        private void BindProcessType(string processID)
        {
            ddlProcessType.Items.Clear();
            ProcessTypeBLL bll = new ProcessTypeBLL();
            IList<ProcessTypeInfo> processList = bll.GetProcessType();
            foreach (var item in processList)
            {
                if (item.ID.Trim()==processID)
                {
                    ListItem listitem = new ListItem(item.ProcessType,item.ID);

                    ddlProcessType.Items.Add(listitem);
                }
            }
            //ddlProcessType.DataSource = bll.GetProcessType();
            //ddlProcessType.DataBind();
        }

        /// <summary>
        /// 绑定新建的规则表
        /// </summary>
        private void BindRuleTable()
        {
            if (!string.IsNullOrEmpty(hfSelectedApproveNode.Value) || !string.IsNullOrEmpty(hfSelectedRequestNode.Value))
            {
                tApproveTable.Rows.Clear();

                //选择的审批节点
                ProcessRuleBLL bll = new ProcessRuleBLL();
                IList<ProcessNodeInfo> pnList = bll.GetApproveNodesByNodeIds(hfSelectedApproveNode.Value);

                //选择的申请节点
                IList<RequestNodeInfo> rnList = bll.GetRequestNodesByNodeIds(hfSelectedRequestNode.Value);

                if (pnList != null)
                {
                    TableRow trHead = new TableRow();

                    TableCell tcHeadFoodName = new TableCell();
                    TableCell tcHeadType = new TableCell();


                    tcHeadFoodName.Text = "发起节点";
                    tcHeadType.Text = "表达式";


                    trHead.Cells.Add(tcHeadFoodName);
                    trHead.Cells.Add(tcHeadType);

                    trHead.HorizontalAlign = HorizontalAlign.Center;

                    trHead.Height = 30;

                    //循环创建列
                    foreach (ProcessNodeInfo info in pnList)
                    {
                        TableCell tdc = new TableCell();
                        tdc.Text = info.NodeName;
                        trHead.Cells.Add(tdc);
                    }

                    tApproveTable.Rows.Add(trHead);

                    int rowLoop = 0;
                    int colLoop = 1;

                    if (rnList != null)
                    {
                        //循环创建行
                        foreach (RequestNodeInfo info in rnList)
                        {
                            colLoop = 1;
                            ++rowLoop;
                            TableRow tr = new TableRow();
                            TableCell tc1 = new TableCell();
                            TableCell tc2 = new TableCell();

                            Label lb1 = new Label();
                            lb1.ID = info.NodeID.ToString();
                            lb1.Text = info.NodeName;

                            Label lb2 = new Label();
                            lb2.ID = info.NodeID.ToString() + "_2";
                            lb2.Text = info.Expression;

                            tc1.Controls.Add(lb1);
                            tr.Cells.Add(tc1);
                            tc2.Controls.Add(lb2);
                            tr.Cells.Add(tc2);

                            //添加CheckBox
                            foreach (ProcessNodeInfo pn in pnList)
                            {
                                ++colLoop;
                                TableCell tc = new TableCell();
                                CheckBox cb = new CheckBox();
                                cb.ID = "cbApprove" + rowLoop + colLoop;
                                cb.Attributes.Add("RID", info.NodeID.ToString());
                                cb.Attributes.Add("PID", pn.NodeID.ToString());
                                cb.Attributes.Add("onclick", "checkNode(this,'" + info.NodeID.ToString() + "','" + pn.NodeID.ToString() + "')");

                                //cb.Checked = bll.GetIsApproveByNodeIDs(info.NodeID.ToString(), pn.NodeID.ToString());
                                cb.Checked = false;
                                if (hfSelectedCheckBox.Value.Contains(info.NodeID.ToString() + ";" + pn.NodeID.ToString() + ";"))
                                {
                                    cb.Checked = true;
                                }
                                tc.Controls.Add(cb);
                                tr.Cells.Add(tc);
                            }
                            tApproveTable.Rows.Add(tr);
                        }
                    }
                }
            }
        }
        protected void lbReload_Click(object sender, EventArgs e)
        {
            //重新绑定
            //BindProcessType();

            BindRuleTable();
        }
        protected void InintApproveRule(string tableId)
        {
            hfSelectedApproveNode.Value = string.Empty;
            hfSelectedRequestNode.Value = string.Empty;
            hfSelectedCheckBox.Value = string.Empty;

            //通过ID取得审批表选中的节点ID
            ProcessRuleBLL bll = new ProcessRuleBLL();
            DataSet ds = bll.GetNodeIDsByTableID(tableId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!hfSelectedRequestNode.Value.Contains(dr["RequestNodeID"].ToString()))
                    {
                        hfSelectedRequestNode.Value += dr["RequestNodeID"].ToString() + ";";
                    }
                    if (!hfSelectedApproveNode.Value.Contains(dr["ProcessNodeID"].ToString()))
                    {
                        hfSelectedApproveNode.Value += dr["ProcessNodeID"].ToString() + ";";
                    }
                }
            }
            ds = bll.GetRuleInfoByTableID(tableId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtGroup.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();
                txtRequestSPName.Text = ds.Tables[0].Rows[0]["RequestSPName"].ToString();
                //txtRuleTableName.Text = ds.Tables[0].Rows[0]["RuleTableName"].ToString();
            }
            ReBindRuleTable();
        }
        /// <summary>
        /// 创建审批规则的XML
        /// </summary>
        /// <returns></returns>
        private string CreateRuleXML()
        {
            bool isFirst = true;
            string[] requestNodes = hfSelectedRequestNode.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] approveNode = hfSelectedApproveNode.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (requestNodes.Length > 0 && approveNode.Length > 0)
            {
                string ruleTableName = "Tb_" + txtGroup.Text;
                XmlDocument xDoc = new XmlDocument();
                int rowLoop = 0;
                int colLoop = 1;

                foreach (string rn in requestNodes)
                {
                    ++rowLoop;
                    colLoop = 1;
                    foreach (string an in approveNode)
                    {
                        ++colLoop;
                        CheckBox cbApprove = tApproveTable.Rows[rowLoop].Cells[colLoop].FindControl("cbApprove" + rowLoop + colLoop) as CheckBox;

                        if (cbApprove != null)
                        {
                            string approve = cbApprove.Checked == true ? "true" : "false";
                            if (isFirst)
                            {
                                AttributeParameter[] parms ={
                                            new AttributeParameter("ID",Guid.NewGuid().ToString()),
                                            new AttributeParameter("RequestNodeID",rn),
                                            new AttributeParameter("ApproveNodeID",an),
                                            new AttributeParameter("IsApprove",approve),
                                            new AttributeParameter("RuleTableName",ruleTableName),
                                            new AttributeParameter("CreatedBy",Page.User.Identity.Name)
                                       };
                                XmlParameter[] xmlParms ={
                                         new XmlParameter("Node",parms)
                                    };

                                xDoc = XmlHelper.CreateXML("ApproveRule", "ApproveNodes", xmlParms);    //创建XML
                                isFirst = false;
                            }
                            else
                            {
                                AttributeParameter[] parms ={
                                            new AttributeParameter("ID",Guid.NewGuid().ToString()),
                                            new AttributeParameter("RequestNodeID",rn),
                                            new AttributeParameter("ApproveNodeID",an),
                                            new AttributeParameter("IsApprove",approve),
                                            new AttributeParameter("RuleTableName",ruleTableName),
                                            new AttributeParameter("CreatedBy",Page.User.Identity.Name)
                                       };
                                XmlParameter[] xmlParms ={
                                         new XmlParameter("Node",parms)
                                    };

                                XmlHelper.AddNewNode(xDoc, "ApproveNodes", xmlParms);           //添加新节点
                            }
                        }
                    }
                }
                return xDoc.InnerXml;
            }
            return string.Empty;
        }

        private void ReBindRuleTable()
        {
            if (!string.IsNullOrEmpty(hfSelectedApproveNode.Value) || !string.IsNullOrEmpty(hfSelectedRequestNode.Value))
            {
                tApproveTable.Rows.Clear();

                //选择的审批节点
                ProcessRuleBLL bll = new ProcessRuleBLL();
                IList<ProcessNodeInfo> pnList = bll.GetApproveNodesByNodeIds(hfSelectedApproveNode.Value);

                //选择的申请节点
                IList<RequestNodeInfo> rnList = bll.GetRequestNodesByNodeIds(hfSelectedRequestNode.Value);

                if (pnList != null)
                {
                    TableRow trHead = new TableRow();

                    TableCell tcHeadFoodName = new TableCell();
                    TableCell tcHeadType = new TableCell();


                    tcHeadFoodName.Text = "发起节点";
                    tcHeadType.Text = "表达式";


                    trHead.Cells.Add(tcHeadFoodName);
                    trHead.Cells.Add(tcHeadType);

                    trHead.HorizontalAlign = HorizontalAlign.Center;

                    trHead.Height = 30;

                    //循环创建列
                    foreach (ProcessNodeInfo info in pnList)
                    {
                        TableCell tdc = new TableCell();
                        tdc.Text = info.NodeName;
                        trHead.Cells.Add(tdc);
                    }

                    tApproveTable.Rows.Add(trHead);

                    int rowLoop = 0;
                    int colLoop = 1;

                    if (rnList != null)
                    {
                        //循环创建行
                        foreach (RequestNodeInfo info in rnList)
                        {
                            colLoop = 1;
                            ++rowLoop;
                            TableRow tr = new TableRow();
                            TableCell tc1 = new TableCell();
                            TableCell tc2 = new TableCell();

                            Label lb1 = new Label();
                            lb1.ID = info.NodeID.ToString();
                            lb1.Text = info.NodeName;

                            Label lb2 = new Label();
                            lb2.ID = info.NodeID.ToString() + "_2";
                            lb2.Text = info.Expression;

                            tc1.Controls.Add(lb1);
                            tr.Cells.Add(tc1);
                            tc2.Controls.Add(lb2);
                            tr.Cells.Add(tc2);

                            //添加CheckBox
                            foreach (ProcessNodeInfo pn in pnList)
                            {
                                ++colLoop;
                                TableCell tc = new TableCell();
                                CheckBox cb = new CheckBox();
                                cb.ID = "cbApprove" + rowLoop + colLoop;
                                cb.Attributes.Add("RID", info.NodeID.ToString());
                                cb.Attributes.Add("PID", pn.NodeID.ToString());
                                cb.Attributes.Add("onclick", "checkNode(this,'" + info.NodeID.ToString() + "','" + pn.NodeID.ToString() + "')");

                                cb.Checked = bll.GetIsApproveByNodeIDs(info.NodeID.ToString(), pn.NodeID.ToString(), txtGroup.Text.Trim());
                                if (cb.Checked)
                                {
                                    hfSelectedCheckBox.Value += info.NodeID.ToString() + ";" + pn.NodeID.ToString() + ";";
                                }
                                tc.Controls.Add(cb);
                                tr.Cells.Add(tc);
                            }
                            tApproveTable.Rows.Add(tr);
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BindRuleTable();
            string xml = CreateRuleXML();
            ProcessRuleBLL bll = new ProcessRuleBLL();
            string ruleTableName = "Tb_" + txtGroup.Text;
            bll.InsertApproveRule(xml, txtRequestSPName.Text, ruleTableName, txtGroup.Text, ddlProcessType.SelectedValue);



            MessageBox.ShowAndClose(this.Page, "操作成功", "/Admin/ApproveRuleManage.aspx");
        }
    }
}