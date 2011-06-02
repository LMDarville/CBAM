<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioForUtil>" %>
<%@ Import Namespace="CBAM.Helpers"%>



 
        <h5> <%= Model.Name  %>  &nbsp&nbsp ID: <%= Model.scenarioID%> </h5>
        <label for="Description">Description:</label>
        <hr/>
        <%=Html.Hidden("scenarioid", Model.scenarioID)%>
        <div class="box">
            <%for (int i = 0; i < Model.utilities.Count; i++) { %>
                <%=Html.EditorFor(x => x.utilities[i], "UtilityRow")%><br />
                <%=Html.Hidden("id", Model.scenarioID)%>
                
            <%} %>
        </div>
        
  
  
  