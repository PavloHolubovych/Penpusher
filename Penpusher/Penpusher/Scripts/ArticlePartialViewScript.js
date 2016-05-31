addToReadLater = function (articleId) {
    $.post("/api/Articles/ToReadLater", { "articleId": articleId, "flag": true })
        .error(function (request, textStatus) {
            alert("Error: " + textStatus);
        });
}
