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
    public class ProxyClientePotencial
    {
        #region Singleton
        private static readonly ProxyClientePotencial _instance = new ProxyClientePotencial();
        public static ProxyClientePotencial Instance
        {
            get { return ProxyClientePotencial._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametros(OracleConnection cn, BEClientePotencialReq requestClientePotencial)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.CORREDORES_PKG_POTENCIAL.CORREDORES_GET_CLIENTES_TOP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("PCOMPANIA", OracleDbType.Int32).Value = requestClientePotencial.codigoCompania;
            cmd.Parameters.Add("PRAMO", OracleDbType.Varchar2).Value = (requestClientePotencial.codigoRamo.Equals("0") ? "T" : requestClientePotencial.codigoRamo.ToString());
            cmd.Parameters.Add("PBROKER", OracleDbType.Int32).Value = requestClientePotencial.codigoBroker;
            cmd.Parameters.Add("PBUSQUEDAINI", OracleDbType.Varchar2).Value = requestClientePotencial.filtroBusqueda;
            cmd.Parameters.Add("CANTIDAD_REGISTROS", OracleDbType.Varchar2).Value = requestClientePotencial.cantidadRegistros;
            cmd.Parameters.Add("DIAS_BUSQUEDA", OracleDbType.Varchar2).Value = requestClientePotencial.diasBusqueda;
            cmd.Parameters.Add("FECHA_ACTUALIZACION", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Output;
            cmd.Parameters.Add(new OracleParameter("POCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            return cmd;
        }
        #endregion  Construccion Parametros
        #region Construccion objetos
        private List<BECliente> formatearListaClientes(OracleDataReader dr)
        {
            List<BECliente> listaClientes = new List<BECliente>();
            while (dr.Read())
            {
                listaClientes.Add(
                    new BECliente()
                    {
                        codigoCliente = dr["COD_CLIENTE"].ToString(),
                        nombre = dr["NOMBRE_CLIENTE"].ToString(),
                        cantidadDocumentos = String.Empty.Equals(dr["CANTIDAD_DOC_COBRO"].ToString()) ? "0" : dr["CANTIDAD_DOC_COBRO"].ToString(),
                        cantidadPolizas = String.Empty.Equals(dr["CANTIDAD_POLIZAS"].ToString()) ? "0" : dr["CANTIDAD_POLIZAS"].ToString(),
                        comisionUSD = String.Empty.Equals(dr["COMISION_POTENCIAL_DOL"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL_DOL"].ToString(),

                        primaSOL = String.Empty.Equals(dr["PRIMA_EMITIDA"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA"].ToString(),
                        primaUSD = String.Empty.Equals(dr["PRIMA_EMITIDA_DOL"].ToString()) ? "0.0" : dr["PRIMA_EMITIDA_DOL"].ToString(),
                        comisionSOL = String.Empty.Equals(dr["COMISION_POTENCIAL"].ToString()) ? "0.0" : dr["COMISION_POTENCIAL"].ToString()
                    });
            }
            return listaClientes;
        }
        #endregion Construccion objetos
        #region Logica BD
        public BEClientePotencial obtenerClientesPotenciales(BEClientePotencialReq requestClientePotencial)
        {
            BEClientePotencial clientePotencialResp = null;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametros(cn, requestClientePotencial))
                    {
                        cn.Open();
                        OracleDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            clientePotencialResp = new BEClientePotencial()
                            {
                                fechaActualizacion = cmd.Parameters["FECHA_ACTUALIZACION"].Value.ToString(),
                                clientesPotenciales = formatearListaClientes(dr)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return clientePotencialResp;
        }
        #endregion Logica BD

    }
}
