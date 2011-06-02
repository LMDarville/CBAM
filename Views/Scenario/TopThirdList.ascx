<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Scenario>>" %>
<%@ Import Namespace="CBAM.Models"%>


<script type="text/javascript">
    $(document).ready(function() {
        
        $(".cellData").each(function() {  //only look at cells w/class"CellData"
                var txt = $(this).eq(0).text();
                if ($.trim(txt).length == 0) {
                    this.className = 'ui-state-error highlightEmpty'; //add error format
                } //if
            }); //end each row //highlight empty

    });        //document ready
</script>
    
  <%------------------ Step 2 ----------------%>

  <table>
       <tr>
            <th rowspan="2">Priority</th>
            <th rowspan="2">ID</th>
            <th rowspan="2">Scenario Name</th>
            <th rowspan="2">Scenario Description</th>
            <th colspan="4"> <span class ="">Response Goals</span></th>
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
          
            <% Html.RenderPartial("UtilityColumn", item.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order)); %>
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
   