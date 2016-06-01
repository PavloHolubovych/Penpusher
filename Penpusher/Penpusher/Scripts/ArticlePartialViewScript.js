//addToReadLater = function (articleId) {
//    var element = $(event.target);

//    var parentEl = element.parent();
//    $.post("/api/Articles/ToReadLater", { "articleId": articleId, "flag": true }).done(function(data) {
//            element.text("Already in read later list");
//            element.addClass("disabled");
//        })
//        .error(function(request, textStatus) {
//            alert("Error: " + textStatus);
//        });
//};
//addToFavorite = function (articleId, add) {
//    var element = $(event.target);

//    var parentEl = element.parent();
//    $.post("/api/Articles/AddRemoveFavorites", { "articleId": articleId, "flag": add }).done(function (data) {
//        if (add) {
//            $(parentEl.find("button")[1]).hide();
//            $(parentEl.find("button")[2]).show();
//        } else {
//            $(parentEl.find("button")[2]).hide();
//            $(parentEl.find("button")[1]).show();
//        }
//        element.addClass("disabled");
//    })
//    .class(function (request, textStatus) {
//        alert("Error: " + textStatus);
//    });
//};
