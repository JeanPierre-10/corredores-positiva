using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSProduccion
    {
        public String fechaActualizacion { get; set; }
        public List<ComisionPotencial> comisionPotencial { get; set; }

    }
    public class ComisionPotencial
    {
        public String periodo { get; set; }
        public String primaSOL { get; set; }
        public String primaUSD { get; set; }
        public String comisionSOL { get; set; }
        public String comisionUSD { get; set; }

    }
}
