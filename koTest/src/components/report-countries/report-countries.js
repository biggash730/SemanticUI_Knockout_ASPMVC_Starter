define(['knockout', 'text!./report-countries.html'], function(ko, templateMarkup) {

  function reportCountries(params) {
      var self = this;
      self.data = ko.observableArray([]);
      self.Name = ko.observable('');
      self.ShortCode = ko.observable('');

      $("#filterAccordion").accordion();

      self.print = function () {
          var divToPrint = document.getElementById('reportTable');
          print(divToPrint);
      };

      self.FilterRecords = function () {
          self.getData();
      };

      self.ClearFields = function () {
          self.Name('');
          self.ShortCode('');
      };

      self.getData = function () {
          $.post(serverAddress + 'report/countries', { Name: self.Name(), ShortCode: self.ShortCode() }, function (rData) {
              self.data([]);
              if (rData.Success) {
                  self.data(rData.Data);
              }
          });
      };
      self.getData();
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  reportCountries.prototype.dispose = function() { };
  
  return { viewModel: reportCountries, template: templateMarkup };

});
