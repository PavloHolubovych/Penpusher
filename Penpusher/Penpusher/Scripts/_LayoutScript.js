

var ProvidersModel = function () {
    var self = this;
    var dataArticles = [];
    function NewsProvider(name, id) {
        this.Name = name;
        this.Id = id;
    }
    self.providers = ko.observableArray([]);
    self.articles = ko.observableArray(dataArticles);
    self.selectedProvider = ko.observable();
    $.get("/api/getallsubscription/4",
        function (data) {
            for (var i = 0; i < data.length; i++) {
                self.providers.push(
                    new NewsProvider(data[i].Name, data[i].Id)
                );
            }
        });
    self.loadSubscriptions = function () {
        location.href = "/Main/Subscriptions";
    };
    self.selectedProvider.subscribe(function (newValue) {
        location.href = "/Main/ArticlesBySubscription?providerID=" + newValue;
    }, self);
};
var viewModel = new ProvidersModel();
ko.applyBindings(viewModel);


