using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BELinea
    {
        //Declaramos variables

        public string DBConexion { get; set; }
        private String cLinFecRegistro;
        private String cLinFecVigFin;
        private String cLinFecVigIni;
        private Int32 nLinActivo;
        private Int32 nLinId;
        private Int32 nMatId;
        private String cDesActivo;

        //Generamos getters y setters
        [BEColumn("cLinFecRegistro")]
        public String pcLinFecRegistro
        {
            get { return cLinFecRegistro; }
            set { cLinFecRegistro = value; }
        }

        [BEColumn("cLinFecVigFin")]
        public String pcLinFecVigFin
        {
            get { return cLinFecVigFin; }
            set { cLinFecVigFin = value; }
        }

        [BEColumn("cLinFecVigIni")]
        public String pcLinFecVigIni
        {
            get { return cLinFecVigIni; }
            set { cLinFecVigIni = value; }
        }

        [BEColumn("nLinActivo")]
        public Int32 pnLinActivo
        {
            get { return nLinActivo; }
            set { nLinActivo = value; }
        }

        [BEColumn("cDesActivo")]
        public String pcDesActivo
        {
            get { return cDesActivo; }
            set { cDesActivo = value; }
        }

        [BEColumn("nLinId")]
        public Int32 pnLinId
        {
            get { return nLinId; }
            set { nLinId = value; }
        }

        [BEColumn("nMatId")]
        public Int32 pnMatId
        {
            get { return nMatId; }
            set { nMatId = value; }
        }



        public Int32 PageNumber { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 TotalPages { get; set; }
        public String pcOpcion { get; set; }

        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }
    }
}
