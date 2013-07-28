<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.SA_FlowTemplate>" %>
<% using (Html.BeginForm("EditStep", "WorkFlow", new { @area = "manage", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "EditStepPost" }))
   { %>
<table class="border-none">
    <tr class="border-none">
        <td class="border-none">
                            <div class="editSteperror">
            </div>
            <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
            <%: Html.HiddenFor(model => model.FlowTemplateID)%>
            <%: Html.HiddenFor(model => model.FlowTemplateArticleTypeID)%>
            <div class="editor-label">
                步骤序号：
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.FlowTemplateStatusID)%>
                     <div> <strong class="tred">前后请使用递增顺序！如果不清楚如何设置请保持默认。</strong></div>
            </div>
            <div class="editor-label">
                步骤描述：
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.FlowTemplateStatusDesp)%>
            </div>
            <div class="editor-field">
                <%: Html.CheckBoxFor(model => model.FlowTemplateAlowEditStep, new { @cheked="true"})%>允许编辑步骤
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
            <div class="editor-label">
                上一步骤序号：
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.FlowTemplatePreviousStatusID)%>
                     <div> <strong class="tred">如果不清楚如何设置请保持默认。</strong></div>
            </div>
            <div class="editor-label">
                下一步骤序号：
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.FlowTemplateNextStatusID)%>
                <div>
                <strong class="tred">如果不清楚如何设置请保持默认。</strong></div>
            </div>
                    
        </td>
    </tr>
</table>
<% } %>
<script type="text/javascript">

    $(function () {
        $("#EditStepPost").validate({
            onfocusout: false,
            onkeyup: false,
            onclick: false,
            rules: {
                FlowStatusID: { required: true, digits: true, max: 98, min: 2 },
                FlowStatusDesp: { required: true },
                FlowTemplatePreviousStatusID: { required: true, digits: true, max: 98, min: 1 },
                FlowTemplateNextStatusID: { required: true, digits: true, max: 99, min: 2 }
                //                    ArticleContent: { required: true },
                //                    ArticleDescription: { required: true },
                //ArticleSendDate: { required: true, dateISO: true },
                //ArticleCount: { required: true, digits: true }
            },
            messages: {
                FlowStatusID: { required: "步骤序号不许为空！", digits: "步骤序号必须输入整数！", max: "步骤序号不能大于98", min: "步骤序号不能小于2" },
                FlowStatusDesp: '请填写步骤描述！',
                FlowTemplatePreviousStatusID: { required: "上一步骤序号不许为空！", digits: "上一步骤序号必须输入整数！", max: "上一步骤序号不能大于98", min: "上一步骤序号不能小于1" },
                FlowTemplateNextStatusID: { required: "下一步骤序号！", digits: "下一步骤序号必须输入整数！", max: "下一步骤序号不能大于等于99", min: "下一步骤序号不能小于3" }
                //                    ArticleContent: '请填写内容！',
                //                    ArticleDescription: '请填写描述！', 
                //ArticleSendDate: { required: "请填写日期！", dateISO: "日期格式错误！" },
            },
            //                              success: function (label) {
            //                               label.addClass("valid").text("√")
            //                              },
            errorContainer: "div.editSteperror",
            errorLabelContainer: $("div.editSteperror"),
            wrapper: "div"
            //submitHandler: function () { alert("操作已完成!") }
        });
        $("#dialog-form-Edit").dialog("open");
    });
</script>
