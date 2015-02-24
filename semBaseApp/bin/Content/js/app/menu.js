$(document).ready(function () {
    $("#userDropdown").dropdown({
        onChange: function (val) {
            if (val == 'logout') {
                ClearToken();
            }
        }
    });
    
    $("#menuDropdown").dropdown();

    /*function menuViewModel() {
        var self = this;
        self.showMain = ko.observable(true);

        
    }
    ko.applyBindings(new menuViewModel());*/
});