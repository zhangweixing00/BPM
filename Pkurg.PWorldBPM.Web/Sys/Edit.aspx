<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="Edit.aspx.cs" Inherits="Sys_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CheckForm() {
            if ($('#<%=txtName.ClientID%>').val() == "") {
                alert("名称 不能为空！");
                $('#<%=txtName.ClientID%>').focus();
                return false;
            }
        }  
        
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                业态业种</div>
            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            名称：
                        </th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="green_btn" Text="保存" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
