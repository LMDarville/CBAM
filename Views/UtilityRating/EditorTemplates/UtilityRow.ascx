<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.utilRating>" %>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>



 

<%-- <% Html.EnableClientValidation();%>  --%>
<span class = "utilityCell">
                <%=Html.TextBoxFor(m => m.Utility, new { @style = "background-color: FFFF84; height: 20px; width:50px;", @class = "numToCheck" })%>
                <%=Html.ValidationMessageFor(m => m.Utility)%>
                <%=Html.HiddenFor(m => m.ID)%> 
 </span>
      
 
<style type="text/css">
    table
    {
    	border-collapse: collapse;
    	padding:0px;
        margin-top:0px; margin-bottom:0px;
    }
    
    
    table td
    {
        padding:0px;
        border: none;
    }

</style>