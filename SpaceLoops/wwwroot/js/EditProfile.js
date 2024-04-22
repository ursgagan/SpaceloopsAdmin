
$('#PhoneNumber').on('keydown', function (e) {
    // Allow: backspace, delete, tab, escape, enter
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // Let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) &&
        (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});


$("#btnEditProfile").click(function () {


    var FirstName = $("#FirstName").val();
    var LastName = $("#LastName").val();
    var PhoneNumber = $("#PhoneNumber").val();
    var EmailId = $("#EmailId").val();
    var isValid = false;

    if (FirstName == "") {
        toastr.error("Please Fill First Name", " Invalid First Name");
        $("#FirstName").focus();
        isValid = false;
        return false;
    }
    else {
        isValid = true;
    }
    if (LastName == "") {
        toastr.error("Please Fill Last Name", " Invalid Last Name");
        $("#LastName").focus();
        isValid = false;
        return false;
    }
    else {
        isValid = true;
    }

    if (PhoneNumber == "") {
        toastr.error("Please Fill Phone Number", " Invalid Phone Number");
        $("#PhoneNumber").focus();
        isValid = false;
        return false;
    }
    else {
        isValid = true;
    }

    if (PhoneNumber.length < 10) {
        toastr.error("Please enter correct Phone Number", " Invalid Phone Number");
        $("#PhoneNumber").focus();
        isValid = false;
        return false;
    }
    else {
        isValid = true;
    }


    if (isValid) {


        var formData = {};

        formData.FirstName = FirstName;
        formData.LastName = LastName;
        formData.PhoneNumber = PhoneNumber;
        formData.EmailId = EmailId;

        $.ajax({
            type: 'POST',
            url: '/Address/EditProfile',
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: { userRegistration: formData },

            success: function (response) {
                
                if (response) {

                    toastr.success("Profile Updated", "User Profile is updated ");
                } else {
                    console.log('User not found or error updating profile!');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error updating profile:', error);
            }
        });
    }
});


$("#uploadImageBtn").click(function () {
    debugger

    var fileInput = $('#Image')[0];
    if (fileInput.files.length > 0) {
        var file = fileInput.files[0];
        var formData = new FormData();
        formData.append('imageFile', file);
    }
    else {
        console.error('No file selected.');
    }

    $.ajax({
        url: '/Address/UploadImage',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {

            if (response = true) {
                debugger;
                $("#DisplayedImage").attr("src", $("#b64").val());
                toastr.success("Image Uploaded successfully", "Image Uploaded")
                $('#Image').val('');
            }
        },
        error: function (xhr, status, error) {
            // Handle error response
            console.error(xhr.responseText);
        }
    })
});


function readFile() {

    if (!this.files || !this.files[0]) return;

    const FR = new FileReader();

    FR.addEventListener("load", function (evt) {
        // document.querySelector("#DisplayedImage").src = evt.target.result;
        $("#b64").val(evt.target.result);

    });

    FR.readAsDataURL(this.files[0]);

}

document.querySelector("#Image").addEventListener("change", readFile);
