(function () {

    var app = angular.module("timeKeeper");

    app.controller("daysController", ["$scope", "$uibModal", "dataService", "timeConfig",
        function ($scope, $uibModal, dataService, timeConfig) {

            $scope.admin = false;

            $scope.dayType = timeConfig.dayType;
            $scope.months = timeConfig.months;

            //default

            $scope.month = 6;
            $scope.year = 2017;
            $scope.years = [2016, 2017, 2018];
            $scope.empId = currentUser.id;

            if (currentUser.role === "Administrator") {
                $scope.admin = true;
                dataService.list("employees?all", function (data) {
                    $scope.people = data;
                });

                setTimeout(
                    $scope.buildCalendar = function () {
                        if ($scope.empId === undefined) {
                            console.log('You have to choose an employee');
                            $scope.empId = currentUser.id;
                            listCalendar($scope.empId, $scope.year, $scope.month + 1);
                        } else
                            listCalendar($scope.empId, $scope.year, $scope.month + 1);
                    }, 500);


            } else {
                setTimeout(
                    $scope.buildCalendar = function () {
                        console.log('You have to choose an employee')
                        listCalendar($scope.empId, $scope.year, $scope.month + 1);
                    }, 500);
            }

            $scope.$on('calendarUpdated', function (event) {
                listCalendar($scope.empId, $scope.year, $scope.month + 1);
            });

            function listCalendar(empId, year, month) {
                var url = "days/" + empId;
                if (year != 'undefined') url += "/" + year;
                if (month != 'undefined') url += "/" + month;
                dataService.list(url, function (data) {
                    console.log("DATAAAAAAA");
                    console.log(data);
                    $scope.calendar = data;
                    $scope.empId = data.employee;
                    $scope.year = data.year;
                    $scope.month = data.month - 1;

                    $scope.num = function () {
                        var size = new Date(data.days[0].date).getDay() - 1;
                        if (size < 0) size = 6;
                        return new Array(size);
                    }
                });

            };

            $scope.edit = function (day, empId) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: 'views/calendarModal.html',
                    controller: 'ModalCalendarCtrl',
                    size: 'lg',
                    resolve: {
                        day: function () {
                            return day;
                        },
                        empId : function(){
                            return empId;
                        }
                    }
                });
            }
        }
    ]);

    app.controller('ModalCalendarCtrl', ["$uibModalInstance", "$scope", "dataService", "timeConfig", "day", "empId",
        function ($uibModalInstance, $scope, dataService, timeConfig, day, empId) {

            $scope.day = day;
            $scope.dayType = timeConfig.dayDesc;
            $scope.projects = [];

            dataService.read("employees", empId, function (data) {
                $scope.empData = data;
                for(var i = 0; i< $scope.empData.projects.length; i++) {
                    $scope.projects.push($scope.empData.projects[i]);
                }
                console.log("Projekti:");
                console.log($scope.projects);
            });
            initNewTask();

            $scope.add = function (task) {
                $scope.day.details.push(task);
                sumHours();
                initNewTask();
            };

            $scope.upd = function (task, index) {
                sumHours();
            };

            $scope.del = function (index) {
                $scope.day.details[index].deleted = true;
                sumHours();
            };

            function sumHours() {
                $scope.day.hours = 0;
                for (var i = 0; i < $scope.day.details.length; i++) {
                    if (!$scope.day.details[i].deleted) $scope.day.hours += Number($scope.day.details[i].hours);
                }
            }

            function initNewTask() {
                $scope.newTask = {
                    id: 0,
                    description: '',
                    hours: 0,
                    project: {
                        id: 0,
                        name: ''
                    },
                    deleted: false
                };
            }

            $scope.ok = function () {
                $scope.day.employeeId = empId;
                console.log("emp iddddd");
                console.log( $scope.day.employeeId);


                dataService.insert("days", $scope.day, function (data) {
                    $scope.$emit('calendarUpdated');
                });
                $uibModalInstance.close();
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss();
            };

            $scope.typeChanged = function () {
                if ($scope.day.type != 1) {
                    $scope.day.hours = 8;
                }
            }
        }
    ]);
})();