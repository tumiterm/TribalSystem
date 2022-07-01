var app = angular.module("myApp", []);

app.controller("myCntrl", function ($scope, $http) {
    $scope.Index = function () {
        $http({
            method: 'get',
            url: "/Headsmen/Index"
        }).then(function (response) {
            $scope.headsman = response.data;
        }, function () {
            alert("Error Occur!");
        })
    };
    $scope.Create = function () {
     
        var type = document.getElementById("btnSubmit").getAttribute("value");
   
        if (type == "Submit") {
            $scope.headsman = {};
            $scope.headsman.Name = $scope.Name;
            $scope.headsman.LastName = $scope.LastName;
            $scope.headsman.Address = $scope.Address;
            $scope.headsman.Email = $scope.Email;

            $http({
                method: "post",
                url: "/Headsmen/Create",
                datatype: "json",
                data: JSON.stringify($scope.headsmen)

            }).then(function (response) {
                alert(response.data);
                $scope.Index();
                $scope.Name = "";
                $scope.Email = "";
                $scope.Address = "";
            })
        } else {
            $scope.headsman = {};
            $scope.headsman.Id = sessionStorage.getItem("Id");
            $scope.student.Name = $scope.Name;
            $scope.student.Email = $scope.Email;
            $scope.student.Address = $scope.Address;
            $http({
                method: "post",
                url: "/Headsmen/Edit",
                datatype: "json",
                data: JSON.stringify($scope.student)

            }).then(function (response) {
                alert(response.data);
                $scope.Index();
                $scope.Name = "";
                $scope.Email = "";
                $scope.Address = "";
                document.getElementById("btnSubmit").setAttribute("value", "Submit");
            })
        }
    };
    $scope.Edit = function (Std) {
        sessionStorage.setItem("Id", Std.Id);
        $scope.Name = Std.Name;
        $scope.Email = Std.Email;
        $scope.Address = Std.Address;
        document.getElementById("btnSubmit").setAttribute("value", "Update");
    };
    $scope.DeleteHeadsmen = function (Std) {
        $http({
            method: "post",
            url: "/Headsmen/Delete",
            datatype: "json",
            data: JSON.stringify(Std)
        }).then(function (reponse) {
            alert(reponse.data);
            $scope.Index();
        })
    };

});



