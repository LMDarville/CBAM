<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.Project>" %>

<script type="text/css" src="<%= Url.Content("~/css/jquery-ui-1.8.1.custom.css") %>"></script>

<script type="text/javascript">
    $(document).ready(function () {

        $(".button").addClass("ui-button");
        $(".button").addClass("ui-widget");
        $(".button").addClass("ui-state-default");
        $(".button").addClass("ui-corner-all");
   
        $(function () {
            $("button, input:submit, a", ".demo").button();
            $("a", ".demo").click(function () { return false; });
        });

    });  //document ready
</script>

<style type="text/css">
   .button{
            white-space: nowrap;
            padding: 0 0 0 0.25em;
            margin: 0.25em 0 0 1em;
   }
   .pimage {float:left;}	
   
  
</style>

 <span="demo">
                  <span class="startbutton ui-corner-all ui-state-default" >
                    <span> <%= string.Format("<a class='ui-icon ui-icon-circle-triangle-e pimage' href='{0}'> Generate Report </a>", Url.RouteUrl(new { controller = "Report", action = "RunReports", projID = Model.ID }))%>
                           <%= string.Format("<a href='{0}'> Generate Report </a>", Url.RouteUrl(new { controller = "Report", action = "RunReports", projID = Model.ID }), new { @style = "border: none;" })%>
                    </span>
                </span>
                
 </span>



