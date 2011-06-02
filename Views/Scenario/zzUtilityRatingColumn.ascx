<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Utility>>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
  
   
<script type="text/javascript">
    $(document).ready(function() {
    });        //document ready
</script>
        
            <% foreach (var item in Model) { %>
                    <td width="100px" align="left">
                        <%= Html.Encode(item.Description) %>
                    </td>
                    <td width="100px" align="left">
                        <%= Html.Encode(item.Utility1) %>
                    </td>
            <% } %>
        

