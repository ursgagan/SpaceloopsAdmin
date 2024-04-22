$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        // alert('@TempData["SuccessMessage"]');
        toastr.success("Record Added", "Successfully Added");
    }
    if ($("#hiddenTempData").val() != null && $("#hiddenTempData").val() != '') {
        toastr.success("Record Updated", "Successfully Updated");
    }
});

function functionConfirm(Id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: "POST",
                url: "/Address/DeleteUser",
                data: { userId: Id },
                success: function (response) {

                    Swal.fire({
                        title: "Deleted!",
                        text: "User has been deleted.",
                        icon: "success",
                    });
                    setTimeout(function () {
                        window.location.reload(1);
                    }, 3000);
                },
                error: function () {
                    Swal.fire({
                        title: "Error!",
                        text: "Failed to communicate with the server.",
                        icon: "error"
                    });
                }
            });
        }
    });
    return false;
}
