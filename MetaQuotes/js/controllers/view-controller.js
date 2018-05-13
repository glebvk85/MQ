var myApp = angular.module('SearchApp');
myApp.controller('viewController', function ($scope) {
    $scope.data = {};
    $scope.setFile = function () {
        if ($scope.data.mode == 'ip')
            return 'views/search-ip.html';
        else if ($scope.data.mode == 'city')
            return 'views/search-city.html';
    };
    $scope.modes = [{
        value: 'ip',
        label: 'Search by IP'
    }, {
        value: 'city',
        label: 'Search by city'
    }];
});