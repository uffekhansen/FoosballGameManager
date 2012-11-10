function copyListToList(sourceListId, destinationListId)
{
	var selectedItems = [];

	$('#' + sourceListId + ' :selected').each
	(
		function (i, selected) {
			selectedItems[i] = selected;
		}
	);

	$.each(selectedItems, function (i, selection)
	{
		$('#' + destinationListId).append($('<option></option>').val(selection.value).text(selection.text));
		$('#' + sourceListId + '  option[value=' + selection.value + ']').remove();
	});

	sortDropDownListByText(destinationListId);
}

function sortDropDownListByText(selectId)
{
	// Loop for each select element on the page.
	$("select#" + selectId).each(function () {

		// Keep track of the selected option.
		var selectedValue = $(this).val();

		// Sort all the options by text. I could easily sort these by val.
		$(this).html($("option", $(this)).sort(function (a, b) {
			return a.text == b.text ? 0 : a.text < b.text ? -1 : 1;
		}));

		// Select one option.
		$(this).val(selectedValue);
	});
}
