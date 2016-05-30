var NewsProviderModel = function () {
    var self = this;
    var userId = 5;
    self.newsproviders = ko.observableArray([]);
    self.availableChannels = ko.observableArray([]);
    self.AddLink = ko.observable();
    self.AddName = ko.observable();
    self.AddDescription = ko.observable();

   $.getJSON('/api/Subscriptions/GetByUser/' + userId, function (channel) {
        self.newsproviders(channel);
    });

   $.getJSON('/api/Subscriptions/GetAllNewsProviders', function (channel) {
        self.availableChannels(channel);
    });

    self.addNewsProvider = function (newsprovider) {
        var link = newsprovider.AddLink();
        var name = newsprovider.AddName;
        var description = newsprovider.AddDescription;
        var newsProvider = {};
        newsProvider.Link = link;
        newsProvider.Name = name;
        newsProvider.Description = description;

        //var length = self.availableChannels;

        for (var i = 0; i < self.availableChannels().length; i++) {
            if (link === self.availableChannels()[i].Link) {
                alert("You have subscribed on this channel");
            }  
        }
        
            $.ajax({
                url: '/api/Subscriptions/Post',
                type: "POST",
                data: newsProvider,
                success: function() {
                    $.getJSON('/api/Subscriptions/GetByUser/' + userId, function(channel) { self.newsproviders(channel); });

                    $.getJSON('/api/Subscriptions/GetAllNewsProviders', function(channel) { self.availableChannels(channel); });
                }
            });
    };

    self.addSubscriptionFromList = function (newsprovider) {
            $.ajax({
                url: '/api/Subscriptions/Post',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsprovider,
                success: function() {
                    $.getJSON('/api/Subscriptions/GetByUser/' + userId, function(channel) {

                        self.newsproviders(channel);
                    });
                }
            });
    };

    self.removeNewsProvider = function (newsprovider) {
        var id = newsprovider.Id;

        $.ajax({
            url: '/api/Subscriptions/Delete/' + id,
            type: "Delete",
            /*contentType: "application/json; charset=utf-8",*/
            data: {},
            dataType: 'json',
            success: function () {
            }
        });
        self.newsproviders.remove(newsprovider);
    };
};

ko.applyBindings(new NewsProviderModel, document.getElementById('generalSubscriptions'));
document.getElementById("SubscriptionsPage").className = "active";