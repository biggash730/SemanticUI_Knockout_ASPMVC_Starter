$(document).ready(function () {

    function cancelViewModel() {
        var self = this;
        self.data = ko.observableArray([]);
        self.Id = ko.observable();
        self.Date = ko.observable();
        self.SenderName = ko.observable();
        self.SenderPhoneNumber = ko.observable();
        self.Amount = ko.observable();
        self.Currencies = ko.observableArray([]);
        self.CurrencyId = ko.observable();
        self.UniqueCode = ko.observable();
        self.Pin = ko.observable();
        self.RecipientName = ko.observable();
        self.RecipientPhoneNumber = ko.observable();
        self.Countries = ko.observableArray([]);
        self.RecipientCountryId = ko.observable();
        self.Status = ko.observable();
        self.Notes = ko.observable();
        self.Fee = ko.observable();
        self.CurrencySymbol = ko.observable();
        self.RecipientCountryName = ko.observable();
        self.Page = ko.observable(0);
        self.Size = ko.observable(20);

        self.showDetailsView = ko.observable(false);

        

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

        self.cancel = function () {
            var model = { Id: self.Id() };
            $.post('transactions/cancel', model, function (rData) {
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
        
        /*self.printInfo = function (data) {
            console.log(data);
            self.Currencies().forEach(function (cu) {
                if (cu.Id == data.CurrencyId) self.CurrencySymbol(cu.Symbol);
            });
            self.UniqueCode(data.UniqueCode);
            self.Pin(data.Pin);
            self.Fee(self.CurrencySymbol() + ' ' + data.Fees);
            self.Amount(self.CurrencySymbol() + ' ' + data.Amount);
            self.Countries().forEach(function (cu) {
                if (cu.Id == data.CountryId) self.RecipientCountryName(cu.Name);
            });

            self.print = function () {
                var divToPrint = document.getElementById('SendPrintOut');
                print(divToPrint);
            };
        };*/

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
            $.post('transactions/getallpersonal?page=' + self.Page() + '&size=' + self.Size(), function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };
        
        self.getData();
    }

    ko.applyBindings(new cancelViewModel(), $('#CancelPage').get(0));
});