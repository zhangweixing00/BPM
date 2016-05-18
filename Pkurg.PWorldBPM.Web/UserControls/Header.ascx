<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<div id="header" style="border-bottom: none;">
    <div>
        <a href="/Default.aspx">
            <img src="/Resource/images/logo.png" alt="北大资源" style="float: left;" /></a>
        <div id="userInfo">
        </div>
    </div>
    <div id="menusDiv" style="clear: both;">
        <ul id="menus">
            <%--            <li><a href="/Default.aspx">BPM首页</a></li>
            <li>|</li>--%>
            <li><a href="http://oa.founder.com/Group/Resource/Default.aspx">首页</a></li>
            <li>|</li>
            <li><a href="http://oa.founder.com/Group/Resource/NewsCenter/Lists/CompanyNews">新闻中心</a></li>
            <li>|</li>
            <li><a href="http://zyinfo.founder.com/doc/default.aspx">文档中心</a></li>
            <li>|</li>
            <li><a href="http://zybpm.founder.com/Default.aspx">流程管理</a></li>
            <li>|</li>
            <li><a href="http://oa.founder.com/Group/Resource/CorporateCulture/Pages/Default.aspx">
                企业文化</a></li>
            <li>|</li>
            <li><a href="http://oa.founder.com/Group/Resource/CommonService/Lists/CommonList">公共服务区</a></li>
            <li>|</li>
            <li><a href="http://oa.founder.com/OAWeb/FounderOAResourceGroup/Modules/FounderOA/Portal/SelfWorkspace.aspx">
                个人工作区</a></li>           
        </ul>
    </div>
</div>
