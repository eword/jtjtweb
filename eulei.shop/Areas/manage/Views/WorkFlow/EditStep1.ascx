<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eulei.shop.Models.SA_Flow>" %>

<script src="<%: Url.Content("~/Scripts/jquery-1.7.1.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>" type="text/javascript"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>SA_Flow</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowSourceID) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowSourceID) %>
            <%: Html.ValidationMessageFor(model => model.FlowSourceID) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowStatusID) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowStatusID) %>
            <%: Html.ValidationMessageFor(model => model.FlowStatusID) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowStatusDesp) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowStatusDesp) %>
            <%: Html.ValidationMessageFor(model => model.FlowStatusDesp) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowState) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowState) %>
            <%: Html.ValidationMessageFor(model => model.FlowState) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowSendMoveMsg) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowSendMoveMsg) %>
            <%: Html.ValidationMessageFor(model => model.FlowSendMoveMsg) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowAlowEdit) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowAlowEdit) %>
            <%: Html.ValidationMessageFor(model => model.FlowAlowEdit) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowIsSynergy) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowIsSynergy) %>
            <%: Html.ValidationMessageFor(model => model.FlowIsSynergy) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FlowSynergy) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.FlowSynergy) %>
            <%: Html.ValidationMessageFor(model => model.FlowSynergy) %>
        </div>

        <p>
            <input type="submit" value="Create" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>
