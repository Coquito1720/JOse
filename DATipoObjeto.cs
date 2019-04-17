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
    public class DATipoObjeto : DADBOBase
    {
        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["SARPeruBDConexion"].ToString();

        public List<BETipoObjeto> fListaTipoObjetoDL(BETipoObjeto objTipoObjeto)
        {
            List<BETipoObjeto> objListaAct = new List<BETipoObjeto>();
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objTipoObjeto.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaTipoObjeto_Filter";
                pAddParameter(cmdSQL, "@cTipEliminado", objTipoObjeto.pcTipEliminado == "" ? "" : objTipoObjeto.pcTipEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cTipNombre", objTipoObjeto.pcTipNombre == "" ? "" : objTipoObjeto.pcTipNombre, DbType.String);
                pAddParameter(cmdSQL, "@cTipAbreviatura", objTipoObjeto.pcTipAbreviatura == "" ? "" : objTipoObjeto.pcTipAbreviatura, DbType.String);
                pAddParameter(cmdSQL, "@nLenId", objTipoObjeto.pnLenId == 0 ? 0 : objTipoObjeto.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@nTipId", objTipoObjeto.pnTipId == 0 ? 0 : objTipoObjeto.pnTipId, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", objTipoObjeto.pcOpcion == "" ? "00" : objTipoObjeto.pcOpcion, DbType.String);
                pAddParameter(cmdSQL, "@nPageNumber", objTipoObjeto.PageNumber == 0 ? 1 : objTipoObjeto.PageNumber, DbType.Int32);
                pAddParameter(cmdSQL, "@nPageZize", objTipoObjeto.PageSize == 0 ? 99999 : objTipoObjeto.PageSize, DbType.Int32);
                SqlDataReader drSQL = fLeer(cmdSQL);

                objListaAct = (List<BETipoObjeto>)ConvertirDataReaderALista<BETipoObjeto>(drSQL);

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
    }
}
