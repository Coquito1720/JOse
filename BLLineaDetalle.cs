using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
    public class BLLineaDetalle
    {
        DALineaDetalle objDA = new DALineaDetalle();

        public DataTable fListaLineaDetalleBL(BELineaDetalle objLineaDetalle)
        {
            return objDA.fListaLineaDetalleDA(objLineaDetalle);
        }

        public string fMantenimientoLineaDetalleBL(BELineaDetalle objLineaDetalle)
        {
            return objDA.fMantenimientoLineaDetalleDA(objLineaDetalle);
        }

        public BELineaDetalle fObtenerValorBL(BELineaDetalle objLineaDetalle)
        {
            return objDA.fObtenerValorDL(objLineaDetalle);
        }

    }
}
