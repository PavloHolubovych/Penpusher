    var NewsProviderModel = function () {
        var self = this;
        var userId = 5;
        self.articles = ko.observableArray([]);
        self.AddLink = ko.observable("example");
        $.getJSON('/api/Articles/UserFavoriteArticles?userId=' + userId, function (data) {

            self.articles(data);

        });

    };

    ko.applyBindings(new NewsProviderModel, document.getElementById('favoriteArticles'));
