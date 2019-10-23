using LP.Core.ADCorredores.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace LP.Core.ADCorredores.Util
{
    /// <summary>
    /// Clase para administrar las conexiones a las diferentes bases de datos (necesita un archivo de configuración en el cliente)
    /// </summary>
    public class ConexionOracle
    {

        private static readonly ConexionOracle _instancia = new ConexionOracle();


        public static ConexionOracle Instancia
        {
            get { return ConexionOracle._instancia; }
        }

        private ConexionOracle() { }

        public OracleConnection Conectar
        {
            get
            {
                var appSettings = ConfigurationManager.AppSettings;
                var sqlConnection = new OracleConnection(Encryptar.Decrypt(appSettings["ConexionLPCorredores"]));
                // var sqlConnection = new OracleConnection(appSettings["ConexionLPCorredores"]);
                return sqlConnection;
            }
        }
    }
}
