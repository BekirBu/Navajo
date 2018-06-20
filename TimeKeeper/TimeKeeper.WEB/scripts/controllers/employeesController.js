(function () {

    var app = angular.module("timeKeeper");

    app.controller("employeesController", ["$scope", "dataService", function ($scope, dataService) {

        if (currentUser.role === "Administrator") {
            $scope.showEmployees = true;
        }

        dataService.list("roles", function (data) {
            $scope.roles = data;
        });

        $scope.message = "Wait...";
        dataService.list("employees", function (data, headers) {
            console.log(headers('Pagination'));
            $scope.page = JSON.parse(headers('Pagination'));
            console.log($scope.page);
            $scope.message = "";
            $scope.people = data;
        });

        //sorting
        $scope.column = 'firstName';
        $scope.reverse = false;

        $scope.sortColumn = function (col) {
            $scope.column = col;
            if ($scope.reverse) {
                $scope.reverse = false;
                $scope.reverseclass = 'arrow-up';
            } else {
                $scope.reverse = true;
                $scope.reverseclass = 'arrow-down';
            }
        };

        $scope.sortClass = function (col) {
            if ($scope.column == col) {
                if ($scope.reverse) {
                    return 'arrow-down';
                } else {
                    return 'arrow-up';
                }
            } else {
                return '';
            }
        }

        //paging
        $scope.next = function (page) {

            dataService.list("employees?page=" + page.nextPage, function (data, headers) {
                $scope.people = data;
                $scope.page = JSON.parse(headers('Pagination'));
                console.log($scope.page);
            })
        };

        $scope.previous = function (page) {
            dataService.list("employees?page=" + page.prevPage, function (data, headers) {
                $scope.people = data;
                $scope.page = JSON.parse(headers('Pagination'));
            })
        };

        $scope.filter = function (filter) {
            if (filter != "") {
                dataService.list("employees?filter=" + filter, function (data, headers) {
                    $scope.people = data;
                })
            } else if (filter == "") {
                dataService.list("employees", function (data, headers) {
                    $scope.people = data;
                })
            }
        };

        $scope.edit = function (person) {
            $scope.person = person;
        };

        $scope.save = function (person) {
            console.log(person);
            if (person.id === undefined) {
                person.image = ($scope.image) ? $scope.image.base64 : "";
                dataService.insert("employees", person, function (data) {
                    $scope.$emit('employeeDeleted');
                });
            } else {
                person.image = ($scope.image) ? $scope.image.base64 : "";
                dataService.update("employees", person.id, person, function (data) {
                    $scope.$emit('employeeDeleted');
                });
            }
        };

        $scope.setNull = function () {
            $scope.person = null;
        };

        $scope.$on('employeeDeleted', function (event) {
            dataService.list("employees", function (data, headers) {
                $scope.page = JSON.parse(headers('Pagination'));
                console.log(127827612);
                $scope.message = "";
                $scope.people = data;
            });
        });

        $scope.deleteTest = function (person) {
            swal({
                    title: person.firstName + " " + person.lastName,
                    text: "Are you sure you want to delete this employee?",
                    type: "warning",
                    //imageUrl: 'images/hhasic.jpg',
                    //imageSize: '240x100',
                    showCancelButton: true,
                    customClass: "sweetClass",
                    confirmButtonColor: "teal",
                    confirmButtonText: "Yes, sure",
                    cancelButtonColor: "darkred",
                    cancelButtonText: "No, not ever!",
                    closeOnConfirm: false,
                    closeOnCancel: true,
                },
                function (isConfirm) {
                    if (isConfirm) {
                        dataService.delete("employees", person.id, function (data) {
                            $scope.$emit('employeeDeleted');
                        })
                        swal.close();
                    }
                });
        };

    }]);


})();