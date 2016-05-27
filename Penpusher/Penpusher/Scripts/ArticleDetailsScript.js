var QueryString = function () {
    var queryString = {};
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        // If first entry with this name
        if (typeof queryString[pair[0]] === "undefined") {
            queryString[pair[0]] = decodeURIComponent(pair[1]);
            // If second entry with this name
        } else if (typeof queryString[pair[0]] === "string") {
            var arr = [queryString[pair[0]], decodeURIComponent(pair[1])];
            queryString[pair[0]] = arr;
            // If third or later entry with this name
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

    $.get("/api/Articles/ReadLaterInfo?userId=" + localStorage.userId + "&articleIdInfo=" + QueryString().articleId).
           success(function (data) {
               var article = data;
               if (data.IsToReadLater) {
                   $('#addToReadLater').hide();
                   $('#removeFromReadLater').show();
               } else {
                   $('#addToReadLater').show();
                   $('#removeFromReadLater').hide();
               }
           }).error(function (request, textStatus) {
               alert("Error: " + textStatus);
           });

    $.post("/api/Articles/MarkAsRead?userId=" + localStorage.userId + "&articleId=" + QueryString().articleId);

    var setVisibility = function() {
        $.get("/api/Articles/CheckIsFavorite?userId=" + localStorage.userId + "&articleId=" + QueryString().articleId,
            function(result) {
                if (result) {
                    self.addToFavoritesVisibility(false);
                    self.removeFromFavoritesVisibility(true);
                } else {
                    self.addToFavoritesVisibility(true);
                    self.removeFromFavoritesVisibility(false);
                };
            });
    };

    setVisibility();

    self.addToFavorites = function () {
        $.post("/api/Articles/AddRemoveFavorites", { "userId": localStorage.userId, "articleId": QueryString().articleId, "flag": true })
            .success(setVisibility)
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.removeFromFavorites = function () {
        $.post("/api/Articles/AddRemoveFavorites", { "userId": localStorage.userId, "articleId": QueryString().articleId, "flag": false })
            .success(setVisibility)
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.addToReadLater = function () {
        $.post("/api/Articles/ToReadLater?userId=" + localStorage.userId + "&articleIdRl=" + QueryString().articleId + "&add=true")
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
        $.post("/api/Articles/ToReadLater?userId=" + localStorage.userId + "&articleIdRl=" + QueryString().articleId + "&add=false")
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
};

$.get("/api/Articles/GetArticleDetail?articleId=" + localStorage.articleId,
 function (data) {
     var article = new ArticleViewModel(data.Title, data.Link);
     ko.applyBindings(article, document.getElementById("articleContent"));
 });