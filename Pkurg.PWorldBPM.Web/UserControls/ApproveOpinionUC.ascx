<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApproveOpinionUC.ascx.cs"
    Inherits="UserControls_ApproveOpinionUC" %>
<div style="border: none;">
    <asp:HiddenField ID="hfDept" runat="server" />
    <div>
        <asp:Label ID="lablDeptLeaderOpion" runat="server" Text=""></asp:Label>
    </div>
    <div style="clear: both;">
        <asp:TextBox ID="tbDeptLeaderOpion" runat="server" CssClass="longtxt" Height="50"
            TextMode="MultiLine"></asp:TextBox>
    </div>
</div>
