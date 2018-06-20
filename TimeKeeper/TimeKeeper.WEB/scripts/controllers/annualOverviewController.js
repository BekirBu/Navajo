(function () {

    var app = angular.module("timeKeeper");

    app.controller("annualOverviewController", ["$scope", "dataService", function ($scope, dataService) {

        // default
        $scope.years = [2016, 2017, 2018];
        $scope.year = 2017;

        if (currentUser.role === "Administrator") {
            $scope.showReport = true;
        }

        $scope.message = "Wait...";

        $scope.buildReport = function () {
            listAnnualOverview($scope.year);

        };

        function listAnnualOverview(year) {
            dataService.list("reports/annual/" + year, function (data, headers) {
                $scope.annual = data;
                console.log($scope.annual);


            });
        };

    }]);

})();