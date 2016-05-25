var ProviderViewModel = function () {
    var self = this;

    self.provider = $.getJSON('/api/GetProviderDetails/' + providerId);

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
        ko.applyBindings(new ProviderViewModel(provider), document.getElementById('providerDescriptionContainer'));
    });