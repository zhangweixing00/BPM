<%@ Page Language="C#" AutoEventWireup="true" CodeFile="E_OA_CustomWorkflow.aspx.cs"
    Inherits="Workflow_EditPage_E_OA_CustomWorkflow" %>

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
                     <asp:label ID="lbFormTitle" runat="server"></asp:label>
                </div>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table" cellspacing="1" cellpadding="0">
                        <tbody>
                            <!--业务表单-->
                            <tr>
                                <th>
                                    经办部门：
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddlDepartName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartName_SelectedIndexChanged"
                                    Width="300px" Height="20px">
                                    </asp:DropDownList>
                                </td>
                                <th>
                                    日期：
                                </th>
                                <td>
<%--                                    <input id="UpdatedTextBox" runat="server" class="txt" style="width: 250px" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />--%>
                                    <asp:TextBox ID="tbDate" runat="server" CssClass="txt edit" Width="100px"></asp:TextBox>
                                    <asp:label ID="lbDate" runat="server" Width="100px"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    经办人：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPerson" runat="server" CssClass="txt edit"  Width="100px"></asp:TextBox>
                                </td>
                                <th>
                                    电话：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPhone" runat="server" CssClass="txt edit"  Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    主题：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbTheme" runat="server" CssClass="longtxt edit" ValidationGroup="su"
                                        Width="700"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="tbTheme" ValidationGroup="su"></asp:RequiredFieldValidator>
                                </td>
                                
                            </tr>
                            <tr>
                                <th>
                                    呈报内容：
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="tbContent" runat="server" CssClass="heighttxt edit" TextMode="MultiLine"
                                        ValidationGroup="su" Width="700"  Height="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbContent"
                                        ErrorMessage="*" ValidationGroup="su"></asp:RequiredFieldValidator>
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
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3011" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <div style="float: right;">
                        <asp:LinkButton ID="lbtnAddStep" runat="server" CausesValidation="false" Text="添加步骤"
                            OnClientClick='return AddOrUpdateItem(-1);' OnClick="lbtnAddStep_Click"></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lbtnSaveStepToTemp" runat="server" OnClientClick="return SaveTemplation();"
                            CausesValidation="false" Text="保存模板" ></asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lbtnSelectTemplation" runat="server" OnClientClick="return SelectTemplation();"
                            CausesValidation="false" Text="选择模板" onclick="lbtnSelectTemplation_Click"></asp:LinkButton>
                        &nbsp;</div>
                        <div id="Div_NoStepsTip" runat="server" style="width:100%; border-color:#eaeaea; text-align:center; height:50px; margin-top:50px; clear:both; font-weight:bold;">请在流程发起前设置流程步骤！流程步骤名称不能相同！
                        </div>
                        <div id="Div_des" runat="server" visible="false" style="width:100%;text-align:left; border-color:#eaeaea;  height:20px; margin-top:20px; clear:both; font-weight:bold;">
                        </div>                        
                    <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                        <HeaderTemplate>
                            <table class="List">
                                <tr>
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        步骤名
                                    </th>
                                    <th>
                                        审批人员
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                                onmouseout="this.style.backgroundColor=c;">
                                <td>
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td>
                                    <%#Eval("StepName")%>
                                </td>
                                <td>
                                    <uc1:ShowUserNames ID="ShowUserNames1" runat="server" UserList='<%#Eval("PartUsers")%>'
                                        IsShowDelete="false" />
                                </td>
                                <td>
                                    <!--整体解析-->
                                    <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick='<%#"return AddOrUpdateItem("+ Eval("StepID").ToString()+")" %>'
                                        CausesValidation="false" Text='编辑' CommandName='EDIT'></asp:LinkButton>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("StepID") %>'
                                        OnClientClick="return confirm('确定要删除吗?');" CausesValidation="false" Text="删除"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
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
                    <asp:LinkButton ID="lbSave" runat="server" OnClick="Save_Click" 
                        ValidationGroup="su">保存</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click" 
                        ValidationGroup="su">提交</asp:LinkButton></li>
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
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
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
