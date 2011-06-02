<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioList>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EDIT Utility Descriptions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EDIT Utility Descriptions</h2>
    <div>
        <%= string.Format("<a href='{0}#{1}'>Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index" }), "tab-1")%>
    </div>
    <% Html.RenderPartial("Edit"); %>


</asp:Content>
