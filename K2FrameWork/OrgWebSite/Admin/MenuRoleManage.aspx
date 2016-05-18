<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuRoleManage.aspx.cs"
    Inherits="Sohu.OA.Web.MenuRoleManage" %>

<%@ Register Src="../Process/Controls/Sitemap.ascx" TagName="Sitemap" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../JavaScript/Checkbox.js" type="text/javascript"></script>
    <script src="../Javascript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function ()
        {
            //弹出添加角色
            $("#btnAddNewRole").bind("click", function ()
            {
                var vurl = "Admin/Popup/MenuRoleAdd.aspx?oper=add";
                top.window.ymPrompt.win(vurl, 820, 310, '添加角色', null, null, null, true, null, null, true, false, true);
            });
            //删除角色信息
            $("#btnDelRole").bind("click", function ()
            {
                var $chkRole = $("#GridView1").find("#chkRole");
                var roleids = GetDelRoleIds($chkRole);
                $.ajax({
                    type: "POST",
                    url: "DelRoles.ashx",
                    data: { roleids: roleids },
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
        //删除角色结果处理方法
        function msg(data)
        {
            if (data == "true")
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
            else
            {
                top.window.ymPrompt.alert({ title: "删除角色", message: "删除失败！" });
            }

        }
        //弹出编辑角色窗体方法
        function ShowEditRole(obj)
        {
            var roleid = $(obj).parent().find("#hidroleid1").val();
            var vurl = "Admin/Popup/MenuRoleAdd.aspx?oper=edit&&roleid=" + roleid + "";
            top.window.ymPrompt.win(vurl, 820, 310, '修改角色', null, null, null, true, null, null, true, false, true);
        }
        //检查是否勾选角色项
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
        //获取勾选的角色id信息
        function GetDelRoleIds(obj)
        {
            var DelRoleIds = "";
            $(obj).each(function ()
            {
                if ($(this).is(":checked"))
                {
                    DelRoleIds += $(this).parent().find("#hfRoleId").val() + ",";
                }
            });
            DelRoleIds = DelRoleIds.substring(0, DelRoleIds.length - 1);
            return DelRoleIds;
        }
        //验证输入页码
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
        //删除确认方法
        function confirmmsg()
        {
            var $chkRole = $("#GridView1").find("#chkRole");
            if (IsRoleChecked($chkRole))
            {
                var roleid = GetDelRoleIds($chkRole);
                //                if (roleid.split(',').length == 1)
                //                {
                //                    $.ajax({
                //                        type: "POST",
                //                        url: "IsRoleHasUser.ashx",
                //                        data: { roleid: roleid },
                //                        success: function (data)
                //                        {

                //                            if (data == "true")
                //                            {
                //                                top.window.ymPrompt.confirmInfo({
                //                                    message: "确定要删除这些角色以及相关人员吗？",
                //                                    title: '删除',
                //                                    handler: function ConFirm(tp)
                //                                    {
                //                                        if (tp == "ok")
                //                                        {
                //                                            $("#btnDelRole").click();
                //                                        }
                //                                    }
                //                                });
                //                            }
                //                            else
                //                            {
                //                                top.window.ymPrompt.confirmInfo({
                //                                    message: "确定要删除吗？",
                //                                    title: '删除',
                //                                    handler: function ConFirm(tp)
                //                                    {
                //                                        if (tp == "ok")
                //                                        {
                //                                            $("#btnDelRole").click();
                //                                        }
                //                                    }
                //                                });
                //                            }
                //                        },
                //                        error: function (data)
                //                        {
                //                            msg(data);
                //                        }
                //                    });
                //                }
                //                else
                {
                    top.window.ymPrompt.confirmInfo({
                        message: "确定要删除角色以及相关人员吗？",
                        title: '删除',
                        handler: function ConFirm(tp)
                        {
                            if (tp == "ok")
                            {
                                $("#btnDelRole").click();
                            }
                        }
                    });
                }
            }
            else
            {
                top.window.ymPrompt.alert({ title: '提示信息', message: '请选择要删除的角色！' });
            }
        }


    </script>
    <title></title>
    <style type="text/css">
        .ddlcss
        {
            border: 1px #999999 solid;
        }
        #role_oper a, #role_oper a:active
        {
            line-height: 2em;
            border: 1px solid #FFC125;
            text-decoration: none;
            margin-left: 15px;
            padding: 5px;
            color: #FF7F24;
        }
        #role_oper a:hover
        {
            line-height: 2em;
            border: 1px solid #FFF;
            text-decoration: none;
            margin-left: 15px;
            padding: 5px;
            background-color: #FFEC8B;
            color: #76650b;
        }
        
        .login, .login_form
        {
            padding: 3px 0 0 10px;
            float: left;
        }
        li
        {
            list-style: none;
        }
        
        .style1
        {
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container" style="width: 790px;">
        <div>
            <div class="nav">
                <p>
                    <uc1:Sitemap ID="Sitemap1" runat="server" />
                </p>
            </div>
        </div>
        <div class="nav_1" style="display: none;">
            <p>
                信息查询</p>
        </div>
        <div class="pro" style="margin: 10px 0; height: 60px; display: none;">
            <table class="datalist1" border="0" cellspacing="5" width="600px" align="right">
                <tr>
                    <td style="width: 100px; text-align: right;">
                        角色编码：
                    </td>
                    <td style="width: 90px;">
                        <asp:TextBox ID="txtRoleCode" runat="server" Style="padding-top: 4px; height: 18px;
                            border: 1px #999999 solid;"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right;">
                        角色名称：
                    </td>
                    <td style="width: 90px;">
                        <asp:TextBox ID="txtRoleName" runat="server" Style="padding-top: 4px; height: 18px;
                            border: 1px #999999 solid;"></asp:TextBox>
                    </td>
                    <td style="width: 90px; text-align: right;">
                        角色描述：&nbsp;
                    </td>
                    <td style="width: 90px;">
                        <asp:TextBox ID="txtDescription" runat="server" Style="padding-top: 4px; height: 18px;
                            border: 1px #999999 solid;"></asp:TextBox>
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
                角色列表</p>
        </div>
        <div id="role_List" class="pro_1" style="padding-bottom: 30px;">
            <table style="width: 765px;">
                <tr>
                    <td colspan="7" style="text-align: right; height: 30px;">
                        <asp:ImageButton ID="btnAddNewRole" runat="server" onmouseover="SaveMouseover('btnAddNewRole','../../../pic/btnImg/btnAddNewRole_over.png')"
                            OnClientClick="return false;" onmouseout="SaveMouseout('btnAddNewRole','../../../pic/btnImg/btnAddNewRole.png')"
                            ImageUrl="~/pic/btnImg/btnAddNewRole.png"></asp:ImageButton>
                        &nbsp;
                        <asp:ImageButton ID="btnDelRole" runat="server" Style="display: none" onmouseover="SaveMouseover('btnDelRole','../../../pic/btnImg/btnDeleteRole_over.png')"
                            onmouseout="SaveMouseout('btnDelRole','../../../pic/btnImg/btnDeleteRole_nor.png')"
                            ImageUrl="~/pic/btnImg/btnDeleteRole_nor.png" OnClientClick="return false;">
                        </asp:ImageButton>
                        <asp:ImageButton ID="btndelInfo" runat="server" onmouseover="SaveMouseover('btndelInfo','../../../pic/btnImg/btnDeleteRole_over.png')"
                            onmouseout="SaveMouseout('btndelInfo','../../../pic/btnImg/btnDeleteRole_nor.png')"
                            ImageUrl="~/pic/btnImg/btnDeleteRole_nor.png" OnClientClick="confirmmsg();return false;">
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
                                    EmptyDataText="<div style='height:20px;width:100%;text-align:center;background:#f0f0f0; line-height :20px;border-bottom:1px solid #999999'>无系统角色数据</div>"
                                    OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" BorderWidth="1px"
                                    AllowPaging="True" CssClass="datalist2" OnPageIndexChanging="GridView1_PageIndexChanging"
                                    OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRole" runat="server" />
                                                <asp:HiddenField ID="hfRoleId" runat="server" Value='<%#Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" Visible="False" />
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                角色编号
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="RoleCode" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                角色名称
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="RoleName" Text='<%# Eval("RoleName") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="155px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                角色说明
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="RoleTypeDes" Text='<%# Eval("Desciption") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="160px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                操作
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbEdit" OnClientClick="ShowEditRole(this);return false;"
                                                    Text="编辑"></asp:LinkButton>
                                                <asp:HiddenField ID="hidroleid1" Value='<%#Eval("ID") %>' runat="server" />
                                                &nbsp;
                                                <asp:LinkButton runat="server" ID="lbSelect" PostBackUrl='<%# String.Format("MenuRoleUserManage.aspx?rolecode={0}&PageIndex={1}",Eval("ID").ToString(),this.GridView1.PageIndex) %>'
                                                    Text="人员管理"></asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton runat="server" ID="LinkButton1" Text="添加菜单" PostBackUrl='<%# String.Format("RoleMenuManage.aspx?rolecode={0}&PageIndex={1}",Eval("ID").ToString(), this.GridView1.PageIndex) %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hidroleCode" Value='<%#Eval("ID") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" HorizontalAlign="Center"></ItemStyle>
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
