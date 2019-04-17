//-- ------------------------------------------------------------------------
//SISTEMA : General
//SUBSISTEMA : General
//NOMBRE : serviceGeneral.js
//DESCRIPCIÓN : Service General y unico el cual tiene los dos tipos de Funciones 
//              con llamadas Ajax para la conexion con el Servidor y consumir las funciones
//              que se encuentran en este
//AUTOR : Kevin Ochoa Andrade
//FECHA CREACIÓN : 01/11/2018
//------------------------------------------------------------------------
//FECHA MODIFICACIÓN  EMPLEADO
//--------------------------------------------------------------------
(function () {

    var serviceGeneral = function ($http, $q,$filter) {
        var baseUrl = _URLApiBase;
        var TypeGet = function (Api, Method, _params) {
            var url = baseUrl + Api+"/"+Method;
            return $http.get(url, { params: _params });
        }
        var TypePost = function (Api, Method, _params) {
            return $http({
                method: 'POST',
                url: baseUrl + Api + "/" + Method,
                data: $.param(_params),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            }).success(function (response) {
                return response;
            });
        }
        var valFecha = function (cadenaDate) {
            
            var respuesta = true;
            var arrayVal = cadenaDate.split('/');
            var lastDate = new Date(arrayVal[2], arrayVal[1]+1, 0);
            var maxDay = $filter('date')(lastDate,'dd');
            if (arrayVal.length != 3) {
                respuesta = false;
            }else if (arrayVal[1] > 12 || arrayVal[1] == 0) {
                respuesta = false;
            } else if (arrayVal[0] > maxDay || arrayVal[0] == 0) {
                respuesta = false;
            }else if (arrayVal[2] < 1900) {
                respuesta = false;
            }
            return respuesta;

        }
        var utilitario = {
        
            pantalla: "",
            pnCEEId: 0,
            pnProId: 0,
            tipoPantalla: "",
            titulo: "",
            pnEtaId:0,
                   
        
        }
        var objListaReque = {
            'pnSisId': 0,
            'pnWorId': 0,
            'pcTipReq': '',
            'pnActividadWf': '',
            'pcSolicitanteAbreviatura': '',
            'pcSolicitanteNombres': '',
            'pnDAcCodigo':0,
            'pcWorNombre': '',
            'pnProId': 0,
            'pcProNombre': '',
            'pnActId': 0,
            'pnProActId': 0,
            'pcActNombre': '',
            'pcActSgteNombre': '',
            'pnPAEId': 0,
            'pcPAENombre': '',
            'pnCodigo': 0,
            'pcCodSol': 0,
            'pnSecuenciaId': 0,
            'pcEtaNombre':''
        }

        return {

            TypeGet: TypeGet,
            TypePost: TypePost,
            utilitario: utilitario,
            objListaReque: objListaReque,
            valFecha: valFecha


        };
    };

    angular.module('commonServiceGeneral', []).factory("GeneralService", serviceGeneral);

})();