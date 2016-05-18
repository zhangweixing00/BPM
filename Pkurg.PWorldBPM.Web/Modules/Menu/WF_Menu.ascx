<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WF_Menu.ascx.cs" Inherits="Modules_Menu_WF_Menu" %>
<%@ Register src="../AddSign/AddSignDeptInner.ascx" tagname="AddSignDeptInner" tagprefix="uc1" %>
<%@ Register src="../AddSign/AddSign.ascx" tagname="AddSign" tagprefix="uc2" %>
<!--快捷菜单-->
<div id="scroll">
    <div id="scroll_title">
        快捷菜单</div>
    <div id="scroll_button">
        <!--根据需要，放入按钮-->
        <ul class="scroll_ul" >
            <div id="Options" runat="server">
            </div>
            <uc2:AddSign ID="c_AddSign" runat="server" Visible="false" />
             <uc2:AddSign ID="c_ChangeSign" runat="server" Visible="false" />
            <uc1:AddSignDeptInner ID="c_AddSignDeptInner" runat="server"  Visible="false"/>
            <li><a href='#' onclick='window.close();'>关闭</a></li>
        </ul>
    </div>
</div>
