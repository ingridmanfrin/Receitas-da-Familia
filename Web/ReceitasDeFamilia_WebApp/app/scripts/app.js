let templateFakeData = false;
'use strict';
/**
 * @ngdoc overview
 * @name sbAdminApp
 * @description
 * # sbAdminApp
 *
 * Main module of the application.
 */
angular.module('local.datatables', ['datatables', 'datatables.buttons', 'datatables.options', 'datatables.fixedcolumns']);


angular
  .module('sbAdminApp', [
    'oc.lazyLoad',
    'ui.router',
    'ui.bootstrap',
    'angular-loading-bar',
    'isteven-multi-select',
    'ngMask',
    'cur.$mask',
    'local.datatables',
  ])
  .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

    $ocLazyLoadProvider.config({
      debug: false,
      events: true,
    });

    $urlRouterProvider.otherwise('/dashboard/order');

    $stateProvider
      .state('dashboard', {
        url: '/dashboard',
        templateUrl: 'views/dashboard/main.html',
        resolve: {
          loadMyDirectives: function ($ocLazyLoad) {
            return $ocLazyLoad.load(
              {
                name: 'sbAdminApp',
                files: [
                  'scripts/directives/header/header.js',
                  'scripts/directives/header/header-notification/header-notification.js',
                  'scripts/directives/sidebar/sidebar.js',
                  'scripts/directives/loadSpinner/loadSpinner.js',
                  'scripts/directives/confirmDialog/confirmDialog.js',
                  'scripts/directives/sidebar/sidebar-search/sidebar-search.js'
                ]
              }),
              $ocLazyLoad.load(
                {
                  name: 'toggle-switch',
                  files: ["bower_components/angular-toggle-switch/angular-toggle-switch.min.js",
                    "bower_components/angular-toggle-switch/angular-toggle-switch.css"
                  ]
                }),
              $ocLazyLoad.load(
                {
                  name: 'ngAnimate',
                  files: ['bower_components/angular-animate/angular-animate.js']
                })
            $ocLazyLoad.load(
              {
                name: 'ngCookies',
                files: ['bower_components/angular-cookies/angular-cookies.js']
              })
            $ocLazyLoad.load(
              {
                name: 'ngResource',
                files: ['bower_components/angular-resource/angular-resource.js']
              })
            $ocLazyLoad.load(
              {
                name: 'ngSanitize',
                files: ['bower_components/angular-sanitize/angular-sanitize.js']
              })
            $ocLazyLoad.load(
              {
                name: 'ngTouch',
                files: ['bower_components/angular-touch/angular-touch.js']
              })
          }
        }
      })
      .state('404', {
        templateUrl: 'views/404/404.html',
        url: '/404',
        resolve: {
          loadMyFiles: function ($ocLazyLoad) {
            return $ocLazyLoad.load({
              name: 'sbAdminApp',
              files: [
                'views/404/404.css'
              ]
            })
          }
        }
      })
      .state('login', {
        templateUrl: 'views/login/login.html',
        url: '/login',
        controller: 'loginCtrl',
        templateConfig: {
        },
        resolve: {
          loadMyFiles: function ($ocLazyLoad) {
            return $ocLazyLoad.load({
              name: 'sbAdminApp',
              files: [
                'views/login/login.controller.js',
                'views/login/login.css',
                'scripts/services/usuario.service.js'
              ]
            })
          }
        }
      })
      .state('dashboard.home', {
        url: '/home',
        controller: 'HomeCtrl',
        templateUrl: 'views/home/home.html',
        authorize: true,
        templateConfig: {
        },
        resolve: {
          loadMyFiles: function ($ocLazyLoad) {
            return $ocLazyLoad.load({
              name: 'sbAdminApp',
              files: [
                'views/home/home.controller.js',
                'views/home/home.css',
              ]
            })
          }
        }
      })
      .state('dashboard.usuario', {
        url: '/usuario',
        controller: 'usuarioCtrl',
        templateUrl: 'views/usuario/usuario.html',
        authorize: true,
        templateConfig: {
        },
        resolve: {
          loadMyFiles: function ($ocLazyLoad) {
            return $ocLazyLoad.load({
              name: 'sbAdminApp',
              files: [
                'views/usuario/usuario.controller.js',
                'views/usuario/usuario.css',
                'scripts/services/usuario.service.js'
              ]
            })
          }
        }
      })

  }])
  .run(['$rootScope', '$state', 'principal', 'authorization', '$timeout', 'localStorageService', 'confirmDialog', function ($rootScope, $state, principal, authorization, $timeout, localStorageService, confirmDialog) {
    $rootScope.$on('$stateChangeStart', function (event, toState, toStateParams) {
      $rootScope.toState = toState;
      $rootScope.toStateParams = toStateParams;
      if (toState.authorize) {
        authorization.authorize().then(() => {
          let savedSession = localStorageService.getSession();
          if (toState.name !== 'login' && savedSession && toState.name !== savedSession.state) {
            $timeout(() => {
              let conf = {
                buttons: {
                  close: {
                    label: "Não"
                  },
                  confirm: {
                    label: "Sim"
                  }
                }
              }
              confirmDialog.show('Recuperar Dados', 'Existem alterações não salvas, deseja recuperar?', conf)
                .then((resp) => {
                  if (resp) {
                    $timeout(() => {
                      $state.go(savedSession.state);
                    });
                  } else {
                    localStorageService.clearSession();
                  }
                });
            }, 1000);
          }
        }, () => {
          var msgT = "Você não possui autorização para acessar este recurso.";//i18n.t('alerts:interceptor.unauthorized'),
          let msg = 'Sessão expirada, faça login novamente.';//i18n.t('alerts:interceptor.expired_seccion');
          toastr.error("", msg, {
            "closeButton": true,
            "newestOnTop": true,
            "progressBar": true,
            "preventDuplicates": true,
          });
        });


        // if (principal.isIdentityResolved())
        //   authorization.authorize();
        // else {
        //   var msgT = "Você não possui autorização para acessar este recurso.";//i18n.t('alerts:interceptor.unauthorized'),
        //   let msg = 'Seção expirada, faça login novamente.';//i18n.t('alerts:interceptor.expired_seccion');
        //   toastr.error("",msg, {
        //     "closeButton": true,
        //     "newestOnTop": true,
        //     "progressBar": true,
        //     "preventDuplicates": true,
        //   });
        //   $timeout(() => {
        //     $state.go('login');
        //   });
        // }
      }
    });

    $rootScope.$on('$stateNotFound', function (event, unfoundState, fromState, fromParams) {
      $state.go('404');
    });

    // $rootScope.$on('$viewContentLoaded', function (event) {
    //   // debugger;
    //     if (event.targetScope && event.targetScope.loaded) {
    //         event.targetScope.loaded();
    //     }
    // });
  }]);;


  function _initModalZIndexControl() {
    $('.modal').on('hidden.bs.modal', function (event) {
      $(this).removeClass('fv-modal-stack');
      $('body').data('fv_open_modals', $('body').data('fv_open_modals') - 1);
    });


    $('.modal').on('shown.bs.modal', function (event) {

      // keep track of the number of open modals

      if (typeof ($('body').data('fv_open_modals')) == 'undefined') {
        $('body').data('fv_open_modals', 0);
      }


      // if the z-index of this modal has been set, ignore.

      if ($(this).hasClass('fv-modal-stack')) {
        return;
      }

      $(this).addClass('fv-modal-stack');

      $('body').data('fv_open_modals', $('body').data('fv_open_modals') + 1);

      $(this).css('z-index', 1040 + (10 * $('body').data('fv_open_modals')));

      $('.modal-backdrop').not('.fv-modal-stack')
        .css('z-index', 1039 + (10 * $('body').data('fv_open_modals')));


      $('.modal-backdrop').not('fv-modal-stack')
        .addClass('fv-modal-stack');

    });
  }
  _initModalZIndexControl();