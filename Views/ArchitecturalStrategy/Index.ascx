<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.ArchitecturalStrategy>>" %>

<script type="text/javascript">
    $(document).ready(function() {

        $(".cellData").each(function() {  //only look at cells w/class"CellData"
            var txt = $(this).eq(0).text();
            if ($.trim(txt) == "NO") {
                this.className = 'ui-state-error'; //add error format
            } //if
        }); //end each row


    });             //document ready
 </script>
 

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th> 
                Cost
            </th>
            <th>
                DateAdded
            </th>
            <th>
                LastModified
            </th>
             <th>
                Definition Complete?
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.ID })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
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
            <td class="alignRight">
                <%= Html.Encode(String.Format("{0:#,###}", item.Cost)) %>
            </td>
            <td class="alignRight">
                <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", item.DateAdded))%>
            </td>
            <td  class="alignRight">
                <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", item.LastModified))%>
            </td>
            <td class="cellData" style="border: 1px solid #E8EEF4";>
                <% if (item.IsComplete)
                     { %>  YES  <% } 
                else { %>  NO <%} %>
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

 
