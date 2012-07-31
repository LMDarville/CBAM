<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.Project>" %>
<%@ Import Namespace="CBAM.Helpers"%>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>


<script type="text/javascript"></script>
<style type="text/css">

 input
   {
   	width:300px;
   }
</style>


    <% Html.EnableClientValidation();%>
    <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
     <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>
     <div id="errMessage" class="field-validation-error"> </div>


    <p>
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

    <% using (Html.BeginForm()) { %>
        <fieldset>
                <%=Html.Hidden("id", Model.ID)%>
            <p>
                <label for="Name">Name:</label>
                <%=Html.TextBoxFor(m => m.Name)%>
                <%=Html.ValidationMessageFor(m => m.Name)%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%=Html.TextAreaFor(m => m.Description, new { cols = "60", rows = "10" })%>
                <%=Html.ValidationMessageFor(m => m.Description)%>
            </p>
           
           
            <p>
                <input style="width:auto;" type="submit" value="Save"/>
            </p>
        </fieldset>
    <% } %>
   
