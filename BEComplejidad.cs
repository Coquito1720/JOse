using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BEComplejidad
    {
        //Declaramos variables
        public string DBConexion { get; set; }
        private String cComEliminado, cDesEliminado;
        private String cComNombre;
        private Int32 nComId;
        private Int32 nComPeso;


        //Generamos getters y setters

        [BEColumn("cComEliminado")]
        public String pcComEliminado
        {
            get { return cComEliminado; }
            set { cComEliminado = value; }
        }

        [BEColumn("cDesEliminado")]
        public String pcDesEliminado
        {
            get { return cDesEliminado; }
            set { cDesEliminado = value; }
        }
        [BEColumn("cComNombre")]
        public String pcComNombre
        {
            get { return cComNombre; }
            set { cComNombre = value; }
        }

        [BEColumn("nComId")]
        public Int32 pnComId
        {
            get { return nComId; }
            set { nComId = value; }
        }

        public Int32 PageNumber { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 TotalPages { get; set; }
        public String pcOpcion { get; set; }

        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }

        private String cComAbreviatura;

        [BEColumn("cComAbreviatura")]
        public String pcComAbreviatura
        {
            get { return cComAbreviatura; }
            set { cComAbreviatura = value; }
        }

        [BEColumn("nComPeso")]
        public Int32 pnComPeso
        {
            get { return nComPeso; }
            set { nComPeso = value; }
        }


    }
}
