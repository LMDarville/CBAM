 <%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CBAM.Models.ScenarioViewModel>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
<%--    Drag formats    --%>
	 <style type="text/css">
           .alt {background: #f5f5f5;}
           .dragHandle {width:80px;}
           .showDragHandle {cursor: move;}
           .myDragClass {background-color:#FFFF99;
                        -moz-border-radius: 5px;
            }
     </style>

     <script type="text/javascript">
     $(document).ready(function() {

         var $tabs = $("#tabs").tabs(); // first tab selected

         $(".text-link").click(function() {
             var i = $("#next").find("input[type='hidden']").val();
             $tabs.tabs('select', i - 1); //switch to next step tab
             return false;
         });


         $(function() {
             $("#tabs").tabs();
             var $tabs = $("#tabs").tabs(); // first tab selected

             $("#tabs").bind("tabsselect", function(event, ui) {
                 //alert($tabs.tabs('option', 'selected')); //current index
                 //alert(ui.index); //new index selected
                 //debugger;
                 //location.reload(); //refresh view for changes
                 document.location = ui.tab.href;  //still points to correct tab
                 location.reload(); //goto tab
                 return false;
             }).show(); //end tabs

         });  //end function



     }); //document ready



</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                                 
<table width="100%"> <%--PRROGRESS BAR--%>
    <tr>
        <td style="border-left:none; border-right:none;"><% Html.RenderAction("ProgressBar", new { projectID = Model.projectID }); %></td>
        <td style="border-left:none; border-right:none;">
            <div id="next"> <%=Html.HiddenFor(x => x.nextStepToComplete.Step1)%> </div>         
            <span class="ui-state-error"> Next Step: <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
            <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link">
                    <%=Html.Encode(Model.nextStepToComplete.Description)%> </a></span>
        </td>
        <td class="titlelabel" style="border-left:none;" > <%=Html.Encode(Model.ProjectName)%></td>

    </tr>   
</table>



<%-- shows steps & complete status for debug   --%>
<%--  <div class="ui-state-highlight"><%=Html.Encode(CBAM.Helpers.ModelStateHelpers.ModelMessage)%> </div>
  <% Html.RenderAction("StepsList", "Steps"); %>--%>
  
  
<div id="tabs">
    <ul><%-----create a tab for each step-----%>
        <li><%= Html.ActionLink("Step 1. " + Model.Steps[0].Description, "Step1")%></li>
        <li><%= Html.ActionLink("Step 2. " + Model.Steps[1].Description, "Step2")%></li>
        <li><%= Html.ActionLink("Step 3. " + Model.Steps[2].Description, "Step3")%></li>
        <li><%= Html.ActionLink("Step 4. " + Model.Steps[3].Description, "Step4")%></li>
        <li><%= Html.ActionLink("Step 5. " + Model.Steps[4].Description, "Step5")%></li>
      
    </ul>
</div>

</asp:Content>
