define(['knockout', 'text!./login.html'], function(ko, templateMarkup) {

  function Login(params) {
    this.message = ko.observable('Hello from the login component!');
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  Login.prototype.dispose = function() { };
  
  return { viewModel: Login, template: templateMarkup };

});
