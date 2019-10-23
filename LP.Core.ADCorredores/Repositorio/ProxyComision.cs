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
    public class ProxyComision
    {
        #region Singleton
        private static readonly ProxyComision _instance = new ProxyComision();
        public static ProxyComision Instance
        {
            get { return ProxyComision._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, int CodigoCompania, String CodigoRamo, int CodigoBroker, String busquedaIni)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_POTENCIAL.CORREDORES_GET_COMISION";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Int32).Value = CodigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = (CodigoRamo.Equals("0") ? "T" : CodigoRamo.ToString());
            cmd.Parameters.Add("PBROKER", OracleDbType.Int32).Value = CodigoBroker;
            cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = busquedaIni;
            cmd.Parameters.Add("FECHA_ACTUALIZACION", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;

            cmd.Parameters.Add(new OracleParameter("POCURSORD0", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSORD3", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSORD6", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSORD9", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            return cmd;
        }
        #endregion
        #region Logica BD
        public BEComisionPG obtenerComisionPotencial(int CodigoCompania, String CodigoRamo, int CodigoBroker, String busquedaIni)
        {
            BEComisionPG comisionPotencial = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn, CodigoCompania, CodigoRamo, CodigoBroker, busquedaIni))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            var listaComision = new List<ComisionPotencial>();
                            String fechaActualizacion = cmd.Parameters["FECHA_ACTUALIZACION"].Value.ToString();
                            listaComision.Add(formatearListaCoaseguroPeriodo(dr, "0"));
                            dr.NextResult();
                            listaComision.Add(formatearListaCoaseguroPeriodo(dr, "30"));
                            dr.NextResult();
                            listaComision.Add(formatearListaCoaseguroPeriodo(dr, "60"));
                            dr.NextResult();
                            listaComision.Add(formatearListaCoaseguroPeriodo(dr, "90"));
                            comisionPotencial = new BEComisionPG()
                            {
                                fechaActualizacion = fechaActualizacion,
                                comisionPotencial = listaComision
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return comisionPotencial;
        }
        #endregion
        #region Construccion objetos
        private ComisionPotencial formatearListaCoaseguroPeriodo(OracleDataReader dr, String periodo)
        {
            if (!dr.Read())
                return new ComisionPotencial();
            return new ComisionPotencial()
            {
                fechaVencimiento = periodo,
                comisionSOL = String.Empty.Equals(dr["COMISION_POTENCIAL"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL"].ToString(),
                primaSol = String.Empty.Equals(dr["PRIMA_EMITIDA"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA"].ToString(),
                comisionUSD = String.Empty.Equals(dr["COMISION_POTENCIAL_USD"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL_USD"].ToString(),
                primaUSD = String.Empty.Equals(dr["PRIMA_EMITIDA_USD"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA_USD"].ToString()
            };
        }
        #endregion
    }
}
