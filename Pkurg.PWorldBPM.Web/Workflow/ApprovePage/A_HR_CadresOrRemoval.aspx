<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_HR_CadresOrRemoval.aspx.cs"
    Inherits="Workflow_ApprovePage_A_HR_CadresOrRemoval" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="uc2" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="uc5" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wf_page">
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <div class="wf_center" style="width: 980px;">
            <div class="wf_form">
                <div class="wf_form_title">
                    干部任免
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <fieldset class="wf_fieldset">
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th style="width:50px">
                                编号：
                            </th>
                            <td>
                                <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody id="tbCadre" runat="server">
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟任命人员基本信息(详细信息请见员工履历表)
                                </th>
                            </tr>
                            <tr>
                                <th style="width:50px">
                                    姓名：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbCadresName" runat="server" CssClass="txt" ReadOnly="true" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    现所在公司\部门\现任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbLocationCompanyDeptJob" runat="server" CssClass="heighttxt" ReadOnly="true"
                                        TextMode="MultiLine" Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟任命公司\部门\拟任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbCadresCompanyDeptJob" runat="server" CssClass="heighttxt"  ReadOnly="true"
                                        TextMode="MultiLine" Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    任命原因及拟任职务主管业务范围：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbCadresContent" runat="server" CssClass="heighttxt" ReadOnly="true" TextMode="MultiLine"
                                        Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="tbRemoval" runat="server">
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟免职人员基本信息(详细信息请见员工履历表)
                                </th>
                            </tr>
                            <tr>
                                <th style="width:50px">
                                    姓名：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbRemovalName" runat="server" CssClass="txt" ReadOnly="true" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    现所在公司\部门\现任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbLocationCompanyDeptJobR" runat="server" CssClass="heighttxt" ReadOnly="true"
                                        TextMode="MultiLine" Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟免职公司\部门\拟免职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbRemovalCompanyDeptjob" runat="server" CssClass="heighttxt" ReadOnly="true"
                                        TextMode="MultiLine" Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    免职原因及拟免职务主管业务范围：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="tbRemovalContent" runat="server" CssClass="heighttxt" ReadOnly="true" TextMode="MultiLine"
                                        Width="900" Height="80">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                        <tr>
                            <th style="width:50px">
                                附件：
                            </th>
                            <td colspan="3">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="3013" />
                                说明：最大可上传50M的文件，请不要上传同名文件，否则会覆盖之前的文件，所有文件请先解密再上传！
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                    <tbody>
                            <tr>
                                <th>
                                    人力意见：
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbHRDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionHRDeptManager" CurrentNode="false" CurrentNodeName="人力意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <asp:Label ID="lbDeptManager" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="相关部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan='2'>
                                    <asp:Label ID="lbDirector2" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionDirector1" CurrentNode="false" CurrentNodeName="董事意见1"
                                        runat="server" />
                                        <uc4:ApproveOpinionUC ID="OpinionDirector2" CurrentNode="false" CurrentNodeName="董事意见2"
                                        runat="server" />
                                        <uc4:ApproveOpinionUC ID="OpinionDirector3" CurrentNode="false" CurrentNodeName="董事意见3"
                                        runat="server" />
                                        <uc4:ApproveOpinionUC ID="OpinionDirector4" CurrentNode="false" CurrentNodeName="董事意见4"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <asp:Label ID="lbChairman" runat="server"></asp:Label>
                                    <uc4:ApproveOpinionUC ID="OpinionChairman" CurrentNode="false" CurrentNodeName="董事长意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:Label ID="lblApprovers" runat="server" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hfIsGroup" runat="server" />
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
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <uc5:AddSign ID="AddSign1" runat="server" />
                <uc6:AddSignDeptInner ID="AddSignDeptInner1" runat="server" />
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
        <asp:HiddenField ID="sn" runat="server" />
        <asp:HiddenField ID="nodeID" runat="server" />
        <asp:HiddenField ID="nodeName" runat="server" />
        <asp:HiddenField ID="taskID" runat="server" />
        <asp:HiddenField ID="hf_OpId" runat="server" />
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

</script>
