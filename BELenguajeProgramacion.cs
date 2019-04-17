using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BELenguajeProgramacion
    {
        public string DBConexion { get; set; }
        private String cLenAbreviatura;
        private String cLenNombre;
        private Int32 nLenId;
        private String cLenEliminado, cDesEliminado;
        private String cLinFecRegistro;
        private String cLinFecVigFin;

        //UsuarioPerfilTecLenProg
        private Int32 nUsuId;
        private Int32 nPerId;
        private String cUsuNombreCompleto;
        private String cPerAbreviatura;
        private DateTime dtFechaInicio;

        public DateTime pdtFechaInicio
        {
            get { return dtFechaInicio; }
            set { dtFechaInicio = value; }
        }

        [BEColumn("cLenAbreviatura")]
        public String pcLenAbreviatura
        {
            get { return cLenAbreviatura; }
            set { cLenAbreviatura = value; }
        }

        [BEColumn("cLenNombre")]
        public String pcLenNombre
        {
            get { return cLenNombre; }
            set { cLenNombre = value; }
        }

        [BEColumn("nLenId")]
        public Int32 pnLenId
        {
            get { return nLenId; }
            set { nLenId = value; }
        }

        [BEColumn("cLenEliminado")]
        public String pcLenEliminado
        {
            get { return cLenEliminado; }
            set { cLenEliminado = value; }
        }


        [BEColumn("cDesEliminado")]
        public String pcDesEliminado
        {
            get { return cDesEliminado; }
            set { cDesEliminado = value; }
        }

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

        // UsuarioPerfilTecLenProg

        [BEColumn("nRow")]
        public Int64 pnRow { get; set; }

        [BEColumn("nUsuId")]
        public Int32 pnUsuId
        {
            get { return nUsuId; }
            set { nUsuId = value; }
        }

        [BEColumn("nPerId")]
        public Int32 pnPerId
        {
            get { return nPerId; }
            set { nPerId = value; }
        }

        [BEColumn("cUsuNombreCompleto")]
        public String pcUsuNombreCompleto
        {
            get { return cUsuNombreCompleto; }
            set { cUsuNombreCompleto = value; }
        }

        [BEColumn("cPerAbreviatura")]
        public String pcPerAbreviatura
        {
            get { return cPerAbreviatura; }
            set { cPerAbreviatura = value; }
        }

        //parámetros para el mantenimiento
        public String pcUsuarios { get; set; }
        public String pcPerfiles { get; set; }

        public String strOpcion { get; set; }

        public Int32 PageNumber { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 TotalPages { get; set; }
        public String pcOpcion { get; set; }

        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }
    }
}
