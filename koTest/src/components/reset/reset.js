define(['knockout', 'text!./reset.html', "toastr"], function (ko, templateMarkup, toastr) {

  function reset(params) {
      var self = this;
      self.Email = ko.observable();
      self.Validated = ko.observable(false);

      //Validate the form
      $('#ResetForm').form(validationRules, {
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

      self.reset = function () {
          if (self.Validated()) {
              toastr.info('Password Reset initiated', 'Reset');
              $.post(serverAddress + 'users/reset', { Email: self.Email()}, function (rData) {
                  self.Email('');
                  if (rData.Success) {
                      toastr.success(rData.Message, 'Reset');
                      setTimeout(function () { return true; }, 3000);
                      var nl = window.location.href.replace(window.location.hash, '#login');
                      window.location.href = nl;
                  } else toastr.error(rData.Message, 'Reset');
              });
          }
      };
      self.login = function () {
          var nl = window.location.href.replace(window.location.hash, '#login');
          window.location.href = nl;
      };
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  reset.prototype.dispose = function() { };
  
  return { viewModel: reset, template: templateMarkup };

});
