<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" CodeBehind="DeptEdit.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.DeptEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>部门创建</title>
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
    <div class="divCommon">
        <table class="tbCommon" style="padding-left: 28px; padding-top: 16px;">
            <tr>
                <td style="width: 100px;">
                    部门编号
                </td>
                <td style="">
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtCode"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    部门名称
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtDepartment"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    部门缩写
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" runat="server" ID="txtAbbreviation"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    部门状态
                </td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblState" RepeatDirection="Horizontal">
                        <asp:ListItem Text="启用" Selected="True" Value="1"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    所属部门
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepts" onchange="CallServer(this)" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
                <asp:HiddenField runat="server" ID="hfOriDeptCode" />
            </tr>
            <tr>
                <td>
                    部门类型
                </td>
                <td>
                    <asp:DropDownList ID="ddlDeptType" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    部门序号
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNo" runat="server" MaxLength="5" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td colspan="2" style="text-align:center;">&nbsp;
                  </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:HiddenField runat="server" ID="hfAction" />
                    <asp:Button runat="server" CssClass="btnCommon" ID="btnSave" Text="保 存" OnClientClick="return ValidateData();"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
        <asp:Literal runat="server" ID="litScript"></asp:Literal>
        <script language="javascript" type="text/javascript">
                 function ValidateData()
                 {
                    var ddlDeptType = $$("ddlDeptType");
                    var vueDeptType = ddlDeptType.options[ddlDeptType.selectedIndex].value;
                    
                    var department = $$("txtDepartment").value;
                    
                    if(vueDeptType=='' || department=='')
                    {
                        alert("部门名称，部门类型不能为空！");
                        return false;
                    }
                    
                    return true;
                 }
                 
                function CallServer(obj)
                {
                    var deptCode = obj.options[obj.selectedIndex].value;
                    <%= Page.ClientScript.GetCallbackEventReference(this, "deptCode", "ReceiveServerData",null)%>;
                }
                function ReceiveServerData(rValue)
                {
                    var action = $$("hfAction").value;
                    var ddlOrderNO = $$("ddlOrderNO");
                    var ddlDept = $$("ddlDepts");
                    
                    var selectedDeptCode = ddlDept.options[ddlDept.selectedIndex].value;
                    var oriDeptCode = $$("hfOriDeptCode").value;
                }

        </script>
    </div>
    </form>
</body>
</html>
