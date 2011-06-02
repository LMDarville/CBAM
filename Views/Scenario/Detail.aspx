<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Details</h2>
    Project:  <%=Html.Encode(Model.Scenario.Project.Name)%>
    Scenario: <%=Html.Encode(Model.Scenario.Name)%>
    <% Html.RenderPartial("ScenarioDetailForm"); %>


</asp:Content>
