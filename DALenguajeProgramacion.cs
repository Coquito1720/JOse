using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SES.Entity.BusinessEntity.SAR;

namespace SES.Entity.DataAccess.SAR
{
    public class DALenguajeProgramacion : DADBOBase
    {

        SqlCommand cmdSQL = new SqlCommand();
       
        public List<BELenguajeProgramacion> fListaLenguajeProgramacionDA(BELenguajeProgramacion objELLP)
        {
            List<BELenguajeProgramacion> objListaAct = new List<BELenguajeProgramacion>();

            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objELLP.DBConexion]);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaLenguajeProgramacion_Filter";
                pAddParameter(cmdSQL, "@nLenId", objELLP.pnLenId == 0 ? 0 : objELLP.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@cLenNombre", objELLP.pcLenNombre == "" ? "" : objELLP.pcLenNombre, DbType.String);
                pAddParameter(cmdSQL, "@cLenAbreviatura", objELLP.pcLenAbreviatura == "" ? "" : objELLP.pcLenAbreviatura, DbType.String);
                pAddParameter(cmdSQL, "@cLenEliminado", objELLP.pcLenEliminado == "" ? "" : objELLP.pcLenEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cOpcion", objELLP.pcOpcion == "" ? "00" : objELLP.pcOpcion, DbType.String);
                pAddParameter(cmdSQL, "@nPageNumber", objELLP.PageNumber == 0 ? 0 : objELLP.PageNumber, DbType.Int32);
                pAddParameter(cmdSQL, "@nPageZize", objELLP.PageSize == 0 ? 0 : objELLP.PageSize, DbType.Int32);
                SqlDataReader drSQL = fLeer(cmdSQL);

                objListaAct = (List<BELenguajeProgramacion>)ConvertirDataReaderALista<BELenguajeProgramacion>(drSQL);

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

        public List<BELenguajeProgramacion> fVerificarUsuarioxLenguaje(BELenguajeProgramacion objELLP)
        {
            List<BELenguajeProgramacion> objLista = new List<BELenguajeProgramacion>();
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objELLP.DBConexion]);
                cmdSQL.CommandText = "USP_SAR_VER_UsuarioxLenguaje";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();

                pAddParameter(cmdSQL, "@nUsuId", objELLP.pnUsuId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLenId", objELLP.pnLenId, DbType.Int32);
                SqlDataReader drSQL = fLeer(cmdSQL);
                if (drSQL.HasRows)
                {
                    objLista = (List<BELenguajeProgramacion>)ConvertirDataReaderALista<BELenguajeProgramacion>(drSQL);
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
            return objLista;
        }

    }
}
