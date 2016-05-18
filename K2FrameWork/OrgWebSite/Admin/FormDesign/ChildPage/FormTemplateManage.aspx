<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTemplateManage.aspx.cs"
    Inherits="OrgWebSite.Admin.FormDesign.ChildPage.FormTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单模板</title>
    <link href="../../../Styles/EasyUI/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/EasyUI/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadFromTemplateLibList();
        });
    </script>
    <script type="text/javascript">

        //编辑模板库
        function editFL(Id) {
            window.location.href = 'AddFormTemplateLibrary.aspx?ID=' + Id + '&action=edit';
        }

        //删除模板库
        function delFL(Id) {
            $.messager.confirm("提示", "您确定要执行删除操作吗？", function (data) {
                if (data) {
                    $.ajax({
                        type: "POST",
                        url: "../FormDesignAjaxHandler.ashx",
                        data: { action: "delFLByID", ID: Id },
                        async: false,
                        success: function (data) {
                            if (data == 'Success') {
                                $.messager.alert('提示', '删除成功');
                                LoadFromTemplateLibList();
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

        //编辑模板库内容
        function editFLContent(Id) {
            window.location.href = 'FormTemplateControl.aspx?ID=' + Id + '&action=EditFormTemplateControl';
        }

        function LoadFromTemplateLibList() {
            $('#tblFormTemplateList').datagrid({
                title: '表单模板库',
                width: 600,
                height: 'auto',
                fitColumns: true,
                nowrap: false,
                rownumbers: true,
                showFooter: true,
                columns: [[
                    { field: 'JSON_id', title: 'ID', width: 100, hidden: true },
                    { field: 'JSON_name', title: '表单模板名称', width: 100 },
                    { field: 'JSON_version', title: '版本号', width: 80 },
                    { field: 'JSON_description', title: '描述', width: 180 },
                    { field: 'JSON_createdon', title: 'CreatedOn', width: 100, hidden: true },
                    { field: 'JSON_createdby', title: 'CreatedBy', width: 100, hidden: true },
					{ field: 'opt', title: '操作', width: 100, align: 'center',
					    formatter: function (value, rec) {
					        return '<a style="color:red; cursor:pointer;" onclick="editFL(\'' + rec.JSON_id + '\');">编辑</a>&nbsp;&nbsp;<a style="color:red; cursor:pointer;" onclick="delFL(\'' + rec.JSON_id + '\')">删除</a>&nbsp;&nbsp;<a style="color:red; cursor:pointer;" onclick="editFLContent(\'' + rec.JSON_id + '\')">编辑内容</a>';
					    }
					}
				]],
                pagination: true,
                rownumbers: true,
                toolbar: [{
                    id: 'btnadd',
                    text: '添加模板',
                    iconCls: 'icon-add',
                    handler: function () {
                        window.location.href = 'AddFormTemplateLibrary.aspx?action=add';
                    }
                }]
            });
            $.ajax({
                type: "POST",
                url: "../FormDesignAjaxHandler.ashx",
                data: { action: "getFLDataSource" },
                async: false,
                success: function (data) {
                    if (data) {
                        var json = eval('(' + data + ')');
                        $("#tblFormTemplateList").datagrid("loadData", json);
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
        <table id="tblFormTemplateList">
        </table>
    </div>
    </form>
</body>
</html>
