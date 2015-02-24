$(document).ready(function () {

    function recieveViewModel() {
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
        self.QuestionDescription = ko.observable();
        self.QuestionId = ko.observable(0);
        self.Answer = ko.observable();
        self.TotalAmount = ko.observable(0);
        self.ExchangeRate = ko.observable(0);
        self.AmountInCedis = ko.observable(0);
        
        self.IdTypes = ko.observableArray([]);
        self.IdTypeId = ko.observable(0);
        self.IdNumber = ko.observable('');
        self.IdExpiryDate = ko.observable();
        self.PRecipientName = ko.observable();
        self.PPhoneNumber = ko.observable();

        self.Page = ko.observable(0);
        self.Size = ko.observable(20);

        self.showDetailsView = ko.observable(false);
        self.showPaymentView = ko.observable(false);

        

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
            self.QuestionDescription(data.Question.Description);
            self.TotalAmount(data.Total);
            self.ExchangeRate(data.Rate);
            self.AmountInCedis(accounting.formatMoney((data.Rate * data.Amount), '₵ '));
            self.Answer(data.Answer);

            self.showDetailsView(true);
        };
        self.viewPaymentView = function () {
            /*self.Id(data.Id);
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
            self.QuestionDescription(data.Question.Description);
            self.TotalAmount(data.Total);
            self.ExchangeRate(data.Rate);
            self.AmountInCedis(accounting.formatMoney((data.Rate * data.Amount), '₵ '));
            self.Answer(data.Answer);*/

            self.showPaymentView(true);
        };
        self.closeDetailsView = function () {
            self.showDetailsView(false);
            self.showPaymentView(false);
            self.clearValues();
        };

        self.pay = function () {
            var error = "";
            if (self.PRecipientName() == "") error = error + "Please enter the recipient's name.    ";
            if (self.PPhoneNumber() == "") error = error + "Please enter the recipient's phone number. ";
            if (self.IdNumber() == "") error = error + "Please enter the id number. ";
            if (self.IdExpiryDate() == "") error = error + "Please select the expiry date. ";
            if (!self.IdTypeId()) error = error + "Please select the id type. ";
            if (error != "") {
                showNag(error, false);
                return;
            }

            var model = { Id: self.Id(), PRecipientName: self.PRecipientName(), PPhoneNumber: self.PPhoneNumber(), IdTypeId: self.IdTypeId(), IdNumber: self.IdNumber(), IdExpiryDate: self.IdExpiryDate(), AmountInCedis: self.AmountInCedis() };
            $.post('transactions/pay', model, function (rData) {
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
            $.post('transactions/getallCountry?page=' + self.Page() + '&size=' + self.Size(), function (rData) {
                self.data([]);
                if (rData.Success) {
                    self.data(rData.Data);
                }
            });
        };
        
        $.post('idtypes/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.IdTypes(rData.Data);
        });
        
        self.getData();
    }

    ko.applyBindings(new recieveViewModel(), $('#RecievePage').get(0));
});