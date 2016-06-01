var QueryString = function () {
    var queryString = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (typeof queryString[pair[0]] === "undefined") {
            queryString[pair[0]] = decodeURIComponent(pair[1]);
        } else if (typeof queryString[pair[0]] === "string") {
            var arr = [queryString[pair[0]], decodeURIComponent(pair[1])];
            queryString[pair[0]] = arr;
        } else {
            queryString[pair[0]].push(decodeURIComponent(pair[1]));
        }
    }
    return queryString;
};

var ArticleViewModel = function (title, link) {
    var self = this;
    self.title = ko.observable(title);
    self.link = ko.observable(link);
    self.addToFavoritesVisibility = ko.observable(true);
    self.removeFromFavoritesVisibility = ko.observable(false);

    $('#addToReadLater').show();
    $('#removeFromReadLater').hide();

    $.get("/api/Articles/UserArticleInfo?articleIdInfo=" + QueryString().articleId).
        success(function (data) {
            if (data.IsToReadLater) {
                $('#addToReadLater').hide();
                $('#removeFromReadLater').show();
            } else {
                $('#addToReadLater').show();
                $('#removeFromReadLater').hide();
            }
            if (data.IsFavorite) {
                self.addToFavoritesVisibility(false);
                self.removeFromFavoritesVisibility(true);
            } else {
                self.addToFavoritesVisibility(true);
                self.removeFromFavoritesVisibility(false);
            };
        }).error(function (request, textStatus) {
            alert("Error: " + textStatus);
        });

    $.post("/api/Articles/MarkAsRead", { "articleId": QueryString().articleId });

    self.addToFavorites = function () {
        $.post("/api/Articles/AddRemoveFavorites", { "articleId": QueryString().articleId, "flag": true })
            .success(function (data) {
                if (data) {
                    self.addToFavoritesVisibility(false);
                    self.removeFromFavoritesVisibility(true);
                } else {
                    self.addToFavoritesVisibility(true);
                    self.removeFromFavoritesVisibility(false);
                };
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.removeFromFavorites = function () {
        $.post("/api/Articles/AddRemoveFavorites", { "articleId": QueryString().articleId, "flag": false })
            .success(function (data) {
                if (data) {
                    self.addToFavoritesVisibility(false);
                    self.removeFromFavoritesVisibility(true);
                } else {
                    self.addToFavoritesVisibility(true);
                    self.removeFromFavoritesVisibility(false);
                };
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.addToReadLater = function () {
        $.post("/api/Articles/ToReadLater", { "articleId": QueryString().articleId, "flag": true })
            .success(function (data) {
                if (data.IsToReadLater) {
                    $('#addToReadLater').hide();
                    $('#removeFromReadLater').show();
                }
                else {
                    $('#addToReadLater').show();
                    $('#removeFromReadLater').hide();
                }
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    }

    self.deleteFromReadLater = function () {
        $.post("/api/Articles/ToReadLater", { "articleId": QueryString().articleId, "flag": false })
            .success(function (data) {
                if (data.IsToReadLater) {
                    $('#addToReadLater').hide();
                    $('#removeFromReadLater').show();
                } else {
                    $('#addToReadLater').show();
                    $('#removeFromReadLater').hide();
                }
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    }

    self.goBack = function () {
        window.history.back();
    }
};

$.get("/api/Articles/GetArticleDetail?articleId=" + QueryString().articleId,
 function (data) {
     var article = new ArticleViewModel(data.Title, data.Link);
     ko.applyBindings(article, document.getElementById("articleContent"));
 });