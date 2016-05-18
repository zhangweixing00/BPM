using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using K2Utility;

namespace OrgWebSite.Admin.Popup
{
    public partial class RequestNodeEdit : BasePage
    {
        /// <summary>
        /// new和edit状态
        /// </summary>
        public string State
        {
            get
            {
                return Request.QueryString["state"];
            }
        }

        /// <summary>
        /// 节点所属流程ID
        /// </summary>
        public string ProcessID
        {
            get
            {
                return Request.QueryString["ProcessID"];
            }
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeID
        {
            get
            {
                return Request.QueryString["NodeID"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ProcessID))
                {
                    if (State.Equals("edit", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(NodeID))
                        BindData();
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 当为编辑状态(edit)时绑定数据
        /// </summary>
        private void BindData()
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            RequestNodeInfo info = bll.GetRequestNodeByNodeId(NodeID);
            if (info != null)
            {
                txtNodeName.Text = info.NodeName;
                txtExpression.Text = info.Expression;
                hfProcessID.Value = info.ProcessID.ToString();
            }
        }

        /// <summary>
        /// 创建页面对象
        /// </summary>
        /// <returns></returns>
        private RequestNodeInfo CreateObject()
        {
            RequestNodeInfo info = new RequestNodeInfo();
            info.NodeName = txtNodeName.Text;
            info.ProcessID = ProcessID;
            info.Expression = txtExpression.Text;
            if (!string.IsNullOrEmpty(NodeID) && NodeID != "undefined")
                info.NodeID = new Guid(NodeID);
            return info;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ProcessRuleBLL bll = new ProcessRuleBLL();
            bool ret = false;
            if (State.Equals("new", StringComparison.OrdinalIgnoreCase))
            {
                ret = bll.InsertRequestNode(CreateObject());
                if (ret)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('添加成功');</script>");
                }
                else
                {
                    ExecAlertScritp("添加失败");
                }
            }
            else
            {
                ret = bll.UpdateRequestNode(CreateObject());
                if (ret)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>AlertAndNewLoad('更新成功');</script>");
                }
                else
                {
                    ExecAlertScritp("更新失败");
                }
            }
        }
    }
}