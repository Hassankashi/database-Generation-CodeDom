app.controller("AngularCtrlCreateTable", function ($scope, angularServiceCreateTable) {
    $scope.divGen = true;
    $scope.divNewGen = false;
    GetAllDataType1();
    GetAllTable();

    //To Get All Records  
    function GetAllDataType1() {
        var Data = angularServiceCreateTable.getDataType();
        Data.then(function (data) {
            $scope.items1 = data.data;
            $scope.items2 = data.data;
        }, function () {
            alert('Error');
        });
    }

     
    //To Get All Records  
    function GetAllTable() {
        var Data = angularServiceCreateTable.getTable();
        Data.then(function (tb) {
            $scope.itemsPT = tb.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.Generator = function () {
        var dt1 = $scope.selectedItem1;
        var dt2 = $scope.selectedItem2;
        var dt3 = $scope.selectedItemParentTable;
        var getmsg = angularServiceCreateTable.Generator($scope.TableName, $scope.Field1, dt1, $scope.Field2, dt2, dt3);
        getmsg.then(function (messagefromcontroller) {
            $scope.divGen =false;
            $scope.divNewGen = true;
           alert(messagefromcontroller.data);
        }, function () {
            alert('There is error in database generation');
        });
    }

    $scope.GeneratorNew = function () {
            $scope.divGen = true;
            $scope.divNewGen = false;
            GetAllTable();
            GetAllDataType2();
            GetAllDataType1();
            $scope.TableName = "";
            $scope.Field1 = "";
            $scope.Field2 = "";
    }

});