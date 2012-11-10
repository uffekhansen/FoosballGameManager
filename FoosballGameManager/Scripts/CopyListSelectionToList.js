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
