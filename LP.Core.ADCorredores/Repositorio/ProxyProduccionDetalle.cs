using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.ADCorredores.Entity;
using LP.Core.ADCorredores.EntityRequest;
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
    public class ProxyProduccionDetalle
    {
        #region Singleton
        private static readonly ProxyProduccionDetalle _instance = new ProxyProduccionDetalle();

        public static ProxyProduccionDetalle Instance
        {
            get { return ProxyProduccionDetalle._instance; }
        }
        #endregion Singleton
        public BEDataComparativoMensualDetalle GetProduccionDetalle(int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker, String busquedaFiltro)
        {
            BEDataComparativoMensualDetalle resultadoComparativoMensualDetalle = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = new OracleCommand { Connection = cn })
                    {
                        cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_BALANCE_PRD_DETALLE";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("PCOMPANIA", OracleDbType.Int32).Value = CodigoCompania;
                        cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = (CodigoRamo.Equals("0") ? "T" : CodigoRamo.ToString());
                        cmd.Parameters.Add("PBROKER", OracleDbType.Int32).Value = CodigoBroker;
                        cmd.Parameters.Add("PREGION", OracleDbType.Int32).Value = CodigoRegion;
                        cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = busquedaFiltro;

                        cmd.Parameters.Add(new OracleParameter("POCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new OracleParameter("COMOCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new OracleParameter("RAOCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new OracleParameter("REOCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            resultadoComparativoMensualDetalle = new BEDataComparativoMensualDetalle();

                            resultadoComparativoMensualDetalle.DataCoasegPorPeriodo = formatearListaCoaseguroPeriodo(dr);
                            dr.NextResult();
                            resultadoComparativoMensualDetalle.DataCoasegPorCompania = formatearListaCoaseguroCompania(dr);
                            dr.NextResult();
                            resultadoComparativoMensualDetalle.DataCoasegPorRamo = formatearListaCoaseguroRamo(dr);
                            dr.NextResult();
                            resultadoComparativoMensualDetalle.DataCoasegPorRegion = formatearListaCoaseguroRegion(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultadoComparativoMensualDetalle;
        }

        private List<BEDataCoasegPorPeriodo> formatearListaCoaseguroPeriodo(OracleDataReader dr)
        {
            List<BEDataCoasegPorPeriodo> listDataPorPeriodo = new List<BEDataCoasegPorPeriodo>();
            while (dr.Read())
            {
                var productionBE = new BEDataCoasegPorPeriodo()
                {
                    Anio = dr["ANIO"].ToString(),
                    MesCodigo = dr["MES_CODIGO"].ToString(),
                    MontoUSD = dr["MONTO_USD"].ToString(),
                    MontoPEN = dr["MONTO_PEN"].ToString(),
                    NombreMes = dr["MES"].ToString(),
                    FechaActualizacion = dr["FEC_ACTUALIZACION"].ToString(),
                };
                listDataPorPeriodo.Add(productionBE);
            }
            return listDataPorPeriodo;
        }

        private List<BEDataCoasegPorCompania> formatearListaCoaseguroCompania(OracleDataReader dr)
        {
            List<BEDataCoasegPorCompania> listDataPorCompania = new List<BEDataCoasegPorCompania>();
            while (dr.Read())
            {
                var productionBE = new BEDataCoasegPorCompania()
                {
                    CodigoCompania = Int32.Parse(dr["COD_COMPANIA"].ToString()),
                    NombreCompania = dr["NOM_COMPANIA"].ToString(),
                    MontoUSD = dr["MONTO_USD"].ToString(),
                    MontoPEN = dr["MONTO_PEN"].ToString()
                };
                listDataPorCompania.Add(productionBE);
            }
            return listDataPorCompania;
        }
        private List<BEDataCoasegPorRamo> formatearListaCoaseguroRamo(OracleDataReader dr)
        {
            List<BEDataCoasegPorRamo> listDataPorRamo = new List<BEDataCoasegPorRamo>();
            while (dr.Read())
            {
                var ramoBE = new BEDataCoasegPorRamo()
                {
                    CodigoRamo = dr["COD_RAMO_COMERCIAL"].ToString(),
                    NombreRamo = dr["NOM_RAMO_COMERCIAL"].ToString(),
                    MontoUSD = dr["MONTO_USD"].ToString(),
                    MontoPEN = dr["MONTO_PEN"].ToString()
                };
                listDataPorRamo.Add(ramoBE);
            }
            return listDataPorRamo;
        }
        private List<BEDataCoasegPorRegion> formatearListaCoaseguroRegion(OracleDataReader dr)
        {
            List<BEDataCoasegPorRegion> listDataPorRegion = new List<BEDataCoasegPorRegion>();
            while (dr.Read())
            {
                var productionBE = new BEDataCoasegPorRegion()
                {
                    CodigoRegion = Int32.Parse(dr["COD_REGION"].ToString()),
                    NombreRegion = dr["NOM_REGION"].ToString(),
                    MontoUSD = dr["MONTO_USD"].ToString(),
                    MontoPEN = dr["MONTO_PEN"].ToString()
                };
                listDataPorRegion.Add(productionBE);
            }
            return listDataPorRegion;
        }
    }
}

