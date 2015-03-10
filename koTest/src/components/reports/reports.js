define(['knockout', 'text!./reports.html'], function(ko, templateMarkup) {

  function reports(params) {
      var self = this;
      self.type = ko.observable();
      self.Title = ko.observable('Countries');
      self.componentName = ko.observable('report-countries');

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
              case "countries":
                  self.componentName('report-countries');
                  break;
          }
          return '';
      };
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  reports.prototype.dispose = function() { };
  
  return { viewModel: reports, template: templateMarkup };

});
