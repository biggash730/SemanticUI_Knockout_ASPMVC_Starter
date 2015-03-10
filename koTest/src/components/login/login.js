define(['knockout', 'text!./login.html', "toastr"], function(ko, templateMarkup, toastr) {

  function login(params) {
      var self = this;
      self.Username = ko.observable();
      self.Password = ko.observable();
      self.RememberMe = ko.observable(true);
      self.Validated = ko.observable(false);

      //Validate the form
      $('#LoginForm').form(validationRules, {
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

      self.login = function () {
          if (self.Validated()) {
              toastr.info('Login initiated', 'Login');
              $.post(serverAddress + 'users/login', { UserName: self.Username(), Password: self.Password(), RememberMe: self.RememberMe() }, function (rData) {
                  if (rData.Success) {
                      toastr.success("Login Successful",'Login');
                      localStorage.koToken = "Bearer " + rData.Message;
                      setTimeout(function () { return true; }, 3000);
                      var nl = window.location.href.replace(window.location.hash, '#');
                      window.location.href = nl;
                      window.location.reload();
                  } else {
                      toastr.error('Please check the login details', 'Login');
                      //self.clearValues();
                  }

              });
          }
      };

      self.clearValues = function () {
          self.Username('');
          self.Password('');
          self.RememberMe(true);
      };
      self.reset = function () {
          var nl = window.location.href.replace(window.location.hash, '#reset');
          window.location.href = nl;
      };
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  login.prototype.dispose = function() { };
  
  return { viewModel: login, template: templateMarkup };

});
