define(['knockout', 'text!./users.html'], function(ko, templateMarkup) {

  function Users(params) {
    this.message = ko.observable('Hello from the users component!');
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  Users.prototype.dispose = function() { };
  
  return { viewModel: Users, template: templateMarkup };

});
