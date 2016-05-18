<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyJoined1.ascx.cs" Inherits="Sohu.OA.Web.WorkSpace.UC.MyJoined1" %>
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
            <td align="right" style="width: 89px;">
                流程名称：
            </td>
            <td>
                <asp:DropDownList ID="ddlProcess" runat="server" Width="150" CssClass="ddlcss">
                </asp:DropDownList>
            </td>
            <td align="right" style="width: 89px;">
                表单号：
            </td>
            <td>
                <asp:TextBox ID="txtFolio" runat="server" Style="height: 22px; border: 1px #999999 solid;"></asp:TextBox>
            </td>
            <td align="right" style="width: 87px;">
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
                    <asp:ListItem Text="审批中" Value="Active"></asp:ListItem>
                    <asp:ListItem Text="已完成" Value="Completed"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                时间段：
            </td>
            <td>
                <asp:TextBox ID="txtStartDate" runat="server" Style="width: 63px; width: 67px \9;
                    height: 22px; border: 1px #999999 solid; font-family: Hei, san-serif; font-size: 12px;"
                    onclick="WdatePicker({readOnly:true})"></asp:TextBox>
                -
                <asp:TextBox ID="txtEndDate" runat="server" Style="width: 64px; width: 68px \9; height: 22px;
                    border: 1px #999999 solid; font-family: Hei, san-serif; font-size: 12px;" onclick="WdatePicker({readOnly:true})"></asp:TextBox>
            </td>
              <td align="right">
            </td>
            <td>
              </td>
            <td align="right">
            </td>
            <td align="right" rowspan="2" valign="top">
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
                    <asp:ImageButton ID="btnSearch" runat="server" onmouseover="SaveMouseover('MyJoined1_btnSearch','../../../pic/btnImg/btnQueryBit.png')"
                        onmouseout="SaveMouseout('MyJoined1_btnSearch','../../../pic/btnImg/btnQueryBit_nor.png')"
                        ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" OnClick="btnSearch_Click">
                    </asp:ImageButton>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="nav_2">
    <p>
        任务列表</p>
</div>
<div class='pro_1'>
    <asp:GridView ID="gvMyJoined" runat="server" AutoGenerateColumns="False" CssClass="datalist2"
        Width="768px" BorderWidth="1px" HeaderStyle-BackColor="#fef5c7" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>没有任务记录</div>"
        OnRowDataBound="gvMyJoined_RowDataBound">
        <%--<EmptyDataTemplate>
            <table style="width: 768px;">
                 <tbody>
                    <tr style="background-color: #fef5c7;">
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            表单号
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            申请人
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            申请时间
                        </th>
                        <th scope='col' style="border: none;">
                            流程状态
                        </th>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>--%>
        <Columns>
            <asp:TemplateField HeaderText="表单号" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("Folio")%>
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
            <asp:TemplateField HeaderText="申请人" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <%#Eval("Originator")%>
                </ItemTemplate>
                <ItemStyle BorderColor="#f2dd81" />
                <HeaderStyle BorderColor="#f2dd81" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="参与结点" HeaderStyle-Width="10%">
                <ItemTemplate>
                    <asp:Label ID="lbActName" runat="server" Text='<%#Eval("ActName") %>' />
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
            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="流程状态">
                <ItemTemplate>
                    <%#Eval("Status")%>
                    <asp:HiddenField ID="hfFlowStatus" runat="server" Value='<%#Eval("Status") %>' />
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
                        <asp:ImageButton ID="btnGoPage" runat="server" onmouseover="SaveMouseover('MyJoined1_btnGoPage','../../../pic/btnImg/BtnGoto_over.png')"
                            onmouseout="SaveMouseout('MyJoined1_btnGoPage','../../../pic/btnImg/BtnGoto_nor.png')"
                            ImageUrl="~/pic/btnImg/BtnGoto_nor.png" Width="40px" OnClick="btnGoPage_Click">
                        </asp:ImageButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
