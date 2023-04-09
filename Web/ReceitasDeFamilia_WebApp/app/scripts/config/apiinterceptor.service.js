(function () {
    'use strict';

    angular.module('sbAdminApp').factory('apiInterceptorService', service);

    service.$inject = ['authIdentity', 'generalConfig'];
    function service(authIdentity, generalConfig) {
        var service = {
            request: request,
            responseError: responseError
        };
        return service;

        function request(config) {
            if (config.url.indexOf('/api/') != -1) {
                var identity = authIdentity.get();

                if (hasValidToken(identity) && !isRequestToken(config.url)) {
                    config.headers.authorization = 'Bearer ' + identity.access_token;
                    if (authIdentity.isExpired()){
                        window.location.reload();
                    }
                }

                if (isRequestToken(config.url)) {
                    config.data = $.param(config.data);
                    config.headers['Content-Type'] = 'application/x-www-form-urlencoded; charset=utf-8';
                } else if (isFormData(config.data)) {
                    config.headers['Content-Type'] = undefined;
                } else {
                    config.data = JSON.stringify(config.data);
                    config.headers['Content-Type'] = 'application/json';
                }

                if (config.method.toUpperCase() == 'GET') {
                    config.headers['Cache-Control'] = 'no-cache';
                    config.headers['Pragma'] = 'no-cache';
                }

                config.headers['Accept'] = 'application/json';

                config.url = injectBaseAddressApi(config.url);
            } else if (!generalConfig.cacheFrontEndFiles && !config.isTemplate) {
                if(generalConfig.cacheBlacklist.indexOf(config.url) === -1){
                    config.url = makeUrlTimeStamp(config.url);
                }
            }

            // if (!requestIsForNotification(config.url)) { Pace.restart(); }

            return config;
        }

        function responseError(response) {
            if (response.status === 403) {
                //$rootScope.$broadcast('badRequest');
                var msgT = "Bad Request"//i18n.t('alerts:interceptor.bad_request')
                toastr.warning('', msgT, { closeButton: true });
            }

            if (response.status === 401) {
                var msgT = "Você não possui autorização para acessar este recurso.";//i18n.t('alerts:interceptor.unauthorized'),
                let msg = '';
                if (authIdentity.isExpired()) {
                    msg = 'Sessão expirada, faça login novamente.';//i18n.t('alerts:interceptor.expired_seccion');
                }

                //$rootScope.$broadcast('unauthorized');
                toastr.error(msg, msgT, { closeButton: true });

                // $state.go('login');
            }

            if (response.status === 404) {
                //$rootScope.$broadcast('notFound');
                var msgT = 'Recurso não encontrado: 404';//i18n.t('alerts:interceptor.not_found')
                toastr.error('', msgT, { closeButton: true });
            }

            if (response.status === 406 && response.data.error === 'acesso inválido') {
                var msgT = 'Muitas tentativas de login erradas, você foi bloqueado por 5 minutos. Status: 406';//i18n.t('alerts:interceptor.not_found')
                toastr.error('', msgT, { closeButton: true });
            }

            if (response.status === 429) {
                //$rootScope.$broadcast('notFound');
                var msgT = 'Limite de requisições atingidas, aguarde. Código: 429';//i18n.t('alerts:interceptor.not_found')
                toastr.error('', msgT, { closeButton: true });
            }

            //if (response.status === 500) {
            //    $rootScope.exception = response.data;
            //    //$rootScope.$broadcast('internalServerError');
            //    $location.url('/index/500');
            //}

            if (response.status === -1) {
                var msgT = 'Acesso Negado!';//i18n.t('alerts:interceptor.denied_access_source');
                toastr.error('', msgT, { closeButton: true });
            }

            // spinnerFullPage.hide();
            return response;
        }

        function isRequestToken(url) {
            return url.indexOf('/Token') != -1;
        }

        function hasValidToken(identity) {
            return !!identity && !!identity.access_token;
        }

        function isFormData(data) {
            return Object.prototype.toString.call(data) === "[object FormData]";
        }

        function injectBaseAddressApi(url) {
            let ret = url.replace('/api/', generalConfig.baseAddressApi);
            if (isRequestToken(url)) {
                ret = ret.replace(/\/api\//i, '/');
            }
            return ret;
        }

        function requestIsForNotification(url) {
            return (url.indexOf('/api/notification/get') !== -1);
        }

        function makeUrlTimeStamp(url) {
            var dc = new Date().getTime();
            if (url.indexOf('?') >= 0) {
                if (url.substring(0, url.length - 1) === '&') {
                    return url + '_dc=' + dc;
                }
                return url + '&_dc=' + dc;
            } else {
                return url + '?_dc=' + dc;
            }
        }
    }
})();