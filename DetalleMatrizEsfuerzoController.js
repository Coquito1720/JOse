//-- ------------------------------------------------------------------------
//SISTEMA : SAR SBP
//SUBSISTEMA : Fabrica
//NOMBRE : DetalleMatrizEsfuerzoController.js
//DESCRIPCIÓN : Realiza la gestión Detalle de Matriz de Esfuerzo
//AUTOR : MCV
//FECHA CREACIÓN : 14/03/2019
//------------------------------------------------------------------------
//FECHA MODIFICACIÓN  EMPLEADO
//--------------------------------------------------------------------
app.controller('MntDetalleMatrizEsfuerzoController', function (
    $scope,
    $http,
    $cookieStore,
    $location,
    $filter,
    $q,
    GeneralService,
    serviceGestionRequerimientos,
    DTOptionsBuilder,
    DTColumnDefBuilder,
    DTDefaultOptions
    ) {

    $scope.dtOptions = DTOptionsBuilder.newOptions()
        .withOption('lengthMenu', [[10, 20, 30], [10, 20, 30]])
        .withDisplayLength(10);

    $scope.dtColumnDefs = [
        DTColumnDefBuilder.newColumnDef(1).notSortable()
    ];

    $scope.registrarReq = serviceGestionRequerimientos.registrarReq;
    $scope.objEditRequerimiento = GeneralService.objListaReque;
    $scope.objListaReque = GeneralService.objListaReque;
    $scope.objMatriz = GeneralService.objMatriz;
    $scope.usuario = $cookieStore.get('usuario');
    $scope.Usuario = $cookieStore.get('usuario');
    $scope.lstComboComplejidad = [];
    $scope.ListadoPerfiles = [];
    $scope.ListarDetalleMatriz = [];
    $scope.ListarDetalleLinea = [];
    $scope.lockObj = 0;
    $scope.promValida = [];
    
    /*Listado*/
    $scope.fnConsultar = function () {
        var _parametros = {
            pcOpcion: '06',
            DBConexion: $scope.Usuario.DBConexion
        }
        GeneralService.TypeGet('CuadroEsfuerzo', 'LIST_PerfilTecnico', _parametros)
        .success(function (response) {
            //console.log('Perfiles')
            $scope.ListarPerfilTecnico = response
            //console.log(response)
        })
        .error(function (error) {
            console.log(error);
        })
    }
   $scope.indexComplejidad = 0;
    $scope.getindexComplejidad = function () {
        $scope.indexComplejidad = $("#ddlComplejidad")[0].selectedIndex;
        //alert($scope.indexComplejidad);
        if ($scope.indexComplejidad==0) {
            fnalert('warning', '¡Aviso!', 'Debe elegir una complejidad')
        }
    }
    $scope.fnDetalleMatriz = function () {
        debugger;
        $scope.getindexComplejidad();
        var _parametros = {
            pnLinDetId: 0,
            pnMatId: $scope.registrarReq.pnMatId,
            pnLinId: 1,
            pnTipId: $scope.objeto.pnTipId,
            pnComId: $scope.objMatriz.pnComId,
            pnPerId: 0,
            pcOpcion: '01',
            DBConexion:$scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_DetalleMatriz', _parametros)
        .success(function (response) {
                console.log('Detalle Matriz');
                $scope.ListarDetalleMatriz = response;
                if (response.length > 0) {
                    for (i = 0; i < response.length; i++) {
                        delete response[i].nMatId;
                        delete response[i].nLinId;
                        delete response[i].nComId;
                        delete response[i].cComNombre;
                }
                console.log(response);
            } else {
                fnalert('warning', '¡Aviso!', 'No existe tipos de objetos registrados en la complejidad seleccionada.')
            }
        })
        .error(function (error) {
            console.log(error);
        })
    }
   /***************************************************************************************************/
    function fnCargaComplejidad() {
        var params = {};
        params = {
            pcComEliminado: '0',
            pcOpcion: '01',
            DBConexion: $scope.usuario.DBConexion
        };
        GeneralService.TypeGet('RegistroHoras', 'CHARGE_ComboComplejidad', params)
            .success(function (carga) {

                if (carga.length > 0) {
                    $scope.lstComboComplejidad = carga;
                }
                else { }
            })
    }

    function fnCargaObjetos() {
        var params = {};
        params = {
            pnLenId: $scope.registrarReq.pnLenId,
            pnTipId: 0,
            pcTipNombre: '',
            pcTipEliminado: '0',
            pcTipAbreviatura:'',
            pcOpcion: '01',
            DBConexion: $scope.usuario.DBConexion
        };
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_TipoObjeto', params)
        .success(function (response) {
            //console.log('Objetos');
            $scope.ListadoObjetos = response;
            //console.log(response)
        })
        .error(function (error) {
            console.log(error);
        })

    }
    /*Objeto*/
    $scope.objeto = {
        pnTipId: 0,
        pcObjeto: '',
    }

    $scope.nuevoObjeto = function (item) {
        $scope.objeto = {
            pnTipId: item.nTipId,
            pcObjeto: item.Objeto,
        }
        //debugger;
        $scope.btnDetallePerfil();
        //console.log('item');
        //console.log(item);
    }
    /*Registro Detalle*/
    $scope.registrarDetalle = function (item) {
        if ($scope.indexComplejidad == 0) {
            fnalert('warning', '¡Aviso!', 'Debe elegir una complejidad')
        } else {
            $('#modalPerfilTecnico2').modal('show');
            $scope.objListaReque.pcOpcion = '01';
        }
    }

    $scope.detalleLinea = function (item) {
        //debugger;
        var _parametros = {
            pnLinDetId: 0,
            pnMatId: $scope.registrarReq.pnMatId,
            pnLinId: 1,
            pnTipId: $scope.nuevoObjeto.objeto.pnTipId,
            pnComId: $scope.objMatriz.pnComId,
            pnPerId: 0,
            pcOpcion: '02',
            DBConexion:$scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_DetalleMatriz', _parametros)
        .success(function (response) {
            //console.log('Detalle Linea');
            $scope.ListarDetalleLinea = response;
            //console.log(response);
        })
        .error(function (error) {
            console.log(error);
        })
    }
    /*LLamado de HTML*/
    $scope.btnMatrizEsfuerzo = function () {
        $location.path('MatrizEsfuerzo');
        fnClearDetalle();
    }

    $scope.btnDetallePerfil = function (item) {
        $scope.lockObj = 1;
        $scope.detalleLinea2(item);
        $('#modalPerfilTecnico').modal('show');
        $scope.objListaReque.pcOpcion = '01';
    }

    $scope.detalleLinea2 = function (item) {
        //debugger;
        var _parametros = {
            pnLinDetId: 0,
            pnMatId: $scope.registrarReq.pnMatId,
            pnLinId: 1,
            pnTipId: $scope.objeto.pnTipId,
            pnComId: $scope.objMatriz.pnComId,
            pnPerId: 0,
            pcOpcion: '02',
            DBConexion: $scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_DetalleMatriz', _parametros)
        .success(function (response) {
            //console.log('Detalle Linea2');
            $scope.ListarDetalleLinea = response;
            //console.log(_parametros);
        })
        .error(function (error) {
            console.log(error);
        })
    }

    $scope.indexObjeto = 0;
    $scope.getindexObjeto = function () {
        $scope.indexObjeto = $("#objeto2")[0].selectedIndex;
        //alert($scope.indexComplejidad);
        if ($scope.indexObjeto == 0) {
            fnalert('warning', '¡Aviso!', 'Debe elegir un tipo de objeto')
        } else {
            $scope.fnAsociar();
        }
    }
    //$scope.Valor = 0;
    $scope.fnAsociar = function () {
            //debugger;
            var cont = 0;
            var indexPerfiles = $scope.ListarDetalleLinea.length;
            //console.log('Numero');
            console.log(indexPerfiles);
            console.log($scope.ListarDetalleLinea)

            $scope.promValida = $q.defer();
            $scope.fnValidacionObj($scope.nuevoObjeto.objeto.pnTipId);
            $scope.promValida.promise.then(function () {
                if ($scope.existe == 0) {
                    for (var i = 0; i < indexPerfiles; i++) {
                        //$scope.Valor = $scope.registrarReq.pnLDetValor;
                        $scope.fnGuardar(i);
                        cont++;
                    } if (cont > 1) {
                        fnalert('success', 'Aviso!', 'Se registro satisfactoriamente');
                    } else {
                        fnalert('danger', 'Aviso!', 'Error al registrar el detalle');
                    }
                } else {
                    fnalert('warning', 'Aviso!', 'El tipo de objeto ya ha sido registrado anteriormente');
                }
            })
            //fnClearDetalle2();
    }

    $scope.fnGuardar = function (index) {
        //debugger;
        var _parametros = {
            pnLinDetId: 0,
            pnComId: $scope.objMatriz.pnComId,
            pnLDetValor: $scope.ListarDetalleLinea[index].nLDetValor,//$scope.Valor,$scope.registrarReq.pnLDetValor,,,$scope.ListarDetalleLinea[index].nLDetValor
            pnLinId: 1,
            pnMatId: $scope.registrarReq.pnMatId,
            pnPerId: $scope.ListarDetalleLinea[index].nPerId,
            pnTipId: $scope.nuevoObjeto.objeto.pnTipId,
            pcOpcion: "01",
        }
                GeneralService.TypePost('MatrizEsfuerzo', 'fn_INS_LineaDetalle', _parametros)
                    .success(function (response) {
                       if (response) {
                           $scope.fnDetalleMatriz($scope.objMatriz.pnComId);
                       } 
                       //console.log("Aqui");
                       //console.log(response);
                    })
                    .error(function (error) {
                    console.log(error);
                    });
    }

    $scope.fnAsociar2 = function () {
        //debugger;
        var cont = 0;
        var indexPerfiles = $scope.ListarDetalleLinea.length;
        //console.log('Numero');
        console.log(indexPerfiles);
        console.log($scope.ListarDetalleLinea)
                for (var i = 0; i < indexPerfiles; i++) {
                    //$scope.Valor = $scope.registrarReq.pnLDetValor;
                    $scope.fnActualizar(i);
                    cont++;
                } if (cont > 1) {
                    fnalert('success', 'Aviso!', 'Se actualizo satisfactoriamente');
                } else {
                    fnalert('danger', 'Aviso!', 'Error al actualizar el detalle');
                }
        //fnClearDetalle2();
    }

    $scope.fnActualizar = function (index) {
        debugger;
        var _parametros = {
            pnLinDetId: 0,
            pnComId: $scope.objMatriz.pnComId,
            pnLDetValor: $scope.ListarDetalleLinea[index].nLDetValor,//$scope.Valor,$scope.registrarReq.pnLDetValor,,,$scope.ListarDetalleLinea[index].nLDetValor
            pnLinId: 1,
            pnMatId: $scope.registrarReq.pnMatId,
            pnPerId: $scope.ListarDetalleLinea[index].nPerId,
            pnTipId: $scope.objeto.pnTipId,
            pcOpcion: "02",
        }
        GeneralService.TypePost('MatrizEsfuerzo', 'fn_UPD_LineaDetalle', _parametros)
            .success(function (response) {
                if (response) {
                    $scope.fnDetalleMatriz($scope.objMatriz.pnComId);
                }
                console.log("Aqui");
                console.log(response);
            })
            .error(function (error) {
                console.log(error);
            });
    }

    $scope.fnValidacionObj = function (pnTipId) {
        var valid = {
            pnLinDetId: 0,
            pnMatId: $scope.registrarReq.pnMatId,
            pnLinId: 1,
            pnTipId: pnTipId,
            pnComId: $scope.objMatriz.pnComId,
            pnPerId: 0,
            pcOpcion: '03',
            DBConexion: $scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_DetalleMatriz', valid)
            .success(function (response) {
                $scope.existe = response;
                $scope.promValida.resolve();
            })
            .error(function (error) {
                console.log(error);
            })
    }
    /******************************************************/
     $scope.fnCerrar = function() {
         $('#modalPerfilTecnico2').modal('toggle');
         $scope.nuevoObjeto.objeto.pnTipId = '';
         $scope.ListarDetalleLinea = [];
    }

    function fnClearDetalle() {
        $scope.objMatriz = {
            pnComId: ''
        }
    }

    $scope.fnIniciarD = function () {
        cargarCombos();
        $scope.fnConsultar();     
        //cargarDatos();
    }

    function cargarCombos() {
        fnCargaComplejidad();
        fnCargaObjetos();
    }

});
