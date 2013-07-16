<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/CustomerService/Views/Shared/CustomerService_Areas.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<eulei.shop.Models.VW_Letter>>" %>

<%@ Import Namespace="Q42.Wheels.Mvc.Paging" %>
<%@ Import Namespace="eulei.shop.Code" %>
<%@ Import Namespace="Eword.ValidateCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    留言
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%: Url.Content("~/Content/js/confirm.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Content/themes/ui/js/jquery-ui-1.8.22.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.form.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js") %>"></script>
    <script type="text/javascript" src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript">
        $(function () {

            $("#aclick").bind("click", function () {
                $.get('<%: Url.Content("~/Common/GetValidateCodeString") %>?time=' + Math.random(), function (result) {
                    $("#ConfirmvaliCodeText").val(result);
                    $("#valiCodeImg").attr({ src: '<%: Url.Content("~/Common/GetValidateCodeImage?code=")%>' + $("#ConfirmvaliCodeText").attr("value") });
                });
            });
            $("#valiCodeImg").bind("click", function () {
                $.get('<%: Url.Content("~/Common/GetValidateCodeString") %>?time=' + Math.random(), function (result) {
                    $("#ConfirmvaliCodeText").val(result);
                    $("#valiCodeImg").attr({ src: '<%: Url.Content("~/Common/GetValidateCodeImage?code=")%>' + $("#ConfirmvaliCodeText").attr("value") });
                });
            });

            $("#createLetter").bind("click", function (event) {
                $.get('<%: Url.Content("~/Common/GetValidateCodeString") %>?time=' + Math.random(), function (result) {
                    $("#ConfirmvaliCodeText").val(result);
                    $("#valiCodeImg").attr({ src: '<%: Url.Content("~/Common/GetValidateCodeImage?code=")%>' + $("#ConfirmvaliCodeText").attr("value") });
                });
                $("#dialog-form-Create").dialog("open");
            });

            $('.deletelink').confirm({
                wrapper: '<div style="float:right;"><br/><div>',
                msg: '删除？',
                ok: '是',
                cancel: '否',
                timeout: 10000
            });
            $("#PostSearch").validate({
                onfocusout: false,
                onkeyup: false,
                onclick: false,
                rules: {
                    SearchTextTEL: { required: true, number: true },
                    SearchTextSender: { required: true }
                   
                },
                messages: {
                    SearchTextSender: '请输入“姓名”',
                    SearchTextTEL: {
                        required: "请输入“联系电话！”",
                        number: '“联系电话”只能输入数字！'
                    }   
                },
                //success: function (label) {
                //    label.addClass("valid").text("√")
                //}
                errorContainer: "div.searchError",
                errorLabelContainer: $("div.searchError"),
                wrapper: "div" 

            }
                );


            $("#CreatepostForm").validate({
                onfocusout: false,
                onkeyup: false,
                onclick: false,
                rules: {
                    LetterTEL: { required: true, number: true },
                    LetterEmail: { email: true },
                    LetterSender: { required: true },
                    LetterTitle: { required: true },
                    LetterContent: { required: true },
                    LetterAddressee: { required: true },
                    LetterTypeID: { required: true },
                    valiCodeText: { required: true, equalTo: "#ConfirmvaliCodeText" }
                },
                messages: {
                    LetterAddressee: '请选择“收件人”！',
                    LetterTypeID: '请选择“类型”！',
                    valiCodeText: {
                        required: '请填写“验证码”！',
                        equalTo: '“验证码”错误！'
                    },
                    LetterTEL: {
                        required: "请输入“联系电话！”",
                        number: '“联系电话”只能输入数字！'
                    },
                    LetterEmail: {
                        email: '你输入的“Email”格式不正确！'
                    },
                    LetterSender: '请输入“姓名”',
                    LetterTitle: '请输入“标题”',
                    LetterContent: '请输入“内容”'
                },
                //success: function (label) {
                //    label.addClass("valid").text("√")
                //}
                errorContainer: "div.createError",
                errorLabelContainer: $("div.createError"),
                wrapper: "div",
                submitHandler: function (form) {
                    $("#dialog-form-Create").dialog("close");
                    form.submit();
                },
                //showErrors: function (errors) {                   
                //    this.defaultShowErrors();                    
                //        $("#dialog-form-Create").dialog({ height: 480 });                   
                //}
                invalidHandler: function (form, validator) {
                    var errors = validator.numberOfInvalids();
                    if (errors) {
                        $("#dialog-form-Create").dialog({ height: 480 });
                    } else {
                        $("#dialog-form-Create").dialog({ height: 380 });
                    }
                }

            }
                       );

            $("#dialog-form-Create").dialog({
                autoOpen: false,
                height: 380,
                width: 550,
                modal: true,
                buttons: {
                    "提交": function () {
                        $("#CreatepostForm").submit();
                    },
                    "取消": function () {
                        $(this).dialog("close");
                        $("#CreatepostForm").resetForm();
                    }
                },
                close: function () {
                    $(".createError").html("");
                    $(this).dialog({ height: 380 });                    
                }
            });

        });
    </script>
    <div class="listpager_arrow_box">
        <div class="listpager_arrow">
            当前位置：<%:Html.MvcSiteMap().SiteMapPath() %>&gt;&gt;内容
        </div>
    </div>

    <div class="LetterTopContent">
        <div>
            <strong class="tred bold">各位网友：</strong>
            <p class="p">您好！首先感谢你光临“<%:ConfigurationManager.AppSettings["webTitle"].ToString()%>”。</p>

            <p class="p"><%:ConfigurationManager.AppSettings["webTitle"].ToString()%>网站的开通，缩短了我们之间的距离，使我们之间交流更加便捷。您对我们的工作有什么意见和建议，请你通过市国资委网站咨询投诉电子信箱或其他渠道进行反映。</p>

            <div class="SearchBoxOut">
                <% using (Html.BeginForm("Index", "Letter", new { @area="CustomerService"}, FormMethod.Post, new { @id = "PostSearch" }))
                   { %>
                <div class="SearchBoxIn">
                    <div class="searchBox">
                        姓名：<%:Html.TextBox("SearchTextSender", "", new { @class = "SearchTextSender" })%>
                        电话：<%:Html.TextBox("SearchTextTEL", "", new { @class = "SearchTextTEL" })%>
                        <input type="submit" id="searchSubmit" class="btnSearchSubmit" value="  " />
                    </div>
                    <div>
                        <a href="javascript:void()" id="createLetter" class="createLetter"></a>
                        <%:Html.ActionLink("  ", "Help", "PublicActor", new { @id = 22 }, new { @class = "LetterHelp" })%>
                    </div>
                    <div class="searchError error"></div>
                </div>
                <%} %>
            </div>

        </div>
    </div>


    <div>
        <%
            int _i = 1;
            foreach (var item in this.GetPage(Model))
            { %>
        <div class="<%=_i%2==0?"lbgalt":"lbg" %>  leaveWordList">
            <div class="top">
                <div  class="left">
                    <div>                      
                        <img src="../../../../Content/images/face/face1.png" alt="头像1" />
                    </div>
                    <div>
                        <strong>留&nbsp;&nbsp;言&nbsp;人： </strong><%: Html.DisplayFor(modelItem => item.LetterSender) %>
                    </div>
                    <div>
                        <strong>类&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型： </strong><%: Html.DisplayFor(modelItem => item.DictionaryDescription) %>
                    </div>
                    <div>
                       <strong> 留言时间： </strong><%: Html.DisplayFor(modelItem => item.LetterTimeSend) %>
                    </div>
                </div>
                <div class="right">
                    <div class="title"><strong>标题：<%: Html.DisplayFor(modelItem => item.LetterTitle) %> </strong></div>                  
                    <div><strong>内容： </strong></div>
                    <div class="content"><%: Html.DisplayFor(modelItem => item.LetterContent) %></div>
                </div>
            </div>
            <%if (item.LetterIsWriteBack && (!string.IsNullOrEmpty(item.LetterWriteBackContent)))
              { %>
            <div class="bottom">
                <div  class="left">
                      <div>                      
                        <img src="../../../../Content/images/face/face2.jpg" alt="头像1" />
                    </div>
                    <div>
                        <strong>回&nbsp;&nbsp;复&nbsp;人：</strong><%: Html.DisplayFor(modelItem => item.UserInfoFriendName) %>
                    </div>
                    <div>
                        <strong>回复时间：</strong><%: Html.DisplayFor(modelItem => item.LetterTimeWriteBack) %>
                    </div>
                </div>
                <div class="right">
                    <div class="title"><strong>回复内容：</strong></div>
                    <div class="content"><%: Html.DisplayFor(modelItem => item.LetterWriteBackContent) %></div>
                </div>
            </div>
            <%} %>
        </div>
        <%_i++;
            } %>
    </div>
    <div class="page">
        <%= Html.RenderPagination() %>
    </div>
    <div id="dialog-form-Create" title="留言" style="display:none;">
        <div id="CreateAjaxBox">
            <%using (Html.BeginForm("Create", "Letter", new { @area = "CustomerService", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "CreatepostForm" }))
              { %>
            <table>
                <tr>
                    <td class="editor-label">
                        <strong class="tred bold">*</strong>姓名：
                       
                    </td>
                    <td class="editor-field">
                        <%: Html.Editor("LetterSender") %>
                    </td>
                    <td class="editor-label">E-mail：
                    </td>
                    <td class="editor-field">
                        <%: Html.Editor("LetterEmail") %>
                    </td>
                </tr>
                <tr class="alt">
                    <td class="editor-label"><strong class="tred bold">*</strong> 联系电话：
                    </td>
                    <td class="editor-field">
                        <%: Html.Editor("LetterTEL") %>
                    </td>
                    <td class="editor-label">邮政编码：
                    </td>
                    <td class="editor-field">
                        <%: Html.Editor("LetterPostalCode") %>
                    </td>
                </tr>
                <tr>
                    <td class="editor-label">通讯地址
                    </td>
                    <td colspan="3" class="editor-field">
                        <%: Html.Editor("LetterAddress") %>
                    </td>
                </tr>
                <tr class="alt">
                    <td class="editor-label"><strong class="tred bold">*</strong> 收件人：
                    </td>
                    <td class="editor-field">
                        <%: Html.DropDownList("LetterAddressee", ViewBag.LetterAddressee as SelectList)%>
                    </td>
                    <td class="editor-label"><strong class="tred bold">*</strong> 类型：
                    </td>
                    <td class="editor-field">
                        <%: Html.DropDownList("LetterTypeID",ViewBag.LetterTypeID as SelectList)%>
                    </td>
                </tr>
                <tr>
                    <td class="editor-label"><strong class="tred bold">*</strong> 标题：
                    </td>
                    <td colspan="3" class="editor-field">
                        <%: Html.Editor("LetterTitle") %>
                    </td>
                </tr>
                <tr class="alt">
                    <td class="editor-label"><strong class="tred bold">*</strong> 内容：
                    </td>
                    <td colspan="3" class="editor-field">
                        <%: Html.TextArea("LetterContent") %>
                    </td>
                </tr>
                <tr>
                    <td class="editor-label">验证码：
                    </td>
                    <td colspan="3">
                        <%  ValidateCode vCode = new ValidateCode();%>
                        <%  string mycode = vCode.CreateValidateCode(5);%>
                        <%: Html.TextBox("valiCodeText")%>
                        <img id="valiCodeImg" style="cursor: pointer;" src="<%: Url.Content("~/Common/GetValidateCodeImage?code="+mycode) %>"
                            alt="验证码" />
                        <%: Html.Hidden("ConfirmvaliCodeText", mycode)%>
                        <a href="javascript:void();" id="aclick">看不清楚？</a>
                    </td>
                </tr>
            </table>
            <div class="createError error">
            </div>
            <%} %>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headOtherInfo" runat="server">
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["CustomerServiceTheme"].ToString()+"/css/master.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["CustomerServiceTheme"].ToString()+"/css/letter.css") %>"
        rel="stylesheet" />
    <link type="text/css" href="<%:Url.Content("~/App_Themes/"+ConfigurationManager.AppSettings["GeneralTheme"].ToString()+"/css/general.css") %>"
        rel="stylesheet" />
    <link rel="stylesheet" href="<%: Url.Content("~/Content/themes/ui/css/smoothness/jquery-ui-1.8.22.custom.css") %>"
        type="text/css" media="all" />
</asp:Content>
