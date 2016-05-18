<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="Workflow_Approval" %>

<%@ Register Src="../../Modules/Menu/WF_Menu.ascx" TagName="WF_Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>流程审批</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .nodisplay
        {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnDoAction" runat="server" Visible="true"   CssClass='nodisplay' OnClick="btnDoAction_Click" UseSubmitBehavior="true" />
    <asp:HiddenField  ID="hf_ActionName" Value="" runat="server"/>
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">流程审批</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center">
            <!--流程主表单-->
<%--            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>founder\zybpmadmin</asp:ListItem>
                <asp:ListItem>founder\zhangweixing</asp:ListItem>
            </asp:DropDownList>
            当前用户：<asp:TextBox ID="tbCurrentUser" runat="server"></asp:TextBox>--%>
            <asp:PlaceHolder ID="phForm" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    <!--快捷菜单-->
    <uc1:WF_Menu ID="WF_Menu1" runat="server" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function DoAction(action,type) {
            if (type == "0") {
               //客户端事件
                return;
            }
            else
            {
                //alert(action);
                $('#<%= hf_ActionName.ClientID %>').val(action);
                <%=Page.ClientScript.GetPostBackEventReference(btnDoAction,"",false) %>
            }
        }
    </script>
    <asp:HiddenField ID="hf_OpId" runat="server" />
    </form>
</body>
</html>
