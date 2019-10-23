using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.ADCorredores.Entity;
using LP.Core.ADCorredores.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Repositorio
{
    public class ProxyBells
    {
        #region Singleton
        private static readonly ProxyBells _instance = new ProxyBells();
        public static ProxyBells Instance
        {
            get { return ProxyBells._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_GET_BELLS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }

        #endregion Construccion Parametros
        #region Construccion objetos
        private List<BEBells> formatearObjectoSalida(OracleDataReader dr)
        {
            var listBells = new List<BEBells>();
            while (dr.Read())
            {
                var bellsObject = new BEBells() { Code = Int32.Parse(dr[0].ToString()), Name = dr[1].ToString() };
                listBells.Add(bellsObject);
            }
            return listBells;
        }
        #endregion Construccion objetos
        #region Logica BD
        public List<BEBells> ObtenerCampanias()
        {
            List<BEBells> listCompanias = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            listCompanias = formatearObjectoSalida(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listCompanias;
        }
        #endregion Logica BD
    }
}
