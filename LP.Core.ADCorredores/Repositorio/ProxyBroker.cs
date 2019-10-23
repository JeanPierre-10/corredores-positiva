using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.ADCorredores.Entity;
using LP.Core.ADCorredores.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Repositorio
{
    public class ProxyBroker
    {
        #region Singleton
        private static readonly ProxyBroker _instance = new ProxyBroker();
        public static ProxyBroker Instance
        {
            get { return ProxyBroker._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_GET_BROKER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }

        #endregion Construccion Parametros
        #region Construccion objetos
        private List<BEBroker> formatearObjectoSalida(OracleDataReader dr)
        {
            var listBroker = new List<BEBroker>();
            while (dr.Read())
            {
                var bouquetObject = new BEBroker()
                {
                    Code = Int32.Parse(dr[0].ToString()),
                    CodigoVTime = dr[1].ToString(),
                    CodigoInsunix = dr[2].ToString(),
                    Name = dr[3].ToString(),
                };
                listBroker.Add(bouquetObject);
            }
            return listBroker;
        }
        #endregion Construccion objetos
        #region Logica BD
        public List<BEBroker> GetBroker()
        {
            List<BEBroker> listBroker = null;
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
                            listBroker = formatearObjectoSalida(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBroker;
        }
        #endregion Logica BD

    }
}
