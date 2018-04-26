/// <reference path="../jquery-3.3.1.js" />
/// <reference path="../jquery.validate.js" />

$(document).ready(function () {
    $("#registration").validate({
        rules: {
            UserName: {
                required: true

            },

            Password: {
                required: true,
                minlength: 8
            },

            EmailAddress: {
                email: true,
                required: true
            },

        },
        messages: {
            UserName:{
                required: "Must be a valid username."
            },
            EmailAddress: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            Password: {
                required: "Password is required.",
                minlength: "Password must be at least 8 characters long."
            },
            errorClass: "error",
            validClass: "valid"
        },
    });
});