<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/manage/Views/Shared/Manage_Areas.Master"
    Inherits="System.Web.Mvc.ViewPage<eulei.shop.Models.VW_SA_ShareArticle>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    转载确认
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/core/base.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/ligerui.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerComboBox.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Libs/ligerUI/js/plugins/ligerTree.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $("#ShareArticleCitedTypeIDCombobox").ligerComboBox({
                width: 150,
                height: 20,
                selectBoxWidth: 250,
                selectBoxHeight: 300,
                textField: 'textcontent', valueField: 'id', valueFieldID: 'ShareArticleCitedTypeID', treeLeafOnly: false,
                tree: {
                    url: '<%: Url.Content("~/Common/GetJsonForArticleTypeID") %>',
                    checkbox: false,
                    textFieldName: "textcontent",
                    idFieldName: "id",
                    parentIDFieldName: "fid"
                }
            });
            $("#tb_reprint").bind("click", function (event) {
                if ($("#ShareArticleCitedTypeIDCombobox").val() == "") {
                    alert("请选择文章分类！");
                    return false;
                }             
            });

            <%if (Model.ShareArticleIsApplyReturn)
              {%>
            $('#tb_reprint').confirm({
                msg: '推送方申请了退回操作，请问是否继续转载？',
                ok: '是',
                cancel: '否',
                timeout: 10000,
                buttons: {
                    wrapper: '<button></button>',
                    separator: '  '
                }
            });
            <%}%>
        });
    </script>
    <br />
    <h3>您真的确定要转载该文章吗？</h3>
    <%if (Model.ShareArticleIsApplyReturn)
      {%>
    <strong class="tred">推送方已经申请了退回！</strong>
    <%}%>
    <% using (Html.BeginForm("ShareArticleReprint", "Article", new { @area = "manage" }, FormMethod.Post, new { @id = "postForm" }))
       { %>

    <table>
        <tr>
            <td><%:Html.Hidden("_returnUrl", ViewData["_returnUrl"].ToString())%>
                <%:Html.HiddenFor(m=>m.ShareArticleID)%>
                <input type="submit" id="tb_reprint" value="转载" class="btnsubmit  rmargin15" />
                | <a href="<%= ViewData["_returnUrl"].ToString() %>">返回</a>
            </td>
        </tr>
        <tr>
            <td class="tl">文章转载分类：</td>
        </tr>
        <tr>
            <td>
                <input id="ShareArticleCitedTypeIDCombobox" type="text" />
            </td>
        </tr>
    </table>
    <%} %>
    <hr />
    <br />
    <div class="article-details">
        <div class="article-details-box">
            <div>
                <div class="article-details-box-date">
                    <div class="article-details-box-date-yearandmonth">
                        <%:string.Format("{0}-{1}", Model.ShareArticleSendDate.Year, Model.ShareArticleSendDate.Month)%>
                    </div>
                    <div class="article-details-box-date-day">
                        <%:Model.ShareArticleSendDate.Day%>
                    </div>
                </div>
                <div class="article-details-box-title">
                    <h1>
                        <a href="javascript:void();"><%:Model.ShareArticleTitle %></a>
                    </h1>
                    <span class="categories">原分类：<a href="#" title="分类内容"><%:Model.ShareArticleType %></a></span>
                </div>
            </div>
            <div class="fixck article-details-box-content">
                <%=Model.ShareArticleContent %>
            </div>
            <div class="article-details-box-footer">
                <p class="article-details-box-footer-tags">
                    标签:
                    <%if (!string.IsNullOrEmpty(Model.ShareArticleLabel))
                      {
                          string[] sArray = Model.ShareArticleLabel.Split(new char[] { ',' });
                          for (int _i = 0; _i < sArray.Length; _i++)
                          {
                              if (sArray[_i] != "")
                              {%>
                                <a href="javascript:void();"><%=sArray[_i].ToString() %></a>
                                <%if (_i < sArray.Length - 1)
                                  {
                                      Response.Write("、");
                                  }
                              }
                          }
                      }%>
                </p>
                <p class="article-details-box-footer-docomment">
                    浏览量:--
                </p>
            </div>
        </div>
    </div>
    <div class="pager">
        <% Html.RenderAction("CustomerPagination", "Common", new { @Area = "", @CustomerUrl = ViewBag.Url, @FirstPage = ViewBag.FirstPage, @CurrentPage = ViewBag.CurrentPage, @LastPage = ViewBag.LastPage });%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/Libs/ligerUI/skins/Aqua/css/ligerui-all.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["ManageTheme"].ToString()+"/css/ArticleAuditing.css") %>"
        rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TabTitleContent" runat="server">
    转载确认
</asp:Content>
