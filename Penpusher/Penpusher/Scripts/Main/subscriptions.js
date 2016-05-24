    var NewsProviderModel = function () {
        var self = this;
        self.newsproviders = ko.observableArray([]);
        self.AddLink = ko.observable("example");
        $.getJSON('/api/getallsubscription/4', function (data) {

            self.newsproviders(data);

        });

        self.addNewsProvider = function (newsprovider) {
            var link = newsprovider.AddLink;
            console.log({ link });
            var newsProvider = {};
            newsProvider.Link = link;
            $.ajax({
                url: '/api/add',
                type: "POST",
                //contentType: 'application/json; charset=utf-8',
                data: newsProvider,
                success: function () {
                    //self.newsproviders(data);
                    //console.log(data);
                    //alert('data.result');
                    //console.log(data.result);
                    $.getJSON('/api/getallsubscription/4', function (data) {

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
                    //console.log(data);
                    //alert('data.result');
                    //console.log(data.result);
                }
            });
            self.newsproviders.remove(newsprovider);
        };

    };


    ko.applyBindings(new NewsProviderModel, document.getElementById('subscription'));

// Activate jQuery Validation
//$("form").validate({
//  submitHandler: PeopleModel.save
//});