<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcessLog.ascx.cs" Inherits="OrgWebSite.Process.Controls.ProcessLog" %>
<link href="/Styles/css.css" rel="stylesheet" type="text/css" />
<link href="/Styles/style.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .main
    {
        margin: 0 auto;
        width: 1020px;
        height: auto;
        min-height: 200px;
        background: #fffdf1;
    }
    .datalist2{ width:800px; border:1px solid #f2dd81; color:#333; border-collapse:collapse;
        text-align: center;
    }
   .datalist2 td,.datalist2 th{ border:1px solid #f2dd81;}
</style>
<div class="conter_right_list_main">

     <div class=" conter_right_list_nav" id="InputPersonDiv" style=" height:26px;" runat="server">
        <span>审批记录</span>
    </div>

     <div style="margin-top:20px; float:right;margin-right:28px; * margin-right:16px; margin-right:28px \0;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#F2DD81"
    EmptyDataText="&lt;div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'&gt;没有审批记录&lt;/div&gt;"
        BorderWidth="1px" Width="540px" CssClass="datalist2" ClientIDMode="Static">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    审批人
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_Employees" runat="server" 
                        Text=' <%# Eval("ApproverName")%>' 
                        style="text-align: center"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="8%"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <HeaderTemplate>
                    审批节点
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_Employees" runat="server" Text='<%# Bind("ActivityName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="7%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    审批动作
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_BatchDate" runat="server" Text='<%# K2Utility.ProcessPage.GetActionString(Eval("Operation").ToString()) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="6%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    审批时间
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_State" runat="server" Text='<%# Bind("Operatetime") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="8%"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    审批意见
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_Description" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="15%" />
            </asp:TemplateField>
        </Columns>
    
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BorderColor="#F2DD81" BorderWidth="1px" />
        <SelectedRowStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
    </div>
</div>
