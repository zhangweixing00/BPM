<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDelegation.ascx.cs" Inherits="Sohu.OA.Web.WorkSpace.UC.AddDelegation" %>

    <div class='nav_2' style="top: 0px; width: 90%;">
        <p>
            代理信息</p>
    </div>
    <div class='pro_1' style="width: 90%; top: 15px;">
        <table border="0" style="width: 90%;">
            <tbody>
                <tr>
                    <td scope='col' style="text-align: right; vertical-align: top;">
                        流程名称：
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <asp:DropDownList ID="ddlProcess" runat="server" Style="width: 200px; border: 1px #999999 solid;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td scope='col' style="text-align: right; vertical-align: top;">
                        被代理人：
                    </td>
                    <td style="text-align: left;  width:200px;">
                        <asp:TextBox ID="txtFromUser" runat="server" Style="width: 200px; border: 1px #999999 solid;"></asp:TextBox>
                        <asp:HiddenField ID="hfFromUser" runat="server" />
                    </td>
                    <td style="color: Red" align="left">
                        &nbsp;*
                    </td>
                </tr>
                <tr>
                    <td scope='col' style="text-align: right; vertical-align: top;">
                        代理人：
                    </td>
                    <td style="text-align: left; width:200px;">
                        <asp:TextBox ID="txtToUser" runat="server" Style="width: 200px; border: 1px #999999 solid;"></asp:TextBox>
                        <asp:HiddenField ID="hfToUser" runat="server" />
                    </td>
                    <td style="color: Red" align="left">
                        &nbsp;*
                    </td>
                </tr>
                <tr>
                    <td scope='col' style="text-align: right; vertical-align: top;">
                        开始时间：
                    </td>
                    <td style="text-align: left; width:200px;">
                        <asp:TextBox ID="txtStartDate" style="width: 200px; height: 22px;
                            border: 1px #999999 solid;" runat="server" Width="100" class="Wdate1" onclick="WdatePicker({readOnly:true})" onFocus="var date1 = document.getElementById('AddDelegation1_txtEndDate').value;WdatePicker({maxDate:date1,minDate:'%y-%M-%d'})"></asp:TextBox>
                    </td>
                    <td style="color: Red" align="left">
                        &nbsp;*
                    </td>
                </tr>
                <tr>
                    <td scope='col' style="text-align: right; vertical-align: top;">
                        结束时间：
                    </td>
                    <td style="text-align: left; width:200px;">
                        <asp:TextBox ID="txtEndDate" runat="server" style="width: 200px; height: 22px;
                            border: 1px #999999 solid;" class="Wdate1" onclick="WdatePicker({readOnly:true})" onFocus="var date2 = document.getElementById('AddDelegation1_txtStartDate').value;WdatePicker({minDate:date2})"></asp:TextBox>
                    </td>
                    <td style="color: Red" align="left">
                        &nbsp;*
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top;">
                        代办条件：
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <asp:RadioButton ID="rbtnAll" runat="server" Text="全部代办" Checked="true" GroupName="Conditions" />
                        <asp:RadioButton ID="rbtnNew" runat="server" Text="代办新任务" GroupName="Conditions" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; vertical-align: top;">
                        备&nbsp;&nbsp;注：
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <asp:TextBox ID="txtRemark" runat="server" Style="width: 200px; height: 45px; border: 1px #999999 solid;" TextMode="MultiLine" Width="220" Height="40px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align:center; width:90%; margin-top:20px;">
        <asp:ImageButton ID="btnOK" runat="server" onmouseover="SaveMouseover('AddDelegation1_btnOK','../../../pic/btnImg/btnConfirm_over.png')"
                        onmouseout="SaveMouseout('AddDelegation1_btnOK','../../../pic/btnImg/btnConfirm_nor.png')"
                        ImageUrl="~/pic/btnImg/btnConfirm_nor.png" OnClientClick="return OkClick();" onclick="btnOK_Click">
                    </asp:ImageButton>
                 <asp:ImageButton ID="btnCancel" runat="server" onmouseover="SaveMouseover('AddDelegation1_btnCancel','../../../pic/btnImg/btnClose_over.png')"
                        onmouseout="SaveMouseout('AddDelegation1_btnCancel','../../../pic/btnImg/btnClose_nor.png')"
                        ImageUrl="~/pic/btnImg/btnClose_nor.png"  OnClientClick="return CancelClick();">
                    </asp:ImageButton>
        </div>
    </div>