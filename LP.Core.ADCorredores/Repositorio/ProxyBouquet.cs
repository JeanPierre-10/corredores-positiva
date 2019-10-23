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
    public class ProxyBouquet
    {
        #region Singleton
        private static readonly ProxyBouquet _instance = new ProxyBouquet();
        public static ProxyBouquet Instance
        {
            get { return ProxyBouquet._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, String codeBells, String flag)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_GET_RAMO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCODIGO_COMPANIA", OracleDbType.Varchar2).Value = codeBells;
            cmd.Parameters.Add("PFLG", OracleDbType.Varchar2).Value = flag;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }

        #endregion Construccion Parametros
        #region Construccion objetos
        private List<BEBouquet> formatearObjectoSalida(OracleDataReader dr)
        {
            var listBouquet = new List<BEBouquet>();
            while (dr.Read())
            {
                var bouquetObject = new BEBouquet() { Code = dr[0].ToString(), Name = dr[1].ToString() };
                listBouquet.Add(bouquetObject);
            }
            return listBouquet;
        }
        #endregion Construccion objetos
        #region Logica BD
        /// <summary>
        /// Método que retorna una lista de Ramos
        /// </summary>
        /// <param name="codeBells"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<BEBouquet> GetBouquet(String codeBells, String flag)
        {
            List<BEBouquet> listBouquet = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn, codeBells, flag))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            listBouquet = formatearObjectoSalida(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBouquet;
        }
        #endregion Logica BD
    }
}
