<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRoleView.aspx.cs" Inherits="Sohu.OA.Web.Manage.RoleManage.MenuRoleView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #MenuRoleTitle
        {
            width:600px;
            margin:10px 0 0 20px;
            height:10px;
            padding:10px;
            color:#76650b;
            font-weight:bold;
            background:url(../../../pic/right_list_title2.jpg) no-repeat;
        }
         #MenuRoleContent
        {
            width:600px;
            margin:10px 0 0 20px;
            padding:10px;
        }
          #MenuMes
        {
            width:600px;
            margin:10px 0 0 20px;
            padding:10px;
           
        }
        .bordercss
        {
            width: 73%; /**width:343px;**/
            border: 0px;
            border-bottom: 1px solid #333333;
             text-align:center;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="MenuRoleTitle">
            <p>
                查看菜单角色</p>
        </div>
        <div id="MenuMes">
            <table width="600px" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style=" width:80px; text-align:right;">
                        菜单名称：</td>
                    <td style=" width:135px; text-align:center;">
                        <asp:TextBox ID="txtMenuName" runat="server" Width="135px" CssClass="bordercss"></asp:TextBox>
                    </td>
                    <td style=" width:80px; text-align:right;">
                        菜单类型：</td>
                    <td style=" width:135px; text-align:center;">
                        <asp:TextBox ID="txtMenuType" runat="server" Width="135px" CssClass="bordercss"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="MenuRoleContent">
             <asp:GridView ID="GridView1" runat="server" Width="600px" ClientIDMode="Static" AutoGenerateColumns="False"
                                    EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无添加角色信息</div>" BorderWidth="1px"
                                    CssClass="datalist2">
               <Columns>
                   <asp:TemplateField>
                       <HeaderTemplate>
                          角色名称
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:Label ID="RoleName" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <HeaderTemplate>
                          角色描述
                       </HeaderTemplate>
                       <ItemTemplate>
                           <asp:Label ID="Description" runat="server" Text='<%#Eval("Desciption") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
                  <HeaderStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
