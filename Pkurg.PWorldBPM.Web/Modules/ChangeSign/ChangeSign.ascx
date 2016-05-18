<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangeSign.ascx.cs" Inherits="Modules_ChangeSign_ChangeSign" %>
<li><div id="div_content"  runat="server"></div></li>
<script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function ChangeSign(instId, sn) {
        var dataId = $('#hf_OpId').val();
        //alert(dataId);
        if (dataId == "" || dataId == undefined) {
            alert("没有配置审批数据ID（hf_OpId）");
            return;
        }

        var txt = $("#" + dataId).val();
        if (txt.length > 200) {
            alert("审批意见太大");
            return;
        }
        var sUrl = "/Modules/ChangeSign/DoChangeSignV2.aspx?id=" + encodeURI(instId) + "&sn=" + sn + "&optionTxt=" + encodeURI(txt);
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";
        var arg = showModalDialog(sUrl, "", sFeatures);
        if (arg == 1) {
            alert("转签成功！");
            //window.close();
            window.opener.location.href = window.opener.location.href;
            window.opener = null; window.open('', '_self', ''); window.close();
        }
        else if (arg == 0) {
            alert("转签失败！");
        }
    }

</script>
