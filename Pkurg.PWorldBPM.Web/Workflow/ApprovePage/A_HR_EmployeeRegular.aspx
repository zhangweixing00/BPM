<%@ Register Src="../../Modules/ApprovalBox/ApprovalBox.ascx" TagName="ApprovalBox"
    TagPrefix="AB" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="A_HR_EmployeeRegular.aspx.cs"
    Inherits="Workflow_ApprovePage_A_HR_EmployeeRegular" %>

<%@ Register Src="../../Modules/FlowRelated/FlowRelated.ascx" TagName="FlowRelated"
    TagPrefix="FR" %>
<%@ Register Src="../../Modules/UploadAttachments/UploadAttachments.ascx" TagName="UploadAttachments"
    TagPrefix="UA" %>
<%@ Register Src="../../Modules/AddSign/AddSign.ascx" TagName="AddSign" TagPrefix="AS" %>
<%@ Register Src="../../Modules/AddSign/AddSignDeptInner.ascx" TagName="AddSignDeptInner"
    TagPrefix="ASI" %>
<%@ Register Src="../../Modules/Countersign/Countersign.ascx" TagName="Countersign"
    TagPrefix="CS" %>
<%@ Register Src="../../Modules/Countersign/Countersign_Group.ascx" TagName="Countersign_Group"
    TagPrefix="CSG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>审批流程</title>
    <link href="/Resource/css/Default.css" rel="stylesheet" type="text/css" />
    <script src="/js/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="/Resource/jquery/jquery-1.8.0.min.js" type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--page-->
    <div class="wf_page">
        <!--header-->
        <div class="wf_header">
            <img src="/Resource/images/pkurg_bg.jpg" alt="北大资源" style="float: left;" />
            <span class="wf_title">审批流程</span>
            <div class="wf_buttons">
            </div>
        </div>
        <!--center-->
        <div class="wf_center" style="width: 980px;">
            <!--流程主表单-->
            <div class="wf_form">
                <div class="wf_form_title">
                    转正审批单
                </div><div class="wf_form_title_en"><asp:Label ID="lbTitle" runat="server"></asp:Label></div>
                <table class="wf_table" cellpadding="0" cellspacing="1">
                    <tr>
                        <th>
                            编号：
                        </th>
                        <td>
                            <asp:TextBox ID="tbReportCode" runat="server" CssClass="txt" Width="180" ReadOnly="true" />
                        </td>
                    </tr>
                </table>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">报批内容</legend>
                    <table class="wf_table2" cellspacing="1" cellpadding="0">
                        <tbody>
                            <tr>
                                <th>
                                    姓名：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbUserName" runat="server" CssClass="txt" Width="105px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    部门：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbDeptName" runat="server" CssClass="txt" Width="300px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    入司时间：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbEntryTime" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    试用期限：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbProbationPeriod" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    试用期起止时间：
                                </th>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="从"></asp:Label>
                                    <asp:TextBox ID="tbProbationPeriodStart" runat="server" CssClass="txt" Width="100px"
                                        ReadOnly="true"></asp:TextBox>
                                    <asp:Label ID="Label9" runat="server" Text="到"></asp:Label>
                                    <asp:TextBox ID="tbProbationPeriodEnd" runat="server" CssClass="txt" Width="100px"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <th>
                                    职位级别：
                                </th>
                                <td>
                                    <asp:TextBox ID="tbPostLevel" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr1" runat="server">
                                <th>
                                    员工工作业绩：
                                </th>
                                <td colspan="5">
                                    <asp:Label ID="Label1" runat="server" Text="现任职位："></asp:Label>
                                    <asp:TextBox ID="tbPost" runat="server" CssClass="txt" Width="100px" ReadOnly="true"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label2" runat="server" Text="试用期关键工作业绩自述："></asp:Label>
                                    <asp:TextBox ID="tbAchievement" runat="server" CssClass="heighttxt" TextMode="MultiLine"
                                        Width="450" Height="60" ReadOnly="true"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text="本人签字："></asp:Label>
                                    <asp:TextBox ID="tbSign" runat="server" CssClass="txt" Width="55px" ReadOnly="true"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" Text="日期："></asp:Label>
                                    <input id="tbSignDate" runat="server" class="txt" style="width: 100px" 
                                        onfocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM-dd'})" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="wf_table2" cellspacing="0" cellpadding="0">
                        <tbody id="tb360" runat="server" visible="false">
                            <tr>
                                <th rowspan="11" style="width:70px">
                                    360素质考评结果：
                                </th>
                                <th rowspan="11" style="width:70px">
                                    试用期素质考核：
                                </th>
                                <th colspan="3" style="text-align:center;">
                                    评价指标及权重
                                </th>
                                <th style="text-align:center;width:430px">
                                    评价标准
                                </th>
                                <th style="text-align:center;width:120px">
                                    评价（差>>>优秀）
                                </th>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    知识/商务（10%）
                                </td>
                                <td colspan="2">
                                    专业知识
                                </td>
                                <td>
                                    具备本岗位所需要的专业知识
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore1" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    财务、市场、流程知识
                                </td>
                                <td>
                                    具有岗位所需要的财务知识、市场知识；对该岗位核心流程具有清晰的理解。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore2" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    经验/成就（20%）
                                </td>
                                <td colspan="2">
                                    专业、关键岗位/业绩
                                </td>
                                <td>
                                    具备必要的专业岗位经验； 工作业绩超越竞争对手或市场水平。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore3" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="4">
                                    能力/性格（30%）
                                </td>
                                <td rowspan="2">
                                    追求卓越
                                </td>
                                <td>
                                    制造变革/决断力/独立工作能力
                                </td>
                                <td>
                                    勇于承担责任，具有独立处理复杂工作能力；积极主动的改进工作，效果显著；对事物发展的关键因素、发展趋势与机遇的把握准确。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore4" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    结果/质量导向
                                </td>
                                <td>
                                    追求完美质量、卓越结果。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore5" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    认知力
                                </td>
                                <td>
                                    战略导向/分析能力
                                </td>
                                <td>
                                    对事情的判断：以工作目标为行动导向；逻辑清晰，把握关键，具有独立见解。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore6" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    协调沟通能力
                                </td>
                                <td>
                                    内外关系
                                </td>
                                <td>
                                    积极与周围工作环境（如客户、其它部门及集团内其它公司）建立良好的工作关系；能够有效化解矛盾，说服他人，擅长人际交往；主动配合主管、同事及相关部门工作。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore7" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="3">
                                    行为/价值观（40%）
                                </td>
                                <td colspan="2">
                                    诚信、正直、务实、纪律
                                </td>
                                <td>
                                    正直、务实、遵纪守法，廉洁，严于律己。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore8" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    敬业、主动、高效、尽责
                                </td>
                                <td>
                                    勤奋、努力工作，主动承担责任。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore9" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    团结、合作、全局观
                                </td>
                                <td>
                                    与同事、其它部门及集团内其它公司积极配合，公司利益至上。
                                </td>
                                <td>
                                    <asp:TextBox ID="tbQualityScore10" runat="server" CssClass="txt" Width="30px" ReadOnly="true"></asp:TextBox>分
                                </td>
                            </tr>
                            <tr>
                                <th rowspan="2">
                                    用人部门考核意见：
                                </th>
                                <th>
                                    试用期业绩考核：
                                </th>
                                <td colspan="5">
                                    评分表：<br />
                                    评分标准：1、10-50分：低于预期目标（相对不满意）<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 2、60-70分：实现目标（相对满意）<br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 3、80-100分：超越目标（相对非常满意）<br />
                                    工作任务完成情况
                                    <asp:DropDownList ID="ddlWorkCompletion" runat="server" OnSelectedIndexChanged="ddlWorkCompletion_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="90">90</asp:ListItem>
                                        <asp:ListItem Value="80">80</asp:ListItem>
                                        <asp:ListItem Value="70">70</asp:ListItem>
                                        <asp:ListItem Value="60">60</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="40">40</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    突出优点：<asp:TextBox ID="tbAdvantage" runat="server" TextMode="MultiLine" Width="425px"
                                        Height="40" ReadOnly="true"></asp:TextBox>
                                    <br />
                                    不足之处：<asp:TextBox ID="tbDisadvantage" runat="server" TextMode="MultiLine" Width="425px"
                                        Height="40" ReadOnly="true"></asp:TextBox>
                                    <br />
                                    工作改善建议：<asp:TextBox ID="tbSuggest" runat="server" TextMode="MultiLine" Width="400px"
                                        Height="40" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    其他必要情况说明（素质50%，业绩50%）：<br />
                                    素质评分：<asp:TextBox ID="tbQualityScore" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>
                                    业绩评分：<asp:TextBox ID="tbAchievementScore" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>
                                    最终成绩：<asp:TextBox ID="tbTatolScore" runat="server" Width="55px" ReadOnly="true"></asp:TextBox>
                                    <br />
                                    准予转正：<asp:DropDownList ID="ddlIsAgreeRegular" runat="server">
                                        <asp:ListItem Value="按期转正">按期转正</asp:ListItem>
                                        <asp:ListItem Value="提前转正">提前转正</asp:ListItem>
                                        <asp:ListItem Value="不予转正">不予转正</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    上传附件：
                                </th>
                                <td colspan="6">
                                    <UA:UploadAttachments ID="uploadAttachments" runat="server" IsCanEdit="true" AppId="3018" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
                <fieldset class="wf_fieldset">
                    <legend class="wf_legend">审批流程</legend>
                    <table class="wf_table">
                        <tbody>
                            <tr>
                                <th>
                                    用人部门意见：
                                </th>
                                <td colspan="2">
                                    <AB:ApprovalBox ID="Option_4051" Node="用人部门意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    人力资源部意见：
                                </th>
                                <td colspan="2">
                                    <AB:ApprovalBox ID="Option_4052" Node="人力资源部意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbDirector" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <AB:ApprovalBox ID="Option_4054" Node="相关董事分管领导意见" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <asp:Label ID="lbPresident" runat="server"></asp:Label>
                                </th>
                                <td colspan="2">
                                    <AB:ApprovalBox ID="Option_4056" Node="董事长CEO意见" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:HiddenField ID="hfInstanceId" runat="server" />
                    <asp:HiddenField ID="hfIsGroup" runat="server" />
                </fieldset>
            </div>
        </div>
    </div>
    <!--快捷菜单-->
    <div id="scroll">
        <div id="scroll_title">
            快捷菜单</div>
        <div id="scroll_button">
            <!--根据需要，放入按钮-->
            <ul class="scroll_ul">
                <li id="Options" runat="server">
                    <asp:LinkButton ID="lbAgree" runat="server" OnClick="Agree_Click">同意</asp:LinkButton></li>
                <li id="ASOptions" runat="server">
                    <asp:LinkButton ID="lbSubmit" runat="server" OnClick="Submit_Click">提交</asp:LinkButton></li>
                <AS:AddSign id="AddSign1" runat="server" />
                <ASI:AddSignDeptInner id="AddSignDeptInner1" runat="server" />
                <li id="UnOptions" runat="server">
                    <asp:LinkButton ID="lbReject" runat="server" OnClick="Reject_Click">不同意</asp:LinkButton></li>
                <li><a href='#' onclick='Close_Win();'>关闭</a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Close_Win() {
        window.opener = null;
        window.open('', '_self');
        window.close();
    }
</script>
