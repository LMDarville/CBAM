﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Scenario>>" %>
<%@ Import Namespace="CBAM.Models"%>

<script "../../css/jquery-ui-1.8.1.custom.css"  type="text/css" />
<script type="text/javascript">
    $(document).ready(function() {
                        $(function() {
                    $(".accordion").accordion({
                        collapsible: true,
                        active: false

                    });
                }); //accordion


    });   //document ready
 </script>
    
 <div class="accordion">  <%------------------ Step 3 ----------------%>
    <h3><a href="#">Enter Votes</a></h3>
    <div><ul><li>Have stakeholders discuss each scenario and arrive at a vote via consensus or offline voting.   Allocate 100 votes to distribute among the scenarios.   Voting is based on the desired response value for each scenario.</li>
             <li>The top 50% of these scenarios will be reviewed for further analysis.</li>
        </ul>
        <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. "12." Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 316. Print.</footer>
    </div>
</div>
  
       <table>
       <tr>
            <th rowspan="2">Priority</th>
            <th rowspan="2">Scenario Name</th>
            <th rowspan="2">Scenario Description</th>
            <th colspan="4">Quality Attribute Description</th>
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
             <td  class="headerItem" colspan="9"> 
               <div>
                        <a class="ui-icon ui-icon-arrowthickstop-1-n" href="#"></a>
                </div>
             </td>
       </tr>

    </table>
    

   