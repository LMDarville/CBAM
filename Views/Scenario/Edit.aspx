<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EDIT
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>EDIT</h2>
    Step 1: Project:  <%=Html.Encode(Model.Scenario.Project.Name)%>
    Scenario: <%=Html.Encode(Model.Scenario.Name)%>
    <% Html.RenderPartial("ScenarioForm"); %>


</asp:Content>
