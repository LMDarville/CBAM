<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Scenario>>" %>
<%@ Import Namespace="CBAM.Models"%>


<script type="text/javascript">
    $(document).ready(function() {
        $(".cellData").each(function() {  //only look at cells w/class"CellData"
            var txt = $(this).eq(0).text();
            if ($.trim(txt).length == 0) {
                this.className = 'ui-state-error'; //add error format
            } //if
        }); //end each row //highlight empty

    });             //document ready
 </script>
 
 <style type="text/css">
    
 
 </style>
 
    
  <%------------------ Step 4 ----------------%>
 <%-- <div width="50%"><% Html.RenderAction("Step", new { id=4}); %></div>--%>

  <table class="RemovePadding" id="TableBlanks">
       <tr>
            <th rowspan="2">Priority</th>
            <th rowspan="2">ID</th>
            <th rowspan="2">Scenario Name</th>
            <th rowspan="2">Scenario Description</th>
            <th rowspan="2">Votes</th>
            <th colspan="4"> <span class ="">
                            Response Goals/Utility</span></th>
                           
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
      <%--End headers--%>


    <% foreach (var item in Model) { %>
        <tr class="">
            <td rowspan="2">
                <%= Html.Encode(item.Priority) %>
            </td>
            <td rowspan="2">
                <%= Html.Encode(item.ID) %>
            </td>
            
            <td rowspan="2">
                <%= Html.Encode(item.Name) %>
            </td>
             <td rowspan="2">
                <%= Html.Encode(item.Description) %>
            </td>
             <td rowspan="2">
                <%= Html.Encode(item.Votes) %>
            </td>
            
             <% foreach (var subitem in item.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order)) { %>
                    <td width="100px" align="center" style="border-bottom-style: dotted">
                        <%= Html.Encode(subitem.Description)%>
                    </td>
                           
             <% } %>
             </tr>
        <tr>          <%--Includes celldata class to highlight blank cells, need to complete in this step--%>
             <% foreach (var subitem in item.Utilities.OrderBy(x => x.QualityAttributeResponseType.Order)) { %>
                    <td  class="cellData" width="100px" style="border: 1px solid #E8EEF4;" align="center" >
                        <%= Html.Encode(subitem.Utility1)%>
                    </td>
             <% } %>
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
   