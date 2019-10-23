using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Seguridad.Repositorio;

namespace LP.Core.BLCorredores.Service
{
    public class ImpSeguridad : ISeguridad
    {
        public Boolean PostAutenticacion(BSAutenticacion autenticacionBean)
        {
            Boolean autenticacion = false;
            try
            {
                autenticacion = ProxySeguridad.Instance.PostAutenticacion(
                        new ADCorredores.Seguridad.Entity.BEAutenticacion() {
                            Credential = autenticacionBean.Credencial, User = autenticacionBean.Usuario });
            }
            catch (Exception e) { throw e; }
            return autenticacion;
        }
    }
}
