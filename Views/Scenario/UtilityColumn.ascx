<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Utility>>" %>
<%@ Import Namespace="CBAM.Controllers"%>
<%@ Import Namespace="CBAM.Models"%>
  
   
<script type="text/javascript">
    $(document).ready(function() {
    });        //document ready
</script>
        
            <% foreach (var item in Model) { %>
                    <%--<th  align="left">
                        <%= Html.Encode(item.QualityAttributeResponseType.Type) %>
                    </th>--%>
                    
                    <td class="cellData" style="border: 1px solid #E8EEF4;"  width="100px" align="left">
                        <%= Html.Encode(item.Description) %>
                    </td>
            <% } %>
        

