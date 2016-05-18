<%@ page title="首页" Language="C#" MasterPageFile="~/WorkFlowRule.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="WorkFlowRule_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--引用原来流程门户的脚本-->
    <script type="text/javascript">
        $(function () {
            $(".header_li").removeClass("header_li_current");
            $(".header_li:eq(0)").addClass("header_li_current");
        });
    </script>
    <div class="search" style="display: none;">
        <div class="search_bar">
            <span class="inputSearch_cnt">
                <asp:TextBox ID="txtKey" runat="server" Text="项目立项" CssClass="inputSearch"></asp:TextBox>
            </span>
            <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="buttonSearch_cnt" />
        </div>
    </div>
    <div>
        <div style="display: none;">
            <div>
                <h3 class="h-title" style="float: left;">
                    我收藏的流程</h3>
                <h3 class="h-title" style="float: right;">
                    <a title="查看更多收藏" href="Favorites.aspx">更多收藏...</a>
                </h3>
            </div>
            <div class="clear">
            </div>
            <div>
                <ul class="quicks">
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>请示单</span></a></li>
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>项目立项</span></a> </li>
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>招标启动</span></a> </li>
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>请假申请</span></a> </li>
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>印章申请</span></a> </li>
                    <li><a href="WorkFlow.aspx"><i class="icon"></i><span>董事意见函</span></a> </li>
                </ul>
            </div>
        </div>
        <div class="clear">
        </div>
        <div>
            <h3 class="h-title" style="margin-top: -20px;">
                全部流程</h3>
            <div>
                <table class="ui-table ui-table-noborder helper-table">
                    <tbody>
                        <tr>
                            <td class="helper-table-first">
                                <i class="item"></i>
                                <br>
                                <em>房地产业务流程</em>
                            </td>
                            <td id="row1" class="helper-table-second">
                                <div class="helper-table-limit">
                                    <h4>
                                        投资立项
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        招标采购
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="helper-table-first">
                                <i class="item"></i>
                                <br>
                                <em>集团总部管理流程 </em>
                            </td>
                            <td id="row2" class="helper-table-second">
                                <div class="helper-table-limit">
                                    <h4>
                                        常用流程
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        行政流程
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        人力流程
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        人力流程（资源投资）
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        人力流程（资源集团）
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="helper-table-first">
                                <i class="item"></i>
                                <br>
                                <em>城市公司管理流程 </em>
                            </td>
                            <td id="row3" class="helper-table-second">
                                <div class="helper-table-limit">
                                    <h4>
                                        通用流程
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        湖南公司
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        城市公司
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
				<div class="helper-table-limit">
                                    <h4>
                                        北大医疗健康管理中心
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        物业集团
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                <div class="helper-table-limit">
                                    <h4>
                                        方亚海泰
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
				<div class="helper-table-limit">
                                    <h4>
                                        北大科技园
                                    </h4>
                                    <ul>
                                    </ul>
                                </div>
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
