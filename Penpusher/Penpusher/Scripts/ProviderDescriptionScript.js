var ProviderViewModel = function (name, link, subscriptionDate, description) {
    var self = this;
     
    self.name = ko.observable(name);
    self.link = ko.observable(link);
    self.description = ko.observable(description);
    self.subscriptionDate = ko.observable(subscriptionDate);
     


};

$(document)
    .ready(function () {
        var provider;
        $.getJSON('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function(data) {
            provider = data;
        });


        ko.applyBindings(new ProviderViewModel(provider.Name, provider.Link,
             provider.Subscription.Data, provider.Description), document.getElementById("providerDescriptionContainer"));
    });