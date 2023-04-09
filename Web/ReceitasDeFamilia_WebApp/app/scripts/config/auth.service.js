(function () {
    'use strict';

    let showFake = templateFakeData;

    angular.module('sbAdminApp')
        .factory('principal', principalService)
        .factory('authIdentity', authIdentityService)
        .factory('authToken', authTokenService)
    // .factory('authRole', authRoleService)
    .factory("signoutService", signout);

    principalService.$inject = ['$q', '$http', 'authIdentity'];
    function principalService($q, $http, authIdentity, authToken) {
        var _identity;
        var authenticated = false;

        var service = {
            isIdentityResolved: isIdentityResolved,
            isAuthenticated: isAuthenticated,
            isInRole: isInRole,
            isInAnyRole: isInAnyRole,
            authenticate: authenticate,
            identity: identity,
            getUserNameLogged: getUserNameLogged,
            dispose: dispose
        };
        return service;


        function isIdentityResolved() {
            return angular.isDefined(_identity);
        }

        function isAuthenticated() {
            return authenticated && !authIdentity.isExpired();
        }

        function isInRole(role) {
            if (!authenticated || !_identity.roles) return false;

            return _identity.roles.indexOf(role) != -1;
        }

        function isInAnyRole(roles) {
            if (!authenticated || !_identity.roles) return false;

            for (var i = 0; i < roles.length; i++) {
                if (this.isInRole(roles[i])) return true;
            }

            return false;
        }

        function authenticate(identity) {
            var deferred = $q.defer();

            _identity = identity;
            authenticated = identity != null;

            if (identity)
                authIdentity.set(identity);

            if (!identity || authIdentity.isExpired()) {
                authIdentity.remove();
                _identity = undefined;
                deferred.resolve();
            } else {
                // authRole.get(_identity.userName)
                //         .then(function (response) {
                //             identity.roles = response.data;
                //             _identity = identity;

                //             authIdentity.set(identity);
                deferred.resolve();
                //         });
            }

            return deferred.promise;
        }

        function identity(force) {
            var deferred = $q.defer();

            if (force === true) _identity = undefined;

            if (isIdentityResolved()) {
                deferred.resolve(_identity);

                return deferred.promise;
            }

            _identity = authIdentity.get();
            return authenticate(_identity);
        }

        function getUserNameLogged() {
            var identity = _identity || authIdentity.get();

            return !!identity ? identity.userName : '';
        }

        function dispose() {
            authIdentity.remove();
            _identity = undefined;
        }
    }

    function authIdentityService() {
        var service = {
            get: get,
            set: set,
            remove: remove,
            isExpired: isExpired
        };
        return service;

        function get() {
            return angular.fromJson(localStorage.getItem('user.identity'));
        }

        function set(identity) {
            localStorage.setItem('user.identity', angular.toJson(identity));
        }

        function remove() {
            localStorage.removeItem('user.identity');
        }

        function isExpired() {
            var identity = get();

            if (!identity) return true;

            var expiresIn = (new Date(identity['.expires'])).getTime();
            var nowSeconds = (new Date()).getTime();

            return expiresIn < nowSeconds;
        }
    }

    authTokenService.$inject = ['$http', '$q'];
    function authTokenService($http, $q) {
        var service = {
            get: showFake ? getFake : get
        };
        return service;

        function get(username, password) {
            return $http({
                url: '/api/Login',
                method: 'POST',
                data: {
                    email: username,
                    senha: password,
                    // client_id: generalConfig.apiClientId
                }
            });
        }

        function getFake(username, password) {
            let itemPrototype = {"access_token":"token_fake","token_type":"bearer","expires_in":3599}
            var deferred = $q.defer();

            var response = {
                data: itemPrototype,
                status: 200
            };

            if (!(username == 'admin' && password == '123456')) {
                response = {
                    data: {
                        error: 'invalid_grant',
                        error_description: 'The user name or password is incorrect.'
                    },
                    status: 400
                };
            }

            deferred.resolve(response);
            return deferred.promise;
        }
    }

    authRoleService.$inject = ['$http'];
    function authRoleService($http) {
        var service = {
            get: get
        };
        return service;

        function get(userName) {
            return $http({
                url: '/api/Permission/GetByUserName',
                method: 'GET',
                params: { username: userName }
            });
        }
    }

    signout.$inject = ['principal'];
    function signout(principal) {
        return function () {
            principal.dispose();
            // topNotificationService.stopRefreshNotification();
        }
    }
})();