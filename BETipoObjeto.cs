using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BETipoObjeto
    {

        //Declaramos variables
        public string DBConexion { get; set; }
        private String cTipAbreviatura;
        private String cTipEliminado;
        private String cTipNombre;
        private Int32 nLenId;
        private Int32 nTipId;


        //Generamos getters y setters

        [BEColumn("cTipAbreviatura")]
        public String pcTipAbreviatura
        {
            get { return cTipAbreviatura; }
            set { cTipAbreviatura = value; }
        }

        [BEColumn("cTipEliminado")]
        public String pcTipEliminado
        {
            get { return cTipEliminado; }
            set { cTipEliminado = value; }
        }

        [BEColumn("cTipNombre")]
        public String pcTipNombre
        {
            get { return cTipNombre; }
            set { cTipNombre = value; }
        }

        [BEColumn("nLenId")]
        public Int32 pnLenId
        {
            get { return nLenId; }
            set { nLenId = value; }
        }

        [BEColumn("nTipId")]
        public Int32 pnTipId
        {
            get { return nTipId; }
            set { nTipId = value; }
        }



        public String cDesEliminado;
        [BEColumn("cDesEliminado")]
        public String pcDesEliminado
        {
            get { return cDesEliminado; }
            set { cDesEliminado = value; }
        }
        public String strOpcion { get; set; }
        public Int32 PageNumber { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 TotalPages { get; set; }
        public String pcOpcion { get; set; }

        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }

    }
}
