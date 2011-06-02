<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.ScenarioListUtilRatings>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>

 
<%-- <% Html.EnableClientValidation();%>  --%> 
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.validate.unobtrusive.js") %>"></script>

<script type="text/javascript">

    $(document).ready(function () {

        //highlight blanks
        $(".numToCheck").each(function () {
            $(this).keyup(function () {
                if (isNaN(this.value) || this.value.length == 0) {
                    // this.attr("class", "highlight");
                    this.className = "highlightOnly";
                }
                else this.className = ''; //clear error format
            }); //end keyup
        }); //end .each numToCheck


        //initizlize highlights
        $('.numToCheck').each(function () {
            if (isNaN(this.value) || this.value.length == 0) {
                // this.attr("class", "highlight");
                this.className = "highlightOnly";
                $("#errMessage").html("*Rating is required");
            }
            else this.className = ''; //clear error format
            //end if
        }); //end .each


    });            // end doc ready

    function get_nextsibling(n) {
        x = n.nextSibling;
        while (x.nodeType != 1) {
            x = x.nextSibling;
        }
        return x;
    }

</script>


<%=Html.ValidationSummary(true,"Please correct the errors.") %>  
 <div class="ui-state-highlight"><%=Html.Encode(ModelStateHelpers.ModelMessage)%> </div>
<div id="errMessage" class="field-validation-error"> </div>

<%-- field-validation-valid--%>

     <% using (Html.BeginForm("Edit", "UtilityRating")){ %>
     <%=Html.HiddenFor(x => x.projectID)%> 

     <table class="RemovePadding1px">
        <tr>
            <th rowspan="2">Scenario</th>
            <th colspan="4"> <span class ="">
                            Response Goals/Utility</span></th>
        </tr>
        <tr>
            <% var grp = Model.ScenariosForUtilRatingUpdate.First().QualityAttributeResponseTypes
                       .OrderBy(a => a.Order)
                       .GroupBy(a => a.Type)
                   ;%>
              
             <% foreach (var item in grp) { %>
                    <th class="UtilTable" >  <%= Html.Encode(item.Key) %> </th>
             <% } %>   
        </tr>
        <%----end headers----%>

        <%----data----%>    
        <tr>
        
            <% for(var i =0; i<Model.ScenariosForUtilRatingUpdate.Count; i++) { %>
                           <%= Html.EditorFor(x => x.ScenariosForUtilRatingUpdate[i], "ScenarioItem")%>
                <% } %> 
        </tr>   
     </table>
     <input type="submit" name="Edit" value="Save" />
            
   <%}%>  
   
 