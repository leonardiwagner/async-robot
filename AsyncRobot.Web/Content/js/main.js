var phonecatApp = angular.module('asyncRobotApp', [])
.controller('MainCtrl', function ($scope) {
    $scope.robotCount = 100;
    $scope.runApproach = "sync";
    $scope.threadCount = 2;
    $scope.status = "SET_OPTIONS_PENDING";

    

    $scope.setOptions = function () {
        start($scope.robotCount);
        $scope.status = "RUN_PENDING";
    };

    $scope.run = function() {
        return mazeHub.server.run('{"land": ' + JSON.stringify(landJson) + ', "robots": [' + robots + '], "approach": "' + $scope.runApproach + '", "threadCount":' + $scope.threadCount + "}");
    };
});