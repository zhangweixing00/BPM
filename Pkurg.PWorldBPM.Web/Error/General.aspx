<%@ Page Language="C#" AutoEventWireup="true" CodeFile="General.aspx.cs" Inherits="Error_General" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Error/css/error.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <div class="box_warp">
        <div class="box_left">
            <div class="box_right">
                <div class="box_content">
                    <div>
                        <div class="return">
                            <a href="../Default.aspx" title="返回首页">返回首页</a>
                        </div>
                        <h1>
                            系统出错了!</h1>
                    </div>
                    <div class="note">
                        <h2>
                            <asp:Label ID="lblTitle" runat="server" Visible="false" Text="出错原因:"></asp:Label></h2>
                        <div id="error">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
