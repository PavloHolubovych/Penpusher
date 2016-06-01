addToReadLater = function (articleId) {
    var element = $(event.target);
    $.post("/api/Articles/ToReadLater", { "articleId": articleId, "flag": true }).done(function (data) {
            element.text("Already in read later list");
            element.addClass("disabled");
        })
        .error(function (request, textStatus) {
            alert("Error: " + textStatus);
        });
}
