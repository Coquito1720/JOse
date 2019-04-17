using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
   public class BLTipoObjeto
    {
        DATipoObjeto objDA = new DATipoObjeto();
        public List<BETipoObjeto> fListaTipoObjetoBL(BETipoObjeto objBE) {

            return objDA.fListaTipoObjetoDL(objBE);
        }
    }
}
