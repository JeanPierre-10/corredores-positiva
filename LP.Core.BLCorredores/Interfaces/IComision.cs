using LP.Core.BLCorredores.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Interfaces
{
    public interface IComision
    {
        BSProduccion obtenerComisionPotencial(int CodigoCompania, String CodigoRamo, int CodigoBroker);
        BSClientePotencial obtenerClientesPotencials(int CodigoCompania, String CodigoRamo, int CodigoBroker, String CantidadRegistros, String DiasBusqueda);
        BSComisionRG obtenerComisionPotencialRiesgo(int CodigoCompania, String CodigoRamo, int CodigoBroker, String FiltroBusqueda, String FiltroPeriodo);
        BSComisionRGDetalle obtenerComisionPotencialRiesgoDetalle(int CodigoCompania, String CodigoRamo, int CodigoBroker, String FiltroBusqueda, String FiltroPeriodo);

        // TEMPORAL
        BSComisionRGDetalle obtenerComisionPotencialRiesgoDetalleTemporal(int CodigoCompania, String CodigoRamo, int CodigoBroker, String FiltroBusqueda, String FiltroPeriodo, string moneda);
    }
}
