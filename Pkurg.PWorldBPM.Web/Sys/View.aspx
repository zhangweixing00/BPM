<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="View.aspx.cs" Inherits="Sys_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            备注：
                        </th>
                        <td>
                            <asp:Label ID="lblRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
