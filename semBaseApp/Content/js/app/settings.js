$(document).ready(function () {

    function settingsViewModel() {
        var self = this;
        self.type = ko.observable();
        self.componentName = ko.observable('fees-component');

        $("#selectModelDropdown").dropdown({
            onChange: function (val) {
                self.type(val);
                //console.log(val);
                self.componentToUse();
            }
        });

        self.componentToUse = function () {
            switch (self.type()) {
                case "countries":
                    self.componentName('countries-component');
                    break;
                case "currencies":
                    self.componentName('currencies-component');
                    break;
                case "fees":
                    self.componentName('fees-component');
                    break;
                case "roles":
                    self.componentName('roles-component');
                    break;
                case "cities":
                    self.componentName('cities-component');
                    break;
                case "questions":
                    self.componentName('questions-component');
                    break;
                case "identification types":
                    self.componentName('idtypes-component');
                    break;
            }
            return '';
        };
    }

    function countriesViewModel() {
        var self = this;
        self.Id = ko.observable();
        self.data = ko.observableArray([]);
        self.Name = ko.observable();
        self.Description = ko.observable();
        self.ShortCode = ko.observable();
        self.CurrencyId = ko.observable(0);
        self.Currencies = ko.observableArray([]);
        self.Validated = ko.observable(false);
        
        //Validate the form
        $('#addCountryForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function() {
                $('#CountryFormModal').modal('show');
            },
            onSuccess: function() {
                //check the validation
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#CountryFormModal').modal('show');
            //$('#CountryFormModal').transition('shake');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('countries/update', { Id: self.Id(), Name: self.Name(), Description: self.Description(), ShortCode: self.ShortCode(), CurrencyId: self.CurrencyId() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('countries/insert', { Name: self.Name(), Description: self.Description(), ShortCode: self.ShortCode(), CurrencyId: self.CurrencyId() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.ShortCode(data.ShortCode);
            self.CurrencyId(data.CurrencyId);
            $('#CountryFormModal').modal('show');
        };
        
        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.ShortCode(data.ShortCode);
            self.CurrencyId(data.CurrencyId);
            $('#CountryDeleteModal').modal('show');
        };
        
        self.del = function () {
            $.post('countries/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });
            
            
        };

        self.clearValues = function () {
            self.Id(0);
            self.Name('');
            self.Description('');
            self.ShortCode('');
            self.CurrencyId(0);
        };
        
        self.getData = function () {
            self.data([]);
            $.post('countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };

        $.post('currencies/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Currencies(rData.Data);
        });
        self.getData();
    }
    
    function citiesViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Name = ko.observable();
        self.Description = ko.observable();
        self.Id = ko.observable();
        self.Country = ko.observable();
        self.CountryId = ko.observable();
        self.Countries = ko.observableArray([]);
        self.Validated = ko.observable(false);

        //Validate the form
        $('#addCityForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                $('#CityFormModal').modal('show');
            },
            onSuccess: function () {
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#CityFormModal').modal('show');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('cities/update', { Id: self.Id(), Name: self.Name(), Description: self.Description(), CountryId: self.CountryId() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('cities/insert', { Name: self.Name(), Description: self.Description(), CountryId: self.CountryId() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.CountryId(data.CountryId);
            self.Country(data.Country);
            $('#CityFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.CountryId(data.CountryId);
            self.Country(data.Country);
            $('#CityDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('cities/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });
        };

        self.clearValues = function () {
            self.Id(0);
            self.Name('');
            self.Description('');
            self.CountryId(0);
            self.Country('');
        };

        self.getData = function () {
            self.data([]);
            $.post('cities/getall', { Pager: new Pager(1, allSize) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };

        $.post('countries/getall', { Pager: new Pager(1, allSize) }, function (rData) {
            if (rData.Success) self.Countries(rData.Data);
        });

        self.getData();
    }

    function currenciesViewModel() {
        var self = this;
        self.Id = ko.observable();
        self.data = ko.observableArray([]);
        self.Name = ko.observable();
        self.Description = ko.observable();
        self.Symbol = ko.observable();
        self.Rate = ko.observable();
        self.CurrencySigns = ko.observableArray([{ Id: 0, Sign: "¢" }, { Id: 1, Sign: "$" }, { Id: 2, Sign: "R$" }, { Id: 3, Sign: "¥" }, { Id: 4, Sign: "kr" }, { Id: 5, Sign: "RD$" }, { Id: 6, Sign: "£" }, { Id: 7, Sign: "€" }, { Id: 9, Sign: "₱" }, { Id: 8, Sign: "S" }, { Id: 9, Sign: "CHF" }, { Id: 10, Sign: "₦" }, { Id: 11, Sign: "Kč" }]);
        self.Validated = ko.observable(false);
        
        //Validate the form
        $('#addCurrencyForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function() {
                $('#CurrencyFormModal').modal('show');
            },
            onSuccess: function() {
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#CurrencyFormModal').modal('show');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('currencies/update', { Id: self.Id(), Name: self.Name(), Description: self.Description(), Symbol: self.Symbol(), Rate: self.Rate() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('currencies/insert', { Name: self.Name(), Description: self.Description(), Symbol: self.Symbol(), Rate: self.Rate() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.Symbol(data.Symbol);
            self.Rate(data.Rate);
            $('#CurrencyFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            self.Symbol(data.Symbol);
            self.Rate(data.Rate);
            $('#CurrencyDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('currencies/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });
        };

        self.clearValues = function () {
            self.Id(0);
            self.Name('');
            self.Description('');
            self.Symbol('');
            self.Rate(0);
        };

        self.getData = function () {
            self.data([]);
            $.post('currencies/getall', { Pager: new Pager(0, 0) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };

        self.getData();
    }
    
    function feesViewModel() {
        var self = this;
        self.Id = ko.observable();
        self.data = ko.observableArray([]);
        self.MaximumAmount = ko.observable();
        self.MinimumAmount = ko.observable();
        self.Fee = ko.observable();
        self.Validated = ko.observable(false);

        //Validate the form
        $('#addFeeForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function() {
                $('#FeeFormModal').modal('show');
            },
            onSuccess: function() {
                self.Validated(true);
            }
        });
        
        self.showAdd = function () {
            $('#FeeFormModal').modal('show');
            //clear all old data
            self.clearValues();
        };

        self.add = function() {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('transferfees/update', { Id: self.Id(), MaximumAmount: self.MaximumAmount(), MinimumAmount: self.MinimumAmount(), Fee: self.Fee() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                } else {
                    $.post('transferfees/insert', { MaximumAmount: self.MaximumAmount(), MinimumAmount: self.MinimumAmount(), Fee: self.Fee() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.MaximumAmount(data.MaximumAmount);
            self.MinimumAmount(data.MinimumAmount);
            self.Fee(data.Fee);
            $('#FeeFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.MaximumAmount(data.MaximumAmount);
            self.MinimumAmount(data.MinimumAmount);
            self.Fee(data.Fee);
            $('#FeeDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('transferfees/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });
        };

        self.clearValues = function () {
            self.Id(0);
            self.MaximumAmount(0);
            self.MinimumAmount(0);
            self.Fee(0);
        };

        self.getData = function () {
            self.data([]);
            $.post('transferfees/getall', { Pager: new Pager(1, allSize) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };
        self.getData();
    }
    
    function rolesViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Name = ko.observable();
        self.Id = ko.observable();
        self.Validated = ko.observable(false);

        //Validate the form
        $('#addRoleForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                $('#RoleFormModal').modal('show');
            },
            onSuccess: function () {
                //check the validation
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#RoleFormModal').modal('show');
            //$('#CountryFormModal').transition('shake');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('roles/update', { Id: self.Id(), Name: self.Name()}, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('roles/insert', { Name: self.Name()}, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            $('#RoleFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            $('#RoleDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('roles/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });


        };

        self.clearValues = function () {
            self.Id('');
            self.Name('');
        };

        self.getData = function () {
            self.data([]);
            $.post('roles/getall', { Pager: new Pager(1, allSize) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
                else notifyMe(false, rData.Message);
            });
        };

        self.getData();
    }

    function questionsViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Description = ko.observable();
        self.Id = ko.observable();
        self.Validated = ko.observable(false);

        //Validate the form
        $('#addQuestionForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                $('#QuestionFormModal').modal('show');
            },
            onSuccess: function () {
                //check the validation
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#QuestionFormModal').modal('show');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('questions/update', { Id: self.Id(), Description: self.Description() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                } else {
                    $.post('questions/insert', { Description: self.Description() }, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        }
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Description(data.Description);
            $('#QuestionFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Description(data.Description);
            $('#QuestionDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('questions/delete?id=' + self.Id(), function (rData) {
                if (rData.Success) {
                    //clear form 
                    self.clearValues();
                    //get all data
                    self.getData();
                } //notifyMe(false, rData.Message);
            });


        };

        self.clearValues = function () {
            self.Id(0);
            self.Description('');
        };

        self.getData = function () {
            self.data([]);
            $.post('questions/getall', { Pager: new Pager(0, 0) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };

        self.getData();
    }
    
    function idtypesViewModel() {
        var self = this;
        self.Id = ko.observable();
        self.data = ko.observableArray([]);
        self.Name = ko.observable();
        self.Description = ko.observable();
        self.Validated = ko.observable(false);

        //Validate the form
        $('#addIdTypeForm').form(validationRules, {
            inline: true,
            on: 'blur',
            onFailure: function () {
                $('#IdTypeFormModal').modal('show');
            },
            onSuccess: function () {
                //check the validation
                self.Validated(true);
            }
        });

        self.showAdd = function () {
            $('#IdTypeFormModal').modal('show');
            //clear all old data
            self.clearValues();
        };

        self.add = function () {
            if (self.Validated()) {
                if (self.Id() > 0) {
                    $.post('idtypes/update', { Id: self.Id(), Name: self.Name(), Description: self.Description()}, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                } else {
                    $.post('idtypes/insert', { Name: self.Name(), Description: self.Description()}, function (rData) {
                        if (rData.Success) {
                            //clear form 
                            self.clearValues();
                            //get all data
                            self.getData();
                        } //notifyMe(false, rData.Message);
                    });
                }
            }
        };

        self.showEdit = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            $('#IdTypeFormModal').modal('show');
        };

        self.showDelete = function (data) {
            self.Id(data.Id);
            self.Name(data.Name);
            self.Description(data.Description);
            $('#IdTypeDeleteModal').modal('show');
        };

        self.del = function () {
            $.post('idtypes/delete?id=' + self.Id(), function (rData) {
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
        };

        self.getData = function () {
            self.data([]);
            $.post('idtypes/getall', { Pager: new Pager(0, 0) }, function (rData) {
                if (rData.Success) self.data(rData.Data);
            });
        };
        self.getData();
    }

    ko.components.register('countries-component', { viewModel: countriesViewModel, template: { element: 'countries-template' } });
    ko.components.register('currencies-component', { viewModel: currenciesViewModel, template: { element: 'currencies-template' } });
    ko.components.register('fees-component', { viewModel: feesViewModel, template: { element: 'fees-template' } });
    ko.components.register('cities-component', { viewModel: citiesViewModel, template: { element: 'cities-template' } });
    ko.components.register('roles-component', { viewModel: rolesViewModel, template: { element: 'roles-template' } });
    ko.components.register('questions-component', { viewModel: questionsViewModel, template: { element: 'questions-template' } });
    ko.components.register('idtypes-component', { viewModel: idtypesViewModel, template: { element: 'idtypes-template' } });
    ko.applyBindings(new settingsViewModel(), $('#SettingsPage').get(0));
});