<%@ Page Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true" CodeFile="AppManage_Edit.aspx.cs" Inherits="Sys_AppManage_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CheckForm() {
            if ($('#<%=txtAppName.ClientID%>').val() == "") {
                alert("“应用名称”不能为空！");
                $('#<%=txtAppName.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtWorkFlowName.ClientID%>').val() == "") {
                alert("“工作流名称”不能为空！");
                $('#<%=txtWorkFlowName.ClientID%>').focus();
                return false;
            }

            if ($('#<%=txtFormName.ClientID%>').val() == "") {
                alert("“表单名称”不能为空！");
                $('#<%=txtFormName.ClientID%>').focus();
                return false;
            }

        }  
        
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                应用编辑</div>
            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            应用名称
                        </th>
                        <td>
                            <asp:TextBox ID="txtAppName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                            <asp:HiddenField ID="hideAppName" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            工作流名称</th>
                        <td>
                            <asp:TextBox ID="txtWorkFlowName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            表单名称
                        </th>
                        <td>
                            <asp:TextBox ID="txtFormName" runat="server" CssClass="txt" MaxLength="50"></asp:TextBox>
                            <span class="star">*</span>
                            </td>
                    </tr>
                    <tr>
                        <th>
                            是否开放实例管理
                        </th>
                        <td>
                            <asp:CheckBox ID="cbIsOpen" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="green_btn" Text="保存" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnSave_Click" />
                                
                            &nbsp;
                                
                            <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
