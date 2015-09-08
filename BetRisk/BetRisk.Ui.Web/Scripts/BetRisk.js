var baseApiUrl = "http://localhost:63455/api/";

angular.module('betRisk', [])
    .controller('CustomerController', function ($scope, $http) {

        $scope.customers = [];
        $scope.bets = [];
        $scope.message = "Starting";

        $scope.getCustomers = function () {
            $scope.message = "Loading customers";
            $scope.customers = [];
            $scope.bets = [];
            var url = baseApiUrl + "Customers";
            $http.get(url).success(function (data, status, headers, config) {
                $scope.customers = data.data;
                $scope.message = "Loaded customers";
            }).error(function (data, status, headers, config) {
                $scope.message = data.message;
            });
        }
        $scope.getBets = function (customerId) {
            $scope.message = "Loading bets";
            $scope.customers = [];
            $scope.bets = [];
            var url = baseApiUrl + "Bets";
            if (customerId != null) {
                url += "?customerId=" + customerId;
            }
            $http.get(url).success(function (data, status, headers, config) {
                $scope.bets = data.data;
                $scope.message = "Loaded bets";
            }).error(function (data, status, headers, config) {
                $scope.message = data.message;
            });
        }
    });