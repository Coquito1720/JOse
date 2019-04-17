using SES.Entity.BusinessEntity.SAR;
using SES.Entity.BusinessLogic.SAR;
using System;
using SES.Entity.GeneralLayer;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;

namespace SES.WebAPI.Controllers
{
    public class CuadroEsfuerzoController : ApiController
    {

        [HttpGet]
        public BECEE VAL_CEE(int pnProActId, int pnProId, string pcOpcion,string DBConexion) {
         
            BECEE objResultado = new BECEE();

            BECEE objBE = new BECEE();
            try
            {
                objBE.pnProActId = pnProActId;
                objBE.pnProId = pnProId;
                objBE.pcOpcion = pcOpcion;
                objBE.DBConexion = DBConexion;
                objResultado = new BLCEE().fValidarCEEBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objResultado;
        }

        [HttpGet]
        public List<BECEEActividad> LIST_ListaActividades(int pnCEEId, int pnCEEEtapaId, string pcOpcion,string DBConexion) {
            List<BECEEActividad> objLista = new List<BECEEActividad>();
            BECEEActividad objBE = new BECEEActividad();
            try
            {
                objBE.pnCEEId = pnCEEId;
                objBE.pnCEEEtapaId = pnCEEEtapaId;
                objBE.pcOpcion = pcOpcion;
                objBE.DBConexion = DBConexion;
                objLista = new BLCEEActividad().fListaCEEActividadsBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpGet]
        public List<BECEEEtapa> LIST_ListaEtapas(int pnCEEId, int pnCEEEtapaId, string pcOpcion, int pbInt,string DBConexion)
        {
            List<BECEEEtapa> objLista = new List<BECEEEtapa>();
            BECEEEtapa objBE = new BECEEEtapa();
            try
            {
                objBE.pnCEEId = pnCEEId;
                objBE.pnCEEEtapaId = pnCEEEtapaId;
                objBE.pcOpcion = pcOpcion;
                objBE.pbInt = pbInt;
                objBE.DBConexion = DBConexion;
                objLista = new BLCEEEtapa().fListaCEEEtapasBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpGet]
        public string VAL_RegistroDesarrollo(int pnEtapaId, int pnCEEId,string DBConexion) {

            string resultado = "OK";
            try
            {
                BECEEEtapa objBECEEEtapa = new BLCEEEtapa().fLeerCEEPrimeraEtapaBL(pnCEEId, DBConexion);
                BECEEEtapa objBERespuesta;
                if (pnEtapaId != objBECEEEtapa.pnCEEEtapaId)
                {
                    objBECEEEtapa.pnCEEId = pnCEEId;
                    objBECEEEtapa.pnCEEEtapaId = objBECEEEtapa.pnCEEEtapaId;
                    objBECEEEtapa.pcOpcion = "01";
                    objBECEEEtapa.DBConexion = DBConexion;
                    objBERespuesta = new BLCEEEtapa().fLeerCEEEtapaBL(objBECEEEtapa);
                    if (objBERespuesta.pnCEEEtapaId != 0)
                    {
                        if (objBERespuesta.pnCEEEtapaTotHoras == 0) resultado = "No puede pasar a otra etapa sin antes haber registrado alguna actividad en " + objBERespuesta.pcEtaNombre;                        
                        else resultado = "OK";                        
                    }
                    else resultado = "NO";                    
                }
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }

        [HttpGet]
        public BECEEEtapa OBT_TotalHoras(int pnCEEEtapaId, int pnCEEId, string pcOpcion,string DBConexion)
        {
            BECEEEtapa objBECEEEtapa = new BECEEEtapa();
            BECEEEtapa objBERespuesta;
            try
            {
                objBECEEEtapa.pnCEEId = pnCEEId;
                objBECEEEtapa.pnCEEEtapaId = pnCEEEtapaId;
                objBECEEEtapa.pcOpcion = pcOpcion;
                objBECEEEtapa.DBConexion = DBConexion;
                objBERespuesta = new BLCEEEtapa().fLeerCEEEtapaBL(objBECEEEtapa);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objBERespuesta;
        }

        [HttpGet]
        public List<BEPerfilTecnico> LIST_PerfilTecnico(string pcOpcion,string DBConexion)
        {
            BEPerfilTecnico objBE = new BEPerfilTecnico();
            List<BEPerfilTecnico> objLista = null;
            try
            {
                objBE.strOpcion = pcOpcion;
                objBE.DBConexion = DBConexion;
                objLista = new BLPerfilTecnico().fListaPerfilTecnicoBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpGet]
        public BELineaDetalle VALOR_Linea(int pnWkfId, int pnLenId, int pnTipId, int pnComId, int pnPerId, string pcMateEliminado, string pcOpcion, string DBConexion) {
            BELineaDetalle objBE = new BELineaDetalle();
            BELineaDetalle objRespuesta;
            try
            {
                objBE.pnWorId = pnWkfId;
                objBE.pnLenId = pnLenId;
                objBE.pnTipId = pnTipId;
                objBE.pnTipId = pnTipId;
                objBE.pnComId = pnComId;
                objBE.pnPerId = pnPerId;
                objBE.pcMatEliminado = pcMateEliminado;
                objBE.pcOpcion = pcOpcion;
                objBE.DBConexion = DBConexion;
                objRespuesta = new BLLineaDetalle().fObtenerValorBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objRespuesta;
        }

        [HttpPost]
        public string MNT_Actividades(BECEEActividad objBE) {
            string resultado = "";
            try
            {
                resultado = new BLCEEActividad().fMantenimientoCEEActividadBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }

        [HttpPost]
        public string MNT_ActividadesOtros(Json_BECEEActividad objBE) {
            string resultado = "";
            try
            {
                int contadorCorrectos = 0;
                List<BECEEActividad> objLista = (List<BECEEActividad>)objBE.lstBECEEActividad;
                BECEEActividad objBEAct = objBE.objBECEEActividad;

                int cantLista = objLista.Count();

                for (int i = 0; i < cantLista; i++)
                {
                    BECEEActividad objBEMan = new BECEEActividad();
                    objBEMan.pnCEEId = objLista[i].pnCEEId;
                    objBEMan.pnCEEEtapaId = objLista[i].pnCEEEtapaId;
                    objBEMan.pnEtapaActividadId = objLista[i].pnEtapaActividadId;
                    objBEMan.pbAplica = objLista[i].pbAplica;
                    objBEMan.pnHoras = objLista[i].pnHoras;
                    objBEMan.pnPorcentaje = objLista[i].pnPorcentaje;
                    objBEMan.pcNombreActividad = objLista[i].pcNombreActividad;
                    objBEMan.pcMotivoModif = objLista[i].pcMotivoModif;
                    objBEMan.DBConexion = objLista[i].DBConexion;
                    if (objBEMan.pnCEEId == 0)
                    {
                        if (objBEAct.pnCEEId == 0) break;                        
                        else
                        {
                            objBEMan.pnCEEId = objBEAct.pnCEEId;
                            objBEMan.pcOpcion = "05";
                            resultado = new BLCEEActividad().fMantenimientoCEEActividadBL(objBEMan);
                            if (resultado == "OK") contadorCorrectos++;                            
                        }
                    }
                    else
                    {
                        objBEMan.pcOpcion = "02";
                        resultado = new BLCEEActividad().fMantenimientoCEEActividadBL(objBEMan);
                        if (resultado == "OK") contadorCorrectos++;                        
                    }
                }
                if (contadorCorrectos == cantLista) resultado = "OK";                
                else resultado = "Ha habido un fallo en la operación";                
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }

        [HttpGet]
        public List<BECEEActividad> LIST_ActividadesXEtapaDetalle(int pnCEEId, int pnEtapaId, int pbInt, int PageNumber,string DBConexion) {
            List<BECEEActividad> objLista;
            BECEEActividad objBE = new BECEEActividad();
            try
            {
                objBE.pnCEEId = pnCEEId;
                objBE.pnCEEEtapaId = pnEtapaId;
                objBE.pbInt = pbInt;
                objBE.PageNumber = PageNumber;
                objBE.DBConexion = DBConexion;
                objBE.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PAGESIZE"].ToString());

                objLista = new BLCEEActividad().fListaActvidaXEtapaBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpGet]
        public List<BECEE> LIST_CondicionesSupuestos(int pnProId, int pnOpcion,string DBConexion) {
            List<BECEE> objLista;
            BECEE objBE = new BECEE();
            try
            {
                objBE.pnProId = pnProId;
                objBE.pnOpcion = pnOpcion;
                objBE.DBConexion = DBConexion;
                objLista = new List<BECEE>();
                objLista = new BLCEE().fCargaCondSupuestosBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpPost]
        public string MNT_CondicionesSupuestos(BECEE objBE) {

            string resultado = "";
            try
            {
                resultado = new BLCEE().fInsertaCondSupuestosBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }


        [HttpPost]
        public string MNT_GrabarActividadesXEtapa(Json_BECEEActividad objJson) {

            string resultado = "";
            try
            { 
                int cantLista = 0;
                int cantCorrecto = 0;
                List<BECEEActividad> objListaMan = (List<BECEEActividad>)objJson.lstBECEEActividad;
                BECEEActividad objBEMan = objJson.objBECEEActividad;
                cantLista = objListaMan.Count();

                for (int i = 0; i < cantLista; i++) {
                    BECEEActividad objBE = new BECEEActividad();
                    objBE = objListaMan[i];
                    objBE.pnCEEId = objBEMan.pnCEEId;
                    objBE.pnCEEEtapaId = objBEMan.pnCEEEtapaId;
                    objBE.pcOpcion = "06";
                    objBE.DBConexion = objBEMan.DBConexion;
                    resultado = new BLCEEActividad().fMantenimientoCEEActividadBL(objBE);
                    if (resultado == "OK") cantCorrecto++;
                }

                if (cantCorrecto == cantLista) resultado = "OK";            
                else resultado = "Ha ocurrido un error en la operación";  
                      
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }

        [HttpPost]
        public string MNT_FechaEtapas(Json_BECEEEtapa objJson) {
            string resultado = "";
            try
            {
                int cantLista = 0;
                int cantCorrecto = 0;
                List<BECEEEtapa> objListaMan = (List<BECEEEtapa>)objJson.lstBECEEEtapa;
                BECEEEtapa objBEMan = objJson.objBECEEEtapa;
                cantLista = objListaMan.Count();

                for (int i = 0; i < cantLista; i++)
                {
                    BECEEEtapa objBE = new BECEEEtapa();
                    objBE = objListaMan[i];
                    objBE.pnCEEId = objBEMan.pnCEEId;
                    objBE.DBConexion = objBEMan.DBConexion;
                    objBE.pcOpcion = "02";
                    resultado = new BLCEEEtapa().fMantenimientoCEEEtapaBL(objBE);
                    if (resultado == "OK") cantCorrecto++;
                }

                if (cantCorrecto == cantLista) resultado = "OK";                
                else resultado = "Ha ocurrido un error en la operación";                
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return resultado;
        }

        [HttpGet]
        public DataTable OBT_ReporteCEE(int pnProId,int pnCEEId,string DBConexion) {
            BECEEActividad objBE = new BECEEActividad();
            DataTable dt;
            try
            {
                objBE.pnProId = pnProId;
                objBE.pnCEEId = pnCEEId;
                objBE.DBConexion = DBConexion;
                dt = new BLCEEActividad().fReporteCEEBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return dt;
        }

        [HttpGet]
        public int OBT_EstadoCEE(int pnCEEId,string DBConexion) {

            int respuesta = 0;
            try
            {
                respuesta = new BLCEEActividad().fEstadoCEEBL(pnCEEId, DBConexion);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return respuesta;
        }

        [HttpGet]
        public DataTable LIST_ResumenAsignacionCEE(int pnProId, string pcOpcion,string DBConexion) {
            DataTable dt = new DataTable();
            try
            {
                dt = new BLCEEActividad().fReporteResumenAreaAsignadoBL(pnProId, pcOpcion, DBConexion);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return dt;
        }

        [HttpGet]
        public List<BECEEActividad> LIST_ResumenConsolidado(int pnProId, int pnCEEId,string DBConexion) {
            List<BECEEActividad> objLista = new List<BECEEActividad>();
            BECEEActividad objBE = new BECEEActividad();
            try
            {
                objBE.pnProId = pnProId;
                objBE.pnCEEId = pnCEEId;
                objBE.DBConexion = DBConexion;               
                objLista = new BLCEEActividad().fListaResumenConsolidadoBL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return objLista;
        }

        [HttpGet]
        public DataTable OBT_ReporteCOMCEE(int pnProId, int pnCEEId,string DBConexion) {
            DataTable dt;
            try
            {
                BECEEActividad objBE = new BECEEActividad();
                objBE.pnProId = pnProId;
                objBE.pnCEEId = pnCEEId;
                objBE.DBConexion = DBConexion;
                 dt = new BLCEEActividad().fReporteCOMCEEDL(objBE);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return dt;
        }

        [HttpGet]
        public string FNC_Exportar(int pnProId, int pnCEEId, string pcOpcion,string DBConexion) {
            string respuesta = "";
            DataTable dt = new DataTable();
            try
            {
                BECEEActividad objBECA = new BECEEActividad();
                objBECA.pnProId = pnProId;
                objBECA.pnCEEId = pnCEEId;
                objBECA.DBConexion = DBConexion;
                if (pcOpcion == "01") dt = new BLCEEActividad().fReporteCEEBL(objBECA);                
                else if (pcOpcion == "02") dt = new BLCEEActividad().fReporteCOMCEEDL(objBECA);                
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return respuesta;
        }      
    }
}
