$(document).ready(function () {
    if ($("#hdnTempData").val() != null && $("#hdnTempData").val() != '') {
        toastr.success("Record Added", "Successfully Added");
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
        // Make an AJAX call to delete the item
        $.ajax({
            type: "POST",  // or "GET" depending on your server-side implementation
            url: "/Address/DeleteCountry",  // specify the URL to your server-side delete endpoint
            data: { countryId: Id },  // pass the ID to delete
            success: function (response) {

                Swal.fire({
                    title: "Deleted!",
                    text: "Country has been deleted.",
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
