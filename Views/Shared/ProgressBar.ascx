<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.StepsViewModel>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>


<script type="text/javascript">
    $(document).ready(function() {

        $(function() {
                $("#progressbar").progressbar({ value: 0 });
                updateProgress();
                $("#result").html("HI");
        }); //end progress bar

        function updateProgress() {
            //var value = $("#progressbar").progressbar("option", "value"); //current value
           // debugger;
            var completed = $("#nextStep").find("input[type='hidden']").val() - 1;
            var total = $("#totalSteps").find("input[type='hidden']").val();
            var value = completed / total * 100;
            if (value > 100) { 
                value = 100;
            }

            if (value <= 100) {
                $("#progressbar").progressbar("option", "value", value);
            }
        }

    });            //document ready

</script>
 <style type="text/css">
           .tableprogress table td
            {
             padding: 0;
             
             }
             
             .ui-progressbar .ui-progressbar-value {
                height: 40%;
                margin: -1px;
            }

         
     </style>
 
          
  <span id="nextStep"> <%=Html.Hidden("nextStep",Model.nextStepToComplete.Step1)%> </span>            
  <span id="totalSteps"> <%=Html.Hidden("totalSteps",Model.Steps.Count)%> </span>  
  


 
     <table width="100%" class="tableprogress">
             <tr>
                <td  id="progressbar" colspan="5">
                
                    <table width="100%"><tr>
                    
                    <% for (var i = 0; i < Model.Steps.Count; i++)
                       { %>
                        <td height="10px">
                        <div class="progressSteps">step<%= Html.Encode(Model.Steps[i].Step1)%>
                        </div>
                        </td>
                       
                    <% } %>       
                    
                    
                     </tr>
                    </table>
                </td>
            </tr> 
      
        
  </table>

