require.config({
    //baseUrl: "Content/js",
    paths: {
        "jquery": "Content/js/jquery.min",
        "semantic": "Content/js/semantic.min",
        "knockout": "Content/js/knockout",
        "moment": "Content/js/moment.min",
        "accounting": "Content/js/accounting.min",
        "d3": "Content/js/d3.min",
        "c3": "Content/js/c3.min",
        "general": "Content/js/general"
    },
    shim: {
        "semantic": { deps: ["jquery"] },
        "knockout": { deps: ["jquery"] },
        "c3": { deps: ["d3"] },
        "general": { deps: ["jquery"] },
    }
});