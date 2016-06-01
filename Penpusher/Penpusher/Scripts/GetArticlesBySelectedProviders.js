var pageNumber = 1;
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
var openDetails= function(Id) {
    location.href = '/Main/ArticleContentDetails?articleId=' + Id;
}
var apiController = window.location.origin + "/api/Articles/ArticlesFromSelectedProviders?pageNumber=" + pageNumber;
var viewModel = new ArticlesModel();
ko.applyBindings(viewModel, document.getElementById("articlesSubscriptions"));
document.getElementById("UnreadNews").className = "active";




addToReadLater = function (articleId) {
    var element = $(event.target);

    var parentEl = element.parent();
    $.post("/api/Articles/ToReadLater", { "articleId": articleId, "flag": true }).done(function (data) {
        element.text("In read later");
        element.addClass("disabled");
    })
        .error(function (request, textStatus) {
            alert("Error: " + textStatus);
        });
};
addToFavorite = function (articleId, add) {
    var element = $(event.target);

    var parentEl = element.parent();
    $.post("/api/Articles/AddRemoveFavorites", { "articleId": articleId, "flag": add }).done(function(data) {
        if (add) {
            $(parentEl.find("button")[2]).hide();
            $(parentEl.find("button")[3]).show();
        } else {
            $(parentEl.find("button")[3]).hide();
            $(parentEl.find("button")[2]).show();
        }
        element.addClass("disabled");
    });
};