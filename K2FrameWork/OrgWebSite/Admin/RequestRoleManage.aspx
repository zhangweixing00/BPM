<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestRoleManage.aspx.cs"
    Inherits="OrgWebSite.Admin.RequestRoleManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>申请角色管理</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function RequestNodeAction(state, nodeId) {
            var processType = document.getElementById('ddlProcessType');
            if (processType != null) {
                var processID = processType.options[processType.selectedIndex].value;
                var random = Math.round(Math.random() * 10000);
                var para = "random=" + random + "&ProcessID=" + processID + "&state=" + state + "&NodeID=" + nodeId;

                var vurl = "Admin/Popup/RequestNodeEdit.aspx?" + para;
                top.window.ymPrompt.win(vurl, 520, 340, '编辑入口节点', null, null, null, true, null, null, true, false, true);
            }
        }

        function RequestNodeRuleAction(state, nodeId) {
            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&NodeID=" + nodeId + "&state=" + state;
            var vurl = "Admin/Popup/RequestRuleEdit.aspx?" + para;
            top.window.ymPrompt.win(vurl, 520, 340, '编辑入口节点规则', null, null, null, true, null, null, true, false, true);
        }

        function DeleteRequestNode(nodeId) {
            top.window.ymPrompt.confirmInfo({
                message: "是否要删除？",
                title: '删除请求节点',
                handler: function ConFirm(tp) {
                    if (tp == "ok") {
                        $.ajax({
                            type: "POST",
                            url: "OperateHandler.ashx",
                            data: { nodeId: nodeId, action: "deleteRequestNode" },
                            async: false,
                            success: function (data) {
                                if (data > 0) {
                                    top.window.ymPrompt.alert({ title: '删除请求节点', message: '删除成功！', handler: function Confirm(tp) { if (tp == 'ok') { __doPostBack('lbReload', ''); } } })
                                }
                                else if (data == 0) {
                                    top.window.ymPrompt.alert({ title: '删除请求节点', message: '删除失败！' })
                                }
                            },
                            error: function () {
                                top.window.ymPrompt.alert({ title: '删除请求节点', message: '删除出错！' });
                            }
                        });

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
                <%-- <uc1:Sitemap ID="Sitemap1" runat="server" />--%>
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
                <td style="width: 50px; text-align: right;">
                    公司名称：
                </td>
                <td style="width: 550px;">
                    <asp:DropDownList ID="ddlProcessType" runat="server" DataTextField="ProcessType"
                        DataValueField="ID" AutoPostBack="True" 
                        onselectedindexchanged="ddlProcessType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="Div_peolistName" class="nav_1">
        <p>
            入口节点列表</p>
    </div>
    <div id="process_Node_List" class="pro_1" style="padding-bottom: 30px;">
        <table style="width: 765px;">
            <tr>
                <td style="text-align: right; height: 30px;">
                    <asp:ImageButton ID="btnAddRequestNode" runat="server" Text="添加流程入口节点" onmouseover="SaveMouseover('btnAddRequestNode','../../../pic/btnImg/btnAddone.png')"
                        onmouseout="SaveMouseout('btnAddRequestNode','../../../pic/btnImg/btnAddone_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddone_nor.png" OnClientClick="RequestNodeAction('new')" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvRequestNodes" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="765px" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无节点数据</div>"
                        BorderWidth="1px" AllowPaging="false" CssClass="datalist2">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="<%# gvRequestNodes.PageIndex * gvRequestNodes.PageSize 
                        + gvRequestNodes.Rows.Count + 1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="节点名称" DataField="NodeName" />
                            <asp:BoundField HeaderText="状态" DataField="State" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <a href="javascript:void(0)" onclick="DeleteRequestNode('<%#Eval("NodeID") %>')">删除节点</a>
                                    <a href="javascript:void(0)" onclick="RequestNodeAction('edit','<%#Eval("NodeID") %>');">编辑节点</a> <a href="javascript:void(0)"
                                        onclick="RequestNodeRuleAction('edit','<%#Eval("NodeID") %>');">编辑规则</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinkButton ID="lbReload" runat="server" onclick="lbReload_Click"></asp:LinkButton>
    </form>
</body>
</html>