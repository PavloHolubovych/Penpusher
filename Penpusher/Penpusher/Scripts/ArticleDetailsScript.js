$(document)
   .ready(function () {
       var ArticleModel = function (title, content, link) {
           self = this;
           self.title = ko.observable(title);
           self.content = ko.observable(content);
           self.link = ko.observable(link);
       };
       $.get("/api/Articles/GetArticleDetail?articleId=25",
        function (data) {
            var article = new ArticleModel(data.Title, data.Description, data.Link);
            ko.applyBindings(article, document.getElementById("#articleContent"));
        });

   });