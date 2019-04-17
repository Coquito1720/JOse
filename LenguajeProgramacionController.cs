using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SES.Entity.BusinessLogic.SAR;
using SES.Entity.BusinessEntity.SAR;
using System.Diagnostics;

namespace SES.WebAPI.Controllers
{
    public class LenguajeProgramacionController : ApiController
    {
        [HttpGet]
        public IEnumerable<BELenguajeProgramacion> LIST_LenguajesProgramacion(string DBConexion) {

            List<BELenguajeProgramacion> lstLengPro = new List<BELenguajeProgramacion>();

            try {
                BELenguajeProgramacion objLengPro = new BELenguajeProgramacion();
                BLLenguajeProgramacion objBLLengPro = new BLLenguajeProgramacion();
                objLengPro.DBConexion = DBConexion;
                objLengPro.pcOpcion = "01";
                objLengPro.pcLenEliminado = "0";
                lstLengPro = objBLLengPro.fListaLenguajeProgramacionBL(objLengPro);

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }

            return lstLengPro;
        }

    }
}
