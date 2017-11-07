app.service("angularServiceCreateTable", function ($http) {
    this.getDataType = function () {
        return $http.get("/GenerateDB/getDataType");
    };

    this.getTable = function () {
        return $http.get("/GenerateDB/getTable");
    };
   
    //Database Generation 
    this.Generator = function (tblName, fldName1, dType1, fldName2, dType2, prtTableName) {
        var response = $http({
            method: "post",
            url: "/GenerateDB/Generator",
            params: {
                tableName: tblName,
                fieldName1: fldName1,
                dataType1: dType1,
                fieldName2: fldName2,
                dataType2: dType2,
                Parent: prtTableName
            }
        });
        return response;
    }

});