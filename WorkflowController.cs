//--------------------------------------------------------------
//Sistema		: SAR SBP
//Módulo		: API
//Nombre		: Workflow.cs 			
//Autor			: Kevin Ochoa Andrade
//Fecha de creación	: 21/06/2018
//Descripción		: Api Controller de Workflow
//--------------------------------------------------------------
//Fecha de modificación	            Descripción
//-------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SES.Entity.BusinessEntity.Workflow;
using SES.Entity.BusinessLogic.Workflow;
using System.Configuration;
using System.Diagnostics;

namespace SES.WebAPI.Controllers
{
    public class WorkflowController : ApiController
    {
        [HttpGet]
        public IEnumerable<BEWorkflow> LIST_Workflow(int pnSisId,string DBConexion) {
            BEWorkflow objBEWKF = new BEWorkflow();
            BLWorkflow objBLWorkflow = new BLWorkflow();
           

            objBEWKF.pnSisId = pnSisId;
            objBEWKF.DBConexion = DBConexion;
            objBEWKF.pcWorEliminado = "0";
            objBEWKF.pcOpcion = "01";
            objBEWKF.pnWorValidado = 1;

            List<BEWorkflow> lstWorkflow = new List<BEWorkflow>();
            try
            {
                lstWorkflow = objBLWorkflow.fListaWorkflowBL(objBEWKF);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = frame.GetFileLineNumber();
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message + "; line:" + frame.ToString()));
            }
            return lstWorkflow;
        }
    }
}
