<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.StrategyForExpectedResponse>" %>
<%@ Import Namespace="CBAM.Helpers" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>


<script type="text/javascript">
     $().ready(function() {
         

     });

</script>
        
   <style type="text/css">
          .SingleDrop
           {
          	 width: 60px;
             height: 20px;
          }

           .shortInput
            {
                width: 110px;
                text-align:right;
            }

           table td.blankcol
          {
          	background: none;
          	border: none;
          }
          table th.blankcol
          {
          	background: none;
          	border: none;
          }
          
 </style>
 


<% using (Html.BeginForm("EditUtilityDescriptions", "ArchitecturalStrategy", FormMethod.Post))
   { %>
   <br />
     Part 2: Define Expected utility achieved by strategy for each affected scenario.
       <table class="RemovePadding">
                    <%=Html.Hidden("ID", Model.ID)%>
                    
       <%--HEADERS--%>
       <tr> 
            <th rowspan="2" width="150px">Scenario Name</th>
            <th rowspan="2" colspan="2">Expected Utility</th>
            <th rowspan="2"  width="20px" class="blankcol"> </th>
            <th colspan="4"> <span class ="ui-accordion-header ui-helper-reset ui-state-default ui-corner-all">
                            Response Goals/Utility</span></th>
       </tr>
       
       
        <%--HEADERS FOR BEST/WORSE/DESIRED/CURRENT --%>
       <tr>
            <% var grp = Model.ScenariosForStratUtil.First().Utilities.Select(z => z.QualityAttributeResponseType)
                       .OrderBy(a => a.Order)
                       .GroupBy(a => a.Type)
                   ;%>
              
             <% foreach (var item in grp) { %>
                    <th width="100px" >  <%= Html.Encode(item.Key) %> </th>
             <% } %>   
       </tr>
       
       
       <%--//DATA--%>
                
        <% for (var i = 0; i < Model.ScenariosForStratUtil.Count; i++){ %>
      
        <tr class="">
            <td rowspan="2">
                 <%=Html.HiddenFor(m => m.ScenariosForStratUtil[i].scenarioID)%>
                 <%= Html.Encode(Model.ScenariosForStratUtil[i].Name)%> 
                 
            </td>
            
             <td>
                 Description
                 <%=Html.HiddenFor(m => m.ScenariosForStratUtil[i].expectedUtilID)%>
      
            </td>
              
            <td><%--DESCRIPTION--%>  <%--EEXPECTED UTILITY TO DEFINE--%>
                <%=Html.TextBoxFor(m => m.ScenariosForStratUtil[i].ExpectedUtilityDescription, new { @class = "shortInput" })%>
                <%=Html.ValidationMessageFor(m => m.ScenariosForStratUtil[i].ExpectedUtilityDescription)%>
            </td>
            <td class="blankcol"></td>
            
            
             <%--Description--%> <%--EXISTING DEFINITIONS FOR REFERENCE: BEST/WORSE/DESIRED/CURRENT--%>
    
             <% foreach (var subitem in Model.ScenariosForStratUtil[i].Utilities.OrderBy(x => x.QualityAttributeResponseType.Order))
                { %>
                    <td width="100px" align="center" style="border-bottom-style: dotted">
                        <%= Html.Encode(subitem.Description)%>
                    </td>
             <% } %>
             </tr>
           
           
              
              <tr>
              
                <td>
                     Utility 
                </td>
                <td><%--Utility--%>  <%--EEXPECTED UTILITY TO DEFINE--%>
                    <%=Html.TextBoxFor(m => m.ScenariosForStratUtil[i].ExpectedUtility, new { @class = "shortInput" })%>
                    <%=Html.ValidationMessageFor(m => m.ScenariosForStratUtil[i].ExpectedUtility)%>
                </td>

                 <td class="blankcol" > </td>
                 
                 <%--Utility--%> <%--EXISTING DEFINITIONS FOR REFERENCE: BEST/WORSE/DESIRED/CURRENT--%>
                 <% foreach (var subitem in Model.ScenariosForStratUtil[i].Utilities.OrderBy(x => x.QualityAttributeResponseType.Order))
                    { %>
                        <td width="100px" style="color: #004276; border-top-style: dotted" align="center" >
                            <%= Html.Encode(subitem.Utility1)%>
                        </td>
                 <% } %>
            </tr>
            
   <% } %>
       
    
                        
                
                      
            
           </table> 
           <input type="submit" value="Finished" />
           <% Html.EndForm(); %>
<% } %>


 
 