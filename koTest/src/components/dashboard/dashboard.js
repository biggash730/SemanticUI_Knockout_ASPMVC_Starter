define(['knockout', 'text!./dashboard.html'], function(ko, templateMarkup) {

  function Dashboard(params) {
    this.message = ko.observable('Hello from the dashboard component!');
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  Dashboard.prototype.dispose = function() { };
  
  return { viewModel: Dashboard, template: templateMarkup };

});
