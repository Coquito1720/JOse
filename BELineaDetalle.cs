using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.BusinessEntity.SAR
{
    public class BELineaDetalle
    {

        public string DBConexion { get; set; }

        
        //Declaramos variables
        private Int32 nComId;
        private Decimal nLDetValor;
        private Int32 nLinId;
        private Int32 nMatId;
        private Int32 nPerId;
        private Int32 nTipId;
        private Int32 nLinDetId;
        private String cTipNombre, cComNombre;

        [BEColumn("Objeto")]
        public string pcObjeto { get; set; }

        //Generamos getters y setters
        [BEColumn("nLinDetId")]
        public Int32 pnLinDetId
        {
            get { return nLinDetId; }
            set { nLinDetId = value; }
        }
        [BEColumn("cTipNombre")]
        public string pcTipNombre
        {
            get { return cTipNombre; }
            set { cTipNombre = value; }
        }

        [BEColumn("cComNombre")]
        public string pcComNombre
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

        [BEColumn("nLDetValor")]
        public Decimal pnLDetValor
        {
            get { return nLDetValor; }
            set { nLDetValor = value; }
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

        [BEColumn("nPerId")]
        public Int32 pnPerId
        {
            get { return nPerId; }
            set { nPerId = value; }
        }

        [BEColumn("nTipId")]
        public Int32 pnTipId
        {
            get { return nTipId; }
            set { nTipId = value; }
        }

        public String pcOpcion { get; set; }


        //Para obtener valor

        private Int32 nWorId;
        private Int32 nLenId;
        private String cMatEliminado;

        [BEColumn("nWorId")]
        public Int32 pnWorId
        {
            get { return nWorId; }
            set { nWorId = value; }
        }

        [BEColumn("nLenId")]
        public Int32 pnLenId
        {
            get { return nLenId; }
            set { nLenId = value; }
        }

        [BEColumn("cMatEliminado")]
        public String pcMatEliminado
        {
            get { return cMatEliminado; }
            set { cMatEliminado = value; }
        }
    }
}
