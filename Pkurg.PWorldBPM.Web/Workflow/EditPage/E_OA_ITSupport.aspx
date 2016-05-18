<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_ITSupport.aspx.cs" Inherits="Workflow_EditPage_E_OA_ITSupport" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<%@ Register Src="../../Modules/Custom/ShowUserNames.ascx" TagName="ShowUserNames"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>发起流程</title>
    <style type="text/css">
        
    </style>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
    <script src="/Resource/js/PreventRepeatSubmit.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
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
                <div class="wf_form_title" id="wf_form_title" runat="server">
                    <%= FormTitle %>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <!--业务表单-->

                            <tr>
                                <th>
                                    所属公司：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbCompany" MaxLength="80" runat="server" CssClass="txt" />
                                </td>
                                <th>
                                    申请人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt"  ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    所属部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDepartName" runat="server"   CssClass="txt"></asp:TextBox>
                                    <%--
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                </td>
                                <th>
                                    申请日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDate" runat="server" CssClass="txt"  ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    邮箱：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEmail" runat="server"  CssClass="txt"></asp:TextBox>
                                </td>
                                <th>
                                    联系电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server"   CssClass="txt"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPhone"
                                        ErrorMessage="*(必填项)" ValidationGroup="su" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr>
                                <th>
                                    常见问题：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlQuestions" runat="server" Width="250px" Height="25px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlQuestions_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    系统名称：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlTypes" runat="server" Width="250px" Height="25px" 
                                        AutoPostBack="True" onselectedindexchanged="ddlTypes_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th style="color:Red">
                                    温馨提示：
                                </th>
                                <td colspan="3" style="color:Red">
                                    请先选择常见问题，若没有符合的常见问题，直接选择系统名称即可，保持常见问题为"--请选择--"的状态。
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    问题描述：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        ValidationGroup="su"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbContent"
                                        ErrorMessage="*(必填项)" ValidationGroup="su"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    关联流程：
                                </th>
                                <td colspan="3">
                                   <FR:FlowRelated ID="flowRelated" runat="server" IsCanEdit="true" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="3">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3014" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table" colspan="1"  style=" height:100px;">
                        <tbody>
                            <tr>
                                <th>
                                    IT顾问处理：
                                </th>
                                <td colspan='2'>
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
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" ValidationGroup="su">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" ValidationGroup="su">提交</asp:LinkButton></li>
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
    //    $(function () {
    //        $("#scroll_button").find("a[href^='javascript:__doPostBack']").each(function () {
    //            // var href = $(this).attr("href");
    //            // alert(href);
    //            //if (href.indexOf("javascript:__doPostBack") > -1) {
    //            //alert($(this).attr("id"));
    //            $(this).click(function () {
    //                if ($(this).hasClass("disable")) {
    //                    alert("系统处理中，请稍后");
    //                    return false;
    //                }
    //                //alert($(this).attr("id"));
    //                $(this).attr("disabled", "disabled");
    //                $("a[disabled]").addClass("disable");
    //                __doPostBack($(this).attr("id"), "");
    //            });
    //            // }
    //        });
    //    });

    function AddOrUpdateItem(stepid) {
        var formid = '<%=FormId %>';
        var procid = '<%= _BPMContext.ProcID %>';
        var sUrl = "../CustomWorkFlow/StepOperation.aspx?id=" + encodeURI(procid) + "&formid=" + encodeURI(formid) + "&stepid=" + encodeURI(stepid) + "&t=" + generateMixed(6);
        var sFeatures = "status:no;scroll:no;dialogWidth:800px;dialogHeight:600px;help:no;center:yes";
        // alert('hahah');
        var arg = showModalDialog(sUrl, "", sFeatures);
        if (arg == 1) {
            //  alert("操作成功！");

            return true;
        }
        else if (arg == 0) {
            alert("操作失败！");

        }
        return false;
    }

    function SelectTemplation() {
        var formid = '<%=FormId %>';
        var sUrl = "../CustomWorkFlow/TemplateOperation.aspx?formid=" + encodeURI(formid) + "&t=" + generateMixed(6);
        var sFeatures = "status:no;scroll:no;dialogWidth:700px;dialogHeight:500px;help:no;center:yes";
        // alert('hahah');
        var arg = showModalDialog(sUrl, "", sFeatures);
        if (arg == 1) {
            //  alert("操作成功！");

            return true;
        }
        else if (arg == 0) {
            alert("加载模板失败！");

        }
        return false;
    }
    function SaveTemplation() {
        var formid = '<%=FormId %>';
        var sUrl = "../CustomWorkFlow/SaveTemplation.aspx?formid=" + encodeURI(formid) + "&t=" + generateMixed(6);
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:300px;help:no;center:yes";
        // alert('hahah');
        var arg = showModalDialog(sUrl, "", sFeatures);
        if (arg == 1) {
            alert("模板保存成功！");

            return false;
        }
        else if (arg == 0) {
            alert("模板保存失败！");

        }
        return false;
    }
    var chars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];

    function generateMixed(n) {
        var res = "";
        for (var i = 0; i < n; i++) {
            var id = Math.ceil(Math.random() * 35);
            res += chars[id];
        }
        return res;
    }
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
