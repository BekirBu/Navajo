(function () {

    var app = angular.module("timeKeeper");

    app.controller("dashboardController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        $scope.message = "Wait...";
        $scope.months = timeConfig.months;

        // default values
        $scope.years = [2016, 2017, 2018];
        $scope.yearDashboard = 2017;
        $scope.monthDashboard = 6;

        dataService.list("employees/" + currentUser.id , function (data, headers) {
            $scope.employee = data;
        });

        $scope.buildDashboard = function () {
            listPersonalDashboard($scope.yearDashboard, $scope.monthDashboard + 1);
        };

        function listPersonalDashboard(year, month) {

            var currentDate = new Date();
            year1 = currentDate.getFullYear();
            month1 = currentDate.getMonth() + 1; //zero based

            var url = "reports/personal/" + currentUser.id;
            if (year != undefined) {
                url += "/" + year;
            } else if (year == undefined) {
                url += "/" + year1;
            }

            if (isNaN(month) == false) {
                url += "/" + month;
            } else if (isNaN(month) == true) {
                url += "/" + month1;
            }

            dataService.list(url, function (data, headers) {
                console.log(url);
                $scope.personal = data;
                console.log($scope.personal);

                //working smiley
                if($scope.personal.workingDays <= $scope.personal.workingDaysInMonth
                         && $scope.personal.workingDays >= $scope.personal.workingDaysInMonth - 4) {
                    $scope.workingNeutral= false;
                    $scope.workingBad = false;
                    $scope.workingGood = true;
                }
                else if ($scope.personal.workingDays < $scope.personal.workingDaysInMonth - 4 
                    && $scope.personal.workingDays >= $scope.personal.workingDaysInMonth - 8) {
                        $scope.workingGood = false;
                        $scope.workingBad = false;
                        $scope.workingNeutral= true;
                }
                else {
                    $scope.workingNeutral= false;
                    $scope.workingGood = false
                    $scope.workingBad = true;;
                }

                //Trial,Active,Leaver
                if($scope.personal.employee.statusEmployee == 0) {
                    $scope.statusEmp = "Trial";
                    $scope.statusBad = false;
                    $scope.statusGood = false;
                    $scope.statusNeutral= true;
                }
                else if ($scope.personal.employee.statusEmployee == 1) {
                    $scope.statusEmp = "Active";
                    $scope.statusBad = false;
                    $scope.statusNeutral= false;
                    $scope.statusGood = true;
                }
                else if ($scope.personal.employee.statusEmployee == 2) {
                    $scope.statusEmp = "Leaver";
                    $scope.statusNeutral= false;
                    $scope.statusGood = false
                    $scope.statusBad = true;;
                }

                //days chart
                var wDays = $scope.personal.workingDays;
                var phDays = $scope.personal.publicHolidayDay;
                var rDays = $scope.personal.religiousDays;
                var slDays = $scope.personal.sickLeavesDays;
                var vDays = $scope.personal.vacationDays;
                var oDays = $scope.personal.otherDays;
                var baDays = $scope.personal.businessAbscenceDays;

                $scope.labelsDays= ["Working days", "Public holiday days", "Religious days", "Sick leaves days", "Vacation days", "Business abscence days", "Other days"];
                $scope.dataDays = [wDays, phDays, rDays, slDays, vDays, baDays, oDays];

                //pto
                $scope.ptoEmp = (phDays + rDays + slDays + vDays + baDays) * 8;
                console.log($scope.ptoEmp);

                //utilization
                var utilizationCanvas = document.getElementById("utilizationChart");

                Chart.defaults.global.defaultFontFamily = "Lato";
                Chart.defaults.global.defaultFontSize = 18;

                var utilizationData = {
                    labels: [
                        "Utilization",
                        "Exploit"
                    ],
                    datasets: [{
                        data: [$scope.personal.utilization, 100 - $scope.personal.utilization],
                        backgroundColor: [
                            "#EE9143",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var chartOptions = {
                    rotation: -Math.PI ,
                    cutoutPercentage: 30,
                    circumference: Math.PI,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var utilizationChart = new Chart(utilizationCanvas, {
                    type: 'doughnut',
                    data: utilizationData,
                    options: chartOptions
                });

                //Bradford factor
                var bfCanvas = document.getElementById("bfChart");

                var bf = ($scope.personal.missingEntries * $scope.personal.missingEntries) * $scope.personal.workingDaysInMonth;
                var worstCaseScenario_bf = Math.pow($scope.personal.workingDaysInMonth, 3);
                console.log(bf);
                console.log(worstCaseScenario_bf);

                var bfData = {
                    labels: [
                        "Score",
                        "Max score"
                    ],
                    datasets: [{
                        data: [worstCaseScenario_bf - bf, bf],
                        backgroundColor: [
                            "#429E53",
                            "grey"
                        ],
                        borderColor: "white",
                        borderWidth: 2
                    }]
                };

                var bfOptions = {
                    rotation: -Math.PI ,
                    cutoutPercentage: 30,
                    circumference: Math.PI,
                    legend: {
                        display: true,
                        position: 'right'
                    },
                    animation: {
                        animateRotate: true,
                        animateScale: true
                    }
                };

                var bfChart = new Chart(bfCanvas, {
                    type: 'doughnut',
                    data: bfData,
                    options: bfOptions
                });

                //verzija 2 total hours
                $scope.labelsMissingHours = ["Hours working", "Hours not working"];
                $scope.dataMissingHours = [$scope.personal.totalHours, ($scope.personal.workingDaysInMonth) * 8 - $scope.personal.totalHours];

            });
        };

    }]);

})();