define(['knockout', 'text!./change-password.html'], function(ko, templateMarkup) {

  function ChangePassword(params) {
    this.message = ko.observable('Hello from the change-password component!');
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  ChangePassword.prototype.dispose = function() { };
  
  return { viewModel: ChangePassword, template: templateMarkup };

});
