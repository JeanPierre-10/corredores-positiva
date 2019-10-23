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
    public class ProxyProduccion
    {
        #region Singleton
        private static readonly ProxyProduccion _instance = new ProxyProduccion();

        public static ProxyProduccion Instance
        {
            get { return ProxyProduccion._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametrosPrimaCoaseguro(OracleConnection cn, String InicioConsulta, String FinConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker, String busquedaFiltro)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_BALANCE_PRODUCCIONM";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Varchar2).Value = CodigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = CodigoRamo;
            cmd.Parameters.Add("PBROKER", OracleDbType.Varchar2).Value = CodigoBroker;
            cmd.Parameters.Add("PREGION", OracleDbType.Varchar2).Value = CodigoRegion;
            cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = InicioConsulta;
            cmd.Parameters.Add("PBUSQUEDAFIN", OracleDbType.Varchar2).Value = FinConsulta;
            cmd.Parameters.Add("PCASEBUSQUEDA", OracleDbType.Varchar2).Value = busquedaFiltro;
            cmd.Parameters.Add(new OracleParameter("OCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }

        private OracleCommand crearParametrosAvanceCoaseguro(OracleConnection cn, String InicioConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker, String busquedaFiltro)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_PRODUCCION.CORREDORES_AVANCE_PRD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Varchar2).Value = CodigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = CodigoRamo;
            cmd.Parameters.Add("PBROKER", OracleDbType.Varchar2).Value = CodigoBroker;
            cmd.Parameters.Add("PREGION", OracleDbType.Varchar2).Value = CodigoRegion;
            cmd.Parameters.Add("PCASEBUSQUEDA", OracleDbType.Varchar2).Value = busquedaFiltro;
            cmd.Parameters.Add("PBUSQUEDAINIF", OracleDbType.Varchar2).Value = InicioConsulta;
            cmd.Parameters.Add("FECHAACTUALIZACION", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("ANIOPS", OracleDbType.Varchar2, 4).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("MESPS", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("MONTOANPEN", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("MONTOANUSD", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("MONTOMENPEN", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("MONTOMENUSD", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("FILTERANPEN", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("FILTERANUSD", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("FILTERMONPEN", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("FILTERMONUSD", OracleDbType.Decimal, 30).Direction = ParameterDirection.Output;

            return cmd;
        }
        #endregion
        #region Construccion objetos
        private List<BEProduccion> formatearPrimaCoaseguro(OracleDataReader dr)
        {
            var listProductionBE = new List<BEProduccion>();
            while (dr.Read())
            {
                var productionBE = new BEProduccion()
                {
                    DateUpdate = dr["FEC_ACTUALIZACION"].ToString(),
                    CodeMonthYear = dr["COD_ANO_MES"].ToString(),
                    Year = dr["ANIO"].ToString(),
                    Month = dr["MES"].ToString(),
                    AmountPEN = dr["MONTO_PEN"].ToString(),
                    AmountUSD = dr["MONTO_USD"].ToString()
                };
                listProductionBE.Add(productionBE);
            }
            return listProductionBE;
        }
        private BEAvanceCoaseguro formatearAvanceCoaseguro(OracleCommand cmd)
        {
            var anual = new DataAvance(
                                 Decimal.Parse(cmd.Parameters["MONTOANPEN"].Value.ToString()),
                                 Decimal.Parse(cmd.Parameters["MONTOANUSD"].Value.ToString()),
                                 Decimal.Parse(cmd.Parameters["FILTERANUSD"].Value.ToString()),
                                 Decimal.Parse(cmd.Parameters["FILTERANPEN"].Value.ToString()));

            var mensual = new DataAvance(
                     Decimal.Parse(cmd.Parameters["MONTOMENPEN"].Value.ToString()),
                     Decimal.Parse(cmd.Parameters["MONTOMENUSD"].Value.ToString()),
                     Decimal.Parse(cmd.Parameters["FILTERMONUSD"].Value.ToString()),
                     Decimal.Parse(cmd.Parameters["FILTERMONPEN"].Value.ToString()));

            var avanceCoaseguro = new BEAvanceCoaseguro()
            {
                Date = cmd.Parameters["FECHAACTUALIZACION"].Value.ToString(),
                Year = cmd.Parameters["ANIOPS"].Value.ToString(),
                Month = cmd.Parameters["MESPS"].Value.ToString(),
                AnnualData = anual,
                MonthlyData = mensual
            };
            return avanceCoaseguro;
        }
        #endregion
        #region Logica BD
        public List<BEProduccion> GetPrimaCoaseguro(String InicioConsulta, String FinConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker, String busquedaFiltro)
        {
            List<BEProduccion> listProductionBE = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametrosPrimaCoaseguro(cn, InicioConsulta, FinConsulta, CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker, busquedaFiltro))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            listProductionBE = formatearPrimaCoaseguro(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listProductionBE;
        }
        public BEAvanceCoaseguro GetAvanceCoaseguro(String InicioConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker, String busquedaFiltro)
        {
            BEAvanceCoaseguro avanceCoaseguro = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametrosAvanceCoaseguro(cn, InicioConsulta, CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker, busquedaFiltro))
                    {
                        cn.Open();
                        cmd.ExecuteReader();
                        avanceCoaseguro = formatearAvanceCoaseguro(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return avanceCoaseguro;
        }
        #endregion
    }
}

