$(document).ready(function () {

    function sendViewModel() {
        var self = this;
        self.Id = ko.observable();
        self.Date = ko.observable('');
        self.SenderName = ko.observable('');
        self.SenderPhoneNumber = ko.observable('');
        self.Amount = ko.observable(0);
        self.Currencies = ko.observableArray([]);
        self.CurrencyId = ko.observable(0);
        self.UniqueCode = ko.observable();
        self.Pin = ko.observable();
        self.RecipientName = ko.observable('');
        self.RecipientPhoneNumber = ko.observable('');
        self.Countries = ko.observableArray([]);
        self.RecipientCountryId = ko.observable(0);
        self.RecipientCountry = ko.observable();
        self.Status = ko.observable();
        self.Notes = ko.observable();
        self.Fees = ko.observableArray([]);
        self.Fee = ko.observable(0);
        self.CurrencySymbol = ko.observable();
        self.RecipientCountryName = ko.observable();
        self.TotalAmount = ko.observable(0);
        self.ExchangeRate = ko.observable(0);
        self.AmountInCedis = ko.observable(0);
        self.Questions = ko.observableArray([]);
        self.QuestionId = ko.observable(0);
        self.Answer = ko.observable('');
        self.Agents = ko.observableArray([]);
        self.SenderCountry = ko.observable(0);

        self.Validated = ko.observable(false);
        self.showAddView = ko.observable(true);
        self.showInfoView = ko.observable(false);
        self.showPrintView = ko.observable(false);

        //Validate the form
        $('#AddTransactionForm').form(validationRules, {
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
        
        self.currencyChanged = function () {
            self.Currencies().forEach(function (currency) {
                if (currency.Id == self.CurrencyId()) {
                    self.ExchangeRate(currency.Rate);
                }
            });
            
            self.Fees().forEach(function (fee) {
                if (+self.Amount() >= fee.MinimumAmount && +self.Amount() <= fee.MaximumAmount) {
                    self.Fee(fee.Fee);
                }
            });
            self.AmountInCedis(accounting.formatMoney(+self.Amount() * self.ExchangeRate(), '₵ '));
            self.TotalAmount(+self.Amount() + self.Fee());
        };

        self.InfoView = function () {
            //alert(self.QuestionId());
            var error = "";
            if (self.SenderName() == "") error = error + "Please enter the sender's name.    ";
            if (self.SenderPhoneNumber() == "") error = error + "Please enter the sender's phone number. ";
            if (self.Date() == "") error = error + "Please select a date. ";
            if (self.Amount() == "") error = error + "Please enter an amount. ";
            if (!self.CurrencyId()) error = error + "Please select the currency. ";
            if (!self.QuestionId()) error = error + "Please select a question. ";
            if (!self.RecipientCountryId()) error = error + "Please select the recipient's country. ";
            if (self.RecipientName() == "") error = error + "Please enter the recipient's name. ";
            if (self.RecipientPhoneNumber() == "") error = error + "Please enter recipient's phone number. ";
            if (self.Answer() == "") error = error + "Please enter an answer. ";
            if (error != "") {
                showNag(error, false);
                return;
            } 


            self.showAddView(false);
            self.showPrintView(false);
            self.showInfoView(true);
        };
        self.closeInfoView = function () {
            self.showInfoView(false);
            self.showPrintView(false);
            self.showAddView(true);
        };
        self.closePrintView = function () {
            self.clearValues();
            self.showInfoView(false);
            self.showPrintView(false);
            self.showAddView(true);
        };

        self.save = function () {
            if (self.Validated()) {
                var model = { Date: self.Date(), SenderName: self.SenderName(), SenderPhoneNumber: self.SenderPhoneNumber(), Amount: self.Amount(), CurrencyId: self.CurrencyId(), RecipientName: self.RecipientName(), RecipientPhoneNumber: self.RecipientPhoneNumber(), RecipientCountryId: self.RecipientCountryId(), Notes: self.Notes(), QuestionId: self.QuestionId(), Answer: self.Answer(), Fee: self.Fee(), Rate: self.ExchangeRate(), Total: self.TotalAmount()  };
                $.post('transactions/insert', model, function (rData) {
                    if (rData.Success) {
                        //todo: print reciept for the sender
                        self.printInfo(rData.Data);
                        self.showInfoView(false);
                        self.showPrintView(true);
                        //self.print();
                        /*self.clearValues();
                        self.closeInfoView();*/
                    }
                    showNag(rData.Message, rData.Success);
                });
            }
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
            self.Answer('');
            self.QuestionId(0);
            self.ExchangeRate(0);
            self.TotalAmount(0);
        };
        
        self.printInfo = function (data) {
            //console.log(data);
            self.Currencies().forEach(function (cu) {
                if (cu.Id == data.CurrencyId) self.CurrencySymbol(cu.Symbol);
            });
            self.UniqueCode(data.UniqueCode);
            self.Pin(data.Pin);
            self.Fee(accounting.formatMoney(data.Fee, self.CurrencySymbol()));
            self.Amount(accounting.formatMoney(data.Amount, self.CurrencySymbol()));
            self.TotalAmount(accounting.formatMoney(data.Amount, self.CurrencySymbol()));
            self.ExchangeRate(data.Rate);
            self.RecipientName(data.RecipientName);
            self.SenderName(data.SenderName);
            self.RecipientCountryName(data.RecipientCountry.Name);
            self.Date(moment(data.Date).format('Do MMMM, YYYY'));
            self.AmountInCedis(accounting.formatMoney((data.Rate * data.Amount), '₵ '));
            self.Agents().forEach(function (ag) {
                console.log(1);
                if (ag.Id == data.CreatedById) {
                    console.log(123);
                    console.log(ag);
                    self.SenderCountry(ag.Branch.City.Country.Name);
                }
            });
            
            /*var divToPrint = document.getElementById('SendPrintOut');
            print(divToPrint);*/
        };
        self.print = function () {
            var divToPrint = document.getElementById('SendPrintOut');
            print(divToPrint);
        };

        $.post('currencies/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Currencies(rData.Data);
        });
        
        $.post('countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Countries(rData.Data);
        });
        $.post('transferfees/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Fees(rData.Data);
        });
        $.post('questions/getall', { Pager: new Pager(0, 0) }, function (rData) {
            if (rData.Success) self.Questions(rData.Data);
        });
        
        $.post('Agents/get', function (rData) {
            if (rData.Success) self.Agents(rData.Data);
        });
    }

    ko.applyBindings(new sendViewModel(), $('#SendPage').get(0));
});