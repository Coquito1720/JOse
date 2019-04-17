using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessLogic.SAR
{
    public class BLPerfilTecnico
    {
        DAPerfilTecnico objDA = new DAPerfilTecnico();

        public List<BEPerfilTecnico> fListaPerfilTecnicoBL(BEPerfilTecnico oELPerfilTecnico)
        {
            BEPerfilTecnico objPaginacion = new BEPerfilTecnico();
            List<BEPerfilTecnico> objLista = new List<BEPerfilTecnico>();
            objLista = new DAPerfilTecnico().fListaPerfilTecnicoDA(oELPerfilTecnico);
            if (objLista.Count > 0)
            {
                int TotalRows = objLista.First().pnTotalRows;
                objPaginacion.pnTotalRows = TotalRows;
                int Paginas = 0;
                if (oELPerfilTecnico.PageSize == 0)
                { Paginas = 1; }
                else
                {
                    Paginas = ((int)(TotalRows / oELPerfilTecnico.PageSize));
                }
                if (oELPerfilTecnico.PageSize * Paginas != TotalRows) Paginas += 1;
                objPaginacion.TotalPages = Paginas;
            }
            else
            {
                objPaginacion.pnTotalRows = 0;
                objPaginacion.TotalPages = 0;
            }
            foreach (BEPerfilTecnico item in objLista)
            {
                item.TotalPages = objPaginacion.TotalPages;
                item.pnTotalRows = objPaginacion.pnTotalRows;
            }
            return objLista;
        }
        
        public List<BEPerfilTecnico> ListaPerfilComercialBL()
        {
            return new DAPerfilTecnico().ListaPerfilComercialDA();
        }
    }
}
