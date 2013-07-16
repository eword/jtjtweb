<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.TB_Letter>" %>
<%using (Html.BeginForm("EditLetter", "CustomerService", new { @area = "manage", @_returnUrl = Request.Url.PathAndQuery }, FormMethod.Post, new { @id = "EditpostForm" }))
  { %>
<table>
    <tr>
        <td class="editor-label">标题：
        </td>
        <td class="editor-field">
            <%:Model.LetterTitle %>
        </td>
    </tr>
    <tr>
        <td class="editor-label">内容：
        </td>
        <td class="editor-field">
            <%:Model.LetterContent %>
        </td>
    </tr>
    <tr>
        <td class="editor-label"><strong class="tred bold tr">*</strong> 回复内容：
        </td>
        <td class="editor-field">
            <%: Html.HiddenFor(m=>m.LetterID) %>
            <%:Html.Hidden("_returnUrl",ViewData["_returnUrl"]) %>
            <%: Html.TextAreaFor(m=>m.LetterWriteBackContent) %>
        </td>
    </tr>
</table>
<div class="editError error">
</div>
<%} %>
<script type="text/javascript">
    $(function () {
        $("#EditpostForm").validate({
            onfocusout: false,
            onkeyup: false,
            onclick: false,
            rules: {
                LetterWriteBackContent: { required: true }
            },
            messages: {
                LetterWriteBackContent: '请选择“回复内容”！'
            },
            errorContainer: "div.editError",
            errorLabelContainer: $("div.editError"),
            wrapper: "div",
            submitHandler: function (form) {
                $("#dialog-form-Edit").dialog("close");
                form.submit();
            },
            invalidHandler: function (form, validator) {
                var errors = validator.numberOfInvalids();
                if (errors) {
                    $("#dialog-form-Edit").dialog({ height: 400 });
                } else {
                    $("#dialog-form-Edit").dialog({ height: 400 });
                }
            }
        });
        $("#dialog-form-Edit").dialog("open");
    });
</script>
