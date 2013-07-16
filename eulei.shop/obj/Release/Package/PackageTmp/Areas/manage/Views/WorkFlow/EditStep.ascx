﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.SA_Flow>" %>
<% using (Html.BeginForm("EditStep", "WorkFlow", new { @area = "manage", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "EditStepPost" }))
   { %>
<table class="border-none">
        <tr class="border-none">
            <td class="border-none">
                <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
                <%: Html.HiddenFor(model => model.FlowID)%>
                <%: Html.HiddenFor(model => model.FlowSourceID)%>
                <div class="editor-label">
                    步骤序号：
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FlowStatusID)%>
                    <strong class="tred">前后请使用递增顺序！如果不清楚如何设置请保持默认。</strong>
                </div>
                <div class="editor-label">
                    步骤描述：
                </div>
                <div class="editor-field">
                    <%: Html.EditorFor(model => model.FlowStatusDesp)%>
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowState, new { @cheked="true"})%>启用
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowSendMoveMsg)%>默认短信
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowAlowEdit)%>允许编辑
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(model => model.FlowIsSynergy)%>启用汇审
                </div>

            </td>
            <td class="border-none">
                <div class="editSteperror">
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
            errorContainer: "div.editSteperror",
            errorLabelContainer: $("div.editSteperror"),
            wrapper: "div"
            //submitHandler: function () { alert("操作已完成!") }
        });
        $("#dialog-form-Edit").dialog("open");
    });
</script>