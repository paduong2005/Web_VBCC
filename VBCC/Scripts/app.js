var app = angular.module('myApp', ['angucomplete-alt', 'ui.bootstrap', 'myDirective']); //add angucomplete-alt dependency in app
app.controller('myCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.Countries = [];
    $scope.SelectedCountry = null;
    //event fires when click on textbox
    $scope.SelectedCountry = function (selected) {
        if (selected) {
            $scope.SelectedCountry = selected.originalObject;
        }
    }
    //Gets data from the Database
    $http({
        method: 'GET',
        url: '/ThoiKhoaBieuGV/getAllGiaoVien'
    }).then(function (data) {
        $scope.Countries = data.data;
    }, function () {
        alert('Error');
    })
}]);