(function () {
    angular.module('sbAdminApp').service('usuarioService', service);
    let showFake = templateFakeData;

    service.$inject = inject_args(service);

    function service($q, $http) {

        return {
            cadastrarUsuario,
            getUsuario,
            atualizarUsuario
        };

        function cadastrarUsuario(model) {
            var defered = $q.defer();
            $http({
                method: 'POST',
                url: '/api/User',
                data: model
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function atualizarUsuario(model) {
            var defered = $q.defer();
            $http({
                method: 'PUT',
                url: `/api/User/${model.Id}`,
                data: {nome: model.nome}
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function getUsuario() {
            var defered = $q.defer();
            $http({
                method: 'GET',
                url: '/api/User'
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function fake(data) {
            var defered = $q.defer();
            defered.resolve(data);
            return defered.promise;
        };
    };
})()

function inject_args(fx) {
    var ans = String(fx).replace(/\n[\s\S]*/, '').replace(/.*\(|\).*/g, '').split(/[^\w$]+/).filter(function (required) {
        return !required ? false : true;
    });
    return ans;
};