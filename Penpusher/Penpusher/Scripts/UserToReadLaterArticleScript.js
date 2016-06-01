$('.addbutton').hide();
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
            if (!data.length) $('#alertMessage').show();
            bindD(data);
        },
        error: function (request, textStatus) {
            alert("Error: " + textStatus);
        }
    });
    function bindD(data) {
        var lenghtRow = 3;
        for (var i = 0; i <= data.length; i++) {
            if (i % lenghtRow === 0) {
                self.rows.push(data.slice(i - lenghtRow, i));
            }
            if (i === data.length - 1 && data.length % lenghtRow !== 0) {
                self.rows.push(data.slice(data.length - data.length % lenghtRow, data.length));
            }
        }
    }
}

var openDetails = function (Id) {
    location.href = '/Main/ArticleContentDetails?articleId=' + Id;
}
var apiController = window.location.origin + "/api/Articles/GetReadLeaterArticles";
var viewModel = new ArticlesModel1();
ko.applyBindings(viewModel, document.getElementById("readArticlesContainer"));
document.getElementById("ReadLaterPage").className = "active";