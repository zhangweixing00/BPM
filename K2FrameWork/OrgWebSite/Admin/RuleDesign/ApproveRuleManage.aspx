<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRuleManage.aspx.cs"
    Inherits="OrgWebSite.Admin.RuleDesign.ApproveRuleManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批规则管理</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#lbtnEditTable').attr('style', 'display:none;');
        });
        function AddApproveNode() {
            $('#hfSelectedApproveNode').val('');
            var processType = document.getElementById('ddlProcessType');
            if (processType != null) {
                var processID = processType.options[processType.selectedIndex].value;
                var random = Math.round(Math.random() * 10000);
                var para = "ProcessID=" + processType.value;

                var vurl = "Admin/Popup/SelectApproveNode.aspx?" + para;
                top.window.ymPrompt.win(vurl, 520, 400, '审批节点', callBackUpdate, null, null, true, null, null, true, false, true);
            }
        }

        function callBackUpdate(rel) {
            if (rel != "close") {
                if (rel == null)
                    return false;
                else {
                    document.getElementById('<%=hfSelectedApproveNode.ClientID %>').value = rel;
                    document.getElementById('<%=lbReload.ClientID %>').click();
                }
            }
        }

        function AddRequestNode() {
            $('#hfSelectedRequestNode').val('');
            var processType = document.getElementById('ddlProcessType');
            if (processType != null) {
                var processID = processType.options[processType.selectedIndex].value;
                var random = Math.round(Math.random() * 10000);
                var para = "ProcessID=" + processType.value;
                var vurl = "Admin/Popup/SelectRequestNode.aspx?" + para;
                top.window.ymPrompt.win(vurl, 520, 400, '入口节点', callBackUpdate1, null, null, true, null, null, true, false, true);
            }
        }

        function callBackUpdate1(rel) {
            if (rel != "close") {
                if (rel == null)
                    return false;
                else {
                    document.getElementById('<%=hfSelectedRequestNode.ClientID %>').value = rel;
                    document.getElementById('<%=lbReload.ClientID %>').click();
                }
            }
        }

        function DelTable(tableId) {
            top.window.ymPrompt.confirmInfo({
                message: "是否要删除？",
                title: '删除规则表',
                handler: function ConFirm(tp) {
                    if (tp == "ok") {
                        $.ajax({
                            type: "POST",
                            url: "OperateHandler.ashx",
                            data: { tableId: tableId, action: "deleteRuleTable" },
                            async: false,
                            success: function (data) {
                                if (data > 0) {
                                    top.window.ymPrompt.alert({ title: '删除规则表', message: '删除成功！', handler: function Confirm(tp) { if (tp == 'ok') { __doPostBack('lbReload1', '') } } })
                                }
                                else if (data == 0) {
                                    top.window.ymPrompt.alert({ title: '删除规则表', message: '删除失败！' })
                                }
                            },
                            error: function () {
                                top.window.ymPrompt.alert({ title: '删除规则表', message: '删除出错！' });
                            }
                        });

                    }
                }
            });
            return false;
        }

        //编辑规则表
        function EditTable(tableId) {
            $('#hfSelectTableID').val(tableId);
            //$('#lbtnEditTable').click();
            __doPostBack('lbtnEditTable', tableId);
        }

        //检查CheckBox是否选择并记录状态
        function checkNode(obj, rowNodeID, colNodeID) {
            var hfSelectcb = document.getElementById('hfSelectedCheckBox');
            if (hfSelectcb != null) {
                if (obj.checked) {
                    if (!(hfSelectcb.value.indexOf(rowNodeID + ';' + colNodeID + ';') > 0)) {
                        hfSelectcb.value += rowNodeID + ';' + colNodeID + ';';
                    }
                }
                else {
                    hfSelectcb.value = hfSelectcb.value.replace(rowNodeID + ';' + colNodeID + ';', '');
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="nav">
            <p>
                <%-- <uc1:Sitemap ID="Sitemap1" runat="server" />--%>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
    </div>
    <div class="nav_1">
        <p>
            分组</p>
    </div>
    <div class="pro" style="margin: 10px 0; height: 60px;">
        <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
            <tr>
                <td style="width: 50px; text-align: right;">
                    流程名称：
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="ddlProcessType" runat="server" DataTextField="ProcessType"
                        DataValueField="ID" Width="200px">
                    </asp:DropDownList>
                </td>
                <td style="width: 50px; text-align: right;">
                    分组名称：
                </td>
                <td style="width: 200px;">
                    <asp:DropDownList ID="ddlGroup" runat="server" DataTextField="ProcessType" DataValueField="ID"
                        Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;" colspan="4">
                    <asp:ImageButton ID="btnSearch" runat="server" onmouseover="SaveMouseover('btnSearch','../../../pic/btnImg/btnQueryBit.png')"
                        onmouseout="SaveMouseout('btnSearch','../../../pic/btnImg/btnQueryBit_nor.png')"
                        ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" 
                        onclick="btnSearch_Click">
                    </asp:ImageButton>
                </td>
            </tr>
        </table>
    </div>

    <div id="apped" class="nav_1">
        <p>
            查询列表</p>
    </div>
    <div id="appedTable" runat="server" style="padding-top:30px; padding-left:50px;">
        
    </div>

    <div id="app" class="nav_1">
        <p>
            审批列表</p>
    </div>
    <div id="appTable" class="pro_1" style="padding-bottom: 30px;">
        <table style="width: 765px;">
            <tr>
                <td colspan="4" style="text-align: right; height: 30px;">
                    <asp:ImageButton ID="btnAddApproveNode" runat="server" Text="添加审批节点" onmouseover="SaveMouseover('btnAddApproveNode','../../../pic/btnImg/btnAddapp.png')"
                        onmouseout="SaveMouseout('btnAddApproveNode','../../../pic/btnImg/btnAddapp_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddapp_nor.png" OnClientClick="AddApproveNode();return false;" />
                    <asp:ImageButton ID="btnAddRequestNode" runat="server" Text="添加申请节点" onmouseover="SaveMouseover('btnAddRequestNode','../../../pic/btnImg/btnAddReqone.png')"
                        onmouseout="SaveMouseout('btnAddRequestNode','../../../pic/btnImg/btnAddReqone_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddReqone_nor.png" OnClientClick="AddRequestNode();return false;" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right;">
                    审批表名称：
                </td>
                <td style="width: 200px; text-align:left;">
                    <asp:TextBox ID="txtRuleTableName" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                </td>
                <td style="width: 80px; text-align: right;">
                    入口存储过程名：
                </td>
                <td style="width: 200px; text-align:left;">
                    <asp:TextBox ID="txtRequestSPName" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; text-align: right;">
                    分组名：</td>
                <td style="width: 200px; text-align:left;">
                    <asp:TextBox ID="txtGroup" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                </td>
                <td style="width: 80px; text-align: right;">
                    &nbsp;</td>
                <td style="width: 200px; text-align:left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Table ID="tApproveTable" runat="server" border="1" CellSpacing="0" CellPadding="0"
                        Width="560px" CssClass="datalist2" Style="padding-bottom: 30px; padding-left:50px;">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 0 auto; margin-top: 10px; padding-bottom: 30px; text-align: center;">
        <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnSave_over.png')"
            onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnSave_nor.png')" ImageUrl="~/pic/btnImg/btnSave_nor.png"
            OnClick="btnSave_Click" />
    </div>
    <asp:LinkButton ID="lbReload" runat="server" OnClick="lbReload_Click"></asp:LinkButton>
    <asp:HiddenField ID="hfSelectedApproveNode" runat="server" />
    <asp:HiddenField ID="hfSelectedRequestNode" runat="server" />
    <asp:HiddenField ID="hfSelectedCheckBox" runat="server" />
    <asp:HiddenField ID="hfSelectTableID" runat="server" />
    <asp:Button ID="lbtnEditTable" runat="server" onclick="lbtnEditTable_Click" />
    </form>
</body>
</html>
