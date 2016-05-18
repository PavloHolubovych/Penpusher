
$(document)
    .ready(function() {
        var ProvidersModel = function(providers) {
            var self = this;

            self.providers = ko.observableArray(providers);

            $.get("/api/getallsubscription/4",
                function(data, status) {
                    for (var i = 0; i < data.length; i++) {
                        self.providers.push(
                            data[i].Name
                        );
                    }
                });
            self.loadSubscriptions = function() {
                location.href = "/Main/Subscriptions";
            };
        };
        var viewModel = new ProvidersModel();
        ko.applyBindings(viewModel);
    });
