(function(){

    var app = angular.module("timeKeeper");

    app.controller("customersController", ["$scope", "dataService", "infoService",  function($scope, dataService, infoService) {

        if(currentUser.role === "Administrator") {
            $scope.showCustomers = true;
        }

        $scope.message = "Wait...";
        dataService.list("customers", function(data){
            $scope.message = "";
            $scope.customers = data;
        });

        $scope.$on('customerDeleted', function(event) {
            dataService.list("customers", function(data, headers){
                $scope.page = JSON.parse(headers('Pagination'));
                $scope.message = "";
                $scope.customers = data;
            });
        });
    }]);

    app.controller("custController", ["$scope", "$uibModal", "dataService", function($scope, $uibModal, dataService) {

        var $cust = this;
        $scope.edit = function (data) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'views/custModal.html',
                controller: 'ModalCtrl',
                controllerAs: '$cust',
                resolve: {
                    customer: function () {
                        return data;
                    }
                }
            }).closed.then(function () { $scope.$emit('customerDeleted'); });
        };

        $scope.deleteTest = function (data) {
            swal({
                    title: data.name,
                    text: "Are you sure you want to delete this customer?",
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
                function(isConfirm){
                    if(isConfirm){
                        dataService.delete("customers", data.id, function (data) { $scope.$emit('customerDeleted'); })
                        swal.close();
                    }
                });
        };

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'views/custModal.html',
                controller: 'ModalCtrl',
                controllerAs: '$cust',
                resolve: {
                    customer: function () {
                        return;
                    }
                }
            }).closed.then(function () { $scope.$emit('customerDeleted'); });
        }
    }]);

    app.controller('ModalCtrl', ["$uibModalInstance", "$scope", "customer", "dataService", function ($uibModalInstance, $scope, customer, dataService) {
        var $cust = this;
        console.log(customer);
        $scope.customer = customer;

        $scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

        $scope.save = function(customer){
            console.log(customer);
            if(customer.id === undefined){
                dataService.insert("customers", customer, function(data){
                    $scope.$emit('customerDeleted');
                });
            }
            else{
                dataService.update("customers", customer.id, customer, function(data){
                    $scope.$emit('customerDeleted');
                });
            }
            $uibModalInstance.dismiss();
        };
    }]);
})();