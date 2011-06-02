<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CBAM.Models.util>" %>
<%@ Import Namespace="CBAM.Models"%>
<%@ Import Namespace="CBAM.Helpers"%>


    <% Html.EnableClientValidation();%>
 <table id="tableScenarios" class="">
        <tr>
            <td width="70px" align="right">
                <%= Html.Encode(Model.QualityAttributeResponseTypeType) %>
            </td> 
            <td width="10px">
            </td> 
            <td> 
                <%=Html.TextAreaFor(m => m.Description, new { @style = "background-color: FFFF84; height: 20px; width: 600px;" })%>
                <%=Html.ValidationMessageFor(m => m.Description)%>
                <%=Html.HiddenFor(m => m.ID)%>
             </td>
       </tr>
 </table> 
 
 
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