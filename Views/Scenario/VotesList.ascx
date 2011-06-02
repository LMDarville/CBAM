<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Scenario>>" %>
<%@ Import Namespace="CBAM.Models"%>

<script "../../css/jquery-ui-1.8.1.custom.css"  type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
                  

    });   //document ready
 </script>
    
      <%------------------ Step 3 ----------------%>
 
  <table>
       <tr>
            <th rowspan="2">Priority</th>
            <th rowspan="2">ID</th>
            <th rowspan="2">Scenario Name</th>
            <th rowspan="2">Scenario Description</th>
            <th colspan="4">Response Goal Descriptions</th>
            <th rowspan="2" class="ui-accordion-header ui-helper-reset ui-state-default ui-corner-all">Votes</th>
       </tr>
       <tr>
            <% var grp = Model.First().Utilities.Select(z => z.QualityAttributeResponseType)
                       .OrderBy(a => a.Order)
                       .GroupBy(a => a.Type)
                   ;%>
              
             <% foreach (var item in grp) { %>
                    <th width="100px" >  <%= Html.Encode(item.Key) %> </th>
             <% } %>   
       </tr>
      

    <% foreach (var item in Model) { %>
        <tr class="">
            <td>
                <%= Html.Encode(item.Priority) %>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
             <td>
                <%= Html.Encode(item.Description) %>
            </td>
          
            <% Html.RenderPartial("UtilityColumn", item.Utilities); %>
            
            <td colspan="4">
               <%= Html.Encode(item.Votes) %>
            </td>
         </tr>
       
        
    <% } %>
      <tr> 
             <td  class="headerItem" colspan="8"> 
               <div>
                        <a class="ui-icon ui-icon-arrowthickstop-1-n" href="#"></a>
                </div>
             </td>
       </tr>

    </table>
    

   