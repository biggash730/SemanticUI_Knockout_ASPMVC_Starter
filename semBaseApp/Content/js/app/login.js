$(document).ready(function () {

    function loginViewModel() {
        var self = this;
        self.Username = ko.observable();
        self.Password = ko.observable();
        self.RememberMe = ko.observable(true);
        self.Validated = ko.observable(false);
        self.showProgress = ko.observable(false);
        
        //Validate the form
        /*$('#LoginForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                //do something on failure
            },
            onSuccess: function () {
                //check the validation
                self.Validated(true);
            }
        });*/
        
        self.login = function () {            
            self.showProgress(true);
            //todo: Validate form input and set the validated to true
            if (!self.Validated()) {
                toast('Login initiated', 2000, 'rounded')
                $.post('users/login', { UserName: self.Username(), Password: self.Password(), RememberMe: self.RememberMe() }, function(rData) {
                    self.showProgress(false);
                    if (rData.Success) {
                        toast('Logged in Successfully', 3000, 'rounded')
                        localStorage.token = "Bearer " + rData.Message;
                        setTimeout(function () { return true; }, 3000);
                        window.location = "/Home/Index";
                    } else {
                        toast('Please check the login details', 5000, 'rounded')
                        self.showProgress(false);
                        self.clearValues();
                    }

                });
                //clear form 
                self.clearValues();
            }
            //self.showProgress(false);
        };
        
        self.clearValues = function () {
            self.Username('');
            self.Password('');
            self.RememberMe(true);
        };
    }
    ko.applyBindings(new loginViewModel(), $('#LoginPage').get(0));
});