'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('familiasCtrl', function ($scope, $state, $filter, familiaService, DTOptionsBuilder, DTColumnDefBuilder, $timeout, confirmDialog) {
    _initModalZIndexControl();
    $('body').data('fv_open_modals', 0);
    $scope.familias = [];

    $scope.familiaModal = {
      model: null,
      onOpen: (item) => {
        $scope.familiaModal.model = item;
        $('#familiaModal').modal('toggle');
      },
      onClose: () => {
        $scope.familiaModal.model = null;
        $('#familiaModal').modal('toggle');
      },
      onDelete: () => {
        if ($scope.familiaModal.model == null) {
          let msg = 'Nenhuma família selecionada.';
          toastr.warning("", msg, {
            "closeButton": true,
            "newestOnTop": true,
            "progressBar": true,
            "preventDuplicates": true,
          });
          $scope.familiaModal.onClose();
          return;
        }

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
        confirmDialog.show('Deletar Família', `Tem certeza que quer deletar família ${$scope.familiaModal.model.nome}?`, conf)
          .then((resp) => {
            if (resp) {
              $timeout(() => {
                familiaService.deletar($scope.familiaModal.model.id).then((resp) => {
                  if (resp.status !== 200) {
                    let msg = 'Erro ao deletar família';
                    toastr.error("", msg, {
                      "closeButton": true,
                      "newestOnTop": true,
                      "progressBar": true,
                      "preventDuplicates": true,
                    });
                  } else {
                    let msg = 'Deletado com sucesso.';
                    toastr.success("", msg, {
                      "closeButton": true,
                      "newestOnTop": true,
                      "progressBar": true,
                      "preventDuplicates": true,
                    });
                  }
                  $scope.familiaModal.onClose();
                })
              });
            } else {
            }
          });

        

      }
    }
    configDataTables();

    familiaService.getAll().then((resp) => {
      if (resp.status != 200) {
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