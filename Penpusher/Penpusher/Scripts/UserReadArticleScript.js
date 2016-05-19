var articleInfo = function (title, articleText, image) {
    this.title = title;
    this.articleText = articleText;
    this.image = image;
}

var articlesList = new ko.observableArray();
var userId = 4;

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
                    var article = new articleInfo(item.Title, item.Description, "http://hi-news.ru/wp-content/uploads/2015/10/community_image_1403628549-650x370.jpg");
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