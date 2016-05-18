using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;

namespace OrgWebSite.Admin
{
    /// <summary>
    /// Summary description for OperateHandler
    /// </summary>
    public class OperateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.Params["action"]))
            {
                string action = context.Request.Params["action"];
                string resultString = string.Empty; //记录返回结果
                switch (action)
                {
                    case "deleteProcessNode":
                        resultString = DeleteProcessNode();
                        break;
                    case "deleteRequestNode":
                        resultString = DeleteRequestNode();
                        break;
                    case "deleteRuleTable":
                        resultString = DeleteRuleTable();
                        break;
                }
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.Write(resultString);
            }
        }

        /// <summary>
        /// 删除流程审批节点
        /// </summary>
        /// <returns></returns>
        private string DeleteProcessNode()
        {
            string nodeId = HttpContext.Current.Request.Params["nodeId"];
            if (!string.IsNullOrEmpty(nodeId))
            {
                ProcessRuleBLL bll = new ProcessRuleBLL();
                bool ret = bll.DeleteProcessNode(nodeId);
                if (ret)
                    return "1";
            }
            return "0";
        }

        /// <summary>
        /// 删除流程审批节点
        /// </summary>
        /// <returns></returns>
        private string DeleteRequestNode()
        {
            string nodeId = HttpContext.Current.Request.Params["nodeId"];
            if (!string.IsNullOrEmpty(nodeId))
            {
                ProcessRuleBLL bll = new ProcessRuleBLL();
                bool ret = bll.DeleteRequestNode(nodeId);
                if (ret)
                    return "1";
            }
            return "0";
        }

        /// <summary>
        /// 删除规则表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        private string DeleteRuleTable()
        {
            string tableId = HttpContext.Current.Request.Params["tableId"];
            if (!string.IsNullOrEmpty(tableId))
            {
                ProcessRuleBLL bll = new ProcessRuleBLL();
                bool ret = bll.DeleteRuleTableByID(tableId);
                //bool ret = true;
                if (ret)
                    return "1";
            }
            return "0";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}