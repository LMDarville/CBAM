<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.Project>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptContent" runat="server">

<script type="text/css" src="<%= Url.Content("~/css/jquery-ui-1.8.1.custom.css") %>"></script>


<script type="text/javascript">
     $(document).ready(function() {
    
                      $(".button").addClass("ui-button");
                      $(".button").addClass("ui-widget");
                      $(".button").addClass("ui-state-default");
                      $(".button").addClass("ui-corner-all");
                      //$(".button").addClass("ui-button-text-icon-primary ui-icon ui-icon-locked");
                      //$(".button").addClass("ui-button-text")
                      //$("#button").addClass("ui-button-text");

                      $(function() {
                          $("button, input:submit, a", ".demo").button();
                          $("a", ".demo").click(function() { return false; });
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
</asp:Content>





<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Projects</h2>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>


    <table>
        <tr>
            <th>
                ID
            </th>
        
        </tr>

        <tr>
            <td class="demo1">
                <span class="startbutton ui-corner-all ui-state-default" >
                    <span> <%= string.Format("<a class='ui-icon ui-icon-circle-triangle-e pimage' href='{0}'> Run Report </a>", Url.RouteUrl(new { controller = "Report", action = "RunReports", projID = Model.ID }))%>
                      </span>
                     <span><%= string.Format("<a href='{0}'> Start </a>", Url.RouteUrl(new { controller = "Report", action = "RunReports", projID = Model.ID }), new { @style = "border: none;" })%>
                    </span>
                </span>
                
            </td>
           
        </tr>
    
  
        <tr> 
                 <td  class="headerItem" colspan="11"> 
                   <div>
                            <a class="ui-icon ui-icon-arrowthickstop-1-n" href="#"></a>
                    </div>
                 </td>
       </tr>
   </table>
</asp:Content>



