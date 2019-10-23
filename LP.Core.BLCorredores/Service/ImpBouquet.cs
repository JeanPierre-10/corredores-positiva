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
    public class ImpBouquet : IBouquet
    {
        /// <summary>
        /// Método que retorna una lista de Bouquet
        /// </summary>
        /// <param name="codeBells"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public List<BSBouquet> GetBouquet(String codeBells, String flag = "")
        {
            var listBouquet = new List<BSBouquet>();
            try
            {
                var bouquetAD = ProxyBouquet.Instance.GetBouquet(codeBells, flag);
                if (bouquetAD != null)
                {
                    listBouquet = bouquetAD.Select(entity => new BSBouquet { Codigo = entity.Code, Nombre = entity.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listBouquet;
        }
    }
}
