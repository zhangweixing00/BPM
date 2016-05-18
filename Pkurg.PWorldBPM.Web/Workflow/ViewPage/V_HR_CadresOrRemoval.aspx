<%@ Page Language="C#" AutoEventWireup="true" CodeFile="V_HR_CadresOrRemoval.aspx.cs"
    Inherits="Workflow_ViewPage_V_HR_CadresOrRemoval" %>

<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/ApproveOpinionUC.ascx" TagName="ApproveOpinionUC"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看流程</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/helper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">查看流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    干部任免
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                    <table class="wf_table" cellpadding="0" cellspacing="1">
                        <tr>
                            <th style="width:50px">
                                编号：
                            </th>
                            <td>
                                <asp:label ID="tbReportCode" runat="server" />
                            </td>
                        </tr>
                    </table>
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
                                    <asp:label ID="tbCadresName" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    现所在公司\部门\现任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbLocationCompanyDeptJob" runat="server" ></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟任命公司\部门\拟任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbCadresCompanyDeptJob" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    任命原因及拟任职务主管业务范围：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbCadresContent" runat="server"></asp:label>
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
                                    <asp:label ID="tbRemovalName" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    现所在公司\部门\现任职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbLocationCompanyDeptJobR" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    拟免职公司\部门\拟免职务：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbRemovalCompanyDeptjob" runat="server"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th colspan="2" style="text-align:left;width:300px; padding-left:15px">
                                    免职原因及拟免职务主管业务范围：
                                </th>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:label ID="tbRemovalContent" runat="server"></asp:label>
                                </td>
                            </tr>
                        </tbody>
                        <tr>
                            <th style="width:50px">
                                附件：
                            </th>
                            <td colspan="3">
                                <uc1:UploadAttachments ID="UploadAttachments1" runat="server" IsCanEdit="false" AppId="3013" IsOnlyRead="true" />
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
                                    <uc4:ApproveOpinionUC ID="OpinionHRDeptManager" CurrentNode="false" CurrentNodeName="人力意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    相关部门意见：
                                </th>
                                <td colspan='2'>
                                    <uc4:ApproveOpinionUC ID="OpinionDeptManager" CurrentNode="false" CurrentNodeName="相关部门意见"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan='2'>
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
                                    <uc4:ApproveOpinionUC ID="OpinionChairman" CurrentNode="false" CurrentNodeName="董事长意见"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="sn" runat="server" />
        <asp:HiddenField ID="nodeID" runat="server" />
        <asp:HiddenField ID="nodeName" runat="server" />
        <asp:HiddenField ID="taskID" runat="server" />
        <asp:HiddenField ID="hf_OpId" runat="server" />
    </form>
</body>
</html>
