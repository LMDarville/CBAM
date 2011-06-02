<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ArchitecturalStrategyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Architectural Strategy</h2>
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index" }), "tab-5")%>
    </div>
     <% Html.RenderPartial("StrategyForm"); %>
    
     
     <% if (Model.strategyForExpectedResponse.ID != 0 && Model.strategyForExpectedResponse.ID != null
                    && Model.strategyForExpectedResponse.ScenariosForStratUtil.Count != 0)
        { %>
          <% Html.RenderPartial("ExpectedUtilityForm", Model.strategyForExpectedResponse); %>
     <% } else { %>
          No Scenarios selected
     <%} %>

</asp:Content>


