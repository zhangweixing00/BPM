<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="AppManage_List.aspx.cs" Inherits="Sys_AppManage_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="titlebg">
            <div class="title">
                应用管理</div>
            <div class="new">
                <asp:LinkButton ID="lbtnAdd" runat="server" Text="新建" OnClick="lbtnAdd_Click"></asp:LinkButton>
            </div>
            查询：
            <input type="text" id="txtAppID" name="txtAppID" runat="server" value="应用号" onfocus="if(value==defaultValue){value='';this.style.color='#000'}"
                onblur="if(!value){value=defaultValue;this.style.color='#999'}" style="color: #999999" />
            <input type="text" id="txtAppName" name="txtAppName" runat="server" value="应用名称"
                onfocus="if(value==defaultValue){value='';this.style.color='#000'}" onblur="if(!value){value=defaultValue;this.style.color='#999'}"
                style="color: #999999" />
            <input type="text" id="txtWorkFlow" name="txtWorkFlow" runat="server" value="工作流名称"
                onfocus="if(value==defaultValue){value='';this.style.color='#000'}" onblur="if(!value){value=defaultValue;this.style.color='#999'}"
                style="color: #999999" />
            <input type="text" id="txtFormName" name="txtFormName" runat="server" value="表单名称"
                onfocus="if(value==defaultValue){value='';this.style.color='#000'}" onblur="if(!value){value=defaultValue;this.style.color='#999'}"
                style="color: #999999" />
            <asp:Button ID="btnStart" runat="server" CssClass="green_btn" Text="搜索" OnClick="btnStart_Click" />
        </div>
        <div class="content" style="height: 500px; overflow: scroll">
            <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                <HeaderTemplate>
                    <table class="List">
                        <tr>
                            <th>
                                应用号
                            </th>
                            <th>
                                应用名称
                            </th>
                            <th>
                                工作流名称
                            </th>
                            <th>
                                表单名称
                            </th>
                            <th>
                                实例管理是否开放
                            </th>
                            <th>
                                操作
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr onmouseover="c=this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';"
                        onmouseout="this.style.backgroundColor=c;">
                        <td>
                            <%#Eval("AppId")%>
                        </td>
                        <td>
                            <%#Eval("AppName")%>
                        </td>
                        <td>
                            <%#Eval("WorkFlowName")%>
                        </td>
                        <td>
                            <%#Eval("FormName")%>
                        </td>
                        <td>
                            <%#Eval("IsOpen").ToString()=="1"?"开放":"关闭"%>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("AppId") %>'
                                CausesValidation="false" Text="编辑"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("AppId") %>'
                                OnClientClick="return confirm('确定要删除吗?');" CausesValidation="false" Text="删除"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
