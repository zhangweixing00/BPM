<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="TemplateOperation.aspx.cs"
    Inherits="Workflow_CustomWorkFlow_TemplateOperation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择流程模板</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <script src="../../Resource/jquery/jquery-1.8.0.min.js" type="text/javascript"></script>
    <link href="/Resource/css/Layout.css" rel="stylesheet" type="text/css" />
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
        <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //点击回车，自动查询
            $(document).keydown(function (e) {
                if (!e)
                    e = window.event;
                if ((e.keyCode || e.which) == 13) {
                    $('#<%=btnQuery.ClientID%>').click();
                }
            })
            $(".tab a").click(function () {
                alert($(this).attr("class"));
            });
        }); 
    </script>
    <style type="text/css">
        .style1
        {
            width: 59px;
        }
        
        #tab
        {
            overflow: hidden;
            zoom: 1;
            border: 0px solid #000;
            width: 300px;
            text-align: center;
        }
        #tab td
        {
            float: left;
            color: #fff;
            height: 30px;
            cursor: pointer;
            line-height: 30px;
            background-color: Gray;
            list-style-type: none;
        }
        #tab td.current
        {
            color: #000;
            background: #ccc;
        }
        #tab td a
        {
            color: White;
            width: 100%;
            height: 100%;
            font-weight: bold;
        }
        #tab td.Uncurrent
        {
        }
        #tab td:active
        {
            color: #000;
            background: #ccc;
        }
        .NoShow
        {
            width: 100%;
            height: 100% !important;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container" style="margin: 5px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
<%--                <div class="titlebg">
                    <div class="title">
                        选择流程模板</div>
                    <div style="float: right">
                        &nbsp;</div>
                </div>--%>
                <div class="content">
                <br />
                    <table class="FormTable">
                        <tbody>
                            <tr>
                                <th style="width: 100px;">
                                    模板名称：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbSearchName" runat="server" CssClass="txt" Width="100"></asp:TextBox>
                                </td>
                                <th class="style1">
                                    创建日期：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDate" runat="server" CssClass="txt" Width="100" onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <%--green_btn--%>
                                    <asp:Button ID="btnQuery" runat="server" Text=" 查询 " CssClass="green_btn2" OnClick="btnQuery_Click" />
                                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="gray_btn2" OnClientClick="javascript:window.returnValue=2;window.close();"
                                        Text=" 关闭 " />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table id="tab">
                        <tr>
                            <td class="current" onclick="this.style.backgroundColor;this.style.backgroundColor='#F3F3F3';">
                                <asp:Button ID="lbtnCommon" runat="server" CssClass="tabActive" OnClick="lbtnCommon_Click"
                                    Text="公有流程" />
                                <%--                                <asp:LinkButton ID="lbtnCommon" runat="server" CssClass="NoShow" 
                                    onclick="lbtnCommon_Click">公有流程</asp:LinkButton>--%>
                            </td>
                            <td>
                                <asp:Button ID="lbtnSelf" runat="server" CssClass="tabUnActive" OnClick="lbtnSelf_Click"
                                    Text="自有流程" />
                                <%--                                <asp:LinkButton ID="lbtnSelf" runat="server" CssClass="NoShow" 
                                    onclick="lbtnSelf_Click">自有流程</asp:LinkButton>--%>
                            </td>
                        </tr>
                    </table>
                    <%--                    <ul>
                        <li >公有流程</li>
                        <li>自有流程<input class='Uncurrent' type="button" /></li>
                    </ul>--%>
                    <div class="content">
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" CssClass="List"
                            AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" PagerSettings-FirstPageText="首页"
                            PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="尾页" PagerStyle-CssClass="anpager"
                            PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" 
                            >
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        操作
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbSelected" runat="server" CommandArgument='<%#Eval("Id") %>'
                                            OnCommand="lbSelected_Command" Text="选择"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="模板名称" />
                                <asp:BoundField DataField="CreateUserName" HeaderText="创建人" 
                                    ItemStyle-Width="150px">
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreateTime" HeaderText="创建日期" 
                                    ItemStyle-Width="150px" >
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        删除
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbDel" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            Text="删除" oncommand="lbDel_Command" 
                                            Visible='<%# LinkButtonVisible%>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div style="width:100%; text-align:center; height:100px; vertical-align:middle; ">没有相关模板！</div>
                            </EmptyDataTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                            <PagerStyle CssClass="anpager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
                <div>
                </div>
                <input id="hd_SelectType" type="hidden" runat="server" value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
