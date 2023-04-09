(function () {
    'use strict';

    var generalConfig = {
        //baseAddressApi: '/api/',                         //Quando a API estiver no mesmo endereço do Front-End Web
        baseAddressApi: 'https://localhost:7036/',      //Quando a API estiver em endereço diferente do Front-End Web
        apiClientId: 'WebAngularAppAuth',
        defaultPage: 'home',
        cacheFrontEndFiles: false,
        cacheBlacklist: ['isteven-multi-select.htm'],
        logoImg: {
            login: 'public/images/logo/logo-lg.png',
        },
        cacheObjects: {
            enabled: true,
            expirationInHours: 8,
            
        }
    };

    angular.module('sbAdminApp')
        .config(interceptorsConfig)
        .run(initializeApp)
        .constant('generalConfig', generalConfig)
        .factory('spinnerFullPage', spinnerFullPage)
        .factory('confirmDialog', confirmDialog);

    templateRequestProvider.$inject = ['$templateRequestProvider'];
    function templateRequestProvider($templateRequestProvider) {
        $templateRequestProvider.httpOptions({ isTemplate: true });
    }
    confirmDialogInitializer.$inject = ['$rootScope'];
    function confirmDialogInitializer($rootScope) {
        $rootScope.confirmDialog = {};

        $rootScope.confirmDialog.init = (ctrl) => {
            $rootScope.confirmDialog.control = ctrl;
        };
    }
    function toastrOptions() {
        // opções globais para os toastr
        toastr.options.closeButton = true;
    };

    initializeApp.$inject = ['$rootScope'];
    function initializeApp($rootScope) {
        confirmDialogInitializer($rootScope);
        $rootScope.logoutUser = function () {
            console.log(123)
            localStorage.removeItem('user.identity');
        }
    };

    interceptorsConfig.$inject = ['$httpProvider'];
    function interceptorsConfig($httpProvider) {
        $httpProvider.interceptors.push('apiInterceptorService');
    };


    spinnerFullPage.$inject = ['$rootScope'];
    function spinnerFullPage($rootScope) {
        var config = {
            show: show,
            hide: hide
        };
        return config;

        $rootScope.spinnerFullPageShow = false;
        function show(bool) {
            if (typeof bool === 'undefined') bool = true;
            $rootScope.spinnerFullPageShow = bool;
        };
        function hide() {
            $rootScope.spinnerFullPageShow = false;
        };

    };

    confirmDialog.$inject = ['$rootScope', '$timeout'];
    function confirmDialog($rootScope, $timeout) {
        var config = {
            show: show,
            hide: hide,
            getControl: getControl
        };
        let control = null;
        if (!$rootScope.confirmDialog)
            $rootScope.confirmDialog = {};

        $rootScope.confirmDialog.init = function (ctrl) {
            control = ctrl;
        }

        return config;

        function getControl() {
            return control;
        };
        function show(title, message, config) {
            if (!control) {
                if ($rootScope.confirmDialog && $rootScope.confirmDialog.control)
                    control = $rootScope.confirmDialog.control;
            }
            if (control)
                return control.show(title, message, config);
        };
        function hide() {
            $rootScope.spinnerFullPageShow = false;
        };
    };

    globalize.$inject = ['$rootScope', 'principal'];
    function globalize($rootScope, principal) {
        $rootScope.generalConfig = generalConfig;
        var user = principal.getUserNameLogged();
        if (user)
            $rootScope.loggedUser = user;
    };
})();