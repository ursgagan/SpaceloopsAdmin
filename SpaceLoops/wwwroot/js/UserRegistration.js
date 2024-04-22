$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        toastr.success("Record Added", "Successfully Added");
    }
    if ($("#hdncountryId").val() != null && $("#hdncountryId").val() != '') {
        GetStatesByCountryId($("#hdncountryId").val());

    }
    if ($("#hdnStateId").val() != null && $("#hdnStateId").val() != '') {
        GetCitiesByStatesId($("#hdnStateId").val());

    }
    if ($("#TempExistData").val() != null && $("#TempExistData").val() != '') {
        toastr.success("Akready Exist", "Email Already Exists");
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
            debugger
            
            $('#StateId').empty(); 
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

$('#StateId').on('change', function () {
    var selectedStateId = $(this).val();

    GetCitiesByStatesId(selectedStateId);

});

function GetCitiesByStatesId(selectedStateId) {
    $.ajax({
        url: '/Address/GetCitiesByStatesId', 
        type: 'GET',
        data: { stateId: selectedStateId },
        success: function (data) {
            debugger;
           
            $('#CityId').empty(); 
            $.each(data, function (index, city) {
                debugger;
                $('#CityId').append($('<option>', {
                    value: city.id,
                    text: city.cityName
                }));
            });
            if ($("#hdnCityId").val() != null) {
                $('#CityId').val($("#hdnCityId").val());
            }
        },
        error: function (error) {
            console.error('Error fetching city: ' + error);
        }
    });
}