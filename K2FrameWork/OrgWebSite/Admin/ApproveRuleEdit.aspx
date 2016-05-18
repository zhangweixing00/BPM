<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveRuleEdit.aspx.cs" Inherits="OrgWebSite.Admin.ApproveRuleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>审批规则编辑</title>
     <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/css.css" rel="stylesheet" type="text/css" />
      <link href="../JavaScript/DIVLayer/skin/sohu/ymPrompt.css" rel="stylesheet" type="text/css" />
       <script src="../Javascript/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/BtnStyle.js" type="text/javascript"></script>
    <script src="../Javascript/DIVLayer/ymPrompt.js" type="text/javascript"></script>
     <script type="text/javascript">

         function AddApproveNode() {
             $('#hfSelectedApproveNode').val('');
             var processType = document.getElementById('ddlProcessType');
             if (processType != null) {
                 var processID = processType.options[processType.selectedIndex].value;
                 var random = Math.round(Math.random() * 10000);
                 var para = "ProcessID=" + processID;

                 var vurl = "/Admin/Popup/SelectApproveNode.aspx?" + para;
                 top.window.ymPrompt.win(vurl, 520, 400, '审批节点', callBackUpdate, null, null, true, null, null, true, false, true);
             }
         }

         function callBackUpdate(rel) {
             if (rel != "close") {
                 if (rel == null)
                     return false;
                 else {
                     document.getElementById('<%=hfSelectedApproveNode.ClientID %>').value = rel;
                     document.getElementById('<%=lbReload.ClientID %>').click();
                 }
             }
         }

         function AddRequestNode() {
             $('#hfSelectedRequestNode').val('');
             var processType = document.getElementById('ddlProcessType');
             if (processType != null) {
                 var processID = processType.options[processType.selectedIndex].value;
                 var random = Math.round(Math.random() * 10000);
                 var para = "ProcessID=" + processType.value;
                 var vurl = "/Admin/Popup/SelectRequestNode.aspx?" + para;
                 top.window.ymPrompt.win(vurl, 520, 400, '入口节点', callBackUpdate1, null, null, true, null, null, true, false, true);
             }
         }

         function callBackUpdate1(rel) {
             if (rel != "close") {
                 if (rel == null)
                     return false;
                 else {
                     document.getElementById('<%=hfSelectedRequestNode.ClientID %>').value = rel;
                     document.getElementById('<%=lbReload.ClientID %>').click();
                 }
             }
         }

         //检查CheckBox是否选择并记录状态
         function checkNode(obj, rowNodeID, colNodeID) {
             var hfSelectcb = document.getElementById('hfSelectedCheckBox');
             if (hfSelectcb != null) {
                 if (obj.checked) {
                     if (!(hfSelectcb.value.indexOf(rowNodeID + ';' + colNodeID + ';') > 0)) {
                         hfSelectcb.value += rowNodeID + ';' + colNodeID + ';';
                     }
                 }
                 else {
                     hfSelectcb.value = hfSelectcb.value.replace(rowNodeID + ';' + colNodeID + ';', '');
                 }
             }
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="con_main">
        <div class="topnew">
            <div class="top_text_name">
                
            </div>
            <div class="top_text_copyright">
                </div>
        </div>
        <div class="conter_main">
        
       <div id="app" class="nav_1">
        <p>
            配置流程</p>
        </div>

        <div id="appTable" class="pro_1" style="padding-bottom: 30px;">
        
        <table style="width: 100%;">
            <tr>
             <td style="width: 15%; text-align: right;">
                    所属公司：
                </td>
                <td style="width: 40%;">
                    <asp:DropDownList ID="ddlProcessType" runat="server"  >
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="text-align: right; height: 30px;">
                    <asp:ImageButton ID="btnAddApproveNode" runat="server" Text="添加审批节点" onmouseover="SaveMouseover('btnAddApproveNode','../../../pic/btnImg/btnAddapp.png')"
                        onmouseout="SaveMouseout('btnAddApproveNode','../../../pic/btnImg/btnAddapp_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddapp_nor.png" OnClientClick="AddApproveNode();return false;" />
                    <asp:ImageButton ID="btnAddRequestNode" runat="server" Text="添加申请节点" onmouseover="SaveMouseover('btnAddRequestNode','../../../pic/btnImg/btnAddReqone.png')"
                        onmouseout="SaveMouseout('btnAddRequestNode','../../../pic/btnImg/btnAddReqone_nor.png')"
                        ImageUrl="~/pic/btnImg/btnAddReqone_nor.png" OnClientClick="AddRequestNode();return false;" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right;">
                    流程名称：
                </td>
                <td style="width: 40%; text-align:left;">
                    <asp:TextBox ID="txtGroup" runat="server" Width="200" MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[A-Za-z0-9]+$" ControlToValidate="txtGroup" runat="server" ErrorMessage="请输入字母与数字"></asp:RegularExpressionValidator>
                </td>
                <td style="width: 15%; text-align: right;">
                    入口存储过程：
                </td>
                <td style="width: 30%; text-align:left;">
                    <asp:TextBox ID="txtRequestSPName" runat="server" Text="SProc_GetRequestRole" Width="200" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
               <td></td>
                <td></td>
                 <td></td>
                  <td></td>
                   <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Table ID="tApproveTable" runat="server" border="1" CellSpacing="0" CellPadding="0"
                        Width="100%" CssClass="datalist2" Style="padding-bottom: 30px; padding-left:50px;">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 0 auto; margin-top: 10px; padding-bottom: 30px; text-align: center;">
        <asp:ImageButton ID="btnSave" Width="68px" Height="21px" runat="server" onmouseover="SaveMouseover('btnSave','../../../pic/btnImg/btnSave_over.png')"
            onmouseout="SaveMouseout('btnSave','../../../pic/btnImg/btnSave_nor.png')" 
            ImageUrl="~/pic/btnImg/btnSave_nor.png" OnClick="btnSave_Click"
             />
             <img  id="NewImage" src="../../../pic/right_botton3.jpg"   
                        onclick="window.close();" alt="xinjian" name="ffff" />
    </div>
     <asp:LinkButton ID="lbReload" runat="server" OnClick="lbReload_Click"></asp:LinkButton>
    <asp:HiddenField ID="hfSelectedApproveNode" runat="server" />
    <asp:HiddenField ID="hfSelectedRequestNode" runat="server" />
    <asp:HiddenField ID="hfSelectedCheckBox" runat="server" />
    <asp:HiddenField ID="hfSelectTableID" runat="server" />
        </div>
        </div>
    </div>
    </form>
</body>
</html>
