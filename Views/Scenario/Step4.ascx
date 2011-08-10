<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioViewModel>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
  
  <div><% Html.RenderAction("Step", new { id=4, projID = Model.projectID}); %></div>

    <% if (Model.Scenarios.FirstOrDefault() != null && Model.Scenarios.Count() > 6)
        { %>
          <% Html.RenderAction("TopSixthList", new { projID = Model.projectID }); %>
    <% } else { %>
                    Need at least 6 scenarios to continue. 
                  <%= Html.ActionLink("Add more scenarios","Index")%>
    <%} %>




