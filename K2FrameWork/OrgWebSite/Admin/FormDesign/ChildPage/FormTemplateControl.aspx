<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTemplateControl.aspx.cs"
    Inherits="OrgWebSite.Admin.FormDesign.ChildPage.FormTemplateControl" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../Styles/EasyUI/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/EasyUI/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <style type="text/css">
        .layout-panel
        {
            position: static;
            overflow: hidden;
            float: left;
        }
        .controlstyle
        {
            width: 100%;
            border: 0px solid #efefef;
            border-bottom: transparent 1px solid;
            border-top: transparent 1px solid;
            border-left: transparent 1px solid;
            border-right: transparent 1px solid;
            line-height: 34px;
            padding: 1px 6px 1px 10px;
            background: none transparent scroll repeat 0% 0%;
            color: #444;
            cursor: default;
            display: inline-block;
            font-size: 12px;
            height: 18px;
            outline-color: invert;
            outline-style: none;
            outline-width: medium;
            text-decoration: none;
            zoom: 1;
        }
        .easyui-layout .panel-body-noheader
        {
            border: none;
        }
        .easyui-layout .datagrid-body table
        {
            margin-left: 8px;
        }
        .easyui-layout .datagrid-header
        {
            border: none;
        }
        .easyui-layout .datagrid-view2
        {
            position: static;
        }
        .easyui-layout .datagrid-view
        {
            position: static;
        }
        .easyui-layout .datagrid-wrap
        {
            position: static;
        }
        .drag
        {
            height: 30px;
            display: block;
        }
    </style>
    <script type="text/javascript">
        //返回
        function S4()
        {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
        function NewGuid()
        {
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }
        function retBack()
        {
            window.location.href = "FormTemplateManage.aspx";
        }

        function OpenTable()
        {
            $('#divTable').window('open');
        }
        function CloseTable()
        {
            $('#divTable').window('close');
        }
        function CreateTable()
        {
            var rowCount = $("#tablerow").val();
            var Count = $("#talbecol").val();

            var table = $("<table border='1' style='width:100%;'></table>");
            for (var i = 0; i < rowCount; i++)
            {
                var tr = $("<tr></tr>");
                tr.appendTo(table);
                for (var j = 0; j < Count; j++)
                {
                    var td = $("<td class='drag' id='cell" + NewGuid() + "'></td>");
                    td.appendTo(tr);
                }
            }
            $("#createtable").append(table);

            CloseTable();

            table.draggable({
                revert: true,
                proxy: 'clone',
                onBeforeDrag: function (e) { },
                onStartDrag: function (e) { },
                onDrag: function (e) { },
                onBeforeDrag: function (e) { }
            });

            $('.drag').droppable({
                accept: '.datagrid-body tr',
                onDrop: function (e, source)
                {
                    if (source.id != "atable")
                    {
                        $("#hfTargetCell").val("#" + $(this).attr("id"));
                        //                        $(this).html($(source).html());

                        //                        $(source).find("div").draggable({
                        //                            revert: true,
                        //                            proxy: 'clone'
                        //                        });
                        OpenPropertygrid(source);
                    }
                }
            });
        }
        function OpenPropertygrid(source)
        {
            $("#tpropertygrid").propertygrid({
                cache: false,
                width: 400,
                height: "auto",
                showGroup: true,
                groupField: "group",
                scrollbarSize: 0,
                columns: [[
                                    { field: "name", title: "属性名称", width: 100 },
                                    { field: "value", title: "属性值", width: 100 },
                                    { field: "type", title: "属性类型", width: 100, hidden: true }
                                 ]]
            });

            $("#hfControlID").val($(source).find("td[field='JSON_id'] div").html());
            $("#hfControlName").val($(source).find("td[field='JSON_name'] div").html());
            $("#hfControlType").val($(source).find("td[field='JSON_type'] div").html());
            $("#hfControlClass").val($(source).find("td[field='JSON_class'] div").html());
            $("#hfControlHtml").val($(source).find("td[field='JSON_html'] div").html());

            var json = $(source).find("td[field='JSON_json'] div").html();
            $("#tpropertygrid").propertygrid("loadData", eval('(' + json + ')'));
            $('#divpropertygrid').window('open');
        }
        function SavePropertygrid()
        {
            var s = '';
            var attr = "";
            var rows = $('#tpropertygrid').propertygrid('getRows');
            for (var i = 0; i < rows.length; i++)
            {
                s += rows[i].name + '|' + rows[i].value + '|' + rows[i].type + ';';
                if (!(rows[i].type == "bool" && rows[i].value == "false"))
                {
                    attr += " " + rows[i].name + "='" + rows[i].value + "' ";
                }
            }
            ClosePropertygrid();
            $($("#hfTargetCell").val()).html($("#hfControlHtml").val().replace("[attr]", attr));

        }
        function Save()
        {
            $.ajax({
                type: "POST",
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                url: "../FormDesignAjaxHandler.ashx",
                data:
                                {
                                    action: "EditFormTemplateControl",
                                    html: $("#createtable").html(),
                                    formTemplateID: $("#hfFormTemplateID").val()
                                },
                async: true,
                cache: false,
                success: function (data)
                {
                    if (data == "Success")
                    {
                        $.messager.alert('', "保存成功");
                    }
                    else
                    {
                        $.messager.alert('错误', "保存失败", 'error');
                    }
                },
                error: function ()
                {
                    $.messager.alert('错误', "Ajax 错误", 'error');
                }
            });
        }

        function ClosePropertygrid()
        {
            $('#divpropertygrid').window('close');
        }
    </script>
    <script type="text/javascript">
        $(function ()
        {
            $('#tblControlList').datagrid({
                width: 91,
                height: 'auto',
                fitColumns: true,
                nowrap: false,
                noheader: true,
                rownumbers: false,
                showFooter: false,
                showHeader: false,
                singleSelect: true,
                columns: [[
                    { field: 'JSON_id', title: 'ID', width: 100, hidden: true },
                    { field: 'JSON_name', title: '', width: '100%',
                        styler: function (value, row, index)
                        {
                            return 'background:none;border:none;';
                        }
                    },
                    { field: 'JSON_type', title: 'Type', width: 100, hidden: true },
                    { field: 'JSON_class', title: 'Class', width: 100, hidden: true },
                    { field: 'JSON_json', title: 'Json', width: 100, hidden: true },
                    { field: 'JSON_html', title: 'Html', width: 100, hidden: true },
                    { field: 'JSON_description', title: 'Description', width: 100, hidden: true },
                    { field: 'JSON_createdon', title: 'CreatedOn', width: 100, hidden: true },
                    { field: 'JSON_createdby', title: 'CreatedBy', width: 100, hidden: true }
				]],
                rowStyler: function (index, row, css)
                {
                    return 'background:none;';
                }
            });
            $.ajax({
                type: "POST",
                url: "../FormDesignAjaxHandler.ashx",
                data: { action: "getCLDataSource" },
                async: false,
                success: function (data)
                {
                    if (data)
                    {
                        var json = eval('(' + data + ')');
                        $("#tblControlList").datagrid("loadData", json);
                    }
                },
                error: function ()
                {
                    $.messager.alert('错误', '数据加载错误，请联系管理员!');
                }
            });

            $.ajax({
                type: "POST",
                url: "../FormDesignAjaxHandler.ashx",
                data:
                {
                    action: "getFormTemplateControl",
                    formTemplateID: $("#hfFormTemplateID").val()
                },
                async: false,
                success: function (data)
                {
                    $("#createtable").html(data)
                },
                error: function ()
                {
                    $.messager.alert('错误', '数据加载错误，请联系管理员!');
                }
            });

            $(".datagrid-body tr").draggable({
                revert: true,
                proxy: 'clone'
            });
            $('#center').droppable({
                accept: '#atable',
                onDragEnter: function (e, source)
                {
                    //$(source).draggable('options').cursor = 'auto';
                    //$(source).draggable('proxy').css('border', '1px solid red');
                    //$(this).addClass('over');
                },
                onDragLeave: function (e, source)
                {
                    //$(source).draggable('options').cursor = 'not-allowed';
                    //$(source).draggable('proxy').css('border', '1px solid #ccc');
                    //$(this).removeClass('over');
                },
                onDrop: function (e, source)
                {
                    if (source.id == "atable")
                    {
                        OpenTable();
                    }
                }
            });
            $('#Recyclebin').droppable({
                accept: '#createtable *',
                onDrop: function (e, source)
                {
                    $(this).append(source);
                    $(source).attr('style', 'display:none;');
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-layout" style="width: 620px; height: 400px;">
        <div id="center" region="center" title="Main Title" style="background: #fafafa; overflow-y: auto;">
            <div id="createtable">
            </div>
        </div>
        <div id="divdrop" region="east" icon="icon-reload" title="East" split="true" style="width: 100px;
            overflow: hidden;">
            <div style="width: 91px" class="panel datagrid">
                <div style="width: 91px; height: auto" class="datagrid-wrap panel-body panel-body-noheader"
                    title="">
                    <div style="width: 91px; height: 25px;" class="datagrid-view">
                        <div style="width: 91px; left: 0px" class="datagrid-view2">
                            <div style="width: 91px;" class="datagrid-body">
                                <table border="0" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr id="atable" style="background: none transparent scroll repeat 0% 0%" class="datagrid-row"
                                            datagrid-row-index="0">
                                            <td style="border-bottom: medium none; border-left: medium none; background: none transparent scroll repeat 0% 0%;
                                                border-top: medium none; border-right: medium none" field="JSON_name">
                                                <div style="text-align: left; white-space: normal; height: auto" class="datagrid-cell ">
                                                    表格</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table id="tblControlList">
            </table>
            <div id="Recyclebin" style="width: 100px; height: 100px; display: block; background-color: Gray;">
                回收站</div>
        </div>
    </div>
    <div id="divTable" class="easyui-window" title="表格属性" style="width: 225px; height: 180px;
        top: 100px; left: 140px; padding: 5px; overflow: hidden;" collapsible="false"
        minimizable="false" maximizable="false" draggable="false" resizable="false" modal="true"
        closed="true">
        <br />
        行：<input class="easyui-numberspinner" id="tablerow" value="1" increment="1" style="width: 120px;" /><br />
        <br />
        列：<input class="easyui-numberspinner" id="talbecol" value="1" increment="1" style="width: 120px;" />
        <br />
        <br />
        <div border="false" style="text-align: center; padding: 5px 0;">
            <a class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="CreateTable()">
                确定</a> <a class="easyui-linkbutton" iconcls="icon-cancel" href="javascript:void(0)"
                    onclick="CloseTable()">取消</a>
        </div>
    </div>
    <div id="divpropertygrid" class="easyui-window" title="控件属性" style="width: 425px;
        top: 50px; left: 45px; height: 280px; padding: 5px; overflow: hidden;" collapsible="false"
        minimizable="false" maximizable="false" draggable="false" resizable="false" modal="true"
        closed="true">
        <table id="tpropertygrid">
        </table>
        <div region="south" border="false" style="text-align: right; padding: 5px 0;">
            <a class="easyui-linkbutton" iconcls="icon-ok" href="javascript:void(0)" onclick="SavePropertygrid()">
                确定</a> <a class="easyui-linkbutton" iconcls="icon-cancel" href="javascript:void(0)"
                    onclick="ClosePropertygrid()">取消</a>
        </div>
    </div>
    <div>
        <a id="btnSace" href="#" iconcls="icon-save" class="easyui-linkbutton" onclick="Save();"
            plain="true">保存</a> <a id="btnRet" href="#" iconcls="icon-back" class="easyui-linkbutton"
                onclick="retBack();" plain="true">返回</a>
    </div>
    <asp:HiddenField ID="hfTargetCell" runat="server" />
    <asp:HiddenField ID="hfFormTemplateID" runat="server" />
    <asp:HiddenField ID="hfControlID" runat="server" />
    <asp:HiddenField ID="hfControlName" runat="server" />
    <asp:HiddenField ID="hfControlType" runat="server" />
    <asp:HiddenField ID="hfControlClass" runat="server" />
    <asp:HiddenField ID="hfControlHtml" runat="server" />
    </form>
</body>
</html>
