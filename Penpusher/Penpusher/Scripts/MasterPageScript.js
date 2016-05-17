$(document).ready(function () {
    $("#ToFavoriteButton").click(function () {

        $.get('@Url.Action("part","Home")', function (data) {
            alert("sfd");
            $('#content').replaceWith(data);

        });
    });
    $("#sideBarSelect").change(function () {

    });

});


