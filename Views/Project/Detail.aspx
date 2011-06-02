<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.Project>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Detail</h2>
    <p>
        <%= Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%= Html.ActionLink("Back to List", "Index") %>
    </p>

    <% using (Html.BeginForm()) { %>
        <fieldset>
            <p>
                <label for="Name">Name:</label>
                <%=Html.Encode(Model.Name)%>
            </p>
            <p>
                <label for="Description">Description:</label>
                <%=Html.Encode(Model.Description)%>
            </p>
            
            <p>
                <label for="Date Added">Added:</label>
                 <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", Model.DateAdded)) %>
            </p>
            
             <p>
                <label for="Last Modified">Last Modified:</label>
                 <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", Model.LastModified)) %>
            </p>
          
            <p>
            </p>
        </fieldset>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

