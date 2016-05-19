$(document).ready(function () {
    var apiController = window.location.origin + "/api/Articles/ArticlesFromSelectedProviders?someUserId=";
    var someUserId = 4;
    var drawArticlesOnPage = function (result) {
        ko.applyBindings({
            Articles: result
        });
    };
    $.ajax({
        url: apiController + someUserId,
        method: "GET",
        success: function (result) {
            drawArticlesOnPage(result);
        },
        error: function (request, textStatus) {
            alert("Error: " + textStatus);
        }
    });
})