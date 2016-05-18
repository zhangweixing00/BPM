<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessNodeEdit.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.ProcessNodeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程节点编辑</title>
    <base target="_self" />
    <link href="/Styles/StyleBatchManage.css" rel="stylesheet" type="text/css" />
    <link href="/JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="/JavaScript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
    <script src="/JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.alert = function (msg) {
            ymPrompt.alert({ title: '提示信息', message: msg })
        }
        function AlertAndNewLoad(msg) {
            ymPrompt.alert({ title: '提示信息', message: msg, handler: function ConFirm(tp) { if (tp == 'ok') { top.window.ymPrompt.close(); top.frames[0].location.href = top.frames[0].location.href.toString().replace('#', ''); } } });
        }
    </script>
    <link href="../../../Styles/PMS/PMS.css" rel="stylesheet" type="text/css" />
    
    <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ddlcss
        {
            border: 1px #999999 solid;
        }
        #RoleAddTitle
        {
            width: 770px;
            margin: 10px 0 0 20px;
            height: 10px;
            padding: 10px;
            color: #76650b;
            font-weight: bold;
            background: url(../../../pic/right_list_title_bg2.png) no-repeat;
        }
        .table_title
        {
            width:25%;
            text-align: right;
        }
        .table_content
        {
            width: 65%;
            text-align: left;
        }
        .table_xing
        {
            width: 10%;
        }
        .txtcss
        {
            padding-top: 4px;
            height: 18px;
            border: 1px #999999 solid;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            WeightedChange();
        });
        function checkInput() {
            if ($('#txtNodeName').val() == '') {
                alert('请输入节点名称');
                return false;
            }
            if ($('#txtURL').val() == '') {
                alert('请输入节点URL');
                return false;
            }
            if ($('#txtOrderNo').val() == '') {
                alert('请输入节点排序号');
                return false;
            }
            var ddlVal = $('#ddlWeighted').val();   //取得加权方式
            if (ddlVal == 'P') {
                if ($('#txtSamplingRate').val() == '') {
                    alert('请输入取样率');
                    return false;
                }
                if (isNaN($('#txtSamplingRate').val())) {
                    alert('取样率必须为数字');
                    return false;
                }
                if (!($('#txtSamplingRate').val() >= 0.1 && $('#txtSamplingRate').val() <= 1)) {
                    alert('取样率是一个大于等于0.1小于等于1的数字');
                    return false;
                }
            }

            return true;
        }


       

        //当加权下拉框change时触发
        function WeightedChange() {
            var ddlVal = $('#ddlWeighted').val();
            if (ddlVal == 'P') {
                //$('trSamplingRate').attr('style', 'display:inline');
                //$('trSamplingRate').show();
                trSamplingRate.style.display = "block"; //隱藏 
            }
            else {
                //$('trSamplingRate').attr('style', 'display:none');
                //$('trSamplingRate').hide();
                trSamplingRate.style.display = "none"; //隱藏 
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="con_main">
        <div class="topnew">
            <div class="top_text_name">
                
            </div>
            <div class="top_text_copyright">
                </div>
        </div>
        <div class="conter_main">
           
            <div >
               <table border="0" cellpadding="0"  width="100%" cellspacing="0">
               <tr>
               <td  colspan="3"></td>
               </tr>
                 <tr>
               <td  colspan="3"></td>
               </tr>
            <tr>
                <td class="table_title">
                    节点名称：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtNodeName" runat="server"  Width="340px" CssClass="txtcss" MaxLength="50"
                        ToolTip=""></asp:TextBox>
                </td>
                <td class="table_xing">
                    <div style="color: Red;" id="div1" runat="server">
                        *</div>
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr style=" display:none;">
                <td class="table_title">
                    节点地址：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtURL" runat="server"   Width="340px" CssClass="txtcss" text="Process/Delivery/Approve.aspx?SN="  MaxLength="100"
                        ToolTip=""></asp:TextBox>
                </td>
                <td class="table_xing">
                    <div style="color: Red;" id="div5" runat="server">
                        *</div>
                </td>
            </tr>
        
            <tr>
                <td class="table_title">
                    退回节点：
                </td>
                <td class="table_content">
                    <asp:DropDownList ID="ddlWayBack" runat="server" DataTextField="NodeName" DataValueField="NodeID">
                    </asp:DropDownList>
                </td>
                <td class="table_xing">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="table_title">
                    节点加权类型：
                </td>
                <td class="table_content">
                    <asp:DropDownList ID="ddlWeighted" runat="server" Width="140px">
                        <asp:ListItem Value="N">默认审批模式</asp:ListItem>
                       <%-- <asp:ListItem Value="R">一人审批即通过</asp:ListItem>
                        <asp:ListItem Value="P">配置取样率</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td class="table_xing">
                    <div style="color: Red;" id="div7" runat="server">
                        *</div>
                </td>
            </tr>
            <tr id="trSamplingRate">
                <td class="table_title">
                    取样率(0.1-1)：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtSamplingRate" runat="server" MaxLength="5" Width="80px"></asp:TextBox>
                </td>
                <td class="table_xing">
                    <div style="color: Red;" id="div8" runat="server">
                        *</div>
                </td>
            </tr>
            <tr>
                <td class="table_title">
                    排序号：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtOrderNo" runat="server" Width="80px" onkeyup="this.value=this.value.replace(/\D/g,'')"
                        onafterpaste="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                </td>
                <td class="table_xing">
                    <div style="color: Red;" id="div6" runat="server">
                        *</div>
                </td>
            </tr>
          
            <tr>
                <td class="table_title">
                    选择所属部门：
                </td>
                <td class="table_content" >
                    <asp:DropDownList ID="ddlDept" runat="server"  ></asp:DropDownList>
                    <label style=" color:Red;">注意：所属部门如果为空，则默认为流程发起者所在部门</label>
                    <dx:aspxgridview runat="server" KeyFieldName="DepartCode" AutoGenerateColumns="False"  
        ClientInstanceName="cgvDeptList" Width="100%"  ID="gvDeptList"   
                        OnAfterPerformCallback="gvDeptList_AfterPerformCallback"  >
        <SettingsBehavior  AllowFocusedRow="true"  />
        <Columns>
           
            <dx:GridViewDataTextColumn Width="5%" Caption="序号" VisibleIndex="0">
                <HeaderStyle HorizontalAlign="Center" />
                <DataItemTemplate>
                    <%# Container.ItemIndex+1%>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn FieldName="DepartCode" Caption="部门编号" VisibleIndex="1" Width="15%">
            <Settings AutoFilterCondition="Contains"></Settings>

                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="DepartName" Caption="部门简称" Width="25%" VisibleIndex="2">
            <Settings AutoFilterCondition="Contains"></Settings>

                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <CellStyle HorizontalAlign="left">
                </CellStyle>
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="DepartFullName" Caption="部门全称" Width="45%" VisibleIndex="3">
            <Settings AutoFilterCondition="Contains"></Settings>

                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <CellStyle HorizontalAlign="left">
                </CellStyle>
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataColumn>
           
            <dx:GridViewDataColumn Caption="操作" HeaderStyle-HorizontalAlign="Left" CellStyle-HorizontalAlign="Center"
                Width="10%">
                <DataItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxButton runat="server" ID="btnEdit" Text="选择" OnClick="btnEdit_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>
                                
                                <asp:HiddenField ID="hfdeptCode" runat="server" Value='<%# Bind("DepartCode") %>'>
                                </asp:HiddenField>
                                <asp:HiddenField ID="hfdeptName" runat="server" Value='<%# Bind("DepartFullName") %>'>
                                </asp:HiddenField>
                            </td>
                        </tr>
                    </table>
                </DataItemTemplate>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataColumn>
        </Columns>
        
        <SettingsPager PageSize="15">
        </SettingsPager>
        <Settings ShowFilterRow="True"></Settings>
        
    </dx:aspxgridview>
                    </td>
                <td class="table_xing">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr style=" display:none;">
                <td class="table_title">
                    通知：
                </td>
                <td class="table_content">
                    <asp:TextBox ID="txtNotification" runat="server" TextMode="MultiLine" Width="340px"
                        Height="54px" Style="padding-top: 4px; border: 1px #999999 solid;" MaxLength="100"></asp:TextBox>
                </td>
                <td class="table_xing">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 5px;" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-top: 15px; text-align: center;">
                    <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnAffirm_over.png')"
                        onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnAffirm_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAffirm_nor.png" OnClick="btnSave_Click" OnClientClick="return checkInput();" />&nbsp;
                    <asp:ImageButton ID="btnCancel" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnCancel','../../../pic/btnImg/btnCancel_over.png')"
                        onmouseout="SaveMouseout('btnCancel','../../../pic/btnImg/btnCancel_nor.png')"
                        ImageUrl="~/pic/btnImg/btnCancel_nor.png" OnClientClick="window.close(); " />
                </td>
            </tr>
        </table>
            </div>
        </div>
    </div>
    <%--<div id="brand_detail" runat="server" style="margin: 20px auto auto auto; width: 500px;">
        
    </div>--%>
    </form>
</body>
</html>
