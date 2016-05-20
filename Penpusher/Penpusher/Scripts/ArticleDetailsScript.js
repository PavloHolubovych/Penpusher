$(document)
   .ready(function () {
       var ArticleModel = function (title, link) {
           self = this;
           self.title = ko.observable(title); 
           self.link = ko.observable(link);
       };
       $.get("/api/Articles/GetArticleDetail?articleId=25",
        function (data) {
            var article = new ArticleModel(data.Title, data.Link);
           
            ko.applyBindings(article, document.getElementById("articleContent"));
        });

       $.get("/api/Articles/MarkAsRead?userId=5&articleId=29",
        function (data) { 
        });
   });