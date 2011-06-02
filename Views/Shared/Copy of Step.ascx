<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.StepsViewModel>" %>

 <!-- Select View to Render -->
            <% switch (Model.thisStep.Step1)
               { %>
                <% case 0: %>   <!------------ Step 1 ------------>
                     <div><%= Html.ActionLink("Create New", "Create") %></div>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
<%--                     <% Html.RenderPartial("ScenarioList", Model.Scenarios); %>
--%>                     
                <% break; %>   
                
                <% case 1: %>   <!------------ Step 2 ------------>
                     <%--createIndex -->Utilities only has top 1/3 based  on priority--%>
                     <%  if (Model.nextStepToComplete.Step1 >= Model.thisStep.Step1 + 1) //ensure prior steps are complete 
                          { %>  <div><%= Html.ActionLink("Edit", "Edit", "Utility")%></div>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                          before continuing.
                                    </span>
                        <%; } %>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
<%--                     <% Html.RenderPartial("TopThirdList", Model.TopScenarios); %>
--%>                 
                <% break; %>
                
                <% case 2: %>   <!------------ Step 3 ------------>
                     <div>
                        <%if (Model.nextStepToComplete.Step1 >= Model.thisStep.Step1 + 1) //ensure prior steps are complete 
                          { %> <%= Html.ActionLink("Edit", "Edit", "Votes")%>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                      before continuing.  </span><%;} %>
                    </div>
                     
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
                     <% Html.RenderAction("VotesList", "Votes"); %>
                <% break; %>
                
                <% case 3: %>   <!------------ Step 4 ------------>
                        <%if (Model.nextStepToComplete.Step1 >= Model.thisStep.Step1 + 1) //ensure prior steps are complete 
                          { %> <%= Html.ActionLink("Edit", "Edit", "UtilityRating")%>
                        <%; }  
                         else { %> <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                                    <a style="color: blue" href="Scenario#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                                      before continuing.  </span><%;} %>
                     <%=Html.ValidationSummary("Please correct the errors and try again.") %>  
<%--                     <% Html.RenderPartial("TopSixthList", Model.TopSixthScenarios); %>
--%>                <% break; %>
                

                <% case 4: %>    <!------------ Step 5 Strategy ------------>
                      <%if (Model.nextStepToComplete.Step1 >= Model.thisStep.Step1 + 1) //ensure prior steps are complete 
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
 

  <% Html.RenderPartial("StepAccordion", Model); %>