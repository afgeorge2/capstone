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

//$('#myCarousel').carousel({
//    interval: 40000
//});

//$('.carousel .item').each(function () {
//    var next = $(this).next();
//    if (!next.length) {
//        next = $(this).siblings(':first');
//    }
//    next.children(':first-child').clone().appendTo($(this));

//    if (next.next().length > 0) {

//        next.next().children(':first-child').clone().appendTo($(this)).addClass('rightest');

//    }
//    else {
//        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));

//    }
//});
$('.multi-item-carousel').carousel({
    interval: false
});

// for every slide in carousel, copy the next slide's item in the slide.
// Do the same for the next, next item.
$('.multi-item-carousel .item').each(function () {
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    if (next.next().length > 0) {
        next.next().children(':first-child').clone().appendTo($(this));
    } else {
        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
    }
});