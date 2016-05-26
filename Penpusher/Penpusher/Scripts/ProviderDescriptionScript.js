var ProviderViewModel = function (name, image, description, subscriptionDate) {
    var self = this;

    self.name = ko.observable(name);
    self.image = ko.observable(image);
    self.description = ko.observable(description);
    self.date = ko.observable(subscriptionDate);
};

$.get('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function(data) {
    ko.applyBindings(new ProviderViewModel(data.Name, data.RssImage, data.Description, data.SubscriptionDate), document.getElementById("providerDescriptionContainer"));
});