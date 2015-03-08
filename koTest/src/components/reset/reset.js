define(['knockout', 'text!./reset.html'], function(ko, templateMarkup) {

  function Reset(params) {
    this.message = ko.observable('Hello from the reset component!');
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  Reset.prototype.dispose = function() { };
  
  return { viewModel: Reset, template: templateMarkup };

});
