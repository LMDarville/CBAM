<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.Utility>" %>

    <% using (Html.BeginForm()) {%>
        <%= Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ID) %>
                <%= Html.ValidationMessageFor(model => model.ID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.ScenarioID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.ScenarioID) %>
                <%= Html.ValidationMessageFor(model => model.ScenarioID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.QualityAttributeResponseTypeID) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.QualityAttributeResponseTypeID) %>
                <%= Html.ValidationMessageFor(model => model.QualityAttributeResponseTypeID) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Description) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Description) %>
                <%= Html.ValidationMessageFor(model => model.Description) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Utility1) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Utility1) %>
                <%= Html.ValidationMessageFor(model => model.Utility1) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%= Html.ActionLink("Back to List", "Index") %>
    </div>


