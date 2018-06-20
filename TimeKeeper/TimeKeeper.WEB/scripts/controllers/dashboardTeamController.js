(function () {

    var app = angular.module("timeKeeper");

    app.controller("dashboardTeamController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        $scope.message = "Wait...";
        $scope.months = timeConfig.months;
        $scope.showTeam = false;

        // default values

        if (currentUser.role === "Administrator") {
            $scope.teamLeadToTeams = new Array();

            dataService.list("teams", function (data) {
                $scope.teams = data;

                for (i = 0; i < $scope.teams.length; i++) {
                    $scope.teamLeadToTeams.push($scope.teams[i].id);
                    // $scope.teamId = $scope.teamLeadToTeams[0];


                    // $scope.teamId = $scope.teamLeadToTeams[1];
                    // console.log($scope.teamLeadToTeams[1]);
                    // if ($scope.teamId !== "O") {
                    //     $scope.teamId == $scope.teamLeadToTeams[1];
                    // }
                }
            });
        }

        $scope.teamLeadToTeams = new Array();
        for (i = 0; i < currentUser.teamLeadTo.length; i++) {
            $scope.teamLeadToTeams.push(currentUser.teamLeadTo[i]);
            $scope.teamId = $scope.teamLeadToTeams[5];
            console.log($scope.teamLeadToTeams[5]);
            if ($scope.teamId !== "O") {
                $scope.teamId = $scope.teamLeadToTeams[0];
            }
            console.log('teamid : ' + $scope.teamId);
        }

        if (currentUser.role === "Administrator" || $scope.teamLeadToTeams.length !== 0) {
            $scope.showTeam = true;
        }

        $scope.years = [2016, 2017, 2018];
        $scope.yearDashboard = 2017;
        $scope.monthDashboard = 7;
        $scope.teamId = "Y";

        //setTimeout(
            $scope.buildDashboard = function () {
                listTeamDashboard($scope.yearDashboard, $scope.teamId, $scope.monthDashboard + 1);
            }
            //}, 500);

        function listTeamDashboard(year, teamId, month) {
            var url = "reports/team/" + teamId + "/" + year + "/" + month;

            dataService.list(url, function (data, headers) {
                $scope.team = data;
                console.log($scope.team);

                $scope.teamName = $scope.team.name;
                console.log($scope.teamName);

                //overtime by employee
                var employeesInTeams = new Array();

                for (i = 0; i < $scope.team.reports.length; i++) {
                    employeesInTeams.push($scope.team.reports[i].employee.name);
                    //console.log(employeesInTeams[i]);
                }

                var employeesOvertimeInTeams = new Array();

                for (i = 0; i < $scope.team.reports.length; i++) {
                    employeesOvertimeInTeams.push($scope.team.reports[i].days.overtimeHours);
                }

                $scope.labelsOvertimeInTeams = employeesInTeams;
                $scope.dataOvertimeInTeams = employeesOvertimeInTeams;

                //missing entries by employee
                var employeesMissingEntriesInTeams = new Array();

                for (i = 0; i < $scope.team.reports.length; i++) {
                    employeesMissingEntriesInTeams.push($scope.team.reports[i].days.missingEntries);
                }

                $scope.labelsMissingEntriesInTeams = employeesInTeams;
                $scope.dataMissingEntriesInTeams = employeesMissingEntriesInTeams;

                //utilization employees
                $scope.labelsUtilization = [
                    "Utilization",
                    "Exploit"
                ];
                $scope.dataUtilization = [$scope.team.days.percentageOfWorkingDays, 
                                            100 - $scope.team.days.percentageOfWorkingDays];

                $scope.optionsUtilizations = {
                    legend: { 
                        display: false,
                        position: 'bottom'
                    }
                }
            });
        };

    }]);

})();