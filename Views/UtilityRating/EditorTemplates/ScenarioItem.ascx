<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioForUtilRating>" %>
<%@ Import Namespace="CBAM.Helpers"%>

            <%--put utilities in correct order--%>
             <% var grp = Model.utilities
                       .OrderBy(b => b.Order)
                       .GroupBy(a => a.Description)
                   ;%>
             <% var grp2 = Model.utilities
                       .OrderBy(b => b.Order)
                       .GroupBy(a => a.Utility)
                   ;%>
              
            

 <tr class="">
            <td rowspan="2">
                    ID: <%= Model.scenarioID%>  &nbsp&nbsp <%= Model.Name%> 
            </td>
                    <%foreach (var item in grp){ %>
                     <td align="center" class="UtilTable"" style="border-bottom: none">
                        <%=Html.Encode(item.Key)%>

                     </td>
                    <%} %>
            </tr>
            <tr>
                <%for (int i = 0; i < Model.utilities.Count; i++) { %>
                <td align="center" class="UtilTable" style="border-top: none">
                    <%=Html.EditorFor(x => x.utilities[i], "UtilityRow")%><br />
                    <%=Html.Hidden("id", Model.scenarioID)%>
                    <%=Html.Hidden("scenarioid", Model.scenarioID)%>
                </td>

                <%} %>
               
            </tr>
        
    
  
  
  