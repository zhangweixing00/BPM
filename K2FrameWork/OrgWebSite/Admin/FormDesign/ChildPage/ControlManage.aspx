<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlManage.aspx.cs"
    Inherits="OrgWebSite.Admin.FormDesign.ChildPage.ControlManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>控件库管理</title>
    <link href="../../../Styles/EasyUI/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/EasyUI/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadControlLibList();
        });
    </script>
    <script type="text/javascript">
        //编辑控件
        function editCL(Id) {
            window.location.href = 'AddControl.aspx?ID=' + Id + '&action=edit';
        }

        //删除控件
        function delCL(Id) {
            $.messager.confirm("提示", "您确定要执行删除操作吗？", function (data) {
                if (data) {
                    $.ajax({
                        type: "POST",
                        url: "../FormDesignAjaxHandler.ashx",
                        data: { action: "delCLByID", ID: Id },
                        async: false,
                        success: function (data) {
                            if (data == 'Success') {
                                $.messager.alert('提示', '删除成功');
                                LoadControlLibList();
                            }
                            else {
                                $.messager.alert('提示', '删除失败');
                            }
                        },
                        error: function () {
                            $.messager.alert('错误', '操作发生异常，请联系管理员!');
                        }
                    });
                }
            });
        }
        function LoadControlLibList() {
            $('#tblControlList').datagrid({
                title: '控件库',
                width: 600,
                height: 'auto',
                fitColumns: true,
                nowrap: false,
                rownumbers: true,
                showFooter: true,
                columns: [[
                    { field: 'JSON_id', title: 'ID', width: 100, hidden: true },
                    { field: 'JSON_name', title: 'Name', width: 100 },
                    { field: 'JSON_type', title: 'Type', width: 100 },
                    { field: 'JSON_class', title: 'Class', width: 100 },
                    { field: 'JSON_json', title: 'Json', width: 100, hidden: true },
                    { field: 'JSON_html', title: 'Html', width: 100, hidden: true },
                    { field: 'JSON_description', title: 'Description', width: 100 },
                    { field: 'JSON_createdon', title: 'CreatedOn', width: 100, hidden: true },
                    { field: 'JSON_createdby', title: 'CreatedBy', width: 100, hidden: true },
					{ field: 'opt', title: '操作', width: 100, align: 'center',
					    formatter: function (value, rec) {
					        return '<a style="color:red; cursor:pointer;" onclick="editCL(\'' + rec.JSON_id + '\');">编辑</a>&nbsp;&nbsp;<a style="color:red; cursor:pointer;" onclick="delCL(\'' + rec.JSON_id + '\')">删除</a>';
					    }
					}
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '添加控件',
                    iconCls: 'icon-add',
                    handler: function () {
                        window.location.href = 'AddControl.aspx?action=add';
                    }
                }]
            });
            $.ajax({
                type: "POST",
                url: "../FormDesignAjaxHandler.ashx",
                data: { action: "getCLDataSource" },
                async: false,
                success: function (data) {
                    if (data) {
                        var json = eval('(' + data + ')');
                        $("#tblControlList").datagrid("loadData", json);
                    }
                },
                error: function () {
                    $.messager.alert('错误', '数据加载错误，请联系管理员!');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblControlList">
        </table>
    </div>
    </form>
</body>
</html>
