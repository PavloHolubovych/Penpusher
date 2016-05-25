var articleInfo = function (title, articleText, image, id) {
    this.title = title;
    this.articleText = articleText;
    this.image = image;
    this.articleDetailLink = "/Main/ArticleContentDetails?articleId=" + id;
}

var articlesList = new ko.observableArray();
var userId = 5;

var ToReadLaterArticlesViewModel = function () {
    $.ajax({
        type: "GET",
        url: "/api/Articles/GetReadLeaterArticles",
        data: JSON.stringify(articleInfo),
        contentType: "application/json; charset=utf8",
        accept: "application/json",
        success: function (data) {
            $.each(data,
                function (key, item) {
                    var article = new articleInfo(item.Title, item.Description, "https://upload.wikimedia.org/wikipedia/en/thumb/4/43/Feed-icon.svg/128px-Feed-icon.svg.png", item.Id);
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
        ko.applyBindings(ToReadLaterArticlesViewModel, document.getElementById('readArticlesContainer'));
    });