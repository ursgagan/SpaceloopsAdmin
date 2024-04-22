$(document).ready(function () {

    $("#btnResetPassword").click(function () {
        debugger;
        // Get the values of the password fields
        var newPassword = $("#NewPassword").val();
        var confirmPassword = $("#ConfirmPassword").val();
        var resetCodeId = $("#ResetCode").val();

        // Compare and validate the passwords
        if (newPassword !== confirmPassword) {
            // Display an error message or perform any other action
            alert("New Password and Confirm Password do not match");
            return false; // Prevent form submission
        }


        $.ajax({
            type: 'POST',
            url: '/Address/ForgotPassword',
            data: { password: newPassword, resetCodeId: resetCodeId }, // Pass the emailId as data to the server
            dataType: 'json',
            success: function (data) {

                if (data) {
                    debugger;
                    window.location.href = '/Address/UserLogin';

                }
                else {
                    alert('Email not send!');
                }
            },
            error: function (error) {
                // Handle errors, e.g., show an error message
                console.error('Error sending email:', error);
            }
        });

    });
});
