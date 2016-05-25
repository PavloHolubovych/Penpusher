var ProviderViewModel = function (provider) {
    var self = this;

    self.provider = ko.observable(provider);
    console.log(provider.Name);

    self.subscribe = function(newsprovider) {
        var link = newsprovider.AddLink;
        console.log({ link });
        var newsProvider = {};
        newsProvider.Link = link;
        $.ajax({
            url: '/api/add',
            type: "POST",
            data: newsProvider,
            success: function() {
                console.log("+");
            },
            error: function(request, textStatus) {
                alert("Error: " + textStatus);
            }
        });
    };
};

$(document)
    .ready(function () {
        var provider;
        $.getJSON('/api/Subscriptions/GetProviderDetails?providerId=' + localStorage.providerId, function(data) {
            provider = data;
        });


        ko.applyBindings(new ProviderViewModel(provider), document.getElementById('providerDescriptionContainer'));
    });