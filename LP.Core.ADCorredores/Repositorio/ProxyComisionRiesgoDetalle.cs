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
    public class ProxyComisionRiesgoDetalle
    {
        #region Singleton
        private static readonly ProxyComisionRiesgoDetalle _instance = new ProxyComisionRiesgoDetalle();
        public static ProxyComisionRiesgoDetalle Instance
        {
            get { return ProxyComisionRiesgoDetalle._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, int CodigoCompania, String CodigoRamo, int CodigoBroker, String BusquedaIni, String FiltroBusqueda, String FiltroPeriodo)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_POTENCIAL.CORREDORES_GET_PRD_RIESGO_DET";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Int32).Value = CodigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = (CodigoRamo.Equals("0") ? "T" : CodigoRamo.ToString());
            cmd.Parameters.Add("PBROKER", OracleDbType.Int32).Value = CodigoBroker;
            cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = BusquedaIni;
            cmd.Parameters.Add("FILTROS_BUSQUEDA", OracleDbType.Varchar2).Value = FiltroBusqueda;
            cmd.Parameters.Add("FILTROS_PERIODO", OracleDbType.Varchar2).Value = FiltroPeriodo;

            cmd.Parameters.Add("FECHA_ACTUALIZACION", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("NOM_BROKER", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("NOM_COMPANIA", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("NOM_RAMO_COMERCIAL", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("PRAMOCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("PCOMPANIACURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("PBROKERCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            return cmd;
        }
        #endregion
        #region Construccion objetos
        private List<ComisionRiesgo> formatearListaComision(OracleDataReader dr)
        {
            var listaComisionRiesgo = new List<ComisionRiesgo>();
            while (dr.Read())
                listaComisionRiesgo.Add(new ComisionRiesgo()
                {
                    periodo = dr["PERIODO_BUSQUEDA"].ToString(),
                    primaPEN = String.Empty.Equals(dr["PRIMA_EMITIDA"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA"].ToString(),
                    comisionPEN = String.Empty.Equals(dr["COMISION_POTENCIAL"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL"].ToString(),
                    primaUSD = String.Empty.Equals(dr["PRIMA_EMITIDA_USD"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA_USD"].ToString(),
                    comisionUSD = String.Empty.Equals(dr["COMISION_POTENCIAL_USD"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL_USD"].ToString(),
                    nombre = dr["DESCRIPCION"].ToString()
                });
            return listaComisionRiesgo;
        }
        #endregion
        #region Logica BD
        public BEComisionPotencialDetalle obtenerComisionPotencialRiesgo(int CodigoCompania, String CodigoRamo, int CodigoBroker, String BusquedaIni, String FiltroBusqueda, String FiltroPeriodo)
        {
            BEComisionPotencialDetalle comisionRiesgoDetalle = null;
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
                            List<ComisionRiesgo> listaComisionRiesgo = formatearListaComision(dr);
             
                            dr.NextResult();
                            List<ComisionRiesgo> listaComisionRiesgoRamo = formatearListaComision(dr);
			    
			                dr.NextResult();
                            List<ComisionRiesgo> listaComisionRiesgoCompania = formatearListaComision(dr);

			                dr.NextResult();	
			                List<ComisionRiesgo> listaComisionRiesgoBroker = formatearListaComision(dr);
  
                            comisionRiesgoDetalle = new BEComisionPotencialDetalle()
                            {
                                fechaActualizacion = cmd.Parameters["FECHA_ACTUALIZACION"].Value.ToString(),
                                broker = cmd.Parameters["NOM_BROKER"].Value.ToString(),
                                compania = cmd.Parameters["NOM_COMPANIA"].Value.ToString(),
                                ramo = cmd.Parameters["NOM_RAMO_COMERCIAL"].Value.ToString(),
                                comisionRiesgo = listaComisionRiesgo,
                                comisionRiesgoBroker = listaComisionRiesgoBroker,
                                comisionRiesgoCompania = listaComisionRiesgoCompania,
                                comisionRiesgoRamo = listaComisionRiesgoRamo
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return comisionRiesgoDetalle;
        }
        #endregion
    }
}
