<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.SA_FlowTemplate>" %>
<% using (Html.BeginForm("CreateStep", "WorkFlow", new { @area = "manage", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "CreateStepPost" }))
   { %>

    <table class="border-none">
        <tr class="border-none">
            <td class="border-none">
                <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
                <%: Html.HiddenFor(model => model.FlowTemplateID)%>
                <%: Html.HiddenFor(model => model.FlowTemplateArticleTypeID)%>
                <div class="editor-label">
                    步骤序号：
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FlowTemplateStatusID)%>
                    <strong class="tred">前后请使用递增顺序！如果不清楚如何设置请保持默认。</strong>
                </div>
                <div class="editor-label">
                    步骤描述：
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FlowTemplateStatusDesp)%>
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowTemplateState, new { @cheked="true"})%>启用
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowTemplateSendMoveMsg)%>默认短信
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowTemplateAlowEdit)%>允许编辑
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowTemplateIsSynergy)%>启用汇审
                </div>
            </td>
            <td class="border-none">
                <div class="createSteperror">
                </div>
            </td>
        </tr>

    </table>



<% } %>
<script type="text/javascript">
    $(function () {
        $("#CreateStepPost").validate({
            onfocusout: false,
            onkeyup: false,
            onclick: false,
            rules: {
                FlowStatusID: { required: true, digits: true,max:98,min:2 },
                FlowStatusDesp: { required: true }
                //                    ArticleContent: { required: true },
                //                    ArticleDescription: { required: true },
                //ArticleSendDate: { required: true, dateISO: true },
                //ArticleCount: { required: true, digits: true }
            },
            messages: {
                FlowStatusID: { required: "步骤序号不许为空！", digits: "步骤序号必须输入整数！", max: "步骤序号不能大于98", min: "步骤序号不能小于2" },
                FlowStatusDesp: '请填写步骤描述！'

                //                    ArticleContent: '请填写内容！',
                //                    ArticleDescription: '请填写描述！', 
                //ArticleSendDate: { required: "请填写日期！", dateISO: "日期格式错误！" },

            },
            //                              success: function (label) {
            //                               label.addClass("valid").text("√")
            //                              },
            errorContainer: "div.createSteperror",
            errorLabelContainer: $("div.createSteperror"),
            wrapper: "div"
            //submitHandler: function () { alert("操作已完成!") }
        });
        $("#dialog-form-Create").dialog("open");
    });
</script>
