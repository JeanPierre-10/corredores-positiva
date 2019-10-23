using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEComisionPG
    {
        public String fechaActualizacion { get; set; }
        public List<ComisionPotencial> comisionPotencial { get; set; }
    }
}
