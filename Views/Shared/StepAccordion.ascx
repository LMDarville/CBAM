<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.StepsViewModel>" %>

<script type="text/javascript">
    $(document).ready(function() { 
       
       $(function() {
            $(".accordion").accordion({
                collapsible: true,
                active: false
            });
        }); //accordion//accordion
    });            //document ready


</script>


                        
<div class="accordion">  <%------------------ Step 1 ----------------%>
    <h3><a href="#">Step <%=Html.Encode(Model.thisStep.Step1)%>. <%=Html.Encode(Model.thisStep.Description)%></a></h3>
    <div> <%= Model.thisStep.Instructions %>  </div>
  
  <%--  <h3><a href="#">Second header</a></h3>
    <div>Second content</div>--%>
</div>

