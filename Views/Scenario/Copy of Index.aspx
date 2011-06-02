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
                                 
<table> <%--PRROGRESS BAR--%>
    <tr>
        <td >
        <div id="next" visible="false"> <%=Html.HiddenFor(x => x.nextStepToComplete.Step1)%> </div>         
        <span class="ui-state-error"> Next Step: <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
            <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a></span>
        </td>
        <td width="50%"><% Html.RenderAction("ProgressBar"); %></td>
    </tr>   
    
    
</table>

<%-- shows steps & complete status for debug   --%>
<%--  <div class="ui-state-highlight"><%=Html.Encode(CBAM.Helpers.ModelStateHelpers.ModelMessage)%> </div>
  <% Html.RenderAction("StepsList", "Steps"); %>--%>
  
  
<div id="tabs">
    <ul><%-----create a tab for each step-----%>
        <% for(int i = 0; i < Model.Steps.Count; i++)  { %>
        <li><a href="#tab-<%= i %>"><span> <%= i+1 %>. <%= Html.Encode(Model.Steps[i].Description) %></span></a></li>
        <% } %>
    </ul>
  

<%--------------------------------------%>
       
   
    <% for (int i = 0; i < Model.Steps.Count; i++) { %>
        <% string projectType = Model.Steps[i].Description; %>
        <div id="tab-<%= i %>">
          
            
            <!-- Select View to Render -->
            <% switch(i) { %>
                <% case 0: %>   <!------------ Step 1 ------------>
                     <div> Step <%= i+1 %>. <%=Html.Encode(Model.Steps[i].Description)%>
                      (Full List)
                     </div>
                      <div><%= Html.ActionLink("Create New", "Create") %></div>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
                     <% Html.RenderPartial("ScenarioList", Model.Scenarios); %>
                     
                <% break; %>
                
                <% case 1: %>   <!------------ Step 2 ------------>
                     <div>Step <%= i+1 %>. <%=Html.Encode(Model.Steps[i].Description)%>
                         (Top 1/3)
                     </div>
                     <%--createIndex -->Utilities only has top 1/3 based  on priority--%>
                     <%  if (Model.nextStepToComplete.Step1 >= i + 1) //ensure prior steps are complete 
                          { %>  <div><%= Html.ActionLink("Edit", "Edit", "Utility")%></div>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                          before continuing.
                                    </span>
                        <%; } %>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
    <%--                 <% Html.RenderPartial("TopThirdList", Model.TopScenarios); %>
    --%>             
                <% break; %>
                
                <% case 2: %>   <!------------ Step 3 ------------>
                     <div>Step <%= i+1 %>. <%=Html.Encode(Model.Steps[i].Description)%>
                     (Top 1/3)
                     </div>
                     <div>
                        <%if (Model.nextStepToComplete.Step1 >= i+1) //ensure prior steps are complete 
                          { %> <%= Html.ActionLink("Edit", "Edit", "Votes")%>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                      before continuing.  </span><%;} %>
                    </div>
                     
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
    <%--                 <% Html.RenderPartial("VotesList", Model.TopScenarios); %>--%>
                <% break; %>
                
                <% case 3: %>   <!------------ Step 4 ------------>
                      <div>Step <%= i+1 %>. <%=Html.Encode(Model.Steps[i].Description)%> </div>
                
                        <%if (Model.nextStepToComplete.Step1 >= i+1) //ensure prior steps are complete 
                          { %> <%= Html.ActionLink("Edit", "Edit", "UtilityRating")%>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                      before continuing.  </span><%;} %>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
      <%--               <% Html.RenderPartial("TopSixthList", Model.TopSixthScenarios); %>--%>
                <% break; %>
                

                <% case 4: %>    <!------------ Step 5 Strategy ------------>
                    <div>Step <%= i+1 %>. <%=Html.Encode(Model.Steps[i].Description)%> </div> 
                    
                      <%if (Model.nextStepToComplete.Step1 >= i+1) //ensure prior steps are complete 
                          {%>  <div><%= Html.ActionLink("Create New", "Create", "ArchitecturalStrategy")%></div>
                       
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                      before continuing.  </span><%;} %>
                            <% Html.RenderAction("Index", "ArchitecturalStrategy"); %>
                    
                  
                <% break; %>
                <% default: %>
                <% break; %>
            <% }; %>


        
              
        </div>
    <% } %>
</div>

</asp:Content>
