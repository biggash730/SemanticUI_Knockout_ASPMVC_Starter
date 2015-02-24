$(document).ready(function () {

    function reportsViewModel() {
        var self = this;
        self.type = ko.observable();
        self.Title = ko.observable('Transfer Fees');
        self.componentName = ko.observable('allTransactions-component');

        $("#reportDropdown").dropdown({
            onChange: function (val) {
                self.type(val);
                //console.log(self.type());
                self.Title(val.toUpperCase());
                self.componentToUse();
            }
        });

        self.componentToUse = function () {
            switch (self.type()) {
                case "all transactions":
                    self.componentName('allTransactions-component');
                    break;
                case "cancelled transactions":
                    self.componentName('cancelledTransactions-component');
                    break;
                case "agent transactions summary":
                    self.componentName('agentsSummary-component');
                    break;
                case "transfer fees":
                    self.componentName('fees-component');
                    break;
                case "branches":
                    self.componentName('branches-component');
                    break;
            }
            return '';
        };
    }

    function allTransactionsViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.DateFrom = ko.observable(moment().subtract(100, 'y').format("DD-MM-YYYY"));
        self.DateTo = ko.observable(moment().add(100, 'y').format("DD-MM-YYYY"));
        self.AmountFrom = ko.observable(0);
        self.AmountTo = ko.observable(0);
        self.Name = ko.observable('');
        self.UniqueCode = ko.observable('');
        self.SenderCountryId = ko.observable(0);
        self.RecipientCountryId = ko.observable(0);
        self.Countries = ko.observableArray([]);
        self.Status = ko.observable('');
        self.CurrencyId = ko.observable(0);
        self.Currencies = ko.observableArray([]);
        self.Statuses = ko.observableArray([{Id:1, Name:"Pending"},{Id:2, Name:"Recieved"}]);

        
        $("#allTransactionsFilterAccordion").accordion();

        self.print = function () {
            var divToPrint = document.getElementById('AllTransactionsReportTable');
            print(divToPrint);
        };
        
        self.FilterRecords = function () {
            self.getData();
        };
        
        self.ClearFields = function () {
            self.DateFrom(moment().subtract(100, 'y').format("DD-MM-YYYY"));
            self.DateTo(moment().add(100, 'y').format("DD-MM-YYYY"));
            self.AmountFrom(0);
            self.AmountTo(0);
            self.Name('');
            self.UniqueCode('');
            self.SenderCountryId(0);
            self.RecipientCountryId(0);
            self.Status('');
            self.CurrencyId(0);
        };

        self.getData = function () {
            $.post('alltransactions/report', { DateFrom: self.DateFrom(), DateTo: self.DateTo(), AmountFrom: self.AmountFrom(), AmountTo: self.AmountTo(), Name: self.Name(), UniqueCode: self.UniqueCode(), SenderCountryId: self.SenderCountryId(), RecipientCountryId: self.RecipientCountryId(), Status: self.Status(), CurrencyId: self.CurrencyId() }, function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };
        
        $.post('countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Countries(rData.Data);
        });
        $.post('currencies/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Currencies(rData.Data);
        });

        self.getData();
    }

    function cancelledTransactionsViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.DateFrom = ko.observable(moment().subtract(100, 'y').format("DD-MM-YYYY"));
        self.DateTo = ko.observable(moment().add(100, 'y').format("DD-MM-YYYY"));
        self.AmountFrom = ko.observable(0);
        self.AmountTo = ko.observable(0);
        self.Name = ko.observable('');
        self.UniqueCode = ko.observable('');
        self.SenderCountryId = ko.observable(0);
        self.RecipientCountryId = ko.observable(0);
        self.Countries = ko.observableArray([]);
        self.CurrencyId = ko.observable(0);
        self.Currencies = ko.observableArray([]);


        $("#cancelledTransactionsFilterAccordion").accordion();

        self.print = function () {
            var divToPrint = document.getElementById('CancelledTransactionsReportTable');
            print(divToPrint);
        };

        self.FilterRecords = function () {
            self.getData();
        };

        self.ClearFields = function () {
            self.DateFrom(moment().subtract(100, 'y').format("DD-MM-YYYY"));
            self.DateTo(moment().add(100, 'y').format("DD-MM-YYYY"));
            self.AmountFrom(0);
            self.AmountTo(0);
            self.Name('');
            self.UniqueCode('');
            self.SenderCountryId(0);
            self.RecipientCountryId(0);
            self.CurrencyId(0);
        };

        self.getData = function () {
            $.post('cancelledtransactions/report', { DateFrom: self.DateFrom(), DateTo: self.DateTo(), AmountFrom: self.AmountFrom(), AmountTo: self.AmountTo(), Name: self.Name(), UniqueCode: self.UniqueCode(), SenderCountryId: self.SenderCountryId(), RecipientCountryId: self.RecipientCountryId(), CurrencyId: self.CurrencyId() }, function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };

        $.post('countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Countries(rData.Data);
        });
        $.post('currencies/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Currencies(rData.Data);
        });

        self.getData();
    }

    function agentsSummaryViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Branches = ko.observableArray([]);
        self.BranchId = ko.observable(0);
        self.Agents = ko.observableArray([]);
        self.AgentId = ko.observable(0);
        self.DateFrom = ko.observable(moment().subtract(100, 'y').format("DD-MM-YYYY"));
        self.DateTo = ko.observable(moment().add(100, 'y').format("DD-MM-YYYY"));

        $("#agentsSummaryFilterAccordion").accordion();

        self.print = function () {
            var divToPrint = document.getElementById('AgentsSummaryReportTable');
            print(divToPrint);
        };

        self.FilterRecords = function () {
            self.getData();
        };

        self.ClearFields = function () {
            self.ClientId(0);
            self.Title('');
            self.Status('');
            self.DateFrom(moment().subtract(100, 'y').format("DD-MM-YYYY"));
            self.DateTo(moment().add(100, 'y').format("DD-MM-YYYY"));
        };

        self.getData = function () {
            $.post('AgentsSummaries/report', { BranchId: self.BranchId(), AgentId: self.AgentId(), DateFrom: self.DateFrom(), DateTo: self.DateTo() }, function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };

        $.post('Agents/get', function (rData) {
            if (rData.Success) self.Agents(rData.Data);
        });
        $.post('branches/getall?page=0&size=0', function (rData) {
            if (rData.Success) self.Branches(rData.Data);
        });

        self.getData();
    }
    
    function feesViewModel() {
        var self = this;
        self.data = ko.observableArray([]);

        self.print = function () {
            var divToPrint = document.getElementById('FeesReportTable');
            print(divToPrint);
        };


        self.getData = function () {
            $.post('fees/report', function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };
        self.getData();
    }
    
    function branchesViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Name = ko.observable('');
        self.Email = ko.observable('');
        self.CountryId = ko.observable(0);
        self.Countries = ko.observableArray([]);
        self.CityId = ko.observable(0);
        self.Cities = ko.observableArray([]);

        $("#branchesFilterAccordion").accordion();

        self.print = function () {
            var divToPrint = document.getElementById('BranchesReportTable');
            print(divToPrint);
        };

        self.FilterRecords = function () {
            self.getData();
        };

        self.ClearFields = function () {
            self.Name('');
            self.Email('');
            self.CountryId(0);
            self.CityId(0);
        };

        self.getData = function () {
            $.post('branches/report', { Name: self.Name(), Email: self.Email(), CountryId: self.CountryId(), CityId: self.CityId()}, function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };

        $.post('countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Countries(rData.Data);
        });
        $.post('cities/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Cities(rData.Data);
        });
        self.getData();
    }

    ko.components.register('allTransactions-component', { viewModel: allTransactionsViewModel, template: { element: 'allTransactions-template' } });
    ko.components.register('cancelledTransactions-component', { viewModel: cancelledTransactionsViewModel, template: { element: 'cancelledTransactions-template' } });
    ko.components.register('agentsSummary-component', { viewModel: agentsSummaryViewModel, template: { element: 'agentsSummary-template' } });
    ko.components.register('fees-component', { viewModel: feesViewModel, template: { element: 'fees-template' } });
    ko.components.register('branches-component', { viewModel: branchesViewModel, template: { element: 'branches-template' } });
    ko.applyBindings(new reportsViewModel(), $('#ReportsPage').get(0));
});