$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        toastr.success("Record Added", "Successfully Added");
    }

    if ($("#hiddenTempData").val() != null && $("#hiddenTempData").val() != '') {
        toastr.success("Record Updated", "Successfully Updated");
    }
});