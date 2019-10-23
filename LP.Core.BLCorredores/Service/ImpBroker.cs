using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.ADCorredores.Entity;
using LP.Core.ADCorredores.Comunes.Util;

namespace LP.Core.BLCorredores.Service
{
    public class ImpBroker : IBroker
    {
        public List<BSBroker> GetBroker()
        {
            var listBroker = new List<BSBroker>();
            try
            {
                var bouquetAD = ProxyBroker.Instance.GetBroker();
                if (bouquetAD != null)
                {
                    listBroker = bouquetAD.Select(entity => new BSBroker()
                    {
                        Codigo = entity.Code,
                        Nombre = entity.Name,
                        CodigoInsunix = entity.CodigoInsunix,
                        CodigoVTime = entity.CodigoVTime
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listBroker;
        }


    }
}
