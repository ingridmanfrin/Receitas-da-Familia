'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('HomeCtrl', function ($scope, $state, $filter, localStorageService, DTOptionsBuilder, DTColumnDefBuilder, $timeout) {
    $scope.orders = [];
    $scope.clients = [];

    configDataTables();

    function configDataTables() {
      $scope.dataTableObject = {
        options: DTOptionsBuilder.newOptions()
          // .withOption( 'scrollY', '379px' ) //379
          //  .withOption( 'scrollX', '500px' ) //379
          .withOption('paging', false)
          // .withOption( 'autoWidth', false )
          .withOption('order', [])
          .withOption('searching', false)
          .withOption('info', false)
          .withOption('ordering', true),
        columnDefs: [
          DTColumnDefBuilder.newColumnDef([1, 2])
            .renderWith(function (data, type, full) {
              if (type == 'sort') {
                var value = toDate(data);
                if (value == null) {
                  return 0
                } 
                return value.getTime();
              }
              return data
            }),
        ],
        instance: {},
      };
    };
    function toDate(dateStr) {
      var parts = dateStr.split("/");
      return new Date(parts[2], parts[1] - 1, parts[0]);
  }
    function rerenderDatatable() {
      $timeout(function () {
        if ($scope.dataTableObject.instance && !$scope.dataTableObject.rerendering) {
          $scope.dataTableObject.rerendering = true;
          $timeout(function () {
            $scope.dataTableObject.rerendering = false;
          }, 500);
        }
      });
    };
  });