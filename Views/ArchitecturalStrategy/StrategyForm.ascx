<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ArchitecturalStrategyViewModel>" %>
<%@ Import Namespace="CBAM.Helpers" %>

<%--Client Validation--%>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>

<script src="../../Scripts/jquery.validate.unobtrusive.js" type="text/javascript"></script>

<script type="text/javascript">
     $().ready(function() {
        $("#add").click(function() {
            return !$("#ScenariosAvailable option:selected").remove().appendTo("#Strategy_ScenariosIDs"); 
         });
         $("#remove").click(function() {
            return !$("#Strategy_ScenariosIDs option:selected").remove().appendTo("#ScenariosAvailable");
        });
        $("form").submit(function() {
            $('#Strategy_ScenariosIDs option').each(function(i) {
                $(this).attr("selected", "selected");
            });
        });
     });

</script>

  <style type="text/css">
          a.form {
           display: block;
           border: 1px solid #aaa;
           text-decoration: none;
           background-color: #fafafa;
           color: #123456;
           margin: 2px;
           clear:both;
          }
         
        
          select.multiselect {
           width: 300px;
           height: 220px;
          }
 </style>

<% Html.EnableClientValidation();%>
<%=Html.ValidationSummary("Please correct the errors and try again.") %>
 <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>

<% using (Html.BeginForm(new { projID = Model.Strategy.ProjectID, id = Model.Strategy.ID }))
   { %>
       Part 1: Define Strategy: Enter name, description and cost, then select scenarios affected by strategy.
       <%=Html.HiddenFor(m => m.Strategy.ProjectID)%>
        <table>
            <tr>
                <th><label for="Name">Name:</label></th>
                <th> <label for ="Cost">Cost:</label></th>
                <th colspan="3"> <label for="Description">Description:</label>
                </th>
            </tr>
            <tr>
                <td>
                    <%=Html.Hidden("ID", Model.Strategy.ID)%>
                    <%=Html.TextAreaFor(m => m.Strategy.Name)%>
                    <%=Html.ValidationMessageFor(m => m.Strategy.Name)%>
               </td>
               <td>
                    <%=Html.TextBoxFor(m => m.Strategy.Cost)%>
                    
                    <%=Html.ValidationMessageFor(m => m.Strategy.Cost)%>
               </td>
               <td  colspan="3">
                    <%=Html.TextAreaFor(m => m.Strategy.Description, new { cols= "80", @class = "textarea"})%>
                    <%=Html.ValidationMessageFor(m => m.Strategy.Description)%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-bottom-style: none;">
                        <label style="vertical-align: top;"  for="Scenarios Affected">Select Scenarios Affected:</label>
                </td>
            </tr> 
             <tr>
                <td colspan="4" style="color: silver; border-top-style: none; border-bottom-style: none;" >
                        Use Ctl key to select multiple or deselect.
                </td>
            </tr>  
            <tr> 
                 <td colspan="2" style="border-top-style: none;">  
                        <%= Html.ListBox("ScenariosAvailable", Model.ScenarioSelectList as MultiSelectList, new { @class = "multiselect" })%>
                      
                 </td>
             
            <td colspan="1" style="border-top-style: none;">
                   <a class= "form" href="#" id="add">add >></a>
                   <br />
                   <a class= "form" href="#" id="remove"><< remove</a>
            </td>
       
            <td colspan="1" style="border-top-style: none;">
                   <div>
                        <%= Html.ListBoxFor(m => m.Strategy.ScenariosIDs, Model.ScenariosSelectedList as MultiSelectList, new { @class = "multiselect" })%>
                        <%=Html.ValidationMessageFor(m => m.ScenariosIDs)%>
                     
                  </div>
            </td>
        </tr>
        
      
           
        </table> 
           <input type="submit" value="Save Strategy" />  
       <div class="ui-widget">
			<div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em;"> 
				<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
				<strong>Hint!</strong> Save Strategy to update affected scenarios below.</p>
			</div>

		</div>

  
<% } %>
   