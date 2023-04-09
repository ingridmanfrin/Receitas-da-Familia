'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
  .controller('usuarioCtrl', function ($scope, $state, $filter, usuarioService, DTOptionsBuilder, DTColumnDefBuilder, $timeout) {
    $scope.usuario = undefined;
    let atualizaUser = (data)=>{
      $scope.usuario = data;
      $scope.usuario.createdDatetime = $filter('date')(new Date($scope.usuario.createdDatetime), "dd-MM-yyyy - hh:mm:ss");
      $scope.usuario.lastEditDatetime = $filter('date')(new Date($scope.usuario.lastEditDatetime), "dd-MM-yyyy - hh:mm:ss");
    }
    usuarioService.getUsuario().then((ret) => {
      if (ret.status === 200) {
        atualizaUser(ret.data);
      }

      $scope.update = () => {
        usuarioService.atualizarUsuario($scope.usuario).then((resp) => {
          if(resp.status === 200){
            atualizaUser(resp.data);
            let msg = 'Usuário atualizado com sucesso.';
            toastr.success("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
            });
          }else{
            let msg = 'Falha ao atualizar usuário.';
            toastr.error("", msg, {
                "closeButton": true,
                "newestOnTop": true,
                "progressBar": true,
                "preventDuplicates": true,
            });
          }
        });
      }
    });
  });