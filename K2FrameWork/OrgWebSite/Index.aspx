<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Sohu.OA.Web.Index" %>

<%@ Register Src="Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="styles/styleindex.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        a
        {
            cursor: pointer;
        }
        .ruzhiguanli
        {
            width: 778px;
            margin-left: 25px;
        }
        .ruzhiguanli .title
        {
            height: 24px;
            width: 100%;
            float: left;
            color: #5b5b5b;
            background-image: url(pic/right_list_title_bg2.png);
        }
        .ruzhiguanli .title p
        {
            color: #76650b;
            font-weight: bold;
            margin-top: 6px;
            margin-left: 18px;
        }
        .ruzhiguanli ul
        {
            width: 100%;
            list-style: none;
            margin: 0;
            padding: 0;
            list-style-type: none;
        }
        .ruzhiguanli ul li
        {
            height: 21px;
            line-height: 21px;
            text-align: left;
            float: left;
            width: 134px;
            margin: 3px 0px;
            margin-left: 45px;
            padding: 0;
            list-style: 0;
            text-align: center;
            font-size: 12px;
        }
        .ruzhiguanli ul li a span
        {
            height: 21px;
            line-height: 21px;
            margin-bottom: 100px;
            color: #0066ff;
            float: left;
        }
        .ruzhiguanli ul li img
        {
            margin-bottom: -5px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="nav">
        <p>
            <uc1:Sitemap ID="Sitemap1" runat="server" />
        </p>
    </div>
    <div style="height: 12px;">
    </div>
    <%=this.LeftMenuString %>
    <%--   <div class=" ruzhiguanli">
        <div class="title">
           <p> 入职离职管理</p>
        </div>
        <ul>
         
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
        </ul>
    </div>
    <div class=" ruzhiguanli">
        <div class="title">
           <p> 入职离职管理</p>
        </div>
        <ul>
            <li>
                <img src="pic/right_an2.jpg" /><a href="Process/OAF/HRBP/OnBoardManage.aspx"><span>待入职人员录入</span></a></li>
        </ul>
    </div>--%>
    </form>
</body>
</html>
