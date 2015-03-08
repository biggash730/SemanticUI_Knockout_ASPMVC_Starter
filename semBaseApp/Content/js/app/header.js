$(document).ready(function () {
    $(".dropdown-button").dropdown();
    /*$('.button-collapse').sideNav({
        menuWidth:200, // Default is 240
        edge: 'left', // Choose the horizontal origin
        closeOnClick: true // Closes side-nav on <a> clicks, useful for Angular/Meteor
    });*/
    $('.button-collapse').sideNav();

    $('.collapsible').collapsible({
        accordion: false // A setting that changes the collapsible behavior to expandable instead of the default accordion style
    });

});