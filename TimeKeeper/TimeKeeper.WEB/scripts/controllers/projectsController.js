(function(){

    var app = angular.module("timeKeeper");

    app.controller("projectsController", ["$scope", "dataService", function($scope, dataService) {

        if(currentUser.role === "Administrator") {
            $scope.showProjects = true;
        }

        dataService.list("customers", function(data){
            $scope.customers = data;
        });

        dataService.list("teams", function(data){
            $scope.teams = data;
        });

        $scope.message = "Wait...";
        dataService.list("projects", function(data, headers){
            $scope.page = JSON.parse(headers('Pagination'));
            $scope.message = "";
            $scope.projects = data;
        });

        //sorting
        $scope.column = 'name';
        $scope.reverse = false; 
        
        $scope.sortColumn = function(col){
            $scope.column = col;
            if($scope.reverse){
                $scope.reverse = false;
                $scope.reverseclass = 'arrow-up';
            } else{
                $scope.reverse = true;
                $scope.reverseclass = 'arrow-down';
            }
        };
        
        $scope.sortClass = function(col){
            if($scope.column == col ){
            if($scope.reverse){
                return 'arrow-down'; 
            }else{
                return 'arrow-up';
            }
            }else{
            return '';
            }
        } 

        //paging
        $scope.next = function (page) {

            dataService.list("projects?page=" + page.nextPage, function (data, headers) {
                $scope.projects = data;
                $scope.page = JSON.parse(headers('Pagination'));
                console.log($scope.page);
            })
        }

        $scope.previous = function (page) {
            dataService.list("projects?page=" + page.prevPage, function (data, headers) {
                $scope.projects = data;
                $scope.page = JSON.parse(headers('Pagination'));
            })
        }

        $scope.filter = function (filter) {
            if (filter != "") {
                dataService.list("projects?filter=" + filter, function (data, headers) {
                    $scope.projects = data;
                })
            }
            else if (filter == ""){
                dataService.list("projects", function (data, headers) {
                    $scope.projects = data;
                })
            }
        }

        $scope.edit = function(project){
            $scope.project = project;
        };

        $scope.save = function(project){
            console.log(project);
            if(project.id === undefined){
                dataService.insert("projects", project, function(data){
                    $scope.$emit('projectDeleted');
                });
            }
            else{
                dataService.update("projects", project.id, project, function(data){
                    $scope.$emit('projectDeleted');
                });
            }
        };

        $scope.setNull = function () {
            $scope.project = null;
        };

        $scope.$on('projectDeleted', function(event) {
            dataService.list("projects", function(data, headers){
                $scope.message = "";
                $scope.projects = data;
            });
        });

        $scope.deleteTest = function (project) {
            swal({
                    title: project.name,
                    text: "Are you sure you want to delete this project?",
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
                        dataService.delete("projects", project.id, function (data) { $scope.$emit('projectDeleted'); })
                        swal.close();
                    }
                });
        };
    }]);
})();