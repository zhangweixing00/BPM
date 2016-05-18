<%@ Page Title="切换用户" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="Switch.aspx.cs" Inherits="Switch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .remark
        {
            background: none repeat scroll 0 0 #fff4be;
            border: 1px solid #cccccc;
            color: #e66e1e;
            font-size: 14px;
            height: 26px;
            line-height: 26px;
            overflow: hidden;
            padding: 0 10px;
            margin: 5px 0;
            text-align: left;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">

        function CheckForm() {
            if ($('#<%=txtUserCode.ClientID%>').val() == "") {
                alert("域账号 不能为空！");
                $('#<%=txtUserCode.ClientID%>').focus();
                return false;
            }
            if ($('#<%=txtPwd.ClientID%>').val() == "") {
                alert("密码 不能为空！");
                $('#<%=txtPwd.ClientID%>').focus();
                return false;
            }
        }  
        
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                切换用户</div>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            域账号：
                        </th>
                        <td>
                            founder\
                            <asp:TextBox ID="txtUserCode" runat="server" CssClass="txt" MaxLength="50" Width="195"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密码:
                        </th>
                        <td>
                            <asp:TextBox ID="txtPwd" runat="server" CssClass="txt" MaxLength="50" TextMode="Password"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnConfirm" runat="server" CssClass="green_btn" Text="确定" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnConfirm_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="green_btn" Text="取消" OnClick="btnCancel_Click" />
                            <span style="margin-right: 10px;"></span>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <%--<div class="remark">
                注：切换用户成功以后，请记得点击“退出”登录。
            </div>--%>
        </div>
    </div>
</asp:Content>
