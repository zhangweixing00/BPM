<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadAttachments.ascx.cs"
    Inherits="UploadAttachments" %>
<style type="text/css">
    .attachmentTable td
    {
        border: none;
        height: auto;
    }
    .attachButton span
    {
        border: 1px;
        clear: none;
    }
    
    .dxgvControl, .dxgvDisabled
    {
        border: none;
    }
    
    td.dxgv
    {
        border-width: 0px !important;
    }
</style>
<table border="0" cellspacing="0" cellpadding="0" style="border: 0; width: 100%;">
    <tr>
        <td style="border: 0;">
            <table border="0" class="attachmentTable" cellspacing="0" cellpadding="0" style="border: 0;
                width: 100%;">
                <tr>
                    <td style="border: 0;">
                        <asp:FileUpload ID="upFileUpload" runat="server" CssClass="mainText" />
                    </td>
                    <td style="border: 0;">
                        <asp:Button ID="btnUPload" runat="server" Text="上传" OnClick="UploadButton_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="border: 0; padding-left: 5px;" colspan="2">
                        <asp:GridView ID="gvAttachment" runat="server" AutoGenerateColumns="False" Width="100%"
                            ShowHeader="false">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href='<%#Eval("Url")%>' target="_blank" title=' <%#Eval("AttachmentName")%>'>
                                            <%#Eval("AttachmentName").ToString().Length > 51 ? Eval("AttachmentName").ToString().Substring(0, 50) + "..." : Eval("AttachmentName")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreateByUserName" ItemStyle-Width="8%" />
                                <asp:BoundField DataField="CreateAtTime" ItemStyle-Width="18%" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDeleteAttach" runat="server" CommandArgument='<%# Eval("AttachmentId") %>'
                                            Text='删除' OnCommand="lbDeleteAttach_Command" Visible='<%# (Eval("CreateByUserCode").ToString()==_BPMContext.CurrentPWordUser.EmployeeCode)&&(!string.IsNullOrEmpty(_BPMContext.Sn)||this.IsCanEdit) %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
