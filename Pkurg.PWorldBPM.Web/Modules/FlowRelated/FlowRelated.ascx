<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowRelated.ascx.cs" Inherits="Workflow_Modules_FlowRelated_FlowRelated" %>
<style id="Style1" type="text/css" runat="server">
    .RelatedFlowTb td
    {
        padding: 0px,5px,0px,0px;
        style: == "white-space:nowrap;";
    }
</style>
<table style="width: 100%">
    <tr>
        <td id="RelatedFlowList">
            <asp:DataList ID="dlRelatedFlow" runat="server" OnDeleteCommand="dlRelatedFlow_DeleteCommand"
                DataKeyField="RelatedID" Width="100%">
                <ItemTemplate>
                    <table class="RelatedFlowTb" width="100%">
                        <tr>
                            <td style="white-space: normal; width: 64%">
                                <a href="<%# "/Workflow/ViewPage/ViewPageHandler.ashx?id="+Eval("RelatedFlowID") %>"
                                    target="_blank">
                                    <%#Eval("RelatedFlowName")%></a>
                            </td>
                            <td style="width: 10%">
                                <%#Eval("CreatorName")%>
                            </td>
                            <td style="width: 12%">
                                <%#DateTime.Parse(Eval("CreateTime").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                            <td style="width: 14%">
                                <asp:LinkButton runat="server" SafetyControlName="lbtnDelete" ID="linkDelete" CausesValidation="false"
                                    Visible='<%# this.IsCanEdit %>' OnClientClick="return confirm('您确定要删除此关联流程吗？')"
                                    CommandName="Delete" CommandArgument='<%#Eval("RelatedID") %>'>
                                    <img src="/images/wf_relation/ico_del.gif" border="0"/><span style="color:#666;height:22px;cursor:hand;padding-top:3px;" >删除</span>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
            &nbsp;
        </td>
        <td style="width: 30px">
            <%=this.IsCanEdit?@"<a href='#' id='lbtnAdd'  
                CssClass='linkbtn' onclick='ShowFlowSelect();return false;'><span>添加</span></a>":"" %>
        </td>
    </tr>
</table>
<div id="divAlert" style="color: Blue;">
    <!-- style="color: Green; font-weight: bold;"-->
    <%=this.IsCanEdit?"提醒：添加关联流程前请先保存表单！":""%>
</div>
<asp:HiddenField ID="hidRelatedFlowProcIdList" runat="server" />
<asp:HiddenField ID="hidProcIDForRelated" runat="server" />
<asp:Button ID="btnAdd" CausesValidation="false" runat="server" Style="display: none"
    OnClick="btnAdd_Click" />
    <script type="text/javascript" src="/js/jquery-1.4.2.min.js"></script>
<script type="text/javascript">

    //如果存在控件linkUpload，则显示“提示信息”
    //controlDivRelatedFlow();
    var procId=$("INPUT[id$='<%=hidProcIDForRelated.ClientID  %>']").val();
    function controlDivRelatedFlow() {
        var divDisplay = false;
        $("a[id$='lbtnAdd']").each(function(index) {
            divDisplay = true;
        });
        if (divDisplay) {
            if (procId != "") {
                document.getElementById("divAlert").style.display = "none";
            }
            else {
                document.getElementById("divAlert").style.display = "block";
            }

        }
        else {
            document.getElementById("divAlert").style.display = "none";
        }
    }

    function ShowFlowSelect() {
        if (procId == "") {
            //window.saveFlowForm();
            alert("您还没有保存表单，请先点击“保存”按钮保存表单！");
            return false;
        }
        //alert("dd");
        //判断用户是否选择了相应的部门
        var ProcIdList = document.all["<%=hidRelatedFlowProcIdList.ClientID%>"].value;
        var arg, sUrl;
        var sFeatures = "status:no;scroll:no;dialogWidth:600px;dialogHeight:500px;help:no;center:yes";

        sUrl = "/Modules/FlowRelated/ShowCanSelectItems.aspx?keys=" + encodeURI(ProcIdList) ;

        arg = showModalDialog(sUrl, "", sFeatures);
      
        if (arg != null ) {
            document.all["<%=hidRelatedFlowProcIdList.ClientID %>"].value = arg;
            //arg中是选择的关联流程 ProcId 列表
            <% =Page.ClientScript.GetPostBackEventReference(btnAdd, "")%>
        }
    }
</script>
