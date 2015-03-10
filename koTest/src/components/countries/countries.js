define(['knockout', 'text!./countries.html', 'jquery', 'toastr'], function (ko, templateMarkup, $, toastr) {

  function countries(params) {
      var self = this;
      self.Id = ko.observable();
      self.data = ko.observableArray([]);
      self.Name = ko.observable();
      self.Description = ko.observable();
      self.ShortCode = ko.observable();
      self.Validated = ko.observable(false);

      //Validate the form
      $('#addCountryForm').form(validationRules, {
          inline: true,
          on: 'blur',
          onFailure: function () {
              $('#CountryFormModal').modal('show');
          },
          onSuccess: function () {
              //check the validation
              self.Validated(true);
          }
      });

      self.showAdd = function () {
          $('#CountryFormModal').modal('show');
          //$('#CountryFormModal').transition('shake');
          //clear all old data
          self.clearValues();
      };

      self.add = function () {
          toastr.info('Please Wait ...', 'Countries');
          if (self.Validated()) {
              var jsonData = { Id: self.Id(), Name: self.Name(), Description: self.Description(), ShortCode: self.ShortCode()};
              if (self.Id() > 0) {
                  $.post(serverAddress + 'countries/update', jsonData, function (rData) {
                      toastr.info(rData.Message, 'Countries');
                      if (rData.Success) {
                          //clear form 
                          self.clearValues();
                          //get all data
                          self.getData();
                      }
                  });
              } else {
                  $.post(serverAddress + 'countries/insert', jsonData, function (rData) {
                      toastr.info(rData.Message, 'Countries');
                      if (rData.Success) {
                          //clear form 
                          self.clearValues();
                          //get all data
                          self.getData();
                      } //notifyMe(false, rData.Message);
                  });
              }
          }
      };

      self.showEdit = function (data) {
          self.Id(data.Id);
          self.Name(data.Name);
          self.Description(data.Description);
          self.ShortCode(data.ShortCode);
          $('#CountryFormModal').modal('show');
      };

      self.showDelete = function (data) {
          self.Id(data.Id);
          self.Name(data.Name);
          self.Description(data.Description);
          self.ShortCode(data.ShortCode);
          $('#CountryDeleteModal').modal('show');
      };

      self.del = function () {
          $.post(serverAddress + 'countries/delete?id=' + self.Id(), function (rData) {
              toastr.info(rData.Message, 'Countries');
              if (rData.Success) {
                  //clear form 
                  self.clearValues();
                  //get all data
                  self.getData();
              } //notifyMe(false, rData.Message);
          });


      };

      self.clearValues = function () {
          self.Id(0);
          self.Name('');
          self.Description('');
          self.ShortCode('');
      };

      self.getData = function () {
          self.data([]);
          $.post(serverAddress + 'countries/getall', { Pager: new Pager(0, 0) }, function (rData) {
              //toastr.info(rData.Message, 'Countries');
              if (rData.Success) self.data(rData.Data);
          });
      };
      self.getData();
  }

  // This runs when the component is torn down. Put here any logic necessary to clean up,
  // for example cancelling setTimeouts or disposing Knockout subscriptions/computeds.
  countries.prototype.dispose = function() { };
  
  return { viewModel: countries, template: templateMarkup };

});
