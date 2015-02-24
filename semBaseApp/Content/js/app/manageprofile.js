function ProfileViewModel() {
    var self = this;
    self.Id = ko.observable();
    self.FullName = ko.observable();
    self.UserName = ko.observable();
    self.Password = ko.observable();
    self.DateOfBirth = ko.observable();
    self.PhoneNumber = ko.observable();
    self.Email = ko.observable();
    self.IsActive = ko.observable(true);
    self.IsAdministrator = ko.observable(false);
    self.Roles = ko.observable();
    self.Validated = ko.observable(false);
    self.OPassword = ko.observable();
    self.CPassword = ko.observable();
    self.NPassword = ko.observable();
    self.Branch = ko.observable();
    self.City = ko.observable();
    self.Country = ko.observable();
    self.IsAgent = ko.observable(false);

    //Validate the form
    $('#ChangePasswordForm').form(validationRules, {
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

    self.change = function () {
        //$('#PleaseWaitModal').modal('show');
        if (self.Validated()) {
            var model = { OldPassword: self.OPassword(), NewPassword: self.NPassword(), ConfirmPassword: self.CPassword() };
            $.post('change', model, function (rData) {
                if (rData.Success) {
                    setTimeout(function () { return true; }, 3000);
                    window.location = "/Home/Index";
                } //notifyMe(false, rData.Message);
            });
        }
        //$('#PleaseWaitModal').modal('show');
    };
    
    self.getData = function () {
        $.post('get', function (rData) {
            if (rData.Success) {
                var data = rData.Data;
                //set the values Here
                self.Id(data.Id);
                self.FullName(data.FullName);
                self.UserName(data.UserName);
                self.Password('password');
                self.PhoneNumber(data.PhoneNumber);
                self.Email(data.Email);
                self.IsActive(data.IsActive);
                self.Roles(data.Roles);
                self.DateOfBirth(moment(data.DateOfBirth).format('Do MMMM, YYYY'));
                if (data.Roles == "Agent") {
                    self.Branch(data.Branch.Name);
                    self.City(data.Branch.City.Name);
                    self.Country(data.Branch.City.Country.Name);
                    self.IsAgent(true);
                }
                
            }
        });
    };

    self.getData();
}

ko.applyBindings(new ProfileViewModel(), $('#ManageProfilePage').get(0));