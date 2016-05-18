$(document).ready(function () {
    if ($("#OAF1_isreadonly").length == 0) {
        return;
    }
    var clickindex = 0;
    //如果不是只读页面 
    if ($("#OAF1_isreadonly").val() != "true") {
        //正常初始化所有绑定方法
        new FunctionClass();
        //电脑特殊配置为否 则隐藏特殊配置说明
        if ($("#OAF1_ComputerCode").find("option:selected").val() != 5) {
            document.getElementById("tr_es_2").style.display = "none";
        }
        //是否有分机为否 则隐藏分机DIV
        if ($("#OAF1_IsPhone").find("option:selected").val() == 0) {
            document.getElementById("td_tele_1").style.display = "none";
            document.getElementById("td_tele_2").style.display = "none";
            document.getElementById("tr_tel").style.display = "none";

        }
        //是否自动分配分机为是 则隐藏分机号码填写框
        if ($("#OAF1_IsAutoTel").find("option:selected").val() == 1) {
            document.getElementById("tr_tel").style.display = "none";

        }
        else {
            document.getElementById("tr_tel").style.display = "block";
            $("#OAF1_TelCode").attr("regex", "mustinput:必须输入分机号码;mustNumber:必须输入数字");
        }
        //是否有资产为否 则隐藏资产框
        if ($("#OAF1_IsES").find("option:selected").val() == 0) {
            document.getElementById("tr_es_1").style.display = "none";
            document.getElementById("tr_es_2").style.display = "none";
            document.getElementById("tr_es_3").style.display = "none";
            document.getElementById("tr_es_4").style.display = "none";
        }
        //是否有邮箱为否 则隐藏邮件组框
        if ($("#OAF1_IsEmail").find("option:selected").val() == 0) {
            document.getElementById("tr_email").style.display = "none";
        }
        //手机号码不为空 则显示手机号码 
        //debugger
        if ($.trim($("#OAF1_Tele").val()) == "") {
            $("[name=trMobile]").css("display", "none");
            //document.getElementsByName("trMoblie").style.display = "none";
        }
        else {
            $("[name=trMobile]").css("display", "block");
        }


    }
    else {
        $("li[mustinput=true]").remove(); //移除所有*号
        $("li").removeClass("mustinput"); //移除所有必填样式
        $("[regex]").removeAttr("regex"); //移除所有必填验证
        $("[title]").removeAttr("title"); //移除所有标题
        $("#information").css("display", "none"); //隐藏右上角必填项注释
        document.getElementById("tr_name").style.display = "none"; //姓名行隐藏
        document.getElementById("tr_employeeCode").style.display = "block"; //员工编号行显示
        if ($("#OAF1_hfEmployeeCode").val() == "") //如果没有员工编号
        {
            document.getElementById("li_employeecode_1").style.display = "none"; //员工编号隐藏
            document.getElementById("li_employeecode_2").style.display = "none"; //员工编号隐藏
        }
        else {
            document.getElementById("li_employeecode_1").style.display = "block"; //员工编号显示
            document.getElementById("li_employeecode_2").style.display = "block"; //员工编号显示
        }

        //手机号码不为空 则显示手机号码 
        if ($.trim($("#OAF1_Tele").val()) == "") {
            $("[name=trMobile]").css("display", "none");
        }
        else {
            $("[name=trMobile]").css("display", "block");
        }


        $("#OAF1_OnboardDate").attr("border", "0");
        $("#OAF1_ContractEnd").attr("border", "0");
        $("#OAF1_PostDetailName").removeClass("inputreadonly_2");
        $("#OAF1_PostName").removeClass("inputreadonly_2");
        $("#OAF1_PostDetailName").addClass("inputreadonly");
        $("#OAF1_PostName").addClass("inputreadonly");
    }

    //是查看页面
    if ($("#OAF1_ShowAllDiv").val() == "1") {
        document.getElementById("trDate").style.display = "none";
        document.getElementById("trHire").style.display = "none";
        document.getElementById("trSalary").style.display = "none";
        document.getElementById("trSpecial").style.display = "none";
        document.getElementById("div_other").style.display = "none";
        document.getElementById("div_hrbp_confirm").style.display = "none";
        document.getElementById("div_it").style.display = "none";
        document.getElementById("tr_it_confirm").style.display = "none";
        document.getElementById("tr_Employee_Has_telCode").style.display = "none";
        document.getElementById("div_es").style.display = "none";
        document.getElementById("tr_card").style.display = "none";
        //取得当前操作人的角色 显示相应区域
        $.each($("#OAF1_EmployeeRole").val().split(";"), function (index, Value) {
            if ($.trim(Value) != "") {
                $("#" + $.trim(Value)).css("display", "block");
            }
        });

        //员工没有填写了邮箱 则不显示员工填写信息 
        if ($.trim($("#OAF1_EmployeeHas_Email").val()) == "") {
            $("#tr_employee_hasEmail").css("display", "none");
        }
        else {
            $("#OAF1_EmployeeHas_Email").css("color", "")
        }


        //it资源确认页面都变为只读
        changeDivToReadOnly("tr_it_confirm", "inputreadonly", true);
        document.getElementById("li_email_conftrm_mustinput").style.display = "none"; //隐藏输入框后面的*
        document.getElementById("li_email_conftrm_button").style.display = "none"; //隐藏分派邮箱按钮

        //如果没有邮箱
        if ($("#OAF1_hfIsEmail").val() == "0") {
            document.getElementById("tr_it_email").style.display = "none";
            document.getElementById("tr_it_email_2").style.display = "none";
            document.getElementById("tr_it_confirm").style.display = "none";
            document.getElementById("tr_email").style.display = "none"; //邮件组隐藏
        }

        //如果没有电话号码 电话号码行相应信息隐藏
        if ($("#OAF1_hfIsPhone").val() == "0") {
            //document.getElementById("tr_it_tel").style.display = "none";
            document.getElementById("td_tele_1").style.display = "none";
            document.getElementById("td_tele_2").style.display = "none";
        }

        //如果BP填写了分机 显示BP申请信息
        if ($("#OAF1_TelCode").val() != "") {
            var label = "<li class='conter_right_list_input_bg_border' style='width: 465px;'><input style='width: 460px;' type='text' style='font-weight:normal;' class='inputreadonly' readonly='readonly' value='";
            label += "HR BP申请分机号码为 " + $("#OAF1_TelCode").val() + ";"
            label += "' /></li>";

            $(label).insertAfter($("#OAF1_TelCode").parent());
            $("#OAF1_TelCode").parent().hide();
        }

        //员工没有填写分机 不显示员工填写信息
        if ($("#OAF1_HasTel").val() != "1") {
            $("#tr_Employee_Has_telCode").css("display", "none");
        }
        else {
            $("#OAF1_EmployeeHas_TelCode").css("color", "#333333")
        }


        //ES确认分机信息为空 则不显示ES确认信息
        if ($.trim($("#OAF1_ES_TelCode").val()) == "") {

            //document.getElementById("tr_es_telconfirm").style.display = "none";
            $("#OAF1_ES_TelCode").parent().removeClass("conter_right_list_input_bg").addClass("conter_right_list_input_bg_border");
            if ($("#OAF1_HasTel").val() == "1" || ($("#OAF1_hfIsAutoTel").val() == 0 && $("#OAF1_TelCode").val() != "") || IsPostLevelLargeThanFive()) {
                $("#OAF1_ES_TelCode").val("暂无ES确认信息");
            }
            else {
                $("#OAF1_ES_TelCode").val("无需ES确认");
            }
        }
        else {
            document.getElementById("li_es_telmustinput").style.display = "none";
            $("#OAF1_ES_TelCode").parent().removeClass("conter_right_list_input_bg").addClass("conter_right_list_input_bg_border");
        }

        //分机确认页面变为只读 如果分机确认字段为空 则不显示
        if ($.trim($("#OAF1_IT_TelCode").val()) == "") {
            //document.getElementById("tr_tel_confirm").style.display = "none";
            changeDivToReadOnly("tr_tel_confirm", "inputreadonly", true);
            document.getElementById("li_autoTel").style.display = "none";
            document.getElementById("li_itmustinput").style.display = "none";

            if ($("#OAF1_hfIsPhone").val() == "1") {
                $("#OAF1_IT_TelCode").val("暂无IT确认信息")
            }
            else {
                $("#OAF1_IT_TelCode").val("该员工无分机");

            }


        }
        else {
            changeDivToReadOnly("tr_tel_confirm", "inputreadonly", true);
            document.getElementById("li_autoTel").style.display = "none";
            //document.getElementById("tr_tel_confirm").style.display = "none";
            document.getElementById("li_itmustinput").style.display = "none";
        }

        //是否有资产为否 则隐藏资产页面信息
        if ($("#OAF1_hfIsES").val() == 0) {
            document.getElementById("tr_es_1").style.display = "none";
            document.getElementById("tr_es_2").style.display = "none";
            document.getElementById("tr_es_3").style.display = "none";
            document.getElementById("tr_es_4").style.display = "none";

        }

        //如果邮箱已经确认 则加载邮箱到邮箱确认框
        if ($("#OAF1_InterfaceState").val() == 1) {
            $("#OAF1_ITConfirmEmail").val($("#OAF1_Email").val().split("@")[0]);
        }

        //如果IT资源确认信息为空 则不显示相应信息
        //全部为空则隐藏节点
        if ($.trim($("#OAF1_ES_MailGroup").val()) == "" && $.trim($("#OAF1_ITConfirmEmail").val()) == "") {
            document.getElementById("tr_it_confirm").style.display = "none";

        }

        //邮件组确认为空则隐藏邮件组确认行
        else if ($.trim($("#OAF1_ES_MailGroup").val()) == "") {
            document.getElementById("tr_it_email").style.display = "none";
        }
        //邮件确认为空则隐藏邮件确认行
        else if ($.trim($("#OAF1_ITConfirmEmail").val()) == "") {
            document.getElementById("tr_it_email_2").style.display = "none";
        }

        //入职日期确认变为只读
        var labeldate = "<input style='font-weight:normal;' class='inputreadonly' readonly='readonly' type='text' value='" + $("#OAF1_ConfirmOnboardDate").val() + "' />";
        $(labeldate).insertAfter($("#OAF1_ConfirmOnboardDate"));
        $("#OAF1_ConfirmOnboardDate").remove();
    }

    //如果没有邮箱 则邮箱字段不显示
    if ($("#OAF1_Email").val() == "") {
        document.getElementById("td_email_2").style.display = "none";
        document.getElementById("td_email_1").style.display = "none";
    }


    //如果没有文件夹编号 则文件夹不显示
    if ($("#OAF1_BatchCode").val() == "") {
        document.getElementById("tr_BatchCode").style.display = "none";
    }
    else {
        document.getElementById("tr_BatchCode").style.display = "block";
    }

    //如果有分机且分机号码不为空 则显示分机号码
    if ($("#OAF1_hfIsPhone").val() == "1") {
        if ($("#OAF1_TelCode").val() != "") {
            document.getElementById("tr_tel").style.display = "block";
        }
    }

    //如果没有员工编号 则员工编号不显示
    if ($("#OAF1_hfEmployeeCode").val() == "") {
        if ($("#OAF1_isreadonly").val() != "true") {
            document.getElementById("tr_employeeCode").style.display = "none";
        }
        else {
            document.getElementById("tr_employeeCode").style.display = "block";
            document.getElementById("li_employeecode_1").style.display = "none"; //员工编号隐藏
            document.getElementById("li_employeecode_2").style.display = "none"; //员工编号隐藏
        }
    }
    //如果电脑描述不为空 则显示电脑描述行
    if ($("#OAF1_ComputerDescription").val() != "") {
        document.getElementById("tr_es_2").style.display = "block";

    }
    else {
        document.getElementById("tr_es_2").style.display = "none";
    }

    //如果是HRBP确认入职页面
    if ($("#OAF1_hrbpconfirm").val() == 1) {
        document.getElementById("div_hrbp_confirm").style.display = "block";
        $("#OAF1_ConfirmOnboardDate").val($("#OAF1_OnboardDate").val());
        document.getElementById("li4").style.display = "block"; //可以查看入职员工填写信息
    }
    else if ($("#OAF1_ConfirmOnboardDate").val() != "" && $("#OAF1_ConfirmOnboardDate").val() != undefined) {
        document.getElementById("div_hrbp_confirm").style.display = "block";
        var labeldate = "<input style='font-weight:normal;' class='inputreadonly' readonly='readonly' type='text' value='" + $("#OAF1_ConfirmOnboardDate").val() + "' />";
        $(labeldate).insertAfter($("#OAF1_ConfirmOnboardDate"));
        $("#OAF1_ConfirmOnboardDate").remove();
    }
    else {
        document.getElementById("div_hrbp_confirm").style.display = "none";
    }
    $("#OAF1_Tel_Confirm").addClass("conter_right_list_input_bg");


    if ($("#div_it") != undefined && $("#div_ex") != undefined) {
        //判断是否是IT确认页面 如果是则把IT确认信息显示
        if ($("#OAF1_IS_IT_Confirm").val() == "1") {
            document.getElementById("tr_it_confirm").style.display = "block";


            //把邮件组确认信息初始化为邮件组信息
            $("#OAF1_ES_MailGroup").val($("#OAF1_MailGroup").val());
            $("#OAF1_ES_MailGroup").bind("focus", FunctionClass.prototype.EmailImgeSearch2);
            $("#OAF1_ES_MailGroup").addClass("inputbackimg");
            //$("#OAF1_IT_TelCode").val($("#OAF1_TeleCode").val());

            //debugger
            //员工填写了邮箱 则显示员工填写信息 EmployeeHas_Email
            if ($.trim($("#OAF1_EmployeeHas_Email").val()) != "") {
                $("#tr_employee_hasEmail").css("display", "block");
            }
            else {
                $("#tr_employee_hasEmail").css("display", "none");
            }

            //如果没有有邮箱 隐藏确认邮件组部分
            if ($("#OAF1_hfIsEmail").val() == "0") {
                document.getElementById("tr_it_email").style.display = "none";
                document.getElementById("tr_it_email_2").style.display = "none";
            }
            else {
                //如果已经分配了AD账号 则要把分配按钮隐藏 并且将邮箱确认字段变为只读
                if ($("#OAF1_InterfaceState").val() == 1) {
                    $("#OAF1_hfITConfirmEmail").val("1");
                    $("#OAF1_ITConfirmEmail").val($("#OAF1_Email").val());
                    //$("#OAF1_Email").val(strValue);
                    $("#OAF1_ITConfirmEmail").attr("readonly", "readonly");
                    document.getElementById("li_email_conftrm_button").style.display = "none";
                }
                else {
                    document.getElementById("tr_it_email").style.display = "block";
                    document.getElementById("tr_it_email_2").style.display = "block";
                    $("#OAF1_ITConfirmEmail").val($("#OAF1_Email").val().split("@")[0]);
                    //$("#OAF1_ITConfirmEmail").bind("change", function () {
                    //$("#OAF1_Email").val($("#OAF1_ITConfirmEmail").val());
                    //});
                    $("#OAF1_hfITConfirmEmail").attr("regex", "mustinput:必须创建AD账号");
                }
            }

        }


        //判断是否是ES分机确认页面 如果是则把ES分机确认信息显示 并且添加必填验证
        if ($("#OAF1_IS_Es_TelConfirm").val() == "1") {
            document.getElementById("li_es_telmustinput").style.display = "block";
            document.getElementById("tr_es_telconfirm").style.display = "block";
            document.getElementById("tr_email_top").style.display = "none"; //邮件信息隐藏
            document.getElementById("tr_email").style.display = "none"; //邮件组信息隐藏
            $("#tr_it_telcode").addClass("right_top");
            //ES分机填写框设为只读
            $("#OAF1_ES_TelCode").attr("readonly", "readonly");
            //如果BP填写了分机 显示BP申请信息
            if ($("#OAF1_TelCode").val() != "") {
                var label = "<li class='conter_right_list_input_bg_border' style='width: 465px;color:red;'>";
                label += "HR BP申请分机号码为 " + $("#OAF1_TelCode").val();
                label += "</li>";

                $(label).insertAfter($("#OAF1_TelCode").parent());
                $("#OAF1_TelCode").parent().hide();
            }

            //员工填写了分机 显示员工填写信息
            if ($("#OAF1_HasTel").val() == "1") {
                $("#tr_Employee_Has_telCode").css("display", "block");
            }

            //必填验证
            $("#OAF1_ES_TelCode").attr("regex", "mustinput:没有确认分机信息，请选择确认信息");

            //5级以上领导 ES分配分分机
            var checkboxli = "<ul ";
            if (IsPostLevelLargeThanFive()) {
                //$("#OAF1_ES_TelCode").attr("regex", "mustinput:没有确认分机信息");

                $("#OAF1_ES_TelCode").bind("focus", function () {
                    showModalDialoge('/Manage/TelNOManage/UpdateTelCode.aspx?FormId=' + $("#OAF1_FormId").val() + '&Region=' + $("#li_LocationCode").find("input[type=text]:eq(0)").val() + "&controlid=OAF1_ES_TelCode&telcode=" + $("#OAF1_ES_TelCode").val(), '分机确认信息', 460, 540);
                })

                checkboxli += "><li>";
            }
            //非5级以上领导 ES确认分机即可
            else {
                $("#OAF1_ES_TelCode").parent().parent().css("display", "none");
                checkboxli += "class='right_top'><li>ES确认分机："
            }


            checkboxli += "</li><li  style='width: 465px;'><table style='width:100%;' ><tr>";
            //如果BP填写的分机与员工填写的分机相同
            if ($("#OAF1_HasTel").val() == "1" && ($.trim($("#OAF1_TelCode").val()) == $.trim($("#OAF1_EmployeeHas_TelCode").val()))) {
                checkboxli += "<td align='left' style='width:20px;'><input  type='checkbox' name='es_checkTel'value='" + $("#OAF1_TelCode").val() + "' /></td><td style='width:110px;text-align:left;' align='left'>分配号码：" + $("#OAF1_TelCode").val() + "</td>";

            }
            else {
                //BP填写了分机
                if ($.trim($("#OAF1_TelCode").val()) != "") {
                    checkboxli += "<td align='left' style='width:20px;'><input  type='checkbox' name='es_checkTel'value='" + $("#OAF1_TelCode").val() + "' /></td><td style='width:110px;text-align:left;' align='left'>分配号码：" + $("#OAF1_TelCode").val() + "</td>";

                }
                //员工填写了分机
                if ($("#OAF1_HasTel").val() == "1") {
                    checkboxli += "<td align='left' style='width:20px;'><input  type='checkbox' name='es_checkTel'value='" + $("#OAF1_EmployeeHas_TelCode").val() + "' /></td><td style='width:110px;text-align:left;' align='left'>分配号码：" + $("#OAF1_EmployeeHas_TelCode").val() + "</td>";

                }
            }
            //如果自动分配分机
            if ($.trim($("#OAF1_hfIsAutoTel").val()) == "1") {
                checkboxli += "<td align='left' style='width:20px;'><input  type='checkbox' name='es_checkTel'value='自动分配分机' /></td><td style='width:100px;text-align:left;' align='left'>自动分配分机</td>";
            }
            //暂不分配分机
            checkboxli += "<td align='left' style='width:20px;'><input  type='checkbox' name='es_checkTel'value='暂不分配分机' /></td><td style='width:100px;text-align:left;' align='left'>暂不分配分机</td>";
            //分配按钮
            //checkboxli += "<td align='left'><Img alt='' style='cursor: pointer;' id='btn_tel_Confirm' onmouseover='SaveMouseover(this.id ,&#39;../../pic/btnImg/btnAffirm_over.png&#39;)' onmouseout='SaveMouseout(this.id ,&#39;../../pic/btnImg/btnAffirm_nor.png&#39;)' src='../../pic/btnImg/btnAffirm_nor.png' /></td>
            checkboxli += "<td></td></tr></table></li></ul>";

            $(checkboxli).insertAfter($("#OAF1_ES_TelCode").parent().parent());
            //只能选中一项
            $.each($("[name=es_checkTel]"), function (index, Value) {
                $(Value).bind("click", function () {
                    $(Value).parent().siblings().find("[name=es_checkTel]").prop("checked", false);
                    if ($(Value).prop("checked")) {
                        $("#OAF1_ES_TelCode").val($(Value).attr("value"));
                    }
                    else {
                        $("#OAF1_ES_TelCode").val("");
                    }
                })

            })

            $("#OAF1_ES_TelCode").bind("propertychange", function () {
                //debugger
                var selectTelCode = $("#OAF1_ES_TelCode").val();
                $.each($("[name=es_checkTel]"), function (index, Value) {
                    if ($(Value).attr("value") == selectTelCode) {
                        $(Value).prop("checked", true);
                    }
                    else {
                        $(Value).prop("checked", false);
                    }
                });

            })

        }



    }
    //如果是资产确认页面
    if ($("#OAF1_IS_Es_Confirm").val() == "1") {
        document.getElementById("li_print").style.display = "block";
    }

    //如果是确认分机页面
    if ($("#OAF1_IS_TelConfirm").val() == "1") {
        document.getElementById("tr_email_top").style.display = "none"; //邮件信息隐藏
        document.getElementById("tr_email").style.display = "none"; //邮件组信息隐藏
        $("#tr_it_telcode").addClass("right_top");
        $("#tr_card").css("display", "none"); //隐藏身份证信息
        $("#OAF1_IT_TelCode_3").attr("regex", "mustinput:号码未分配,请点击分配分机按钮");
        $("#OAF1_ES_TelCode").attr("readonly", "readonly");

        //IT确认信息发生了改变 则要重新分配
        $("#OAF1_IT_TelCode").bind("change", function () {

            if ($("#OAF1_IT_TelCode").val() != "暂不分配分机") {
                $("#OAF1_IT_TelCode_3").val("");
                $("#OAF1_IT_TelCode_3").attr("regex", "mustinput:号码未分配,请点击分配分机按钮");
            }
            else {
                $("#OAF1_IT_TelCode_3").removeAttr("regex");
            }
        });



        //如果BP填写了分机 显示BP申请信息
        if ($("#OAF1_TelCode").val() != "") {
            var label = "<li class='conter_right_list_input_bg_border' style='width: 465px;color:red;'>";
            label += "HR BP申请分机号码为 " + $("#OAF1_TelCode").val();
            label += "</li>";

            $(label).insertAfter($("#OAF1_TelCode").parent());
            $("#OAF1_TelCode").parent().hide();
        }


        //员工填写了分机 显示员工填写信息
        if ($("#OAF1_HasTel").val() == "1") {
            $("#tr_Employee_Has_telCode").css("display", "block");
        }

        //ES确认分机信息为空 则不显示ES确认信息
        if ($.trim($("#OAF1_ES_TelCode").val()) == "") {
            document.getElementById("tr_es_telconfirm").style.display = "none";

        }
        else {
            document.getElementById("tr_es_telconfirm").style.display = "block";
            document.getElementById("li_es_telmustinput").style.display = "none";
            $("#OAF1_ES_TelCode").parent().removeClass("conter_right_list_input_bg").addClass("conter_right_list_input_bg_border");
            $("#OAF1_ES_TelCode").css("color", "red");
        }


        $("Tel_Confirm").addClass("conter_right_list_input_bg");
        document.getElementById("tr_tel_confirm").style.display = "block";

        $("#tr_it_tel_2").css("display", "block");

        //***********************************************
        //如果自动分配分机
        //        if ($("#OAF1_hfIsAutoTel").val() == 1) {
        //            //员工无分机
        //            if ($.trim($("#OAF1_HasTel").val()) != "1") {
        //                var newbtn = "<Img alt='' style='cursor: pointer;' id='btn_tel_Confirm' onmouseover='SaveMouseover(this.id ,&#39;../../pic/btnImg/btnAffirm_over.png&#39;)' onmouseout='SaveMouseout(this.id ,&#39;../../pic/btnImg/btnAffirm_nor.png&#39;)' src='../../pic/btnImg/btnAffirm_nor.png' />";
        //                $("#UCProcessAction1_btnConfirm").css("display", "none");
        //                $(newbtn).insertAfter($("#UCProcessAction1_btnConfirm"));
        //                $("#OAF1_IT_TelCode").attr("regex", "mustinputconfirm:没有分配分机,是否要继续？");
        //                if ($("#OAF1_ES_TelCode").val() != "暂不分配分机" && $("#OAF1_ES_TelCode").val() != "自动分配分机") {
        //                    $("#OAF1_IT_TelCode").val($("#OAF1_ES_TelCode").val());
        //                    $("#OAF1_IT_TelCode").attr("readonly", "");
        //                }
        //                //默认号码为空
        //                else {
        //                    $("#OAF1_IT_TelCode").val("");
        //                    $("#OAF1_IT_TelCode").attr("readonly", "readonly");
        //                }


        //            }
        //            //员工已有分机
        //            else if ($.trim($("#OAF1_HasTel").val()) == "1") {
        //                //如果ES没选择暂不分配分机 和自动分配分机 则IT确认时带出ES确认号码 
        //                if ($("#OAF1_ES_TelCode").val() != "暂不分配分机" && $("#OAF1_ES_TelCode").val() != "自动分配分机") {
        //                    $("#OAF1_IT_TelCode").val($("#OAF1_ES_TelCode").val());
        //                }
        //                //默认号码为空
        //                else {
        //                    $("#OAF1_IT_TelCode").val("");
        //                }

        //                var newbtn = "<Img alt='' style='cursor: pointer;' id='btn_tel_Confirm' onmouseover='SaveMouseover(this.id ,&#39;../../pic/btnImg/btnAffirm_over.png&#39;)' onmouseout='SaveMouseout(this.id ,&#39;../../pic/btnImg/btnAffirm_nor.png&#39;)' src='../../pic/btnImg/btnAffirm_nor.png' />";
        //                $("#UCProcessAction1_btnConfirm").css("display", "none");
        //                $(newbtn).insertAfter($("#UCProcessAction1_btnConfirm"));
        //                $("#OAF1_IT_TelCode").attr("regex", "mustinputconfirm:没有分配分机,是否要继续？");
        //                //$("#OAF1_IT_TelCode").attr("readonly", "readonly");

        //            }

        //        }
        //        //非自动分派
        //        else {
        //            //如果ES没选择暂不分配分机 和自动分配分机 则IT确认时带出ES确认号码 
        //            if ($("#OAF1_ES_TelCode").val() != "暂不分配分机" && $("#OAF1_ES_TelCode").val() != "自动分配分机") {
        //                $("#OAF1_IT_TelCode").val($("#OAF1_ES_TelCode").val());
        //            }
        //            //默认号码为空
        //            else {
        //                $("#OAF1_IT_TelCode").val("");
        //            }

        //            var newbtn = "<Img alt='' style='cursor: pointer;' id='btn_tel_Confirm' onmouseover='SaveMouseover(this.id ,&#39;../../pic/btnImg/btnAffirm_over.png&#39;)' onmouseout='SaveMouseout(this.id ,&#39;../../pic/btnImg/btnAffirm_nor.png&#39;)' src='../../pic/btnImg/btnAffirm_nor.png' />";
        //            $("#UCProcessAction1_btnConfirm").css("display", "none");
        //            $(newbtn).insertAfter($("#UCProcessAction1_btnConfirm"));

        //            $("#OAF1_IT_TelCode").attr("regex", "mustinputconfirm:没有分配分机,是否要继续？");

        //        }
        //        $("#btn_tel_Confirm").unbind("click");
        //        $("#btn_tel_Confirm").bind("click", function () {
        //            //debugger
        //            if ($.trim($("#OAF1_IT_TelCode").val()) != "" && $("#OAF1_IT_TelCode_2").val() != "") {
        //                $("#OAF1_IT_TelCode").attr("regex", "mustinput:必须输入确认分机？");
        //                $("#UCProcessAction1_btnConfirm").click();

        //            }
        //            //如果填写了号码 但是没点分配分机的按钮
        //            else if ($.trim($("#OAF1_IT_TelCode").val()) != "" && $.trim($("#OAF1_IT_TelCode_2").val()) == "") {

        //                window.ymPrompt.confirmInfo({ message: "号码 " + $("#OAF1_IT_TelCode").val() + " 没有被分配，是否要保存<br/><br/>此号码？", title: '系统提示', handler: function ConFirm(tp) {
        //                    if (tp == 'ok') {
        //                        $("#UCProcessAction1_btnConfirm").click();
        //                    } else {
        //                        if ($.trim($("#OAF1_IT_TelCode_3").val()) != "") {
        //                            $("#OAF1_IT_TelCode").val($("#OAF1_IT_TelCode_3").val());
        //                            $("#OAF1_IT_TelCode_2").val("1");
        //                        }
        //                    }
        //                }
        //                });
        //            }
        //        });
        //******************************************************************

        //如果ES选择了分机 则默认带出分机
        if ($("#OAF1_ES_TelCode").val() != "暂不分配分机" && $("#OAF1_ES_TelCode").val() != "自动分配分机" && $("#OAF1_ES_TelCode").val() != "") {
            $("#OAF1_IT_TelCode").val($("#OAF1_ES_TelCode").val());
        }
        //默认号码为空
        else {
            $("#OAF1_IT_TelCode").val("");
        }

        //
        var li = "<li id='li7' style='width: 100px;'><table style='width: 100%;'><tr><td style='width: 10px;'><input name='it_checkTel' id='it_checkTel_1' type='checkbox' value='' title='自动分配' /></td><td align='left'>自动分配</td></tr></table></li>";
        $("#tr_it_tel_2").append(li);

        //BP选择了自动分派分机
        if ($("#OAF1_hfIsAutoTel").val() == 1) {

        }
        else {
            var li = "<li id='li7' style='width: 100px;'><table style='width: 100%;'><tr><td style='width: 10px;'><input name='it_checkTel' id='it_checkTel_1' type='checkbox' value='' title='手动录入' /></td><td align='left'>手动录入</td></tr></table></li>";
            $("#tr_it_tel_2").append(li);
        }

        var tellist = new Array();
        var bptel = $("#OAF1_TelCode").val();
        var emtel = $("#OAF1_EmployeeHas_TelCode").val();
        var estel = $("#OAF1_ES_TelCode").val();

        if (bptel != "") {
            tellist.push(bptel);
        }
        if (emtel != "" && emtel != bptel) {
            tellist.push(emtel);
        }
        if (estel != "" && estel != emtel && estel != bptel && estel != "暂不分配分机" && estel != "自动分配分机") {
            tellist.push(estel);
        }

        $.each(tellist, function (index, Value) {
            var li = "<li id='li7' style='width: 100px;'><table style='width: 100%;'><tr><td style='width: 10px;'><input name='it_checkTel' id='it_checkTel_1' type='checkbox' value='" + Value + "' title='分配" + Value + "' /></td><td align='left'>分配" + Value + "</td></tr></table></li>";
            $("#tr_it_tel_2").append(li);
        })

        var li2 = "<li id='li7' style='width: 100px;'><table style='width: 100%;'><tr><td style='width: 10px;'><input name='it_checkTel' id='it_checkTel_1' type='checkbox' value='" + "暂不分配分机" + "' title='暂不分配分机' /></td><td align='left'>暂不分配分机</td></tr></table></li>";
        $("#tr_it_tel_2").append(li2);

        //如果ES选择了号码 则默认选中ES的号码
        if ($.trim(estel) != "" && $.trim(estel) != "暂不分配分机" && $.trim(estel) != "自动分配分机") {
            $.each($("[name=it_checkTel]"), function (index, Value) {
                if ($(Value).attr("value") == estel) {
                    $(Value).prop("checked", true);
                }
            })
        }
        //如果ES没有选中 且 选择了自动分派分机 则默认选择自动分派分机
        else if ($("#OAF1_hfIsAutoTel").val() == 1 && $.trim(estel) != "暂不分配分机") {
            $.each($("[name=it_checkTel]"), function (index, Value) {
                if ($(Value).attr("title") == "自动分配") {
                    $(Value).prop("checked", true);
                    $("#OAF1_IT_TelCode").attr("readonly", "readonly");
                }
            })
        }
        else if ($.trim(estel) == "暂不分配分机") {
            $.each($("[name=it_checkTel]"), function (index, Value) {
                if ($(Value).attr("title") == "暂不分配分机") {
                    $(Value).prop("checked", true);
                    $("#OAF1_IT_TelCode_3").val("暂不分配分机");
                    $("#OAF1_IT_TelCode").val($(Value).val());
                    $("#OAF1_IT_TelCode").attr("readonly", "readonly");
                    $("#OAF1_btn_AutoTelNumber").css("display", "none");
                }
            })
        }
        //如果BP没默认有输入号码，ES也没勾选分机 则默认选中手动录入
        else {
            $.each($("[name=it_checkTel]"), function (index, Value) {
                if ($(Value).attr("title") == "手动录入") {
                    $(Value).prop("checked", true);
                    $("#OAF1_IT_TelCode").attr("readonly", false);
                }
            })

        }



        //只能选中一项 
        $.each($("[name=it_checkTel]"), function (index, Value) {
            $(Value).bind("change", function () {
                $(Value).parent().parent().parent().parent().parent().siblings().find("[name=it_checkTel]").prop("checked", false);
                if ($(Value).prop("checked")) {
                    $("#OAF1_IT_TelCode").val($(Value).attr("value"));
                    if ($(Value).attr("title") == "自动分配") {
                        $("#OAF1_IT_TelCode").attr("readonly", "readonly");
                        $("#OAF1_btn_AutoTelNumber").css("display", "block");
                        $("#OAF1_IT_TelCode_3").attr("regex", "mustinput:号码未分配,请点击分配分机按钮");
                        $("#OAF1_IT_TelCode_3").val("");
                    }
                    else if ($(Value).attr("title") == "手动录入") {
                        $("#OAF1_IT_TelCode").attr("readonly", false);
                        $("#OAF1_IT_TelCode_3").val("");
                        $("#OAF1_btn_AutoTelNumber").css("display", "block");
                        $("#OAF1_IT_TelCode_3").attr("regex", "mustinput:号码未分配,请点击分配分机按钮");
                    }
                    else if ($(Value).attr("title") == "暂不分配分机") {
                        $("#OAF1_IT_TelCode").attr("readonly", "readonly");
                        $("#OAF1_btn_AutoTelNumber").css("display", "none");
                        $("#OAF1_IT_TelCode_3").removeAttr("regex");
                        $("#OAF1_IT_TelCode_3").val("");

                        AjaxRemoveTelCodeByEmployeeCode();

                    }
                    else {
                        $("#OAF1_IT_TelCode").attr("readonly", "readonly");
                        $("#OAF1_btn_AutoTelNumber").css("display", "block");
                        $("#OAF1_IT_TelCode_3").val("");
                        $("#OAF1_IT_TelCode_3").attr("regex", "mustinput:号码未分配,请点击分配分机按钮");

                    }
                }
                else {
                    $("#OAF1_IT_TelCode").val("");
                }
            })

        })





    }

});

function getMailGroup(obj) {
    top.window.HrbpEmailSearch(null, $(obj), false, true, false);
}

function radioTelCode() {
    if ($("#IsAutoTel").prop("option:selected") == 1) {
        document.getElementById("tr_tel").display = "none";
    }
    else {
        document.getElementById("tr_tel").display = "block";

    }
}

function SetMailGroup(obj) {
    if ($("#OAF1_isEmailConfirm").val() != 1) {
        $("#OAF1_MailGroup").val("");
        $("#tr_email").find("input[type=text]").each(function (index, Value) {
            if (index != 0) {
                $("#OAF1_MailGroup").val($("#OAF1_MailGroup").val() + "," + $(Value).val());
            }
            else {
                $("#OAF1_MailGroup").val($(Value).val());
            }
        });
    }
    else {

        $("#OAF1_ES_MailGroup").val("");
        $("#tr_email").find("input[type=text]").each(function (index, Value) {
            if (index != 0) {
                $("#OAF1_ES_MailGroup").val($("#OAF1_ES_MailGroup").val() + "," + $(Value).val());

            }
            else {
                $("#OAF1_ES_MailGroup").val($(Value).val());
            }
        });

    }
}


var FunctionObj;
function FunctionClass() {
    this.name = "";
    this.timeID = "";
    this.datainfoID = "";
    this.loadID = "";
    this.TimeoutID = "";
    this.emailValue = "";
    this.emailGroup = "";
    this.flag = false;
    FunctionObj = this;
    this.Init();
}

FunctionClass.prototype.Init = function () {
    $("#OAF1_CompetentName").bind("focus", this.ClickEmployeeCheck);
    $("#OAF1_CompetentName").addClass("inputbackimg");
    $("#OAF1_FirstDeptName").bind("focus", this.ClickFirstDepartment);
    $("#OAF1_FirstDeptName").addClass("inputbackimg");
    $("#OAF1_SecondDeptName").bind("focus", this.ClickSecondDepartment);
    $("#OAF1_SecondDeptName").addClass("inputbackimg");
    $("#OAF1_ThirdDeptName").bind("focus", this.ClickThirdDepartment);
    $("#OAF1_ThirdDeptName").addClass("inputbackimg");
    $("#OAF1_PostLevelName").bind("focus", this.ClickPostionRelation);
    $("#OAF1_PostLevelName").addClass("inputbackimg");
    $("#OAF1_CompanyName").bind("focus", this.ClickCompany);
    $("#OAF1_CompanyName").addClass("inputbackimg");
    $("#OAF1_ContractCompanyName").bind("focus", this.ClickContractCompany);
    $("#OAF1_ContractCompanyName").addClass("inputbackimg");
    $("#OAF1_MailGroup").bind("focus", this.EmailImgeSearch);
    $("#OAF1_MailGroup").addClass("inputbackimg");
    $("#OAF1_OnboardDate").addClass("inputborder");
    $("#OAF1_ContractEnd").addClass("inputborder");

    $("#OAF1_IsEmail").bind("change", function () {
        if ($("#OAF1_IsEmail").find("option:selected").val() == 0) {

            document.getElementById("tr_email").style.display = "none";

        }
        else {
            document.getElementById("tr_email").style.display = "block";
        }
    });

    //电脑特殊配置
    $("#OAF1_ComputerCode").bind("change", function () {
        if ($("#OAF1_ComputerCode").find("option:selected").val() != 5) {
            document.getElementById("tr_es_2").style.display = "none";
        }
        else {
            document.getElementById("tr_es_2").style.display = "block";
        }
    })

    //是否有分机
    $("#OAF1_IsPhone").bind("change", function () {
        if ($("#OAF1_IsPhone").find("option:selected").val() == 0) {
            document.getElementById("td_tele_1").style.display = "none";
            document.getElementById("td_tele_2").style.display = "none";
            document.getElementById("tr_tel").style.display = "none";
            $("#OAF1_TelCode").val("");
            $("#OAF1_TelCode").removeAttr("regex");


        }
        else {
            document.getElementById("td_tele_1").style.display = "block";
            document.getElementById("td_tele_2").style.display = "block";

            if ($("#OAF1_IsAutoTel").find("option:selected").val() == 1) {
                document.getElementById("tr_tel").style.display = "none";
            }
            else {
                document.getElementById("tr_tel").style.display = "block";

            }
        }
    });

    //是否自动分配分机
    $("#OAF1_IsAutoTel").bind("change", function () {
        if ($("#OAF1_IsAutoTel").find("option:selected").val() == 1) {
            document.getElementById("tr_tel").style.display = "none";
            $("#OAF1_TelCode").removeAttr("regex");
            $("#OAF1_TelCode").attr("regex", "mustNumber:必须输入数字");
            $("#OAF1_TelCode").val("");
        }
        else {
            $("#OAF1_TelCode").removeAttr("regex");
            $("#OAF1_TelCode").attr("regex", "mustinput:必须输入分机号码;mustNumber:必须输入数字");
            document.getElementById("tr_tel").style.display = "block";

        }

    });

    //是否有资产
    $("#OAF1_IsES").bind("change", function () {
        if ($("#OAF1_IsES").find("option:selected").val() == 0) {
            document.getElementById("tr_es_1").style.display = "none";
            document.getElementById("tr_es_2").style.display = "none";
            document.getElementById("tr_es_3").style.display = "none";
            document.getElementById("tr_es_4").style.display = "none";

        }
        else {
            document.getElementById("tr_es_1").style.display = "block";
            document.getElementById("tr_es_2").style.display = "block";
            document.getElementById("tr_es_3").style.display = "block";
            document.getElementById("tr_es_4").style.display = "block";
        }
    });

    //证件类型
    $("#OAF1_CardTypeCode").bind("change", function () {
        if ($("#OAF1_CardTypeCode").find("option:selected").val() == "01") {
            $("#OAF1_Card").attr("regex", "mustinput:必须填写身份证;card:身份证填写不正确;");
        }
        else {
            $("#OAF1_Card").attr("regex", "mustinput:必须填写身份证;");
        }
    });
    //页面初始化完毕后 判断证件类型是身份证则加身份证校验
    if ($("#OAF1_CardTypeCode").find("option:selected").val() == "01") {
        $("#OAF1_Card").attr("regex", "mustinput:必须填写身份证;card:身份证填写不正确;");
    }
    else {
        $("#OAF1_Card").attr("regex", "mustinput:必须填写身份证;");
    }
}

//公司
FunctionClass.prototype.ClickCompany = function () {
    top.window.CompanyCode($("#OAF1_CompanyName"), $("#OAF1_CompanyCode"));
}

//合同签署公司
FunctionClass.prototype.ClickContractCompany = function () {
    top.window.ConCompanyCode($("#OAF1_ContractCompanyName"), $("#OAF1_ContractCompanyCode"));
}

//职位职级职位详细
FunctionClass.prototype.ClickPostionRelation = function () {
    top.window.Position($("#OAF1_PostLevelName"), $("#OAF1_PostLevelCode"), $("#OAF1_PostName"), $("#OAF1_PostCode"), $("#OAF1_PostDetailName"), $("#OAF1_PostDetailCode"), true, true, true);
}

//大部门
FunctionClass.prototype.ClickFirstDepartment = function () {
    top.window.HrpbFirstDeptCode($("#OAF1_FirstDeptName"), $("#OAF1_FirstDeptCode"), $("#OAF1_EmployeeCode").val());
}

//中部门
FunctionClass.prototype.ClickSecondDepartment = function () {
    top.window.SecondDeptCode($("#OAF1_SecondDeptName"), $("#OAF1_SecondDeptCode"));
}

//小部门
FunctionClass.prototype.ClickThirdDepartment = function () {
    top.window.ThirdDeptCode($("#OAF1_ThirdDeptName"), $("#OAF1_ThirdDeptCode"));
}

//主管
FunctionClass.prototype.ClickEmployeeCheck = function () {
    top.window.EmployeeCheck($("#OAF1_CompetentName"), $("#OAF1_CompetentCode"), $("#OAF1_EmployeeCode").val());
}

//切换input控件样式为只读或者开放
function changInputStyle(divid, className, IsWite) {
    var inputs = $("#" + divid + " input[type=text]");
    $.each(inputs, function (index, Value) {
        $(Value).addClass(className);
        if (IsWite) {
            $(Value).attr("readonly", "readonly");
            $(Value).unbind("click");
            //$(Value).attr("background","")

        }
        else {
            $(Value).removeAttr("readonly");
        }

    });
    /*日期输入框也换成Label*/
    if (IsWite) {
        var label1 = "<input style='font-weight:normal;' class='inputreadonly' readonly='readonly' type='text' value='" + $("#OAF1_OnboardDate").val() + "' />";
        $(label1).insertAfter($("#OAF1_OnboardDate"));
        $("#OAF1_OnboardDate").remove();

        var label2 = "<input style='font-weight:normal;' class='inputreadonly' readonly='readonly' type='text' value='" + $("#OAF1_ContractEnd").val() + "' />";
        $(label2).insertAfter($("#OAF1_ContractEnd"));
        $("#OAF1_ContractEnd").remove();

    }
}
////邮件检索
//FunctionClass.prototype.EmailImgeSearch = function () {
//    top.window.HrbpEmailSearch(null, $("#OAF1_MailGroup"), false, true, false, $("#OAF1_MailGroup").val());
//}

//邮件检索
FunctionClass.prototype.EmailImgeSearch = function () {
    //    top.window.HrbpEmailSearch(null, $("#" + event.srcElement.id), false, true, false, $("#" + event.srcElement.id).val());
    // top.window.HrbpEmailDimension(null, $("#OAF1_MailGroup"), false, true, false, $("#OAF1_MailGroup").val());
    top.window.HrbpEmailSearch(null, $("#OAF1_MailGroup"), false, true, false, $("#OAF1_MailGroup").val());
}
FunctionClass.prototype.EmailImgeSearch2 = function () {
    //    top.window.HrbpEmailSearch(null, $("#" + event.srcElement.id), false, true, false, $("#" + event.srcElement.id).val());
    //top.window.HrbpEmailSearch(null, $("#OAF1_ES_MailGroup"), false, true, false, $("#OAF1_ES_MailGroup").val());
    top.window.HrbpEmailDimension(null, $("#OAF1_ES_MailGroup"), false, true, false, $("#OAF1_ES_MailGroup").val());
}

//切换下拉框控件样式为Label
function changeSelectToLabel(divid) {
    var selects = $("#" + divid + " select");
    $.each(selects, function (index, Value) {
        var selectvalue = $(Value).find("option:selected").text();
        var label = "<input type='text' style='font-weight:normal;' class='inputreadonly' readonly='readonly' value='" + selectvalue + "' />";
        $(label).insertAfter($(Value));

        $(Value).remove();

    });

}
//切换textarea控件样式为只读或者开放
function changTextareaStyle(divid, className, IsWite) {
    var inputs = $("#" + divid + " textarea");
    $.each(inputs, function (index, Value) {
        $(Value).addClass(className);
        if (IsWite) {
            $(Value).attr("readonly", "readonly");
            $(Value).unbind("click");
            $(Value).removeClass("input");
            //$(Value).addClass("inputreadonly");
        }
        else {
            $(Value).removeAttr("readonly");
        }

    });
}
/*把一个DIV中 文本框 多行文本框 下拉框 全部换为Label 样式*/
function changeDivToReadOnly(divid, className, IsWite) {

    $.each(divid.split(";"), function (index, Value) {
        changInputStyle(Value, className, IsWite);
        changTextareaStyle(Value, className, IsWite)
        changeSelectToLabel(Value);
        $("#" + Value + " .conter_right_list_input_bg").addClass("conter_right_list_input_bg_border");
        $("#" + Value + " .conter_right_list_input_bg").removeClass("conter_right_list_input_bg");
    });


}

/*隐藏DIV*/
function HiddenDiv(divId, IsHidden) {

    if (IsHidden) {
        $.each(divId.split(";"), function (index, Value) {
            if ($.trim(Value) != "" && Value != undefined) {
                document.getElementById(Value).style.display = "none";
            }
        })
    }
}

/*自动分配分机*/
function AutoTelNumber() {
    var value = $("#OAF1_hfIsAutoTel").val();
    var oaddress = $("#hfLocationCode").val()//工作地点
    var ouserCode = $("#OAF1_lbl_EmployeeCode").val(); //员工编号
    var ouserName = $("#OAF1_Name").val(); //员工姓名
    var userCode = $("#OAF1_EmployeeCode").val()//填写人姓名
    var telcode = $("#OAF1_IT_TelCode").val(); //电话
    var hasEmail = $.trim($("#OAF1_HasTel").val());
    var ESInfo = $("#OAF1_ES_TelCode").val();



    var checkConditon = $("[name=it_checkTel]:checked").attr("title");

    //是否自动分配
    //条件1：自动分配分机为1 员工没有分机 且不是五级以上或外地员工 ；条件2：自动分配分机为 1 且 号码框为空
    //(value == "1" && hasEmail != "1" && !IsPostLevelLargeThanFive()) || (value == "1" && $.trim(telcode) == "")
    if (checkConditon == "自动分配") {
        //测试数据
        //        ouserCode = "BJ0909";
        //        oaddress = "BJ-Beijing";
        var param = "ouserCode=" + encodeURI(ouserCode) + "&ouserName=" + encodeURI(ouserName) + "&oaddress=" + encodeURI(oaddress) + "&userCode=" + encodeURI(userCode) + "&FormId=" + encodeURI($("#OAF1_FormId").val()) + "&ProcessId=" + encodeURI($("#OAF1_ProcessId").val()) + "&date=" + new Date();
        $.ajax({
            type: "GET",
            url: "/JavaScript/OAFAutoPhoneNum.ashx?&date=" + encodeURI(new Date()),
            data: param,
            success: function (data) {

                if (data != "没有可用分机信息") {
                    $("#OAF1_IT_TelCode").val(data);
                    //$("#btn_tel_Confirm").bind("click", function () {
                    //$("#UCProcessAction1_btnConfirm").click();
                    //});
                    //$("#UCProcessAction1_btnConfirm").css("display", "block");
                    $("#OAF1_IT_TelCode_3").val(data);
                    //$("#OAF1_IT_TelCode_2").val(data);
                    alert("分配成功");
                } else {
                    alert(data);
                }

            },
            error: function () {
                alert("数据加载错误！");
            }
        });

    }
    //手动分配或者已经有分机
    else {
        //手动分配
        //window.alert = top.window.ymPrompt.alert;

        if (telcode == "") {
            alert("分机号码为空，不能进行分配");
            return;
        }
        var param = "ouserCode=" + encodeURI(ouserCode) + "&ouserName=" + encodeURI(ouserName) + "&oaddress=" + encodeURI(oaddress) + "&userCode=" + encodeURI(userCode) + "&FormId=" + encodeURI($("#OAF1_FormId").val()) + "&ProcessId=" + encodeURI($("#OAF1_ProcessId").val())
        + "&telcode=" + telcode + "&state=0" + "&date=" + new Date();
        $.ajax({
            type: "POST",
            url: "/JavaScript/OAFAutoPhoneNum.ashx?&date=" + new Date(),
            data: param,
            success: function (data) {
                switch (data) {

                    case "2":
                        $("#OAF1_IT_TelCode_2").val("2");
                        //$("#btn_tel_Confirm").unbind("click");
                        //$("#btn_tel_Confirm").bind("click", function () {
                        //$("#UCProcessAction1_btnConfirm").click();
                        //});
                        $("#OAF1_IT_TelCode_3").val($("#OAF1_IT_TelCode").val());
                        //$("#OAF1_IT_TelCode_2").val($("#OAF1_IT_TelCode").val());
                        alert("号码分配成功！");
                        break;
                    case "3":
                        top.window.ymPrompt.confirmInfo({ message: "该号码是保留号码，您确定使用<br/><br/>此号码？", title: '分配号码', handler: function ConFirm(tp) {
                            if (tp == 'ok') {
                                var param2 = "ouserCode=" + encodeURI(ouserCode) + "&ouserName=" + encodeURI(ouserName) + "&oaddress=" + encodeURI(oaddress) + "&userCode=" + encodeURI(userCode) + "&telcode=" + encodeURI(telcode) + "&state=1" + "&date=" + new Date() + "&FormId=" + encodeURI($("#OAF1_FormId").val()) + "&ProcessId=" + encodeURI($("#OAF1_ProcessId").val());
                                $.ajax({
                                    type: "POST",
                                    url: "/JavaScript/OAFAutoPhoneNum.ashx",
                                    data: param2,
                                    success: function (data) {
                                        if (data == "4") {
                                            //$("#OAF1_IT_TelCode_2").val("4");
                                            $("#OAF1_IT_TelCode_3").val($("#OAF1_IT_TelCode").val());
                                            alert("分配号码成功！");
                                        } else {
                                            $("#OAF1_IT_TelCode_3").val("");
                                            alert("分配未成功！");
                                        }
                                    }
                                });
                            }
                        }
                        });
                        break;
                    case "0":
                        //$("#OAF1_IT_TelCode_2").val("0");
                        //$("#btn_tel_Confirm").unbind("click");
                        //$("#btn_tel_Confirm").bind("click", function () {
                        //  $("#UCProcessAction1_btnConfirm").click();
                        //});
                        alert("此号码在分机库中不存在！");
                        $("#OAF1_IT_TelCode_3").val("");
                        break;
                    default:
                        $("#OAF1_IT_TelCode_2").val("1");
                        top.window.ymPrompt.confirmInfo({ message: data + "<br/><br/>仍然分配此号码？", title: '分配号码', handler: function ConFirm(tp) {
                            if (tp != 'ok') {
                                $("#OAF1_IT_TelCode_2").val("");
                                //$("#OAF1_IT_TelCode").val("");

                            }
                            else {
                                var param3 = "ouserCode=" + encodeURI(ouserCode) + "&ouserName=" + encodeURI(ouserName) + "&oaddress=" + encodeURI(oaddress) + "&userCode=" + encodeURI(userCode) + "&telcode=" + encodeURI(telcode) + "&state=2" + "&date=" + new Date() + "&FormId=" + encodeURI($("#OAF1_FormId").val()) + "&ProcessId=" + encodeURI($("#OAF1_ProcessId").val());
                                $.ajax({
                                    type: "POST",
                                    url: "/JavaScript/OAFAutoPhoneNum.ashx",
                                    data: param3,
                                    success: function (data) {
                                        if (data == "2") {
                                            //$("#OAF1_IT_TelCode_2").val("4");
                                            $("#OAF1_IT_TelCode_3").val($("#OAF1_IT_TelCode").val());
                                            alert("分配号码成功！");
                                        } else {
                                            $("#OAF1_IT_TelCode_3").val("");
                                            alert("分配失败");
                                        }
                                    }
                                });
                            }
                        }
                        });
                        break;


                }
                //top.window.ymPrompt.alert("分配成功！");
            },
            error: function () {
                top.window.ymPrompt.alert("数据加载错误！");
            }
        });

    }
}

//根据工号释放分机6225880136542736
function AjaxRemoveTelCodeByEmployeeCode() {
    $.ajax({ //请求登录处理页
        url: "/JavaScript/OAFAutoPhoneNum.ashx?date=" + new Date(),
        type: "Get",
        //传送请求数据
        data: {
            RemoveEmCode: $("#OAF1_hfEmployeeCode").val(),
            RemoveBy: $("#OAF1_EmployeeCode").val()
            //date: new Date()
        },
        //插入成功后返回GUID
        success: function (strValue) {
            if ($.trim(strValue) != "") {
                alert(strValue);
            }

        },
        error: function (strValue) {
            alert("strValue");
        }
    })

}

//时间加三年
function AddDate(id1, id2) {
    if ($("#" + id1).val() != "") {
        var date = new Date($("#" + id1).val().replace(/-/g, "/"));
        $("#" + id2).val(date.getFullYear() + 3 + "-" + $("#" + id1).val().split("-")[1] + "-" + new Date(date.getFullYear() + 3, date.getMonth() + 1, 0).getDate());
    }
}


//时间加三年
function AddDateForFF_1() {
    if ($("#OAF1_OnboardDate").val() == "") {
        return;
    }
    var date = new Date($("#OAF1_OnboardDate").val().replace(/-/g, "/"));
    $("#OAF1_ContractEnd").val(date.getFullYear() + 3 + "-" + $("#" + "OAF1_OnboardDate").val().split("-")[1] + "-" + new Date(date.getFullYear() + 3, date.getMonth() + 1, 0).getDate());
}

function AddDateForFF_2() {
    var date = new Date($("#OAF1_ConfirmOnboardDate").val().replace(/-/g, "/"));
    $("#OAF1_ContractEnd").val(date.getFullYear() + 3 + "-" + $("#OAF1_ConfirmOnboardDate").val().split("-")[1] + "-" + new Date(date.getFullYear() + 3, date.getMonth() + 1, 0).getDate());
}


//姓名填写
function changeName() {
    var lastname = $.trim($("#OAF1_LastName").val());
    var firstname = $.trim($("#OAF1_FirstName").val());
    var name = firstname + lastname;
    $("#OAF1_Name").val(name);
    $("#OAF1_hfName").val(name);
}

function showPersonInfo() {
    window.open("../OAF/ShowPersonInfo.aspx?FormId=" + $("#OAF1_FormId").val());
}

/*创建AD账号*/
var waitmsg;
function CreateAD() {
    //debugger
    var email = $.trim($("#OAF1_ITConfirmEmail").val());
    if (email == "") {
        alert("必须输入邮箱");
        return false;
    }
    else if (isEmail(email + "@sohu.com") == false) {
        alert("邮箱格式不正确");
        return false;
    }
    else {
        waitmsg = new WaitMsg("49%", "创建AD中......");
        waitmsg.begin();
        AjaxCreateAD();

    }
}



//AJAX 方式创建AD账号
var errorIndex = 0;
function AjaxCreateAD() {
    //debuggeg
    $.ajax({ //请求登录处理页
        url: "CreateAD.ashx",
        type: "Get",
        //传送请求数据
        data: {
            FormId: $("#OAF1_FormId").val(),
            ProcessId: $("#OAF1_ProcessId").val(),
            Name: $("#OAF1_Name").val(),
            Email: $("#OAF1_ITConfirmEmail").val() + "@sohu-inc.com",
            P_FirstName: $("#OAF1_P_FirstName").val(),
            P_LastName: $("#OAF1_P_LastName").val(),
            CN: $("#OAF1_Name").val(),
            Title: $("#OAF1_PostName").val(),
            FirstDepartCode: $("#OAF1_FirstDeptCode").val(),
            FirstDepartment: $("#OAF1_FirstDeptName").val(),
            SecondDepartment: $("#OAF1_SecondDeptName").val(),
            ThirdDepartment: $("#OAF1_ThirdDeptName").val(),
            Location: $("#li_LocationCode").find("input[type=text]:eq(0)").val(),
            EmployeeCode: $("#OAF1_hfEmployeeCode").val(),
            EmployeeId: $("#OAF1_PostLevelName").val(),
            FirstName: $("#OAF1_FirstName").val(),
            LastName: $("#OAF1_LastName").val(),
            TelCode: document.getElementById("OAF1_IT_TelCode").defaultValue,
            Competent: $("#OAF1_CompetentCode").val(),
            interfaceState: $("#OAF1_InterfaceState").val(),
            MailGroup: $("#OAF1_ES_MailGroup").val(),
            EmployeeTypeCode: $("OAF1_EmployeeTypeCode").val(),
            datetime: new Date().toString()
        },
        //插入成功后返回GUID
        success: function (strValue) {
            if ($.trim(strValue) == "1") {
                alert("创建成功！");
                var interfacestate = $.trim($("#OAF1_InterfaceState").val()) == "" ? 0 : $("#OAF1_InterfaceState").val();
                $("#OAF1_InterfaceState").val(interfacestate + 1);
                $("#OAF1_hfITConfirmEmail").val("1");
                //$("#OAF1_ITConfirmEmail").val(strValue);
                $("#OAF1_Email").val($("#OAF1_ITConfirmEmail").val() + "@sohu-inc.com");
                //$("#OAF1_Email").val(strValue);
                $("#OAF1_ITConfirmEmail").attr("readonly", "readonly");
                document.getElementById("li_email_conftrm_button").style.display = "none";
                waitmsg.end();


            }
            else if ($.trim(strValue) == "3") {
                alert("未找到大部门信息,请联系管理员！");

                waitmsg.end();
            }
            else {
                if (errorIndex > 0) {
                    alert2("创建失败！" + strValue);
                    waitmsg.end();
                }
                else {
                    errorIndex += 1;
                    setTimeout(AjaxCreateAD, 3);

                }

            }
            //waitmsg.end();
        },
        error: function (strValue) {
            alert("响应错误");

            waitmsg.end();
        }
    });
}




//打印
function printES() {
    var employeeCode = $("#OAF1_hfEmployeeCode").val();
    var formId = $("#OAF1_FormId").val();
    var url = '/Process/Print/PrintWithTempLate.aspx?Template=ES&WorkFlow=1&TemplateCategoryCode=5&Param=' + formId + '&EmployeeCode=' + employeeCode + '';
    window.open(url);
}

//alert2
function alert2(msg) {
    top.window.ymPrompt.alert(msg);
}

//判断入职人员职级是否大于5级 或者是广州和上海分公司
function IsPostLevelLargeThanFive() {
    var postlevel = $("#OAF1_PostLevelCode").val();
    var payRollCompany = $("#OAF1_CompanyCode").val();
    //广州分公司
    if (payRollCompany == "03") {
        return true;
    }
    //上海分公司
    if (payRollCompany == "09") {
        return true;
    }

    if (postlevel == "1") {
        return false;
    }
    if (postlevel == "1A") {
        return false;
    }
    if (postlevel == "1B") {
        return false;
    }
    if (postlevel == "2") {
        return false;
    }
    if (postlevel == "2A") {
        return false;
    }
    if (postlevel == "2B") {
        return false;
    }
    if (postlevel == "3") {
        return false;
    }
    if (postlevel == "3A") {
        return false;
    }
    if (postlevel == "3B") {
        return false;
    }
    if (postlevel == "4") {
        return false;
    }
    if (postlevel == "4A") {
        return false;
    }
    if (postlevel == "4B") {
        return false;
    }

    return true;
}






