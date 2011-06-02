<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.Project>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EDIT
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>EDIT</h2>
    Project: <%=Html.Encode(Model.Name)%>
    <% Html.RenderPartial("ProjectForm"); %>



</asp:Content>
