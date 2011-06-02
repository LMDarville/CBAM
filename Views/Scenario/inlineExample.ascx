<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CBAM.Models.Utility>>" %>

<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.inline-edit.goog.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.inline-edit.js") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery.inline-edit.min.js") %>"></script>

  <%--    formats    --%>
     <style type="text/css">
            .hover {background: #ffC;}
     </style>

<script type="text/javascript">
  $(document).ready(function() {

  jQuery(function($) {
     $('.inline-edit').inlineEdit({ hover: 'hover' })
     $('.inline-edit').inlineEdit({ hover: 'hover' })
  });

    });        //document ready
 </script>
    
    

<div class='inline-edit'>
	<div class='display'>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Morbi lacus felis, euismod at, pulvinar sit amet, dapibus eu, eros. Etiam tellus. Nam vestibulum porttitor urna. Phasellus aliquet pretium quam. Proin pharetra, wisi nec tristique accumsan, magna sapien pulvinar purus, vel hendrerit ipsum tellus at ante.</div>
	<div class='form'>

		<div><div><textarea class='text'></textarea></div></div>
		<div>
			<input type='submit' class='save' value=' Save ' />
			<input type='submit' class='cancel' value=' Cancel ' />
		</div>
	</div>
</div>

<div class='inline-edit' style='border: none;'>
	<div class='display'>Unkown Author</div>

	<div class='form'>
		<input type='text' class='text' /><br>
		<input type='submit' class='save' value=' Save ' />
		<input type='submit' class='cancel' value=' Cancel ' />
	</div>
</div>
