<%@ Page Title="模拟用户" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function CheckForm() {
            if ($('#<%=txtUserCode.ClientID%>').val() == "") {
                alert("名称 不能为空！");
                $('#<%=txtUserCode.ClientID%>').focus();
                return false;
            }
        }  
        
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                模拟用户</div>
            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            登录名：
                        </th>
                        <td>
                            founder\<asp:TextBox ID="txtUserCode" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnConfirm" runat="server" CssClass="green_btn" Text="模拟用户" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="green_btn" Text="取消模拟" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
