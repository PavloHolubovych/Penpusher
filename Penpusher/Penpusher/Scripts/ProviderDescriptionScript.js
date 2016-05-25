var ProviderViewModel = function (name, link, subscriptionDate, description) {
    var self = this;
     
    self.name = ko.observable(name);
    self.link = ko.observable(link);
    self.description = ko.observable(description);
    self.subscriptionDate = ko.observable(subscriptionDate);
     


};

$(document)
    .ready(function (){
        $.getJSON('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function(data) {
            var provider = new ProviderViewModel(data.Name, data.Link,
                data.SubscriptionDate, data.Description);
            ko.applyBindings(provider, document.getElementById("providerDescriptionContainer"));
        });        
    });