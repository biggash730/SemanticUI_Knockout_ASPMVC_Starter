$(document).ready(function () {

    function dashboardViewModel() {
        var self = this;
        var date = new Date();
        self.TTYearName = ko.observable(date.getFullYear());
        self.CurrentMonth = ko.observable(date.getMonth() + 1);
        self.TTMonth = ko.observable(self.CurrentMonth());
        self.TTMonthName = ko.observable(months[self.CurrentMonth() - 1].toUpperCase());
        self.Pending = ko.observableArray([]);
        self.Recieved = ko.observableArray([]);
        self.Cancelled = ko.observableArray([]);
        
        $("#transactionsMonthDropdown").dropdown({
            onChange: function (val) {
                self.TTMonth(months.indexOf(val) + 1);
                self.TTMonthName(val.toUpperCase());
                self.GetTransactionsForTheMonth();
            }
        });
        
        self.GetTotalTransactionsForTheYear = function () {
            var pending = ['Pending'];
            var recieved = ['Recieved'];

            $.post('dashboard/GetTransactionsForTheYear?year=' + self.TTYearName(), function (rData) {
                if (rData.Success) {
                    pending = pending.concat(rData.Data.Pending);
                    recieved = recieved.concat(rData.Data.Recieved);

                    var chart = c3.generate({
                        bindto: '#chart2',
                        data: {
                            columns: [pending, recieved],
                            type: 'bar',
                            colors: {
                                recieved: '#0927bb',
                                pending: '#de0909'
                            },
                            color: function (color, d) {
                                // d will be 'id' when called for legends
                                return d.id && d.id === 'recieved' ? d3.rgb(color).darker(d.value / 150) : color;
                            }
                        },
                        axis: {
                            x: {
                                type: 'category',
                                categories: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN', 'JUL', 'AUG', 'SEP', 'OCT', 'NOV', 'DEC']
                            }
                        }
                    });
                }
            });


        };
        self.GetTotalTransactionsForTheYear();
        
        self.GetTransactionsForTheMonth = function () {

            $.post('dashboard/gettransactionsformonth?month=' + self.TTMonth(), function (rData) {
                if (rData.Success) {
                    var chart = c3.generate({
                        bindto: '#chart1',
                        data: {
                            columns: [
                                ['Pending', rData.Data.Pending],
                                ['Recieved', rData.Data.Recieved]
                            ],
                            type: 'pie'/*,
                            colors: {
                                Recieved: '#0927bb',
                                Pending: '#de0909'
                            },
                            color: function (color, d) {
                                // d will be 'id' when called for legends
                                return d.id && d.id === 'Recieved' ? d3.rgb(color).darker(d.value / 150) : color;
                            }*/
                        },
                        pie: {
                            label: {
                                format: function(value, ratio, id) {
                                    return d3.format('')(value);
                                }
                            }
                        }
                    });
                }

            });
        };
        self.GetTransactionsForTheMonth();
        
        self.GetPending = function () {
            $.getJSON('dashboard/transactions?status=Pending', function (rData) {
                if (rData.Success) {
                    self.Pending(rData.Data);
                }
            });
        };
        self.GetPending();
        
        self.GetRecieved = function () {
            $.getJSON('dashboard/transactions?status=Recieved', function (rData) {
                if (rData.Success) {
                    self.Recieved(rData.Data);
                }
            });
        };
        self.GetRecieved();
        
        self.GetCancelled = function () {
            $.getJSON('dashboard/transactions?status=Cancelled', function (rData) {
                if (rData.Success) {
                    self.Cancelled(rData.Data);
                }
            });
        };
        self.GetCancelled();

    }
    ko.applyBindings(new dashboardViewModel(), $('#DashboardPage').get(0));

});