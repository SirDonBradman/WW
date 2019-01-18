var app = angular.module('myApp', []);

app.controller('mainController', function ($scope, $http) {
    $scope.name = "";
    $scope.email = "";
    $scope.coming = "yes";
    $scope.numberOfGuests = 1;

    $scope.addGuests = function () {
        let data = {
            name: $scope.name,
            email: $scope.email,
            coming: $scope.coming === 'yes',
            numberOfGuests: $scope.numberOfGuests
        };
        console.log(data);
        $http.post("/api/guests", data).then(function () {
            alert('success');
        });
    }
});