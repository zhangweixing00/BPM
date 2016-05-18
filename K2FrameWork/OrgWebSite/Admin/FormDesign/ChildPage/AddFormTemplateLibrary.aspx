<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddFormTemplateLibrary.aspx.cs"
    Inherits="OrgWebSite.Admin.FormDesign.ChildPage.AddFormTemplateLibrary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑表单模板</title>
    <link href="../../../Styles/EasyUI/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../../Styles/EasyUI/icon.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../../Javascript/EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').linkbutton({
                plain: true
            });
            $('#btnRet').linkbutton({
                plain: true
            });
        });

        //保存数据
        function saveData() {
            $('#lbtnSave')[0].click();
        }

        //返回
        function retBack() {
            window.location.href = "FormTemplateManage.aspx";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    表单模板名称
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    版本号
                </td>
                <td>
                    <asp:TextBox ID="txtVersion" runat="server" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    描述
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="2000"
                        Height="90px" Width="200px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div>
            <a id="btnSave" href="#" iconcls="icon-save" onclick="saveData()">保存</a>
            <a id="btnRet" href="#" iconcls="icon-back" onclick="retBack()">返回</a>
            <asp:LinkButton ID="lbtnSave" runat="server" onclick="lbtnSave_Click"></asp:LinkButton>
        </div>
    </div>
    </form>
</body>
</html>
