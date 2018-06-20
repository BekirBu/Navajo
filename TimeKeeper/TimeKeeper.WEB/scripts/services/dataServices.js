(function () {
    var app = angular.module("timeKeeper");

    app.factory("dataService", ['$rootScope', '$http', 'timeConfig', 'infoService', function ($rootScope, $http, timeConfig, infoService) {
        var source = timeConfig.apiUrl;

        function setLoader(flag){
            $rootScope.waitForLoad = flag;
        }

        return {
            list: function (dataSet, callback) {
                setLoader(true);
                $http.get(source + dataSet).then(
                    function(response) {
                        setLoader(false);
                        //infoService.success(dataSet, "Data listed!" );
                        return callback(response.data, response.headers);
                    },
                    function(reason) {
                        setLoader(false);
                        console.log(reason.data);
                    });
            },

            read: function (dataSet, id, callback) {
                $http.get(source + dataSet + "/" + id)
                    .then(function success(response) {
                        setLoader(false);
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        console.log(error.data);
                    });
            },

            insert: function (dataSet, data, callback) {
                $http({ method: "post", url: source + dataSet, data: data })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet, "Data successfully inserted!" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        console.log(error.data.message);
                    });
            },

            update: function (dataSet, id, data, callback) {
                $http({ method: "put", url: source + dataSet + "/" + id, data: data })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet, "Data successfully updated!" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        console.log(error.data);
                    });
            },

            delete: function (dataSet, id, callback) {
                $http({ method: "delete", url: source + dataSet + "/" + id })
                    .then(function success(response) {
                        setLoader(false);
                        infoService.success(dataSet, dataSet + " successfully deleted!" );
                        return callback(response.data);
                    }, function error(error) {
                        setLoader(false);
                        console.log(error.data);
                    });
            }
        };
    }]);
}());