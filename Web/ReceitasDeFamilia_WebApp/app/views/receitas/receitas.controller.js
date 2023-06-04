'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('receitasCtrl', function ($scope, $state, $filter, receitaService, familiaService, DTOptionsBuilder, DTColumnDefBuilder, $timeout, confirmDialog) {
    _initModalZIndexControl();
    $('body').data('fv_open_modals', 0);
    $scope.receitas = [];
    $scope.familiasById = {};
    $scope.categoriasById = {};

    $scope.receitaModal = {
      model: null,
      mode: 'edit',
      onOpen: (item) => {
        _untickArray($scope.familiaSelector.inputData);
        _untickArray($scope.categoriasSelector.inputData);
        if (item === 'new') {
          $scope.receitaModal.model = {};
          $scope.receitaModal.mode = 'new';
        } else {
          $scope.receitaModal.mode = 'edit';
          $scope.receitaModal.model = item;
          let fam = $scope.familiaSelector.inputData.filter((e)=> e.Id === item.idFamilia)[0];
          fam.ticked = true;
          let cat = $scope.categoriasSelector.inputData.filter((e)=> e.Id === item.idCategoria)[0];
          cat.ticked = true;
        }
        $('#receitaModal').modal('toggle');
      },
      onClose: () => {
        $scope.receitaModal.model = null;
        $('#receitaModal').modal('toggle');
        _getReceitas();
      },
      onDelete: () => {
        if ($scope.receitaModal.model == null) {
          let msg = 'Nenhuma receita selecionada.';
          toastr.warning("", msg, {
            "closeButton": true,
            "newestOnTop": true,
            "progressBar": true,
            "preventDuplicates": true,
          });
          $scope.receitaModal.onClose();
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
        confirmDialog.show('Deletar Receita', `Tem certeza que quer deletar receita ${$scope.receitaModal.model.nome}?`, conf)
          .then((resp) => {
            if (resp) {
              $timeout(() => {
                receitaService.deletar($scope.receitaModal.model.Id).then((resp) => {
                  if (resp.status !== 200) {
                    let msg = 'Erro ao deletar receita';
                    if (resp.status === 409) {
                      msg = 'Esta receita foi criada por outro usuário.';
                    }
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
                  $scope.receitaModal.onClose();
                })
              });
            } else {
            }
          });
      },
      onSave: () => {
        if ($scope.receitaModal.model == null) {
          let msg = 'Nenhuma receita selecionada.';
          toastr.warning("", msg, {
            "closeButton": true,
            "newestOnTop": true,
            "progressBar": true,
            "preventDuplicates": true,
          });
          $scope.receitaModal.onClose();
          return;
        }
        if ($scope.receitaModal.mode === 'new') {
          receitaService.cadastrar($scope.receitaModal.model).then((resp) => {
            if (resp.status !== 200) {
              let msg = 'Erro ao salvar receita';
              toastr.error("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            } else {
              let msg = `receita ${$scope.receitaModal.model.nome} cadastrada com sucesso.`;
              toastr.success("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            }
            $scope.receitaModal.onClose();
          })
        } else {
          receitaService.atualizar($scope.receitaModal.model).then((resp) => {
            if (resp.status !== 200) {
              let msg = 'Erro ao atualizar receita';
              if (resp.status === 409) {
                msg = 'Esta receita foi criada por outro usuário.'
              }
              toastr.error("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
              });
            } else {
              let msg = `Receita ${$scope.receitaModal.model.nome} atualizada com sucesso.`;
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

    $scope.familiaSelector = {
      inputData: [],
      outputData: [],
      translation: {
        selectAll: "Selecionar todos",
        selectNone: "Desmarcar todos",
        reset: "Desfazer",
        search: "Pesquisar...",
        nothingSelected: "Selecione"         //default-label is deprecated and replaced with this.
      },
      onItemClick: function (data) {

        $scope.receitaModal.model.idFamilia = data.Id;

      },
      onReset: function () {
      }
    }

    $scope.categoriasSelector = {
      inputData: [],
      outputData: [],
      translation: {
        selectAll: "Selecionar todos",
        selectNone: "Desmarcar todos",
        reset: "Desfazer",
        search: "Pesquisar...",
        nothingSelected: "Selecione"         //default-label is deprecated and replaced with this.
      },
      onItemClick: function (data) {

        $scope.receitaModal.model.idCategoria = data.Id;

      },
      onReset: function () {
      }
    }

    configDataTables();

    function _getFamilias() {
      familiaService.getAll().then((resp) => {
        if (resp.status != 200) {
          return;
        }
        $scope.familiaSelector.inputData = resp.data;

        resp.data.map((item) => {
          $scope.familiasById[item.Id] = item.nome;
        });
        rerenderDatatable();
      });
    }
    function _getReceitas() {
      receitaService.getAll().then((resp) => {
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
        $scope.categoriasSelector.inputData = resp.data;
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

    function _untickArray(arr) {
      if (!arr)
        return;

      arr.map((e) => e.ticked = false);
    }

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