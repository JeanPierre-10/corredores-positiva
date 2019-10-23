using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSProduccionMensual
    {
        public String InicioConsulta { get; set; }
        public String FinConsulta { get; set; }
        public BSBells Bells { get; set; }
        public BSBouquet Bouquet { get; set; }
        public BSRegion Region { get; set; }
        public BSBroker Broker { get; set; }
    }
}
