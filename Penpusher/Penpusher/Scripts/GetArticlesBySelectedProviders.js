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
        var someUserId = 4;
        var viewModel = new ArticlesModel(someUserId);
        ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
    });