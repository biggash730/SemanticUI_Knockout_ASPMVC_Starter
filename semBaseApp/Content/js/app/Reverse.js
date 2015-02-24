$(document).ready(function () {

    function reverseViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Id = ko.observable();
        self.Date = ko.observable();
        self.SenderName = ko.observable();
        self.SenderPhoneNumber = ko.observable();
        self.Amount = ko.observable();
        self.Currencies = ko.observableArray([]);
        self.CurrencyId = ko.observable();
        
        self.Pin = ko.observable();
        self.RecipientName = ko.observable();
        self.RecipientPhoneNumber = ko.observable();
        self.Countries = ko.observableArray([]);
        self.RecipientCountryId = ko.observable();
        
        self.Notes = ko.observable();
        self.Fee = ko.observable();
        self.CurrencySymbol = ko.observable();
        self.RecipientCountryName = ko.observable();
        self.Page = ko.observable(0);
        self.Size = ko.observable(20);
        
        self.DateFrom = ko.observable(moment().subtract(100, 'y').format("DD-MM-YYYY"));
        self.DateTo = ko.observable(moment().add(100, 'y').format("DD-MM-YYYY"));
        self.AmountFrom = ko.observable(0);
        self.AmountTo = ko.observable(0);
        self.Agents = ko.observableArray([]);
        self.AgentId = ko.observable('');
        self.UniqueCode = ko.observable();
        self.Statuses = ko.observableArray([{ Id: 1, Name: "Recieved" }, { Id: 2, Name: "Cancelled" }]);
        self.Status = ko.observable();

        self.showDetailsView = ko.observable(false);
        $("#reverseFilterAccordion").accordion();

        self.viewDetails = function (data) {
            self.Id(data.Id);
            self.Date(moment(data.Date).format('Do MMMM, YYYY'));
            self.SenderName(data.SenderName);
            self.SenderPhoneNumber(data.SenderPhoneNumber);
            self.CurrencyId(data.CurrencyId);
            self.UniqueCode(data.UniqueCode);
            self.Pin(data.Pin);
            self.RecipientName(data.RecipientName);
            self.RecipientPhoneNumber(data.RecipientPhoneNumber);
            self.RecipientCountryId(data.RecipientCountryId);
            self.Status(data.Status);
            self.Notes(data.Notes);
            self.CurrencySymbol(data.Currency.Symbol);
            self.RecipientCountryName(data.RecipientCountry.Name);
            self.Fee(accounting.formatMoney(data.Fee, data.Currency.Symbol));
            self.Amount(accounting.formatMoney(data.Amount, data.Currency.Symbol));

            self.showDetailsView(true);
        };
        self.closeDetailsView = function () {
            self.showDetailsView(false);
            self.clearValues();
        };
        
        self.FilterRecords = function () {
            self.getData();
        };

        self.ClearFilters = function () {
            self.AgentId('');
            self.UniqueCode('');
            self.Status('');
            self.DateFrom(moment().subtract(100, 'y').format("DD-MM-YYYY"));
            self.DateTo(moment().add(100, 'y').format("DD-MM-YYYY"));
            self.AmountFrom(0);
            self.AmountTo(0);
        };

        self.reverse = function () {
            var model = { Id: self.Id() };
            $.post('transactions/reverse', model, function (rData) {
                if (rData.Success) {
                    //todo: print reciept for the sender
                    //self.printInfo(rData.Data);
                    self.getData();
                    self.closeDetailsView();
                } else {
                    showNag(rData.Message, rData.Success);
                }
            });
        };

        self.clearValues = function () {
            self.Id(0);
            self.Date('');
            self.SenderName('');
            self.SenderPhoneNumber('');
            self.Amount('');
            self.CurrencyId(0);
            self.UniqueCode('');
            self.Pin('');
            self.RecipientName('');
            self.RecipientPhoneNumber('');
            self.RecipientCountryId(0);
            self.Status('');
            self.Notes('');
            self.Fee('');
            self.CurrencySymbol('');
            self.RecipientCountryName('');
        };
        
        self.nextPage = function () {
            if (self.data().length >= self.Size()) {
                self.Page(self.Page() + 1);
                self.getData();
            }
        };

        self.prevPage = function () {
            if (self.Page() > 0) {
                self.Page(self.Page() - 1);
                self.getData();
            }
        };
        
        self.getData = function () {
            var model = { Page: self.Page(), Size: self.Size(), AgentId: self.AgentId(), UniqueCode: self.UniqueCode(), Status: self.Status(), DateFrom: self.DateFrom(), DateTo: self.DateTo(), AmountFrom: self.AmountFrom(), AmountTo: self.AmountTo() };
            $.post('transactions/GetPaidAndCancelled',model, function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };
        
        $.post('Agents/get', function (rData) {
            if (rData.Success) self.Agents(rData.Data);
        });
        
        self.getData();
    }

    ko.applyBindings(new reverseViewModel(), $('#ReversePage').get(0));
});