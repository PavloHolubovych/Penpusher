var ProviderViewModel = function (name, image, description, subscriptionDate, buttonText) {
    var self = this;
    self.name = ko.observable(name);
    self.image = ko.observable(image);
    self.description = ko.observable(description);
    self.date = ko.observable(subscriptionDate);
    self.buttonText = ko.observable(buttonText);
    self.subscribe = function () {
        $.post('/api/Subscriptions/SubscribeUserToProvider?providerId=' + localStorage.providerId, function (data) { });
    };
};

var isSubscribed = true;
$.get('/api/Subscriptions/IsUserSubscriberToProvider?providerId=' + localStorage.providerId, function (data) {
    isSubscribed = data;
});

$.get('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function (data) {
    var buttext = "Subscribe";
    if (isSubscribed === true)
        buttext = "Unsubscribe";
    ko.applyBindings(new ProviderViewModel(data.Name, data.RssImage, data.Description,
        data.SubscriptionDate, buttext), document.getElementById("providerDescriptionContainer"));

});






