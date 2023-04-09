(function () {
    'use strict';

    let showFake = templateFakeData;

    angular.module('sbAdminApp')
        .factory('localStorageService', localStorageService);

    localStorageService.$inject = ['generalConfig'];
    function localStorageService(generalConfig) {

        var service = {
            saveSession,
            getSession,
            removeSession,
            clearSession,
            cacheObjects: { getCached, setCache }
        };
        return service;


        function saveSession(state, obj) {
            if (!state || !obj) {
                console.log('error in saveSession - localStorageService');
                return;
            }
            let storageObject = {
                state: state,
                data: obj
            };
            localStorage.setItem('savedSessionData', JSON.stringify(storageObject));
        }

        function getSession(state) {
            let savedSessionData = localStorage.getItem('savedSessionData');
            if (!savedSessionData)
                return;

            savedSessionData = JSON.parse(savedSessionData);

            if (state && savedSessionData.state !== state)
                return

            return savedSessionData;
        }

        function removeSession(state) {
            if (!state) {
                console.log('error in removeSession - localStorageService');
                return;
            }

            if (getSession(state))
                clearSession();
        }

        function clearSession() {
            localStorage.removeItem('savedSessionData');
        }

        function getCached(cacheName, clearCache) {
            if (generalConfig.cacheObjects.enabled) {
                let tmp = localStorage.getItem(cacheName);
                if (tmp) {
                    let cache = JSON.parse(tmp);
                    let dueDate = new Date(cache.dueDate);
                    if (dueDate > new Date() && !clearCache) {
                        return cache.data;
                    } else {
                        localStorage.removeItem(cacheName);
                    }
                }
            }
            return null;
        }

        function setCache(cacheName, data) {
            // var dueDate = new Date();
            // dueDate.setDate(dueDate.getDate() + cacheDuration);

            var dueDate = new Date();
            dueDate.setTime(dueDate.getTime() + (generalConfig.cacheObjects.expirationInHours * 60 * 60 * 1000));

            localStorage.setItem(cacheName, JSON.stringify({ dueDate, data: data }));
        }

    }
})();