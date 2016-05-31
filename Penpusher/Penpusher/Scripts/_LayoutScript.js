var userId = 5;
var providersModel = function () {
    var self = this;
    self.providers = ko.observableArray();
    self.selectedProvider = ko.observable();

    $.get("/api/Subscriptions/GetByUser/" + userId,
        function (data, status) {
            for (var i = 0; i < data.length; i++) {
                self.providers.push(data[i]);
            }
        });
    var hrefdata = document.getElementsByClassName('hrefsub');
    document.getElementById('subscriptions').className = "nav nav-second-level collapse";
    DeleteActiveClass(hrefdata);
};
var viewModel = new providersModel();
ko.applyBindings(viewModel, document.getElementById("subscriptions"));

var generalMenu = document.getElementById("side-menu").children;
//DeleteActiveClass(generalMenu);
generalMenu[0].children[0].className = "";

function DeleteActiveClass(data) {
    for (var li in data) {
        if (data.hasOwnProperty(li)) {
            data[li].className = "";
        }
    }
}

function ManageContentPage() {
    location.href = "/Main/Subscriptions";
}

function LoadReadPage() {
    location.href = "/Main/UserReadArticles";
}

function LoadReadLaterPage() {
    location.href = "/Main/UserToReadLaterArticles";
}

function LoadFavoritePage() {
    location.href = "/Main/UserFavoriteArticles";
}

function LoadSubscriptionsPage() {
    location.href = "/Main/Subscriptions";
}
function LoadMainPage() {
    location.href = "/";
}