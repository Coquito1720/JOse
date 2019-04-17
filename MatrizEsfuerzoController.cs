/* ------------------------------------------------------------------------*/
/* SISTEMA : SAR SBP
/* SUBSISTEMA : ModuloVCC
/* NOMBRE : MatrizEsfuerzoController.cs
/* DESCRIPCIÓN : 
/*       LIST_MatrizEsfuerzo
/*       fn_INS_MatrizEsfuerzo
/*       fn_UPD_MatrizEsfuerzo
/*       fn_DEL_MatrizEsfuerzo    
/* AUTOR : MCV
/* FECHA CREACIÓN : 14/03/2019
/* ------------------------------------------------------------------------*/
/* FECHA MODIFICACIÓN  EMPLEADO    
/* ------------------------------------------------------------------------*/
using SES.Entity.BusinessEntity.SAR;
using SES.Entity.BusinessLogic.SAR;
using SES.Entity.GeneralLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SES.WebAPI.Controllers
{
    public class MatrizEsfuerzoController : ApiController
    {
        BLMatrizEsfuerzo MatrizEsfuerzoBL = new BLMatrizEsfuerzo();
        BLLineaDetalle LineaDetalleBL = new BLLineaDetalle();
        BLTipoObjeto TipoObjetoBL = new BLTipoObjeto();
        BLLinea LineaBL = new BLLinea();

        [HttpGet]
        public List<BEMatrizEsfuerzo> LIST_MatrizEsfuerzo(string DBConexion)
        {
            BEMatrizEsfuerzo objBEMat = new BEMatrizEsfuerzo();
            List<BEMatrizEsfuerzo> lstMat = new List<BEMatrizEsfuerzo>();
            try
            {
                objBEMat.DBConexion = DBConexion;
                objBEMat.pcOpcion = "01";
                objBEMat.pcMatEliminado = "0";
                lstMat = MatrizEsfuerzoBL.fnListaMatrizEsfuerzoBL(objBEMat);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return lstMat;
        }



        [HttpGet]
        public List<BEMatrizEsfuerzo> LIST_MatrizEsfuerzoFiltro(int pnMatId, string pcMatNombre, string pcOpcion, int pnLenId, int pnWorId, string pcMatEliminado,string DBConexion)
        {
            BEMatrizEsfuerzo objBEMat = new BEMatrizEsfuerzo();
            List<BEMatrizEsfuerzo> lstMatF = new List<BEMatrizEsfuerzo>();
            try
            {
                objBEMat.pnLenId = pnLenId;
                objBEMat.pnMatId = pnMatId;
                objBEMat.pnWorId = pnWorId;
                objBEMat.pcMatNombre = pcMatNombre;
                objBEMat.pcMatEliminado = pcMatEliminado;
                objBEMat.pcOpcion = pcOpcion; 
                objBEMat.DBConexion = DBConexion;  
                lstMatF = MatrizEsfuerzoBL.fnListaMatrizEsfuerzoBL(objBEMat);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return lstMatF;
        }

        [HttpGet]
        public DataTable LIST_DetalleMatriz(int pnLinDetId, int pnMatId, int pnLinId, int pnTipId, int pnComId, int pnPerId, string pcOpcion, string DBConexion)
        {
            BELineaDetalle objDet = new BELineaDetalle();

            objDet.pnLinDetId = pnLinDetId;
            objDet.pnMatId = pnMatId;
            objDet.pnLinId = pnLinId;
            objDet.pnTipId = pnTipId;
            objDet.pnComId = pnComId;
            objDet.pnPerId = pnPerId;
            objDet.pcOpcion = pcOpcion;
            objDet.DBConexion = DBConexion;
            return LineaDetalleBL.fListaLineaDetalleBL(objDet);
        }

        [HttpGet]
        public List<BETipoObjeto> LIST_TipoObjeto(int pnLenId, int pnTipId, string pcTipNombre, string pcTipEliminado, string pcTipAbreviatura, string pcOpcion, string DBConexion)
        {
            BETipoObjeto objBETip = new BETipoObjeto();
            List<BETipoObjeto> lstTip = new List<BETipoObjeto>();
            try
            {
                objBETip.pnLenId = pnLenId;
                objBETip.pnTipId = pnTipId;
                objBETip.pcTipNombre = pcTipNombre;
                objBETip.pcTipEliminado = pcTipEliminado;
                objBETip.pcTipAbreviatura = pcTipAbreviatura;
                objBETip.pcOpcion = pcOpcion;
                objBETip.DBConexion = DBConexion;
                lstTip = TipoObjetoBL.fListaTipoObjetoBL(objBETip);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return lstTip;
        }

        [HttpGet]
        public List<BELinea> LIST_Linea(int pnMatId, int pnLinId, int pnLinActivo, string pcOpcion,string DBConexion)
        {
            BELinea objBELin = new BELinea();
            List<BELinea> lstLin = new List<BELinea>();
            try
            {
                objBELin.pnMatId = pnMatId;
                objBELin.pnLinId = pnLinId;
                objBELin.pnLinActivo = pnLinActivo;
                objBELin.pcOpcion = pcOpcion;
                objBELin.DBConexion = DBConexion;
                lstLin = LineaBL.fnListaLineaBL(objBELin);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return lstLin;
        }

        [HttpPost]
        public string fn_INS_LineaDetalle(BELineaDetalle objBELin/*int pnLinDetId, int pnComId, int pnLDetValor, int pnLinId, int pnMatId, int pnPerId, int pnTipId, int pcOpcion*/)
        {

            string Respuesta = "";
            try
            {
                BELineaDetalle objLin = new BELineaDetalle();
                objLin.pnLinDetId = objBELin.pnLinDetId;
                objLin.pnComId = objBELin.pnComId;
                objLin.pnLDetValor = objBELin.pnLDetValor;
                objLin.pnLinId = 1;
                objLin.pnMatId = objBELin.pnMatId;
                objLin.pnPerId = objBELin.pnPerId;
                objLin.pnTipId = objBELin.pnTipId;
                objLin.pcOpcion = "01";
                Respuesta = LineaDetalleBL.fMantenimientoLineaDetalleBL(objLin);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Respuesta;
        }

        [HttpPost]
        public string fn_UPD_LineaDetalle(BELineaDetalle objBELin/*int pnLinDetId, int pnComId, int pnLDetValor, int pnLinId, int pnMatId, int pnPerId, int pnTipId, int pcOpcion*/)
        {

            string Respuesta = "";
            try
            {
                BELineaDetalle objLin = new BELineaDetalle();
                objLin.pnLinDetId = objBELin.pnLinDetId;
                objLin.pnComId = objBELin.pnComId;
                objLin.pnLDetValor = objBELin.pnLDetValor;
                objLin.pnLinId = 1;
                objLin.pnMatId = objBELin.pnMatId;
                objLin.pnPerId = objBELin.pnPerId;
                objLin.pnTipId = objBELin.pnTipId;
                objLin.pcOpcion = "02";
                Respuesta = LineaDetalleBL.fMantenimientoLineaDetalleBL(objLin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Respuesta;
        }

        [HttpPost]
        public string fn_INS_MatrizEsfuerzo(BEMatrizEsfuerzo objMat)
        {
            int rpta = 0;
            string Respuesta = "";
            try
            {
            BEMatrizEsfuerzo objEsf = new BEMatrizEsfuerzo();
            objEsf.pnMatId = objMat.pnMatId;
            objEsf.pnLenId = objMat.pnLenId;
            objEsf.pnWorId = objMat.pnWorId;
            objEsf.pcMatNombre = objMat.pcMatNombre;
            objEsf.pcMatEliminado = objMat.pcMatEliminado;
            objEsf.pcOpcion = objMat.pcOpcion;
            objEsf.pnSisId = 3;
            objEsf.DBConexion = objMat.DBConexion;
            rpta = new BLMatrizEsfuerzo().fnINSMatrizEsfuerzoBL(objEsf);
                if (rpta > 0)
                {
                    BELinea objLin = new BELinea();
                    objLin.pnLinId = 0;
                    objLin.pnMatId = rpta;
                    objLin.pcLinFecVigIni = objMat.pcLinFecVigIni;
                    objLin.pcLinFecVigFin = objMat.pcLinFecVigFin;
                    objLin.pnLinActivo = 0;
                    objLin.pcOpcion = objMat.pcOpcion;
                    objLin.DBConexion = objMat.DBConexion;
                    Respuesta = new BLLinea().fnINSMantenimientoLineaBL(objLin);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return Respuesta;
        }

        [HttpPost]
        public string fn_INS_Linea(BELinea Request)
        {

            string Respuesta = "";
            try
            {
                Respuesta = LineaBL.fnINSMantenimientoLineaBL(Request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Respuesta;
        }

        [HttpPost]
        public string fn_UPD_MatrizEsfuerzo(BEMatrizEsfuerzo objMat)
        {
            string rpta = "";
            string Respuesta = "";
            try
            {
                BEMatrizEsfuerzo objEsf = new BEMatrizEsfuerzo();
                objEsf.pnMatId = objMat.pnMatId;
                objEsf.pnLenId = objMat.pnLenId;
                objEsf.pnWorId = objMat.pnWorId;
                objEsf.pcMatNombre = objMat.pcMatNombre;
                objEsf.pcMatEliminado = objMat.pcMatEliminado;
                objEsf.pcOpcion = "02";
                objEsf.pnSisId = 3;
                objEsf.DBConexion = objMat.DBConexion;
                objEsf.pnLinCantidad = objMat.pnLinCantidad;
                rpta = new BLMatrizEsfuerzo().fnUPDMatrizEsfuerzoBL(objEsf);
                if (rpta.Length > 0)
                {
                    BELinea objLin = new BELinea();
                    objLin.pnLinId = objMat.pnLinCantidad;
                    objLin.pnMatId = objMat.pnMatId;
                    objLin.pcLinFecVigIni = objMat.pcLinFecVigIni;
                    objLin.pcLinFecVigFin = objMat.pcLinFecVigFin;
                    objLin.pnLinActivo = 0;
                    objLin.pcOpcion = objMat.pcOpcion;
                    objLin.DBConexion = objMat.DBConexion;
                    Respuesta = new BLLinea().fnINSMantenimientoLineaBL(objLin);
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return Respuesta;
        }

        [HttpPost]
        public string fn_DEL_MatrizEsfuerzo(BEMatrizEsfuerzo objMat)
        {
            string Respuesta = "";
            try
            {
                BEMatrizEsfuerzo ObjBEMat = new BEMatrizEsfuerzo();
                ObjBEMat.pnMatId = objMat.pnMatId;
                ObjBEMat.pcOpcion = "03";
                ObjBEMat.DBConexion = objMat.DBConexion;
                ObjBEMat.pnDetalleMatriz = objMat.pnDetalleMatriz;
                Respuesta = MatrizEsfuerzoBL.fnDELMatrizEsfuerzoBL(ObjBEMat);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Respuesta;
        }

    }
}
