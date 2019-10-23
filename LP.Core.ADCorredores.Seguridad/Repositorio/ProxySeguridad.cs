using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.ADCorredores.Seguridad.Entity;
using LP.Core.ADCorredores.Util;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Seguridad.Repositorio
{
    public class ProxySeguridad
    {
        #region Singleton
        private static readonly ProxySeguridad _instance = new ProxySeguridad();
        public static ProxySeguridad Instance
        {
            get { return ProxySeguridad._instance; }
        }
        #endregion Singleton
        #region Construccion Parametros
        private OracleCommand crearParametrosAutenticacion(OracleConnection cn, BEAutenticacion entityAutenticacion)
        {
            OracleCommand cmd = new OracleCommand { Connection = cn };
            cmd.CommandText = "INSUDB.SEGURIDADCORREDORES.VALIDARUSUARIO";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("PVALIDATION", OracleDbType.Int32);
            cmd.Parameters["PVALIDATION"].Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add("PUSERNAME", OracleDbType.Varchar2).Value = entityAutenticacion.User;
            cmd.Parameters.Add("PPASSWORD", OracleDbType.Varchar2).Value = entityAutenticacion.Credential;
            return cmd;
        }
        #endregion
        #region Logica BD
        public Boolean PostAutenticacion(BEAutenticacion entityAutenticacion)
        {
            Boolean autenticacion = false;
            try
            {
                using (OracleConnection cn = ConexionOracle.Instancia.Conectar)
                {
                    using (OracleCommand cmd = crearParametrosAutenticacion(cn, entityAutenticacion))
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        autenticacion = (Convert.ToString(cmd.Parameters["PVALIDATION"].Value)).Equals("1") ? true : false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return autenticacion;
        }
        #endregion
    }
}
