using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
    public class BLLinea
    {
        DALinea objDA = new DALinea();

        public List<BELinea> fnListaLineaBL(BELinea Request)
        {
            return objDA.fnListaLineaDA(Request);
        }
        public string fnINSMantenimientoLineaBL(BELinea Request)
        {
            return objDA.fnMantenimientoLineaDA(Request);
        }
    }
}
