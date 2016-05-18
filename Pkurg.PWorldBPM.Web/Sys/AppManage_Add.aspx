<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="AppManage_Add.aspx.cs" Inherits="Sys_AppManage_Add" %>

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
                新增流程定义</div>
        </div>
        <div class="content">
            <table class="FormTable">
                <tr>
                    <th>
                        应用名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtAppName" CssClass="txt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        工作流名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtWorkFlowName" CssClass="txt" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        表单名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txtFormName" CssClass="txt" runat="server"></asp:TextBox>
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
                    <td>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="btnSubmit" runat="server" CssClass="green_btn" Text="提交"  OnClientClick="javascript:return CheckForm();"
                        OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回"  OnClick="btnReturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
    </div>
</asp:Content>
