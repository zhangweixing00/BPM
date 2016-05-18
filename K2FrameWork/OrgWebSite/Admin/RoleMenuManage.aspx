<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleMenuManage.aspx.cs"
    Inherits="Sohu.OA.Web.Manage.RoleManage.RoleMenuManage" ViewStateMode="Enabled" %>

<%@ Register Src="UC/UC_RoleInfo.ascx" TagName="UC_RoleInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScript/jquery-1.6.1.min.js"></script>
    <script type="text/javascript" src="../JavaScript/jquery.query-2.1.7.js"></script>
    <script type="text/javascript">
        window.alert = function (msg)
        {
            top.window.ymPrompt.alert({ title: '提示', message: msg })
        }
        function AlertAndNewLoad(msg)
        {
            top.window.ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); window.location.href = '/Manage/RoleManage/MenuRoleManage.aspx'; } } });
        }

        $(function ()
        {
            $("#trMenu :checkbox").click(function ()
            {
                var $aChild = $(this).siblings("a");
                if ($aChild.length > 0)
                {
                    var id = $($aChild.get(0)).attr("id");
                    setInput(id, $(this).attr("checked") ? true : false);
                }
            });
        });

        function setInput(id, isSelect)
        {
            id = "#" + id.replace("trMenut", "trMenun") + "Nodes";
            $(id + " > table :checkbox").each(function ()
            {
                $(this).attr("checked", isSelect);
                var $aChild = $(this).siblings("a");
                if ($aChild.length > 0)
                {
                    var id = $($aChild.get(0)).attr("id");
                    setInput(id, isSelect);
                }
            });
        }
    </script>
    <title>给角色添加菜单</title>
    <style type="text/css">
        .selectedNodeStyle
        {
            background-color: #efb303;
            border-color: #888888;
            border-style: solid;
            border-width: 1px;
            padding: 1px 1px 3px 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:UC_RoleInfo ID="UC_RoleInfo1" runat="server" />
    <div style="padding-left: 18px; margin-top:6px;">
        <div class="rightTitle">
            <span>菜单信息</span></div>
        <table style="width: 769px;">
            <tbody>
                <tr>
                    <td class=" text">
                        &nbsp;
                    </td>
                    <td colspan="5" style="text-align: right;">
                        &nbsp;<asp:ImageButton ID="ibtAdd" ClientIDMode="Static" runat="server" ImageUrl="~/pic/btnImg/save_nor.png"
                            onmouseover="this.src='/pic/btnImg/btnsave_over.png'" onmouseout="this.src='/pic/btnImg/save_nor.png'"
                            OnClick="bnSave_Click" />
                        <asp:ImageButton ID="btnReturnList" runat="server" onmouseover="this.src='/pic/btnImg/btnBackList_over.png'"
                            onmouseout="this.src='/pic/btnImg/btnBackList_nor.png'" OnClientClick="javascript: window.location.href='/Admin/MenuRoleManage.aspx?PageIndex='+$.query.get('PageIndex');;window.close();return false;"
                            ImageUrl="~/pic/btnImg/btnBackList_nor.png" Style="margin-left: 15px;" />
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="margin-left: 13px; text-align: left;">
            <asp:TreeView ID="trMenu" runat="server" ShowCheckBoxes="All" />
        </div>
    </div>
    <asp:HiddenField ID="hfRoleCode" runat="server" />
    </form>
</body>
</html>
