<%@ Page Title="" Language="C#" MasterPageFile="~/BPM.master" AutoEventWireup="true"
    CodeFile="RuleEdit.aspx.cs" Inherits="WorkFlowRule_Rule_RuleEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        //校验日期格式
        function checkDate(id) {
            var ctrl = $("#" + id);
            if (ctrl.val() != "" && !IsDate(ctrl.val())) {
                ctrl.focus();
                ctrl.val("");
                alert("日期格式有误！");
            }
        }

        //判断日期是否合法
        function IsDate(dateValue) {
            var regex = new RegExp("^(?:(?:([0-9]{4}(-|\/)(?:(?:0?[1,3-9]|1[0-2])(-|\/)(?:29|30)|((?:0?[13578]|1[02])(-|\/)31)))|([0-9]{4}(-|\/)(?:0?[1-9]|1[0-2])(-|\/)(?:0?[1-9]|1\\d|2[0-8]))|(((?:(\\d\\d(?:0[48]|[2468][048]|[13579][26]))|(?:0[48]00|[2468][048]00|[13579][26]00))(-|\/)0?2(-|\/)29))))$");
            if (!regex.test(dateValue)) {
                return false;
            }
            return true;
        }

        //校验表单
        function CheckForm() {
            if ($('#<%=ddlCategory.ClientID%>').val() == "-1") {
                alert("分类 不能为空！");
                $('#<%=ddlCategory.ClientID%>').focus();
                return false;
            }
            if ($.trim($('#<%=txtTitle.ClientID%>').val()) == "") {
                alert("标题 不能为空！");
                $('#<%=txtTitle.ClientID%>').focus();
                return false;
            }
            if ($.trim($('#<%=txtSummary.ClientID%>').val()) == "") {
                alert("摘要 不能为空！");
                $('#<%=txtSummary.ClientID%>').focus();
                return false;
            }
        }  
        
    </script>
    <div class="container">
        <div class="titlebg">
            <div class="title">
                制度</div>
            <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="content">
            <table class="FormTable">
                <tbody>
                    <tr>
                        <th>
                            分类：
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" Width="260" AppendDataBoundItems="true">
                                <asp:ListItem Text="请选择..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            施行时间：
                        </th>
                        <td>
                            <input id="txtPublishDate" class="Wdate" type="text" onclick="WdatePicker()" readonly="readonly"
                                style="width: 255px;" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            标题：
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="txt" Width="800" MaxLength="200"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            摘要：
                        </th>
                        <td>
                            <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Width="800" Height="200"></asp:TextBox>
                            <span class="star">*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            附件：
                        </th>
                        <td>
                            <asp:Repeater ID="rptAttachment" runat="server" OnItemCommand="rptAttachment_ItemCommand">
                                <HeaderTemplate>
                                    <table class="List" style="margin: 2px 2px;">
                                        <tr>
                                            <th style="width: 600px; text-align: left;">
                                                名称
                                            </th>
                                            <th style="width: 50px; text-align: left;">
                                                大小(M)
                                            </th>
                                            <th style="width: 150px; text-align: left;">
                                                上传时间
                                            </th>
                                            <th style="width: 60px; text-align: left;">
                                                操作
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%#Eval("FileName")%>
                                        </td>
                                        <td>
                                            <%#  String.Format("{0:F}", Convert.ToInt32(Eval("FileSize")) / 1024.0/  1024.0)%>
                                        </td>
                                        <td>
                                            <%#Eval("Created_On")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("Attachment_ID") %>'
                                                OnClientClick="return confirm('确定要删除吗?');" CausesValidation="false" Text="删除"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <!--表单-->
                            <table class="FormTable">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="400" />
                                            <asp:Button ID="btnAddAttachment" runat="server" Text="上传" CssClass="green_btn" OnClick="btnAddAttachment_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="green_btn" Text="保存" OnClientClick="javascript:return CheckForm();"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnReturn" runat="server" CssClass="green_btn" Text="返回" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
