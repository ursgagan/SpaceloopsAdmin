$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        toastr.success("Record Added", "Successfully Added");
    }
    if ($("#hiddenTempData").val() != null && $("#hiddenTempData").val() != '') {
        toastr.success("Record Updated", "Successfully Updated");
    }
    if ($("#hdncountryId").val() != null && $("#hdncountryId").val() != '') {
        GetStatesByCountryId($("#hdncountryId").val());

    }
});


$('#CountryId').on('change', function () {
    var selectedCountryId = $(this).val(); // Get the selected country value
    GetStatesByCountryId(selectedCountryId);
});


function GetStatesByCountryId(selectedCountryId) {
    $.ajax({
        url: '/Address/GetStatesByCountryId',
        type: 'GET',
        data: { countryId: selectedCountryId },
        success: function (data) {
           
            $('#StateId').empty(); // Clear existing options
            $.each(data, function (index, state) {
                debugger;

                $('#StateId').append($('<option>', {
                    value: state.id,
                    text: state.stateName
                }));
            });
            if ($("#hdnStateId").val() != null) {
                $('#StateId').val($("#hdnStateId").val());
            }
        },
        error: function (error) {
            console.error('Error fetching states: ' + error);
        }
    });
}
