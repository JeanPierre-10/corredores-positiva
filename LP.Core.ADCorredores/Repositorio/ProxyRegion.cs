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
    public class ProxyRegion
    {
        #region Singleton
        private static readonly ProxyRegion _instance = new ProxyRegion();
        public static ProxyRegion Instance
        {
            get { return ProxyRegion._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        /// <summary>
        /// Método que conecta con BD y llama al procedure
        /// </summary>
        /// <param name="cn"></param>
        /// <returns></returns>
        private OracleCommand crearParametrosPrimaCoaseguro(OracleConnection cn)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_GET_REGION";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }
        #endregion
        #region Construccion objetos
        /// <summary>
        /// Método para formatear los objetos extraídos de BD
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private List<BERegion> formatearPrimaCoaseguro(OracleDataReader dr)
        {
            var listBouquet = new List<BERegion>();
            while (dr.Read())
            {
                var bouquetObject = new BERegion() { Code = Int32.Parse(dr[0].ToString()), Name = dr[1].ToString() };
                listBouquet.Add(bouquetObject);
            }
            return listBouquet;
        }

        #endregion
        #region Logica BD
        /// <summary>
        /// Método que retorna una lista de las regiones almacenadas en BD
        /// </summary>
        /// <returns></returns>
        public List<BERegion> GetRegion()
        {
            List<BERegion> listBouquet = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametrosPrimaCoaseguro(cn))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        { listBouquet = formatearPrimaCoaseguro(dr); }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBouquet;
        }
        #endregion
    }
}
