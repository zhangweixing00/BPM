<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkplaceManage.aspx.cs" Inherits="OrgWebSite.Admin.WorkplaceManage" %>

<%@ Register src="../Process/Controls/Sitemap.ascx" tagname="Sitemap" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工作地点数据字典</title>
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../JavaScript/jquery-1.6.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/My97DatePicker/WdatePicker.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/DIVLayer/ymPrompt_Ex.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/Checkbox.js"></script>
    <script language="javascript" src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/Validate.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScript/ValidateNoMustInput.js"></script>
    <script language="javascript" type="text/javascript" src="../Javascript/Common.js"></script>
    <style type="text/css">
        .login, .login_form
        {
            padding: 3px 0 0 10px;
            float: left;
        }
        li
        {
            list-style: none;
        }
    </style>
    <style type="text/css">
        .cmdDiv a
        {
            text-decoration: none;
        }
        .queryTable input
        {
            height: 19px;
            vertical-align: middle;
            padding-top: 3px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CheckPageIndex() {
            var varPageAll = $("#lblPage").text();
            var varALL = varPageAll.split('/');
            var varone = varALL[1].replace('共', '');
            //总页码
            var vartwo = varone.replace('页', '');
            //页码索引
            var varPageIndex = $("#txt_PageIndex").val();
            //没有中页码报错
            if (vartwo != "") {
                if (varPageIndex == "" || parseInt(varPageIndex) <= 0) {
                    top.window.ymPrompt.alert({ title: '分页', message: '请输入大于零的页码！' });


                    return false;
                }
                else if (parseInt(varPageIndex) > parseInt(vartwo)) {

                    top.window.ymPrompt.alert({ title: '分页', message: '请输入页码不能大于总页数！' });

                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                top.window.ymPrompt.alert({ title: '分页', message: '没有页码！' });

                return false;
            }
        }
        function MustNumber() {
            if (((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46)) {
                event.returnValue = true;
            }
            else {
                event.returnValue = false;
            }
        }
        function AlertValue(id) {

            while ($("#" + id).val() == "") {

            }
            alert(id);
        }

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="hidThirdPartment" type="hidden" runat="server" value="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Debug">
    </asp:ScriptManager>
    <div id="container" style="width: 790px;">
        <div class="rightTop">
            <p>
                <uc1:Sitemap ID="Sitemap1" runat="server" />
            </p>
        </div>
        <div class="nav_1">
            <p>
                查询信息</p>
        </div>
        <div class="pro" style="margin: 10px 0; height: 65px;">
            <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
                <tr>
                    <td style="width: 100px; text-align: right;">
                        工作地点名称：
                    </td>
                    <td style="width: 90px;">
                        <asp:TextBox ID="txtFilter" runat="server" CssClass="tableInput" ToolTip="请输入中文名称或英文名称"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right;">
                    </td>
                    <td style="width: 90px;">
                    </td>
                    <td style="width: 90px; text-align: right;">
                        &nbsp;
                    </td>
                    <td style="width: 90px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                    <td align="right">
                        <div>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="/pic/btnImg/btnQueryBit_nor.png"
                                onmouseover="SaveMouseover('btnSearch','/pic/btnImg/btnQueryBit.png')" onmouseout="SaveMouseout('btnSearch','/pic/btnImg/btnQueryBit_nor.png')"
                                OnClick="btnSearch_Click" ClientIDMode="Static" Width="73px" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div_peolistName" class="nav_1">
            <p>
                工作地点列表列表</p>
        </div>
        <div id="menu_List" class="pro_1" style="padding-bottom: 30px; top: 5px;">
        <a id="userAction" href="#"></a>
            <table style="width: 765px;">
                <tr>
                    <td colspan="7" style="text-align: right; height: 30px;">
                        <input type="button" id="btnAdd" value="添加地点" onclick="UserAction('','new')" class="btnCommon" />
                        <asp:Button ID="btnDeleteUser" CssClass="btnCommon" runat="server" Text="删除地点" OnClick="btnDeleteUser_Click"
                            OnClientClick="return ValidateData();" />
                        <asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>
                        <asp:HiddenField runat="server" ID="hfSelectedUser" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteUser" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="gvWorkPlace" EventName="DataBinding" />
                                <asp:AsyncPostBackTrigger ControlID="gvWorkPlace" EventName="PageIndexChanging" />
                                <asp:AsyncPostBackTrigger ControlID="gvWorkPlace" EventName="RowUpdating" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView ID="gvWorkPlace" runat="server" DataKeyNames="ID" BorderColor="#F2DD81"
                                    BorderWidth="1px" Width="769px" CssClass="girdView" AutoGenerateColumns="False"
                                    AllowPaging="True"
                                    PageSize="20" 
                                    EmptyDataText="&lt;div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'&gt;无员工数据&lt;/div&gt;" 
                                    >
                                    <Columns>
                                        <asp:TemplateField ControlStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkUser" onclick="ChooseUser(this)" />
                                                <asp:HiddenField runat="server" ID="hfUserCode" Value='<%#Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ControlStyle Width="10px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="显示名称" ControlStyle-Width="120px">
                                            <ItemTemplate>
                                                <%#Eval("PlaceName") %>
                                            </ItemTemplate>
                                            <ControlStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="编码" ControlStyle-Width="120px">
                                            <ItemTemplate>
                                                <%#Eval("PlaceCode") %>
                                            </ItemTemplate>
                                            <ControlStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ControlStyle-Width="120px">
                                            <ItemTemplate>
                                                <a href="#" onclick='UserAction(&#039;<%#Eval("ID") %>&#039;,&#039;view&#039;)'>
                                                查看</a> <a href="#" 
                                                    onclick='UserAction(&#039;<%#Eval("ID") %>&#039;,&#039;edit&#039;)'>编辑</a>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                操作
                                            </HeaderTemplate>
                                            <ControlStyle BorderWidth="80px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        <div style="text-align: right">
                                            <div class="login" style="width: 700px;">
                                                <br />
                                                <asp:Label ID="lblRowAllCount" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> '></asp:Label>
                                                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                                                    CommandName="Page" CommandArgument="First"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'
                                                    CommandName="Page" CommandArgument="Prev"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                                                    CommandName="Page" CommandArgument="Next"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lbnLast" runat="Server" Text="末页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                                                    CommandName="Page" CommandArgument="Last"></asp:LinkButton>&nbsp;
                                                <asp:TextBox ID="txt_PageIndex" onkeypress="MustNumber()" runat="server" Width="30px"></asp:TextBox>
                                            </div>
                                            <div class="login_form" style="margin-top: 15px; width: 30px;">
                                                <asp:ImageButton ID="btn_go" onmouseover="SaveMouseover('btn_go','../../../pic/btnImg/BtnGoto_over.png')"
                                                    onmouseout="SaveMouseout('btn_go','../../../pic/btnImg/BtnGoto_nor.png')" runat="server"
                                                    CommandName="go" ImageUrl="~/pic/btnImg/BtnGoto_nor.png" OnClientClick="return CheckPageIndex()" />
                                            </div>
                                        </div>
                                    </PagerTemplate>
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <HeaderStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <RowStyle BorderColor="#F2DD81" BorderWidth="1px" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <script language="javascript" type="text/javascript">
            function UserAction(wpID, state) {
                debugger;
                var random = Math.round(Math.random() * 10000);
                var para = "?random=" + random + "&ID=" + wpID + "&state=" + state;
                var title = "";
                var url = "Popup/WorkPlaceEdit.aspx";
                switch (state) {
                    case "new": title = "添加工作地点"; break;
                    case "edit": title = "编辑工作地点"; break;
                    case "view": title = "查看工作地点"; break;
                    default: title = "工作地点"; break;
                }

                var userAction = $$("userAction");
                if (state == 'new')
                    userAction.href = url + '?state=new';
                else
                    userAction.href = url + para;
                userAction.click();
            }

            function ChooseUser(chk) {
                var hfUserCode = cibc(chk) + "hfUserCode";
                var vueSelectedUser = $$("hfSelectedUser").value;
                if (chk.checked) {
                    vueSelectedUser += $$(hfUserCode).value + ",";
                    $$("hfSelectedUser").value = vueSelectedUser;
                }
                else {
                    vueSelectedUser = vueSelectedUser.replace($$(hfUserCode).value + ",", "");
                    $$("hfSelectedUser").value = vueSelectedUser;
                }
            }

            function ValidateData() {
                var vueSelectUser = $$("hfSelectedUser").value;
                if (vueSelectUser == "") {
                    alert("请选择工作地点");
                    return false;
                }
                else {
                    top.window.ymPrompt.confirmInfo({
                        message: "确定要删除吗？",
                        title: '删除',
                        handler: function ConFirm(tp) {
                            if (tp == "ok") {
                                return true;
                            }
                            else {
                                return false;
                            }
                        }
                    });
                }
                return false;
            }
        </script>
    </div>
    </form>
</body>
</html>
