<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioList>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript">

    $(document).ready(function () {

        $('.help').addClass('ui-icon ui-icon-help'); //add ? icon
        $('.help').addClass('ui-state-default ui-corner-all'); //add theme

        // Dialog			
        $(function () {
            $(".dialog").dialog({
                autoOpen: false,
                show: "blind",
                hide: "explode"
            });

            $("#helpIcon").click(function () {
                $("#dialogBox").dialog("open");
                return false;
            });
        }); //end dialog

        //hover states on the static widgets
        $('.help, ul#icons li').hover(
					function () { $(this).addClass('ui-state-hover'); },
					function () { $(this).removeClass('ui-state-hover'); }
				);

    });    //document ready

</script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EDIT Utility Descriptions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dialogBox" class = "dialog" title="Response Goals">
        <p>The top 1/3 scenarios as defined and prioritized in the prior step are shown for further refinement.
        Describe the Goals for Best, Current, Desired and Worse Case for each Scenario.</p>
        <p>Examples of Response Goals:</p>
            <ul>
                <li>Best: 0% hung</li>
                <li>Desired: 1% hung</li>
                <li>Current: 5% hung</li>
                <li>Worst: 10% hung</li>
            </ul>
        <p>
            This step is complete once response goals have been defined for the scenarios listed (top 1/3).
        </p>           
        <p>The Utility associated with each case will be added in a later step.</p>
    </div>
    
    <div id="container">
        <div class="leftHelpIcon"><p>  <button id="helpIcon" class= "help" >Open Dialog</button></p></div>
        <div class="rightIndentForHelp">
        <h2>EDIT Response Goal Descriptions</h2></div>
        <div class="clear"></div>
    </div>

    
    <div>
    Step 2:    <%= string.Format("<a href='{0}#{1}'> Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index", projID = Model.projectID}), "tab-2")%>

    </div>
    <% Html.RenderPartial("EditForm"); %>


</asp:Content>
