<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioList>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>

<%--<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>
--%>

 <% Html.EnableClientValidation();%>
 <%=Html.ValidationSummary(true,"Please correct the errors.") %>  
 <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>

<%-- field-validation-valid--%>
         
     <% using (Html.BeginForm("Edit", "Utility")){ %>
     <%=Html.HiddenFor(x => x.projectID)%> 

     <% for(var i =0; i<Model.ScenariosForUtilUpdate.Count; i++) { %>
            <%--, FormMethod.Post, new { scenarioToUpdate = Model.Scenarios.ElementAt(i) }--%>           
                    <div class="ScenarioItem">
                       <%= Html.EditorFor(x => x.ScenariosForUtilUpdate[i], "ScenarioItem")%>
                    </div>
                    <div>
                        <a class="ui-icon ui-icon-arrowthickstop-1-n" href="#">Go to Top</a>
                        <a class="ui-icon ui-icon-arrowthickstop-1-s" href="#save">Go to Bottom</a> 
                    </div>

            <% } %>    
    <a name="save">
    <input type="submit" name="Edit" value="Save" /></a>
            
   <%}%>  