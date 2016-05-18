<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="K2FlowCheck.aspx.cs" Inherits="OrgWebSite.Search.K2FlowCheck.K2FlowCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/works.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../JavaScript/jquery-1.6.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/K2Employee.js"></script>
    <script src="../../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        function QieHuan(type, id1, id2)
        {
            document.getElementById(id1).className = "selectedtags";
            document.getElementById(id2).className = "noselectedtags";
            if (type == 1)
            {
                document.getElementById('dvEmployee').style.display = "";
                document.getElementById('box1').style.display = "none";
            }
            else if (type == 2)
            {
                document.getElementById('dvEmployee').style.display = "none";
                document.getElementById('box1').style.display = "";
            }
        }
    </script>
    <script type="text/javascript">
        function SelectedUserDivDis()
        {
            document.getElementById('divSelectedUser').style.display = 'none';
        }
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title></title>
    <style type="text/css">
        .emalNamecsstable
        {
            width: 100%;
            cellpadding: 0;
            cellspacing: 0;
            position: inherit;
        }
        .ahadden
        {
            cursor: hand;
            color: Blue;
            text-decoration: underline;
        }
        .createtd1
        {
            width: 45px;
        }
        .selectedtags
        {
            height: 23px;
            margin-right: 1px;
            margin-top: 10px;
            float: left;
        }
        .selectedtags .middlea a
        {
            text-decoration: none;
            float: left;
            height: 23px;
            width: 70px;
            line-height: 25px;
            color: #333;
            text-align: center;
        }
        .selectedtags .lefta
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_left.png');
            width: 6px;
        }
        .selectedtags .middlea
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_middle.png');
            width: 70px;
        }
        .selectedtags .righta
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_right.png');
            width: 6px;
        }
        .noselectedtags
        {
            height: 23px;
            margin-right: 1px;
            margin-top: 10px;
            float: left;
        }
        .noselectedtags .middlea a
        {
            text-decoration: none;
            float: left;
            height: 23px;
            width: 70px;
            line-height: 25px;
            color: #333;
            text-align: center;
        }
        .noselectedtags .lefta
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_gray_left.png');
            width: 6px;
        }
        .noselectedtags .middlea
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_gray_middle.png');
            width: 70px;
        }
        .noselectedtags .righta
        {
            float: left;
            height: 23px;
            list-style-type: none;
            text-align: center;
            background-image: url('../../pic/lable_gray_right.png');
            width: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="gemDiv" style="width: 750px; height: 500px; padding-top:20px;" id="firstDiv" runat="server">
        <div class="emailcontent" style="width: 700px; height: 400px;">
            <div style="padding-left: 25px;">
                <ul class="selectedtags" id="selectedtags">
                    <li class="lefta" style="height: 23px;"></li>
                    <li class="middlea" style="height: 23px;"><a href="javascript:QieHuan(1,'selectedtags','noselectedtags');">
                        人员选择</a></li>
                    <li class="righta" style="height: 23px;"></li>
                </ul>
                <ul class="noselectedtags" id="noselectedtags" runat="server">
                    <li class="lefta" style="height: 23px;"></li>
                    <li class="middlea" style="height: 23px;"><a href="javascript:QieHuan(2,'noselectedtags','selectedtags');">
                        角色选择</a></li>
                    <li class="righta" style="height: 23px;"></li>
                </ul>
            </div>
            <div class="cemDivEmai" style="overflow: hidden; width: 700px; height: 416px; margin-top: 0px;"
                id="secDiv" runat="server">
                <ul id="dvEmployee">
                    <li class="lia" style="padding-top: 4px;">可选人员【请输入员工姓名、员工编号、员工邮箱或员工部门进行人员查询。多条件查询以空格相隔，双击进行选择】</li>
                    <li class="lib">
                        <div class="lib_inputbig" style="width: 685px;">
                            <input id="txtInfo" class="inputbig_text" type="text" name="" value="" style="width: 660px;
                                margin-left: 2px;" />
                            <img id="check" alt="" class="input_text_img" src="../../Img/menu1.png" /></div>
                    </li>
                    <li>
                        <div class="dataDiv hDiv" style="height: 230px; border-bottom: 0px;">
                            <table id="dataDiv" class="tablecs" cellpadding="0" cellspacing="0">
                            </table>
                        </div>
                    </li>
                </ul>
                <ul id="box1" style="display: none;">
                    <li class="lia" style="padding-top: 4px;">可选角色 【请输入角色名称或角色描述进行角色查询。多条件查询以空格相隔，双击进行选择】</li>
                    <li class="lib">
                        <div class="lib_inputbig" style="width: 685px;">
                            <input id="IputDepartment" class="inputbig_text" type="text" value="" title="首屏未显示全部，输入可模糊查询全部"
                                style="width: 660px; margin-left: 2px;" />
                            <img id="Img1" alt="" class="input_text_img" src="../../Img/menu1.png" /></div>
                    </li>
                    <li>
                        <div id="" class="dataDiv hDiv" style="height: 230px; border-bottom: 0px;">
                            <table id="dataInfo" class="tablecs" cellpadding="0" cellspacing="0">
                                <tr bgcolor='#fef5c7'>
                                    <th class='th2' style='width: 130px; background-color: #fef5c7;'>
                                        角色名称
                                    </th>
                                    <th class='th1' style="background-color: #fef5c7;">
                                        描述
                                    </th>
                                </tr>
                            </table>
                        </div>
                    </li>
                </ul>
                <div class="cemDivEmai1" style="overflow: hidden; width: 700px; margin-left: 0px;
                    border-left: 0px;" id="divSelectedUser">
                    <ul>
                        <li style="background-color: #fef5c7; border-bottom: #f1d974 1px solid;"><span style="position: relative;
                            margin-left: 15px; margin-top: 3px;">已选择列表</span></li>
                        <li>
                            <div id="emalName" class="emalNamecss" style="height: 100px; overflow: hidden; overflow-y: scroll;
                                position: inherit;">
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="thirdDiv" runat="server" class="onlinebig" style="width: 300px; padding-right: 150px;"
                align="right">
                <img style="cursor: pointer;" alt="" id="btnOk" src="../../pic/btnImg/btnConfirm_nor.png"
                    onmouseover="SaveMouseover('btnOk','../../pic/btnImg/btnConfirm_over.png')" onmouseout="SaveMouseout('btnOk','../../pic/btnImg/btnConfirm_nor.png')" />
                <img style="cursor: pointer;" alt="" id="btTrue" src="../../pic/btnImg/btnClear_nor.png"
                    runat="server" />
                <img style="cursor: pointer;" alt="" id="btFalse" src="../../pic/btnImg/btnCancel_nor.png"
                    onmouseover="SaveMouseover('btFalse','../../pic/btnImg/btnCancel_over.png')"
                    onmouseout="SaveMouseout('btFalse','../../pic/btnImg/btnCancel_nor.png')" />
            </div>
        </div>
    </div>
    <input id="param" runat="server" type="hidden" value="00000000-0000-0000-0000-000000000000" />
    <input id="checkstyle" runat="server" type="hidden" value="true" />
    <input id="pos" runat="server" type="hidden" value="0" />
    </form>
</body>
</html>
