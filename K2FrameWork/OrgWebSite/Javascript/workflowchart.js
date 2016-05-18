/************
**生成流程图**
************/
(function () {
    // 创建WFChart构造方法的闭包和全局引用
    var WFChart = window.WFChart = function (id) {
        return new wfchart(id);
    };

    var WFNode = window.WFNode = function (nodeType) {
        return new wfnode(nodeType);
    };

    //初始化会签节点
    var CSNode = window.CSNode = function (wfpos) {
        return new csnode(wfpos);
    };

    /*流程图类*/
    var wfchart = function (id) {
        var self = this;

        //流程图区域(绘图区)
        var wfcPanel = document.getElementById(id);

        //流程中所含节点数组
        var arrayNodes = new Array();

        //插入一个节点
        this.InsertAt = function (node, pos) {
            if (pos) {
                arrayNodes.InsertAt(node, pos);
            } else {
                arrayNodes.InsertAt(node, arrayNodes.length);
            }
        };

        //删除一个节点
        this.RemoveAt = function (pos) {
            arrayNodes.splice(pos, 1);
        };

        //清除数组
        this.Clear = function () {
            arrayNodes.length = 0;
        };

        //从一个数组copy元素到对象中
        //        this.CopyArray = function (fa) {
        //            for (var i = 0; i < fa.length; i++) {
        //                arrayNodes.push(fa[i]);
        //            }
        //        };

        //取得一个节点
        this.getNode = function (pos) {
            return arrayNodes[pos];
        };

        //选中某个CheckBox
        this.setCheckBox = function (pos) {
            var node = arrayNodes[pos];
            node.isChekced = !node.isChekced;
        };

        //设置是否展开
        this.setOpen = function (pos, ipn) {
            arrayNodes[pos].isOpen = ipn;
            if (document.getElementById('CDF1_hfjqFlowChart'))
                document.getElementById('CDF1_hfjqFlowChart').value = JSON.stringify(arrayNodes);
            else
                document.getElementById('hfjqFlowChart').value = JSON.stringify(arrayNodes);
        };

        //取得当前节点的index
        this.getCurrentIndex = function () {
            for (var i = 0; i < arrayNodes.length; i++) {
                if (arrayNodes[i].nodeState == 1) {
                    return i;
                }
            }
            return -1;
        };

        /*
        ** 序列化 **
        ** 参数：obj为要序列化的对象；serId为保存序列化值的HiddenField的ID
        */
        this.Objserialize = function (obj, serId) {
            document.getElementById(serId).value = JSON.stringify(arrayNodes);
        };

        //绘制流程图
        this.paint = function () {
            document.getElementById(id).innerHTML = '';     //清空
            var tbl = document.getElementById('tblChart');  //取得流程列表
            if (tbl != null) {
                while (tbl.rows.length > 1) {
                    tbl.deleteRow(1);
                }
            }


            //绘制开始节点
            if (tbl != null) {
                var newRow = tbl.insertRow(-1);    //添加一行
                for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                    var newCell = newRow.insertCell(i);     //添加列
                    if (i == 0) {
                        newCell.innerHTML = '<label title="' + (tbl.rows.length - 1) + '">' + (tbl.rows.length - 1) + '</label>';
                    }
                    else if (i == 1) {
                        var applicant = document.getElementById('CDF1_ApplicantName').value.split('(');

                        newCell.innerHTML = '<label title="审批人">' + applicant[0] + '</label>';
                    }
                    else if (i == 2) {
                        newCell.innerHTML = '<label title="节点名称">申请者</label>';
                    }
                    else if (i == 3) {
                        newCell.innerHTML = '<label title="状态">提交申请</label>';
                    }
                    else if (i == 4) {
                        newCell.innerHTML = '<label></label>';
                    }
                }
            }


            //循环绘制节点
            for (var i = 0; i < arrayNodes.length; i++) {
                var Id = 'wfnode_' + i;
                arrayNodes[i].paintNode(id, Id, i);
                arrayNodes[i].paintList('tblChart', i);
            }

            //绘制结束节点
            if (tbl != null) {
                var newRow = tbl.insertRow(-1);    //添加一行
                for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                    var newCell = newRow.insertCell(i);     //添加列
                    if (i == 0) {
                        newCell.innerHTML = '<label title="' + (tbl.rows.length - 1) + '">' + (tbl.rows.length - 1) + '</label>';
                    }
                    else if (i == 1) {
                        newCell.innerHTML = '<label title="审批人">结束</label>';
                    }
                    else if (i == 2) {
                        newCell.innerHTML = '<label title="节点名称">结束</label>';
                    }
                    else if (i == 3) {
                        newCell.innerHTML = '<label title="审批"></label>';
                    }
                    else if (i == 4) {
                        newCell.innerHTML = '<label></label>';
                    }
                }
            }
        }

        /*
        ** 初始化List列表 **
        ** 参数node：节点类
        ** 参数tbID：TableID
        ** 参数pos：数组位置
        */
        function setEmpList(node, tbId, pos) {
            if (node.username && node.usercode && node.deptname && node.deptcode) {
                var nameList = node.username.split(';');
                var codeList = node.usercode.split(';');
                var deptnameList = node.deptname.split(';');
                var deptcodeList = node.deptcode.split(';');

                var tbl = document.getElementById(tbId);     //取得table
                for (var i = 0; i < nameList.length; i++) {
                    if (codeList[i] != '' && nameList[i] != '' && deptnameList[i] != '' && deptcodeList[i] != '') {
                        if (tbl != null) {
                            var newRow = tbl.insertRow(-1);    //添加一行
                            for (var j = 0; j < tbl.rows[0].cells.length; j++) {
                                var newCell = newRow.insertCell(j);  //成本中心
                                if (j == 0) {
                                    newCell.innerHTML = nameList[i];
                                }
                                else if (j == 1) {
                                    newCell.innerHTML = deptnameList[i];
                                }
                                else if (j == 2) {
                                    newCell.innerHTML = codeList[i];
                                    newCell.style.display = "none";
                                }
                                else if (j == 3) {
                                    newCell.innerHTML = deptcodeList[i];
                                    newCell.style.display = "none";
                                }
                                else if (j == 4) {
                                    newCell.innerHTML = '<img src="../../../img/delete.gif" onclick="deleteUser(' + i + ',' + pos + ')"/>';
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /*流程的节点*/
    var wfnode = function (nodeType) {

        //当前节点唯一标示GUID
        this.nodeId = newGuid();

        //是否被选中
        this.isChekced = false;

        //是否是会签
        this.isCounterSign = false;

        //所处状态(0表示已走过的节点、1表示当前节点、2表示刚创建的节点、3表示未审批的节点）
        this.nodeState = 2;

        //标题文字
        this.ftitle = '审批节点';

        //用户名
        this.username = '';

        //用户编码
        this.usercode = '';

        //部门名称
        this.deptname = '';

        //部门代码
        this.deptcode = '';

        //会签节点数组
        this.arrayCounter = new Array();

        //是否查看、默认为False
        this.isView = false;

        //是否展开 （会前节点是否展开默认为展开）
        this.isOpen = true;

        if (nodeType)
            this.nodeType = nodeType;
        else
            this.nodeType = 'User';

        //角色代码
        this.roleCode = '';

        //角色名称
        this.roleName = '';

        //节点地址
        this.URL = 'Process/CDF/Approve.aspx';

        /*
        ** 绘制节点 **
        ** 参数paintId:画板ID;Id:tableId
        */
        this.paintNode = function (paintId, tableId, pos) {
            //设置两个会签节点的ID
            var countedTableId, counteringTableId;
            countedTableId = tableId + '_0';
            counteringTableId = tableId + '_1';
            if (this.nodeState == 0) {
                //已完成节点
                document.getElementById(paintId).innerHTML += paintApprovedNode(tableId, pos, this.isView, this.username, this.deptname, this.arrayCounter, this.isOpen, this.nodeType, this.roleName, this.roleCode);

                //设置会签节点
                var tbl = document.getElementById('counter_' + pos);
                if (tbl != null) {
                    setCounterList(tbl, this.arrayCounter, pos, false, this.isOpen);
                }
            }
            else if (this.nodeState == 1) {
                //当前节点
                document.getElementById(paintId).innerHTML += paintCurrentNode(tableId, pos, this.isView, this.username, this.deptname, this.arrayCounter, this.isOpen, this.nodeType, this.roleName, this.roleCode);
                var tbl = document.getElementById('counter_' + pos);
                if (tbl != null) {
                    setCounterList(tbl, this.arrayCounter, pos, true, this.isOpen);
                }
            }
            else if (this.nodeState == 2) {
                //初始化节点（发起）
                document.getElementById(paintId).innerHTML += paintInitNode(tableId, pos, this.isChekced, this.username, this.deptname, this.nodeType, this.roleName, this.roleCode);
            }
            else if (this.nodeState == 3) {
                //未审批节点
                document.getElementById(paintId).innerHTML += paintUnApproveNode(tableId, pos, this.isView, this.username, this.deptname, this.nodeType, this.roleName, this.roleCode);
            }
        };


        /*
        ** 绘制列表行 **
        ** 参数tableID：列表ID
        */
        this.paintList = function (tableId, pos) {
            if (this.nodeState == 0) {
                //已完成节点
                paintApprovedRow(tableId, pos, this.isView, this.username, this.deptname, this.arrayCounter, this.isOpen, this.nodeType, this.roleName, this.roleCode);
            }
            else if (this.nodeState == 1) {
                //当前节点
                paintCurrentRow(tableId, pos, this.isView, this.username, this.deptname, this.arrayCounter, this.isOpen, this.nodeType, this.roleName, this.roleCode);
            }
            else if (this.nodeState == 2) {
                //初始化节点（发起）
                paintInitRow(tableId, pos, this.isChekced, this.username, this.deptname, this.nodeType, this.roleName, this.roleCode);
            }
            else if (this.nodeState == 3) {
                //未审批节点
                paintUnApproveRow(tableId, pos, this.isView, this.username, this.deptname, this.nodeType, this.roleName, this.roleCode);
            }
        };


        /*设置节点的人员信息*/
        this.setNodeValue = function (username, usercode, deptname, deptcode, nodeType, roleName, roleCode) {
            this.username = username;
            this.usercode = usercode;
            this.deptname = deptname;
            this.deptcode = deptcode;
            this.nodeType = nodeType;
            this.roleCode = roleCode;
            this.roleName = roleName;
        };

        /*删除行*/
        this.deleteNodeValue = function (rowIndex) {
            var tmpusercode = new Array();
            var tmpusername = new Array();
            var tmpdeptname = new Array();
            var tmpdeptcode = new Array();

            tmpusercode = this.usercode.split(';');
            tmpusername = this.username.split(';');
            tmpdeptname = this.deptname.split(';');
            tmpdeptcode = this.deptcode.split(';');

            tmpusercode.removeAt(rowIndex);
            tmpusername.removeAt(rowIndex);
            tmpdeptname.removeAt(rowIndex);
            tmpdeptcode.removeAt(rowIndex);

            //清空信息
            this.username = this.usercode = this.deptname = this.deptcode = '';

            for (var i = 0; i < tmpusercode.length; i++) {
                this.usercode += tmpusercode[i] + ';';
                this.username += tmpusername[i] + ';';
                this.deptcode += tmpdeptcode[i] + ';';
                this.deptname += tmpdeptname[i] + ';';
            }
        };

        //设置节点的会签节点
        this.setCounterArray = function (csnode, cspos) {
            for (var i = 0; i < this.arrayCounter.length; i++) {
                if (this.arrayCounter[i].counterSignCodes == csnode.counterSignCodes && this.arrayCounter[i].enabled) {
                    return false;
                }
            }
            this.arrayCounter.InsertAt(csnode, cspos);
            return true;
        };

        this.clearCounterArray = function () {
            this.arrayCounter.length = 0;
        };

        //取得会签节数组长度
        this.getCounterArrayLength = function () {
            return this.arrayCounter.length;
        };

        //删除会签节点
        this.delCounter = function (cspos) {
            this.arrayCounter.removeAt(cspos);
        };

        //判断节点是否存在可用的会签节点
        this.ExistEnableCounterNode = function () {
            var ret = false;
            for (var i = 0; i < this.arrayCounter.length; i++) {
                if (this.arrayCounter[i].enabled) {
                    ret = true;
                    break;
                }
            }
            return ret;
        };

        //计算字符串长度并截取
        function SubstringLength(str, subLength) {
            var count = 0;
            var pos = 0;
            for (var i = 0; i < str.length; i++) {
                if (count > subLength) {
                    return str.substring(0, pos) + '...';
                }
                else {
                    var re = new RegExp("[^\x00-\xff]", "g");
                    if (re.test("阿")) {
                        count += 2;
                        ++pos;
                    }
                    else {
                        ++count;
                        ++pos;
                    }
                }
            }
            return str;
        }

        //绘制初始化节点（发起）
        function paintInitNode(tableId, pos, isChecked, username, deptname, nodeType, roleName, roleCode) {
            if (nodeType == 'User') {
                return '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"><div style="text-align: left; float:left;  font-weight:bold; padding-top:4px; ">&nbsp;' + username + '</div><div style="text-align: right;"><img src="../../pic/20110907034550102_easyicon_cn_24.png" width="16" height="16" onclick="SelectU(' + pos + ');" style="cursor:pointer;" />&nbsp;&nbsp;<img src="../../pic/icon_delete.png" width="16" height="16" onclick="DelNode(' + pos + ');" style="cursor:pointer;" /></div><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom" ><img src="../../pic/icon_add.png" onclick="SelectUAdd(' + pos + ');" style="cursor:pointer;" /></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg">&nbsp<span style="padding-top:0px;" title="' + deptname + '">' + SubstringLength(deptname, 20) + '</span></td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            }
            else if (nodeType == 'Role') {
                return '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"><div style="text-align: left; float:left;  font-weight:bold; padding-top:4px; ">&nbsp;角色</div><div style="text-align: right;"><img src="../../pic/20110907034550102_easyicon_cn_24.png" width="16" height="16" onclick="SelectU(' + pos + ');" style="cursor:pointer;" />&nbsp;&nbsp;<img src="../../pic/icon_delete.png" width="16" height="16" onclick="DelNode(' + pos + ');" style="cursor:pointer;" /></div><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom" ><img src="../../pic/icon_add.png" onclick="SelectUAdd(' + pos + ');" style="cursor:pointer;" /></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg">&nbsp<span style="padding-top:0px;" title="' + roleName + '">' + SubstringLength(roleName, 20) + '</span></td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            }
            else if (nodeType == 'Solid') {
                if (roleCode == undefined || roleName == undefined) {
                    roleCode = '';
                    roleName = '';
                }
                return '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"><div style="text-align: left; float:left;  font-weight:bold; padding-top:4px; ">&nbsp;固定角色节点</div><div style="text-align: right;"></div><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom" ></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg">&nbsp<span style="padding-top:0px;" title="' + roleName + '">' + SubstringLength(roleName, 20) + '</span></td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            }
        }

        //绘制初始化节点行（发起）
        function paintInitRow(tableId, pos, isChecked, username, deptname, nodeType, roleName, roleCode) {
            if (nodeType == 'User') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">' + username + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            if (document.location.href.indexOf('Approve.aspx') >= 0)
                                newCell.innerHTML = '<label title="审批">加签者</label>';
                            else
                                newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            if (username == '') {
                                newCell.innerHTML = '<a href="javascript:SelectU(' + pos + ')">添加审批人</a>&nbsp;<a href="javascript:SelectUAdd(' + pos + ')">添加节点</a>&nbsp;<a href="javascript:DelNode(' + pos + ')">删除节点</a>';
                            }
                            else {
                                newCell.innerHTML = '<a href="javascript:SelectU(' + pos + ')">修改审批人</a>&nbsp;<a href="javascript:SelectUAdd(' + pos + ')">添加节点</a>&nbsp;<a href="javascript:DelNode(' + pos + ')">删除节点</a>';
                            }
                        }
                    }
                }
            }
            else if (nodeType == 'Role') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="' + roleName + '">' + SubstringLength(roleName, 15) + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            if (document.location.href.indexOf('Approve.aspx') >= 0)
                                newCell.innerHTML = '<label title="审批">加签者</label>';
                            else
                                newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            if (roleName == '') {
                                newCell.innerHTML = '<a href="javascript:SelectU(' + pos + ')">添加审批人</a>&nbsp;<a href="javascript:SelectUAdd(' + pos + ')">添加节点</a>&nbsp;<a href="javascript:DelNode(' + pos + ')">删除节点</a>';
                            }
                            else {
                                newCell.innerHTML = '<a href="javascript:SelectU(' + pos + ')">修改审批人</a>&nbsp;<a href="javascript:SelectUAdd(' + pos + ')">添加节点</a>&nbsp;<a href="javascript:DelNode(' + pos + ')">删除节点</a>';
                            }
                        }
                    }
                }
            }
            else if (nodeType == 'Solid') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">固定角色节点</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            if (document.location.href.indexOf('Approve.aspx') >= 0)
                                newCell.innerHTML = '<label title="审批">加签者</label>';
                            else
                                newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
        }

        //绘制已审批节点
        function paintApprovedNode(tableId, pos, isView, username, deptname, counterList, isOpen, nodeType, roleName, roleCode) {
            var retVal = '';
            //先判断是否有会签
            if (counterList.length != 0) {
                if (isOpen) {
                    retVal = '<div style="font-size: 12px;"><table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="width: 196px; background: url(../../pic/huiqian_yiwancheng_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight: 700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowup.png" style="cursor: pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_yiwancheng_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div></div>';
                }
                else {
                    retVal = '<div style="font-size: 12px;"><table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="width: 196px; background: url(../../pic/huiqian_yiwancheng_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight: 700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowdown.png" style="cursor: pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_yiwancheng_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div></div>';
                }
            }
            if (nodeType == 'User')
                retVal += '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/green_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/green_center1.jpg" style=" font-weight:bold;">&nbsp;' + username + '</td><td width="8" rowspan="3"><img src="../../pic/green_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/green_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/green_center3.jpg" title="' + deptname + '">&nbsp' + SubstringLength(deptname, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            else if (nodeType == 'Role')
                retVal += '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/green_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/green_center1.jpg" style=" font-weight:bold;">&nbsp;角色</td><td width="8" rowspan="3"><img src="../../pic/green_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/green_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/green_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            else if (nodeType == 'Solid')
                retVal += '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/green_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/green_center1.jpg" style=" font-weight:bold;">&nbsp;固定角色节点</td><td width="8" rowspan="3"><img src="../../pic/green_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/green_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/green_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            return retVal;
        }

        /*
        ****绘制已申请列表
        */
        function paintApprovedRow(tableId, pos, isView, username, deptname, counterList, isOpen, nodeType, roleName, roleCode) {
            var retVal = '';
            //先判断是否有会签
            if (counterList.length != 0) {
                if (isOpen) {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        for (var j = 0; j < counterList.length; j++) {      //循环取得会签人
                            var newRow = tbl.insertRow(-1);    //添加一行
                            for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                                var newCell = newRow.insertCell(i);     //添加列
                                if (i == 0) {
                                    newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                                }
                                else if (i == 1) {
                                    newCell.innerHTML = '<label title="审批人">' + counterList[j].counterSignNames + '</label>';
                                }
                                else if (i == 2) {
                                    newCell.innerHTML = '<label title="节点名称">会签者</label>';
                                }
                                else if (i == 3) {
                                    newCell.innerHTML = '<label title="审批">已确认</label>';
                                }
                                else if (i == 4) {
                                    newCell.innerHTML = '<label></label>';
                                }
                            }
                        }
                    }
                }
                else {
                    retVal = '';
                }
            }
            if (nodeType == 'User') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">' + username + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">审批通过</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
            else if (nodeType == 'Role') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="' + roleName + '">' + SubstringLength(roleName, 15) + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">审批通过</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
            else if (nodeType == 'Solid') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">固定角色节点</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">审批通过</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
        }

        //绘制当前节点
        function paintCurrentNode(tableId, pos, isView, username, deptname, counterList, isOpen, nodeType, roleName, roleCode) {
            var retVal = '';
            //先判断是否有会签
            if (counterList.length != 0) {
                var state = false;      //标示是否全所有会签都结束。false表示结束
                for (var k = 0; k < counterList.length; k++) {
                    if (counterList[k].enabled) {
                        state = true;
                        break;
                    }
                }
                if (isOpen) {
                    if (state)
                        retVal = '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="font-size:12px;"><div style="width: 196px; background: url(../../pic/huiqian_dangqian_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight:700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowup.png" style="cursor:pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_dangqian_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div></div>';
                    else
                        retVal = '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="width: 196px; background: url(../../pic/huiqian_yiwancheng_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight: 700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowup.png" style="cursor: pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_yiwancheng_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div>';
                }
                else {
                    if (state)
                        retVal = '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="font-size:12px;"><div style="width: 196px; background: url(../../pic/huiqian_dangqian_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight:700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowdown.png" style="cursor:pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_dangqian_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div></div>';
                    else
                        retVal = '<table style="font-size:12px; text-align:left;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td><div style="width: 196px; background: url(../../pic/huiqian_yiwancheng_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight: 700;" align="left">会签</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowdown.png" style="cursor: pointer;" onclick="setElementStatus(this,' + pos + ');" /></tr></table></div><table id="counter_' + pos + '" width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_yiwancheng_xia.jpg) no-repeat;"></div></td><td style="width:32px;"></td></tr></table><div style="width:224px; text-align:center; margin-left:-32px;"><img src="../../pic/arrow.png" /></div>';
                }
            }
            if (isView) {
                if (nodeType == 'User')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;' + username + '</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + deptname + '">&nbsp' + SubstringLength(deptname, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
                else if (nodeType == 'Role')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;角色</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
                else if (nodeType == 'Solid')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;固定角色节点</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            }
            else {
                if (nodeType == 'User')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;' + username + '</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"><img src="../../pic/icon_add.png" onclick="CounterOrNodeControl(' + pos + ');" style="cursor:pointer;" /></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + deptname + '">&nbsp' + SubstringLength(deptname, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
                else if (nodeType == 'Role')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;角色</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"><img src="../../pic/icon_add.png" onclick="CounterOrNodeControl(' + pos + ');" style="cursor:pointer;" /></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
                else if (nodeType == 'Solid')
                    retVal += '<table  style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/yellow_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/yellow_center1.jpg"><div style="text-align:left; font-weight:bold; padding-top:3px;">&nbsp;固定角色节点</div></td><td width="8" rowspan="3"><img src="../../pic/yellow_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3" valign="bottom"></td></tr><tr><td height="2"><img src="../../pic/yellow_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/yellow_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            }
            return retVal;
        }

        //绘制当前节点（行）
        function paintCurrentRow(tableId, pos, isView, username, deptname, counterList, isOpen, nodeType, roleName, roleCode) {
            //先判断是否有会签
            if (counterList.length != 0) {
                if (isOpen) {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        for (var j = 0; j < counterList.length; j++) {
                            var newRow = tbl.insertRow(-1);    //添加一行
                            for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                                var newCell = newRow.insertCell(i);     //添加列
                                if (i == 0) {
                                    newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                                }
                                else if (i == 1) {
                                    newCell.innerHTML = '<label title="审批人">' + counterList[j].counterSignNames + '</label>';
                                }
                                else if (i == 2) {
                                    newCell.innerHTML = '<label title="节点名称">会签者</label>';
                                }
                                else if (i == 3) {
                                    if (counterList[j].enabled)
                                        newCell.innerHTML = '<label title="审批">未确认</label>';
                                    else
                                        newCell.innerHTML = '<label title="审批">已确认</label>';
                                }
                                else if (i == 4) {
                                    if (counterList[j].enabled)
                                        newCell.innerHTML = '<a href="javascript:delMeetSign(' + j + ',' + pos + ')">删除</a>';
                                    else
                                        newCell.innerHTML = '<label></label>';
                                }
                            }
                        }
                    }
                }
            }
            if (isView) {
                if (nodeType == 'User') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="审批人">' + username + '</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label></label>';
                            }
                        }
                    }
                }
                else if (nodeType == 'Role') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="' + roleName + '">' + SubstringLength(roleName, 15) + '</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label></label>';
                            }
                        }
                    }
                }
                else if (nodeType == 'Solid') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="审批人">固定角色节点</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label></label>';
                            }
                        }
                    }
                }
            }
            else {
                if (nodeType == 'User') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="审批人">' + username + '</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label><a id = "addTableRow" href="javascript:document.getElementById(\'hfIsMeet\').value = \'false\';CounterOrNodeControl(' + pos + ');">加签</a>&nbsp;<a id = "addTableRowConter" href="javascript:document.getElementById(\'hfIsMeet\').value = \'true\';CounterOrNodeControl(' + pos + ');">会签</a></label>';
                            }
                        }
                    }
                }
                else if (nodeType == 'Role') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="' + roleName + '">' + SubstringLength(roleName, 15) + '</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label><a href="document.getElementById("hfIsMeet").value = "false";CounterOrNodeControl(' + pos + ');">加签</a>&nbsp;<a href="document.getElementById("hfIsMeet").value = "true";CounterOrNodeControl(' + pos + ');">会签</a></label>';
                            }
                        }
                    }
                }
                else if (nodeType == 'Solid') {
                    var tbl = document.getElementById(tableId);     //取得table
                    if (tbl != null) {
                        var newRow = tbl.insertRow(-1);    //添加一行
                        for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                            var newCell = newRow.insertCell(i);     //添加列
                            if (i == 0) {
                                newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                            }
                            else if (i == 1) {
                                newCell.innerHTML = '<label title="审批人">固定角色节点</label>';
                            }
                            else if (i == 2) {
                                newCell.innerHTML = '<label title="节点名称">审批者</label>';
                            }
                            else if (i == 3) {
                                newCell.innerHTML = '<label title="审批">正在审批</label>';
                            }
                            else if (i == 4) {
                                newCell.innerHTML = '<label></label>';
                            }
                        }
                    }
                }
            }
        }

        //绘制流未审批节点
        function paintUnApproveNode(tableId, pos, isView, username, deptname, nodeType, roleName, roleCode) {
            if (nodeType == 'User')
                return '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"  style="font-weight:bold; text-align:left;">&nbsp;' + username + '</td><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg" title="' + deptname + '">&nbsp' + SubstringLength(deptname, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            else if (nodeType == 'Role')
                return '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"  style="font-weight:bold; text-align:left;">&nbsp;角色</td><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
            else if (nodeType == 'Solid')
                return '<table style="text-align:left; font-size:12px;" width="224" border="0" cellspacing="0" cellpadding="0" height="62"><tr><td width="24" rowspan="3"><img src="../../pic/gray_left.jpg" width="24" height="62" /></td><td width="160" height="31" background="../../pic/gray_center1.jpg"  style="font-weight:bold; text-align:left;">&nbsp;固定角色节点</td><td width="8" rowspan="3"><img src="../../pic/gray_right.jpg" width="8" height="62" /></td><td width="32" rowspan="3"></td></tr><tr><td height="2"><img src="../../pic/gray_center2.jpg" width="164" height="2" /></td></tr><tr><td height="29" background="../../pic/gray_center3.jpg" title="' + roleName + '">&nbsp' + SubstringLength(roleName, 20) + '</td></tr><tr><td colspan="3" align="center"><img src="../../pic/arrow.png" /></td></tr></table>';
        }


        //绘制未审批节点
        function paintUnApproveRow(tableId, pos, isView, username, deptname, nodeType, roleName, roleCode) {
            if (nodeType == 'User') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">' + username + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
            else if (nodeType == 'Role') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="' + roleName + '">' + SubstringLength(roleName, 15) + '</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
            else if (nodeType == 'Solid') {
                var tbl = document.getElementById(tableId);     //取得table
                if (tbl != null) {
                    var newRow = tbl.insertRow(-1);    //添加一行
                    for (var i = 0; i < tbl.rows[0].cells.length; i++) {
                        var newCell = newRow.insertCell(i);     //添加列
                        if (i == 0) {
                            newCell.innerHTML = '<label title="' + pos + '">' + (pos + 2) + '</label>';
                        }
                        else if (i == 1) {
                            newCell.innerHTML = '<label title="审批人">固定角色节点</label>';
                        }
                        else if (i == 2) {
                            newCell.innerHTML = '<label title="节点名称">审批者</label>';
                        }
                        else if (i == 3) {
                            newCell.innerHTML = '<label title="审批">未审批</label>';
                        }
                        else if (i == 4) {
                            newCell.innerHTML = '<label></label>';
                        }
                    }
                }
            }
        }

        //绘制会签节点
        function paintCounterNode(pos, arrayCounter, countedTableId, counteringTableId) {
            var counterHTML = '';
            for (var i = 0; i < arrayCounter.length; i++) {
                if (i == 0) {
                    counterHTML += arrayCounter[i].paintNodes(i, pos, arrayCounter[i], countedTableId, counteringTableId);
                }
            }
            return counterHTML;
        }

        //设置节点人员
        function setCounterList(tbObj, arrayCounter, wfpos, isCurrent, isOpen) {
            for (var i = 0; i < arrayCounter.length; i++) {
                var newRow = tbObj.insertRow(-1);    //添加一行
                if (isCurrent)      //当前节点
                {
                    var state = false;
                    for (var k = 0; k < arrayCounter.length; k++) {
                        if (arrayCounter[k].enabled) {
                            state = true;       //还存在未会签节点
                            break;
                        }
                    }
                    if (isOpen) {
                        if (state) {
                            //会签人
                            $(newRow).css({ 'background-color': '#ffe892' });
                            var row = newRow.insertCell(0);
                            $(row).css({ 'border-left': 'solid 1px #d8ae0e', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'inline' });
                            row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';
                        }
                        else {
                            //会签人
                            $(newRow).css({ 'background-color': '#c6ea93' });
                            var row = newRow.insertCell(0);
                            $(row).css({ 'border-left': 'solid 1px #9dbf6b', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'inline' });
                            row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';
                        }
                        //会签人部门
                        var rowDept = newRow.insertCell(1);
                        $(rowDept).css({ 'width': '105px', 'text-align': 'left', 'display': 'inline', 'overflow': 'hidden', 'text-overflow': 'ellipsis' });
                        //rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + arrayCounter[i].counterDeptName + '</span>';
                        var tmpDeptName = arrayCounter[i].counterDeptName;
                        rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + SubstringLength(tmpDeptName, 10) + '</span>';
                    }
                    else {
                        if (stat) {
                            //会签人
                            $(newRow).css({ 'background-color': '#ffe892' });
                            var row = newRow.insertCell(0);
                            $(row).css({ 'border-left': 'solid 1px #d8ae0e', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'none' });
                            row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';
                        }
                        else {
                            //会签人
                            $(newRow).css({ 'background-color': '#c6ea93' });
                            var row = newRow.insertCell(0);
                            $(row).css({ 'border-left': 'solid 1px #9dbf6b', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'none' });
                            row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';
                        }
                        //会签人部门
                        var rowDept = newRow.insertCell(1);
                        $(rowDept).css({ 'width': '105px', 'text-align': 'left', 'display': 'none', 'overflow': 'hidden', 'text-overflow': 'ellipsis' });
                        var tmpDeptName = arrayCounter[i].counterDeptName;
                        rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + SubstringLength(tmpDeptName, 30) + '</span>';
                    }
                }
                else//已完成会签节点
                {
                    if (isOpen) {
                        //会签人
                        $(newRow).css({ 'background-color': '#c6ea93' });
                        var row = newRow.insertCell(0);
                        $(row).css({ 'border-left': 'solid 1px #9dbf6b', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'inline' });
                        row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';

                        //会签人部门
                        var rowDept = newRow.insertCell(1);
                        $(rowDept).css({ 'width': '130px', 'text-align': 'left', 'overflow': 'hidden', 'text-overflow': 'ellipsis' });
                        //rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + arrayCounter[i].counterDeptName + '</span>';
                        var tmpDeptName = arrayCounter[i].counterDeptName;
                        rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + SubstringLength(tmpDeptName, 10) + '</span>';
                    }
                    else {
                        //会签人
                        $(newRow).css({ 'background-color': '#c6ea93' });
                        var row = newRow.insertCell(0);
                        $(row).css({ 'border-left': 'solid 1px #9dbf6b', 'padding-left': '10px; width:50px', 'text-align': 'left', 'padding-left': '10px', 'display': 'none' });
                        row.innerHTML = '<span style="font-weight:700;">' + arrayCounter[i].counterSignNames + '</span>';

                        //会签人部门
                        var rowDept = newRow.insertCell(1);
                        $(rowDept).css({ 'width': '130px', 'text-align': 'left', 'display': 'none' });
                        //rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + arrayCounter[i].counterDeptName + '</span>';
                        var tmpDeptName = arrayCounter[i].counterDeptName;
                        rowDept.innerHTML = '<span title="' + arrayCounter[i].counterDeptName + '">' + SubstringLength(tmpDeptName, 10) + '</span>';
                    }
                }

                //删除人操作
                if (state) {
                    if (isOpen) {
                        var op = newRow.insertCell(2);
                        $(op).css({ 'border-right': 'solid 1px #d8ae0e', 'display': 'inline' });
                        //                        $(newRow).bind('click', function ()
                        //                        {
                        //                            setMeet(i);
                        //                        });
                        //op.innerHTML = '<img src="../../Img/delete.gif" style="cursor: pointer;" onclick="delMeetSign(' + i + ',' + wfpos + ')" />';
                    }
                    else {
                        var op = newRow.insertCell(2);
                        $(op).css({ 'border-right': 'solid 1px #d8ae0e', 'display': 'none' });
                        //                        $(newRow).bind('click', function ()
                        //                        {
                        //                            setMeet(i);
                        //                        });
                        op.innerHTML = '<img src="../../Img/delete.gif" style="cursor: pointer;" onclick="delMeetSign(' + i + ',' + wfpos + ')" />';
                    }
                }
                else {
                    if (isOpen) {
                        var emptyOp = newRow.insertCell(2);
                        $(emptyOp).css({ 'border-right': 'solid 1px #9dbf6b', 'display': 'inline' });
                        emptyOp.innerHTML = '';
                    }
                    else {
                        var emptyOp = newRow.insertCell(2);
                        $(emptyOp).css({ 'border-right': 'solid 1px #9dbf6b', 'display': 'none' });
                        emptyOp.innerHTML = '';
                    }
                }
            }
        }

        //生成GUID
        function newGuid() {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        }
    }


    /*会签节点类*/
    var csnode = function (wfpos) {

        //当前会签节点唯一标示GUID
        var csnodeGuid = newGuid();

        //是否被选中
        this.isChecked = false;

        //所处状态（0表示已会签、1表示当前会签人、2表示未会签）
        this.csState = 2;

        //标题
        this.csTitle = '会签';

        //会签人Code
        this.counterSignCodes = '';

        //会签人姓名
        this.counterSignNames = '';

        //会签人部门
        this.counterDeptName = '';

        //会签人部门代码
        this.counterDeptCode = '';

        //是否可用
        this.enabled = true;

        //取得绘制位置
        function getPaintPos(wfpos) {
            leftPos = $('#wfnode_' + wfpos).offset().left;
            topPos = $('#wfnode_' + wfpos).offset().top;
            wfwidth = 150;
            return { left: leftPos, top: topPos, width: wfwidth };
        };

        //绘制节点（counterpos:表示是第几个会签，wfpos：表示是第几个审批节点，csNode表示会签类）
        this.paintNodes = function (counterpos, wfpos, csNode, isView) {
            return '<div style="font-size:12px;"><div style="width: 196px; background: url(../../pic/huiqian_dangqian_tou.jpg) no-repeat; height: 37px;"><table style="width: 100%; height: 100%" border="0" cellpadding="0" cellpadding="0" cellspacing="0"><tr><td width="25"></td><td valign="middle" style="font-weight:700;" align="left">' + this.csTitle + '</td><td width="50"></td><td valign="top" align="right" style="padding-top: 10px; padding-right: 8px;"><img src="../../pic/icon_arrowup.png" style="cursor:pointer;" onclick="" /></tr></table></div><table width="196" cellpadding="0" cellpadding="0" cellspacing="0"></table><div style="width: 196px; height: 7px; background: url(../../pic/huiqian_dangqian_xia.jpg) no-repeat;"></div></div>';
        };

        //生成GUID
        function newGuid() {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        }
    }
})();