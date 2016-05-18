<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProcessAction.ascx.cs" Inherits="OrgWebSite.Process.Controls.UCProcessAction" %>
<link href="/Styles/css.css" rel="stylesheet" type="text/css" />
<link href="../../../Styles/PMS/common.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function WindowClose() {
        window.opener = null;
        window.open("", "_self");
        window.close();
        return false;
    }
    function SaveMouseover(btnName, pngImg) {
        document.getElementById(btnName).src = pngImg;
    }
    function SaveMouseout(btnName, pngImg) {
        document.getElementById(btnName).src = pngImg;
    }

    //判断js函数是否存在
    function fnExist(fnName) {
        try {
            return typeof (eval(fnName)) == "function";
        } catch (e) {
            return false;
        }
    }

    //执行某个js函数
    function executeFnIfExist(fnName) {
        if (fnExist(fnName)) {
            return eval(fnName + "();");
        }
        return true;
    }

    //弹出框提示
    var isReturn = false;
    function clickBtn() {
        if ($("#ucTAF_IsFesco").val() != undefined && $("#ucTAF_IsFesco").val() != "1") {
            top.window.ymPrompt.alert({ title: '提示信息', message: '请确认是否已提交该员工社保材料<br/><br/>给Fesco！' });
            return false;
        }
        if ($("#UCEditTAF_IsFesco").val() != undefined && $("#UCEditTAF_IsFesco").val() != "1") {
            top.window.ymPrompt.alert({ title: '提示信息', message: '请确认是否已提交该员工社保材料<br/><br/>给Fesco！' });
            return false;
        }
        if (isReturn == false) {
            top.window.ymPrompt.confirmInfo('确定要发起离职流程并发送邮件吗？', null, null, null, function ConFirm(tp) { tp == 'ok' ? handlerm(tp) : false });
        }
        return isReturn;
    }
    function handlerm() {
        isReturn = true;
        //            if ($("#UCAction_btnLeaveSubmitHRBP") != null) {
        $("#UCAction_btnLeaveSubmitHRBP").click();
        //            }
        //            else if ($("#ucAction_btnLeaveSubmitHRBP")!=null) {
        $("#ucAction_btnLeaveSubmitHRBP").click();
        //            }
    }

    var isSaveHRCB = false;
    function SaveHRCB() {
        if (isSaveHRCB == false) {
            top.window.ymPrompt.confirmInfo('您确定要提交数据吗？提交后<br/><br/>仅HR C&B可见！', null, null, null, function ConFirm(tp) { tp == 'ok' ? handlserm(tp) : false });
        }
        return isSaveHRCB;
    }

    function handlserm() {
        isSaveHRCB = true;
        $("#ucAction_btSaveHRBP").click();
    }

    $(document).ready(function () {
        var myworklist = document.getElementById('<%= hfMyWorklist.ClientID%>').value;
        //        var mydraft = document.getElementById('UCProcessAction1_hfMyDraft').value;
        //        var myjoined = document.getElementById('UCProcessAction1_hfMyJoined').value;
        //        var mystarted = document.getElementById('UCProcessAction1_hfMyStarted').value;
        //        var mydelegation = document.getElementById('UCProcessAction1_hfMyDelegation').value;

        //        var newdelegation = window.parent.document.getElementById('mydelegation');
        var newworklist = window.parent.document.getElementById('myworklist');
        //        var newstarted = window.parent.document.getElementById('mystarted');
        //        var newjoined = window.parent.document.getElementById('myjoined');
        //        var newdraft = window.parent.document.getElementById('mydraft');

        //        //代理
        //        if (newdelegation)
        //        {
        //            var start = newdelegation.innerHTML.indexOf('(');
        //            var end = newdelegation.innerHTML.indexOf(')');
        //            newdelegation.innerHTML = mask(newdelegation.innerHTML, start, end, '(' + mydelegation + ')');
        //        }

        //        //草稿
        //        if (newdraft)
        //        {
        //            var start = newdraft.innerHTML.indexOf('(');
        //            var end = newdraft.innerHTML.indexOf(')');
        //            newdraft.innerHTML = mask(newdraft.innerHTML, start, end, '(' + mydraft + ')');
        //        }

        //我的任务
        if (newworklist) {
            var start = newworklist.innerHTML.indexOf('(');
            var end = newworklist.innerHTML.indexOf(')');
            newworklist.innerHTML = mask(newworklist.innerHTML, start, end, '(' + myworklist + ')');
        }

        //        //我的参与
        //        if (newjoined)
        //        {
        //            var start = newjoined.innerHTML.indexOf('(');
        //            var end = newjoined.innerHTML.indexOf(')');
        //            newjoined.innerHTML = mask(newjoined.innerHTML, start, end, '(' + myjoined + ')');
        //        }

        //        //我的申请
        //        if (newstarted)
        //        {
        //            var start = newstarted.innerHTML.indexOf('(');
        //            var end = newstarted.innerHTML.indexOf(')');
        //            newstarted.innerHTML = mask(newstarted.innerHTML, start, end, '(' + mystarted + ')');
        //        }
    });


    //str,要替换的字符串，begin替换起始位置,end替换结束位置,char替代查找到的字符串
    function mask(str, begin, end, char) {
        var fstStr = str.substring(0, begin);
        var scdStr = str.substring(end + 1);
        return fstStr + char + scdStr;
    }

    function goback() {
        window.history.go(-1);
        window.close();
    }
</script>
<style type="text/css">
    .main
    {
        width: 820px;
        margin: 0 auto;
        height: 200px;
    }
    .ButtonArea
    {
        width:100%;
        text-align: center;
        padding-bottom: 20px;
        margin-top:15px;
    }
    .ButtonArea input[type="image"]{margin-top:10px;}
    .comitbtn
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnSubmit_nor.png');
    }
    .savebtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/save_nor.png');
    }
    .returnbtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnBack_nor.png');
    }
    .agravebtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnAgree_nor.png');
    }
    .RefusebtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnRefuse_nor.png');
    }
    .AffirmbtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnAffirm_nor.png');
    }
    .CancelbtnImg
    {
        border: none;
        width: 68px;
        height: 21px;
        background-image: url('/pic/btnImg/btnCancel_nor.png');
    }
    .SubmitAndNewbtnImg
    {
        border: none;
        width: 116px;
        height: 21px;
        background-image: url('/pic/btnImg/btnSubmitAndNew_nor.png');
    }
</style>
<div class="conter_right_list_main">

    <div class="conter_right_list_nav" id="divComments" style="height: 26px;" runat="server">
        <span>审批意见</span>
    </div>


    <asp:Panel ID="plComments" runat="server" Width="650px">
        <div class="ItemTitle" style="float: left">
            <span id="span1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
        <div style="float: left; width: 600px; padding-left: 28px; padding-top: 10px;">
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="537px" Rows="4"></asp:TextBox></div>
    </asp:Panel>

    <div class="ButtonArea" id="divActionButtons">
        
 
        <asp:ImageButton ID="btnSubmit" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmit_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <asp:ImageButton ID="btnSubmitDraft" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitDraft_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <asp:ImageButton ID="btnSubmitHRBPDraft" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitHRBPDraft_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <asp:ImageButton ID="btnSaveDraft" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/save_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/save_nor.png')" OnClick="btnSaveDraft_Click"
            ImageUrl="/pic/btnImg/save_nor.png" />
        <!--add by lee  OnClientClick="return SaveProcess();"-->
        <asp:ImageButton ID="btnSaveDraftHR" Visible="false" runat="server" onmouseover="SaveMouseover(this.id , '/pic/btnImg/btnsave_over.png')"
            onmouseout="SaveMouseout(this.id , '/pic/btnImg/save_nor.png')" OnClick="btnSaveDraftHR_Click"
            ImageUrl="/pic/btnImg/save_nor.png" />
        <!--提  交-->
        <asp:ImageButton ID="btnSubmitHRBP" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitHRBP_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <!--end-->
        <asp:ImageButton ID="btnRework" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnRework_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <asp:ImageButton ID="btnSave" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnsave_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/save_nor.png')" OnClick="btnSave_Click"
            ImageUrl="/pic/btnImg/save_nor.png" />
        <!--add by lee 2011-6-16-->
        <asp:ImageButton ID="btnSubmitMore" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmitAndNew_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmitAndNew_nor.png')" OnClick="btnSubmitMore_Click"
            ImageUrl="/pic/btnImg/btnSubmitAndNew_nor.png" />
        <asp:ImageButton ID="btnSubmitOnboard" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitOnboard_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" />
        <asp:ImageButton ID="btnApprove" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnAgree_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnAgree_nor.png')" OnClick="btnApprove_Click"
            ImageUrl="/pic/btnImg/btnAgree_nor.png" />
        <asp:ImageButton ID="btnReject" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnRefuse_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnRefuse_nor.png')" OnClick="btnReject_Click"
            ImageUrl="/pic/btnImg/btnRefuse_nor.png" OnClientClick="return executeFnIfExist('RejectCheck');" />
        <asp:ImageButton ID="btnRejectCF" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnRefuse_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnRefuse_nor.png')" OnClick="btnReject_Click"
            ImageUrl="/pic/btnImg/btnRefuse_nor.png" Style="display: none;" />
        <asp:ImageButton ID="btnConfirm" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnAffirm_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnAffirm_nor.png')" OnClick="btnConfirm_Click"
            ImageUrl="/pic/btnImg/btnAffirm_nor.png" />
        <%-- 王红福 离职确认提示--%>
        <asp:ImageButton ID="btLeaveFormConfrim" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnAffirm_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnAffirm_nor.png')" OnClick="btnConfirm_Click"
            OnClientClick=" return MyConfirm();" ImageUrl="/pic/btnImg/btnAffirm_nor.png"
            Style="height: 21px" />
        <%-- wfl添加 begin--%>
        <%-- 部门领导确认页面--%>
        <asp:ImageButton ID="btDepartBossConfrim" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btDepartLeader_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btDepartLeader_nor.png')" OnClick="btnConfirm_Click"
            OnClientClick=" return MyConfirm();" ImageUrl="/pic/btnImg/btDepartLeader_nor.png"
            Style="height: 21px" />
        <!--提  交发送邮件-->
        <asp:ImageButton ID="btnLeaveSubmitHRBP" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/BeginLeave_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/BeginLeave_nor.png')" ImageUrl="/pic/btnImg/BeginLeave_nor.png"
            OnClientClick="return clickBtn();" OnClick="btnLeaveSubmitHRBP_Click" />
        <%-- 保存可查看--%>
        <asp:ImageButton ID="btSaveHRBP" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/HOLDSary_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/HOLDSary_nor.png')" ImageUrl="/pic/btnImg/HOLDSary_nor.png"
            OnClientClick="return SaveHRCB();" OnClick="btSaveHRBP_Click" />
        <%--   只保存数据--%>
        <asp:ImageButton ID="btnleaveSave" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" ImageUrl="/pic/btnImg/btnSubmit_nor.png"
            OnClick="btnleaveSave_Click" />
        <%-- 离职保存草稿--%>
        <asp:ImageButton ID="bnLeaveSaveDraft" Visible="false" runat="server" onmouseover="SaveMouseover(this.id , '/pic/btnImg/btnsave_over.png')"
            onmouseout="SaveMouseout(this.id , '/pic/btnImg/save_nor.png')" ImageUrl="/pic/btnImg/save_nor.png"
            OnClick="bnLeaveSaveDraft_Click" />
        <asp:ImageButton ID="bnLeaveCloseHR" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_nor.png')" ImageUrl="/pic/btnImg/btnBack_nor.png"
            OnClick="bnLeaveCloseHR_Click" />
        <%--wfl添加 end--%>
        <asp:ImageButton ID="btnClose" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnClose_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnClose_nor.png')" OnClientClick="return WindowClose()"
            ImageUrl="/pic/btnImg/btnClose_nor.png" />
        <asp:ImageButton ID="btnCloseHR" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_nor.png')" OnClick="btnCloseHR_Click"
            ImageUrl="/pic/btnImg/btnBack_nor.png" />
        <%-- --%>
        <asp:ImageButton ID="imagReturn" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_nor.png')" OnClientClick="goback();return false;"
            ImageUrl="/pic/btnImg/btnBack_nor.png" />
        <!--add by CustomWorkFlow-->
        <img id="btnPrevious" style="display: none; cursor: pointer;" alt="" src="/pic/btnImg/btnStep_nor.png"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnStep_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnStep_nor.png')"
            onclick="DisplayForm();" />
        <img id="btnApprovePrevious" style="display: none; cursor: pointer;" alt="" src="/pic/btnImg/btnStep_nor.png"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnStep_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnStep_nor.png')"
            onclick="location.reload();" />
        <asp:ImageButton ID="btnSubmitCF" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitCF_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" OnClientClick="return executeFnIfExist('PageCheck');" />
        <asp:ImageButton ID="btnApproveSave" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnsave_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/save_nor.png')" OnClick="btnApproveSave_Click"
            ImageUrl="/pic/btnImg/save_nor.png" />
        <asp:ImageButton ID="btnViewFlow" Visible="false" runat="server" ImageUrl="/pic/btnImg/jiaqian_nor.png"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/jiaqian_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/jiaqian_nor.png')"
            OnClientClick="DisplaySelectFunction(); return false;" />
        <asp:ImageButton ID="btnSubmitDraftCF" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnSubmitDraft_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" OnClientClick="return executeFnIfExist('PageCheck');" />
        <asp:ImageButton ID="btnReworkCF" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')" OnClick="btnRework_Click"
            ImageUrl="/pic/btnImg/btnSubmit_nor.png" OnClientClick="return executeFnIfExist('PageCheck');" />
        <asp:ImageButton ID="btnCancel" Visible="false" runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnCancel_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnCancel_nor.png')" OnClick="btnCancel_Click"
            ImageUrl="/pic/btnImg/btnCancel_nor.png" OnClientClick="return executeFnIfExist('CancelProcess');" />
        <!--会签按钮-->
        <asp:ImageButton ID="btnStartCounter" Style="display: none;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnSubmit_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnSubmit_nor.png')"
            OnClick="btnStartCounter_Click" ImageUrl="/pic/btnImg/btnSubmit_nor.png" OnClientClick="return executeFnIfExist('PageCheck');" />
        <!--查看流程图按钮-->
        <asp:ImageButton ID="btnReViewFlow" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/chakanliuchengtu_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/chakanliuchengtu_nor.png')" ImageUrl="/pic/btnImg/chakanliuchengtu_nor.png"
            OnClientClick="executeFnIfExist('ReViewFlow'); return false;" />
        <asp:ImageButton ID="btnCDFStartBack" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_nor.png')"
            ImageUrl="/pic/btnImg/btnBack_nor.png" OnClick="btnCDFStartBack_Click" OnClientClick="window.history.go(-1);window.close(); return false;" />
        <asp:ImageButton ID="btnCDFApproveBack" Style="cursor: pointer;" Visible="false"
            runat="server" onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_over.png')"
            onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_nor.png')" ImageUrl="/pic/btnImg/btnBack_nor.png"
            OnClick="btnCDFApproveBack_Click" />
        <asp:ImageButton ID="btnCDFDraftBack" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_nor.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_over.png')"
            ImageUrl="/pic/btnImg/btnBack_nor.png" OnClick="btnCDFDraftBack_Click" OnClientClick="window.history.go(-1);window.close(); return false;" />
        <asp:ImageButton ID="btnCDFViewBack" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnBack_nor.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnBack_over.png')"
            ImageUrl="/pic/btnImg/btnBack_nor.png" OnClick="btnCDFViewBack_Click" />
        <asp:ImageButton ID="btnCDFEndorsement" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/jiaqian_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/jiaqian_nor.png')"
            ImageUrl="/pic/btnImg/jiaqian_nor.png" OnClientClick="executeFnIfExist('Endorsement'); return false;"/>
        <asp:ImageButton ID="btnCDFCountersign" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/btnCountersign_over.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/btnCountersign_nor.png')"
            ImageUrl="/pic/btnImg/btnCountersign_nor.png" OnClientClick="executeFnIfExist('CounterSign'); return false;"/>

        <asp:ImageButton ID="btnCDFSaveToDraft" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/10.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/9.png')"
            ImageUrl="/pic/btnImg/9.png" OnClick="btnSaveDraft_Click"/>

        <asp:ImageButton ID="btnCDFAbandon" Style="cursor: pointer;" Visible="false" runat="server"
            onmouseover="SaveMouseover(this.id ,'/pic/btnImg/12.png')" onmouseout="SaveMouseout(this.id ,'/pic/btnImg/11.png')"
            ImageUrl="/pic/btnImg/11.png" OnClick="btnCDFStartBack_Click" OnClientClick="window.history.go(-1);window.close(); return false;"/>
        <asp:HiddenField ID="hfMyDraft" runat="server" Value="0" />
        <asp:HiddenField ID="hfMyWorklist" runat="server" Value="0" />
        <asp:HiddenField ID="hfMyStarted" runat="server" Value="0" />
        <asp:HiddenField ID="hfMyJoined" runat="server" Value="0" />
        <asp:HiddenField ID="hfMyDelegation" runat="server" Value="0" />
        <asp:HiddenField ID="hfProcessName" runat="server" />
    </div>

</div>