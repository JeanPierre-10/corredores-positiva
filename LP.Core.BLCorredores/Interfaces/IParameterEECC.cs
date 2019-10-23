using LP.Core.BLCorredores.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Interfaces
{
    public interface IParameterEECC
    {
        List<BSParameterEECC> ObtenerParametrosEECC(String parameterSearch, String parameterState);
    }
}
