<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyStarted1.ascx.cs"
    Inherits="Sohu.OA.Web.WorkSpace.UC.MyStarted1" %>
<style type="text/css">
    .ddlcss
    {
        height: 22px;
        border: 1px #999999 solid;
    }
    .datalist3 TD A
    {
        color: #0066ff;
        text-decoration: underline;
    }
</style>
<script type="text/javascript">
    function expand(open, clo, num) {

        var divopened = document.getElementById(open + num);
        var divclosed = document.getElementById(clo + num);
        divopened.style.cssText = "display:inline-table";
        divclosed.style.cssText = "display:none";
    }
</script>
<div class='nav_1' style="top: 16px;">
    <p>
        信息查询</p>
</div>
<div class="pro" style="margin-top: 15px;">
    <table class="datalist1" border="0" cellspacing="5">
        <tr>
            <td align="right" style="width: 90px;">
                流程名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlProcess" runat="server" Width="150" CssClass="ddlcss">
                </asp:DropDownList>
            </td>
            <td align="right" style="width: 90px;">
                表单号：
            </td>
            <td>
                <asp:TextBox ID="txtFolio" runat="server" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
            </td>
            <td align="right" style="width: 90px;">
                流程状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlFlowStatus" runat="server" Width="150" CssClass="ddlcss">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="已提交" Value="HRBPSubmit"></asp:ListItem>
                    <asp:ListItem Text="处理中" Value="Running"></asp:ListItem>
                    <asp:ListItem Text="拒绝" Value="Rejected"></asp:ListItem>
                    <asp:ListItem Text="已取消" Value="Cancelled"></asp:ListItem>
                    <asp:ListItem Text="已完成" Value="Finished"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                时间段：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" Style="width:64px; width:68px \9; height: 22px; border: 1px #999999 solid;
                    font-family: Hei, san-serif; font-size: 12px;" onclick="WdatePicker({readOnly:true})"></asp:TextBox>
                -
                <asp:TextBox ID="txtEndDate" runat="server" Style="width:64px; width:68px \9; height: 22px; border: 1px #999999 solid;
                    font-family: Hei, san-serif; font-size: 12px;" onclick="WdatePicker({readOnly:true})"></asp:TextBox>
            </td>
            <td align="right">
            </td>
            <td align="right">
            </td>
            <td align="right">
            </td>
            <td align="right" rowspan="2" valign="top">
                <div>
                    <asp:ImageButton ID="btnSearch" runat="server" onmouseover="SaveMouseover('MyStarted1_btnSearch','../../../pic/btnImg/btnQueryBit.png')"
                        onmouseout="SaveMouseout('MyStarted1_btnSearch','../../../pic/btnImg/btnQueryBit_nor.png')"
                        ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" OnClick="btnSearch_Click">
                    </asp:ImageButton>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="nav_2" style="margin-top:13px;">
    <p>
        任务列表</p>
</div>
<div class='pro_1'>
    <asp:GridView ID="gvMyStarted" runat="server" AutoGenerateColumns="False" CssClass="datalist2"
        Width="768px" BorderWidth="1px" HeaderStyle-BackColor="#fef5c7" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>没有任务记录</div>"
        OnRowCommand="gvMyStarted_RowCommand" DataKeyNames="ProcInstID,Status,ApproveCount,FirstActivity">
        <%--<EmptyDataTemplate>
            <table style="width: 768px;">
               <tbody>
                    <tr bgcolor='#fef5c7'>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            表单号
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            申请时间
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            当前节点
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            当前处理人
                        </th>
                        <th scope='col' style="border: none;">
                            流程状态
                        </th>
                        <th scope='col' style="border:none;">
                            召回
                        </th>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>--%>
        <Columns>
            <asp:TemplateField HeaderText="表单号" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("Folio") %>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="流程名称" HeaderStyle-Width="8%">
                <ItemTemplate>
                    <%#Eval("ProcName")%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间" HeaderStyle-Width="12%">
                <ItemTemplate>
                    <%#Eval("StartDate").ToString()%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="当前节点">
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" style="border-style: none; text-align: center;
                        width: 100%;">
                        <tr style="border-style: none; display: <%#!String.IsNullOrEmpty(Eval("CurrentActName").ToString())?"inline-table;":"none"%>;"
                            id="oneActive<%# Container.DataItemIndex %>">
                            <td style="border-style: none;">
                                <div>
                                    <%#!String.IsNullOrEmpty(Eval("CurrentActName").ToString())?Eval("CurrentActName").ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)[0]:""%></div>
                            </td>
                            <td style="border-style: none; width: 30px;">
                                <a href="javascript:expand('allActive','oneActive',<%# Container.DataItemIndex %>);">
                                    展开</a>
                            </td>
                        </tr>
                        <tr style="border-style: none; display: none;" id="allActive<%# Container.DataItemIndex %>">
                            <td style="border-style: none;">
                                <div>
                                    <%#Eval("CurrentActName").ToString().Replace(";","<br/>")%></div>
                            </td>
                            <td style="border-style: none; width: 30px;">
                                <a href="#" onclick="expand('oneActive','allActive',<%# Container.DataItemIndex %>);">
                                    收起</a>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="12%" />
                <ItemStyle BorderColor="#F2DD81" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="当前处理人" HeaderStyle-Width="15%">
                <ItemTemplate>
                    <table cellpadding="0" cellspacing="0" style="border-style: none; text-align: center;
                        width: 100%;">
                        <tr style="border-style: none; display: <%#!String.IsNullOrEmpty(Eval("CurrentActioners").ToString())?"inline-table;":"none"%>;"
                            id="one<%# Container.DataItemIndex %>">
                            <td style="border-style: none;">
                                <div>
                                    <%#!String.IsNullOrEmpty(Eval("CurrentActioners").ToString()) ? Eval("CurrentActioners").ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)[0] : ""%></div>
                            </td>
                            <td style="border-style: none; width: 30px;">
                                <a href="#" onclick="expand('all','one',<%# Container.DataItemIndex %>);">展开</a>
                            </td>
                        </tr>
                        <tr style="border-style: none; display: none;" id="all<%# Container.DataItemIndex %>">
                            <td style="border-style: none;">
                                <div>
                                    <%#Eval("CurrentActioners").ToString().Replace(";", "<br/>")%></div>
                            </td>
                            <td style="border-style: none; width: 30px;">
                                <a href="#" onclick="expand('one','all',<%# Container.DataItemIndex %>);">收起</a>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-Width="7%" HeaderText="流程状态">
                <ItemTemplate>
                    <%#Eval("Status")%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="召回" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton0" runat="server" CausesValidation="false" OnClientClick="ConfirmDel(this.id); return false;"
                        Text="召回" Visible='<%#(ConfigurationManager.AppSettings["K2CanCallBackProcess"].Contains( Eval("ProcName").ToString())&&Eval("ApproveCount").ToString()=="1"&&!string.IsNullOrEmpty(Eval("CurrentActName").ToString())&&Eval("CurrentActName").ToString()==Eval("FirstActivity").ToString()) ? true : false %>'></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="CallBack"
                        Text="召回" Style="display: none;" Visible='<%#(ConfigurationManager.AppSettings["K2CanCallBackProcess"].Contains( Eval("ProcName").ToString())&&Eval("ApproveCount").ToString()=="1"&&!string.IsNullOrEmpty(Eval("CurrentActName").ToString())&&Eval("CurrentActName").ToString()==Eval("FirstActivity").ToString()) ? true : false %>'></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="5%" />
                <ItemStyle BorderColor="#F2DD81" />
            </asp:TemplateField>--%>
        </Columns>
        <HeaderStyle BackColor="#FEF5C7"></HeaderStyle>
    </asp:GridView>
    <div id="divfenye" runat="server">
        <table class='datalist3' border='1' bordercolor='#f2dd81' style="width: 768px; border-top: none;">
            <tbody>
                <tr style="color: #8c4510;">
                    <td style="text-align: right; border-style: none;">
                        共<asp:Literal ID="txtTotalNum" runat="server"></asp:Literal>条 第<asp:Literal ID="txtCurrPage"
                            runat="server"></asp:Literal>页/共<asp:Literal ID="txtPageCount" runat="server"></asp:Literal>页
                        <asp:LinkButton ID="lbFirst" runat="server" Text="" CommandName="First" OnCommand="lbPage_Command">首页</asp:LinkButton>
                        <asp:LinkButton ID="lbPrev" runat="server" Text="" CommandName="Prev" OnCommand="lbPage_Command">上一页</asp:LinkButton>
                        <asp:LinkButton ID="lbNext" runat="server" Text="" CommandName="Next" OnCommand="lbPage_Command">下一页</asp:LinkButton>
                        <asp:LinkButton ID="lbLast" runat="server" Text="" CommandName="Last" OnCommand="lbPage_Command">末页</asp:LinkButton>
                        <asp:TextBox ID="txtPageNum" runat="server" Width="30"></asp:TextBox>
                    </td>
                    <td style="width: 50px; border-style: none;">
                        <%--<asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click" CssClass="btn" />--%>
                        <asp:ImageButton ID="btnGoPage" runat="server" onmouseover="SaveMouseover('MyStarted1_btnGoPage','../../../pic/btnImg/BtnGoto_over.png')"
                            onmouseout="SaveMouseout('MyStarted1_btnGoPage','../../../pic/btnImg/BtnGoto_nor.png')"
                            ImageUrl="~/pic/btnImg/BtnGoto_nor.png" Width="40px" OnClick="btnGoPage_Click">
                        </asp:ImageButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
