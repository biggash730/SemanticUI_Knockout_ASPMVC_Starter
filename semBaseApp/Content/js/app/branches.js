$(document).ready(function () {

    function branchesViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Id = ko.observable();
        self.Name = ko.observable();
        self.Description = ko.observable('');
        self.Telephone = ko.observable();
        self.Email = ko.observable();
        self.PostalAddress = ko.observable();
        self.ResidentialAddress = ko.observable();
        self.Cities = ko.observableArray([]);
        self.CityId = ko.observable();
        self.CityName = ko.observable();
        self.Validated = ko.observable(false);
        self.showAddView = ko.observable(false);
        self.showViewView = ko.observable(false);
        self.FormTitle = ko.observable();
        self.Page = ko.observable(0);
        self.Size = ko.observable(20);
        var pager = new Pager(self.Page());

        var typingTimer;

        //Validate the form
        $('#AddBranchForm').form(validationRules, {
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

        self.showAdd = function () {
            self.FormTitle('Add New Branch');
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
            if (self.Validated()) {
                var model = { Id: self.Id(), Name: self.Name(), Description: self.Description(), Telephone: self.Telephone(), Email: self.Email(), ResidentialAddress: self.ResidentialAddress(), PostalAddress: self.PostalAddress(), CityId: self.CityId()};
                if (self.Id() > 0) {
                    $.post('branches/update', model, function (rData) {
                        if (rData.Success) {
                            self.closeForms();
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                } else {
                    $.post('branches/insert', model, function (rData) {
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

        self.showView = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.Email(data.Email);
            self.Telephone(data.Telephone);
            self.PostalAddress(data.PostalAddress);
            self.ResidentialAddress(data.ResidentialAddress);
            self.CityId(data.CityId);
            self.CityName(data.City.Name);

            self.showAddView(false);
            self.showViewView(!self.showViewView());
        };

        self.showEdit = function (data) {
            //set the values Here
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.Email(data.Email);
            self.Telephone(data.Telephone);
            self.PostalAddress(data.PostalAddress);
            self.ResidentialAddress(data.ResidentialAddress);
            self.CityId(data.CityId);
            self.CityName(data.City.Name);

            self.FormTitle('Update Branch');
            self.showAddView(!self.showAddView());
            self.showViewView(false);
        };

        self.showDelete = function (data) {
            //set the values Here
            self.Id(data.Id);
            $('#BranchDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('branches/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                }
            });
        };

        self.clearValues = function () {
            self.Id(0);
            self.Name('');
            self.Description('');
            self.Email('');
            self.Telephone('');
            self.PostalAddress('');
            self.ResidentialAddress('');
            self.CityId(0);
            self.CityName('');
        };

        self.getData = function () {
            $.post('branches/getall?page=' + 0 + '&size=' + 0, function (rData) {
                self.data([]);
                if (rData.Success) self.data(rData.Data);
                showNag(rData.Message, rData.Success);
            });
        };

        pager.Size = 0;
        $.post('cities/getall', pager, function (rData) {
            if (rData.Success) self.Cities(rData.Data);
        });

        self.getData();
    }

    ko.applyBindings(new branchesViewModel(), $('#BranchesPage').get(0));
});