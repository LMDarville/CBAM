<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioViewModel>" %>
<%@ Import Namespace="CBAM.Helpers"%>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>

<script type="text/javascript">

    $(document).ready(function() {

        $('.help').addClass('ui-icon ui-icon-help'); //add ? icon
        $('.help').addClass('ui-state-default ui-corner-all'); //add theme

        // Dialog			
        $(function() {
            $(".dialog").dialog({
                autoOpen: false,
                show: "blind",
                hide: "explode"
            });

            $("#helpSource").click(function() {
                $("#dialogSource").dialog("open");
                return false;
            });
            $("#helpStimulus").click(function() {
                $("#dialogStimulus").dialog("open");
                return false;
            });
            $("#helpEnvironment").click(function() {
                $("#dialogEnvironment").dialog("open");
                return false;
            });
            $("#helpArtifact").click(function() {
                $("#dialogArtifact").dialog("open");
                return false;
            });
            $("#helpResponse").click(function() {
                $("#dialogResponse").dialog("open");
                return false;
            });
            $("#helpResponseMeasure").click(function() {
                $("#dialogResponseMeasure").dialog("open");
                return false;
            });
        }); //end dialog

        //hover states on the static widgets
        $('.help, ul#icons li').hover(
					function() { $(this).addClass('ui-state-hover'); },
					function() { $(this).removeClass('ui-state-hover'); }
				);




    });   //document ready

</script>

<style type="text/css">
    
    
    table td
    {
    	border-style: none; 
    }
    
   textarea
   {    width:500px;
   }
   
   input
   {
   	width:500px;
   }
    .help
    {
    	display:inline;
    }
    
</style>

    <% Html.EnableClientValidation();%>
    <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
    <p>
        <%= string.Format("<a href='{0}#{1}'>Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index", projID = Model.Scenario.ProjectID }), "tab-1")%>

<%--        <%= Html.ActionLink("Back to List", "Index", new { projID = Model.Scenario.ProjectID })%>
--%>    </p>





<div id="dialogSource" class = "dialog" title="Source">
	<p>The source of stimulus.  This is some entity (human, computer system or any other actuator) that generated the stimulus. 
    </p>
    <p>Examples:  External to System, Developer, User</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>
<div id="dialogStimulus" class = "dialog" title="Stimulus">
	<p>An interaction with the system or condition that needs to be considered then it arrives at the system. </p>
	<p>Examples:  Unanticipated Message, Wishes to Change to UI, Initiate transactions</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>
<div id="dialogEnvironment" class = "dialog" title="Environment">
	<p>System’s state at time of stimulus.</p>
	<p>Examples: Normal Operation, At design time, At completion of component</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>
<div id="dialogArtifact" class = "dialog" title="Artifact">
	<p>Artifact that is stimulated.  This may be the whole system or some pieces of it.</p>
	<p>Examples: Process, Code, System, Data within the System, Component of System</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>
<div id="dialogResponse" class = "dialog" title="Response">
	<p>The activity undertaken after the arrival of the stimulus.</p>
	<p>Examples: Inform operator continue to operate, Modification is made with no side effects, transactions are processed</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>

<div id="dialogResponseMeasure" class = "dialog" title="Response Measure">
	<p>Measurable quality attribute that results.</p>
	<p>Examples: No Downtime, in 3 hours, with average Latency of 2 seconds</p>
    <footer>Steps defined by: Bass, Len, Paul Clements, and Rick Kazman. Software Architecture in Practice. 2nd ed. Boston: Addison-Wesley, 2003. 75. Print.</footer>
</div>



    <% using (Html.BeginForm(new { projID = Model.Scenario.ProjectID, id = Model.Scenario.ID }))
       { %>
                <%=Html.HiddenFor(m => m.Scenario.ProjectID)%>
                <%=Html.Hidden("id", Model.Scenario.ID)%>
        <fieldset>
        <table>
            <tr>
                <td class="labelcell"><label for="Name">Name:</label></td>
                <td>
                    <%=Html.TextAreaFor(m => m.Scenario.Name)%>
                    <%=Html.ValidationMessageFor(m => m.Scenario.Name)%>
                </td>
            </tr>
            <tr>
                <td class="labelcell"><label for="Description">Description:</label></td>
                <td>
                <%=Html.TextAreaFor(m => m.Scenario.Description)%>
                <%=Html.ValidationMessageFor(m => m.Scenario.Description)%>
              </td>
            </tr>
            <tr><td class="labelcell">
                    <button id="helpSource" class= "help" >Open Dialog</button>
                    <label for="Source" >Source:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.Source)%>
                <%=Html.ValidationMessageFor(m => m.Scenario.Source)%>
                </td>
            </tr>
            <tr>
                <td class="labelcell">
                <button id="helpStimulus" class= "help" >Open Dialog</button>
                <label for="Stimulas">Stimulas:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.Stimulas)%>
                <%=Html.ValidationMessageFor(m => m.Scenario.Stimulas)%>
                </td>
            </tr>
            <tr>
                <td class="labelcell">
                 <button id="helpArtifact" class= "help" >Open Dialog</button>
                <label for="Artifact">Artifact:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.Artifact)%>               
                <%=Html.ValidationMessageFor(m => m.Scenario.Artifact)%>
                </td>
             </tr>
            <tr>
                <td class="labelcell">
                <button id="helpEnvironment" class= "help" >Open Dialog</button>
                <label for="Environment">Environment:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.Environment)%>               
                <%=Html.ValidationMessageFor(m => m.Scenario.Environment)%>
                </td>
            </tr>
            <tr>
                <td class="labelcell">
                <button id="helpResponse" class= "help" >Open Dialog</button>
                <label for="Response">Response:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.Response)%>               
                <%=Html.ValidationMessageFor(m => m.Scenario.Response)%>
                </td>
             </tr>
            <tr>
                <td class="labelcell">
                <button id="helpResponseMeasure" class= "help" >Open Dialog</button>
                <label for="ResponseMeasure">ResponseMeasure:</label></td>
                <td>
                <%=Html.TextBoxFor(m => m.Scenario.ResponseMeasure)%>               
                <%=Html.ValidationMessageFor(m => m.Scenario.ResponseMeasure)%>
                </td>
             </tr>
            <tr>
                <td class="labelcell">
                  <label for="Importance">Importance:</label></td>
                <td>
                        <%= Html.DropDownListFor(m => m.Scenario.ImportanceRatingID, Model.ImportanceList, "-- Select Rating --")%>
                        <%--<%= Html.DropDownList("ImportanceID", Model.ImportanceList)%>--%>
                        <%--<%= Html.ValidationMessageFor(model => model.Scenario.ImportanceRatingID) %>--%>
                </td>
              </tr>
            <tr><td class="labelcell"></td>
                <td>
                    <input style="width:80px;" type="submit" value="Save"/>
                </td>
            </tr>
        </table>
        </fieldset>
    <% } %>
   
