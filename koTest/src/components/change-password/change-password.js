define(['knockout', 'text!./change-password.html','jquery', 'toastr', 'moment'], function(ko, templateMarkup, $, toastr, moment) {

  function changePassword(params) {
      var self = this;

      $.ajaxSetup({
          beforeSend: function (xhr) {
              xhr.setRequestHeader('Authorization', localStorage.getItem('koToken'));
          }
      });

      self.Id = ko.observable();
      self.FullName = ko.observable();
      self.UserName = ko.observable();
      self.Password = ko.observable();
      self.DateOfBirth = ko.observable();
      self.PhoneNumber = ko.observable();
      self.Email = ko.observable();
      self.IsActive = ko.observable(true);
      self.IsAdministrator = ko.observable(false);
      self.Roles = ko.observable();
      self.Validated = ko.observable(false);
      self.OPassword = ko.observable();
      self.CPassword = ko.observable();
      self.NPassword = ko.observable();
      self.Branch = ko.observable();
      self.City = ko.observable();
      self.Country = ko.observable();
      self.IsAgent = ko.observable(false);

      //Validate the form
      $('#ChangePasswordForm').form(validationRules, {
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

      self.change = function () {
          if (self.Validated()) {
              var model = { OldPassword: self.OPassword(), NewPassword: self.NPassword(), ConfirmPassword: self.CPassword() };
              $.post(serverAddress + 'change', model, function (rData) {
                  toastr.info(rData.Message, 'Change Password');
                  self.showProgress(false);
                  if (rData.Success) {
                      setTimeout(function () { return true; }, 3000);
                      var nl = window.location.href.replace(window.location.hash, '#');
                      window.location.href = nl;
                      window.location.reload();
                  }
              });
          }
      };

      self.getData = function () {
          $.getJSON(serverAddress + 'users/get', function (rData) {
              if (rData.Success) {
                  var data = rData.Data;
                  //set the values Here
                  self.Id(data.Id);
                  self.FullName(data.FullName);
                  self.UserName(data.UserName);
                  self.Password('password');
                  self.PhoneNumber(data.PhoneNumber);
                  self.Email(data.Email);
                  self.IsActive(data.IsActive);
                  self.Roles(data.Roles);
                  self.DateOfBirth(moment(data.DateOfBirth).format('Do MMMM, YYYY'));
              }
          });
      };

      self.getData();
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  changePassword.prototype.dispose = function() { };
  
  return { viewModel: changePassword, template: templateMarkup };

});
