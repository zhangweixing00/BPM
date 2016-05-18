<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManage.aspx.cs" Inherits="Sohu.OA.Web.Manage.RoleManage.MenuManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        //重写window的alert方法
        window.alert = function (msg)
        {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        $(function ()
        {
            //绑定弹出添加菜单窗体
            $("#btnAddMenu").bind("click", function ()
            {
                var vurl = "Admin/Popup/MenuAdd.aspx?oper=add";
                top.window.ymPrompt.win(vurl, 820, 340, '添加菜单', null, null, null, true, null, null, true, false, true);
            });
            //绑定批量删除菜单方法
            $("#btnDelMenu").bind("click", function ()
            {
                var $chkMenu = $("#GridView1").find("#chkMenu");
                var menuguids = GetMenuGuids($chkMenu);
                $.ajax({
                    type: "POST",
                    url: "DeleteMenus.ashx",
                    data: { menuguids: menuguids },
                    success: function (data)
                    {

                        msg(data);
                    },
                    error: function (data)
                    {
                        msg(data);
                    }
                });
            });
        });
        //删除结果处理
        function msg(data)
        {

            if (data == "True")
            {
                top.window.ymPrompt.alert({
                    title: "删除角色",
                    message: "删除成功！",
                    handler: function ConFirm(tp)
                    {

                        if (tp == "ok")
                        {
                            $("#btnSeach").click();
                        }
                    }
                });
            }
            else if (data == "False")
            {
                top.window.ymPrompt.alert({ title: '系统提示', message: "删除失败！" });
                //alert("删除失败！");
            }
        }
        //弹出编辑菜单窗体
        function ShowEditMenu(obj)
        {
            var menuguid = $(obj).parent().find("#hfMenuEdit").val();
            var vurl = "Admin/Popup/MenuAdd.aspx?oper=edit&&menuguid=" + menuguid + "";
            top.window.ymPrompt.win(vurl, 820, 340, '修改菜单', null, null, null, true, null, null, true, false, true);
        }
        //跳转到菜单角色管理页面
        function ShowMenuRole(obj)
        {
            var menuguid = $(obj).parent().find("#hfMenuRole").val();
            var vurl = "Admin/Popup/MenuRoleView.aspx?menuguid=" + menuguid + "";
            top.window.ymPrompt.win(vurl, 660, 330, '查看菜单角色信息', null, null, null, true, null, null, true, false, true);
        }
        //检查是否选择了菜单项
        function IsRoleChecked(obj)
        {
            var checkflag = false;
            $(obj).each(function ()
            {

                if ($(this).is(":checked"))
                {
                    checkflag = true;
                    return false;
                }
            });
            return checkflag;
        }
        //获取选择的菜单项
        function GetMenuGuids(obj)
        {
            var DelMenuGuids = "";
            $(obj).each(function ()
            {
                if ($(this).is(":checked"))
                {
                    DelMenuGuids += $(this).parent().find("#hfMenuGuid").val() + ",";
                }
            });
            DelMenuGuids = DelMenuGuids.substring(0, DelMenuGuids.length - 1);
            return DelMenuGuids;
        }
        //确认删除方法
        function confirmmsg()
        {
            var $chkMenu = $("#GridView1").find("#chkMenu");
            if (IsRoleChecked($chkMenu))
            {
                top.window.ymPrompt.confirmInfo({
                    message: "确定要删除吗？",
                    title: '删除',
                    handler: function ConFirm(tp)
                    {
                        if (tp == "ok")
                        {
                            $("#btnDelMenu").click();
                        }
                    }
                });
            }
            else
            {
                top.window.ymPrompt.alert({ title: '提示信息', message: '请选择要删除的菜单！' });
            }
        }
        //验证输入page页码
        function CheckPageIndex()
        {
            var varPageAll = $("#lblPage").text();
            var varALL = varPageAll.split('/');
            var varone = varALL[1].replace('共', '');
            //总页码
            var vartwo = varone.replace('页', '');
            //页码索引
            var varPageIndex = $("#txt_PageIndex").val();

            var PageIndexText = varPageIndex.replace(/(^\s*)|(\s*$)/g, "");
            var re = /^[1-9]+[0-9]*]*$/;    //判断字符串是否为数字      //判断正整数 /^[1-9]+[0-9]*]*$/
            if (re.test(PageIndexText))
            {
                //没有中页码报错
                if (vartwo != "")
                {
                    if (varPageIndex == "" || parseInt(varPageIndex) <= 0)
                    {
                        top.window.ymPrompt.alert({ title: '分页', message: '只能输入正整数！' });
                        return false;
                    }
                    else if (parseInt(varPageIndex) > parseInt(vartwo))
                    {
                        top.window.ymPrompt.alert({ title: '分页', message: '超过最大页码了！' });
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    top.window.ymPrompt.alert({ title: '分页', message: '没有页码！' });
                    return false;
                }
            }
            else
            {
                top.window.ymPrompt.alert({ title: "分页", message: "只能输入正整数！" });
                return false;
            }
        }
        //验证必须为数字方法
        function MustNumber()
        {
            if (((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46))
            {
                event.returnValue = true;
            }
            else
            {
                event.returnValue = false;
            }
        }
    </script>
    <style type="text/css">
         .ddlcss
        {
            border: 1px #999999 solid;
        }
        <!--
.login,.login_form { padding:3px 0 0 10px;float:left;}
li { list-style:none; }
//-->
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container" style="width: 790px;">
        <div>
            <div class="nav">
                <p>
                    <%-- <uc1:Sitemap ID="Sitemap1" runat="server" />--%>
                    <uc1:Sitemap ID="Sitemap1" runat="server" />
                </p>
            </div>
        </div>
        <div class="nav_1">
            <p>
                菜单查询</p>
        </div>
        <div class="pro" style="margin: 10px 0; height: 60px;">
            <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
                <tr>
                    <td style="width: 100px; text-align: right;">
                        菜单名称：
                    </td>
                    <td style="width: 90px;">
                        <asp:TextBox ID="txtMenuName" runat="server" Style="padding-top: 4px; height: 18px;
                            border: 1px #999999 solid;"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right;">
                        上级菜单：
                    </td>
                    <td style="width: 90px;">
                        <asp:DropDownList ID="ddlParentMenu" runat="server" CssClass="ddlcss" Width="147px"
                            Height="22px">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 90px; text-align: right;">
                        菜单类型：&nbsp;
                    </td>
                    <td style="width: 90px;">
                        <asp:DropDownList ID="ddlMenutype" CssClass="ddlcss" runat="server" Width="147px"
                            Height="22px">
                            <asp:ListItem Value="">请选择</asp:ListItem>
                            <asp:ListItem Value="LEFT">导航菜单</asp:ListItem>
                            <asp:ListItem Value="RIGHT">页面菜单</asp:ListItem>
                        </asp:DropDownList>
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
                            <asp:ImageButton ID="btnSeach" runat="server" onmouseover="SaveMouseover('btnSeach','../../../pic/btnImg/btnQueryBit.png')"
                                onmouseout="SaveMouseout('btnSeach','../../../pic/btnImg/btnQueryBit_nor.png')"
                                ImageUrl="~/pic/btnImg/btnQueryBit_nor.png" Width="73px" OnClick="btnSeach_Click">
                            </asp:ImageButton>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Div_peolistName" class="nav_1">
            <p>
                菜单列表</p>
        </div>
        <div id="menu_List" class="pro_1" style="padding-bottom: 30px;">
            <table style="width: 765px;">
                <tr>
                    <td colspan="7" style="text-align: right; height: 30px;">
                        <asp:ImageButton ID="btnAddMenu" runat="server" onmouseover="SaveMouseover('btnAddMenu','../../../pic/btnImg/btnAddone.png')"
                            OnClientClick="return false;" onmouseout="SaveMouseout('btnAddMenu','../../../pic/btnImg/btnAddone_nor.png')"
                            ImageUrl="~/pic/btnImg/btnAddone_nor.png"></asp:ImageButton>
                        &nbsp;
                        <asp:ImageButton ID="btnDelMenu" runat="server" Style="display: none" onmouseover="SaveMouseover('btnDelMenu','../../../pic/btnImg/btnDeleteRole_over.png')"
                            onmouseout="SaveMouseout('btnDelMenu','../../../pic/btnImg/btnDeleteRole_nor.png')"
                            ImageUrl="~/pic/btnImg/btnDeleteRole_nor.png" OnClientClick="return false;">
                        </asp:ImageButton>
                        <asp:ImageButton ID="btndelInfo" runat="server" onmouseover="SaveMouseover('btndelInfo','../../../pic/btnImg/btnDelete_over.png')"
                            onmouseout="SaveMouseout('btndelInfo','../../../pic/btnImg/btnDelete_nor.png')"
                            ImageUrl="~/pic/btnImg/btnDelete_nor.png" OnClientClick="confirmmsg();return false;">
                        </asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="7">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" Width="765px" ClientIDMode="Static" AutoGenerateColumns="False"
                                    EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无菜单数据</div>"
                                    BorderWidth="1px" AllowPaging="True" CssClass="datalist2" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkMenu" runat="server" />
                                                <asp:HiddenField ID="hfMenuGuid" runat="server" Value='<%#Eval("MenuGuid") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="65px"></ItemStyle>
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField>
                                            <HeaderTemplate>
                                                排序号码
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="DisplayOrder" Text='<%# Eval("DisplayOrder") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                菜单名称
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="MenuName" Text='<%# Eval("MenuName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="165px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                菜单类型
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="MenuType" Text='<%# Eval("MenuType") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                上级菜单
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="ParentMenu" Text='<%# Eval("ParentMenuGuid") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                菜单URL
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Menu_Url" ClientIDMode="AutoID" Text='<%# Eval("MenuURL") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="165px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                操作
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbEdit" OnClientClick="ShowEditMenu(this);return false;"
                                                    Text="编辑"></asp:LinkButton>
                                                <asp:HiddenField ID="hfMenuEdit" Value='<%#Eval("MenuGuid") %>' runat="server" />
                                                &nbsp;
                                                <asp:LinkButton runat="server" ID="lbSelect" OnClientClick="ShowMenuRole(this);return false;"
                                                    Text="查看"></asp:LinkButton>
                                                <asp:HiddenField ID="hfMenuRole" Value='<%#Eval("MenuGuid") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerTemplate>
                                        <div style="text-align: right">
                                            <div class="login" style="width: 680px; height: 15px;">
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
                                    <HeaderStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <RowStyle BorderColor="#F2DD81" BorderWidth="1px" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#fef5c7" Font-Bold="True" ForeColor="Black" />
                                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSeach" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
