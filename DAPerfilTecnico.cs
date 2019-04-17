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
    public class DAPerfilTecnico : DADBOBase
    {
        String strCon = ConfigurationManager.AppSettings["SARPeruBDConexion"].ToString();
        SqlCommand cmdSQL = new SqlCommand();

        public List<BEPerfilTecnico> fListaPerfilTecnicoDA(BEPerfilTecnico oBEPerfilTecnico)
        {
            List<BEPerfilTecnico> objLista = new List<BEPerfilTecnico>();
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[oBEPerfilTecnico.DBConexion]);
                cmdSQL.CommandText = "USP_SAR_MNT_PerfilTecnico";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nPerId", oBEPerfilTecnico.pnPerId == 0 ? 0 : oBEPerfilTecnico.pnPerId, DbType.Int32);
                pAddParameter(cmdSQL, "@cPerNombre", oBEPerfilTecnico.pcPerNombre == "" ? "" : oBEPerfilTecnico.pcPerNombre, DbType.String);
                pAddParameter(cmdSQL, "@cPerAbreviatura", oBEPerfilTecnico.pcPerAbreviatura == "" ? "" : oBEPerfilTecnico.pcPerAbreviatura, DbType.String);
                pAddParameter(cmdSQL, "@nPageNumber", oBEPerfilTecnico.PageNumber == 0 ? 0 : oBEPerfilTecnico.PageNumber, DbType.Int32);
                pAddParameter(cmdSQL, "@nPageZize", oBEPerfilTecnico.PageSize == 0 ? 0 : oBEPerfilTecnico.PageSize, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", oBEPerfilTecnico.strOpcion == "" ? "0" : oBEPerfilTecnico.strOpcion, DbType.String);
                SqlDataReader drSQL = fLeer(cmdSQL);
                if (drSQL.HasRows)
                {
                    objLista = (List<BEPerfilTecnico>)ConvertirDataReaderALista<BEPerfilTecnico>(drSQL);
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
        
        public List<BEPerfilTecnico> ListaPerfilComercialDA()
        {
            List<BEPerfilTecnico> objLista = new List<BEPerfilTecnico>();
            try
            {
                cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_SEL_PerfilComercial";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();

                SqlDataReader drSQL = fLeer(cmdSQL);
                if (drSQL.HasRows)
                {
                    objLista = (List<BEPerfilTecnico>)ConvertirDataReaderALista<BEPerfilTecnico>(drSQL);
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
