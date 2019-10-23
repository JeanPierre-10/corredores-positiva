using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.ADCorredores.Entity;

namespace LP.Core.BLCorredores.Service
{
    public class ImpParameterEECC : IParameterEECC
    {
        public List<BSParameterEECC> ObtenerParametrosEECC(String parameterSearch, String parameterState)
        {
            var listRetorno = new List<BSParameterEECC>();
            var listBEParametrosEECC = ProxyParameterEECC.Instance.ObtenerParametrosEECC(parameterSearch, parameterState);

            if (listBEParametrosEECC != null)
            {
                String[] parameterSearchFilter = parameterSearch.Split(',');
                foreach (var parametro in parameterSearchFilter)
                {
                    listRetorno.Add(new BSParameterEECC()
                    {
                        parametros = RetornarModelo(parametro, listBEParametrosEECC).OrderBy(x => x.Codigo).ToList(),
                        tipoParametro = parametro
                    }); 
                }
            }


            return listRetorno;
        }

        private List<BSParameterEECCModel> RetornarModelo(String parameter, List<BEDataEECC> listBEParametrosEECC)
        { 
            return listBEParametrosEECC.Where(x => x.TipoParametro.Equals(parameter)).ToList().Select(s => new BSParameterEECCModel
            {
                Codigo = s.Codigo,
                Descripcion = s.Descripcion,
                ValorCadena = s.ValorCadena,
                ValorNumerico = s.ValorNumerico,
                Estado = s.Estado //.Equals("A") ? "Activo" : "Inactivo"
            }).ToList();
        }
    }
}
