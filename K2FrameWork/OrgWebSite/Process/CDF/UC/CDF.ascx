<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CDF.ascx.cs" Inherits="OrgWebSite.Process.CDF.UC.CDF" %>
<style type="text/css">
    .datalist11
    {
        border-style: none;
        border-color: inherit;
        border-width: 0;
        width: 769px;
    }
    .nodeOuter
    {
        border: 1px solid #ffd600;
        margin-top: 12px;
        font-size: 12px;
        width: 152px;
    }
    .unapprovenode
    {
        border-bottom: 1px solid #f1d974;
    }
    .approvenode
    {
        border-bottom: 1px solid #f1d974;
    }
    .currentnode
    {
        border-bottom: 1px solid #f1d974;
    }
    .inputreadonly
    {
        border: 0;
        border-bottom: 1px solid #999999;
        width: 150px;
        text-align: left;
    }
    .FloatRight
    {
        float: right;
    }
</style>
<style type="text/css">
    /*文本框只读*/
    
    .conter_right_list_nav ul li.conter_right_list_input_bg_border
    {
        border-bottom: 1px solid #333333;
        width: 177px;
        text-align: left;
        vertical-align: middle;
        height: 22px;
    }
    .conter_right_list_nav ul li.conter_right_list_input_bg_border_red
    {
        border: 1px solid red;
        width: 177px;
        text-align: left;
        vertical-align: middle;
        height: 22px;
    }
    
    .conter_right_list_nav ul li.conter_right_list_input_bg_textare_border
    {
        height: 55px;
        border: #999999 none 1px;
        vertical-align: middle;
    }
    
    .inputreadonly
    {
        border: 0px none #9a9a9a;
        vertical-align: top;
        color: #333333;
        overflow: hidden;
        padding-top: 4px;
    }
    select
    {
        width: 100%;
        border: 0;
    }
    
    input[type="text"]
    {
        width: 170px;
    }
    .mustinput
    {
        color: Red;
    }
    
    .conter_right
    {
        float: left;
        width: 798px;
        height: 100%;
        min-height: 580px;
    }
    
    textarea
    {
        font-size: 12px;
        font-weight: normal;
    }
    
    .FaultClass
    {
        background-color: Red;
    }
    
    .inputbackimg
    {
        background: url(../../../pic/menu1.png) no-repeat right;
    }
    .style1
    {
        color: #FF0000;
    }
</style>
<script type="text/javascript">
    //显示表单页
    function DisplayForm() {
        var strUrl = window.location.href;
        var arrUrl = strUrl.split("/");
        var strPage = arrUrl[arrUrl.length - 1];
        if (strPage.indexOf('Approve.aspx') >= 0) {
            document.getElementById("btnApprovePrevious").style.display = "none";
        }
        else if (strPage.indexOf('StartProcess') >= 0 || strPage.indexOf('Draft.aspx') >= 0 || strPage.indexOf('ReSubmit.aspx') >= 0) {
            document.getElementById("btnPrevious").style.display = "none";
        }
        resetDiv();
        $('#CDF1_InputPersonDiv').css('display', 'inline');
        document.getElementById('<%=hfIsForm.ClientID %>').value = 'Form';

        $('#divActionButtons').css('width', '600px');
    }

    //显示流程指引页面
    function DisplayDirectFlow() {
        $('#divActionButtons').css('width', '510px');
        var values = document.getElementById('<%=hfIsForm.ClientID %>').value;
        var strUrl = window.location.href;
        var arrUrl = strUrl.split("/");
        var strPage = arrUrl[arrUrl.length - 1];
        if (strPage.indexOf('Approve.aspx') >= 0) {
            document.getElementById("btnApprovePrevious").style.display = "inline";
        }
        else if (strPage.indexOf('StartProcess') >= 0 || strPage.indexOf('Draft.aspx') >= 0 || strPage.indexOf('ReSubmit.aspx') >= 0) {
            document.getElementById("btnPrevious").style.display = "inline";
        }
        resetDiv();
        $('#dvFlowChart_OUT').css('display', 'inline');
        document.getElementById('<%=hfIsForm.ClientID %>').value = 'FlowChart';
        window.parent.scroll(0, 0);
        if (values == 'Form') {
            return false;
        }
        else {
            return true;
        }
    }

    //重置
    function resetDiv() {
        $('#CDF1_InputPersonDiv').css('display', 'none');
        $('#dvFlowChart_OUT').css('display', 'none');
    }

    //回发跳转Div
    $(document).ready(function () {
        if (document.getElementById('<%=hfIsForm.ClientID %>').value == 'Form') {
            DisplayForm();
        }
        else if (document.getElementById('<%=hfIsForm.ClientID %>').value == 'FlowChart') {
            DisplayDirectFlow();
        }

        //判断是否显示详细列表
//        if ($('#CDF1_hfHiddenDetailFlag').val() == '0')
//            $('#dv_detailInfo').css('display', 'none');
//        else
//            $('#dv_detailInfo').css('display', '');

        $('#CDF1_trGvFiles').height($('#CDF1_gvFiles').height() + 10);      //设置模板列高

        $('#ul_attach').height($('#CDF1_Upload1_gvAttachList').height() + 50);   //设置附件列高

        if (document.getElementById('CDF1_hfReturnVal').value != '') {
            var returnVal = document.getElementById('CDF1_hfReturnVal').value;
            document.getElementById('CDF1_hfEmployeeCode').value = returnVal.split(';')[1];
            document.getElementById('CDF1_FirstDeptName').value = returnVal.split(';')[5];
            document.getElementById('CDF1_SecondDeptCode').value = returnVal.split(';')[7];
            document.getElementById('CDF1_hfApplicantCode').value = returnVal.split(';')[1];

            document.getElementById('CDF1_ApplicantName').value = returnVal.split(';')[0] + "(" + returnVal.split(';')[1] + ")";
            document.getElementById('CDF1_EmployeeCode').value = returnVal.split(';')[1];
            document.getElementById('CDF1_DeptName').value = returnVal.split(';')[3];
            document.getElementById('CDF1_CompanyName').value = returnVal.split(';')[9];
            document.getElementById('CDF1_Email').value = returnVal.split(';')[2];
            document.getElementById('CDF1_Tel').value = returnVal.split(';')[13];
        }

        //隐藏表头
        var attachList = document.getElementById('CDF1_Upload1_gvAttachList');
        if (attachList != null) {
            if (attachList.rows.length == 1) {
                document.getElementById('CDF1_Upload1_ibtnDel').disabled = true;    //灰掉
                for (var i = 0; i < attachList.rows[0].cells.length; i++) {
                    attachList.rows[0].cells[i].style.display = "none";
                }
            }
        }
    });


    function print() {
        ymPrompt.win({ message: '../Process/print/print.aspx?TemplateID=CDF&PARAM=<%=FormId.Value %>', handler: function () {
            ymPrompt.close();
        }, width: 800, height: 620, title: '打印页面', iframe: true
        });
    }

    //选择申请人
    function SelectEmployee() {
        var para = "?checkstyle=true";
        ymPrompt.win({ message: '../Search/K2EmployeeCheck/K2EmployeeCheck.aspx' + para, handler: this.callBack_1, width: 760, height: 470, title: '选择人员', iframe: true });
    }

    function callBack_1(returnVal) {
        if (returnVal != "close") {
            if (returnVal && returnVal.length != 0) {
                document.getElementById('CDF1_hfEmployeeCode').value = returnVal[0].split(';')[1];
                document.getElementById('CDF1_FirstDeptName').value = returnVal[0].split(';')[5];
                document.getElementById('CDF1_SecondDeptCode').value = returnVal[0].split(';')[7];
                document.getElementById('CDF1_hfReturnVal').value = returnVal[0];
                document.getElementById('CDF1_hfApplicantCode').value = returnVal[0].split(';')[1];

                document.getElementById('CDF1_ApplicantName').value = returnVal[0].split(';')[0] + "(" + returnVal[0].split(';')[1] + ")";
                document.getElementById('CDF1_EmployeeCode').value = returnVal[0].split(';')[1];
                document.getElementById('CDF1_DeptName').value = returnVal[0].split(';')[3];
                document.getElementById('CDF1_CompanyName').value = returnVal[0].split(';')[9];
                document.getElementById('CDF1_Email').value = returnVal[0].split(';')[2];
                document.getElementById('CDF1_Tel').value = returnVal[0].split(';')[13];
            }
        }
        ymPrompt.close();
    }
</script>
<script type="text/javascript">
    function setElementStatus(currentObj, wfpos) {
        if (chart.getNode(wfpos).isOpen) {
            $(currentObj).attr('src', '../../pic/icon_arrowup.png');
            for (var i = 0; i < $(currentObj).parent().parent().parent().parent().parent().next().children().children().length; i++) {
                $($(currentObj).parent().parent().parent().parent().parent().next().children().children()[i]).css('display', 'none');
            }
            chart.setOpen(wfpos, false);
            chart.paint();                  //绘制流程图
        }
        else if (!chart.getNode(wfpos).isOpen) {
            $(currentObj).attr('src', '../../pic/icon_arrowdown.png');
            for (var i = 0; i < $(currentObj).parent().parent().parent().parent().parent().next().children().children().length; i++) {
                $($(currentObj).parent().parent().parent().parent().parent().next().children().children()[i]).css('display', 'inline');
            }
            chart.setOpen(wfpos, true);
            chart.paint();                  //绘制流程图
        }
    }

//    function ZK() {
//        if ($('#CDF1_hfHiddenDetailFlag').val() == '0') {
//            $('#CDF1_hfHiddenDetailFlag').val('1');
//        } else {
//            $('#CDF1_hfHiddenDetailFlag').val('0');
//        }
//        if ($('#CDF1_hfHiddenDetailFlag').val() == '0') {
//            $('#dv_detailInfo').css('display', 'none');
//        }
//        else {
//            $('#dv_detailInfo').css('display', '');
//        }
//    }
</script>
<div style="float: left; margin-bottom: 30px;">
    <div style="height: 20px;">
        &nbsp;</div>
    <div class="conter_right_title" style="width: 780px;">
        <div class="conter_right_title_text" style="color: #776408; width: 100px; font-size: 13px;
            font-weight: bold;">
            POC流程</div>
        <div class="conter_right_title_text">
            表单号：<asp:Label ID="lbl_formid" runat="server"></asp:Label></div>
        <div class="conter_right_title_text">
            申请日期：<asp:Label ID="lbl_date" runat="server"></asp:Label></div>
        <div class="conter_right_title_text">
            填写人：<asp:Label ID="EmployeeName" runat="server"></asp:Label>
        </div>
    </div>
    <div id="dvForm" style="width: 780px;">
        <div class=" conter_right_list_main" id="InputPersonDiv" runat="server">
            <div class="conter_right_list_nav">
                <span>基本信息</span>
                <div id="information" style="float: right; width: 200px; padding-right: 30px; padding-top: 2px;
                    color: #333333; text-align: right; font-weight: normal">
                    注:&nbsp;<a class="style1">*</a> &nbsp;为必填项</div>
                <ul class="right_top" style="width: 600px;">
                    <%--<li class="mustinput" style="line-height: 25px;">业务大类：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="BBCategory" runat="server" CssClass="select" AutoPostBack="True">
                        </asp:DropDownList>
                        <input id="lbBBCategory" runat="server" style="border: none;" class="inputreadonly"
                            readonly="readonly" />
                    </li>
                    <li class="mustinput" style="line-height: 25px;">业务小类：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="BSCategory" runat="server" CssClass="select" AutoPostBack="True">
                        </asp:DropDownList>
                        <input id="lbBSCategory" runat="server" style="border: none;" class="inputreadonly"
                            readonly="readonly" />
                    </li>--%>
                    <li class="mustinput" style="line-height: 25px;">流程选择：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="ddlProcessType" runat="server" CssClass="select" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlProcessType_SelectedIndexChanged">
                        </asp:DropDownList>
                        <input id="lbProcessType" runat="server" style="border: none;" class="inputreadonly"
                            readonly="readonly" />
                    </li>
                    <li class="mustinput" style="line-height: 25px;">部门选择：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="select">
                        </asp:DropDownList>
                        <input id="lbDepartment" runat="server" style="border: none;" class="inputreadonly"
                            readonly="readonly" />
                    </li>
                </ul>
                <ul class="right_top" style="width: 600px;">
                    <li>申请人：</li>
                    <li class="conter_right_list_input_bg_border" style="line-height: 20px;">
                        <input type="text" id="ApplicantName" class="inputreadonly" readonly="readonly" runat="server"
                            style="border: none; width: 105px;" />
                    </li>
                    <li class="conter_right_list_input_border" style="margin-left: -130px; line-height: 20px;">
                        <asp:Label ID="EmployeeCode" runat="server" Style="margin-top: 0px; display: none;"
                            readonly="readonly"></asp:Label>
                    </li>
                </ul>
                <div id="mDataAdd" class="leftInfodiv">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <span id="upspan">
                                    <label onclick="$('#dv_detailInfo').slideToggle('slow');if($('#CDF1_hfHiddenDetailFlag').val() == '0') $('#CDF1_hfHiddenDetailFlag').val('1');else $('#CDF1_hfHiddenDetailFlag').val('0');"
                                        style="display: none;">
                                        详细信息</label></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dv_detailInfo">
                    <ul id="ulsign" runat="server" style="width: 600px;">
                        <li>选择会签人：</li>
                        <li class="conter_right_list_input_bg_border" style="line-height: 20px;">
                            <%--<input id="CompanyName" runat="server" class="inputreadonly" readonly="readonly"
                                style="border: none;" />--%>
                                <asp:CheckBoxList ID="cblSigners" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="contoso\administrator">张三</asp:ListItem>
                                <asp:ListItem Value="contoso\administrator">李四</asp:ListItem>
                                <asp:ListItem Value="contoso\administrator">王五</asp:ListItem>
                            </asp:CheckBoxList>
                        </li>
                        <%--<li></li>
                        <li class="conter_right_list_input_bg_border" style="line-height: 20px;">
                            <input id="DeptName" runat="server" class="inputreadonly" readonly="readonly" style="border: none;" />
                        </li>--%>
                    </ul>
                    <%--<ul style="width: 600px;">
                        <li>分机：</li>
                        <li class="conter_right_list_input_bg_border" style="line-height: 20px;">
                            <input id="Tel" runat="server" class="inputreadonly" readonly="readonly" style="border: none;" />
                        </li>
                        <li>电子邮件：</li>
                        <li class="conter_right_list_input_bg_border" style="line-height: 20px;">
                            <input id="Email" runat="server" class="inputreadonly" readonly="readonly" style="border: none;" />
                        </li>
                    </ul>--%>
                </div>
            </div>
            <asp:HiddenField ID="hfIsForm" runat="server" Value="Form" />
            <asp:HiddenField ID="State" runat="server" />
            <asp:HiddenField ID="FormId" runat="server" />
            <asp:HiddenField ID="SubmitID" runat="server" />
            <asp:HiddenField ID="SubmitName" runat="server" />
            <asp:HiddenField ID="CreatedBy" runat="server" />
            <asp:HiddenField ID="hfReturnVal" runat="server" />
            <asp:HiddenField ID="hfNeedVerification" runat="server" Value="true" />
            <asp:HiddenField ID="hfHiddenDetailFlag" runat="server" Value="0" />
            <asp:HiddenField ID="hfjqFlowChart" runat="server" />
            <div class="conter_right_list_nav">
                <span>申请单信息</span>
                <ul class="right_top" style="display: none;">
                    <li class="mustinput">紧急程度：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="Priority" runat="server" CssClass="select">
                            <asp:ListItem Value="一般">一般</asp:ListItem>
                            <asp:ListItem Value="紧急">紧急</asp:ListItem>
                        </asp:DropDownList>
                        <input id="lbPriority" runat="server" class="inputreadonly" style="border: none;"
                            readonly="readonly" />
                    </li>
                    <li class="mustinput">密级：</li>
                    <li class="conter_right_list_input_bg" style="line-height: 18px;">
                        <asp:DropDownList ID="Urgent" runat="server" CssClass="select">
                            <asp:ListItem Value="一般">一般</asp:ListItem>
                            <asp:ListItem Value="秘密">秘密</asp:ListItem>
                            <asp:ListItem Value="机密">机密</asp:ListItem>
                            <asp:ListItem Value="绝密">绝密</asp:ListItem>
                        </asp:DropDownList>
                        <input id="lbUrgent" runat="server" class="inputreadonly" style="border: none;" readonly="readonly" />
                    </li>
                </ul>
                <ul class="right_top">
                    <li>通知方式：</li>
                    <li>
                        <%--<asp:CheckBoxList ID="Notification" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">邮件</asp:ListItem>
                            <asp:ListItem Value="1">短信</asp:ListItem>
                        </asp:CheckBoxList>--%>
                        <table style="width: 150px;">
                            <tr>
                                <td style="width: 17px; line-height: 21px;">
                                    <asp:CheckBox ID="cbIsEmail" runat="server" Checked="true" Style="text-align: left;
                                        margin: 0px;" />
                                </td>
                                <td style="text-align: left; line-height: 21px;">
                                    邮件
                                </td>
                                <td style="width: 17px; line-height: 21px;">
                                    <asp:CheckBox ID="cbIsSMS" runat="server" Style="text-align: left; margin: 0px;" />
                                </td>
                                <td style="text-align: left; line-height: 21px;">
                                    短信
                                </td>
                            </tr>
                        </table>
                    </li>
                </ul>
                <ul style="height: 350px;">
                    <li><span style="padding-left: 18px; padding-top: 50px;">申请原因：</span> <span class="mustinput"
                        style="padding-left: 14px; margin-top: -5px;">（必填项）</span></li>
                    <%--<li class="conter_right_list_input_bg_textare" style="height: 460px;">--%>
                    <li>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="AppReason" runat="server" TextMode="MultiLine" MaxLength="500" Height="350px"
                                        Width="457px" regex="mustinput:必须填写申请原因" Style="overflow-y: scroll;"></asp:TextBox>
                                </td>
                                <td>
                                    <label class="mustinput">
                                        *</label>
                                </td>
                            </tr>
                        </table>
                    </li>
                    <%--</li>--%>
                </ul>
            </div>
            <%--<div class="">
                <span>&nbsp;&nbsp;&nbsp;</span>
                <ul id="ul_attach">
                    <li>
                        <uc1:Upload ID="Upload1" runat="server" TableName="CustomFlow" Width="540" WorkFlowCode="301595d9-19f6-41a0-9069-21d517626c27"
                            IsConfirm="False" />
                    </li>
                </ul>
            </div>--%>
        </div>
        <div id="dvHelper" class="conter_right_menu_main">
            <div class="conter_right_menu_list">
                <span>流程帮助：</span>
                <ul>
                    <asp:Literal ID="ltlHelp" runat="server"></asp:Literal>
                </ul>
            </div>
            <div class="conter_right_menu_list" id="flowDoc" runat="server">
                <span>业务模板下载：</span>
                <ul id="trGvFiles" runat="server">
                    <li style="width: 140px;">
                        <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" Width="100%"
                            ShowHeader="false" BorderWidth="0">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <%# Container.DisplayIndex+1 %>、
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbDownload" runat="server" Text='<%#Eval("DocName") %>' CommandArgument='<%# ToBase64((string)Eval("AttachXML")) %>'
                                                        CommandName="DownLoad"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:ImageButton ID="ibtnDownload" runat="server" ImageUrl="~/pic/btnImg/btnDownload_nor.png"
                                                        CommandArgument='<%# ToBase64((string)Eval("AttachXML")) %>' CommandName="DownLoad" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div style="text-align: center; margin: 0 auto;">
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="None" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </li>
                </ul>
            </div>
        </div>
        <div id="dvFlowChart_OUT" style="display: none; float: left;">
            <table cellspacing="0" cellpadding="0" style="width: 600px;">
                <tr class="top_1" style="height: 20px;">
                    <td colspan="3" height="20px">
                    </td>
                </tr>
                <%--                <tr class="top_2">
                    <td colspan="3" align="right">
                        <input id="btnAddNode" runat="server" type="image" src="~/pic/btnImg/btnAddone_nor.png"
                            value="添加" onclick="AddNode(); return false;" />
                        <input id="btnDel" runat="server" type="image" src="~/pic/btnImg/btnDelete_nor.png"
                            value="删除" onclick="ConfirmDelNode(); return false;" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/kaishi.png" alt="开始" style="padding-right: 30px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/arrow.png" alt="" style="padding-right: 30px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <div id="dvFlowchart" style="width: 100%; height: 100%; margin: 0 auto; height: auto!important;">
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" valign="top">
                        <img src="../../../pic/jieshu.png" alt="结束" style="padding-right: 30px;" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="bottom">
                    <td colspan="3">
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfProcessSN" runat="server" />
            <asp:HiddenField ID="hfNoCounter" runat="server" />
            <asp:HiddenField ID="hfRoleName" runat="server" />
            <asp:HiddenField ID="hfRoleCode" runat="server" />
            <asp:HiddenField ID="hfTemplateCounts" runat="server" />
            <asp:HiddenField ID="hfProcessID" runat="server" />
            <asp:HiddenField ID="hfSign" runat="server" />
        </div>
    </div>
</div>
