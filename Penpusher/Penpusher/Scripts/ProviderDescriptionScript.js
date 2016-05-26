var ProviderViewModel = function (name, image, description, subscriptionDate, buttonText) {
    var self = this;
    self.name = ko.observable(name);
    self.image = ko.observable(image);
    self.description = ko.observable(description);
    self.date = ko.observable(subscriptionDate);
    self.buttonText = ko.observable(buttonText);
    var subscribe;
    function unsubscribe() {
        self.buttonText("Subscribe");
        self.subscribe = function () {
            $.post('/api/Subscriptions/SubscribeUserToProvider?providerId=' + localStorage.providerId, function (data) {
                subscribe();
           
            });
        };
    }

    subscribe=function() {
        self.buttonText("Unsubscribe");
        self.subscribe = function () {
            $.post('/api/Subscriptions/UnsubscribeUserToProvider?providerId=' + localStorage.providerId, function (data) {
                unsubscribe();
            });

        };
    }

    $.get('/api/Subscriptions/IsUserSubscriberToProvider?providerId=' + localStorage.providerId, function (data) {
        if (data === true) {

        } else {
            unsubscribe();
        }
    });
};


$.get('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function (data) {

    ko.applyBindings(new ProviderViewModel(data.Name, data.RssImage, data.Description,
        data.SubscriptionDate, ""), document.getElementById("providerDescriptionContainer"));

});






