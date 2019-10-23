using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.ADCorredores.Comunes.Util;

namespace LP.Core.BLCorredores.Service
{
    public class ImpRegion : IRegion
    {
        /// <summary>
        /// Método que retorna una lista de las regiones almacenadas en BD
        /// </summary>
        /// <returns></returns>
        public List<BSRegion> GetRegion()
        {
            var listRegion = new List<BSRegion>();
            try
            {
                var regionAD = ProxyRegion.Instance.GetRegion();
                listRegion = regionAD.Select(entity => new BSRegion() { Codigo = entity.Code, Nombre = entity.Name }).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listRegion;
        }
    }
}
