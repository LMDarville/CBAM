<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.Scenario>" %>

    <fieldset>
        <legend>Fields</legend>
        <%= Html.EditorForModel() %> 
        
       
        
    </fieldset>
    <p>

        <%= Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>


