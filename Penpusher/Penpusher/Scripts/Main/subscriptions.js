var NewsProviderModel = function () {
    var self = this;
    var userId = 5;
    self.newsproviders = ko.observableArray([]);
    self.availableChannels = ko.observableArray([]);
    self.AddLink = ko.observable();
    self.AddName = ko.observable();
    self.AddDescription = ko.observable();
    $('#successMessage').hide();
    $('#warningMessage').hide();
    $('#successDeleteMessage').hide();
    

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

           for (var i = 0; i < self.newsproviders().length; i++) {
               if (link === self.newsproviders()[i].Link) {
                   $('#successMessage').hide();
                   $('#warningMessage').show();
                   $('#successDeleteMessage').hide();
                   warning = true;
               }
           }
           $.ajax({
               url: '/api/Subscriptions/Post',
               type: "POST",
               data: newsProvider,
               success: function() {
                   $.getJSON('/api/Subscriptions/GetByUser/' + userId, function(channel) { self.newsproviders(channel); });
                   $.getJSON('/api/Subscriptions/GetAllNewsProviders', function (channel) { self.availableChannels(channel); });
                   if (warning === true) {
                       $('#successMessage').hide();
                       $('#warningMessage').show();
                       $('#successDeleteMessage').hide();
                   } else {
                       $('#successMessage').show();
                       $('#warningMessage').hide();
                       $('#successDeleteMessage').hide();
                   }
               }
           });
   };

    self.addSubscriptionFromList = function (newsprovider) {
        var warning = false;
        var link = newsprovider.Link;
        for (var i = 0; i < self.newsproviders().length; i++) {
            if (link === self.newsproviders()[i].Link) {
                $('#successMessage').hide();
                $('#warningMessage').show();
                $('#successDeleteMessage').hide();
                warning = true;
            }

        }
            $.ajax({
                url: '/api/Subscriptions/Post',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsprovider,
                success: function() {
                    $.getJSON('/api/Subscriptions/GetByUser/' + userId, function(channel) { self.newsproviders(channel); });
                    if (warning === true) {
                        $('#successMessage').hide();
                        $('#warningMessage').show();
                        $('#successDeleteMessage').hide();
                    } else {
                        $('#successMessage').show();
                        $('#warningMessage').hide();
                        $('#successDeleteMessage').hide();
                    }
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
                $('#successMessage').hide();
                $('#warningMessage').hide();
                $('#successDeleteMessage').show();
            }
        });
        self.newsproviders.remove(newsprovider);
    };
};
$('#message').hide();
ko.applyBindings(new NewsProviderModel, document.getElementById('generalSubscriptions'));
document.getElementById("SubscriptionsPage").className = "active";