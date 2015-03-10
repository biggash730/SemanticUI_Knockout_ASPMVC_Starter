define(['knockout', 'text!./settings.html', 'jquery'], function (ko, templateMarkup, $) {

  function settings(params) {
      var self = this;
      self.type = ko.observable();
      self.componentName = ko.observable('countries');

      $("#selectModelDropdown").dropdown({
          onChange: function (val) {
              self.type(val);
              self.componentToUse();
          }
      });

      self.componentToUse = function () {
          switch (self.type()) {
              case "countries":
                  self.componentName('countries');
                  break;
          }
          return '';
      };
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  settings.prototype.dispose = function() { };
  
  return { viewModel: settings, template: templateMarkup };

});
