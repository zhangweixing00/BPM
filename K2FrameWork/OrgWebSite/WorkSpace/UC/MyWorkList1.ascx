<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyWorkList1.ascx.cs"
    Inherits="Sohu.OA.Web.WorkSpace.UC.MyWorkList1" %>
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
                申请人：
            </td>
            <td>
                <asp:TextBox ID="txtSubmittor" runat="server" Style="height: 22px; border: 1px #999999 solid;
                    background: url(../../../pic/menu1.png) no-repeat right" onfocus="return SelectSubmitor();"></asp:TextBox>
                <asp:HiddenField ID="hfSubmittor" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                流程状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlFlowStatus" runat="server" Width="150" CssClass="ddlcss">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="新任务" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已查看" Value="1"></asp:ListItem>
                    <asp:ListItem Text="睡眠" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                时间段：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" Style="width: 64px; width: 67px; height: 22px;
                    border: 1px #999999 solid; font-family: Hei, san-serif; font-size: 12px;" onclick="WdatePicker({readOnly:true})"></asp:TextBox>
                -
                <asp:TextBox ID="txtEndDate" runat="server" Style="width: 63px; width: 68px; height: 22px;
                    border: 1px #999999 solid; font-family: Hei, san-serif; font-size: 12px;" onclick="WdatePicker({readOnly:true})"></asp:TextBox>
            </td>
            <td align="right">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td>
            </td>
            <td align="right">
            </td>
            <td>
            </td>
            <td align="right">
            </td>
            <td align="right">
                <div>
                    <asp:ImageButton ID="btnSearch" runat="server" onmouseover="SaveMouseover('MyWorklist1_btnSearch','../../../pic/btnImg/btnQueryBit.png')"
                        onmouseout="SaveMouseout('MyWorklist1_btnSearch','../../../pic/btnImg/btnQueryBit_nor.png')"
                        ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" OnClick="btnSearch_Click">
                    </asp:ImageButton>
                    <%--&nbsp;&nbsp;
        <asp:Button ID="btnHTMLBatchApproval" runat="server" OnClientClick="BatchApproval()"
            Text="批量审批" CssClass="btn" Width="60" />
        &nbsp;&nbsp;
        <asp:Button ID="btnHTMLBatchRedirect" runat="server" Text="任务转接" CssClass="btn" Width="60"
            OnClick="btnHTMLBatchRedirect_Click" OnClientClick="return BatchRedirect();" />--%>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="nav_2">
    <p>
        任务列表</p>
</div>
<div class="nav_3">
    <p>
        <span style="float: left; line-height: 23px;"><span>
            <asp:CheckBox runat="server" ID="chkBatch" onclick="BatchSeleted();" Style="vertical-align: middle;
                margin-bottom: 2px; margin-left: 6px;" />
            批量审批 </span><span id='divBatch' style="display: none; float: right;">
                <asp:DropDownList ID="ddlProcName" runat="server" Width="100" CssClass="ddlcss">
                    <asp:ListItem Selected="True" Value="K2WorkflowProject1\Process1">流程名称</asp:ListItem>
                    <asp:ListItem Value="">测试流程</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlProcNode" runat="server" Width="100" CssClass="ddlcss">
                    <asp:ListItem Selected="True" Value="">节点名称</asp:ListItem>
                    <asp:ListItem Value="DefaultActivity">DefaultActivity</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnBatch" runat="server" Text="批量审批" OnClientClick="return BatchApprove();"
                    CssClass="btnCommon" OnClick="btnBatch_Click" />
            </span></span>
        <asp:Button ID="btnRedirect" runat="server" Text="转发" CssClass="btnCommon" OnClientClick="return  BatchRedirect();" />
        <asp:Button ID="btnRedirectHF" runat="server" Text="转发" CssClass="btnCommon" OnClick="btnRedirect_Click"
            Style="display: none;" />
        <asp:Button ID="btnDelegate" runat="server" Text="代理" CssClass="btnCommon" OnClientClick="return  BatchDelegate();" />
        <asp:Button ID="btnDelegateHF" runat="server" Text="代理" CssClass="btnCommon" OnClick="btnDelegate_Click"
            Style="display: none;" />
        <asp:Button ID="btnSleep" runat="server" Text="睡眠" CssClass="btnCommon" OnClientClick="return Sleep();" />
        <asp:Button ID="btnSleepHF" runat="server" Text="睡眠" CssClass="btnCommon" OnClick="btnSleep_Click"
            Style="display: none;" />
        <asp:Button ID="btnRelease" runat="server" Text="释放" CssClass="btnCommon" OnClientClick="return Release();"
            OnClick="btnRelease_Click" />
    </p>
</div>
<div class='pro_1'>
    <asp:GridView ID="gvMyWorkList" runat="server" AutoGenerateColumns="False" CssClass="datalist2"
        Width="768px" BorderWidth="1px" HeaderStyle-BackColor="#fef5c7" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>没有任务记录</div>">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="2%">
                <HeaderTemplate>
                    <input type="checkbox" id="chkAll" onclick="CheckAllSN(this)" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID='chkCheck' onclick='ChooseSN(this)' />
                    <asp:HiddenField ID="chkCheckSN" runat="server" Value='<%#Eval("SN")%>' />
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" HorizontalAlign="Center" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="表单号" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("FormURL")%>
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
            <asp:TemplateField HeaderText="申请人" HeaderStyle-Width="7%">
                <ItemTemplate>
                    <%#Eval("Originator")%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:BoundField DataField="ActivityName" HeaderText="当前节点" ItemStyle-BorderColor="#f2dd81"
                HeaderStyle-BorderColor="#f2dd81" HeaderStyle-Width="10%">
                <HeaderStyle BorderColor="#F2DD81" Width="10%"></HeaderStyle>
                <ItemStyle BorderColor="#F2DD81"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="PStartTime" HeaderText="申请时间" ItemStyle-BorderColor="#f2dd81"
                HeaderStyle-BorderColor="#f2dd81">
                <HeaderStyle BorderColor="#F2DD81" Width="12%"></HeaderStyle>
                <ItemStyle BorderColor="#F2DD81"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderStyle-Width="6%" HeaderText="流程状态">
                <ItemTemplate>
                    <%#Eval("AState")%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
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
                        <asp:ImageButton ID="btnGoPage" runat="server" onmouseover="SaveMouseover('MyWorklist1_btnGoPage','../../../pic/btnImg/BtnGoto_over.png')"
                            onmouseout="SaveMouseout('MyWorklist1_btnGoPage','../../../pic/btnImg/BtnGoto_nor.png')"
                            ImageUrl="~/pic/btnImg/BtnGoto_nor.png" Width="40px" OnClick="btnGoPage_Click">
                        </asp:ImageButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<asp:HiddenField ID="hfSelectedSN" runat="server" />
<asp:HiddenField ID="hfAdAcount" runat="server" />
<asp:HiddenField ID="hfSleep" runat="server" Value="0" />
