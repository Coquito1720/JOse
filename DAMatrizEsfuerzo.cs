/* ------------------------------------------------------------------------*/
/* SISTEMA : SAR SBP
/* SUBSISTEMA : ModuloVCC
/* NOMBRE : DAMatrizEsfuerzo.cs
/* DESCRIPCIÓN : 
/*       fnListaMatrizEsfuerzoDA
/*       fnInsertarMatrizEsfuerzoDA
/*       fnActualizarMatrizEsfuerzoDA
/*       fnEliminarMatrizEsfuerzoDA   
/* AUTOR : MCV
/* FECHA CREACIÓN : 14/03/2019
/* ------------------------------------------------------------------------*/
/* FECHA MODIFICACIÓN  EMPLEADO    
/* ------------------------------------------------------------------------*/
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
    public class DAMatrizEsfuerzo : DADBOBase
    {
        SqlCommand cmdSQL = new SqlCommand();
        String strCon = ConfigurationManager.AppSettings["SARPeruBDConexion"].ToString();

        public List<BEMatrizEsfuerzo> fnListaMatrizEsfuerzoDA(BEMatrizEsfuerzo objMat)
        {
            List<BEMatrizEsfuerzo> objListaAct = new List<BEMatrizEsfuerzo>();

            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objMat.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = "USP_SAR_SEL_ListaMatrizEsfuerzo_Filter";

                pAddParameter(cmdSQL, "@nSisId", objMat.pnSisId == 0 ? 0 : objMat.pnSisId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLenId", objMat.pnLenId == 0 ? 0 : objMat.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@nMatId", objMat.pnMatId == 0 ? 0 : objMat.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nWorId", objMat.pnWorId == 0 ? 0 : objMat.pnWorId, DbType.Int32);
                pAddParameter(cmdSQL, "@cMatNombre", objMat.pcMatNombre == "" ? "" : objMat.pcMatNombre, DbType.String);
                pAddParameter(cmdSQL, "@cMatEliminado", objMat.pcMatEliminado == "" ? "" : objMat.pcMatEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cOpcion", objMat.pcOpcion == "" ? "00" : objMat.pcOpcion, DbType.String);

                SqlDataReader drSQL = fLeer(cmdSQL);

                objListaAct = (List<BEMatrizEsfuerzo>)ConvertirDataReaderALista<BEMatrizEsfuerzo>(drSQL);

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



        public int fnInsertarMatrizEsfuerzoDA(BEMatrizEsfuerzo objMat)
        {
            //string sRes = "";
            int valor = 0;
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objMat.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_MNT_MatrizEsfuerzo";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nMatId", objMat.pnMatId == 0 ? 0 : objMat.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLenId", objMat.pnLenId == 0 ? 0 : objMat.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@nWorId", objMat.pnWorId == 0 ? 0 : objMat.pnWorId, DbType.Int32);
                pAddParameter(cmdSQL, "@cMatNombre", objMat.pcMatNombre == "" ? "" : objMat.pcMatNombre, DbType.String);
                pAddParameter(cmdSQL, "@cMatEliminado", objMat.pcMatEliminado == "" ? "" : objMat.pcMatEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cOpcion","01", DbType.String);
                pAddParameter(cmdSQL, "@nSisId", 3, DbType.Int32);
                SqlDataReader dr = fLeer(cmdSQL);
                while (dr.Read()) {

                    valor = dr["nMatId"].Equals(System.DBNull.Value) ? 0 : Convert.ToInt32(dr["nMatId"]);
                }
                //sRes = fEjecutar(cmdSQL);

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
            return valor;
        }

        public string fnActualizarMatrizEsfuerzoDA(BEMatrizEsfuerzo objMat)
        {
            String sRes = "";
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objMat.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_MNT_MatrizEsfuerzo";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nMatId", objMat.pnMatId == 0 ? 0 : objMat.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@nLenId", objMat.pnLenId == 0 ? 0 : objMat.pnLenId, DbType.Int32);
                pAddParameter(cmdSQL, "@nWorId", objMat.pnWorId == 0 ? 0 : objMat.pnWorId, DbType.Int32);
                pAddParameter(cmdSQL, "@cMatNombre", objMat.pcMatNombre == "" ? "" : objMat.pcMatNombre, DbType.String);
                pAddParameter(cmdSQL, "@cMatEliminado", objMat.pcMatEliminado == "" ? "" : objMat.pcMatEliminado, DbType.String);
                pAddParameter(cmdSQL, "@cOpcion", "02", DbType.String);
                pAddParameter(cmdSQL, "@nSisId", 3, DbType.Int32);

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

        public string fnEliminarMatrizEsfuerzoDA(BEMatrizEsfuerzo objMat)
        {
            String sRes = "";
            try
            {
                cmdSQL.Connection = NewConnection(ConfigurationManager.AppSettings[objMat.DBConexion]);
                //cmdSQL.Connection = NewConnection(strCon);
                cmdSQL.CommandText = "USP_SAR_MNT_MatrizEsfuerzo";
                cmdSQL.CommandType = CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                pAddParameter(cmdSQL, "@nMatId", objMat.pnMatId == 0 ? 0 : objMat.pnMatId, DbType.Int32);
                pAddParameter(cmdSQL, "@cOpcion", "03", DbType.String);

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
