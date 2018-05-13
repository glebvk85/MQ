var myApp = angular.module('SearchApp');
myApp.controller('searchController', function ($scope, $http) {
    $scope.data = null;
    $scope.findIp = function (text) {
        if (text != "")
        {
            $scope.data = null;
            $http({ method: 'GET', url: 'ip/location?ip=' + text }).
                then(function success(response) {
                    $scope.data = response.data;
                });
        }
    }
    $scope.findCity = function (text) {
        if (text != "") {
            $scope.data = null;
            $http({ method: 'GET', url: 'city/locations?city=' + text }).
                then(function success(response) {
                    $scope.data = response.data;
                });
        }
    }
});