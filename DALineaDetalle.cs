using SES.Entity.BusinessEntity.SAR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.Entity.DataAccess.SAR
{
    public class DALineaDetalle:DADBOBase
    {

        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["SARPeruBDConexion"].ToString();

        public DataTable fListaLineaDetalleDA(BELineaDetalle objDet)
        {
            DataTable dt = new DataTable();
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objDet.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaLineaDetalle_Filter";
                pAddParameter(cmdSQL, "@nLinDetId", objDet.pnLinDetId == 0 ? 0 : objDet.pnLinDetId, DbType.Int32);
                pAddParameter(cmdSQL, "@nComId", objDet.pnComId == 0 ? 0 : objDet.pnComId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLinId", objDet.pnLinId == 0 ? 0 : objDet.pnLinId, DbType.Int32);
                pAddParameter(cmdSQL, "@nMatId", objDet.pnMatId == 0 ? 0 : objDet.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nPerId", objDet.pnPerId == 0 ? 0 : objDet.pnPerId, DbType.Int32);
                pAddParameter(cmdSQL, "@nTipId", objDet.pnTipId == 0 ? 0 : objDet.pnTipId, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", objDet.pcOpcion == "" ? "00" : objDet.pcOpcion, DbType.String);
                SqlDataAdapter da = new SqlDataAdapter(cmdSQL);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return dt;
        }

        public string fMantenimientoLineaDetalleDA(BELineaDetalle Request)
        {
            String sRes = "";
            try
            {
                cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_MNT_LineaDetalle";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nLinDetId", Request.pnLinDetId == 0 ? 0 : Request.pnLinDetId, DbType.Int32);
                pAddParameter(cmdSQL, "@nComId", Request.pnComId == 0 ? 0 : Request.pnComId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLDetValor", Request.pnLDetValor == 0 ? 0 : Request.pnLDetValor, DbType.Decimal);   //Int32 JRTV
                pAddParameter(cmdSQL, "@nLinId", Request.pnLinId == 0 ? 0 : Request.pnLinId, DbType.Int32);
                pAddParameter(cmdSQL, "@nMatId", Request.pnMatId == 0 ? 0 : Request.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nPerId", Request.pnPerId == 0 ? 0 : Request.pnPerId, DbType.Int32);
                pAddParameter(cmdSQL, "@nTipId", Request.pnTipId == 0 ? 0 : Request.pnTipId, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", Request.pcOpcion == "" ? "00" : Request.pcOpcion, DbType.String);
                sRes = fEjecutar(cmdSQL);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
            finally
            {

                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();

                }
            }
            return sRes;
        }

        public BELineaDetalle fObtenerValorDL(BELineaDetalle Request)
        {
            Request.pnLinDetId = -1;//error
            try
            {
                cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ValorMEE";
                pAddParameter(cmdSQL, "@nWorId", Request.pnWorId == 0 ? 0 : Request.pnWorId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLenId", Request.pnLenId == 0 ? 0 : Request.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@nTipId", Request.pnTipId == 0 ? 0 : Request.pnTipId, DbType.Int32);
                pAddParameter(cmdSQL, "@nComId", Request.pnComId == 0 ? 0 : Request.pnComId, DbType.Int32);
                pAddParameter(cmdSQL, "@nPerId", Request.pnPerId == 0 ? 0 : Request.pnPerId, DbType.Int32);
                pAddParameter(cmdSQL, "@cMatEliminado", Request.pcMatEliminado == "" ? "" : Request.pcMatEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cOpcion", Request.pcOpcion == "" ? "" : Request.pcOpcion, DbType.String);

                SqlDataReader drSQL = fLeer(cmdSQL);
                if (drSQL.HasRows)
                {
                    List<BELineaDetalle> lista = new List<BELineaDetalle>();
                    lista = (List<BELineaDetalle>)ConvertirDataReaderALista<BELineaDetalle>(drSQL);
                    Request.pnLinDetId = lista[0].pnLinDetId;
                    Request.pnLDetValor = lista[0].pnLDetValor;
                }
                else
                {
                    Request.pnLinDetId = 0;
                    Request.pnLDetValor = 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (cmdSQL.Connection.State == ConnectionState.Open)
                {
                    cmdSQL.Connection.Close();
                }
            }
            return Request;
        }

    }
}
