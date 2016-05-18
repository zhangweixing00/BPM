<%@ Page Language="C#" AutoEventWireup="true" Theme="Common" Codebehind="DeptUserAdd.aspx.cs"
    Inherits="OrgWebSite.Admin.Popup.DeptUserAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门用户添加</title>
    <base target="_self" />
     <script language="javascript" src="../../Javascript/Common.js" type="text/javascript"></script>
</head>
<body class="bd_OpenPage">
    <form id="form1" runat="server">
        <div>
            <div style="float: right; margin-top: 10px; margin-right: 10px;">
                <asp:TextBox runat="server" CssClass="txtSearch" ID="txtFilter"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearch" CssClass="btnCommon" Text="查 找" OnClick="btnSearch_Click" />
            </div>
            <div class="divSelectMain">
                <asp:GridView ID="gvUser" runat="server" HeaderStyle-CssClass="gvHeader1" RowStyle-CssClass="gvRow"
                    AlternatingRowStyle-CssClass="gvAltItem" DataKeyNames="ID" AutoGenerateColumns="False"
                    CssClass="gv" AllowPaging="True" OnPageIndexChanging="gvUser_PageIndexChanging"
                    PageSize="15" OnRowDataBound="gvUser_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkUser" onclick="ChooseUser(this)" />
                                <asp:HiddenField runat="server" ID="hfUserCode" Value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <a href='#' onclick="UserView('<%#Eval("ID") %>')"><img src="../Img/view.png" alt="明细" /></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中文名称" HeaderStyle-Width="30%">
                            <ItemTemplate>
                                <%#Eval("CHName") %>
                                <%--<asp:TextBox runat="server" CssClass="txtSelectCommon" ID="txtAccount" Text='<%#Eval("CHName") %>'></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="英文名称" HeaderStyle-Width="30%">
                            <ItemTemplate>
                                <%#Eval("ENName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属地区" HeaderStyle-Width="30%">
                            <ItemTemplate>
                                <%#GetWorkPlace(Eval("WorkPlace").ToString()) %>
                                <%--<asp:TextBox runat="server" CssClass="txtSelectCommon" ID="txtKeyUser" Text='<%#GetWorkPlace(Eval("WorkPlace").ToString()) %>'></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="gvRow" />
                    <HeaderStyle CssClass="gvHeader" />
                    <AlternatingRowStyle CssClass="gvAltItem" />
                </asp:GridView>
            </div>
            <br />

            <script language="javascript" type="text/javascript">
            
            function ValidateData()
            {
                var vueSelectedUser = $$("hfSelectedUser").value;
                
                if(vueSelectedUser == "")
                {
                    alert("请选择用户");
                    return false;
                }

                return true;
            }
            
            function ChooseUser(chk)
            {
                var hfUserCode=cibc(chk)+"hfUserCode";
                var vueSelectedUser = $$("hfSelectedUser").value;
                if(chk.checked)
                {
                    vueSelectedUser += $$(hfUserCode).value + ";";
                    $$("hfSelectedUser").value = vueSelectedUser;
                }
                else
                {
                    vueSelectedUser = vueSelectedUser.replace($$(hfUserCode).value+";","");
                    $$("hfSelectedUser").value = vueSelectedUser;
                }
            }
            
            function UserAction()
            {                
                var random = Math.round(Math.random()*10000);
                var para = "random=" + random + "&action=new";
                var retValue=window.showModalDialog("UserEdit.aspx?"+ para,window,'dialogHeight: 550px; scroll:yes; dialogWidth: 900px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
                
                if(typeof(retValue) != "undefined")
                {
                    document.getElementById('lbReload').click();
                }
            }
            
            function UserView(usercode)
            {
                var random = Math.round(Math.random()*10000);
                var para = "random=" + random + "&userCode=" + usercode;
                var retValue=window.showModalDialog("UserView.aspx?"+ para,window,'dialogHeight: 650px; scroll:yes; dialogWidth: 750px; edge: Raised; center: Yes; help: No; resizable: no; status: No;');
                
                if(typeof(retValue) != "undefined")
                {
                    document.getElementById('lbReload').click();
                }
            }
            </script>

            <div class="divCommand">
            <asp:HiddenField runat="server" ID="hfSelectedUser" />
                <asp:Button ID="btnChoose" runat="server" CssClass="btnCommon" Text="选 择" OnClick="btnChoose_Click"
                    OnClientClick="return ValidateData()" />
                <input type="button" id="btnCancel" value="取 消" class="btnCommon" onclick="window.opener=null;window.open('','_self','');window.close();" />
                <asp:Literal runat="server" ID="litScript"></asp:Literal>
                <asp:LinkButton runat="server" ID="lbReload" OnClick="lbReload_Click"></asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
