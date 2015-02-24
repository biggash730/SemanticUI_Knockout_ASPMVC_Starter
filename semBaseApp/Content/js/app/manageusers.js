$(document).ready(function () {
    
   /* $('#IsActiveCheckBox').checkbox();
    $('#IsAdminCheckBox').checkbox();*/

    function usersViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.FullName = ko.observable();
        self.UserName = ko.observable();
        self.Password = ko.observable();
        self.Id = ko.observable();
        self.DateOfBirth = ko.observable();
        self.PhoneNumber = ko.observable();
        self.Email = ko.observable();
        self.IdNumber = ko.observable();
        self.IsActive = ko.observable(true);
        self.IsAdministrator = ko.observable(false);
        self.Roles = ko.observable();
        self.RolesData = ko.observableArray([]);
        self.RoleId = ko.observable();
        self.RoleName = ko.observable();
        self.BranchName = ko.observable();
        self.Branches = ko.observableArray([]);
        self.BranchId = ko.observable();
        self.IsAgent = ko.observable(false);
        self.Validated = ko.observable(false);
        self.showAddView = ko.observable(false);
        self.showViewView = ko.observable(false);
        self.FormTitle = ko.observable();
        self.Page = ko.observable(0);
        self.Size = ko.observable(20);
        var pager = new Pager(self.Page());

        //Validate the form
        $('#AddUserForm').form(validationRules, {
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
        
        /*$("#selectRoleDropdown:selected").text({
            onChange: function (val) {
                console.log(val);
            }
        });*/
        self.roleChanged = function () {
            self.IsAgent(false);
            if (self.RoleName() == "Agent") {
                self.IsAgent(true);
            }
        };

        self.showAdd = function () {
            self.FormTitle('Add New User');
            self.showAddView(!self.showAddView());
            self.showViewView(false);
            self.clearValues();
        };
        self.closeForms = function () {
            self.showAddView(false);
            self.showViewView(false);
            self.clearValues();
        };

        self.add = function () {
            //$('#PleaseWaitModal').modal('show');
            if (self.Validated()) {
                //var role = self.IsAdministrator() ? administratorString : userString;
                var userModel = { Id: self.Id(), FullName: self.FullName(), UserName: self.UserName(), Password: self.Password(), Email: self.Email(), PhoneNumber: self.PhoneNumber(), IsActive: self.IsActive(), Roles: self.RoleName(), DateOfBirth: moment(self.DateOfBirth()).format('YYYY-MM-DD'), IdNumber: self.IdNumber(), BranchId: self.BranchId() };
                if (self.Id() !='') {
                    $.post('users/update', userModel, function (rData) {
                        if (rData.Success) {
                            self.closeForms();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('users/register', userModel, function (rData) {
                        if (rData.Success) {
                            self.closeForms();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                }
            }
            //$('#PleaseWaitModal').modal('hide');
        };
        
        self.showView = function (data) {
            //set the values Here
            self.Id(data.Id);
            self.FullName(data.FullName);
            self.UserName(data.UserName);
            self.DateOfBirth(moment(data.DateOfBirth).format('Do MMMM, YYYY'));
            self.PhoneNumber(data.PhoneNumber);
            self.Email(data.Email);
            self.IsActive(data.IsActive);
            self.Roles(data.Roles);
            self.IdNumber(data.IdNumber);
            if (data.BranchId>0) {
                self.BranchId(data.BranchId);
                self.BranchName(data.Branch.Name);
                self.IsAgent(true);
            }
            self.showAddView(false);
            self.showViewView(!self.showViewView());
        };

        self.showEdit = function (data) {
            //set the values Here
            self.Id(data.Id);
            self.FullName(data.FullName);
            self.UserName(data.UserName);
            self.Password('password');
            self.DateOfBirth(moment(data.DateOfBirth).format('YYYY-MM-DD'));
            self.PhoneNumber(data.PhoneNumber);
            self.Email(data.Email);
            self.IsActive(data.IsActive);
            self.Roles(data.Roles);
            self.RoleName(data.Roles);
            self.IdNumber(data.IdNumber);
            if (data.BranchId > 0) {
                self.BranchId(data.BranchId);
                self.BranchName(data.Branch.Name);
                self.IsAgent(true);
            }

            $('#username').attr("disabled", "disabled");
            $('#password').attr("disabled", "disabled");
            
            self.FormTitle('Update User Details');
            self.showAddView(!self.showAddView());
            self.showViewView(false);
        };

        self.showDeactivate = function (data) {
            //set the values Here
            self.Id(data.Id);
            $('#UserDeactivateModal').modal('show');
        };

        self.deactivate = function () {
            $.post('users/deactivate?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                }
            });
        };

        self.clearValues = function () {
            self.Id('');
            self.FullName('');
            self.UserName('');
            self.Password('');
            self.DateOfBirth('');
            self.PhoneNumber('');
            self.Email('');
            self.IsActive(true);
            self.Roles('');
            self.IdNumber('');
            self.BranchId(0);
            self.BranchName('');
            $('#username').removeAttr("disabled");
            $('#password').removeAttr("disabled");
        };
        
        self.nextPage = function () {
            if (self.data().length >= self.Size()) {
                self.Page(self.Page() + 1);
                self.getData();
            }
        };

        self.prevPage = function () {
            if (self.Page() > 1) {
                self.Page(self.Page() - 1);
                self.getData();
            }
        };

        self.getData = function () {
            $.post('users/getall?page=' + self.Page() + '&size=' + self.Size(), function (rData) {
                self.data([]);
                if (rData.Success) self.data(rData.Data);
                showNag(rData.Message, rData.Success);
            });
        };
        
        $.post('roles/getall', { Pager: new Pager(0, allSize) }, function (rData) {
            if (rData.Success) self.RolesData(rData.Data);
        });
        
        $.post('branches/getall?page=0&size=0', function (rData) {
            if (rData.Success) self.Branches(rData.Data);
        });

        self.getData();
    }

    ko.applyBindings(new usersViewModel(), $('#UsersPage').get(0));
});