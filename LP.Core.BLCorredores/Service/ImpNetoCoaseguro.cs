using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.ADCorredores.EntityRequest;
using LP.Core.ADCorredores.Entity;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.BLCorredores.Utils;

namespace LP.Core.BLCorredores.Service
{
    public class ImpNetoCoaseguro : INetoCoaseguro
    {
        public BSAvanceCoaseguro GetAvanceCoaseguro(String InicioConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {
            BSAvanceCoaseguro response = new BSAvanceCoaseguro();
            var avanceCoaseguro = ProxyProduccion.Instance.GetAvanceCoaseguro(Util.formatearFecha(InicioConsulta), CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker));
            if (avanceCoaseguro == null)
                return response;

            response = new BSAvanceCoaseguro()
            {
                Year = avanceCoaseguro.Year,
                Date = avanceCoaseguro.Date,
                Month = avanceCoaseguro.Month,
                AnnualData = new DataAmount()
                {
                    AmountFilteredPEN = avanceCoaseguro.AnnualData.AmountFilteredPEN,
                    AmountFilteredUSD = avanceCoaseguro.AnnualData.AmountFilteredUSD,
                    AmountPercentaje = avanceCoaseguro.AnnualData.AmountPercentaje,
                    AmountTotalPEN = avanceCoaseguro.AnnualData.AmountTotalPEN,
                    AmountTotalUSD = avanceCoaseguro.AnnualData.AmountTotalUSD,
                },
                MonthlyData = new DataAmount()
                {
                    AmountFilteredPEN = avanceCoaseguro.MonthlyData.AmountFilteredPEN,
                    AmountFilteredUSD = avanceCoaseguro.MonthlyData.AmountFilteredUSD,
                    AmountPercentaje = avanceCoaseguro.MonthlyData.AmountPercentaje,
                    AmountTotalPEN = avanceCoaseguro.MonthlyData.AmountTotalPEN,
                    AmountTotalUSD = avanceCoaseguro.MonthlyData.AmountTotalUSD,
                },
            };

            return response;
        }

        public BSPrimaCoaseguro GetPrimaCoaseguro(String InicioConsulta, String FinConsulta, int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {

            var coaseguroList = new List<Coaseguro>();
            BSPrimaCoaseguro response = new BSPrimaCoaseguro();
            try
            {
                var PrimaCoaseguro = ProxyProduccion.Instance.GetPrimaCoaseguro(Util.formatearFecha(InicioConsulta), Util.formatearFecha(FinConsulta), CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker));
                if (PrimaCoaseguro == null)
                    return response;

                var listYear = PrimaCoaseguro.Select(p => p.Year).Distinct().ToList();
                var anioCoaseguro = PrimaCoaseguro.Select(p => p.CodeMonthYear).Distinct().First();
                foreach (var entity in listYear)
                {
                    var listGroupYear = PrimaCoaseguro.Where(lf => lf.Year == entity).ToList();
                    coaseguroList.Add(new Coaseguro()
                    {
                        Year = entity,
                        PrimaNeta = listGroupYear.Select(p => new PrimaNeta()
                        {
                            AmountUSD = p.AmountUSD,
                            AmountPEN = p.AmountPEN,
                            Month = p.Month
                        }).ToList()
                    });
                }
                response = new BSPrimaCoaseguro() { Date = anioCoaseguro, DataCoaseguro = coaseguroList };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


    }
}
