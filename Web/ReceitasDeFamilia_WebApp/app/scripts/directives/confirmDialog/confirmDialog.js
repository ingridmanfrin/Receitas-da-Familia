'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('sbAdminApp')
	.directive('confirmDialog', ['$q', '$timeout', function ($q, $timeout) {
		return {
			templateUrl: 'scripts/directives/confirmDialog/confirmDialog.html',
			restrict: 'E',
			replace: true,
			scope: {
				onClose: "=",
				onSubmit: "=",
				init: '='
			},
			link: link
		}
		function link(scope, element, attrs) {
			let ctrl = {};
			let config = {
				message: {
					type: 'text'
				},
				buttons: {
					close:{
						label: "Cancelar",
						hide: false,
					},
					confirm:{
						label: "Salvar",
						hide: false,
					}
				}
			};

			scope.confirmModal = {
				config: Object.assign({}, config)
			};

			_initModalZIndexControl();

			scope.confirmModal.getButtonConfig = _getButtonConfig;

			ctrl.onClose = (typeof scope.onClose === 'function') ? scope.onClose : () => { };
			ctrl.onSubmit = (typeof scope.onSubmit === 'function') ? scope.onSubmit : () => { };
			ctrl.deferred = null;

			ctrl.show = function (title, message, config) {

				ctrl.deferred = $q.defer();

				scope.confirmModal.title = title;
				scope.confirmModal.message = message;
				if (config)
					scope.confirmModal.config = Object.assign(scope.confirmModal.config, config);

				$('#confirmModal').modal('toggle');
				return ctrl.deferred.promise;
			};

			scope.confirmModal.onClose = function () {
				ctrl.onClose();
				$('#confirmModal').modal('toggle');
				ctrl.deferred.resolve(false);
				$timeout(() => {
					scope.confirmModal.config = Object.assign({}, config);
				},500);
			};
			scope.confirmModal.onSubmit = function () {
				ctrl.onSubmit();
				$('#confirmModal').modal('toggle');
				ctrl.deferred.resolve(true);
				$timeout(() => {
					scope.confirmModal.config = Object.assign({}, config);
				},500);
			};

			scope.$watch('init', function (newVal, oldVal) {
				// if (oldVal !== newVal)
				// 	return;
				// if (newVal.quantity <= 0)
				//     newVal.quantity = 1;

				if (typeof newVal === 'function') {
					newVal(ctrl);
				}
			}, true);

			function _getButtonConfig(buttonName) {
				if (!scope.confirmModal.config || !scope.confirmModal.config.buttons)
					return;

				if (scope.confirmModal.config.buttons.hasOwnProperty(buttonName))
					return scope.confirmModal.config.buttons[buttonName];
			}

			

		};
	}]);


