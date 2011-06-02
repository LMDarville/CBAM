<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioListForVotes>" %>


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
	EDIT Votes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dialogBox" class = "dialog" title="Votes">
            <ul>
                <li>Have stakeholders discuss each scenario and arrive at a vote via consensus or offline voting. Allocate 100 votes to distribute among the scenarios. Voting is based on the desired response value for each scenario.</li>
                <li>The top 50% of scenarios will be reviewed for further analysis. </li>
            </ul>
            <div>
                Votes can be saved at any point. This step is complete once 100 votes have been used.
            </div>           
    </div>

     <div id="container">
        <div class="leftHelpIcon"><p>  <button id="helpIcon" class= "help" >Open Dialog</button></p></div>
        <div class="rightIndentForHelp">
        <h2>EDIT Votes</h2></div>
        <div class="clear"></div>
    </div>

    <div>
        Step 3:   <%= string.Format("<a href='{0}#{1}'> Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index", projID = Model.projectID}), "tab-3")%>
    </div>
    <% Html.RenderPartial("EditVotesForm"); %>


</asp:Content>
