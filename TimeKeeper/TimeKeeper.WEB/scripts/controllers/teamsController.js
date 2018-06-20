(function () {

    var app = angular.module("timeKeeper");

    app.controller("teamsController", ["$scope", "dataService", "$uibModal", function ($scope, dataService, $uibModal) {

        if (currentUser.role === "Administrator") {
            $scope.showTeams = true;
        }

        dataService.list("employees", function (data) {
            $scope.employees = data;
        });

        $scope.message = "Wait...";
        dataService.list("teams", function (data) {
            $scope.message = "";
            $scope.teams = data;
        });

        $scope.edit = function (team) {
            $scope.team = team;
        };

        $scope.save = function (team) {
            console.log(team);
            dataService.insert("teams", team, function (data) {
                $scope.$emit('teamDeleted');
            });
        };

        $scope.setNull = function () {
            $scope.team = null;
        };

        $scope.update = function (team) {
            dataService.update("teams", team.id, team, function (data) {
                $scope.$emit('teamDeleted');
            });

        };

        $scope.addMember = function (data) {
            console.log(data);
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'views/addTeamMember.html',
                controller: 'engagementController',
                controllerAs: "$team",
                closeOnCancel: true,
                resolve: {
                    team: function () {
                        return data;
                    }
                }
            }).closed.then(function () {
                $scope.$emit("teamDeleted")
            });
        };

        $scope.remove = function (engagement) {
            dataService.delete("engagements", engagement.id, function (data) {
                $scope.$emit('teamDeleted');
            });

        };

        $scope.$on('teamDeleted', function (event) {
            dataService.list("teams", function (data) {
                $scope.message = "";
                $scope.teams = data;
            });
        });

        $scope.deleteTest = function (team) {
            swal({
                    title: team.name,
                    text: "Are you sure you want to delete this team?",
                    type: "warning",
                    //imageUrl: 'images/hhasic.jpg',
                    //imageSize: '240x100',
                    showCancelButton: true,
                    customClass: "sweetClass",
                    confirmButtonColor: "teal",
                    confirmButtonText: "Yes.",
                    cancelButtonColor: "darkred",
                    cancelButtonText: "No!",
                    closeOnConfirm: false,
                    closeOnCancel: true,

                },
                function (isConfirm) {
                    if (isConfirm) {
                        dataService.delete("teams", team.id, function (data) {
                            $scope.$emit('teamDeleted');
                        })
                        swal.close();
                    }
                });
        }

    }]);

    app.controller("engagementController", ["$scope", "dataService", "timeConfig", "$location", "infoService", "team", "$uibModalInstance",
        function ($scope, dataService, timeConfig, $location, infoService, team, $uibModalInstance) {


            $scope.message = "Wait...";
            $scope.empPagination = false;

            $scope.$on('deleted', function (event) {
                dataService.list("teams", function (data) {
                    $scope.message = "";
                    $scope.teams = data;
                });
            });


            var source = timeConfig.apiUrl;
            var endpoint = "engagements";

            dataService.list("employees?all", function (data) {
                $scope.employees = data;
                console.log($scope.employees);
            });

            dataService.list("roles", function (data) {
                $scope.roles = data;
            });

            dataService.list("teams", function (data) {
                $scope.teams = data;
            });


            $scope.saveMember = function (engagement) {
                console.log(engagement);
                var newMember = {
                    role: {
                        Id: engagement.role.id
                    },
                    team: {
                        Id: team.id
                    },
                    employee: {
                        Id: engagement.employee.id
                    },
                    hours: 20
                };

                console.log(newMember);

                dataService.insert("engagements", newMember, function (data) {
                    $scope.$emit("deleted");
                });

            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss();
                $scope.$emit("deleted");
            };
        }
    ]);
})();