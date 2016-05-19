
var ArticleModel = function (contentData, link, title) {
    this.content = contentData;
    this.link = link;
    this.title = title;
}
var article;
$.ajax({
    type: "GET",
    url: "/api/Articles/GetArticleDetail?articleId=25",
    contentType: "application/json; charset=utf8",
    accept: "application/json",
    success: function (data) {
        article = new ArticleModel(data.Description, data.Link, data.Title);

        ko.applyBindings(article, document.getElementById("#articleContent"));
    },
    error: function (request, textStatus) {
        alert("Error: " + textStatus);
    }
});

