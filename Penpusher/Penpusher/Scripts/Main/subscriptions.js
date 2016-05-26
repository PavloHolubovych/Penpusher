    var NewsProviderModel = function () {
        var self = this;
        var userId = 5;
        self.newsproviders = ko.observableArray([]);
        self.availableChannels = ko.observableArray([]);
        self.AddLink = ko.observable("liga.net");

        $.getJSON('/api/getallsubscription/' + userId, function (channel) {

            self.newsproviders(channel);
        });

        $.getJSON('/api/getall', function (channel) {

            self.availableChannels(channel);
        });
        
        self.addNewsProvider = function (newsprovider) {
            $.ajax({
                url: '/api/add',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsprovider,
                success: function () {
                    $.getJSON('/api/getallsubscription/' + userId, function (data) {
                        self.newsproviders(data);
                    });
                }
            });
        };

        self.removeNewsProvider = function (newsprovider) {
            var id = newsprovider.Id;

            $.ajax({
                url: '/api/delete/'+id,
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