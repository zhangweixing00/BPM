using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using System.Text;
using RuleExpression;
using K2Utility;

namespace OrgWebSite.Admin.Popup
{
    public partial class ConfigurationRuleResult : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ProcessID"]) && !string.IsNullOrEmpty(Request.QueryString["GroupName"]) && !string.IsNullOrEmpty(Request.QueryString["Formula"]))
                {
                    ProcessRuleBLL bll = new ProcessRuleBLL();
                    IList<ApproveRuleGroupInfo> argList = bll.GetApproveRuleGroupByGroupNameAndProcessID(Request.QueryString["GroupName"], Request.QueryString["ProcessID"]);
                    List<DataTable> dtList = new List<DataTable>();
                    DataTable result;
                    if (argList != null)
                    {
                        //int pos = 0;
                        foreach (ApproveRuleGroupInfo info in argList)
                        {
                            DataSet ds = bll.GetApproveTableByTableName(info.RuleTableName, info.ProcessID.ToString());     //审批表
                            dtList.Add(ds.Tables[0]);

                        }
                        result = dtList[0].Copy();
                        result.Clear();
                        StringBuilder sb = new StringBuilder();
                        Descartes.TableDescarts(dtList, result, 0, sb);
                        ChangeSpaceToNbsp(result);
                        Expression expression = new Expression();
                        DataTable dts = new DataTable();
                        DataTable resultModel = new DataTable();
                        string formula = Request.QueryString["Formula"];
                        formula = formula.Replace(" ", "").Replace("AND", "*").Replace("OR", "+").Replace("NOT", "-");
                        expression.clac(formula, result, ref dts, ref resultModel);     //测试用
                        DataTable t = Expression.GetResultCollection(dts, resultModel);
                        gvConfiguration.DataSource = t;
                        gvConfiguration.DataBind();
                    }
                }
            }
        }

        private void ChangeSpaceToNbsp(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["RequestNodeName"] = row["RequestNodeName"].ToString().Replace(" ", "&nbsp;");
            }
            dt.AcceptChanges();
        }

        protected void gvConfiguration_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
        }
    }
}