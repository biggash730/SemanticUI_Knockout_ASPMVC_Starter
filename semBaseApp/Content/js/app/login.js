$(document).ready(function () {

    function loginViewModel() {
        var self = this;
        self.Username = ko.observable();
        self.Password = ko.observable();
        self.RememberMe = ko.observable(true);
        self.Validated = ko.observable(false);
        
        //Validate the form
        $('#LoginForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                //do something on failure
            },
            onSuccess: function () {
                //check the validation
                self.Validated(true);
            }
        });
        
        self.login = function () {
            if (self.Validated()) {
                showNag("Login initiated", true);
                $.post('users/login', { UserName: self.Username(), Password: self.Password(), RememberMe: self.RememberMe() }, function(rData) {
                    if (rData.Success) {
                        showNag("Logged in Successfully", true);
                        localStorage.token = "Bearer " + rData.Message;
                        setTimeout(function() { return true; }, 3000);
                        window.location = "/Home/Index";
                    } else showNag("Please check the login details", false);

                });
                //clear form 
                self.clearValues();
            }
        };
        
        self.clearValues = function () {
            self.Username('');
            self.Password('');
            self.RememberMe(true);
        };
    }
    ko.applyBindings(new loginViewModel(), $('#LoginPage').get(0));
});