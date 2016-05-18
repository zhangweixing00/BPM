<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDelegation.ascx.cs"
    Inherits="Sohu.OA.Web.WorkSpace.UC.MyDelegation" %>
 <script language="javascript" type="text/javascript">
     function SelectSubmitor()
     {
         var para = "?checkstyle=true";

         ymPrompt.win('../Search/K2FlowCheck/K2FlowCheck.aspx' + para, 760, 560, "人员选择", TrueInfos, null, null, true);
     }
     function TrueInfos(retValue)
     {
         switch (retValue)
         {
             case "close": ymPrompt.close(); break;
             case "cancel": ymPrompt.close(); break;
             default:
                 if (retValue && retValue.length != 0)
                 {
                     document.getElementById("MyDelegation1_txtSubmittor").value = retValue[0].split(';')[0];
                     var employeeCode = retValue[0].split(';')[1]
                     if (employeeCode != "")
                         document.getElementById("MyDelegation1_hfSubmittor").value = employeeCode;
                 }
                 else
                 {
                     document.getElementById("MyDelegation1_txtSubmittor").value = "";
                     document.getElementById("MyDelegation1_hfSubmittor").value = "";
                 }
                 ymPrompt.close();
                 break;
         }

         document.getElementById("MyDelegation1_txtSubmittor").blur();
     }
    </script>
<style type="text/css">
    .ddlcss
    {
        height: 22px;
        border: 1px #999999 solid;
    }
    .datalist2 TD A
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
                代理人：
            </td>
            <td>
                <asp:TextBox ID="txtSubmittor" runat="server"  onfocus="return SelectSubmitor();"
                    Style="height: 22px; border: 1px #999999 solid; background: url(../../../pic/menu1.png) no-repeat right"></asp:TextBox>
                <asp:HiddenField ID="hfSubmittor" runat="server" />
            </td>
            <td align="right" style="width: 90px;">
                代理状态：
            </td>
            <td>
                <asp:DropDownList ID="ddlFlowStatus" runat="server" Width="150" CssClass="ddlcss">
                    <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="有效" Value="1"></asp:ListItem>
                    <asp:ListItem Text="过期" Value="0"></asp:ListItem>
                    <asp:ListItem Text="取消" Value="-1"></asp:ListItem>
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
                    <asp:ImageButton ID="btnSearch" runat="server" onmouseover="SaveMouseover('MyDelegation1_btnSearch','../../../pic/btnImg/btnQueryBit.png')"
                        onmouseout="SaveMouseout('MyDelegation1_btnSearch','../../../pic/btnImg/btnQueryBit_nor.png')"
                        ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" OnClick="btnSearch_Click">
                    </asp:ImageButton>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="nav_2" style="top: 16px;">
    <p>
        代理列表</p>
</div>
<div class="nav_3">
    <p>
        <asp:ImageButton ID="btnCreateBatch" runat="server" onmouseover="SaveMouseover('MyDelegation1_btnCreateBatch','../../../pic/btnImg/btnAdd_over.png')"
            onmouseout="SaveMouseout('MyDelegation1_btnCreateBatch','../../../pic/btnImg/btnAdd_nor.png')"
            OnClientClick="AddDelegation(); return false; " ImageUrl="~/pic/btnImg/btnAdd_nor.png" />
    </p>
</div>
<div class='pro_1'>
    <asp:GridView ID="gvDeligation" runat="server" CssClass="datalist2" AllowPaging="True"
        AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvDeligation_RowDeleting"
        OnPageIndexChanging="gvDeligation_PageIndexChanging" OnPreRender="gvDeligation_PreRender"
        Width="768px" BorderWidth="1px" HeaderStyle-BackColor="#fef5c7" EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>没有代理记录</div>"
        OnRowDataBound="gvDeligation_RowDataBound">
        <%--<EmptyDataTemplate>
            <table style="width: 768px;">
                <tbody>
                    <tr bgcolor='#fef5c7'>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            流程名称
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            代理结点
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            被代理人
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            代理人
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            开始时间
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            结束时间
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            备注
                        </th>
                        <th scope='col' style="border-left: none; border-top: none; border-bottom: none;">
                            状态
                        </th>
                        <th scope='col' style="border: none;">
                            取消
                        </th>
                    </tr>
                </tbody>
            </table>
        </EmptyDataTemplate>--%>
        <Columns>
            <%--  <asp:BoundField DataField="Node" HeaderText="代理结点" ItemStyle-BorderColor="#f2dd81"
                HeaderStyle-BorderColor="#f2dd81" >
                <HeaderStyle BorderColor="#F2DD81" ></HeaderStyle>
                <ItemStyle BorderColor="#F2DD81"></ItemStyle>
            </asp:BoundField>--%>
            <asp:TemplateField HeaderText="流程名称">
                <ItemTemplate>
                    <%#GetFlowName(Eval("ProcName").ToString())%>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="100px" />
                <ItemStyle BorderColor="#F2DD81" Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="被代理人">
                <ItemTemplate>
                    <%# Eval("FromUser") %>
                    <asp:HiddenField ID="lbBD" runat="server" Value='<%#Eval("FromUser") %>' />
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="99px" />
                <ItemStyle BorderColor="#F2DD81" Width="99px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="代理人">
                <ItemTemplate>
                    <%# Eval("ToUser") %>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="99px" />
                <ItemStyle BorderColor="#F2DD81" Width="99px" />
            </asp:TemplateField>
            <asp:BoundField DataField="StartDate" HeaderText="开始时间" ItemStyle-BorderColor="#f2dd81"
                HeaderStyle-BorderColor="#f2dd81" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle BorderColor="#F2DD81" Width="100px"></HeaderStyle>
                <ItemStyle BorderColor="#F2DD81" Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="EndDate" HeaderText="结束时间" ItemStyle-BorderColor="#f2dd81"
                HeaderStyle-BorderColor="#f2dd81" DataFormatString="{0:yyyy-MM-dd}">
                <HeaderStyle BorderColor="#F2DD81" Width="100px"></HeaderStyle>
                <ItemStyle BorderColor="#F2DD81" Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="代办条件">
                <ItemTemplate>
                    <asp:Label ID="lbConditions" runat="server" Text='<%#!String.IsNullOrEmpty(Eval("Conditions").ToString())?(Convert.ToBoolean(Eval("Conditions").ToString())?"代办新任务":"全部代办"):"" %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="75px" />
                <ItemStyle BorderColor="#F2DD81" Width="75px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="160px" />
                <ItemStyle BorderColor="#F2DD81" Width="160px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <asp:Label ID="lbStatus" runat="server" Text='<%#Eval("State") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="55px" />
                <ItemStyle BorderColor="#F2DD81" Width="55px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="取消" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton0" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="取消" OnClientClick="ConfirmDel(this.id); return false;" Visible='<%#Eval("State").ToString()=="1" ? true : false %>'></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="取消" Visible='<%#Eval("State").ToString()=="1" ? true : false %>' Style="display: none;"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle BorderColor="#F2DD81" Width="55px" />
                <ItemStyle BorderColor="#F2DD81" Width="55px" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#FEF5C7"></HeaderStyle>
        <PagerTemplate>
            <table class='datalist3' border='0' style="width: 768px;">
                <tbody>
                    <tr style="color: #8c4510;">
                        <td style="text-align: right; border-style: none;">
                            共<%# PageCount %>条 第<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>页/共<%# ((GridView)Container.Parent.Parent).PageCount  %>页
                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                CommandName="Page" Text="首页" />
                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                CommandName="Page" Text="上一页" />
                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                CommandName="Page" Text="下一页" />
                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                CommandName="Page" Text="末页" />
                            <asp:TextBox ID="txtNewPageIndex" runat="server" Width="30" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>' />
                        </td>
                        <td style="width: 50px; border-style: none;">
                            <asp:ImageButton ID="btnGo" runat="server" 
                                ImageUrl="~/pic/btnImg/BtnGoto_nor.png" CausesValidation="false" CommandName="Page"
                                CommandArgument="-1" Width="40px"></asp:ImageButton><!-- here set the CommandArgument of the Go Button to '-1' as the flag -->
                        </td>
                    </tr>
            </table>
        </PagerTemplate>
    </asp:GridView>
</div>
