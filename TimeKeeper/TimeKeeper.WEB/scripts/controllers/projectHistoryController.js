(function () {

    var app = angular.module("timeKeeper");

    app.controller("projectHistoryController", ["$scope", "dataService", function ($scope, dataService) {

        if (currentUser.role === "Administrator") {
            $scope.showReport = true;
        }

        $scope.message = "Wait...";
        dataService.list("projects?all", function (data) {
            $scope.projects = data;
            console.log(data);
            $scope.projectId = data[7].id;
        });

        setTimeout(
            $scope.buildReport = function () {
                listProjectHistory($scope.projectId);
            }, 500);


        function listProjectHistory(projectId) {
            dataService.list("reports/history/" + projectId, function (data, headers) {
                $scope.history = data;
                console.log($scope.history);

            });
        };

    }]);

})();