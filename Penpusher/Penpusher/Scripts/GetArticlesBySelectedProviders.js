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



        var ArticlesModel = function (userId) {
            self = this;
            self.articles = ko.observableArray([]);
            $.ajax({
                url: apiController + userId,
                method: "GET",
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        self.articles.push(
                            data[i]
                        );
                    }
                },
                error: function (request, textStatus) {
                    alert("Error: " + textStatus);
                }
            });
        }
        var apiController = window.location.origin + "/api/Articles/ArticlesFromSelectedProviders?userId=";
        var userId = 5;
        var viewModel = new ArticlesModel(userId);
        ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
        document.getElementById("UnreadNews").className = "active";
    });