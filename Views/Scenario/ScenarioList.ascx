<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Scenario>>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>
  
   
<script type="text/javascript">
    $(document).ready(function () {
        //Set highlight: Shade bottom 2/3-----------------------------------
        // debugger;
        var rows = $("#tableScenarios").find("tBody tr");
        var rMarker = rows.length - 1; //first row is header row
        var stopRow = rows.length -1  ; // < in for loop, last is footer
        var newOrder = new Array();
        $("tr:eq(0)").addClass("nodrag");  //disable headers from drag
        $("tr:eq(0)").addClass("nodrop");  //disable headers from drop
   
        $("tr:nth-child(1n)").removeClass("stripe"); //clear stripes
        if (rMarker > 3) {
            rMarker = Math.floor(rMarker / 3.0); //truncate
        } //get bottom half marker
        else rMarker = 0;   //else less than 3, highlight none
        for (i = 1; i < stopRow; i++) {///row 0 = header, last = footer
            if (i > rMarker) {//restripe bottom 2/3's
                rows[i].className = 'stripe';
            } //end if
        } //end for
 
        $.ajax({ data: {}, traditional: true });
        $("#tableScenarios").tableDnD({
            onDragClass: "myDragClass",
            onDrop: function (table, row) {
                var rows = $(table).find("tBody tr");
                //var newOrder = $.tableDnD.serialize();
                //var newOrder = $("#scenarioId").serializeArray();
                //var newOrder = $("#scenarioId").serialize();
                var newOrder = new Array();
                var rMarker = rows.length - 1; //first row is header row
                var stopRow = rows.length - 1; // < in for loop, last is footer
                if (rMarker > 3) {
                    rMarker = Math.floor(rMarker / 3.0); //truncate
                } //get bottom half marker
                else rMarker = 0;   //else less than 3, highlight none

                //debugger;
                $("tr:nth-child(1n)").removeClass("stripe"); //clear stripes
                for (i = 1; i < stopRow; i++) {///row 0 = header, last row is footer
                    //start newOrder at 0 element
                    newOrder[i - 1] = $(rows[i]).find("input[type='hidden']").val();
                    //debugger;
                    if (i > rMarker) {//restripe bottom 2/3's
                        rows[i].className = 'stripe';
                    }
                } //end for

                //update priority in db
                //debugger;
                var url = '<%= Url.Action("UpdatePriority", "Scenario") %>';
                //$.post(url,parameters,callback,type)
                $.post(url, { priority: newOrder },
                                function (newOrder) {
                                    location.reload(); //TO refresh orders in all tabs!
                                    alert("Priority Updated");
                                });
                // location.reload();//TO refresh orders in all tabs!
        
            }
             
        }); //tableDnD

        function success(result) {
            var data = JSON.parse(result);
            alert("success:" + data.success);
        } //format highlighting   //End Set highlight: Shade bottom 2/3-----------------------------------

    });              //document ready


</script>
 <%=Html.ValidationSummary(true,"Please correct the errors.") %>  
 <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>

 
    <table id="tableScenarios" class="">
        <tr class="nodrop nodrag">
            <th></th>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Priority
            </th>              
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th>
                Importance
            </th>
            <th>
                Votes
            </th>
            <th>
                DateAdded
            </th>
            <th>
                LastModified
            </th>
        </tr>

    <% foreach (var item in Model) { %>
        <tr>
            <td visible=false> <%= Html.Hidden("scenarioId", item.ID)%></td>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Detail", "Detail", new { id=item.ID })%> |
                <%= Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.Priority) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%=Html.Encode(item.ImportanceString) %>
            </td>
            <td>
                <%= Html.Encode(item.Votes) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", item.DateAdded)) %>
            </td>
          
            <td>
                <%= Html.Encode(String.Format("{0:MM/dd/yyyy}", item.LastModified))%>
            </td>
        </tr>
    <% } %>
    
     <tr class = "footer nodrop nodrag"> 
             <td  class="headerItem nodrop nodrag" colspan="11"> 
               <div>
                        <a class="ui-icon ui-icon-arrowthickstop-1-n" href="#"></a>
                </div>
             </td>
     </tr>
    

    </table>

    
