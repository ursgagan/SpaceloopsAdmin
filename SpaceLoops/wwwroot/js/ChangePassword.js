
$(document).ready(function () {
        $("#btnChangePassword").click(function () {
            debugger;
            // Get the values of the password fields
            var currentPassword = $("#CurrentPassword").val();
            var newPassword = $("#NewPassword").val();
            var confirmPassword = $("#ConfirmPassword").val();
            var isValid = false;

            if (currentPassword == "")
            {         
                $("#erroMessage").text("Please insert Current password");
                $("#erroMessage").css("color", "red");
                $("#CurrentPassword").focus();
                isvalid = false;
                return false;
            }
            else {
                isValid = true;               
            }

            if (newPassword == "" ) {
                $("#erroMessage").text("Please insert New password");
                $("#erroMessage").css("color", "red");
                $("#NewPassword").focus();
                isvalid = false;
                return false;
            }

            else {
                isValid = true;
            }
            if (confirmPassword == "") {
                $("#erroMessage").text("Please insert Confirm password");
                $("#erroMessage").css("color", "red");
                $("#ConfirmPassword").focus();
                isvalid = false;
                return false;
            }
            else {
                isValid = true;
            }

            // Compare and validate the passwords
            if (newPassword !== confirmPassword) {
                // Display an error message or perform any other action
                $("#erroMessage").text("Confirm Password does not match New Password");
                $("#erroMessage").css("color", "red");
                return false; // Prevent form submission
            }
            
            $.ajax({
                type: 'POST',
                url: '/Address/CheckPassword',
                data: {
                    currentPassword: currentPassword,
                    newPassword: newPassword,
                    confirmPassword: confirmPassword
                }, 
                dataType: 'json',
                success: function (data) {

                    if (data) {

                        toastr.success("Password Changed Successfully ", "Password Changed");

                        $('#CurrentPassword').val('');
                        $('#NewPassword').val('');
                        $('#ConfirmPassword').val('');

                        setTimeout(function () {
                            window.location.href = '/Address/UserLogout';
                        }, 3000);
                        
                    }
                    else {
                        $("#erroMessage").text("Current password is Incorrect");
                        $("#erroMessage").css("color", "red");
                    }
                }
            });
          
    })
    });