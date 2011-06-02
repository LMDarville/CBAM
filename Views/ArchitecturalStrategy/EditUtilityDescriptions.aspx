<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ArchitecturalStrategyViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Architectural Strategy</h2>
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index" }), "tab-4")%>
    </div>
     <% Html.RenderPartial("StrategyForm"); %>
     <% Html.RenderPartial("ExpectedUtilityForm", Model.strategyForExpectedResponse); %>
   
     

</asp:Content>


