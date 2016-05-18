<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_RoleInfo.ascx.cs" Inherits="Sohu.OA.Web.Manage.RoleManage.UC.UC_RoleInfo" %>
<%@ Register Src="~/Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>

 <style type="text/css">
        .text
        {
            text-align: right;
            height:30px;
            width: 110px;
        }
        .text2
        {
            text-align: left;
            }
        .bordercss
        {
        	 width:110px;
        	 border-bottom:1px solid #333333;
        	 padding-bottom:2px;
        	}
        .roleUserList
        {
            text-align: right;
        }
        .roleUserList td
        {
            text-align: right;
        }
        .displayNone
        {
        	 display:none;
        	}
    </style>
 <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
<div class="rightTop">
        <p>
            <uc1:Sitemap ID="Sitemap1" runat="server" />
        </p>
        <p>
        </p>
    </div>
 <div id="divRoleInfo" runat="server" style="font-size: 12px; margin-left: 18px; width: 780px">
        <div class="rightTitle">
            <span>角色信息</span></div>
        <p>
            <table style="width: 769px;">
                
               <%-- <tr>
                    <td class=" text">
                        流程名称：
                    </td>
                    <td class="  text2">
                        <asp:Label ID="FlowName"  CssClass="bordercss"  runat="server" Width="95%"></asp:Label>
                    </td>
                    <td class=" text">
                        节点名称：
                    </td>
                    <td class="  text2">
                        <asp:Label ID="ActiveName"  CssClass="bordercss"  runat="server" Width="95%"></asp:Label>
                    </td>
                    <td class=" text">
                        &nbsp;
                    </td>
                    <td class="  text2">
                        &nbsp;
                    </td>
                </tr>--%>
                <tr>
                    <td class=" text" style=" width:10%">
                        角色名称：
                    </td>
                    <td class="  text2" >
                        <asp:Label ID="hfRoleName" CssClass="bordercss"  runat="server" Width="400px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class=" text" style=" width:10%">
                        组织名称：
                    </td>
                    <td class="  text2" >
                        <asp:Label ID="lblOrg" CssClass="bordercss"  runat="server" Width="400px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class=" text" style=" width:10%">
                        流程名称：
                    </td>
                    <td class="  text2" >
                        <asp:Label ID="lblProc" CssClass="bordercss"  runat="server" Width="400px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class=" text" style=" width:10%">
                        角色说明：
                    </td>
                    <td>
                        <asp:Label ID="Description"  CssClass="bordercss"  runat="server" Width="97%"></asp:Label>
                    </td>
                </tr>
            </table>
        </p>
        </div>