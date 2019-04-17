/* ------------------------------------------------------------------------*/
/* SISTEMA : SAR SBP
/* SUBSISTEMA : ModuloVCC
/* NOMBRE : BEMatrizEsfuerzo.cs
/* DESCRIPCIÓN :
/*        Entidades de la Matriz de Esfuerzo     
/* AUTOR : MCV
/* FECHA CREACIÓN : 14/03/2019
/* ------------------------------------------------------------------------*/
/* FECHA MODIFICACIÓN  EMPLEADO    
/* ------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BEMatrizEsfuerzo
    {
        //Declaramos variables

        public string DBConexion { get; set; }
        private String cMatEliminado, cDesEliminado;
        private String cMatNombre;
        private String cLenNombre;
        private String cWorNombre;
        private String cLinFecVigFin;
        private String cLinFecVigIni;
        private Int32 nLenId;
        private Int32 nMatId;
        private Int32 nWorId;
        private Int32 nSisId;
        private Int32 nLinCantidad;
        private Int32 nDetalleMatriz;
        private Int32 nLinActiva;
        private String cWorAbreviatura;
        private String cLenAbreviatura;

        //Generamos getters y setters

        [BEColumn("cMatEliminado")]
        public String pcMatEliminado
        {
            get { return cMatEliminado; }
            set { cMatEliminado = value; }
        }
        [BEColumn("cDesEliminado")]
        public String pcDesEliminado
        {
            get { return cDesEliminado; }
            set { cDesEliminado = value; }
        }

        [BEColumn("nSisId")]
        public Int32 pnSisId
        {
            get { return nSisId; }
            set { nSisId = value; }
        }

        [BEColumn("nMatId")]
        public Int32 pnMatId
        {
            get { return nMatId; }
            set { nMatId = value; }
        }



        [BEColumn("cMatNombre")]
        public String pcMatNombre
        {
            get { return cMatNombre; }
            set { cMatNombre = value; }
        }

        [BEColumn("nLenId")]
        public Int32 pnLenId
        {
            get { return nLenId; }
            set { nLenId = value; }
        }



        [BEColumn("cLenNombre")]
        public String pcLenNombre
        {
            get { return cLenNombre; }
            set { cLenNombre = value; }
        }

        [BEColumn("cLenAbreviatura")]
        public String pcLenAbreviatura
        {
            get { return cLenAbreviatura; }
            set { cLenAbreviatura = value; }
        }

        [BEColumn("nWorId")]
        public Int32 pnWorId
        {
            get { return nWorId; }
            set { nWorId = value; }
        }

        [BEColumn("cWorNombre")]
        public String pcWorNombre
        {
            get { return cWorNombre; }
            set { cWorNombre = value; }
        }

        [BEColumn("cWorAbreviatura")]
        public String pcWorAbreviatura
        {
            get { return cWorAbreviatura; }
            set { cWorAbreviatura = value; }
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

       

        [BEColumn("nLinCantidad")]
        public Int32 pnLinCantidad
        {
            get { return nLinCantidad; }
            set { nLinCantidad = value; }
        }



        [BEColumn("nDetalleMatriz")]
        public Int32 pnDetalleMatriz
        {
            get { return nDetalleMatriz; }
            set { nDetalleMatriz = value; }
        }

        [BEColumn("nLinActiva")]
        public Int32 pnLinActiva
        {
            get { return nLinActiva; }
            set { nLinActiva = value; }
        }




        public Int32 PageNumber { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 TotalPages { get; set; }
        public String pcOpcion { get; set; }
        public string strOpcion { get; set; }

        [BEColumn("nTotalRows")]
        public Int32 pnTotalRows { get; set; }

    }
}
