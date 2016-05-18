$(document).ready(function () {
    $("#ToFavoriteButton").click(function () {

        $.get('@Url.Action("part","Home")', function (data) {
            alert("sfd");
            $('#content').replaceWith(data);

        });
    });


    
    
    var DrawSubscriptions = function (result) {

        ko.applyBindings({
            availableCountries: result
        });

        //var viewModel = {
        //    // These are the initial options
        //    availableCountries: ko.observableArray(result)
        //};

        //// ... then later ...
        //viewModel.availableCountries.push('China'); // Adds another option
    };

    

    $.ajax({
        url: window.location.origin + "/api/Subscriprions/Get",
        method: "GET",
        success: function (result) {
            DrawSubscriptions(result);
        },
        error: function (request, textStatus) {
            alert("Error: " + textStatus);
        }
    });




    $("#sideBarSelect").change(function () {
        
    });

});


