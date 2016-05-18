<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="EditDataField.aspx.cs" Inherits="Sys_EditDataField" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function Close_Win() {
            window.opener = null;
            window.open('', '_self');
            window.close();
        }
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                1、强制终止
                <asp:Label ID="lblTitle3" runat="server"></asp:Label>
            </div>
            <div class="new">
                <asp:Label ID="lblMsg3" runat="server" ForeColor="Red"></asp:Label>
                <asp:LinkButton ID="lbtnStop" runat="server" Text="强制终止" OnClick="lbtnStop_Click"
                    OnClientClick="return confirm('确定要删除吗?');"></asp:LinkButton>
                <a href="javascript:void();" onclick="javascript:Close_Win();">关闭</a>
            </div>
        </div>
        <div class="content">
        </div>
    </div>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                2、修改审批人
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
            </div>
            <div class="new">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:LinkButton ID="lbtnSave" runat="server" Text="保存" OnClick="lbtnSave_Click"></asp:LinkButton>
                <a href="javascript:void();" onclick="javascript:Close_Win();">关闭</a>
                <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="content">
            <asp:Label ID="lblException" runat="server" Visible="false" ForeColor="Red"></asp:Label>
            <asp:Repeater ID="rptList" runat="server">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th style="width: 40px;">
                                Index
                            </th>
                            <th>
                                Name
                            </th>
                            <th>
                                Old Value
                            </th>
                            <th>
                                New Value
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblOldValue" runat="server" Text='<%#Eval("Value")%>'></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewValue" runat="server" CssClass="txt" Text='<%#Eval("Value")%>'
                                Width="400"></asp:TextBox>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                3、流程跳转
                <asp:Label ID="lblTitle2" runat="server"></asp:Label></div>
            <div class="new">
                <asp:Label ID="lblMsg2" runat="server" ForeColor="Red"></asp:Label>
                <asp:LinkButton ID="lbtnSave2" runat="server" Text="保存" OnClick="lbtnSave2_Click"></asp:LinkButton>
                <a href="javascript:void();" onclick="javascript:Close_Win();">关闭</a>
            </div>
        </div>
        <div class="content">
            <div class="selectOrderInfo">
                注：特殊的审批步骤(比如会签)不能跳转。
            </div>
            <asp:RadioButtonList ID="rbtnListSteps" runat="server" CssClass="List">
            </asp:RadioButtonList>
        </div>
    </div>
</asp:Content>
