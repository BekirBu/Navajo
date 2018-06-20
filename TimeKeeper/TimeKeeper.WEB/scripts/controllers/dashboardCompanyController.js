(function () {

    var app = angular.module("timeKeeper");

    app.controller("dashboardCompanyController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        $scope.months = timeConfig.months;
        $scope.showCompany = false;

        // default values
        $scope.years = [2016, 2017, 2018];
        $scope.yearDashboard = 2017;
        $scope.monthDashboard = 6;

        if (currentUser.role === "Administrator") {
            $scope.showCompany = true;
        }

        $scope.buildDashboard = function () {
            listCompanyDashboard($scope.yearDashboard, $scope.monthDashboard + 1);
        };

        function listCompanyDashboard(year, month) {
            var currentDate = new Date();

            var url = "reports/company/" + year + "/" + month;

            dataService.list(url, function (data, headers) {

                console.log(url);
                $scope.company = data;
                console.log($scope.company);

                //teams overtime
                var teamNames = new Array();

                for (i = 0; i < $scope.company.overtimeHoursTeams.length; i++) {
                    teamNames.push($scope.company.overtimeHoursTeams[i].teamName);
                }

                var teamOvertimes = new Array();

                for (i = 0; i < $scope.company.overtimeHoursTeams.length; i++) {
                    teamOvertimes.push($scope.company.overtimeHoursTeams[i].overtimeHours);
                }

                $scope.labelsTeamOvertime = teamNames;
                $scope.dataTeamOvertime = teamOvertimes;

                //team missing entries
                var teamMissingEntries = new Array();

                for (i = 0; i < $scope.company.overtimeHoursTeams.length; i++) {
                    teamMissingEntries.push($scope.company.overtimeHoursTeams[i].teamMissingHours);
                }

                $scope.labelsMissingEntries = teamNames;
                $scope.dataMissingEntries = teamMissingEntries;

                //team utilizations
                var teamUtilizations = new Array();

                for (i = 0; i < $scope.company.overtimeHoursTeams.length; i++) {
                    teamUtilizations.push($scope.company.overtimeHoursTeams[i].utilization);
                }

                $scope.labelsUtilizations = teamNames;
                $scope.dataUtilizations = teamUtilizations;

                $scope.optionsUtilizations = {
                    legend: { 
                        display: true,
                        position: 'bottom'
                    }
                }

                //projects revenue
                var projectNames = new Array();

                for (i = 0; i < $scope.company.revenueProjects.length; i++) {
                    projectNames.push($scope.company.revenueProjects[i].projectName);
                }

                var projectRevenue = new Array();
                $scope.sumRevenue = 0;

                for (i = 0; i < $scope.company.revenueProjects.length; i++) {
                    projectRevenue.push($scope.company.revenueProjects[i].revenue);

                    $scope.sumRevenue+=projectRevenue[i];
                }

                console.log($scope.sumRevenue);
                $scope.labelsProjectRevenue = projectNames;
                $scope.dataProjectRevenue = projectRevenue;

                //max revenue
                //$scope.maxRevenue =  Math.max.apply(Math, projectRevenue);
                $scope.maxRevenue = $scope.company.revenueProjects[0].revenue;

                for(i = 1; i < $scope.company.revenueProjects.length; i++) {
                    if($scope.company.revenueProjects[i].revenue > $scope.maxRevenue) {
                            $scope.maxRevenue = $scope.company.revenueProjects[i].revenue;
                            $scope.maxRevenueProject = $scope.company.revenueProjects[i].projectName;
                    }
                }

                console.log("maxreve: ", $scope.maxRevenue);
                console.log($scope.maxRevenueProject);

                //utilizations
                //dev

                var devUtilizationCanvas = document.getElementById("devUtilization");

                var devUtilizationData = {
                    labels: [
                        "Dev Utilization",
                        "Dev Exploit"
                    ],
                    datasets: [{
                        data: [parseFloat($scope.company.devUtilization).toFixed(2), 
                            parseFloat(100 - $scope.company.devUtilization).toFixed(2)],
                        backgroundColor: [
                            "#429E53",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var devUtilizationOptions = {
                    rotation: -Math.PI * 2,
                    cutoutPercentage: 30,
                    circumference: Math.PI * 2,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var devUtilizationChart = new Chart(devUtilizationCanvas, {
                    type: 'doughnut',
                    data: devUtilizationData,
                    options: devUtilizationOptions
                });

                //qa

                var qaUtilizationCanvas = document.getElementById("qaUtilization");

                var qaUtilizationData = {
                    labels: [
                        "QA Utilization",
                        "QA Exploit"
                    ],
                    datasets: [{
                        data: [parseFloat($scope.company.qaUtilization).toFixed(2), 
                            parseFloat(100 - $scope.company.qaUtilization).toFixed(2)],
                        backgroundColor: [
                            "#429E53",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var qaUtilizationOptions = {
                    rotation: -Math.PI * 2,
                    cutoutPercentage: 30,
                    circumference: Math.PI * 2,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var qaUtilizationChart = new Chart(qaUtilizationCanvas, {
                    type: 'doughnut',
                    data: qaUtilizationData,
                    options: qaUtilizationOptions
                });

                //uiux

                var uiuxUtilizationCanvas = document.getElementById("uiuxUtilization");

                var uiuxUtilizationData = {
                    labels: [
                        "UI/UX Utilization",
                        "UI/UX Exploit"
                    ],
                    datasets: [{
                        data: [parseFloat($scope.company.uiuxUtilization).toFixed(2), 
                                parseFloat(100 - $scope.company.uiuxUtilization).toFixed(2)],
                        backgroundColor: [
                            "#429E53",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var uiuxUtilizationOptions = {
                    rotation: -Math.PI * 2,
                    cutoutPercentage: 30,
                    circumference: Math.PI * 2,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var uiuxUtilizationChart = new Chart(uiuxUtilizationCanvas, {
                    type: 'doughnut',
                    data: uiuxUtilizationData,
                    options: uiuxUtilizationOptions
                });

                //pm

                var pmUtilizationCanvas = document.getElementById("pmUtilization");

                var pmUtilizationData = {
                    labels: [
                        "PM Utilization",
                        "PM Exploit"
                    ],
                    datasets: [{
                        data: [parseFloat($scope.company.pmUtilization).toFixed(2), 
                                parseFloat(100 - $scope.company.pmUtilization).toFixed(2)],
                        backgroundColor: [
                            "#429E53",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var pmUtilizationOptions = {
                    rotation: -Math.PI * 2,
                    cutoutPercentage: 30,
                    circumference: Math.PI * 2,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var pmUtilizationChart = new Chart(pmUtilizationCanvas, {
                    type: 'doughnut',
                    data: pmUtilizationData,
                    options: pmUtilizationOptions
                });
            });
        };

        $scope.chartIds = ["overtimeTeamsChart", "projectRevenueChart", "devUtilization", "qaUtilization", "uiuxUtilization", "pmUtilization"]
    }]);

})();