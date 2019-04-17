using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
    public class BLComplejidad
    {
        DAComplejidad objDA = new DAComplejidad();

        public List<BEComplejidad> fListaComplejidadBL(BEComplejidad objBE) {

            return objDA.fListaComplejidadDL(objBE);
        }
    }
}
