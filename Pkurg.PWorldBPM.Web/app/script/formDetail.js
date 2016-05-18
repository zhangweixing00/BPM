(function ($) {
    $.PkurgMobile = $.PkurgMobile || {};
    $.PkurgMobile.FormDetail = $.PkurgMobile.FormDetail || {};
    $.PkurgMobile.FormDetail.Fun = {
        InitMobileDetail: initMobileDetail,
        InitAddSignHistory: initAddSignHistory,
        InitTrUserEvents: initTrUserEvents,
        AddSign: addSign,
        GetPageSize: getPageSize,
        GetAddSignHistorySize: getAddSignHistorySize,
        AddSignHistory: addSignHistory,
        NexPageUser: nexPageUser,
        LastPageUser: lastPageUser,
        InitPage: initPage
    };
    // 分页大小
    function getPageSize() {
        return 3;
    }
    // 加签人历史记录存储大小
    function getAddSignHistorySize() {
        return 6;
    }

    // 初始化手机详情
    function initMobileDetail() {
        var sUserencode = GetQueryString("userencode");
        /* 待办事项详情 */
        var sInstanceid = GetQueryString("id");
        var model00 = { "id": sInstanceid };
        $.ajax({
            type: "POST",
            url: activeUrl + "app.ashx?action=getworkflowinfo&ref=mobile",
            dataType: "jsonp",
            jsonp: "callback",
            jsonpCallback: "handler",
            data: model00,
            beforeSend: function () {
                $("#loading").show();
            },
            complete: function () {
                $("#loading").hide();
            },
            success: function (data) {
                //todo:
                if (data) {
                    $(".qingShiDan_title").html(data.FlowType);
                    $(".FormTitle").html(data.FormTitle);
                    $(".StartUserName").html(data.StartUserName);
                    $(".StartDeptName").html(data.StartDeptName);
                    $(".StartTime").html(data.StartTime);
                    $(".chaKanXiangQing").attr('href', data.DetailUrl + "&ref=mobile&userencode=" + sUserencode);
                    $("#ifrPCDetail").attr('src', data.DetailUrl + "&ref=mobile&userencode=" + sUserencode);

                    if (data.FlowType == "招标定标审批单") {
                        if ($("#tip")) {
                            $("#tip").show();  
                        }                        
                    }

                    // 附件Begin
                    if (data.AttachmentInfos != null && data.AttachmentInfos != undefined && data.AttachmentInfos.length > 0) {
                        var htmlAttachmentInfo = '';
                        $.each(data.AttachmentInfos, function (index, value) {
                            // alert(decodeURI(value.URL));
                            htmlAttachmentInfo += '<div class="sCWrap_content"><p class="p1"><a href=' + value.URL + ' target="_blank">' + value.AttachmentName + '</a></p><p class="p2"><span class="name">' + value.CreateByUserName + '</span><span class="dateTime">' + value.CreateAtTime + '</span></p></div>';
                            if (index != data.AttachmentInfos.length - 1) {
                                htmlAttachmentInfo += '<div style="border:1px solid #43c0dd;"></div>';
                            }
                        });
                        $('#divAttachment').html('');
                        $('#divAttachment').html(htmlAttachmentInfo);
                    } else {
                        $('.attachment_list').hide();
                    }
                    // 附件End

                    $(".daiBanShiXiang_content").html(data.Content);

                    var oStepInfos = data.StepInfos;

                    var str1 = "";
                    for (var i = 0; i < oStepInfos.length; i++) {
                        str1 += "<div class=\"sCWrap\">";
                        str1 += "<div class=\"sCWrap_title\">" + oStepInfos[i].Name + "</div>";
                        var aOptions = oStepInfos[i].Options;
                        for (var j = 0; j < aOptions.length; j++) {
                            //有审批意见才显示
                            if (aOptions[j].Option) {
                                str1 += "<div class=\"sCWrap_content\"><p class=\"p1\">" + aOptions[j].Option + "</p><p class=\"p2\"><span class=\"name\">" + aOptions[j].UserName + "</span><span class=\"dateTime\">" + aOptions[j].ApprovalTime + "</span></p></div>";
                            }
                        }
                        str1 += "</div>";
                    }
                    $(".sCWrap_wrap").html(str1);
                    // iframe载入成功
                    $("#ifrPCDetail").load(function () {
                        // 子页面高度【不支持跨域】
                        var subPageHeight = $(this).contents().find("body").height() + 200;
                        // 文档总高度
                        var docHeight = $(document).height();
                        // 手机表单内容高度
                        var mobileFormHeight = $('.daiBanShiXiang').height();
                        // 取上述三种高度最大者为iframe高度 Begin
                        var iHeight = 0;
                        iHeight = subPageHeight > docHeight ? subPageHeight : docHeight;
                        iHeight = iHeight > mobileFormHeight ? iHeight : mobileFormHeight;
                        $('#ifrPCDetail').height(iHeight);
                        // 取上述三种高度最大者为iframe高度 End
                    });
                    // 如果iframe载入出现错误,进行补救
                    if ($('#ifrPCDetail').height() < $(document).height()) {
                        $('#ifrPCDetail').height($(document).height() + 200);
                    }

                } else {
                    alert('获取失败！');
                };
            },
            error: function (a, b, c, d, e) {
                alert('获取失败！');
            }
        });
    };
    // 加载下一页用户
    function nexPageUser() {
        // 加载更多
        $('.layermchild #btnNextPage').off().on('click', function () {
            var keyWord = $('.layermchild #iptUserAccount').val();
            if (keyWord == null || keyWord == undefined || $.trim(keyWord) == '') {
                layer.open({
                    content: '请输入用户域账号、姓名或姓名拼音进行查询',
                    time: 2
                });
                return false;
            }
            var iPageIndex = parseInt($('#hdPageIndex').val()) + 1;
            var iPageSize = $.PkurgMobile.FormDetail.Fun.GetPageSize();
            var iData = { "keyword": keyWord, pageIndex: iPageIndex, pagesize: iPageSize };
            $.ajax({
                type: "POST",
                url: activeUrl + "app.ashx?action=getuserlist&ref=mobile",
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "handler",
                data: iData,
                success: function (dataUser) {
                    if (dataUser != null && dataUser != undefined && dataUser.length > 0) {
                        var htmlUser = '';
                        $.each(dataUser, function (i, v) {
                            htmlUser += '<tr style="cursor:default;" class="trUser">';
                            htmlUser += '<td>';
                            htmlUser += '<div><input type="radio" name="selectuser" user_account="' + v.LoginName + '"  user_name="' + v.EmployeeName + '"  />' + v.EmployeeName + ' (' + v.LoginName + ')</div>';
                            htmlUser += '<div style="padding-left:20px;padding-top:4px;">【' + v.CompanyName + '】-【' + v.DepartName + '】</div>';
                            htmlUser += '</td>';
                            htmlUser += '</tr>';
                        });
                        $('.layermchild #tbUser').html(htmlUser);
                        $.PkurgMobile.FormDetail.Fun.InitTrUserEvents();
                        $('#hdPageIndex').val(iPageIndex);
                        var iPageSize = $.PkurgMobile.FormDetail.Fun.GetPageSize();
                        if (dataUser.length < iPageSize) {
                            $('.layermchild #btnNextPage').hide();
                        }
                        $('.layermchild #btnLastPage').show();
                        $.PkurgMobile.FormDetail.Fun.LastPageUser();
                    } else {
                        $('.layermchild #btnNextPage').hide();
                    }
                },
                error: function (a, b, c, d, e) {
                    alert('获取异常！');
                }
            });
        });
    }

    // 加载上一页用户
    function lastPageUser() {
        // 加载更多
        $('.layermchild #btnLastPage').off().on('click', function () {
            var keyWord = $('.layermchild #iptUserAccount').val();
            if (keyWord == null || keyWord == undefined || $.trim(keyWord) == '') {
                layer.open({
                    content: '请输入用户的域账号、姓名或姓名全拼进行查询',
                    time: 2
                });
                return false;
            }
            var iPageIndex = parseInt($('#hdPageIndex').val()) - 1;
            var iPageSize = $.PkurgMobile.FormDetail.Fun.GetPageSize();
            var iData = { "keyword": keyWord, pageIndex: iPageIndex, pagesize: iPageSize };
            $.ajax({
                type: "POST",
                url: activeUrl + "app.ashx?action=getuserlist&ref=mobile",
                dataType: "jsonp",
                jsonp: "callback",
                jsonpCallback: "handler",
                data: iData,
                success: function (dataUser) {
                    if (dataUser != null && dataUser != undefined && dataUser.length > 0) {
                        var htmlUser = '';
                        $.each(dataUser, function (i, v) {
                            htmlUser += '<tr style="cursor:default;" class="trUser">';
                            htmlUser += '<td>';
                            htmlUser += '<div><input type="radio" name="selectuser" user_account="' + v.LoginName + '"  user_name="' + v.EmployeeName + '"  />' + v.EmployeeName + ' (' + v.LoginName + ')</div>';
                            htmlUser += '<div style="padding-left:20px;padding-top:4px;">【' + v.CompanyName + '】-【' + v.DepartName + '】</div>';
                            htmlUser += '</td>';
                            htmlUser += '</tr>';
                        });
                        $('.layermchild #tbUser').html(htmlUser);
                        $.PkurgMobile.FormDetail.Fun.InitTrUserEvents();
                        $('#hdPageIndex').val(iPageIndex);
                        $('.layermchild #btnNextPage').show();
                    }
                    if (iPageIndex == 0) {
                        $('.layermchild #btnLastPage').hide();
                        $('.layermchild #btnNextPage').show();
                    }
                },
                error: function (a, b, c, d, e) {
                    alert('获取异常！');
                }
            });
        });
    }


    // 初始化TR事件
    function initTrUserEvents() {
        $('.layermchild #tbUser .trUser').each(function () {
            $(this).off().on('click', function () {
                $(this).find('input[name="selectuser"]').prop("checked", true); //.attr('checked', 'checked');
            });
        });
    }
    // 确定加签  beSignedUserAccount:被加签域账号
    function addSign(beSignedUserAccount, beSignedUserName) {
        var sUsercode = GetQueryString("usercode");
        var sUserencode = GetQueryString("userencode");
        var nPageindex = GetQueryString("pageindex");
        var sUsername = uniencode(GetQueryString("username"));
        var sCompanyname = uniencode(GetQueryString("companyname"));
        var sDeptname = uniencode(GetQueryString("deptname"));
        var nQh_num = GetQueryString("qh_num");

        var sInstanceid = GetQueryString("id");
        var sSn = GetQueryString("sn");
        if (sUserencode == null || sUserencode == undefined || sInstanceid == null || sInstanceid == undefined || sSn == null || sSn == undefined) {
            layer.open({
                content: '操作错误,请刷新页面或退出重新登录系统',
                time: 2
            });
            return false;
        }
        if (beSignedUserAccount == null || beSignedUserAccount == undefined) {
            layer.open({
                content: '请选中用户后再进行加签操作',
                time: 2
            });
        } else {
            var iData = { "userencode": sUserencode, "sn": sSn, "id": sInstanceid, "tousercode": beSignedUserAccount, "remark": $(".sCWrap_content1 textarea").val() };
            layer.open({
                content: '确定加签给 ' + beSignedUserName + '(' + beSignedUserAccount + ')吗？',
                shadeClose: false,
                btn: ['确认', '取消'],
                shadeClose: false,
                yes: function () {
                    //加载层
                    layer.open({
                        type: 2,
                        shadeClose: false
                    });
                    $.ajax({
                        type: "POST",
                        url: activeUrl + "app.ashx?action=addsign&ref=mobile",
                        dataType: "jsonp",
                        jsonp: "callback",
                        jsonpCallback: "handler",
                        data: iData,
                        success: function (data) {
                            layer.closeAll();
                            if (data) {
                                // 历史记录  
                                $.PkurgMobile.FormDetail.Fun.AddSignHistory(sUsercode, beSignedUserAccount, beSignedUserName);
                                layer.open({
                                    content: '加签成功',
                                    shadeClose: false,
                                    btn: ['确定'],
                                    yes: function () {
                                        window.location.href = "list.html?usercode=" + sUsercode + "&userencode=" + sUserencode + "&pageindex=" + nPageindex + "&username=" + sUsername + "&companyname=" + sCompanyname + "&deptname=" + sDeptname + "&qh_num=" + nQh_num + "";
                                    }
                                });
                            } else {
                                layer.open({
                                    content: '加签失败',
                                    time: 2
                                });
                            }
                        },
                        error: function (a, b, c, d, e) {
                            layer.closeAll();
                            alert("加签异常");
                        }
                    });
                }, no: function () {
                    //layer.open({ content: '你选择了取消', time: 1 });
                }
            });
        }
    };

    // 初始化加签常用人(历史记录)
    function initAddSignHistory() {
        var sUsercode = GetQueryString("usercode");
        // 可视窗口高度
        var iWindowHeight = $(window).height();
        // 加签操作区域高度
        var iHtmlHeight = $('#divAddSign').height();
        var iHeight = 0;
        // 获取本地历史记录
        var iAddSignHistory = localStorage.getItem('addSignHistory_' + sUsercode);
        if (iAddSignHistory != null && iAddSignHistory != undefined && iAddSignHistory.length > 0) {
            var obj = JSON.parse(iAddSignHistory);
            var iHtml = '';
            $.each(obj, function (index, value) {
                iHtml += '<span class="i-span-addsign" user_account="' + value.beSignedUserAccount + '"  user_name="' + value.beSignedUserName + '">' + value.beSignedUserName + '(' + value.beSignedUserAccount + ')' + '</span> ';
            });
            $('.layermchild #divdivAddSignHistoryContent').html('');
            $('.layermchild #divdivAddSignHistoryContent').html(iHtml);
            $('.layermchild #divAddSignHistory').show();
            // 加签常用人所占高度
            var iAddSignHistoryHeight = $('.layermchild #divAddSignHistory').height();
            iHeight = (iWindowHeight - iHtmlHeight - iAddSignHistoryHeight) - 10;
        } else {
            iHeight = (iWindowHeight - iHtmlHeight) - 10;
        }
        if (iHeight < 0) {
            iHeight = 0;
        }
        $('.layermchild').css('top', iHeight);
        $('.layermchild .i-span-addsign').each(function () {
            $(this).off().on('click', function () {
                var selectUserAccount = $(this).attr('user_account');
                var selectUserName = $(this).attr('user_name');
                $.PkurgMobile.FormDetail.Fun.AddSign(selectUserAccount, selectUserName);
            });
        });
    }

    // 添加 加签历史(加签常用人)
    function addSignHistory(userCode, beSignedUserAccount, beSignedUserName) {
        if (userCode == null || userCode == undefined || userCode == '') {
            return false;
        }
        if (window.localStorage) {
            // 浏览器支持localStorage
            var addSignUser = new Object();
            addSignUser.userCode = userCode;
            addSignUser.beSignedUserAccount = beSignedUserAccount;
            addSignUser.beSignedUserName = beSignedUserName;
            addSignUser.createTime = new Date().toLocaleString();

            var iAddSignHistory = localStorage.getItem('addSignHistory_' + userCode);
            if (iAddSignHistory != null && iAddSignHistory != undefined) {
                var obj = JSON.parse(iAddSignHistory);
                var iHistorySize = $.PkurgMobile.FormDetail.Fun.GetAddSignHistorySize();

                // 验证本地存储是否已达到历史记录上限 Begin
                if (obj.length >= iHistorySize) {
                    // 本地存储已达到历史记录上限
                    // 删除位置最早的记录
                    obj.splice(0, parseInt(obj.length - iHistorySize) + 1);
                }
                // 验证本地存储是否已达到历史记录上限 End

                // 检索是否已经存在本条数据 Begin
                var iCount = -1;
                $.each(obj, function (i, v) {
                    if (v.beSignedUserAccount == beSignedUserAccount) {
                        iCount = i;
                        return false;
                    }
                });

                if (iCount == -1) {
                    // 不存在就新增本条数据
                    obj.push(addSignUser);
                } else {
                    // 存在就更新创建时间
                    obj.splice(iCount, 1);
                    obj.push(addSignUser);
                }
                // 检索是否已经存在本条数据 End

                // 转为字符串（localStorage只能存储字符串）
                var str = JSON.stringify(obj)
                localStorage.setItem(('addSignHistory_' + userCode), str);
            } else {
                var arraySignUser = new Array();
                arraySignUser.push(addSignUser);
                var str = JSON.stringify(arraySignUser)
                localStorage.setItem(('addSignHistory_' + userCode), str);
            }
        }
        else {
            // 不支持
        }
    }

    // 页面初始化
    function initPage() {
        $.PkurgMobile.FormDetail.Fun.InitMobileDetail();
        // TabSwiper
        var tabsSwiper = new Swiper('#tabs-container', {
            touchRatio: 0,
            speed: 500
        });
        // Tab切换
        $('.wrap01_nav ul li').each(function () {
            $(this).off().on('click', function () {
                $(this).addClass("active").siblings().removeClass("active");
                tabsSwiper.slideTo($(this).index());
            });
        });
        // 加签
        $('#btnAddSign').off().on('click', function () {
            var iHtml = $('#divAddSign').html();
            //页面层
            layer.open({
                type: 1,
                content: iHtml,
                // shadeClose: false,
                fixed: false,
                success: function (elem) {
                    $.PkurgMobile.FormDetail.Fun.InitAddSignHistory();
                    // 确定加签
                    $('.layermchild #btnSignOK').off().on('click', function () {
                        // 获取选中用户
                        var selectUserAccount = $("input[type='radio']:checked").attr('user_account');
                        var selectUserName = $("input[type='radio']:checked").attr('user_name');
                        $.PkurgMobile.FormDetail.Fun.AddSign(selectUserAccount, selectUserName);
                    });
                    // 关闭
                    $('.layermchild #btnClose').off().on('click', function () {
                        layer.closeAll();
                    });
                    // 搜索
                    $('.layermchild #btnSearch').off().on('click', function () {
                        var keyWord = $('.layermchild #iptUserAccount').val();
                        if (keyWord == null || keyWord == undefined || $.trim(keyWord) == '') {
                            layer.open({
                                content: '请输入用户的域账号、姓名或姓名全拼进行查询',
                                //style: 'background-color:#09C1FF; color:#fff; border:none;',
                                time: 2
                            });
                            return false;
                        }
                        $('.layermchild #btnNextPage').hide();
                        $('.layermchild #btnLastPage').hide();
                        $('.layermchild #tbUser').html('');
                        var iPageSize = $.PkurgMobile.FormDetail.Fun.GetPageSize();
                        var iData = { "keyword": keyWord, pageIndex: 0, pagesize: iPageSize };
                        $.ajax({
                            type: "POST",
                            url: activeUrl + "app.ashx?action=getuserlist&ref=mobile",
                            dataType: "jsonp",
                            jsonp: "callback",
                            jsonpCallback: "handler",
                            data: iData,
                            success: function (dataUser) {
                                if (dataUser != null && dataUser != undefined && dataUser.length > 0) {
                                    var htmlUser = '';
                                    $.each(dataUser, function (i, v) {
                                        htmlUser += '<tr style="cursor:default;" class="trUser">';
                                        htmlUser += '<td>';
                                        htmlUser += '<div><input type="radio" name="selectuser" user_account="' + v.LoginName + '"  user_name="' + v.EmployeeName + '" />' + v.EmployeeName + ' (' + v.LoginName + ')</div>';
                                        htmlUser += '<div style="padding-left:20px;padding-top:4px;">【' + v.CompanyName + '】-【' + v.DepartName + '】</div>';
                                        htmlUser += '</td>';
                                        htmlUser += '</tr>';
                                    });
                                    $('.layermchild #tdHead').html('用户列表');
                                    $('.layermchild #tbUser').html(htmlUser);
                                    $.PkurgMobile.FormDetail.Fun.InitTrUserEvents();
                                    $('#hdPageIndex').val(0);
                                    if (dataUser.length >= iPageSize) {
                                        $('.layermchild #btnNextPage').show();
                                        $.PkurgMobile.FormDetail.Fun.NexPageUser();
                                    }
                                } else {
                                    layer.open({
                                        content: '查询不到相关信息',
                                        time: 2
                                    });
                                }
                            },
                            error: function (a, b, c, d, e) {
                                alert('获取失败！');
                            }
                        });
                    });
                },
                anim: 0,
                style: 'position:fixed;left:0;bottom:0;width:100%; height:100%; padding:10px 0; border:none;'
            });
        });

    };

    // 页面加载完成后执行
    $(document).ready(function () {
        $.PkurgMobile.FormDetail.Fun.InitPage();
    });
})(jQuery);