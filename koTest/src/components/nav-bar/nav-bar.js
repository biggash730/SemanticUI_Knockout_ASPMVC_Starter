define(['knockout', 'text!./nav-bar.html', 'jquery'], function (ko, template, $) {
    
    function navBarViewModel(params) {
        var self = this;
        self.User = ko.observable();
        self.FullName = ko.observable();
        self.loggedIn = ko.observable(false);
        if (localStorage.koToken !== "")
            self.loggedIn(true);

        //alert(self.loggedIn());
        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', localStorage.getItem('koToken'));
            }
        });
        $("#userActions").dropdown();
        $("#userActions1").dropdown();
        $("#menuDropdown").dropdown();
        // This viewmodel doesn't do anything except pass through the 'route' parameter to the view.
        // You could remove this viewmodel entirely, and define 'nav-bar' as a template-only component.
        // But in most apps, you'll want some viewmodel logic to determine what navigation options appear.

        self.route = params.route;
        self.logout = function () {
            localStorage.koToken = "";
            var newloc = window.location.href.replace(window.location.hash, '#login');
            window.location.href = newloc;
            window.location.reload();
        };

        self.getUser = function () {
            $.getJSON(serverAddress + 'users/get', function (rData) {
                if (rData.Success) {
                    self.User(rData.Data);
                    self.FullName(rData.Data.FullName);
                }
            });
        };
        self.getUser();
    }

    return { viewModel: navBarViewModel, template: template };
});
