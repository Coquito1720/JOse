using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BEPerfilTecnico
    {
        public string DBConexion { get; set; }
        [BEColumn("nPerId")]
        public Int32 pnPerId { get; set; }
        [BEColumn("cPerNombre")]
        public string pcPerNombre { get; set; }
        [BEColumn("cValidacion")]
        public Int64 pcValidacion { get; set; }
        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }
        [BEColumn("dPerTecCosto")]
        public decimal pdPerTecCosto { get; set; }
        [BEColumn("nPerHrsxDia")]
        public Int32 pnPerHrsxDia { get; set; }
        [BEColumn("cPerAbreviatura")]
        public String pcPerAbreviatura { get; set; }
        [BEColumn("bFlagActivo")]
        public Boolean pbFlagActivo { get; set; }
        [BEColumn("nPerPeso")]
        public Int32 pnPerPeso { get; set; }

        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 TotalPages { get; set; }
        public string strOpcion { get; set; }


    }
}
