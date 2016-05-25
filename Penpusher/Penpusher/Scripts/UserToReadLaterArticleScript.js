var articleInfo = function (title, articleText, image, id) {
    this.title = title;
    this.articleText = articleText;
    this.image = image;
    this.articleDetailLink = "/Main/ArticleContentDetails?articleId=" + id;
}

var articlesList = new ko.observableArray();
var userId = 4;

var ToReadLaterArticlesViewModel = function () {
    $.ajax({
        type: "GET",
        url: "/api/Articles/UserToReadLaterArticles?userId=" + userId + "@articleId=",
        data: JSON.stringify(articleInfo),
        contentType: "application/json; charset=utf8",
        accept: "application/json",
        success: function (data) {
            $.each(data,
                function (key, item) {
                    var article = new articleInfo(item.Title, item.Description, "http://hi-news.ru/wp-content/uploads/2015/10/community_image_1403628549-650x370.jpg", item.Id);
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