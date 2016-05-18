<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="OrgWebSite.Login" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>申请角色管理</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="nav">
            <p>
                <%-- <uc1:Sitemap ID="Sitemap1" runat="server" />--%>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
    </div>
    
    <div id="Div_peolistName" class="nav_1">
        <p>
            入口节点列表</p>
    </div>
    <div id="process_Node_List" class="pro_1" style="padding-bottom: 30px;">
        
         <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            登录名：
                        </th>
                        <td>
                            founder\<asp:TextBox ID="txtUserCode" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                           <%-- <asp:Button ID="btnConfirm" runat="server" CssClass="green_btn" Text="模拟用户" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="green_btn" Text="取消模拟" OnClick="btnCancel_Click" />--%>
                        </td>
                    </tr>
                </tbody>
            </table>
    </div>
  
    </form>
</body>
</html>
