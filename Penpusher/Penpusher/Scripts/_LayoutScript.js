
$(document)
    .ready(function() {
        var ProvidersModel = function(providers) {
            var self = this;
            self.providers = ko.observableArray(providers);
            self.providers.push(
                         "All"
                       );
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
            self.loadRead = function () {
                location.href = "/Main/UserReadArticles";

            }
        };          
        var viewModel = new ProvidersModel();
        ko.applyBindings(viewModel, document.getElementById("#sidebar"));
 });
