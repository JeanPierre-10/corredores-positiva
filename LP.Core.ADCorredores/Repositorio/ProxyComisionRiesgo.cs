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
    public class ProxyComisionRiesgo
    {
        #region Singleton
        private static readonly ProxyComisionRiesgo _instance = new ProxyComisionRiesgo();
        public static ProxyComisionRiesgo Instance
        {
            get { return ProxyComisionRiesgo._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, int CodigoCompania, String CodigoRamo, int CodigoBroker, String BusquedaIni, String FiltroBusqueda, String FiltroPeriodo)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_POTENCIAL.CORREDORES_GET_PRD_RIESGO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Int32).Value = CodigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = (CodigoRamo.Equals("0") ? "T" : CodigoRamo.ToString());
            cmd.Parameters.Add("PBROKER", OracleDbType.Int32).Value = CodigoBroker;
            cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = BusquedaIni;
            cmd.Parameters.Add("FILTROS_BUSQUEDA", OracleDbType.Varchar2).Value = FiltroBusqueda;
            cmd.Parameters.Add("FILTROS_PERIODO", OracleDbType.Varchar2).Value = FiltroPeriodo;
            cmd.Parameters.Add("FECHA_ACTUALIZACION", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            return cmd;
        }
        #endregion
        #region Construccion objetos
        private List<ComisionPotencialR> formatearListaComisionRiesgo(OracleDataReader dr)
        {
            var listaComisionRiesgo = new List<ComisionPotencialR>();
            while (dr.Read())
                listaComisionRiesgo.Add(new ComisionPotencialR()
                {
                    periodo = dr["PERIODO_BUSQUEDA"].ToString(),
                    primaSOL = String.Empty.Equals(dr["PRIMA_EMITIDA"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA"].ToString(),
                    primaUSD = String.Empty.Equals(dr["PRIMA_EMITIDA_USD"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA_USD"].ToString(),
                    comisionSOL = String.Empty.Equals(dr["COMISION_POTENCIAL"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL"].ToString(),
                    comisionUSD = String.Empty.Equals(dr["COMISION_POTENCIAL_USD"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL_USD"].ToString()
                });
            return listaComisionRiesgo;
        }
        #endregion
        #region Logica BD
        public BEComisionRG obtenerComisionPotencialRiesgo(int CodigoCompania, String CodigoRamo, int CodigoBroker, String BusquedaIni, String FiltroBusqueda, String FiltroPeriodo)
        {
            BEComisionRG comisionRiesgo = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn, CodigoCompania, CodigoRamo, CodigoBroker, BusquedaIni, FiltroBusqueda, FiltroPeriodo))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            String fechaActualizacion = cmd.Parameters["FECHA_ACTUALIZACION"].Value.ToString();
                            List<ComisionPotencialR> listaComisionRiesgo = formatearListaComisionRiesgo(dr);
                            comisionRiesgo = new BEComisionRG()
                            {
                                fechaActualizacion = fechaActualizacion,
                                comisionRiesgo = listaComisionRiesgo
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return comisionRiesgo;
        }
        #endregion
    }
}
