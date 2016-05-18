$(document).ready(function () {
    $("#ToFavoriteButton").click(function () {

        $.get('@Url.Action("part","Home")', function (data) {
            alert("sfd");
            $('#content').replaceWith(data);

        });
    });
    
    //var DrawSubscriptions = function (result) {
    //    var viewModel = {
    //        SubscriptionsSelect: ko.observableArray(result),
    //        selectedSubscription: ko.observable()
    //};

    //    ko.applyBindings(viewModel);
    //};

    //$.ajax({
    //    url: window.location.origin + "/api/getall2/4",
    //    method: "GET",
    //    success: function (result) {
    //        DrawSubscriptions(result);
    //    },
    //    error: function (request, textStatus) {
    //        alert("Error: " + textStatus);
    //    }
    //});

    $("#sideBarSelect").change(function () {
        
    });

});