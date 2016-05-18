<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRoleAdd.aspx.cs" Inherits="Sohu.OA.Web.MenuRoleAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
     <script src="../../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
   <script language="javascript" type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
   </script>
    
    <style type="text/css">
         .ddlcss
        {
            border: 1px #999999 solid;
        }
        table td
        {
            line-height:3em;
            }
        #RoleAddTitle
        {
            width:770px;
            margin:10px 0 0 20px;
            height:10px;
            padding:10px;
            color:#76650b;
            font-weight:bold;
            background:url(../../pic/right_list_title_bg2.png) no-repeat;
        }
        </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="RoleAddTitle">
        <asp:Label ID="lblOperName" runat="server"></asp:Label>
    </div>
     <div id="role_detail" runat="server" style=" padding-left:20px; width:780px; height:220px" >
               <table border="0" cellpadding="5" width="780px" height="220px">
                  <tr>
                     <td style=" width:15%; text-align:right;">角色名称：</td>
                     <td style=" padding-left:20px;">
                       <asp:TextBox ID="txtRoleName" runat="server" Width="95%" 
                             style="padding-top: 4px;height: 18px; border: 1px #999999 solid;" 
                             MaxLength="50"></asp:TextBox>
                         <asp:Label ID="lblRoleName" runat="server" ForeColor="Red" Text="*"></asp:Label>
                      </td>
                  </tr>                  
                  <tr>
                     <td style=" width:15%; text-align:right;">组织名称：</td>
                     <td style=" padding-left:20px;">
                       <asp:DropDownList ID="ddlOrg" runat="server"  Width="150" Height="23" CssClass="ddlcss" DataTextField="OrgName"
                        DataValueField="ID">
                    </asp:DropDownList>
                      </td>
                  </tr>
                  <tr style=" display:none;">
                     <td style=" width:15%; text-align:right;">&nbsp;</td>
                     <td style=" padding-left:20px;">
                         <asp:HiddenField ID="hfRoleType" runat="server" Value="1" />
                         <asp:HiddenField ID="hfRoleNameValue" runat="server" />
                      </td>
                  </tr>
                  <tr>
                     <td style=" width:15%; text-align:right;">流程名称：</td>
                     <td style=" padding-left:20px;">
                        <asp:DropDownList ID="ddlProcess"  Width="150" Height="23" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                     <td style=" width:15%; text-align:right;">角色描述：</td>
                     <td style=" padding-left:20px;">
                       <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Width="95%" 
                           Height="54px" style="padding-top: 4px; border: 1px #999999 solid;" 
                             MaxLength="100"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                     <td colspan="2" style="padding-top:15px; text-align:center;">
                       <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnAffirm_over.png')"
                            onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnAffirm_nor.png')"
                            ImageUrl="~/pic/btnImg/btnAffirm_nor.png"
                            OnClick="btnSave_Click" />
                      </td>
                  </tr>
               </table>
        </div>
    </form>
</body>
</html>