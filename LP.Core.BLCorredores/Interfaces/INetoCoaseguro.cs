using LP.Core.BLCorredores.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Interfaces
{
    public interface INetoCoaseguro
    {
        BSPrimaCoaseguro GetPrimaCoaseguro(String InicioConsulta, String FinConsulta, int CodigoCompania,
                                                String CodigoRamo, int CodigoRegion, int CodigoBroker);

        BSAvanceCoaseguro GetAvanceCoaseguro(String InicioConsulta, int CodigoCompania,
                                                String CodigoRamo, int CodigoRegion, int CodigoBroker);
    }
}
