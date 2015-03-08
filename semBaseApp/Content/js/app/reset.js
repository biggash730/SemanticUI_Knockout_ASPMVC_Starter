$(document).ready(function () {

    function resetViewModel() {
        var self = this;
        self.Email = ko.observable();
        self.showProgress = ko.observable(false);
        
        self.reset = function () {            
            self.showProgress(true);
            //todo: Validate form input and set the validated to true
            if (self.Email() != null) {
                $.post('users/resetpassword', { Email: self.Email() }, function(rData) {
                    self.showProgress(false);
                    toast(rData.Message, 5000, 'rounded');
                    if (rData.Success) window.location = "/Home/Login";
                    else {
                        self.showProgress(false);
                        self.Email('');
                    }
                });
                //clear form 
                self.Email('');
            }
            //self.showProgress(false);
        };
    }
    ko.applyBindings(new resetViewModel(), $('#ResetPage').get(0));
});