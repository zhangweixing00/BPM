<%@ Page Title="" Language="C#" MasterPageFile="~/WorkFlowRule.master" AutoEventWireup="true"
    CodeFile="InstitutionInfo.aspx.cs" Inherits="WorkFlowRule_InstitutionInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(function () {
            $(".header_li").removeClass("header_li_current");
            $(".header_li:eq(1)").addClass("header_li_current");
        });
    </script>
    <script type="text/javascript">

        //禁止右键保存
        $(document).ready(function () {
            $(document).bind("contextmenu", function (e) {
                return false;
            });
        });

        //PageLoad
        $(function () {
            var id = $('#<%=hdId.ClientID%>').val();
            var createdBy = $('#<%=hdcreatedBy.ClientID%>').val();
            var createdByName = $('#<%=hdcreatedByName.ClientID%>').val();
            $.get("Ajax.ashx", { action: "CheckIsFocus", type: "rule", id: id, createdBy: createdBy, createdByName: createdByName },
             function (data) {
                 if (data == "1") {
                     $("#addFocus").hide();
                     $("#spanFocus").show();
                 }
             });

        });


        function addFocus() {
            var id = $('#<%=hdId.ClientID%>').val();
            var createdBy = $('#<%=hdcreatedBy.ClientID%>').val();
            var createdByName = $('#<%=hdcreatedByName.ClientID%>').val();

            $.get("Ajax.ashx", { action: "AddFocus", type: "rule", id: id, createdBy: createdBy, createdByName: createdByName },
             function (data) {
                 $("#addFocus").hide();
                 $("#spanFocus").show();
             });
        }
    </script>
    <div class="search_box">
        <h2 class="detail_title">
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
        </h2>
        <div style="float: left;">
            <h3>
                <asp:Label ID="lblPublishDate" runat="server" Visible="false"></asp:Label>
            </h3>
        </div>
        <div style="float: right;">
            <h3>
                <asp:Label ID="lblCreatedByName" runat="server"></asp:Label>
                <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                <a style="margin-left: 10px;" id="addFocus" href="javascript:void(0);" onclick="javascript:addFocus();">
                    添加收藏</a> <span id="spanFocus" style="display: none; margin-left: 10px;">已收藏</span>
            </h3>
        </div>
        <div class="clear">
            <asp:HiddenField ID="hdId" runat="server" />
            <asp:HiddenField ID="hdcreatedBy" runat="server" />
            <asp:HiddenField ID="hdcreatedByName" runat="server" />
        </div>
        <div class="detail_content">
            <asp:Label ID="lblSummary" runat="server"></asp:Label>
        </div>
        <!--Attachements-->
        <div class="detail_content">
            <div style="border-bottom: 1px dashed #e6e6e6;">
                附件：<asp:Label ID="lblAttachements" runat="server" Visible="false" Text="暂无"></asp:Label></div>
            <asp:Repeater ID="rptAttachements" runat="server">
                <ItemTemplate>
                    <div>
                        <%#Container.ItemIndex+1%>
                        - <a target="_blank" href=' <%#Eval("FilePath")%><%#Eval("FileName")%>'>
                            <%#Eval("FileName")%>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
