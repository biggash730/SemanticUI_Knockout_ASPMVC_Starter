define(['knockout', 'text!./users.html', 'jquery', 'moment'], function(ko, templateMarkup, $, moment) {

    function users(params) {
        var self = this;
        if (localStorage.koToken === "") {
            var nl = window.location.href.replace(window.location.hash, '#login');
            window.location.href = nl;
            window.location.reload();
        }

        $.ajaxSetup({
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', localStorage.getItem('koToken'));
            }
        });

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
        self.Size = ko.observable(25);
        var pager = new Pager(self.Page());

        //Validate the form
        $('#AddUserForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function() {
                //do something on failure
            },
            onSuccess: function() {
                //check the validation
                self.Validated(true);
            }
        });

        /*$("#selectRoleDropdown:selected").text({
          onChange: function (val) {
              console.log(val);
          }
      });*/
        self.roleChanged = function() {
            self.IsAgent(false);
            if (self.RoleName() == "Agent") {
                self.IsAgent(true);
            }
        };

        self.showAdd = function() {
            self.FormTitle('Add New User');
            self.showAddView(!self.showAddView());
            self.showViewView(false);
            self.clearValues();
        };
        self.closeForms = function() {
            self.showAddView(false);
            self.showViewView(false);
            self.clearValues();
        };

        self.add = function() {
            if (self.Validated()) {
                var userModel = { Id: self.Id(), FullName: self.FullName(), UserName: self.UserName(), Password: self.Password(), Email: self.Email(), PhoneNumber: self.PhoneNumber(), IsActive: self.IsActive(), Roles: self.RoleName(), DateOfBirth: moment(self.DateOfBirth()).format('YYYY-MM-DD'), IdNumber: self.IdNumber(), BranchId: self.BranchId() };
                if (self.Id() !== '') {
                    $.post(serverAddress + 'users/update', userModel, function (rData) {
                        if (rData.Success) {
                            self.closeForms();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                } else {
                    $.post(serverAddress + 'users/register', userModel, function (rData) {
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
        };

        self.showView = function(data) {
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
            self.showAddView(false);
            self.showViewView(!self.showViewView());
        };

        self.showEdit = function(data) {
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

            $('#username').attr("disabled", "disabled");
            $('#password').attr("disabled", "disabled");

            self.FormTitle('Update User Details');
            self.showAddView(!self.showAddView());
            self.showViewView(false);
        };

        self.showDeactivate = function(data) {
            //set the values Here
            self.Id(data.Id);
            $('#UserDeactivateModal').modal('show');
        };

        self.deactivate = function() {
            $.post(serverAddress + 'users/deactivate?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                }
            });
        };

        self.clearValues = function() {
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
            $('#username').removeAttr("disabled");
            $('#password').removeAttr("disabled");
        };

        self.nextPage = function() {
            if (self.data().length >= self.Size()) {
                self.Page(self.Page() + 1);
                self.getData();
            }
        };

        self.prevPage = function() {
            if (self.Page() > 1) {
                self.Page(self.Page() - 1);
                self.getData();
            }
        };

        self.getData = function() {
            $.post(serverAddress + 'users/getall?page=' + self.Page() + '&size=' + self.Size(), function(rData) {
                self.data([]);
                if (rData.Success) self.data(rData.Data);
                //showNag(rData.Message, rData.Success);
            });
        };

        $.post(serverAddress + 'roles/getall', { Pager: new Pager(0, allSize) }, function(rData) {
            if (rData.Success) self.RolesData(rData.Data);
        });

        self.getData();
    }

    // This runs when the component is torn down. Put here any logic necessary to clean up,
    // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
    users.prototype.dispose = function() {};

    return { viewModel: users, template: templateMarkup };

});
