<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessNodeManage.aspx.cs"
    Inherits="OrgWebSite.Admin.ProcessNodeManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程节点管理</title>
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ProcessNodeAction(state, nodeId) {
            var processType = document.getElementById('ddlProcessType');
            var processID = processType.options[processType.selectedIndex].value;
            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&ProcessID=" + processID + "&state=" + state + "&NodeID=" + nodeId;

            if (processType != null) {
                var vurl = "Popup/ProcessNodeEdit.aspx?" + para;
                //                top.window.ymPrompt.win(vurl, 730, 550, '编辑审批节点', null, null, null, true, null, null, true, true, true);
                window.open(vurl);

            }
        }

        function ProcessNodeRuleAction(state, nodeId) {
            var random = Math.round(Math.random() * 10000);
            var para = "random=" + random + "&NodeID=" + nodeId + "&state=" + state;
            var vurl = "Admin/Popup/ProcessRuleEdit.aspx?" + para;
            top.window.ymPrompt.win(vurl, 520, 340, '编辑审批节点规则', null, null, null, true, null, null, true, false, true);
        }

        function DeleteProcessNode(nodeId) {
            top.window.ymPrompt.confirmInfo({
                message: "是否要删除？",
                title: '删除请求节点',
                handler: function ConFirm(tp) {
                    if (tp == "ok") {
                        $.ajax({
                            type: "POST",
                            url: "OperateHandler.ashx",
                            data: { nodeId: nodeId, action: "deleteProcessNode" },
                            async: false,
                            success: function (data) {
                                if (data > 0) {
                                    top.window.ymPrompt.alert({ title: '删除审批节点', message: '删除成功！', handler: function Confirm(tp) { if (tp == 'ok') { __doPostBack('lbReload', ''); } } })
                                }
                                else if (data == 0) {
                                    top.window.ymPrompt.alert({ title: '删除审批节点', message: '删除失败！' })
                                }
                            },
                            error: function () {
                                top.window.ymPrompt.alert({ title: '删除审批节点', message: '删除出错！' });
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
    <div id="container" style="width: 790px;">
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
                        <asp:DropDownList ID="ddlProcessType" runat="server"  DataTextField="ProcessType"
                            DataValueField="ID" AutoPostBack="True" 
                            onselectedindexchanged="ddlProcessType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="Div_peolistName" class="nav_1">
        <p>
            审批节点列表</p>  
    </div>
    <div id="process_Node_List" class="pro_1" style="padding-bottom: 30px;">
        <table style="width: 765px;">
            <tr>
                <td style="text-align: right; height: 30px;">
                    <asp:ImageButton ID="btnAddProcessNode" runat="server" Text="添加流程节点" onmouseover="SaveMouseover('btnAddProcessNode','../../../pic/btnImg/btnAddone.png')"
                        onmouseout="SaveMouseout('btnAddProcessNode','../../../pic/btnImg/btnAddone_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddone_nor.png" OnClientClick="ProcessNodeAction('new')" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvProcessNodes" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        Width="765px" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无节点数据</div>"
                        BorderWidth="1px" AllowPaging="false" CssClass="datalist2">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="<%# gvProcessNodes.PageIndex * gvProcessNodes.PageSize 
                        + gvProcessNodes.Rows.Count + 1%>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="节点名称" DataField="NodeName" />
                            <asp:BoundField HeaderText="状态" DataField="State" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <a href="javascript:void(0)" onclick="DeleteProcessNode('<%#Eval("NodeID") %>');">删除节点</a>
                                    <a href="javascript:void(0)" onclick="ProcessNodeAction('edit','<%#Eval("NodeID") %>');">编辑节点</a>
                                    <a href="javascript:void(0)" onclick="ProcessNodeRuleAction('edit','<%#Eval("NodeID") %>');">编辑规则</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:LinkButton ID="lbReload" runat="server"  onclick="lbReload_Click"></asp:LinkButton>
    </form>
</body>
</html>
