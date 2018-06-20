(function () {

    var app = angular.module("timeKeeper");

    app.controller("monthlyReportController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        if (currentUser.role === "Administrator") {
            $scope.showReport = true;
        }

        $scope.message = "Wait...";
        $scope.months = timeConfig.months;

        // default
        $scope.years = [2016, 2017, 2018];
        $scope.year = 2017;
        $scope.month = 6;

        dataService.list("projects", function (data) {
            $scope.projects = data;

            var projectsArray = new Array();
            projectsArray.push("Employee");
            //projectsArray.push("Total Hours");

            for (i = 0; i < $scope.projects.length; i++) {
                projectsArray.push($scope.projects[i].name);
                //console.log(projectsArray[i]);
            }
            $scope.proArray = projectsArray;
        });

        setTimeout(
            $scope.buildReport = function () {
                listMonthlyReport($scope.year, $scope.month + 1);
            }, 500);

        function listMonthlyReport(year, month) {
            dataService.list("reports/monthly/" + year + "/" + month, function (data, headers) {
                console.log(year);
                console.log(month);
                $scope.monthly = data;
                console.log($scope.monthly);

                var listEmp = [];

                for (i = 0; i < $scope.monthly.length; i++) {

                    var empArray = {};
                    empArray.empName = $scope.monthly[i].employee.name;
                    empArray.totalHours = $scope.monthly[i].totalWorkingHours;
                    empArray.proName = [];

                    for (j = 1; j < $scope.proArray.length; j++) {
                        empArray.proName[j] = "0";
                    }

                    for (k = 0; k < $scope.monthly[i].projects.length; k++) {
                        for (j = 1; j < $scope.proArray.length; j++) {
                            if ($scope.monthly[i].projects[k].name === $scope.proArray[j]) {
                                //console.log($scope.monthly[i].projects[k].hours);
                                empArray.proName[j] = $scope.monthly[i].projects[k].hours;
                                //$scope.proArray[j];
                            }
                        }
                    }

                    //console.log(empArray);
                    listEmp.push(empArray);
                }

                $scope.empList = listEmp;
            });
        };

    }]);

})();