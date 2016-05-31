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

var ArticlesModel = function () {
    self = this;
    self.rows = ko.observableArray([]);
    self.provider = ko.observable();

    $.ajax({
        url: apiArticles,
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
            console.log("Error: " + textStatus);
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
if (isNaN(providerId) || providerId === undefined) alert("Please choise provider from main menu!");
var apiArticles = window.location.origin + '/api/Articles/ArticlesFromProvider?newsProviderId=' + providerId;
var apiProvider = window.location.origin + '/api/Subscriptions/GetProviderDetails?providerId=' + providerId;

var viewModel = new ArticlesModel();
$.ajax({
    url: apiProvider,
    method: "GET",
    success: function (data) {
        viewModel.provider(data.Name);
    },
    error: function (request, textStatus) {
        console.log("Error: " + textStatus);
    }
});
ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));