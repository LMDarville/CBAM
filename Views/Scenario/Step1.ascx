<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioViewModel>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
  
  <div width="50%"><% Html.RenderAction("Step", new { id=1, projID = Model.projectID}); %></div>

    <% if (Model.Scenarios.FirstOrDefault() != null)
        { %>
          <% Html.RenderAction("ScenarioList",new { projID = Model.projectID }); %>
    <% } else { %>
          No Scenarios
    <%} %>