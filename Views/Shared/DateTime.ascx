
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>
<%=Html.TextBox("", (Model.HasValue ? Model.Value.ToString("MM/dd/yy") : DateTime.Today.ToShortDateString()), new { @class = "UseDatePicker date-text-box" })%>

[DisplayFormat(DataFormatString="{0:MM/dd/yyyy}", ApplyFormatInEditMode=true)]

<div class="display-field"><%=Html.Encode(Model.HasValue ? Model.Value.ToShortDateString() : string.Empty)%></div>  