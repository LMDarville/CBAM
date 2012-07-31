<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioListForVotes>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>


<script type="text/javascript">

    $(document).ready(function() {
        //Set Total Votes-----------------------------------
        //var allInputs = $(":input");
        var allInputs = $("input[type=text]");
        var formChildren = $("form > *");

        $("#messages").html("Found " + allInputs.length + " inputs and the form has " +
                             formChildren.length + " children.");
        //populate sum
        $("#results").html(calculateSum());

 

        //iterate through each textboxes and add keyup
        //handler to trigger sum event
        $(".numberInput").each(function() {
            $(this).keyup(function() {
                $("#results").html(calculateSum());
            });
        });
    }); // end doc ready


    function calculateSum() {
        var sum = 0;
        var rows = $("#tableScenarioVotes").find("tBody tr"); //for stripes
        var i = 0;
        var msg = "result";
        var votesLeft = 0;
            //iterate through each textboxes and add the values
        $(".numberInput").each(function() {

            //add only if the value is number
            if (!isNaN(this.value) && this.value.length != 0) {
                sum += parseFloat(this.value);
                i += 1;
                rows[i].className = ''; //clear error format
                $("#messages").html("Votes can not exceed 100");
                if (sum > 100) {//restripe bottom 2/3's
                    rows[i].className = 'highlight'; //add error format
                } //if
            }
        });

        votesLeft = 100 - sum;
        msg =  "Votes remaining: ";
        $("#messages").attr("class", "");
        $("#remainingVotes").attr("class", "");
       // $("#messages").html = "Votes remaining: ";

            if (sum > 100) {//add error messages and formats
               // $("#tableVoteTotal").find("#messages").addClass = "highlight.";
               // $("#tableVoteTotal").find("#remainingVotes").addClass = "highlight.";
                $("#remainingVotes").attr("class", "highlight");
                $("#messages").attr("class", "highlight");
                msg = "Votes can not exceed 100";
            } //if
            $("#remainingVotes").html(votesLeft);
            $("#messages").html(msg);
            return sum;
        }        

</script>
 
 <%=Html.ValidationSummary(true,"Please correct the errors.") %>  
 <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>
 <div id="errMessage" class="field-validation-error"> </div>

<%-- field-validation-valid--%>
     <% using (Html.BeginForm()){ %>
       <%=Html.HiddenFor(x => x.projectID)%> 
      <table id="tableVoteTotal">
        <tr>
            <td> Total Votes Used:</td>
            <td width= "15px">
               <span id="results"></span>
            </td>
            <td>
            </td>
            <td>
                <span id="messages"></span>
            </td>
            
            <td>
               <span id="remainingVotes"></span>
            </td>
            
        </tr>
     
      </table>  
     
     <%-- Table Headers--%>
      <table id="tableScenarioVotes" class="">
        <tr>
            <th>ID</th>
            <th width="150px">Scenario Name</th>
            <th width="300px">Scenario Description</th>
            <th width="10px">Votes</th>     
       </tr>
     
   
           <% for(var i =0; i<Model.ScenariosForVotes.Count; i++) { %>
                <tr>
                    <td>  <%= Model.ScenariosForVotes[i].scenarioID%> </td>
                    <td>  <%= Model.ScenariosForVotes[i].Name%></td>
                    <td>  <%= Model.ScenariosForVotes[i].Description%></td>
                     <td> <%= Html.TextBoxFor(m => m.ScenariosForVotes[i].Votes, new { @class = "numberInput" })%>
                     </td>  
                     <td>
                         <%=Html.ValidationMessageFor(m => m.ScenariosForVotes[i].Votes)%>
                  
                     </td> 
               
                         <%=Html.HiddenFor(m => m.ScenariosForVotes[i].scenarioID)%> 
                         <%=Html.HiddenFor(m => m.ScenariosForVotes[i].Description)%> 
                         <%=Html.HiddenFor(m => m.ScenariosForVotes[i].Name)%>
                         <%--  <%= Html.EditorFor(x => x.ScenariosForVotes[i], "ScenarioItemVotes")%>--%>
                </tr>
           <% } %>    
           
            
        
    </table>
    <input type="submit" name="Edit" value="Finished" />
            
   <%}%>  
   
   