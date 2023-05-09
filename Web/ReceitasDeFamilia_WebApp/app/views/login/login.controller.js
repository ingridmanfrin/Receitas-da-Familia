'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('loginCtrl', function ($scope, principal, $state, authToken, usuarioService) {
        $scope.loginModel = {
            username: undefined,
            password: undefined,
            rememberMe: undefined,
            login: _login,
        }

        $scope.cadastroModel = {
            nome: undefined,
            email: undefined,
            senha: undefined,
            confirmacaoSenha: undefined,
            validacaoSenha: {
                valido: ()=>{
                    if(!$scope.cadastroModel.senha || !$scope.cadastroModel.confirmacaoSenha){
                        return false;
                    }

                    return $scope.cadastroModel.senha === $scope.cadastroModel.confirmacaoSenha;
                },
                mensagem: ()=> {
                    if($scope.cadastroModel.validacaoSenha.valido() || !$scope.cadastroModel.confirmacaoSenha){
                        return '';
                    }else{
                        return 'Senhas divergentes.'
                    }
                }
            },
            cadastrarUsuario: _cadastrar,
            valido: ()=>{
                return $scope.cadastroModel.validacaoSenha.valido() && $scope.cadastroModel.nome && $scope.cadastroModel.email;
            }
        }
        $scope.activeForm = 'login';
        $scope.setActiveForm = (formName) => $scope.activeForm = formName;


        _init();

        function _init() {
            if (principal.isIdentityResolved() && principal.isAuthenticated()) {
                $state.go("dashboard.home");
                return;
            }

            let username = localStorage.getItem('user.username');
            if (username) {
                $scope.loginModel.rememberMe = true;
                $scope.loginModel.username = username;
            }
        };
        function _login(loginModel) {
            if (!loginModel || !loginModel.username || !loginModel.password)
                return;

            if (loginModel.rememberMe)
                localStorage.setItem('user.username', loginModel.username);
            else
                localStorage.removeItem('user.username');

            authToken.get(loginModel.username, loginModel.password).then((data) => {
                if (data.status !== 200) {
                    let msg = 'Falha ao Logar, verifique usuário e senha.';
                    toastr.warning("", msg, {
                        "closeButton": true,
                        "newestOnTop": true,
                        "progressBar": true,
                        "preventDuplicates": true,
                    });
                    return;
                }

                let dtExpires = new Date();
                dtExpires.setSeconds(dtExpires.getSeconds() + (120 * 60));// 120 minutes

                var token = {
                    access_token: data.data.token,
                    token_type: 'bearer',
                    expires_in: dtExpires,
                    userName: loginModel.username,
                    'as:client_id': 'WebAngularAppAuth',
                    '.issued': new Date(),
                    '.expires': dtExpires
                }
                let ret = { data: token, status: data.status };
                principal.authenticate(ret.data).then(() => {
                    _handleMenu();
                    $state.go('dashboard.home');
                });
            }, (rejected) => {
                let msg = 'Falha ao Logar, verifique usuário e senha.';
                toastr.warning("", msg, {
                    "closeButton": true,
                    "newestOnTop": true,
                    "progressBar": true,
                    "preventDuplicates": true,
                });
            });
        }

        function _cadastrar(cadastroModel){
            usuarioService.cadastrarUsuario(cadastroModel).then((ret)=>{
                if (ret.status !== 200 && ret.status !== 404) {

                    let msg = 'Falha ao cadastrar.';

                    if(ret.status === 409){
                        msg = 'Email já cadastrado.';
                    }

                    toastr.warning("", msg, {
                        "closeButton": true,
                        "newestOnTop": true,
                        "progressBar": true,
                        "preventDuplicates": true,
                    });
                }else{
                    let msg = 'Cadastrado com sucesso!';
                    toastr.success("", msg, {
                        "closeButton": true,
                        "newestOnTop": true,
                        "progressBar": true,
                        "preventDuplicates": true,
                    });
                    $scope.setActiveForm('login');
                }
            }, (rejected)=>{
                let msg = 'Falha ao cadastrar.';
                toastr.warning("", msg, {
                    "closeButton": true,
                    "newestOnTop": true,
                    "progressBar": true,
                    "preventDuplicates": true,
                });
            });
        }
    });