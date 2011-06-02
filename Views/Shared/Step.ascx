<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.StepsViewModel>" %>
 
 

 <script type="text/javascript">
     $(document).ready(function() {
     
     
         var $tabs = $("#tabs").tabs(); // first tab selected

         $(".text-link").click(function() {
             var i = $("#next").find("input[type='hidden']").val();
             $tabs.tabs('select', i - 1); //switch to next step tab
             return false;
         });


     }); //document ready
</script>


 <!-- Select View to Render -->
   <%  if ((Model.nextStepToComplete.Step1 >= Model.thisStep.Step1) || Model.thisStep.Step1 == 0) 
       { %>    <%--ensure prior steps are complete, unless user chooses 1st step 0.--%>
               <%--show link to edit thisstep--%>  
               <% switch (Model.thisStep.Step1)
               {%>
                    <% case 1:%>    <!------------ Step 1 ------------>
                            <div><%= Html.ActionLink("Create New", "Create", new { projID = Model.projectID })%></div>
                            <% break; %>
                    <% case 2: %>   <!------------ Step 2 ------------>
                            <div><%= Html.ActionLink("Edit", "Edit", "Utility", new { projID = Model.projectID }, new { @style = "text-decoration: none; bold; color: #004276;" })%></div>
                            <% break; %>
                    <% case 3: %>   <!------------ Step 3 ------------>
                            <div><%= Html.ActionLink("Edit", "Edit", "Votes", new { projID = Model.projectID }, new { @style = "text-decoration: none; font-weight: bold; color: #004276;" })%></div>
                            <% break; %>
                    <% case 4: %>   <!------------ Step 4 ------------>
                            <div><%= Html.ActionLink("Edit", "Edit", "UtilityRating", new { projID = Model.projectID }, new { @style = "text-decoration: none; font-weight: bold; color: #004276;" })%></div>
                            <% break; %>
                    <% case 5: %>    <!------------ Step 5 Strategy ------------>
                            <div><%= Html.ActionLink("Create New", "Create", "ArchitecturalStrategy", new { projID = Model.projectID }, new { @style = "text-decoration: none; font-weight: bold; color: #004276;" })%></div>
                            <% break; %>
                    <% default: %>
                        <% break; %>
                <%}; %> <%--END SWITCH--%>
      
         <%;}  
         else { %>  <%------ else need to complete a prior step. ------%>
            <span class="ui-state-error"> Must Complete Step <%=Html.Encode(Model.nextStepToComplete.Step1)%>           
                    <a style="color: blue" href="#<%=Html.Encode(Model.nextStepToComplete.TabRef)%>" class="text-link"><%=Html.Encode(Model.nextStepToComplete.Description)%> </a>
                     before continuing.  </span>
       <%;} %>
  
       <%=Html.ValidationSummary("Please correct the errors and try again.") %> 
       <% Html.RenderPartial("StepAccordion", Model); %>  <%--shows accordian helper hints.--%>