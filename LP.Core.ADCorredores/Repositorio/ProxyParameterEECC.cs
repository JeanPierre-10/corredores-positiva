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
    public class ProxyParameterEECC
    {
        #region Singleton
        private static readonly ProxyParameterEECC _instance = new ProxyParameterEECC();
        public static ProxyParameterEECC Instance
        {
            get { return ProxyParameterEECC._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, String parametroBusqueda, String parametroEstado)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_GET_EECC";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PBUSQUEDA", OracleDbType.Varchar2).Value = parametroBusqueda;
            cmd.Parameters.Add("PBUSQUEDAESTADO", OracleDbType.Varchar2).Value = parametroEstado;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }
        #endregion
        #region Construccion objetos
        private List<BEDataEECC> formatearListaComisionRiesgo(OracleDataReader dr)
        {
            var listEECC = new List<BEDataEECC>();
            while (dr.Read())
            {
                var objectEECC = new BEDataEECC()
                {
                    Codigo = dr["CODIGO"].ToString(),
                    Descripcion = dr["DESCRIPCION"].ToString(),
                    TipoParametro = dr["TIPO_PARAMETRO"].ToString(),
                    Estado = dr["ESTADO"].ToString(),
                    ValorCadena = dr["VALOR_CADENA"].ToString(),
                    ValorNumerico = dr["VALOR_NUMERICO"].ToString()
                };
                listEECC.Add(objectEECC);
            }
            return listEECC;
        }
        #endregion
        #region Logica BD
        public List<BEDataEECC> ObtenerParametrosEECC(String parameterSearch, String parameterState)
        {
            List<BEDataEECC> listEECC = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn, parameterSearch, parameterState))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            listEECC = formatearListaComisionRiesgo(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEECC;
        }
        #endregion
    }
}
