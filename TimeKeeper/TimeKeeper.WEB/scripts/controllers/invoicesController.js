(function () {

    var app = angular.module("timeKeeper");

    app.controller("invoicesController", ["$scope", "dataService", "timeConfig", function ($scope, dataService, timeConfig) {

        $scope.months = timeConfig.months;
        $scope.years = [2016, 2017, 2018];

        $scope.year = 2017;
        $scope.month = 6;

        $scope.invoiceDate = new Date(2018,3,20);

        if (currentUser.role === "Administrator") {
            $scope.showInvoice = true;

            $scope.buildInvoices = function () {
                showInvoices($scope.year, $scope.month + 1);
            };

            $scope.deleteRole = function (data) {
                $scope.invoicesRoles.roles = $scope.invoicesRoles.roles.filter(function (el) {
                    return el !== data;
                })
            }
        }

        function showInvoices(year, month) {
            dataService.list("invoices/" + year + "/" + month, function (data) {
                $scope.invoicesData = data;
                console.log($scope.invoicesData);
            });

            $scope.viewInvoice = function(data) {
                $scope.invoicesRoles = data;
                console.log(data);

                $scope.totalAmount = 0;

                for(i=0; i<$scope.invoicesRoles.roles.length; i++) {
                    $scope.totalAmount += $scope.invoicesRoles.roles[i].subotal;
                }

                console.log($scope.totalAmount);
            }

            $scope.sendMails = function (data) {
                dataService.insert("invoices", data, function (data) {
                    $scope.invoicesRoles = data;
                });
            }
        };

    }]);

})();