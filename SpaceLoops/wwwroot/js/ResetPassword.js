﻿$(document).ready(function () {

    $('#btnResetPassword').click(function () {
        event.preventDefault();
        var emailId = $('#Email').val();
        if (emailId != null) {

            $.ajax({
                type: 'POST',
                url: '/Address/ResetPassword',
                data: { emailId: emailId }, // Pass the emailId as data to the server
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        alert('Email sent successfully!');
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
        }
        else {
            $('#erroMessage').text(" Please enter your email");
        }

    });
});