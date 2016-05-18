<%@ Page Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true" CodeFile="ProcessEndManage_E.aspx.cs"
    Inherits="Sys_ProcessEndManage_E" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CheckForm() {
            if ($('#<%=txtAddressToLink.ClientID%>').val() == "") {
                alert("“服务地址”不能为空！");
                $('#<%=txtAddressToLink.ClientID%>').focus();
                return false;
            }
            if ($('#<%=txtClassName.ClientID%>').val() == "") {
                alert("“表单名称”不能为空！");
                $('#<%=txtClassName.ClientID%>').focus();
                return false;
            }
        }  
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                流程结束管理</div>
            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            服务地址
                        </th>
                        <td>
                            <asp:TextBox ID="txtAddressToLink" runat="server" CssClass="txts" MaxLength="100"></asp:TextBox>
                            <span class="star">*</span>
                            </td>
                    </tr>
                    <tr>
                        <th>
                            表单名称
                        </th>
                        <td>
                            <asp:TextBox ID="txtClassName" runat="server" CssClass="txts" MaxLength="100"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" CssClass="green_btn" Text="保存" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnSave_Click"/>
                            &nbsp;
                            <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回" OnClick="btnReturn_Click"/>
                        </td>
                    </tr>
                    </tbody>
            </table>
        </div>
    </div>
</asp:Content>
