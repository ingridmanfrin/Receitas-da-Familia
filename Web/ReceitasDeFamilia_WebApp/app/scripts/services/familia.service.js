(function () {
    angular.module('sbAdminApp').service('familiaService', service);
    let showFake = templateFakeData;

    service.$inject = inject_args(service);

    function service($q, $http) {

        return {
            cadastrar,
            get,
            getAll,
            atualizar,
            deletar
        };

        function cadastrar(model) {
            var defered = $q.defer();
            $http({
                method: 'POST',
                url: '/api/Familia',
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

        function atualizar(model) {
            var defered = $q.defer();
            $http({
                method: 'PUT',
                url: `/api/Familia/${model.Id}`,
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

        function get(id) {
            var defered = $q.defer();
            $http({
                method: 'GET',
                url: '/api/Familia/' + id
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function getAll() {
            var defered = $q.defer();
            $http({
                method: 'GET',
                url: '/api/Familia'
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function deletar(id) {
            var defered = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/Familia/' + id
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