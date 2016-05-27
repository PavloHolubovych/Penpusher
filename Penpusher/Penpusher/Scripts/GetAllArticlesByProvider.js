//$(document)
//    .ready(function () {
//        var ArticlesModel = function (providerId) {
//            self = this;
//            self.articles = ko.observableArray([]);
//            $.ajax({
//                url: apiController + providerId,
//                method: "GET",
//                beforeSend: function () {
//                    $('.articlesbySubscription').hide();
//                },
//                success: function (data) {
//                    $('.articlesbySubscription').show();
//                    $('.loadimage').hide();
//                    for (var i = 0; i < data.length; i++) {
//                        self.articles.push(
//                            data[i]
//                        );
//                    }
//                },
//                error: function(request, textStatus) {
//                    alert("Error: " + textStatus);
//                }
//            });
//        }
//        var apiController = window.location.origin + "/api/Articles/ArticlesFromProvider?newsProviderId=";
//        var providerId = parseInt(QueryString.providerID);
//        if (providerId === NaN) return;
//        var viewModel = new ArticlesModel(providerId);
//        ko.applyBindings(viewModel, document.getElementById("articlesSubscription"));
//    });

$(document)
    .ready(function () {
        ko.bindingHandlers.trimLengthText = {};
        ko.bindingHandlers.trimText = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var trimmedText = ko.computed(function () {
                    var untrimmedText = ko.utils.unwrapObservable(valueAccessor());
                    var defaultMaxLength = 20;
                    var minLength = 5;
                    var maxLength = ko.utils.unwrapObservable(allBindingsAccessor().trimTextLength) || defaultMaxLength;
                    if (maxLength < minLength) maxLength = minLength;
                    var text = untrimmedText.length > maxLength ? untrimmedText.substring(0, maxLength - 1) + '...' : untrimmedText;
                    return text;
                });
                ko.applyBindingsToNode(element, {
                    text: trimmedText
                }, viewModel);

                return {
                    controlsDescendantBindings: true
                };
            }
        };

        var ArticlesModel1 = function () {
            self = this;
            self.rows = ko.observableArray([]);
            $.ajax({
                url: apiController,
                method: "GET",
                beforeSend: function () {
                    $('.articles').hide();
                },
                success: function (data) {
                    $('.articles').show();
                    $('.loadimage').hide();
                    bindD(data);
                },
                error: function (request, textStatus) {
                    alert("Error: " + textStatus);
                }
            });
            function bindD(data) {
                var lenghtRow = 3;
                for (var i = 1; i <= data.length; i++) {
                    if (i % lenghtRow === 0) {
                        self.rows.push(data.slice(i - lenghtRow, i));
                    }
                    if (i === data.length - 1 && data.length % lenghtRow !== 0) {
                        self.rows.push(data.slice(data.length - data.length % lenghtRow, data.length));
                    }
                }
            }
        }
        var providerId = parseInt(QueryString.providerID);
        if (providerId === NaN) return;
        var apiController = window.location.origin + '/api/Articles/ArticlesFromProvider?newsProviderId=' + providerId;

        var viewModel = new ArticlesModel1();
        ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
        //document.getElementById("FavoritePage").className = "active";
    });