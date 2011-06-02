<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioViewModel>" %>
<%--<%@ Import Namespace="CBAM.Models.Scenario"%>--%>
<%@ Import Namespace="CBAM.Helpers"%>


<script type="text/javascript">
</script>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id=Model.Scenario.ID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

    <% using (Html.BeginForm()) { %>
        <fieldset>
            <p>
                <label for="Name">Name:</label>
                <%=Html.Encode(Model.Scenario.Name)%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%=Html.Encode(Model.Scenario.Description)%>
            </p>
            <p>
                <label for="Source">Source:</label>
                <%=Html.Encode(Model.Scenario.Source)%>
         
            </p>
            <p>
                <label for="Stimulas">Stimulas:</label>
                <%=Html.Encode(Model.Scenario.Stimulas)%>
           </p>
            <p>
                <label for="Artifact">Artifact:</label>
                <%=Html.Encode(Model.Scenario.Artifact)%>               
            </p>
             <p>
                <label for="Environment">Environment:</label>
                <%=Html.Encode(Model.Scenario.Environment)%>               
            </p>
             <p>
                <label for="Response">Response:</label>
                <%=Html.Encode(Model.Scenario.Response)%>               
            </p>
             <p>
                <label for="ResponseMeasure">ResponseMeasure:</label>
                <%=Html.Encode(Model.Scenario.ResponseMeasure)%>               
            </p>
                         
            <p>
                  <label for="Importance">Importance:</label>
                        <%= Html.Encode(Model.Scenario.ImportanceString)%>
            </p>
           
           
            <p>
            </p>
        </fieldset>
    <% } %>
   
