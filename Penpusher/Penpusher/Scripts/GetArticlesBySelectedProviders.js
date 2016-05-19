$(document)
    .ready(function () {
        var ArticlesModel = function (someUserId) {
            self = this;
            self.articles = ko.observableArray([]);
            $.ajax({
                url: apiController + someUserId,
                method: "GET",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        self.articles.push(
                            data[i]
                        );
                    }
                },
                error: function (request, textStatus) {
                    alert("Error: " + textStatus);
                }
            });
        }
        var apiController = window.location.origin + "/api/Articles/ArticlesFromSelectedProviders?someUserId=";
        var someUserId = parseInt(QueryString.someUserId);
        if (someUserId === NaN) return;
        var viewModel = new ArticlesModel(someUserId);
        ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
    });

// For getting all params from url
var QueryString = function () {
    // This function is anonymous, is executed immediately and 
    // the return value is assigned to QueryString!
    var queryString = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        // If first entry with this name
        if (typeof queryString[pair[0]] === "undefined") {
            queryString[pair[0]] = decodeURIComponent(pair[1]);
            // If second entry with this name
        } else if (typeof queryString[pair[0]] === "string") {
            var arr = [queryString[pair[0]], decodeURIComponent(pair[1])];
            queryString[pair[0]] = arr;
            // If third or later entry with this name
        } else {
            queryString[pair[0]].push(decodeURIComponent(pair[1]));
        }
    }
    return queryString;
}();