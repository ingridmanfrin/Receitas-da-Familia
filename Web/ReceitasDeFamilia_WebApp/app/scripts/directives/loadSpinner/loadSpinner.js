'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('sbAdminApp')
	.directive('loadSpinner',function(){
		return {
        templateUrl:'scripts/directives/loadSpinner/loadSpinner.html',
        restrict: 'E',
        replace: true,
    	}
	});


