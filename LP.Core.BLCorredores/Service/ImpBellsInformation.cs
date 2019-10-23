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
    public class ImpBellsInformation : IBellsInformation
    {
        public List<BSBells> GetBells()
        {
            var listBells = new List<BSBells>();
            try
            {
                var bellsAD = ProxyBells.Instance.ObtenerCampanias();
                if (bellsAD != null)
                {
                    listBells = bellsAD.Select(x => new BSBells() { Codigo = x.Code, Nombre = x.Name }).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listBells;

        }
    }
}
