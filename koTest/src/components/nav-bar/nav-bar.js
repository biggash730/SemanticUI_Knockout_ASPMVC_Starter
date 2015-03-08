define(['jquery', 'knockout', 'text!./nav-bar.html'], function ($, ko, template) {
    

    function navBarViewModel(params) {
        $("#userDropdown").dropdown({
            onChange: function (val) {
                if (val === 'logout') {
                    //ClearToken();
                }
            }
        });

        $("#userDropdown1").dropdown({
            onChange: function (val) {
                if (val === 'logout') {
                    //ClearToken();
                }
            }
        });

        $("#menuDropdown").dropdown();
        $("#menuDropdown1").dropdown();

        // This viewmodel doesn't do anything except pass through the 'route' parameter to the view.
        // You could remove this viewmodel entirely, and define 'nav-bar' as a template-only component.
        // But in most apps, you'll want some viewmodel logic to determine what navigation options appear.

        this.route = params.route;
    }

    return { viewModel: navBarViewModel, template: template };
});
