(function () {
    var app = angular.module("timeKeeper", ["ngRoute", "ui.bootstrap", "toaster", "ngAnimate", "naif.base64", "chart.js"]);

    currentUser = {
        id: 0,
        name: '',
        role: '',
        teams: [],
        provider: ''
    };

    var baseUrl = "#{ApiUrl}";
    var baseIdsUrl = "#{AuthServer}";

    app.constant('timeConfig', {
        apiUrl: (baseUrl[0] !== "#") ? baseUrl : 'http://localhost:53646/api/',
        idsUrl: (baseIdsUrl[0] !== "#") ? baseIdsUrl + '/connect/token' : 'http://localhost:53671/connect/token',
        dayType: ['empty', 'workingday', 'publicholiday', 'otherabsence', 'religiousday', 'sickleave', 'vacation', 'businessabsence', 'weekend', 'future'],
        dayDesc: [' ', 'Working Day', 'Public Holiday', 'Other Absence', 'Religious Day', 'Sick Leave', 'Vacation', 'Business Absence'],
        months: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
    });

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/teams', {
                templateUrl: 'views/teams.html',
                controller: 'teamsController',
                loginR: true
            })
            .when('/employees', {
                templateUrl: 'views/employees.html',
                controller: 'employeesController',
                loginR: true
            })
            .when('/customers', {
                templateUrl: 'views/customers.html',
                controller: 'customersController',
                loginR: true
            })
            .when('/projects', {
                templateUrl: 'views/projects.html',
                controller: 'projectsController',
                loginR: true
            })
            .when('/timetracking', {
                templateUrl: 'views/timetracking.html',
                controller: 'daysController',
                loginR: true
            })
            .when('/dashboard', {
                templateUrl: 'views/dashboard.html',
                controller: 'dashboardController',
                loginR: true
            })
            .when('/dashboardCompany', {
                templateUrl: 'views/dashboardCompany.html',
                controller: 'dashboardCompanyController',
                loginR: true
            })
            .when('/dashboardTeam', {
                templateUrl: 'views/dashboardTeam.html',
                controller: 'dashboardTeamController',
                loginR: true
            })
            .when('/annualOverview', {
                templateUrl: 'views/annualOverview.html',
                controller: 'annualOverviewController',
                loginR: true
            })
            .when('/monthlyReport', {
                templateUrl: 'views/monthlyReport.html',
                controller: 'monthlyReportController',
                loginR: true
            })
            .when('/projectHistory', {
                templateUrl: 'views/projectHistory.html',
                controller: 'projectHistoryController',
                loginR: true
            })
            .when('/missingEntries', {
                templateUrl: 'views/missingEntries.html',
                controller: 'missingEntriesController',
                loginR: true
            })
            .when('/invoices', {
                templateUrl: 'views/invoices.html',
                controller: 'invoicesController',
                loginR: true
            })
            .when('/login', {
                templateUrl: 'views/login.html',
                controller: 'loginController',
                loginR: false
            })
            .when('/logout', {
                templateUrl: 'views/login.html',
                controller: 'logoutController',
                loginR: false
            })
            .otherwise({
                redirectTo: '/timetracking'
            });
    }]).run(['$rootScope', '$location', function ($rootScope, $location) {
        $rootScope.$on('$routeChangeStart', function (event, next, current) {
            if (currentUser.id === 0 && next.$$route.loginR) {
                $location.path("/login");
            }
        })
    }]);
})();
