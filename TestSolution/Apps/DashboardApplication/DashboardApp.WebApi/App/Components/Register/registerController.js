mainApp.controller('registerController', function ($scope) {

    $scope.message = 'This message comes from register controller! :)';
    $scope.registrationConclusion = "Type infos...";
    $scope.users = [];

    var uri = 'api/users';

    $scope.sendRegistration = function() {
        var password = this.password;
        var email = this.email;
        $scope.registrationConclusion = "Invoked";
        $.ajax({
            type: "POST",
            url: uri,
            data: JSON.stringify({ Email: email, Password: password }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $scope.registrationConclusion = "Registration success! " + data;
                loadUsers();
            },
            failure: function (err) {
                $scope.registrationConclusion = 'Error: ' + err;
            }
        });
    };

    function loadUsers() {
        $scope.users = [];
        $.getJSON(uri)
            .done(function (data) {
                var users = [];
                $.each(data, function (key, item) {
                    users.push(item);
                });
            $scope.users = users;
        });
    }

});