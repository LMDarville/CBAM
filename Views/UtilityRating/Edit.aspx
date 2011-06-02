<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioListUtilRatings>" %>


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
	EDIT Utility 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dialogBox" class = "dialog" title="Utility">
    <div>For the top 1/6 scenarios, determine the utility for each quality attribute response level (worse-case, current, desired, best-case) for the scenarios from step 2. Top 1/6 are determined by top 50% of votes from step 3.</div>
            <ul>
                <li>Enter numeric value of utility achieved by each response goal.</li>
                <li>Utility can be between 0 and 100 for each response.  You may choose to use values between 1 and 5 or 1 and 10 for example. </li>
                <li>Utility for worse<=desired<=best </li>
            </ul>
            <div>
                This step is complete once utility has been defined for all scnearios listed (top 1/6).
            </div>           
    </div>
  
      <div id="container">
        <div class="leftHelpIcon"><p>  <button id="helpIcon" class= "help" >Open Dialog</button></p></div>
        <div class="rightIndentForHelp">
        <h2>EDIT Utility</h2></div>
        <div class="clear"></div>
    </div>
    
    <div>
     Step 4:    <%= string.Format("<a href='{0}#{1}'> Back to List</a>", Url.RouteUrl(new { controller = "Scenario", action = "Index", projID = Model.projectID}), "tab-4")%>
    </div>
    <% Html.RenderPartial("EditForm"); %>


</asp:Content>
