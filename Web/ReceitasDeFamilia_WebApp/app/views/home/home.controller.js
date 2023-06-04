'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('HomeCtrl', function ($scope, $state, $filter, localStorageService,receitaService, familiaService, DTOptionsBuilder, DTColumnDefBuilder, $timeout) {
    $scope.receitas = [];
    $scope.familiasById = {};
    $scope.categoriasById = {};

    $scope.receitaModal = {
      model: null,
      mode: 'edit',
      onOpen: (item) => {
          $scope.receitaModal.mode = 'edit';
          $scope.receitaModal.model = item;
        $('#receitaModal').modal('toggle');
      },
      onClose: () => {
        $scope.receitaModal.model = null;
        $('#receitaModal').modal('toggle');
        _getReceitas();
      },
      onDelete: () => {
      },
      onSave: () => {
      
      },
      onFavorite: () => {
        if($scope.receitaModal.model != null && $scope.receitaModal.model.favorito){
          receitaService.removeFavorito($scope.receitaModal.model.Id).then((resp) => {
            if (resp.status !== 200) {
              let msg = 'Erro ao remover receita favorita';
              toastr.error("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            } else {
              let msg = `receita ${$scope.receitaModal.model.nome} removida dos favoritos.`;
              toastr.success("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            }
            $scope.receitaModal.onClose();
          })
        }else{
          receitaService.addFavorito($scope.receitaModal.model.Id).then((resp) => {
            if (resp.status !== 200) {
              let msg = 'Erro ao adicionar receita favorita';
              toastr.error("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            } else {
              let msg = `receita ${$scope.receitaModal.model.nome} adicionado aos favoritos.`;
              toastr.success("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            }
            $scope.receitaModal.onClose();
          })
        }
      }
    }

    configDataTables();

    function _getFamilias() {
      familiaService.getAll().then((resp) => {
        if (resp.status != 200) {
          return;
        }
        // $scope.familiaSelector.inputData = resp.data;

        resp.data.map((item) => {
          $scope.familiasById[item.Id] = item.nome;
        });
        rerenderDatatable();
      });
    }
    function _getReceitas() {
      receitaService.getAllFavoritos().then((resp) => {
        if (resp.status != 200) {
          return;
        }
        $scope.receitas = resp.data;
        rerenderDatatable();
      });
    }
    function _getCategoriasReceitas() {
      receitaService.getCategorias().then((resp) => {
        if (resp.status != 200) {
          return;
        }
        // $scope.categoriasSelector.inputData = resp.data;
        resp.data.map((item) => {
          $scope.categoriasById[item.Id] = item.nome;
        });
        rerenderDatatable();
      });
    }

    _getFamilias();
    _getCategoriasReceitas();
    _getReceitas();

    function configDataTables() {
      $scope.dataTableReceitas = {
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
      if ($scope.dataTableReceitas.instance && !$scope.dataTableReceitas.rerendering) {
        $scope.dataTableReceitas.rerendering = true;
        $timeout(function () {
          $scope.dataTableReceitas.rerendering = false;
        }, 500);
      }
    });
  };
  });