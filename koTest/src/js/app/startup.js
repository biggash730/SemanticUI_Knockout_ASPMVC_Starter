define(["jquery", "knockout", "router", "semantic", "knockout-projections"], function ($, ko, router) {
    // Components can be packaged as AMD modules, such as the following:
    ko.components.register('nav-bar', { require: 'components/nav-bar/nav-bar' });
    ko.components.register('dashboard', { require: 'components/dashboard/dashboard' });
    ko.components.register('settings', { require: 'components/settings/settings' });
    ko.components.register('reports', { require: 'components/reports/reports' });
    ko.components.register('users', { require: 'components/users/users' });
    ko.components.register('login', { require: 'components/login/login' });
    ko.components.register('reset', { require: 'components/reset/reset' });
    ko.components.register('change-password', { require: 'components/change-password/change-password' });

    // ... or for template-only components, you can just point to a .html file directly:
    ko.components.register('about-page', {
        template: { require: 'text!components/about-page/about.html' }
    });

    // [Scaffolded component registrations will be inserted here. To retain this feature, don't remove this comment.]

    // Start the application
    ko.applyBindings({ route: router.currentRoute });
});

function ClearToken() {
    localStorage.token = "";
}

var Pager = function (page) {
    this.Page = page;
    this.Size = 25;
};

var allSize = 1000;

var print = function (divToPrint) {

    var newWin = window.open('', 'Print-Window', 'width=600,height=600,top=100,left=100');
    newWin.document.open();
    newWin.document.write('<html><head><link href="/Content/css/semantic.min.css" rel="stylesheet"/> <script src="/Content/js/semantic.min.js"></script> </head><body   onload="window.print()"><div><h2 class="ui center aligned header"><div><img @*class="circular ui  image"*@ src="/Content/images/logo.jpg" alt="logo" style="width: 92px; height: 92px;"></div><div class="content"><div class="sub header"><p>Instant Cash Transfer Services | Post Office Box KN 2232, Accra Ghana.</p><p>0244112233 / 0201234567 | instacash@jefam.com</p></div></div></h2></div> <div class="ui divider"></div><div>' + divToPrint.innerHTML + '</div></body></html>');
    newWin.document.close();
};


var validationRules = {
    name: {
        identifier: 'name',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter the name'
            }
        ]
    },
    fullname: {
        identifier: 'fullname',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter the full name'
            }
        ]
    },
    username: {
        identifier: 'username',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a username'
            }
        ]
    },
    password: {
        identifier: 'password',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a password'
            },
            {
                type: 'length[6]',
                prompt: 'Your password must be at least 6 characters'
            }
        ]
    },
    email: {
        identifier: 'email',
        rules: [
            {
                type: 'email',
                prompt: 'The email must be valid'
            }
        ]
    },
    phonenumber1: {
        identifier: 'phonenumber1',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a mobile number'
            }
        ]
    },
    phonenumber: {
        identifier: 'phonenumber',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a phone number'
            }
        ]
    },
    opassword: {
        identifier: 'opassword',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a password'
            },
            {
                type: 'length[6]',
                prompt: 'Your password must be at least 6 characters'
            }
        ]
    },
    npassword: {
        identifier: 'npassword',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a password'
            },
            {
                type: 'length[6]',
                prompt: 'Your password must be at least 6 characters'
            }
        ]
    },
    cpassword: {
        identifier: 'cpassword',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a password'
            },
            {
                type: 'length[6]',
                prompt: 'Your password must be at least 6 characters'
            }
        ]
    },
    accountnumber: {
        identifier: 'accountnumber',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter an account number'
            }
        ]
    },
    title: {
        identifier: 'title',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter a text here'
            }
        ]
    },
    amount: {
        identifier: 'amount',
        rules: [
            {
                type: 'empty',
                prompt: 'Please enter an amount'
            }
        ]
    },
    date: {
        identifier: 'date',
        rules: [
            {
                type: 'empty',
                prompt: 'Please select a date'
            }
        ]
    },
    answer: {
        identifier: 'answer',
        rules: [
            {
                type: 'empty',
                prompt: 'Please an answer'
            }
        ]
    }


};

var userString = "User";
var administratorString = "Administrator";
var serverAddress = "http://localhost:55296/";

var months = ["january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december"];

