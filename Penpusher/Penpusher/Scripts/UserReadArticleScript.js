var articleInfo = function (title, articleText, image, id) {
    this.title = title;
    this.articleText = articleText;
    this.image = image;
    this.articleDetailLink = "/Main/ArticleContentDetails?articleId=" + id;
}

var articlesList = new ko.observableArray();
var userId = 5;

var ReadArticlesViewModel = function () {
    $.ajax({
        type: "GET",
        url: "/api/Articles/UserReadArticles?userId=" + userId,
        data: JSON.stringify(articleInfo),
        contentType: "application/json; charset=utf8",
        accept: "application/json",
        success: function (data) {
            $.each(data,
                function (key, item) {
                    var article = new articleInfo(item.Title, item.Description, item.Image, item.Id);
                    articlesList.push(article);
                });
        },
        error: function (request, textStatus) {
            alert("Error: " + textStatus);
        }
    });
}

$(document)
    .ready(function () {
        ko.applyBindings(ReadArticlesViewModel, document.getElementById('readArticlesContainer'));
    });