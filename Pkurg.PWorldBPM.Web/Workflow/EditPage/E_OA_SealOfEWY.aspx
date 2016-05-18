<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_SealOfEWY.aspx.cs" Inherits="Workflow_EditPage_E_OA_SealOfEWY" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                selectOneCheckList("cblSecurityLevel");
                selectOneCheckList("cblUrgenLevel");
                selectOneCheckList("cblRemark");
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">发起流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    印章使（借）用审批单
                </div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                        </td>
                        <th>
                            保密等级：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="绝密">绝密</asp:ListItem>
                                <asp:ListItem Value="机密">机密</asp:ListItem>
                                <asp:ListItem Value="秘密">秘密</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <th>
                            紧急程度：
                        </th>
                        <td>
                            <asp:CheckBoxList ID="cblUrgenLevel" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="加急">加急</asp:ListItem>
                                <asp:ListItem Value="紧急">紧急</asp:ListItem>
                                <asp:ListItem Value="一般">一般</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table id="tb1" class="wf_table2" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    申请部门：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlDeptName" runat="server" AutoPostBack="True" Width="350px"
                                        OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged" Height="20px">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt edit" Width="60px"></asp:TextBox>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                    <input id="UpdatedTextBox" runat="server" class="txt" style="width: 100px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    <asp:TextBox ID="tbDateTime" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    文件名称：<span style="color: Red;">*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTitle" runat="server" CssClass="longtxt edit" Width="500"></asp:TextBox>
                                </td>
                                <th>
                                    备注：<span style="color: Red;">*</span>
                                </th>
                                <td>
                                    <asp:CheckBoxList ID="cblRemark" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Value="即用">即用</asp:ListItem>
                                        <asp:ListItem Value="外带使用">外带使用</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主要内容：
                                </th>
                                <td colspan="5">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        Width="700" Height="200">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    附件：
                                </th>
                                <td colspan="5">
                                    <UA:uploadattachments id="UploadAttachments1" runat="server" iscanedit="true" appid="3026" />
                                    说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件！
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    申请部门负责人意见：
                                </th>
                                <td colspan="2">
                                <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    <CS:Countersign ID="Countersign1" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见
                                </th>
                                <td colspan='2'>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    副总经理意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbVP" runat="server" Text="选择相关部门主管副总" Checked="true" />
                                    <asp:Label ID="lbVP" runat="server"></asp:Label><br />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    总经理意见：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbPresident" runat="server" Text="选择总经理" Checked="true"/>
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理部门审核：
                                </th>
                                <td colspan="2">
                                <asp:Label ID="lbZHDeptManager" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    印章管理员盖章：
                                </th>
                                <td colspan="2">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll" style="margin-left: 520px;">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" OnClientClick="return Save_Verification()">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" OnClientClick="return Save_Verification()">提交 </asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbClose" runat="server" OnClientClick="return Close_Win()">关闭</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click">终止</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }

    function Save_Verification() {
        var tb1 = document.getElementById("<% =tbTitle.ClientID%>").value;
        var tb2 = document.getElementById("<% =tbContent.ClientID%>").value;
        var checkobj = document.getElementById("<% =cblRemark.ClientID%>");
        var checks = checkobj.getElementsByTagName("input");

        if (tb1 == null || tb1 == "") {
            alert("文件名称不能为空");
            return false;
        }
        else if (tb2 == null || tb2 == "") {
            alert("主要内容不能为空");
            return false;
        }
        else if (checks[0].checked != true && checks[1].checked != true) {
            alert("请选择“即用”还是“外带使用”");
            return false;
        }
        else {
            return true;
        }
    }
</script>
