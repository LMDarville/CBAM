 <%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioList>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Scenarios (---TEST----)</h2>
     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
    
     <% using (Html.BeginForm("EditUtilities", "Scenario")){ %>
   
     <% for(var i =0; i<Model.ScenariosForUtilUpdate.Count; i++) { %>
            <%--, FormMethod.Post, new { scenarioToUpdate = Model.Scenarios.ElementAt(i) }--%>           
                    <div class="ScenarioItem">
                       <%= Html.EditorFor(x => x.ScenariosForUtilUpdate[i], "ScenarioItem")%>
                    </div>
            <% } %>    
    <input type="submit" name="scenarioToUpdate" value="Finished" />
            
   <%}%>  
</asp:Content>
