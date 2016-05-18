<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="UserEdit.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.UserEdit" %>

<%@ Register Src="~/Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人员操作</title>
    <base target="_self" />
    <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../JavaScript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery.query-2.1.7.js"></script>
    <script src="../../JavaScript/CheckBox.js" type="text/javascript"></script>
    <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <link href="../../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/Validate1.3.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Javascript/DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg)
        {
            ymPrompt.alert({ title: '提示信息', message: msg });
        }
        function AlertAndNewLoad(msg)
        {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { window.location.href = '/Admin/UserManage.aspx'; } } });
        }
    </script>
    <style type="text/css">
        body
        {
            font-size: 12px;
        }
        
        .login, .login_form
        {
            padding: 3px 0 0 10px;
            float: left;
        }
        li
        {
            list-style: none;
        }
    </style>
    <style type="text/css">
        .text
        {
            text-align: right;
            height: 30px;
            width: 110px;
        }
        .text2
        {
            text-align: left;
            width: 120px;
        }
        .bordercss
        {
            width: 110px;
            border-bottom: 1px solid #333333;
            padding-bottom: 2px;
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
            display: none;
        }
    </style>
</head>
<body class="bd_OpenPage" >
    <form id="form1" runat="server">
    <div class="rightTop">
        <p>
            <uc1:Sitemap ID="Sitemap1" runat="server" />
        </p>
    </div>
    <div style="padding-left: 18px;">
        <div class="rightTitle">
            <span>人员信息</span></div>
        <table style="width: 769px;">
            <tbody>
                <tr>
                    <td class=" text">
                        &nbsp;
                    </td>
                    <td colspan="5" style="text-align: right;">
                        <asp:ImageButton ID="ibtAdd" ClientIDMode="Static" runat="server" ImageUrl="~/pic/btnImg/save_nor.png"
                            onmouseover="this.src='/pic/btnImg/btnsave_over.png'" onmouseout="this.src='/pic/btnImg/save_nor.png'"
                            OnClick="btnSave_Click" />
                        <asp:ImageButton ID="btnReturnList" runat="server" onmouseover="this.src='/pic/btnImg/btnBackList_over.png'"
                            onmouseout="this.src='/pic/btnImg/btnBackList_nor.png'" OnClientClick="javascript: window.location.href='/Admin/UserManage.aspx';window.close();return false;"
                            ImageUrl="~/pic/btnImg/btnBackList_nor.png" Style="margin-left: 15px;" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table style="width: 769px;">
            <tr>
                <td width="80" height="20">
                    中文名称
                </td>
                <td width="190">
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtCHNName" runat="server"></asp:TextBox>
                </td>
                <td width="80">
                    员工编号
                </td>
                <td width="190">
                    <asp:TextBox ID="txtEmployeeID" Style="height: 22px; border: 1px #999999 solid;"
                        runat="server"></asp:TextBox>
                </td>
                <td width="200" rowspan="14" align="center" bgcolor="#FFFFFF">
                    <asp:Image Style="" ImageUrl="~/Img/noimg.jpg" runat="server" ID="imgPhono" />
                </td>
            </tr>
            <tr>
                <td height="20">
                    英文名称
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtENName" runat="server"></asp:TextBox>
                </td>
                <td>
                    成本中心
                </td>
                <td>
                    <asp:TextBox ID="txtCostCenter" Style="height: 22px; border: 1px #999999 solid;"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    籍贯</td>
                <td colspan="3">
                    国家：<asp:DropDownList ID="ddlNation" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                    省：<asp:DropDownList ID="ddlPromary" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                    市：<asp:DropDownList ID="ddlCity" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="20">
                    AD账号
                </td>
                <td style="">
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtADAccount" runat="server"></asp:TextBox>
                </td>
                <td>
                    工作地点
                </td>
                <td>
                    <asp:DropDownList ID="ddlWorkPlace" CssClass="ddlcss" runat="server">
                        <asp:ListItem Text="北京" Value="BJ"></asp:ListItem>
                        <asp:ListItem Text="香港" Value="HK"></asp:ListItem>
                        <asp:ListItem Text="所有" Value="All"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="22" colspan="4" bgcolor="#fef5c7">
                    <strong>基本资料</strong>
                </td>
            </tr>
            <tr>
                <td height="20">
                    性别
                </td>
                <td>
                    <asp:DropDownList CssClass="ddlcss" runat="server" ID="ddlGender">
                        <asp:ListItem Text="Choose" Value="N"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    出生日期
                </td>
                <td>
                    <asp:TextBox ID="txtBirthDate" Style="height: 22px; border: 1px #999999 solid;" onfocus="new WdatePicker(this)"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    邮箱
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td>
                    政治面貌
                </td>
                <td>
                    <asp:TextBox ID="txtPA" Style="height: 22px; border: 1px #999999 solid;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    手机
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtCellPhone" runat="server"></asp:TextBox>
                </td>
                <td>
                    入职日期
                </td>
                <td>
                    <asp:TextBox ID="txtHireDate" Style="height: 22px; border: 1px #999999 solid;" onfocus="new WdatePicker(this)"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    办公电话
                </td>
                <td>
                    <asp:TextBox Style="height: 22px; border: 1px #999999 solid;" ID="txtOfficePhone"
                        runat="server"></asp:TextBox>
                </td>
                <td>
                    职位
                </td>
                <td>
                    <asp:DropDownList ID="ddlPosition" CssClass="ddlcss" runat="server">
                    <asp:ListItem Text="yuangong" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="20">
                    传真号码
                </td>
                <td>
                    <asp:TextBox ID="txtFax" Style="height: 22px; border: 1px #999999 solid;" runat="server"></asp:TextBox>
                </td>
                <td>
                    毕业院校
                </td>
                <td>
                    <asp:TextBox ID="txtGraduateFrom" Style="height: 22px; border: 1px #999999 solid;"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    BB号码
                </td>
                <td>
                    <asp:TextBox ID="txtBlackBerry" Style="height: 22px; border: 1px #999999 solid;"
                        runat="server"></asp:TextBox>
                </td>
                <td>
                    最高学历
                </td>
                <td>
                    <asp:TextBox ID="txtOAC" Style="height: 22px; border: 1px #999999 solid;" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    经理账号
                </td>
                <td>
                    <asp:TextBox ID="txtManagerAccount" Style="height: 22px; border: 1px #999999 solid;"
                        runat="server"></asp:TextBox>
                </td>
                <td>
                    顺序号
                </td>
                <td>
                    <asp:TextBox ID="txtOrderNO" Style="height: 22px; border: 1px #999999 solid;" runat="server"
                        MaxLength="5">100</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20" id="tdMain" runat="server">
                    主部门
                </td>
                <td>
                    <asp:DropDownList ID="ddlMainDept" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="20">
                    <%--所属部门--%>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepts" Style="display: none" CssClass="ddlcss" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="22" colspan="5" bgcolor="#fef5c7">
                    <strong>经历描述</strong>
                </td>
            </tr>
            <tr>
                <td width="60" height="20">
                    岗位职责<br />
                    描述
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtPositionDesc" Style="border: 1px #999999 solid;" runat="server"
                        Height="50px" Width="600px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    教育背景
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtEductionBackground" Style="border: 1px #999999 solid;" runat="server"
                        TextMode="MultiLine" Height="50px" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    进公司前<br />
                    工作经历
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtWorkExperienceBefore" Style="border: 1px #999999 solid;" runat="server"
                        TextMode="MultiLine" Height="50px" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    本公司工作<br />
                    经历
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtWorkExperienceNow" Style="border: 1px #999999 solid;" runat="server"
                        TextMode="MultiLine" Height="50px" Width="600px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    照片路径
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txtPhotoUrl" Style="height: 22px; border: 1px #999999 solid;" runat="server"
                        Width="500px"></asp:TextBox>
                    <asp:Button ID="btnShowPhoto" runat="server" CssClass="btnCommon" Text="显示照片" OnClick="btnShowPhoto_Click" />
                </td>
            </tr>
            <tr>
                <td height="20" colspan="5" style="float: right; padding-right: 5px; text-align: right;">
                </td>
            </tr>
            <tr>
                <td height="22" colspan="5" bgcolor="#fef5c7">
                    <strong>扩展属性</strong>
                </td>
            </tr>
            <tr>
                <td height="20" colspan="5">
                    <asp:DataList ID="dlExtendInfo" runat="server" Width="100%">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td style="text-align: left; width: 68px;">
                                        <asp:Label ID="lbPropertyName" runat="server" Text='<%#Eval("UserExtProperty") %>'></asp:Label>
                                        <asp:HiddenField ID="hfPropertyID" runat="server" Value='<%#Eval("UserExtPropID") %>' />
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPropertyValue" runat="server" MaxLength="100" Text='<%#Eval("Value") %>'
                                            Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <div class="divCommand" style="text-align: center;">
            <asp:Literal runat="server" ID="litScript"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>
