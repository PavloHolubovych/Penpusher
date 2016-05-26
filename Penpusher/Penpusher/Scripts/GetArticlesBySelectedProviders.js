﻿$(document)
    .ready(function () {

        var ArticlesModel = function (userId) {
            self = this;
            self.articles = ko.observableArray([]);
            $.ajax({
                url: apiController + userId,
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
        var apiController = window.location.origin + "/api/Articles/ArticlesFromSelectedProviders?userId=";
        var userId = 5;
        var viewModel = new ArticlesModel(userId);
        ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
    });