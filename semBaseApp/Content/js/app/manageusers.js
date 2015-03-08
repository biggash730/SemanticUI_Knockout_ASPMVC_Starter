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
        self.showProgress = ko.observable(false);

        //Validate the form
        /*$('#AddUserForm').form(validationRules, {
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
        
        /*$("#selectRoleDropdown:selected").text({
            onChange: function (val) {
                console.log(val);
            }
        });*/
        $('.modal-trigger').leanModal({
            dismissible: false, // Modal can be dismissed by clicking outside of the modal
            opacity: .5 // Opacity of modal background
        });
        // $('#dob').datepicker();
        $('select').material_select();
        $('.tooltipped').tooltip({ delay: 50 });

        self.roleChanged = function () {
            self.IsAgent(false);
            if (self.RoleName() == "Agent") {
                self.IsAgent(true);
            }
        };

        self.showAdd = function () {
            self.FormTitle('New User');
            $('#addModal').openModal();
            self.clearValues();
        };
        self.closeForms = function () {
            self.clearValues();
        };

        self.add = function () {
            self.showProgress(true);
            if (self.Validated()) {
                //var role = self.IsAdministrator() ? administratorString : userString;
                var userModel = { Id: self.Id(), FullName: self.FullName(), UserName: self.UserName(), Password: self.Password(), Email: self.Email(), PhoneNumber: self.PhoneNumber(), IsActive: self.IsActive(), Roles: self.RoleName(), DateOfBirth: moment(self.DateOfBirth()).format('YYYY-MM-DD'), IdNumber: self.IdNumber(), BranchId: self.BranchId() };
                
                if (self.Id() != '') {
                    $.post('users/update', userModel, function (rData) {
                        toast(rData.Message, 5000, 'rounded');
                        self.showProgress(false);
                        if (rData.Success) {
                            //self.closeForms();
                            $('#addModal').closeModal();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                } else {
                    $.post('users/register', userModel, function (rData) {
                        toast(rData.Message, 5000, 'rounded');
                        self.showProgress(false);
                        if (rData.Success) {
                            //self.closeForms();
                            $('#addModal').closeModal();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                }
            }
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
            self.FormTitle('User Details');
            $('#viewModal').openModal();
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

            $('#username').attr("disabled", "disabled");
            $('#password').attr("disabled", "disabled");

            self.FormTitle('Update User Details');
            $('#addModal').openModal();       
            
        };

        self.showDeactivate = function (data) {
            //set the values Here
            self.Id(data.Id);
            //$('#UserDeactivateModal').modal('show');
            toast('<span>Activate/Deactivate User Account. Are you sure?</span><a class=&quot;btn-flat yellow-text&quot; data-bind=&quot;click: deactivate&quot;> Yes<a>', 5000)
        };

        self.deactivate = function () {
            toast('deactivate user', 3000);
            /*$.post('users/deactivate?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                }
            });*/
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
            self.showProgress(true);
            $.post('users/getall?page=' + self.Page() + '&size=' + self.Size(), function (rData) {
                self.data([]);
                toast(rData.Message, 1000, 'rounded');
                self.showProgress(false);
                if (rData.Success) self.data(rData.Data);
                
            });
        };
        
        $.post('roles/getall', { Pager: new Pager(0, allSize) }, function (rData) {
            if (rData.Success) self.RolesData(rData.Data);
        });

        self.getData();
    }

    ko.applyBindings(new usersViewModel(), $('#UsersPage').get(0));
});