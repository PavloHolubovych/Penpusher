var ArticleModel = function (title, link, addVisibility, removeVisibility) {
    self = this;
    self.title = ko.observable(title);
    self.link = ko.observable(link);
    self.addToFavoritesVisibility = ko.observable(addVisibility);
    self.removeFromFavoritesVisibility = ko.observable(removeVisibility);

    self.addToFavorites = function (userId, articleId) {
        $.post("/api/Articles/AddToFavorites", { "userId": userId, "articleId": articleId })
            .success(function () {
                self.addToFavoritesVisibility(false);
                self.removeFromFavoritesVisibility(true);
            })
            .error(function (request, textStatus) {
                alert("Error: " + textStatus);
            });
    };

    self.removeFromFavorites = function (userId, articleId) {
        $.post("/api/Articles/RemoveFromFavorites", { "userId": userId, "articleId": articleId })
            .success(function () {
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
       $('#addToReadLater').show();
       $('#removeFromReadLater').hide();

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

       $.get("/api/Articles/MarkAsRead?userId=" + localStorage.userId + "&articleId=" + localStorage.articleId,
            function(data) {});

       var userId = 4;

       $.get("/api/Articles/ReadLaterInfo?userId=" + userId + "&articleIdInfo=" + QueryString.articleId).
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
       
        });

function AddtoReadLater(userId) {
    $.post("/api/Articles/ToReadLater?userId=" + userId + "&articleIdRL=" + QueryString.articleId + "&add=true")
        .success(function (data) {
            var article = data;
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

function DeleteFromReadLater(userId) {
    $.post("/api/Articles/ToReadLater?userId=" + userId + "&articleIdRL=" + QueryString.articleId + "&add=false")
        .success(function (data) {
            var article = data;
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
// For getting all params from url
var QueryString = function () {
    // This function is anonymous, is executed immediately and 
    // the return value is assigned to QueryString!
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
}();