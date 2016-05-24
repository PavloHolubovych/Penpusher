var ArticleModel = function (title, link, addVisibility, removeVisibility) {
    self = this;
    self.title = ko.observable(title);
    self.link = ko.observable(link);
    self.addToFavoritesVisibility = ko.observable(addVisibility);
    self.removeFromFavoritesVisibility = ko.observable(removeVisibility);

    self.addToFavorites = function (userId, articleId) {
        $.post("/api/Articles/AddToFavorites?userId=" + userId + "&articleId=" + articleId)
            .success(function () {
                alert("Article added to your favorites");
                self.addToFavoritesVisibility(false);
                self.removeFromFavoritesVisibility(true);
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.removeFromFavorites = function (userId, articleId) {
        $.post("/api/Articles/RemoveFromFavorites?userId=" + userId + "&articleId=" + articleId)
            .success(function () {
                alert("Article added to your favorites");
                self.addToFavoritesVisibility(true);
                self.removeFromFavoritesVisibility(false);
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

};

$(document)
   .ready(function () { 

       var addVisibility, removeVisibility;

        $.get("/api/Articles/CheckIsFavorite?userId=" + localStorage.userId + "&articleId=" + localStorage.articleId,
            function(result) {
                if (result === true) {
                    addVisibility = false;
                    removeVisibility = true;
                } else {
                    addVisibility = true;
                    removeVisibility = false;
                }
            });

       $.get("/api/Articles/GetArticleDetail?articleId=" + localStorage.articleId,
        function (data) {
            var article = new ArticleModel(data.Title, data.Link, addVisibility, removeVisibility);
            ko.applyBindings(article, document.getElementById("articleContent"));
        });

       $.get("/api/Articles/MarkAsRead?userId=5&articleId=" + localStorage.articleId,
        function (data) { 
        });
   });
