
$(document)
    .ready(function () {
        var ProvidersModel = function (providers) {
            var ProviderObject = function (Name, ID) {
                this.Name = Name;
                this.Id = ID;
            }
            var self = this;
            self.providers = ko.observableArray([new ProviderObject("All", "All")]);
            self.selectedProvider = ko.observable();
            
            $.get("/api/getallsubscription/4",
                function (data, status) {
                    for (var i = 0; i < data.length; i++) {
                        self.providers.push(
                            new ProviderObject(data[i].Name, data[i].IdNewsProvider)
                        );
                    }
                });
            self.selectedProvider.subscribe(function (data) {
                if (data === "All") location.href = "/Main/ArticlesBySelectedSubscriptions?someUserId=4";
                else
                    location.href = "/Main/ArticlesBySubscription?providerID=" + data;

            }, self);

        };
        var viewModel = new ProvidersModel();
        ko.applyBindings(viewModel, document.getElementById("sideBarSelect"));
    });

function ManageContentPage() {
    location.href = "/Main/Subscriptions";
}

function LoadReadPage() {
    location.href = "/Main/UserReadArticles";
}