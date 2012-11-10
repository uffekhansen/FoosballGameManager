function sortDropDownListByText(selectId)
{
	// Loop for each select element on the page.
	$("select#" + selectId).each(function ()
	{
		// Keep track of the selected option.
		var selectedValue = $(this).val();

		// Sort all the options by text. I could easily sort these by val.
		$(this).html($("option", $(this)).sort(function (a, b)
		{
			return a.text == b.text ? 0 : a.text < b.text ? -1 : 1;
		}));

		// Select one option.
		$(this).val(selectedValue);
	});
}