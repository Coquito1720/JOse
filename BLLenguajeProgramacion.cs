using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SES.Entity.BusinessEntity.SAR;
using SES.Entity.DataAccess.SAR;
namespace SES.Entity.BusinessLogic.SAR
{
    public class BLLenguajeProgramacion
    {
        DALenguajeProgramacion objDALengProg = new DALenguajeProgramacion();
        public List<BELenguajeProgramacion> fListaLenguajeProgramacionBL(BELenguajeProgramacion objBeLengPro) {
            return objDALengProg.fListaLenguajeProgramacionDA(objBeLengPro);
        }
        public List<BELenguajeProgramacion> fVerificarUsuarioxLenguajeBL(BELenguajeProgramacion objBE) {

            return objDALengProg.fVerificarUsuarioxLenguaje(objBE);
        }

    }
}
