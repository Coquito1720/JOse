/* ------------------------------------------------------------------------*/
/* SISTEMA : SAR SBP
/* SUBSISTEMA : ModuloVCC
/* NOMBRE : serviceMatrizEsfuerzo.js
/* DESCRIPCIÓN : 
/*       fnListaMatrizEsfuerzoBL
/*       fnListaLenguajeProgramacion
/*       fnINSMatrizEsfuerzoBL
/*       fnUPDMatrizEsfuerzoBL
/*       fnDELMatrizEsfuerzoBL    
/* AUTOR : MCV
/* FECHA CREACIÓN : 14/03/2019
/* ------------------------------------------------------------------------*/
/* FECHA MODIFICACIÓN  EMPLEADO    
/* ------------------------------------------------------------------------*/
using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
    public class BLMatrizEsfuerzo
    {
        DAMatrizEsfuerzo _DA = new DAMatrizEsfuerzo();
        DALenguajeProgramacion objDALengProg = new DALenguajeProgramacion();

        public List<BEMatrizEsfuerzo> fnListaMatrizEsfuerzoBL(BEMatrizEsfuerzo objMat)
        {
            return _DA.fnListaMatrizEsfuerzoDA(objMat);
        }

        public List<BELenguajeProgramacion> fnListaLenguajeProgramacion(BELenguajeProgramacion Request)
        {
            return objDALengProg.fListaLenguajeProgramacionDA(Request);
        }

        public int fnINSMatrizEsfuerzoBL(BEMatrizEsfuerzo objMatriz)
        {
            return _DA.fnInsertarMatrizEsfuerzoDA(objMatriz);
        }

        public string fnUPDMatrizEsfuerzoBL(BEMatrizEsfuerzo objMatriz)
        {
            return _DA.fnActualizarMatrizEsfuerzoDA(objMatriz);
        }

        public string fnDELMatrizEsfuerzoBL(BEMatrizEsfuerzo objMatriz)
        {
            return _DA.fnEliminarMatrizEsfuerzoDA(objMatriz);
        }


    }
}
