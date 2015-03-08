require(['lib/jquery.min'], function ($) {
    alert('start main.js ');

    $.ajaxSetup({
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', localStorage.getItem('token'));
        }
    });

    function ClearToken() {
        localStorage.token = "";
    }

    var Pager = function (page) {
        this.Page = page;
        this.Size = 25;
    };
    var allSize = 1000;

    var PostData = function (url, data) {
        $.post(url, data, function (rData) {
            return rData;
        });
    };

    var GetData = function (url) {
        $.getJSON(url, function (rData) {
            return rData;
        });
    };

    console.log('end something');

});