    var NewsProviderModel = function () {
        var self = this;
        var userId = 5;
        self.newsproviders = ko.observableArray([]);
        self.availableChannels = ko.observableArray([]);
        self.AddLink = ko.observable();

        $.getJSON('/api/Subscriptions/GetByUser/' + userId, function (channel) {

            self.newsproviders(channel);
        });

        $.getJSON('/api/Subscriptions/GetAllNewsProviders', function (channel) {

            self.availableChannels(channel);
        });
        
        self.addNewsProvider = function (newsprovider) {
            var link = newsprovider.AddLink;
            console.log({ link });
            var newsProvider = {};
            newsProvider.Link = link;
            $.ajax({
                url: '/api/Subscriptions/Post',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsProvider,
                success: function () {
                    $.getJSON('/api/Subscriptions/GetByUser/' + userId, function (data) {
                        self.newsproviders(data);
                    });
                }
            });
        };

        self.addSubscriptionFromList = function (newsprovider) {
            $.ajax({
                url: '/api/Subscriptions/Post',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsprovider,
                success: function () {
                    $.getJSON('/api/Subscriptions/GetByUser/' + userId, function (data) {
                        self.newsproviders(data);
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
                data:{},
                dataType: 'json',
                success: function () {
                }
            });
            self.newsproviders.remove(newsprovider);
        };

    };


    ko.applyBindings(new NewsProviderModel, document.getElementById('subscription'));
    ko.applyBindings(new NewsProviderModel, document.getElementById('availableChannel'));

// Activate jQuery Validation
//$("form").validate({
//  submitHandler: PeopleModel.save
//});