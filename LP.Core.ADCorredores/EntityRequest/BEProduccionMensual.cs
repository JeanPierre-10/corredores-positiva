using LP.Core.ADCorredores.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.EntityRequest
{
    public class BEProduccionMensual
    {
        public BEBells Bells { get; set; }
        public BEBouquet Bouquet { get; set; }
        public BERegion Region { get; set; }
        public BEBroker Broker { get; set; }
    }
}

