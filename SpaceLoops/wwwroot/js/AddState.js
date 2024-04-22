$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        toastr.success("Record Added", "Successfully Added");
    }
});

$(document).ready(function () {
    if ($("#hiddenTempData").val() != null && $("#hiddenTempData").val() != '') {
        toastr.success("Record Updated", "Successfully Updated");
    }
});

$("#CountryId").change(function () {

    var end = this.value;
    alert(end);
});