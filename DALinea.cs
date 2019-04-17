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
    public class DALinea : DADBOBase
    {
        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["SARPeruBDConexion"].ToString();

        public List<BELinea> fnListaLineaDA(BELinea objELLinea)
        {
            List<BELinea> objListaAct = new List<BELinea>();

            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objELLinea.DBConexion]);
                cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaLinea_Filter";
                pAddParameter(cmdSQL, "@nLinId", objELLinea.pnLinId == 0 ? 0 : objELLinea.pnLinId, DbType.Int32);
                pAddParameter(cmdSQL, "@nMatId", objELLinea.pnMatId == 0 ? 0 : objELLinea.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLinActivo", objELLinea.pnLinActivo == 0 ? 0 : objELLinea.pnLinActivo, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", objELLinea.pcOpcion == "" ? "00" : objELLinea.pcOpcion, DbType.String);
                pAddParameter(cmdSQL, "@nPageNumber", objELLinea.PageNumber == 0 ? 0 : objELLinea.PageNumber, DbType.Int32);
                pAddParameter(cmdSQL, "@nPageZize", objELLinea.PageSize == 0 ? 0 : objELLinea.PageSize, DbType.Int32);
                SqlDataReader drSQL = fLeer(cmdSQL);

                objListaAct = (List<BELinea>)ConvertirDataReaderALista<BELinea>(drSQL);

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

            return objListaAct;
        }


        public string fnMantenimientoLineaDA(BELinea objELLinea)
        {
            String sRes = "";
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objELLinea.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_MNT_Linea";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nLinId", objELLinea.pnLinId == 0 ? 0 : objELLinea.pnLinId, DbType.Int32);
                pAddParameter(cmdSQL, "@nMatId", objELLinea.pnMatId == 0 ? 0 : objELLinea.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@cLinFecVigIni", objELLinea.pcLinFecVigIni == "" ? "" : objELLinea.pcLinFecVigIni, DbType.String);
                pAddParameter(cmdSQL, "@cLinFecVigFin", objELLinea.pcLinFecVigFin == "" ? "" : objELLinea.pcLinFecVigFin, DbType.String);
                pAddParameter(cmdSQL, "@nLinActivo", objELLinea.pnLinActivo == 0 ? 0 : objELLinea.pnLinActivo, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", objELLinea.pcOpcion == "" ? "00" : objELLinea.pcOpcion, DbType.String);
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
    }
}
