$(document)
   .ready(function () { 
       var ArticleModel = function (title, link)
       {
           self = this;
           self.title = ko.observable(title); 
           self.link = ko.observable(link);
           addToFavorites = function (userId, articleId) {
               $.post("/api/Articles/AddToFavorites?userId=" + userId + "&articleId=" + articleId)
                   .success(function () {
                       alert("Article added to your favorites");
                       console.log(userId + " " + articleId);
                   })
                   .error(function (request, textStatus) {
                       alert("Error: " + textStatus);
                   });
           };
       };

       $.get("/api/Articles/GetArticleDetail?articleId=" + localStorage.id,
        function (data) {
            var article = new ArticleModel(data.Title, data.Link);
            ko.applyBindings(article, document.getElementById("articleContent"));
        });

       $.get("/api/Articles/MarkAsRead?userId=5&articleId=" + localStorage.id,
        function (data) { 
        });
   });
