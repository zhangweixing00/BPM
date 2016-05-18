<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuAdd.aspx.cs" Inherits="Sohu.OA.Web.Manage.RoleManage.MenuAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <script src="../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
      <script src="../../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
      <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
      <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
      <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
      <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
      <link href="../../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
      <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
      <script language="javascript" type="text/javascript" src="/JavaScript/Validate1.2.js"></script>
       <script language="javascript" type="text/javascript" src="/JavaScript/ValidateNoMustInput.js"></script>
       <script type="text/javascript">
        $(function () {
            var iva = new InputValidate(buttonID = "btnSave;", lostFocus = false, FaultCss = "faultclass");
        });
        </script>
      <style type="text/css">
         .ddlcss
        { 
            border: 1px #999999 solid;
        }
        table td
        {
            line-height:2em;
            }
        #MenuAddTitle
        {
            width:770px;
            margin:10px 0 0 20px;
            height:10px;
            padding:10px;
            color:#76650b;
            font-weight:bold;
            background:url(../../../pic/right_list_title_bg2.png) no-repeat;
        }
        </style>
    <title></title>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="MenuAddTitle">
        <asp:Label ID="lblOperName" runat="server"></asp:Label>
    </div>
     <div id="menu_detail" style=" padding-left:20px; width:770px; height:250px" >
               <table border="0" cellpadding="5" width="770px" height="250px">
                  <tr>
                     <td style=" width:15%; text-align:right;">菜单名称：</td>
                     <td style=" padding-left:20px;">
                         <asp:HiddenField ID="hfMenuGUID" runat="server" />
                       <asp:TextBox ID="MenuName" runat="server" Width="500px" 
                             style="padding-top: 4px;border: 1px #999999 solid;" 
                             MaxLength="30"></asp:TextBox>
                         <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                      </td>
                  </tr>                   
                  <tr>
                     <td style=" width:15%; text-align:right;">菜单类型：</td>
                     <td style=" padding-left:20px;">
                       <asp:DropDownList ID="MenuType" runat="server" CssClass="ddlcss" Height="21px" 
                           Width="280px">
                           <asp:ListItem Value="LEFT">导航菜单</asp:ListItem>
                           <asp:ListItem Value="RIGHT">页面菜单</asp:ListItem>
                       </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                     <td style=" width:15%; text-align:right;">菜单URL：</td>
                     <td style=" padding-left:20px;">
                       <asp:TextBox ID="MenuURL" runat="server" Width="500px" 
                             style="padding-top: 4px;height: 18px; border: 1px #999999 solid;" 
                             MaxLength="1000"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                     <td style=" width:15%; text-align:right;">上级菜单：</td>
                     <td style=" padding-left:20px;">
                         <asp:DropDownList ID="ParentMenuGuid" runat="server" CssClass="ddlcss" 
                             Width="280px" Height="22px">
                             <asp:ListItem Value="">请选择</asp:ListItem>
                         </asp:DropDownList>
                      </td>
                  </tr>
                 <%-- <tr>
                     <td style=" width:15%; text-align:right;">排序号码：</td>
                     <td style=" padding-left:20px;">
                         <asp:TextBox ID="DisplayOrder" runat="server" Width="500px" 
                             style="padding-top: 4px;border: 1px #999999 solid;" MaxLength="10"></asp:TextBox>
                         <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                      </td>
                  </tr>--%>
                  <tr>
                     <td colspan="2" style="padding-top:15px; text-align:center;">
                       <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnAffirm_over.png')"
                            onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnAffirm_nor.png')"
                            ImageUrl="~/pic/btnImg/btnAffirm_nor.png" onclick="btnSave_Click" />
                      </td>
                  </tr>
               </table>
        </div>
    </form>
</body>
</html>
