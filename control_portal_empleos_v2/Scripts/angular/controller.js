

        


app.controller('modals', ['$scope', 'mantenedor_total', function ($scope ,mantenedor_total) {

    $scope.abremodal = function (url, id) {
        mantenedor_total.modal(url, id, $scope);
    };
}]);

app.controller('login', ['$scope', 'mantenedor_total', function ($scope, mantenedor_total) {

    $scope.login = function () {

        var usuarios = { 'nombre_cuenta_usuario': $scope.logins.nombre_cuenta_usuario, 'clave_usuario': $scope.logins.clave_usuario };
        var add_emp = mantenedor_total.agregar_datos('/login/login', usuarios);
        add_emp.then(function (successResponse) {
            if (successResponse.data.success == true) {
                window.location.pathname = successResponse.data.respuesta;


            } else {
                $scope.respuesta = successResponse.data.respuesta;
            }


        },
        function (errorResponse) {
            console.log("error login")
        });
    };


}]);

      
      