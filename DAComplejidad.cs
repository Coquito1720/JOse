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
    public class DAComplejidad : DADBOBase
    {

        SqlCommand cmdSQL = new SqlCommand();
   

        public List<BEComplejidad> fListaComplejidadDL(BEComplejidad objELComp)
        {
            List<BEComplejidad> objListaAct = new List<BEComplejidad>();

            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objELComp.DBConexion].ToString());
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaComplejidadFilter";
                pAddParameter(cmdSQL, "@cComEliminado", objELComp.pcComEliminado == "" ? "" : objELComp.pcComEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cComAbreviatura", objELComp.pcComAbreviatura == "" ? "" : objELComp.pcComAbreviatura, DbType.String);
                pAddParameter(cmdSQL, "@cComNombre", objELComp.pcComNombre == "" ? "" : objELComp.pcComNombre, DbType.String);
                pAddParameter(cmdSQL, "@nComId", objELComp.pnComId == 0 ? 0 : objELComp.pnComId, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", objELComp.pcOpcion == "" ? "00" : objELComp.pcOpcion, DbType.String);
                pAddParameter(cmdSQL, "@nPageNumber", objELComp.PageNumber == 0 ? 1 : objELComp.PageNumber, DbType.Int32);
                pAddParameter(cmdSQL, "@nPageZize", objELComp.PageSize == 0 ? 99999 : objELComp.PageSize, DbType.Int32);
                SqlDataReader drSQL = fLeer(cmdSQL);

                objListaAct = (List<BEComplejidad>)ConvertirDataReaderALista<BEComplejidad>(drSQL);

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
