<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_InstructionOfPKURG.aspx.cs" Inherits="Workflow_EditPage_E_InstructionOfPKURG" %>


<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register src="../../Modules/FlowRelated/FlowRelated.ascx" tagname="FlowRelated" tagprefix="uc2" %>


<%@ Register src="../../Modules/Countersign/Countersign.ascx" tagname="Countersign" tagprefix="uc3" %>


<%@ Register src="../../UserControls/ApproveOpinionUC.ascx" tagname="ApproveOpinionUC" tagprefix="uc4" %>


<%@ Register src="../../Modules/UploadAttachments/UploadAttachments.ascx" tagname="UploadAttachments" tagprefix="uc5" %>


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
            selectOneCheckList("cblUrgentLevel");
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
        <div class="wf_center">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    请示单
                </div>
                <table class="wf_table" cellspacing="1" cellpadding="0" style="border-collapse: separate;">
                    <tbody>
                        <tr>
                            <th style="width: 90px;">
                                保密等级：
                            </th>
                            <td  >
                                <asp:CheckBoxList ID="cblSecurityLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="0">绝密</asp:ListItem>
                                    <asp:ListItem Value="1">机密</asp:ListItem>
                                    <asp:ListItem Value="2">秘密</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <th  style="width: 90px;">
                                紧急程度：
                            </th>
                            <td  >
                                <asp:CheckBoxList ID="cblUrgentLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Value="0">加急</asp:ListItem>
                                <asp:ListItem Value="1">紧急</asp:ListItem>
                                <asp:ListItem Value="2">一般</asp:ListItem>
                            </asp:CheckBoxList>
                            </td>
                            <th  style="width: 70px;">
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbNumber" runat="server" CssClass="txt" Width="130" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
                                <input id="UpdatedTextBox" runat="server" class="txt" style="width: 250px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                    <asp:TextBox ID="tbData" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                  
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTheme" runat="server" CssClass="longtxt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    呈报内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                    
                                    <uc2:FlowRelated ID="FlowRelated1" runat="server"  IsCanEdit="true" />
                                    
                                </td>
                            </tr>
                             <tr>
                              <th>
                                    上传附件：
                                </th>
                                <td colspan="3">

                                    <uc5:UploadAttachments ID="UploadAttachments1" runat="server"   
                                        IsCanEdit="true" AppId="1002" />

                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    部门负责人意见：
                                </th>
                                <td style="width: 160px">
                                    <asp:CheckBox ID="chkDeptManager" runat="server" Checked="true"></asp:CheckBox>
                                    选择<asp:label ID="Label1" runat="server" Text=" 部门负责人 " style="font-weight:bold;"></asp:label>
                                    <asp:label ID="lbDeptmanager" runat="server"></asp:label>
                                </td>
                                <td style="width: 340px">

                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCDeptleader" CurrentNode="false" CurrentNodeName="部门负责人审批" runat="server" />

                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门会签：
                                </th>
                                <td colspan="2">
                                    
                                    <uc3:Countersign ID="Countersign1" runat="server"  IsCanEdit="true"/>
                                    
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan="2">
                                    
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCRealateDept" CurrentNode="false" CurrentNodeName="会签" runat="server" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    分管领导意见：
                                </th>
                                <td colspan="2">
                                    
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCLeader" CurrentNode="false" CurrentNodeName="部门主管领导审批"  runat="server" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    CEO意见：
                                </th>
                                <td colspan="2">
                                   
                                    <uc4:ApproveOpinionUC ID="ApproveOpinionUCCEO" CurrentNode="false" CurrentNodeName="CEO审批" runat="server" />
                                   
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    是否发起新流程：
                                </th>
                                <td colspan="2">
                                    <asp:CheckBox ID="cbIsReport" runat="server" ></asp:CheckBox>
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
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li>
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" OnClientClick="return Save_Verification()">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" onclick="Submit_Click">提交</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbArchive" runat="server" onclick="Archive_Click" >归档</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbClose" runat="server" OnClientClick="return Close_Win()">关闭</asp:LinkButton></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Save_Verification() {
        var pw1 = document.getElementById("<% =tbTheme.ClientID%>").value;
        if (pw1 != null && pw1 != "") {
            return true;
        }
        else {
            alert("标题不能为空");
            return false;
        }
    }

    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
