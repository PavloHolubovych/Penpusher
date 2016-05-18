$(document).ready(function () {
    var APIController = window.location.origin + "/api/Articles/ArticlesFromProvider?newsProviderId=";
    var SubscriptionID = 1;
    var DrawArticlesOnPage = function(result) {
        ko.applyBindings({
            Articles: result
        });
    };
    $.ajax({
        url: APIController + SubscriptionID,
        method: "GET",
        success: function (result) {
            DrawArticlesOnPage(result);
        },
        error: function (request, textStatus) {
            alert("Error: " + textStatus);
        }
    });
})