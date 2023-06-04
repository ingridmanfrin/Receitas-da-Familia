(function () {
    angular.module('sbAdminApp').service('receitaService', service);
    let showFake = templateFakeData;

    service.$inject = inject_args(service);

    function service($q, $http) {

        return {
            cadastrar,
            get,
            getAll,
            atualizar,
            deletar,
            getCategorias,
            addFavorito,
            removeFavorito,
            getAllFavoritos
        };

        // {
        //     "Id": 0,
        //     "idFamilia": 0,
        //     "idCategoria": 0,
        //     "nome": "string",
        //     "criadorReceita": "string",
        //     "tempoPreparoMin": 0,
        //     "rendimento": "string",
        //     "ingredientes": "string",
        //     "modoPreparo": "string",
        //     "informacoesAdicionais": "string"
        //   }

        function cadastrar(model) {
            var defered = $q.defer();
            $http({
                method: 'POST',
                url: '/api/Receita',
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
                url: `/api/Receita/${model.Id}`,
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
                url: '/api/Receita/' + id
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
                url: '/api/Receita'
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
                url: '/api/Receita/' + id
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function getCategorias() {
            var defered = $q.defer();
            $http({
                method: 'GET',
                url: '/api/CategoriaReceita'
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }

        function getAllFavoritos() {
            var defered = $q.defer();
            $http({
                method: 'GET',
                url: '/api/Favorito'
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');

            });
            return defered.promise;
        }
        
        function addFavorito(receitaId) {
            var defered = $q.defer();
            $http({
                method: 'Post',
                url: '/api/Favorito',
                data: {IdReceita: receitaId}
            }).then(function successCallback(response) {
                console.log(response);
                defered.resolve(response);
            }, function errorCallback(response) {
                console.log('error');
                defered.resolve('error');
            });
            return defered.promise;
        }

        function removeFavorito(receitaId) {
            var defered = $q.defer();
            $http({
                method: 'Delete',
                url: '/api/Favorito/'+ receitaId
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