//-- ------------------------------------------------------------------------
//SISTEMA : SAR SBP
//SUBSISTEMA : Fabrica
//NOMBRE : MatrizEsfuerzoController.js
//DESCRIPCIÓN : Realiza la gestión de Matriz de Esfuerzo
//AUTOR : MCV
//FECHA CREACIÓN : 14/03/2019
//------------------------------------------------------------------------
//FECHA MODIFICACIÓN  EMPLEADO
//--------------------------------------------------------------------
app.controller('MntMatrizEsfuerzoController', function (
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
        DTColumnDefBuilder.newColumnDef(7).notSortable()
    ];
    
    $scope.utilitario = serviceGestionRequerimientos.utilitario;
    $scope.utilitario.titulo = $scope.variables;
    $scope.utilitarioGenereal = GeneralService.utilitario;

    $scope.usuario = $cookieStore.get('usuario');
    $scope.Usuario = $cookieStore.get('usuario');
    $scope.registrarReq = serviceGestionRequerimientos.registrarReq;
    $scope.objListaReque = GeneralService.objListaReque;
    $scope.fechaMinima = '';
    $scope.todayDate = FechaActual();
    //$scope.promLen = [];
    $scope.ListarDetalleLinea = [];
    //$scope.existe = [];
    $scope.promValid = [];

    function fnCargarCalendarioSES() {
        var params = {
            'pdFechaInicio': '01/01/2000',
            'pdFechaFin': '01/01/2900',
            'bNoLaborable': true,
            'DBConexion': $scope.usuario.DBConexion
        }
        GeneralService.TypeGet('Sistema', 'LIST_FechasNoLaborables', params)
            .success(function (lista) {
                $scope.ListaCalendarioSES = lista;
            }).error(function (error) {
                console.error(error)
            })
    }

    $scope.objMatriz = {
        pnMatId: $scope.registrarReq.pnMatId,
        pcMatNombre: $scope.registrarReq.pcMatNombre,
        pnLenId: $scope.registrarReq.pnLenId,
        pnWorId: $scope.registrarReq.pnWorId,
        pcLenAbreviatura: $scope.registrarReq.pcLenAbreviatura,
        pcLenNombre: $scope.registrarReq.pcLenNombre,
        pcWorAbreviatura: $scope.registrarReq.pcWorAbreviatura,
        pcWorNombre: $scope.registrarReq.pcWorNombre,
        pcLinFecVigIni: $scope.registrarReq.pcLinFecVigIni,
        pcLinFecVigFin: $scope.registrarReq.pcLinFecVigFin,
        pnLinCantidad: $scope.registrarReq.pnLinCantidad,
        pnProId: $scope.registrarReq.pnProId,
        pcOpcion: '',
        pcRoles: '',
        pnProActId: 0,
        pnRolId: 0,
        pnSecuenciaId: 0,
        pnUsuId: 0,
        pnSisId: $scope.registrarReq.pnSisId,
        pnDetalleMatriz: 0,
        DBConexion: $scope.usuario.DBConexion,
    }

    //Listados
    $scope.ListaMatriz = [];
    $scope.ListadoLenguajes = [];
    $scope.ListadoClasificacion = [];
    $scope.ListaCalendarioSES = [];

    //Combos
    function ComboLengProgramacion() {
        var params = {
            DBConexion: $scope.usuario.DBConexion
        }
        GeneralService.TypeGet('LenguajeProgramacion', 'LIST_LenguajesProgramacion', params)
        .success(function (res) {
            //console.log("Lenguajes");
            $scope.ListadoLenguajes = res
            //console.log(res);

        }).error(function (error) {
            console.log(error);
        });
    }

    function ComboClasificacion() {
        var params = {
            'pnSisId': $scope.usuario.pnSisId,
            'DBConexion': $scope.usuario.DBConexion

        }
        GeneralService.TypeGet('Workflow', 'LIST_Workflow', params)
        .success(function (res) {
            //console.log("Clasificacion");
            $scope.ListadoClasificacion = res;
            //console.log(res);
        }).error(function (error) {
            console.log(error);
        });
    }

    /*-----CONSULTAS--------*/
    $scope.fnConsultar = function () {
        var _parametros = {
            DBConexion:$scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_MatrizEsfuerzo', _parametros)
        .success(function (response) {
            //console.log('Todos');
            $scope.ListarMatriz = response;
            //console.log(response)
        })
        .error(function (error) {
            console.log(error);
        })
    }

    $scope.filtroLeng = { pnLenId: null }
    $scope.filtroWor = { pnWorId: null }

    $scope.fnFiltro = function () {
        var _parametros = {
            pnLenId: ($scope.filtroLeng.pnLenId == null ? 0 : $scope.filtroLeng.pnLenId.pnLenId),
            pnMatId: 0,
            pnWorId: ($scope.filtroWor.pnWorId == null ? 0 : $scope.filtroWor.pnWorId.pnWorId),
            pcMatNombre:'',
            pcMatEliminado: 0,
            pcOpcion: '02',
            DBConexion: $scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_MatrizEsfuerzoFiltro', _parametros)
        .success(function (response) {
            if (response.length > 0) {
                //console.log('Algunos');
                $scope.ListarMatriz = response
                //console.log($scope.ListarMatriz)
            } else {
                fnalert('warning', '¡Aviso!', 'No se encontró registros que coincidan con el criterio de búsqueda')
            }
        })
        .error(function (error) {
            console.log(error);
        })
    }

    $scope.fnConsultarHistorial = function (item) {
        var _parametros = {
            pnMatId: item.pnMatId,
            pnLinId: 0,
            pnLinActivo: -1,
            pcOpcion: '01',
            DBConexion:$scope.usuario.DBConexion,
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_Linea', _parametros)
        .success(function (response) {
            //console.log('---------------');
            $scope.ListarMatrizHistorial = response;
            console.log(response);
        })
        .error(function (error) {
            console.log(error);
        })
    }

    $scope.btnDetalleHistorico = function (item) {
        fnObtenerDatosLista(item);
        $('#modalListadoHistorico').modal('show');
        $scope.fnConsultarHistorial(item);
        $scope.objListaReque.pcOpcion = '01';
        //console.log('aaa');
        //console.log($scope.itemLinea);
    }

    $scope.fnValidarLista = function () {
        var parametros = {
            pnMatId: $scope.objMatriz.pnMatId,
            pnLenId: $scope.filtroLeng.pnLenId.pnLenId,
            pnWorId: $scope.filtroWor.pnWorId.pnWorId,
            pcMatEliminado: $scope.objMatriz.pcMatEliminado,
            pcOpcion: "02"
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_MatrizEsfuerzoFiltro', _parametros)
        .success(function (response) {
                //console.log('Algunos');
                $scope.ListarMatrizValidar = response
                //console.log($scope.ListarMatriz)
        })
        .error(function (error) {
            console.log(error);
        })
    }

    /*---------------------------------------------------*/
    $scope.swMnt = false;
    $scope.swTipoMnt = 0;
    $scope.lock = 0;
    $scope.swVal = 0;
    /*------------PRE-------------*/
    $scope.fnPreRegistrar = function () {
        $scope.swMnt = true;
        $scope.swTipoMnt = 1;/*Op: Registrar*/
        $scope.lock = 1;
        $scope.swVal = 1;
        fnClearMatriz();
        $scope.objMatriz = {
            pcLinFecVigIni:$scope.todayDate,
        }
        document.getElementById('dv-table_length').style.display = 'none';
        document.getElementById('dv-table_filter').style.display = 'none';
        document.getElementById('dv-table_paginate').style.display = 'none';
        document.getElementById('dv-table_info').style.display = 'none';
        Consultar.disabled = true;
    }

    $scope.fnPreEditar = function (obj) {
        $scope.swMnt = true;
        $scope.swTipoMnt = 2;
        $scope.lock = 1;
        $scope.swVal = 2;
        $scope.objMatriz = {
            pnMatId: obj.pnMatId,
            pcMatNombre: obj.pcMatNombre,
            pcLinFecVigIni: $filter('date')(obj.pcLinFecVigIni, 'dd/MM/yyyy'),
            pcLinFecVigFin: $filter('date')(obj.pcLinFecVigFin, 'dd/MM/yyyy'),
            pnLinCantidad: obj.pnLinCantidad,
            pnLenId: obj.pnLenId.toString(),
            pnWorId: obj.pnWorId.toString(),
            pnDetalleMatriz:obj.pnDetalleMatriz,
        }
        if ($scope.objMatriz.pnDetalleMatriz>0) {
            fnalert('warning', 'Aviso!', 'No puede modificar los datos de lenguaje y clasificación por que la matriz está relacionada.');
            document.getElementById('dv-table_length').style.display = 'none';
            document.getElementById('dv-table_filter').style.display = 'none';
            document.getElementById('dv-table_paginate').style.display = 'none';
            document.getElementById('dv-table_info').style.display = 'none';
            dllpnLenId.disabled = true;
            dllpnWorId.disabled = true;
            //console.log($scope.objMatriz.pnDetalleMatriz)
            return;
        }
        document.getElementById('dv-table_length').style.display = 'none';
        document.getElementById('dv-table_filter').style.display = 'none';
        document.getElementById('dv-table_paginate').style.display = 'none';
        document.getElementById('dv-table_info').style.display = 'none';
        //console.log($scope.objMatriz.pnDetalleMatriz)
    }
    
    $scope.fnGuardar = function () {
        //debugger;
        var _parametros = {
            pnMatId: $scope.objMatriz.pnMatId,
            pcMatNombre: $scope.objMatriz.pcMatNombre,
            pnLinId: 0,
            pnLinActivo: 0,
            pcMatEliminado: 0,
            pnLenId: $scope.objMatriz.pnLenId,
            pnWorId: $scope.objMatriz.pnWorId,
            pcLinFecVigIni: $scope.objMatriz.pcLinFecVigIni,
            pcLinFecVigFin: $scope.objMatriz.pcLinFecVigFin,
            DBConexion: $scope.usuario.DBConexion,
            pcOpcion:"01"
        }
        
            $scope.promValid = $q.defer();
            $scope.fnValidacionLenWor($scope.objMatriz.pnLenId, $scope.objMatriz.pnWorId);
            $scope.promValid.promise.then(function () {
                if ($scope.existe == 0) {
                    /**/
                    if ($scope.swTipoMnt == 1) {
                        GeneralService.TypePost('MatrizEsfuerzo', 'fn_INS_MatrizEsfuerzo', _parametros)
                           .success(function (response) {
                                if (response == "OK") {
                                    //$scope.fnGuardar2();
                                    $scope.fnConsultar();
                                    $scope.fnCancelar();
                                    fnalert('success', 'Aviso!', 'Se registro correctamente la matriz');
                                    Consultar.disabled = false;
                                } else {
                                    fnalert('danger', 'Aviso!', 'Error al registrar la matriz');
                                }
                                //console.log("Aqui");
                                //console.log(_parametros);
                                //console.log(response);
                            })
                            .error(function (error) {
                                console.log(error);
                            });
                    }
                    /**/
                } else {
                    fnalert('warning', 'Aviso!', 'Ya existe una MEE relacionada al Lenguaje de Programación y Clasificación elegidos');
                    return;
                }
            })
        
    }

    $scope.btn_ClickSi = function () {
        $scope.objMatriz.pnLinCantidad = 0;
        $scope.objMatriz.pcOpcion = "01";
        $scope.fnValidacion();
    }

    $scope.btn_ClickNo = function () {
        $scope.objMatriz.pcOpcion = "02";
        $scope.fnValidacion();
    }

    $scope.fnActualizar = function () {
        debugger
        var _parametros = {
            pnMatId: $scope.objMatriz.pnMatId,
            pcMatNombre: $scope.objMatriz.pcMatNombre,
            pnLinId: $scope.objMatriz.pnLinCantidad,
            pnLinActivo: 0,
            pcMatEliminado: 0,
            pnLenId: $scope.objMatriz.pnLenId,
            pnWorId: $scope.objMatriz.pnWorId,
            pcLinFecVigIni: $scope.objMatriz.pcLinFecVigIni,
            pcLinFecVigFin: $scope.objMatriz.pcLinFecVigFin,
            DBConexion: $scope.usuario.DBConexion,
            pcOpcion: $scope.objMatriz.pcOpcion,
            pnLinCantidad:$scope.objMatriz.pnLinCantidad,
        }
        $scope.promValid = $q.defer();
        $scope.fnValidacionLenWor($scope.objMatriz.pnLenId, $scope.objMatriz.pnWorId);
        $scope.promValid.promise.then(function () {
            if ($scope.existe == 0) {
                if ($scope.swTipoMnt == 2) {
                    GeneralService.TypePost('MatrizEsfuerzo', 'fn_UPD_MatrizEsfuerzo', _parametros)
                        .success(function (response) {
                            if (response == "OK") {
                                //$scope.fnGuardar2();
                                $scope.fnConsultar();
                                $scope.fnCancelar();
                                fnalert('success', 'Aviso!', 'Registro actualizado correctamente');
                            } else {
                                fnalert('danger', 'Aviso!', 'Error al actualizar el registro');
                            }
                            console.log("Aqui2");
                            console.log(_parametros);
                        })
                        .error(function (error) {
                            console.log(error);
                        });
                }
            } else {
                fnalert('warning', 'Aviso!', 'Ya existe una MEE relacionada al Lenguaje de Programación y Clasificación elegidos');
                return;
            }
        })
    }

    $scope.btnConfirmacion = function (item) {
        if ($scope.swVal == 1) {
            $scope.fnValidacion();
        } else {
            $('#modalConfirmar').modal('show');
        }
    }

    $scope.fnEliminar = function (item) {
        debugger;
            var res = confirm('¿Está seguro que desea eliminar el registro?')
            var _parametros = {
                pnMatId: item.pnMatId,
                DBConexion: $scope.usuario.DBConexion,
                pnDetalleMatriz: item.pnDetalleMatriz,
            };
            if (item.pnDetalleMatriz > 0) {
                fnalert('warning', 'AVISO', 'No puede eliminar porque la Matriz está relacionada.');                
            } else {
                if (res) {
                    GeneralService.TypePost('MatrizEsfuerzo', 'fn_DEL_MatrizEsfuerzo', _parametros)
                    .success(function (response) {
                        if (response == "OK") {
                            $scope.fnConsultar();
                            $scope.fnCancelar();
                            fnalert('success', 'Aviso!', 'Se elimino el registro correctamente');
                            //console.log(_parametros)
                        } else {
                            fnalert('danger', 'Aviso!', 'Error eliminar el registro');
                        }
                    })
                    .error(function (error) {
                        console.log(error);
                    })
                }
            }            
    }

    $scope.fnCancelar = function () {
        $scope.swTipoMnt = 0;
        $scope.lock = 0;
        $scope.swMnt = false;
        document.getElementById('dv-table_length').style.display = 'block';
        document.getElementById('dv-table_filter').style.display = 'block';
        document.getElementById('dv-table_paginate').style.display = 'block';
        document.getElementById('dv-table_info').style.display = 'block';
        Consultar.disabled = false;
    }

    function fnClearMatriz() {
        $scope.objMatriz = {
            pnMatId: 0,
            pcMatNombre: '',
            pcLenAbreviatura: '',
            pnLenId: '',
            pcWorAbreviatura: '',
            pnWorId:'',
            pcLinFecVigIni: '',
            pcLinFecVigFin: '',
            pnLinCantidad: 0
        }
    }
    /*Obtener datos Lista*/
    function fnObtenerDatosLista(item) {
        $scope.objMatriz.pnSisId = item.pnSisId
        $scope.objMatriz.pcMatNombre = item.pcMatNombre
        $scope.objMatriz.pcLenAbreviatura = item.pcLenAbreviatura
        $scope.objMatriz.pcLenNombre = item.pcLenNombre
        $scope.objMatriz.pcWorAbreviatura = item.pcWorAbreviatura
        $scope.objMatriz.pcWorNombre = item.pcWorNombre
        $scope.objMatriz.pnLinCantidad = item.pnLinCantidad
        $scope.objMatriz.pcLinFecVigIni = item.pcLinFecVigIni
        $scope.objMatriz.pcLinFecVigFin = item.pcLinFecVigFin
        $scope.objMatriz.pnMatId = item.pnMatId
        $scope.objMatriz.pnLenId = item.pnLenId
       // console.log('DatosHistorial')
        //console.log(item)
    }

    $scope.detalleMatriz = function (item) {
        fnObtenerDatosLista(item);
        $scope.utilitario.move = '1';
        $scope.utilitario.titulo = 'Matriz Esfuerzo';
        $scope.utilitario.accion = 'Matriz';
        $scope.registrarReq.pnSisId = item.pnSisId;
        $scope.registrarReq.pcMatNombre = item.pcMatNombre;
        $scope.registrarReq.pcLenAbreviatura = item.pcLenAbreviatura;
        $scope.registrarReq.pcLenNombre = item.pcLenNombre;
        $scope.registrarReq.pcWorAbreviatura = item.pcWorAbreviatura;
        $scope.registrarReq.pcWorNombre = item.pcWorNombre;
        $scope.registrarReq.pnLinCantidad = item.pnLinCantidad;
        $scope.registrarReq.pcLinFecVigIni = item.pcLinFecVigIni;
        $scope.registrarReq.pcLinFecVigFin = item.pcLinFecVigFin;
        $scope.registrarReq.pnMatId = item.pnMatId;
        $scope.registrarReq.pnLenId = item.pnLenId;
        //$('#modalListadoHistorico').modal('toggle');
        $('.modal-backdrop').remove();
        //console.log('datos');
        fnClearMatriz();
        $location.path('detalleMatrizEsfuerzo');
        //console.log(item);
    }

    $scope.detalleMatriz2 = function () {
        //debugger
        //fnObtenerDatosLista(item);
        $scope.utilitario.move = '1';
        $scope.utilitario.titulo = 'Matris Esfuerzo';
        $scope.utilitario.accion = 'Matris';
        $scope.registrarReq.pnSisId = $scope.objMatriz.pnSisId;
        $scope.registrarReq.pcMatNombre = $scope.objMatriz.pcMatNombre;
        $scope.registrarReq.pcLenAbreviatura = $scope.objMatriz.pcLenAbreviatura;
        $scope.registrarReq.pcLenNombre = $scope.objMatriz.pcLenNombre;
        $scope.registrarReq.pcWorAbreviatura = $scope.objMatriz.pcWorAbreviatura;
        $scope.registrarReq.pcWorNombre = $scope.objMatriz.pcWorNombre;
        $scope.registrarReq.pnLinCantidad = $scope.objMatriz.pnLinCantidad;
        $scope.registrarReq.pcLinFecVigIni = $scope.objMatriz.pcLinFecVigIni;
        $scope.registrarReq.pcLinFecVigFin = $scope.objMatriz.pcLinFecVigFin;
        $scope.registrarReq.pnMatId = $scope.objMatriz.pnMatId;
        $scope.registrarReq.pnLenId = $scope.objMatriz.pnLenId;
        //$('#modalListadoHistorico').modal('toggle');
        $('.modal-backdrop').remove();
        console.log('datos');
        fnClearMatriz();
        $location.path('detalleMatrizEsfuerzo');
        //console.log(item);
    }

    /*VALIDACION DE DATOS*/
    $scope.fnValidacion = function () {
        var fechaInicio = new Date($scope.objMatriz.pcLinFecVigIni.split('/')[2], $scope.objMatriz.pcLinFecVigIni.split('/')[1], $scope.objMatriz.pcLinFecVigIni.split('/')[0]);
        var fechaFin = new Date($scope.objMatriz.pcLinFecVigFin.split('/')[2], $scope.objMatriz.pcLinFecVigFin.split('/')[1], $scope.objMatriz.pcLinFecVigFin.split('/')[0]);
        $scope.event_cambiarFecha('i');
        if (!$scope.validadorFecha) return;
        $scope.event_cambiarFecha('f');
        if (!$scope.validadorFecha) return;
        if ($scope.objMatriz.pcMatNombre == '') {
            fnalert('warning', '¡Advertencia!', ' Complete los campos obligatorios .');
            return;
        }
        if ($scope.objMatriz.pnLenId == "") {
            fnalert('warning', '¡Advertencia!', ' Complete los campos obligatorios .');
            return;
        }
        if ($scope.objMatriz.pnWorId == "") {
            fnalert('warning', '¡Advertencia!', ' Complete los campos obligatorios .');
            return;
        }
        if ($scope.objMatriz.pcLinFecVigIni == "" || $scope.objMatriz.pcLinFecVigIni == undefined) {
            fnalert('warning', '¡Advertencia!', ' Complete los campos obligatorios .');
            return;
        }
        if ($scope.objMatriz.pcLinFecVigFin == "" || $scope.objMatriz.pcLinFecVigFin == undefined) {
            fnalert('warning', '¡Advertencia!', ' Complete los campos obligatorios .');
            return;
        }
        /*if ($scope.objMatriz.pcLinFecVigIni < $scope.objMatriz.pcLinFecVigFin) {    
            fnalert('warning', '¡Advertencia!', 'La fecha fin debe ser mayor a la fecha inicial.');
            return;
        }*/
        var InicialfechaIni = $scope.objMatriz.pcLinFecVigIni.split('/');
        var InicialfechaFin = $scope.objMatriz.pcLinFecVigFin.split('/');
        /*var InicialnewfechaIni = InicialfechaIni[0] + '-' + InicialfechaIni[1] + '-' + InicialfechaIni[2];
        var InicialnewfechaFin = InicialfechaFin[0] + '-' + InicialfechaFin[1] + '-' + InicialfechaFin[2];*/

        var InicialnewfechaIni2 = InicialfechaIni[2] + '-' + InicialfechaIni[1] + '-' + InicialfechaIni[0];
        var InicialnewfechaFin2 = InicialfechaFin[2] + '-' + InicialfechaFin[1] + '-' + InicialfechaFin[0];

        var InicialdiaIni = new Date(InicialnewfechaIni2);
        var InicialdiaFin = new Date(InicialnewfechaFin2);

        var DateMinmo = new Date('01/01/2005');
        if (DateMinmo > InicialdiaIni) {
            fnalert('warning', '¡Advertencia!', ' La fecha de Inicio minimo es "01/01/2015" ');
            return;
        }
        if (InicialdiaIni > InicialdiaFin) {
            fnalert('warning', '¡Advertencia!', ' La fecha de Inicio no puede ser mayor la fecha Fin. ');
            return;
        }
        $scope.objMatriz.pcLinFecVigIni = InicialnewfechaIni2;
        $scope.objMatriz.pcLinFecVigFin = InicialnewfechaFin2;
        $scope.objMatriz.pnUsuId = $scope.usuario.pnUsuId;
        $scope.objMatriz.DBConexion = $scope.usuario.DBConexion;
        $scope.objMatriz.pnSisId = $scope.usuario.pnSisId;
        if ($scope.swVal == 1) {
            $scope.fnGuardar();
        } else {
            $scope.fnActualizar();
        }
    }

    $scope.event_cambiarFecha = function (val) {

        $scope.validadorFecha = true;
        if (val == "i") {   
            if (!GeneralService.valFecha($scope.objMatriz.pcLinFecVigIni) && $scope.objMatriz.pcLinFecVigIni.length == 10) {
                fnalert('warning', 'AVISO', 'La fecha Inicial de Planificación debe tener el formato correcto: "01/01/1900"');
                $scope.validadorFecha = false;
                return;
            }
        } else if (val == "f") {
            if (!GeneralService.valFecha($scope.objMatriz.pcLinFecVigFin) && $scope.objMatriz.pcLinFecVigFin.length == 10) {
                fnalert('warning', 'AVISO', 'La fecha Fin de Planificación debe tener el formato correcto: "01/01/1900"');
                $scope.validadorFecha = false;
                return;
            }
        }
        if ($scope.objMatriz.pcLinFecVigIni.length == 10 && $scope.objMatriz.pcLinFecVigFin.length == 10) {
            var newDateI = new Date($scope.objMatriz.pcLinFecVigIni.split('/')[2], $scope.objMatriz.pcLinFecVigIni.split('/')[1], $scope.objMatriz.pcLinFecVigIni.split('/')[0]);
            var newDateF = new Date($scope.objMatriz.pcLinFecVigFin.split('/')[2], $scope.objMatriz.pcLinFecVigFin.split('/')[1], $scope.objMatriz.pcLinFecVigFin.split('/')[0]);
            if (newDateI >= newDateF) {
                fnalert('warning', 'AVISO', 'La fecha Inicio no puede ser mayor a la fecha Fin de Planificación');
                $scope.validadorFecha = false;
                return;
            }
        }
        $.each($scope.ListaCalendarioSES, function (x, valx) {
            if (valx.FechaF == $scope.objMatriz.pcLinFecVigIni) {
                fnalert('warning', 'AVISO', 'La fecha Inicio debe ser una fecha laborable.')
                return;
            }
            if (valx.FechaF == $scope.objMatriz.pcLinFecVigFin) {
                fnalert('warning', 'AVISO', 'La fecha Fin debe ser una fecha laborable.')
                return;
            }
        })
    }

    $scope.fnValidacionLenWor = function (pnLenId,pnWorId) {
        var valid = {
            pcOpcion: '02',
            pnLenId: pnLenId,
            pnWorId: pnWorId,
            pcMatEliminado: 0,
            DBConexion: $scope.usuario.DBConexion,
            pnMatId: 0,
            pcMatNombre: ''
        }
        GeneralService.TypeGet('MatrizEsfuerzo', 'LIST_MatrizEsfuerzoFiltro', valid)
            .success(function (response) {
                $scope.existe = response;
                $scope.promValid.resolve();
            })
            .error(function (error) {
                console.log(error);
            })
    }
    /**************************************************************************/
    function FechaActual() {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0
        var yyyy = today.getFullYear();

        if (dd < 10) dd = '0' + dd
        if (mm < 10) mm = '0' + mm

        today = dd + '/' + mm + '/' + yyyy

        return today;
    }
    /**************************************************************************/
    $scope.fnIniciar = function () {
        cargarCombos();
        $scope.fnConsultar();
        fnCargarCalendarioSES();
    }
    function cargarCombos() {
        ComboLengProgramacion();
        ComboClasificacion();
    }
    
    $scope.indexReporte = 0;
    $scope.test = function () {
        $scope.indexReporte = $("#combdd")[0].value;
        alert($scope.indexReporte);
    }
});