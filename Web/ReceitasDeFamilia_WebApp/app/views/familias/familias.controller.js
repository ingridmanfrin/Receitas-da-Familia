'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('familiasCtrl', function ($scope, $state, $filter, familiaService, DTOptionsBuilder, DTColumnDefBuilder, $timeout) {
   $scope.familias = [];
   $scope.familiaModal = {
    editFamilia: function(){
      $('#productModal').modal('toggle');
    }
  }
    configDataTables();

    familiaService.getAll().then((resp)=>{
      if(resp.status != 200){
        return;
      }
      $scope.familias = resp.data;
      rerenderDatatable();
    });

 


    function configDataTables() {
      $scope.dataTableFamilias = {
        options: DTOptionsBuilder.newOptions()
          .withOption('paging', true)
          .withOption('order', [])
          .withOption('searching', true)
          .withOption('info', false)
          .withOption('ordering', true),
        columnDefs: [
          // DTColumnDefBuilder.newColumnDef([1, 2])
          //   .renderWith(function (data, type, full) {
          //     if (type == 'sort') {
          //       var value = toDate(data);
          //       if (value == null) {
          //         return 0
          //       } 
          //       return value.getTime();
          //     }
          //     return data
          //   }),
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
        if ($scope.dataTableFamilias.instance && !$scope.dataTableFamilias.rerendering) {
          $scope.dataTableFamilias.rerendering = true;
          $timeout(function () {
            $scope.dataTableFamilias.rerendering = false;
          }, 500);
        }
      });
    };
  });