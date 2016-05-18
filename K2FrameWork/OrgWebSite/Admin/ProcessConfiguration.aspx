<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessConfiguration.aspx.cs"
    Inherits="OrgWebSite.Admin.ProcessConfiguration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>流程配置</title>
    <style type="text/css">
        .item
        {
            border: 1px solid black;
        }
        .over
        {
            border: 1px dotted black;
        }
        .drop
        {
        }
        .left
        {
            
        }
        .assigned
        {
            background-color: Red;
        }
        .dropingTable
        {
            border:1px solid black;
        }
    </style>
    <script type="text/javascript" src="../Javascript/easyUI/jquery-1.7.2.min.js"></script>
    <script src="../Javascript/easyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Javascript/easyUI/easyloader.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.left .item').draggable({
                revert: true,
                proxy: 'clone',
                onStartDrag: function (e) {
                    var rr = $('#tblRoleRlue');
                    if (rr[0]) {
                        rr.addClass('dropingTable');     //显示
                        //添加一行
                        var row = rr[0].insertRow(rr[0].rows.length);
                        for (var i = 0; i < rr[0].rows[0].cells.length; i++) {
                            var c = row.insertCell();
                            addDropable(c);     //将c对象设置为拖拽目的
                            $(c).attr('style', 'width: 100px; height: 25px;');
                        }
                        rr[0].rows[0].insertCell();    //添加列
                    }
                }
            });


            $('.right td.drop').droppable({
                onDragEnter: function () {
                    $(this).addClass('over');
                },
                onDragLeave: function () {
                    $(this).removeClass('over');
                },
                onDrop: function (e, source) {
                    $(this).removeClass('over');
                    if ($(source).hasClass('assigned')) {
                        $(this).append(source);
                    } else {
                        var c = $(source).clone().addClass('assigned');
                        $(this).empty().append(c);
                        c.draggable({
                            revert: true
                        });
                    }
                }
            });
        });
        $(document).ready(function () {
            //加载所有角色信息

        });

        //添加e为拖拽目的对象
        function addDropable(e) {
            $(e).droppable({
                onDragEnter: function () {
                    $(this).addClass('over');
                },
                onDragLeave: function () {
                    $(this).removeClass('over');
                },
                onDrop: function (e, source) {//放置到目的位置时触发
                    
                    $(this).removeClass('over');
                    if ($(source).hasClass('assigned')) {
                        $(this).append(source);
                    } else {
                        var c = $(source).clone().addClass('assigned');
                        $(this).empty().append(c);
                        c.draggable({
                            revert: true
                        });
                    }
                }
            });
        }

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="left">
            <table>
                <tr>
                    <td>
                        <div class="item">
                            <span>部门负责人</span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="item">
                            行政专员
                        </div>
                    </td>
                </tr>
                <!-- other subjects -->
            </table>
        </div>
        <div class="right">
            <table id="tblRoleRlue" class="as">
                <tr>
                    <td class="blank">
                        入口节点</td>
                    <td class="blank">
                        表达式</td>
                </tr>
                <tr>
                    <td class="drop" style="width: 100px; height: 25px;">
                    </td>
                    <td>
                    </td>
                </tr>
                </table>
        </div>
    </div>
    </form>
</body>
</html>
