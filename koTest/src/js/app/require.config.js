// require.js looks for the following global when initializing
var require = {
    baseUrl: ".",
    paths: {
        "crossroads": "js/lib/crossroads.min",
        "hasher": "js/lib/hasher.min",
        "jquery": "js/lib/jquery.min",
        "knockout": "js/lib/knockout",
        "knockout-projections": "js/lib/knockout-projections",
        "signals": "js/lib/signals.min",
        "text": "js/lib/text",
        "c3": "js/lib/c3.min",
        "d3": "js/lib/d3.min",
        "semantic": "js/lib/semantic.min",
        "accounting": "js/lib/accounting.min",
        "moment": "js/lib/moment.min",
        "router": "js/app/router",
        "toastr": "js/lib/toastr.min"
    },
    shim: {
        "semantic": { deps: ["jquery"] },
        "c3": { deps: ["d3"] }
    }
};