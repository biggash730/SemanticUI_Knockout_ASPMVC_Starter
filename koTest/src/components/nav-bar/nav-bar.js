define(['knockout', 'text!./nav-bar.html', 'jquery'], function (ko, template, $) {
    
    function navBarViewModel(params) {
        var self = this;
        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', localStorage.getItem('token'));
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
            console.log("logout clicked");
            localStorage.token = "";
        };
    }

    return { viewModel: navBarViewModel, template: template };
});
