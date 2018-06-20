(function () {

    var app = angular.module("timeKeeper");

    app.controller("loginController", ["$scope", "$rootScope", "$http", "timeConfig", "$location",
        function ($scope, $rootScope, $http, timeConfig, $location) {

            $rootScope.currentUser = currentUser;

            startApp("loginBtn");

            function startApp(actionButton) {
                gapi.load("auth2", function () {
                    auth2 = gapi.auth2.init({
                        client_id: "75845171096-ld6mp0bkqqtc4a3nrabedug197dfou5t.apps.googleusercontent.com"
                    });
                    attachSignin(document.getElementById(actionButton));
                });
            };

            function attachSignin(element) {
                auth2.attachClickHandler(element, {}, function (googleUser) {

                    var authToken = googleUser.getAuthResponse().id_token;
                    $http.defaults.headers.common.Authorization = "Bearer " + authToken;
                    $http.defaults.headers.common.Provider = "google";
                    $http({
                            method: "post",
                            url: timeConfig.apiUrl + 'login'
                        })
                        .then(function (response) {
                            currentUser = response.data;
                            $rootScope.currentUser = currentUser;
                            $location.path("/dashboard");
                            console.log(currentUser);
                        }, function (error) {
                            console.log(error.message);
                        });
                })
            };

            $scope.login = function () {
                var userData = {
                    grant_type: 'password',
                    username: $scope.user.name,
                    password: $scope.user.pass,
                    scope: 'openid'
                };
                var urlEncodedUrl = {
                    'Content-Type': 'application/x-www-form-urlencoded',
                    'Authorization': 'Basic dGltZWtlZXBlcjokY2gwMGw='
                };

                $http({
                    method: 'POST',
                    url: timeConfig.idsUrl,
                    headers: urlEncodedUrl,
                    data: userData,
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    }
                }).success(function (data, status, headers, config) {
                    authToken = data.access_token;
                    $http.defaults.headers.common.Authorization = 'Bearer ' + authToken;
                    $http.defaults.headers.common.Provider = "iserver";
                    $http({
                        method: 'GET',
                        url: timeConfig.apiUrl + 'login'
                    }).success(function (data, status, headers, config) {
                        currentUser = data;
                        console.log(currentUser);
                        $rootScope.currentUser = currentUser;
                        $location.path("/dashboard");
                    });
                }).error(function (data, status, headers, config) {
                    console.log('ERROR: ' + status);
                });
            };
        }
    ]);

    app.controller("logoutController", ["$rootScope", "$location", function ($rootScope, $location) {
        currentUser = {
            id: 0
        };
        $rootScope.currentUser = currentUser;
        window.location.reload();
        $location.path("/login");
        console.log(currentUser);
    }]);
}());