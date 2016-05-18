<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_RoleUser.ascx.cs"
    Inherits="Sohu.OA.Web.Manage.RoleManage.UC.UC_RoleUser" %>
<%@ Register Src="UC_RoleInfo.ascx" TagName="UC_RoleInfo" TagPrefix="uc1" %>
<link href="../Styles/css.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../JavaScript/jquery-1.6.1.min.js"></script>
<script type="text/javascript" src="../JavaScript/jquery.query-2.1.7.js"></script>
<script src="../JavaScript/CheckBox.js" type="text/javascript"></script>
<script src="../JavaScript/DivWaitMsg.js" type="text/javascript"></script>
<script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
<link href="../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
<script src="../JavaScript/Validate1.3.js" type="text/javascript"></script>
<style type="text/css">
    body
    {
        font-size: 12px;
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
</style>
<style type="text/css">
    .text
    {
        text-align: right;
        height: 30px;
        width: 110px;
    }
    .text2
    {
        text-align: left;
        width: 120px;
    }
    .bordercss
    {
        width: 110px;
        border-bottom: 1px solid #333333;
        padding-bottom: 2px;
    }
    .roleUserList
    {
        text-align: right;
    }
    .roleUserList td
    {
        text-align: right;
    }
    .displayNone
    {
        display: none;
    }
</style>
<title></title>
<script language="javascript" type="text/javascript">
    function UserAction()
    {
        var random = Math.round(Math.random() * 10000);
        var para = "?checkstyle=true&random=" + random;
        top.ymPrompt.win({ message: '../Search/K2FlowCheck/K2FlowCheck.aspx' + para, width: 760, height: 560, title: "人员选择", handler: TrueInfos, iframe: true, titleBar: true });
    }
    function TrueInfos(retValue)
    {
        switch (retValue)
        {
            case "close": top.ymPrompt.close(); break;
            case "cancel": top.ymPrompt.close(); break;
            default:
                if (retValue && retValue.length > 0)
                {
                    document.getElementById('UC_RoleUser1_hfSelectUserAD').value = retValue[0].split(';')[1];
                    document.getElementById('ibtAdd').click();

                }
                top.ymPrompt.close();
                break;
        }
    }
</script>
<script type="text/javascript">
    //    var waitObj = new WaitMsg("请稍后......", 45);
    //    function WaitBegin()
    //    {
    //        //  waitObj.begin();
    //        if ($(":checked").length < 1)
    //        {
    //            alert("请选择要删除的角色人员！");

    //            return false;
    //        }
    //    }
    //    function WaitEnd()
    //    {
    //        waitObj = new WaitMsg("请稍后......", 45);
    //        waitObj.end();
    //    }
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
    window.alert = function (msg)
    {
        top.window.ymPrompt.alert({ title: '提示', message: msg })
    }
    //    $(function ()
    //    {
    //        $("#btnAdd").bind("click", CheckDeptAccess); //部门助理
    //        $("#<%=this.ImageButton1.ClientID %>").bind("click", WaitBegin);
    //    })
    //    function CheckDeptAccess()
    //    {
    //        var varmail = $("#DeptAccessName").val();
    //        var varcode = $("#DeptAccessCode").val();
    //        top.window.EmployeesSearch($("#DeptAccessName"), $("#DeptAccessCode"), $("#ibtAdd"), varmail, varcode);
    //    }

    //添加用户
    //    function UserAction()
    //    {
    //        var random = Math.round(Math.random() * 10000);
    //        var para = "random=" + random;
    //        var retValue = window.showModalDialog("Popup/SelectSingleUser.aspx?" + para, window, 'dialogHeight: 500px; scroll:yes; dialogWidth: 600px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
    //        if (typeof (retValue) != "undefined")
    //        {
    //            if (retValue.split(';')[2])
    //            {
    //                document.getElementById('UC_RoleUser1_hfSelectUserAD').value = retValue.split(';')[2];
    //                document.getElementById('ibtAdd').click();
    //            }
    //        }
    //    }

    function CheckPageIndex()
    {
        var varPageAll = $("#lblPage").text();
        var varALL = varPageAll.split('/');
        var varone = varALL[1].replace('共', '');
        //总页码
        var vartwo = varone.replace('页', '');
        //页码索引
        var varPageIndex = $("#txt_PageIndex").val();
        //没有中页码报错
        if (vartwo != "")
        {
            if (varPageIndex == "" || parseInt(varPageIndex) <= 0)
            {
                alert({ title: '分页', message: '请输入大于零的页码！' });


                return false;
            }
            else if (parseInt(varPageIndex) > parseInt(vartwo))
            {

                alert({ title: '分页', message: '请输入页码不能大于总页数！' });

                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            alert({ title: '分页', message: '没有页码！' });

            return false;
        }
    }

    function BackToRole()
    {
        var path = window.location.href;
        if (path.indexOf("MenuRoleUserManage.aspx") > -1)
        {
            window.location.href = "/Admin/MenuRoleManage.aspx?PageIndex=" + $.query.get("PageIndex");
        } else
        {
            window.location.href = "/Admin/RoleManage.aspx?PageIndex=" + $.query.get("PageIndex");
        }

    }
       
</script>
<uc1:UC_RoleInfo ID="UC_RoleInfo1" runat="server" />
<div style="padding-left: 18px;">
    <div class="rightTitle">
        <span>角色人员信息</span></div>
    <div id="Div1" runat="server" class="taf_emailInfo" style="width: 769px;">
        <table style="width: 769px;">
            <tbody>
                <tr class="displayNone">
                    <td class="text">
                        选择人员：
                    </td>
                    <td colspan="4">
                        <%-- <input id="DeptAccessName" clientidmode="Static" runat="server" class="inputcss" regex="mustinput:部门助理不能为空;" readonly="readonly"
                                            style=" width:100%; font-size:13px; height:19px; padding-top:3px; cursor: hand; background: url(/pic/menu1.png) no-repeat right; min-width:200px;" title="请点击文本框查询部门助理"  />
                        --%>
                        <textarea id="DeptAccessName" class="inputcss mul" rows="3" style="width: 100%;"
                            cols="1" runat="server" readonly="readonly" title="请点击文本框查询部门助理" clientidmode="Static"></textarea>
                        <input id="DeptAccessCode" clientidmode="Static" type="hidden" runat="server" value="" />
                    </td>
                    <td class=" text">
                    </td>
                </tr>
                <tr>
                    <td class=" text">
                        <asp:ImageButton ID="ibtAdd" ClientIDMode="Static" Style="display: none;" runat="server"
                            ImageUrl="~/pic/btnImg/save_nor.png" onmouseover="this.src='/pic/btnImg/btnsave_over.png'"
                            onmouseout="this.src='/pic/btnImg/save_nor.png'" OnClick="bnSave_Click" />
                    </td>
                    <td colspan="5" style="text-align: right;">
                        <img id="btnAdd" alt="添加人员" src="/pic/btnimg/btnAddone_nor.png" onmouseover="this.src='/pic/btnImg/btnAddone.png'"
                            onmouseout="this.src='/pic/btnImg/btnAddone_nor.png'" onclick="UserAction(); return false;"
                            style="cursor: pointer;" />
                        <asp:HiddenField ID="hfSelectUserAD" runat="server" />
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/pic/btnImg/btnDelete_nor.png"
                            onmouseover="this.src='/pic/btnImg/btnDelete_over.png'" onmouseout="this.src='/pic/btnImg/btnDelete_nor.png'"
                            OnClick="bnDelete_Click" regexbutton="confirm:你确定删除吗？;" Style="margin-left: 15px;" />
                        <asp:ImageButton ID="btnReturnList" runat="server" onmouseover="this.src='/pic/btnImg/btnBackList_over.png'"
                            onmouseout="this.src='/pic/btnImg/btnBackList_nor.png'" OnClientClick="javascript: BackToRole();return false;"
                            ImageUrl="~/pic/btnImg/btnBackList_nor.png" Style="margin-left: 15px;" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div style="padding: 5px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="#F2DD81"
            ClientIDMode="Static" BorderWidth="1px" CssClass="girdView" AllowPaging="True"
            DataKeyNames="ID" Width="765px" OnPageIndexChanging="GridView1_PageIndexChanging"
            ViewStateMode="Enabled" EmptyDataText="当前角色不包含员工" PageSize="50">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input id="ckb_All" clientidmode="Static" onclick="CheckAll()" type="checkbox" runat="server" />
                    </HeaderTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="ckSelect" runat="server" />
                    </ItemTemplate>
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        员工</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="EmployeeName" runat="server" Text='<%# Eval("CHName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        AD帐户</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ADAccount" runat="server" Text='<%# Eval("AD") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        部门</HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("DeptName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--       <asp:TemplateField>
                        <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" ClientIDMode="AutoID" runat="server" ImageUrl="~/pic/btnImg/btnDelete_nor.png"  
                                onmouseover="this.src='/pic/btnImg/btnDelete_over.png'" onmouseout="this.src='/pic/btnImg/btnDelete_nor.png'"
                                 CommandArgument='<%#Eval("RoleUserID") %>'
                                regexbutton="confirm:你确定删除吗？;"
                                 CommandName="Add" /> 
                           
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            </Columns>
            <PagerTemplate>
                <div style="text-align: right">
                    <div class="login" style="width: 504px;">
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
                        <asp:ImageButton ID="btn_go" runat="server" CommandName="go" ImageUrl="~/pic/btnImg/BtnGoto_nor.png"
                            OnClientClick="return CheckPageIndex()" onmouseout="this.src='/pic/btnImg/BtnGoto_nor.png'"
                            onmouseover="this.src='/pic/btnImg/BtnGoto_over.png'" />
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
        <br />
    </div>
</div>
<asp:Label ID="hfRoleCode" runat="server" Visible="False"></asp:Label>