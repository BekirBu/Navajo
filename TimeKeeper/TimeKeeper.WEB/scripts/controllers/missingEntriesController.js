(function () {

    var app = angular.module("timeKeeper");

    app.controller("missingEntriesController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        $scope.months = timeConfig.months;
        $scope.years = [2016, 2017, 2018];

        $scope.year = 2017;
        $scope.month = 1;

        if (currentUser.role === "Administrator") {
            $scope.showReport = true;

            $scope.buildMissingEntries = function () {
                showMissingEntries($scope.year, $scope.month + 1);
            };

            $scope.deleteMissingEntry = function (data) {
                $scope.missingEntriesData = $scope.missingEntriesData.filter(function (el) {
                    return el !== data;
                })
                console.log(data);
                console.log($scope.missingEntriesData);
            }
        }

        function showMissingEntries(year, month) {
            dataService.list("missingEntries/" + year + "/" + month, function (data) {

                $scope.missingEntriesData = data;
                $scope.missingDays = data.missingDays;
                console.log($scope.missingEntriesData);
            });

            $scope.sendMails = function (data) {
                dataService.insert("missingEntries", data, function (data) {
                    $scope.missingEntriesData = data;
                    $scope.missingDays = data.missingDays;
                    console.log($scope.missingEntriesData);

                    $scope.year = 2017;
                    $scope.month +=1;

                    showMissingEntries(year, month);
                    
                });
            }
        };

    }]);

})();