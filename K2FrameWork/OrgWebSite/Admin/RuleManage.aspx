        <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleManage.aspx.cs" Inherits="OrgWebSite.Admin.RuleManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>规则管理</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Javascript/DIVLayer/ymPrompt.js"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
    <script language="javascript" type="text/javascript">
        function SelectConfiguration() {
            //取得流程ID以及分组名称
            var ddlProc = document.getElementById('<%=ddlProcessType.ClientID %>')
            var ddlGroupName = document.getElementById('<%=ddlGroup.ClientID %>');
            var procID = ddlProc.options[ddlProc.selectedIndex].value;
            var groupName = ddlGroupName.options[ddlGroupName.selectedIndex].text;
            var formula = document.getElementById('<%=txtExpression.ClientID %>').value;
            var para = "?ProcessID=" + procID + "&GroupName=" + groupName + "&Formula=" + formula;
            top.window.ymPrompt.win('../Admin/Popup/ConfigurationRuleResult.aspx' + para, 760, 560, "配置结果", TrueInfos, null, null, true);
        }
        function TrueInfos(retValue) {
            switch (retValue) {
                case "close": ymPrompt.close(); break;
                case "cancel": ymPrompt.close(); break;
            }
        }

        //插入操作
        function InsertValue(Op) {
            $('#hfValidaterResult').val('');
            if (Op == 'orderno') {
                //插入选择的序号
                $('#txtExpression').append($('#ddlSelectID').val());
            }
            else {
                $('#txtExpression').append(Op);
            }
        }

        //验证
        function ValidateValue() {
            if ($('#txtExpression').val() == '') {
                top.window.ymPrompt.alert('规则表不能为空');
                return;
            }
            var filterCount = 1;        //标识规则表最大OrderNo
            var filterCond = $('#txtExpression').val();
            var tmpStr = filterCond;
            var num = tmpStr.replace(/\D/g, ";");
            var arr = num.split(';');   //取得字符串中的数字

            //获取最大值
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] > filterCount)
                    filterCount = arr[i];
            }
            ++filterCount;
            $.ajax({
                url: 'Validate.ashx?Op=Validate&FilterCount=' + filterCount + '&FilterCond=' + filterCond,
                success: function (data) {
                    if (data == 'true') {
                        $('#hfValidaterResult').val('ok');
                        top.window.ymPrompt.alert('验证成功');
                    }
                    else {
                        $('#hfValidaterResult').val('');
                        top.window.ymPrompt.alert(data);
                    }
                }
            });
        }

        //保存验证
        function SaveValidate() {
            if ($('#txtExpression').val() == '') {
                top.window.ymPrompt.alert('规则表不能为空');
                return false;
            }
            else {
                if ($('#hfValidaterResult').val() == 'ok') {
                    $('#hfValidaterResult').val('');
                    return true;
                }
                else {
                    $('#hfValidaterResult').val('');
                    top.window.ymPrompt.alert('请点击验证规则表');
                    return false;
                }
            }
        }

        //预览验证
        function ReViewValidate() {
            if ($('#txtExpression').val() == '') {
                top.window.ymPrompt.alert('规则表不能为空');
                return;
            }
            var filterCount = 1;        //标识规则表最大OrderNo
            var filterCond = $('#txtExpression').val();
            var tmpStr = filterCond;
            var num = tmpStr.replace(/\D/g, ";");
            var arr = num.split(';');   //取得字符串中的数字

            //获取最大值
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] > filterCount)
                    filterCount = arr[i];
            }
            ++filterCount;
            $.ajax({
                url: 'Validate.ashx?Op=Validate&FilterCount=' + filterCount + '&FilterCond=' + filterCond,
                success: function (data) {
                    if (data == 'true') {
                        $('#hfValidaterResult').val('ok');
                        SelectConfiguration();
                    }
                    else {
                        $('#hfValidaterResult').val('');
                        top.window.ymPrompt.alert(data);
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="nav">
            <p>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
    </div>
    <div class="nav_1">
        <p>
            流程选择</p>
    </div>
    <div class="pro" style="margin: 10px 0; height: 60px;">
        <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
            <tr>
                <td style="width: 80px; text-align: right;">
                    公司名称：
                </td>
                <td style="width: 220px;">
                    <asp:DropDownList ID="ddlProcessType" runat="server" DataTextField="ProcessType"
                        DataValueField="ID" Width="200" AutoPostBack="True" OnSelectedIndexChanged="ddlProcessType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 80px; text-align: right;">
                    流程名称：
                </td>
                <td style="width: 220px;">
                    <asp:DropDownList ID="ddlGroup" runat="server" Width="200" DataTextField="ProcessType"
                        DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div_peolistName" class="nav_1">
        <p>
            列表</p>
    </div>
    <div class="pro_1" style="padding-bottom: 30px;">
        <table style="width: 765px;">
            <tr>
                <td>
                    <asp:GridView ID="gvRuleList" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="765px" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无节点数据</div>"
                        BorderWidth="1px" AllowPaging="false" CssClass="datalist2">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="规则名称" DataField="RuleTableName" />
                            <asp:BoundField HeaderText="绑定存储过程名称" DataField="RequestSPName" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div1" class="nav_1">
        <p>
            规则表达式</p>
    </div>
    <div class="pro_1">
        <table>
            <tr>
                <td style="width: 60px;">
                    <span>条件过滤：</span>
                </td>
                <td style="width: 60px;">
                    <asp:DropDownList ID="ddlSelectID" runat="server" Width="60px">
                    </asp:DropDownList>
                </td>
                <td>
                    <img id="imgInsert" src="../Pic/btnImg/button_insert_nor.png" alt="插入序号" onmouseover="SaveMouseover('imgInsert','../../../pic/btnImg/button_insert_over.png')"
                        onmouseout="SaveMouseout('imgInsert','../../../pic/btnImg/button_insert_nor.png')"
                        onclick="InsertValue('orderno');" />
                </td>
                <td>
                    <img id="imgLeft" src="../Pic/btnImg/button_left_nor.png" alt="插入左括号" onmouseover="SaveMouseover('imgLeft','../../../pic/btnImg/button_left_over.png')"
                        onmouseout="SaveMouseout('imgLeft','../../../pic/btnImg/button_left_nor.png')"
                        onclick="InsertValue('(');" />
                </td>
                <td>
                    <img id="imgRight" src="../Pic/btnImg/button_right_nor.png" alt="插入右括号" onmouseover="SaveMouseover('imgRight','../../../pic/btnImg/button_right_over.png')"
                        onmouseout="SaveMouseout('imgRight','../../../pic/btnImg/button_right_nor.png')"
                        onclick="InsertValue(')');" />
                </td>
                <td>
                    <img id="imgAND" src="../Pic/btnImg/btn_and_nor.png" alt="逻辑与" onmouseover="SaveMouseover('imgAND','../../../pic/btnImg/btn_and_over.png')"
                        onmouseout="SaveMouseout('imgAND','../../../pic/btnImg/btn_and_nor.png')" onclick="InsertValue('AND');" />
                </td>
                <td>
                    <img id="imgOR" src="../Pic/btnImg/btn_or_nor.png" alt="逻辑或" onmouseover="SaveMouseover('imgOR','../../../pic/btnImg/btn_or_over.png')"
                        onmouseout="SaveMouseout('imgOR','../../../pic/btnImg/btn_or_nor.png')" onclick="InsertValue('OR');" />
                </td>
                <td>
                    <img id="imgNOT" src="../Pic/btnImg/btn_not_nor.png" alt="逻辑非" onmouseover="SaveMouseover('imgNOT','../../../pic/btnImg/btn_not_over.png')"
                        onmouseout="SaveMouseout('imgNOT','../../../pic/btnImg/btn_not_nor.png')" onclick="InsertValue('NOT');" />
                </td>
                <td style="text-align: right; width: 320px;">
                    <img id="imgValidate" src="../Pic/btnImg/button_validate_nor.png" alt="验证" onmouseover="SaveMouseover('imgValidate','../../../pic/btnImg/button_validate_over.png')"
                        onmouseout="SaveMouseout('imgValidate','../../../pic/btnImg/button_validate_nor.png')"
                        onclick="ValidateValue();" />
                </td>
            </tr>
        </table>
    </div>
    <div class="pro_1" style="padding-bottom: 30px;">
        <table style="width: 765px;">
            <tr>
                <td style="margin: 0 auto; text-align: center;">
                    <asp:TextBox ID="txtExpression" runat="server" MaxLength="100" Width="765" Height="120"
                        TextMode="MultiLine" onKeyDown="if(event.keyCode==32)   return false;"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 0 auto; margin-top: 10px; padding-bottom: 30px; text-align: center;">
        <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnSave_over.png')"
            onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnSave_nor.png')" ImageUrl="~/pic/btnImg/btnSave_nor.png"
            OnClick="btnSave_Click" OnClientClick="return SaveValidate();" />
        <asp:ImageButton ID="btnView" runat="server" Width="68px" Height="21px" onmouseover="SaveMouseover('btnView','../../../pic/btnImg/btnYLJG_over.png')"
            onmouseout="SaveMouseout('btnView','../../../pic/btnImg/btnYLJG_nor.png')" ImageUrl="~/pic/btnImg/btnYLJG_nor.png"
            OnClientClick="ReViewValidate();return false;" />
    </div>
    <asp:HiddenField ID="hfValidaterResult" runat="server" />
    </form>
</body>
</html>
