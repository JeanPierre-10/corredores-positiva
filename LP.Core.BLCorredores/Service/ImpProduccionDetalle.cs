using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.ADCorredores.Comunes.Util;
using LP.Core.ADCorredores.Entity;

namespace LP.Core.BLCorredores.Service
{
    public class ImpProduccionDetalle : IProduccionDetalle
    {
        public BSProduccionDetalle GetProduccionDetalle(int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {
            String fechaActualizacion = "";
            var coaseguroList = new List<Coaseguro>();
            BSProduccionDetalle response = new BSProduccionDetalle();
            List<BSDataCoaseguroPorPeriodo> DataCoasegPorPeriodo = new List<BSDataCoaseguroPorPeriodo>();
            List<BSDataCoaseguroPorCompania> DataCoasegPorCompania = new List<BSDataCoaseguroPorCompania>();
            List<BSDataCoaseguroPorRamo> DataCoasegPorRamo = new List<BSDataCoaseguroPorRamo>();
            List<BSDataCoaseguroPorRegion> DataCoasegPorRegion = new List<BSDataCoaseguroPorRegion>();
            try
            {
                var DetalleProduccion = ProxyProduccionDetalle.Instance.GetProduccionDetalle(CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker, evaluarParametros(CodigoCompania, CodigoRamo, CodigoRegion, CodigoBroker));

                if (DetalleProduccion == null)
                    return response;
                var listPeriodosAgrupados = DetalleProduccion.DataCoasegPorPeriodo.GroupBy(x => x.MesCodigo).Distinct().ToList();
                var listCompaniasAgrupadas = DetalleProduccion.DataCoasegPorCompania;
                var listRegionAgrupadas = DetalleProduccion.DataCoasegPorRegion;
                var listRamosAgrupadas = DetalleProduccion.DataCoasegPorRamo;


                foreach (var listAgrupadas in listPeriodosAgrupados)
                {
                    foreach (var entity in listAgrupadas)
                    {
                        if (!DataCoasegPorPeriodo.Exists(x => x.CodigoMes == entity.MesCodigo))
                        {
                            DataCoasegPorPeriodo.Add(formatearObjecto(entity, listAgrupadas));
                            fechaActualizacion = entity.FechaActualizacion;
                        }
                    }
                }
                for (int i = 0; i < listCompaniasAgrupadas.Count; i++)
                {
                    String montoPEN = "0";
                    String montoUSD = "0";
                    if (i + 1 != listCompaniasAgrupadas.Count && listCompaniasAgrupadas.ElementAt(i).CodigoCompania == listCompaniasAgrupadas.ElementAt(i + 1).CodigoCompania)
                    {
                        montoPEN = listCompaniasAgrupadas.ElementAt(i + 1).MontoPEN;
                        montoUSD = listCompaniasAgrupadas.ElementAt(i + 1).MontoUSD;
                    }
                    DataCoasegPorCompania.Add(new BSDataCoaseguroPorCompania()
                    {
                        CodigoCompania = listCompaniasAgrupadas.ElementAt(i).CodigoCompania,
                        NombreCompania = listCompaniasAgrupadas.ElementAt(i).NombreCompania,
                        MontoPEN = listCompaniasAgrupadas.ElementAt(i).MontoPEN,
                        MontoUSD = listCompaniasAgrupadas.ElementAt(i).MontoUSD,
                        MontoAnteriorPEN = montoPEN,
                        MontoAnteriorUSD = montoUSD
                    });
                }

                for (int i = 0; i < listRamosAgrupadas.Count; i++)
                {
                    String montoPEN = "0";
                    String montoUSD = "0";
                    if (i + 1 != listRamosAgrupadas.Count && listRamosAgrupadas.ElementAt(i).CodigoRamo == listRamosAgrupadas.ElementAt(i + 1).CodigoRamo)
                    {
                        montoPEN = listRamosAgrupadas.ElementAt(i + 1).MontoPEN;
                        montoUSD = listRamosAgrupadas.ElementAt(i + 1).MontoUSD;
                    }
                    DataCoasegPorRamo.Add(new BSDataCoaseguroPorRamo()
                    {
                        CodigoRamo = listRamosAgrupadas.ElementAt(i).CodigoRamo,
                        NombreRamo = listRamosAgrupadas.ElementAt(i).NombreRamo,
                        MontoPEN = listRamosAgrupadas.ElementAt(i).MontoPEN,
                        MontoUSD = listRamosAgrupadas.ElementAt(i).MontoUSD,
                        MontoAnteriorPEN = montoPEN,
                        MontoAnteriorUSD = montoUSD
                    });
                }

                for (int i = 0; i < listRegionAgrupadas.Count; i++)
                {
                    String montoPEN = "0";
                    String montoUSD = "0";
                    if (i + 1 != listRegionAgrupadas.Count && listRegionAgrupadas.ElementAt(i).CodigoRegion == listRegionAgrupadas.ElementAt(i + 1).CodigoRegion)
                    {
                        montoPEN = listRegionAgrupadas.ElementAt(i + 1).MontoPEN;
                        montoUSD = listRegionAgrupadas.ElementAt(i + 1).MontoUSD;
                    }
                    DataCoasegPorRegion.Add(new BSDataCoaseguroPorRegion()
                    {
                        CodigoRegion = listRegionAgrupadas.ElementAt(i).CodigoRegion,
                        NombreRegion = listRegionAgrupadas.ElementAt(i).NombreRegion,
                        MontoPEN = listRegionAgrupadas.ElementAt(i).MontoPEN,
                        MontoUSD = listRegionAgrupadas.ElementAt(i).MontoUSD,
                        MontoAnteriorPEN = montoPEN,
                        MontoAnteriorUSD = montoUSD
                    });
                }
                response = new BSProduccionDetalle()
                {
                    FechaActualizacion = fechaActualizacion,
                    DataCoasegPorPeriodo = DataCoasegPorPeriodo.OrderByDescending(x => x.CodigoMes).ToList(),
                    DataCoasegPorRegion = DataCoasegPorRegion.Distinct(new BSDataCoaseguroPorRegion()).OrderBy(x => x.CodigoRegion).ToList(),
                    DataCoasegPorRamo = DataCoasegPorRamo.Distinct(new BSDataCoaseguroPorRamo()).OrderBy(x => x.CodigoRamo).ToList(),
                    DataCoasegPorCompania = DataCoasegPorCompania.Distinct(new BSDataCoaseguroPorCompania()).ToList()
                };


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
        private String evaluarParametros(int CodigoCompania, String CodigoRamo, int CodigoRegion, int CodigoBroker)
        {
            String fitroBusqueda = String.Concat("T", (CodigoCompania > 0 ? "C" : ""), (!CodigoRamo.Equals("0") ? "R" : ""), (CodigoRegion > 0 ? "G" : ""), (CodigoBroker > 0 ? "B" : ""));

            return fitroBusqueda;
        }
        private BSDataCoaseguroPorPeriodo formatearObjecto(BEDataCoasegPorPeriodo entity, IGrouping<String, BEDataCoasegPorPeriodo> listAgrupadas)
        {
            DateTime fechaActual = DateTime.Today;
            var listTemp = listAgrupadas.Where(x => x.Anio != entity.Anio).ToList();
            String montoPen = "0.0", montoUsd = "0.0", montoPenAnt = "0.0", montoUsdAnt = "0.0";
            if (fechaActual.Year != Int32.Parse(entity.Anio) && fechaActual.Year - Int32.Parse(entity.Anio) == 0)
            {
                montoPen = (listTemp.Count == 0 ? "0" : listTemp.First().MontoPEN);
                montoUsd = (listTemp.Count == 0 ? "0" : listTemp.First().MontoUSD);
            }
            else if (fechaActual.Year - Int32.Parse(entity.Anio) == 0)
            {
                montoPen = entity.MontoPEN;
                montoUsd = entity.MontoUSD;
                montoPenAnt = (listTemp.Count == 0 || fechaActual.Year - Int32.Parse(listTemp.First().Anio) != 1 ? "0" : listTemp.First().MontoPEN);
                montoUsdAnt = (listTemp.Count == 0 || fechaActual.Year - Int32.Parse(listTemp.First().Anio) != 1 ? "0" : listTemp.First().MontoUSD);
            }
            else if (fechaActual.Year - Int32.Parse(entity.Anio) == 1)
            {
                montoPenAnt = entity.MontoPEN;
                montoUsdAnt = entity.MontoUSD;

            }


            return new BSDataCoaseguroPorPeriodo()
            {
                CodigoMes = entity.MesCodigo,
                NombreMes = entity.NombreMes,
                MontoPEN = montoPen,
                MontoUSD = montoUsd,
                MontoAnteriorPEN = montoPenAnt,
                MontoAnteriorUSD = montoUsdAnt
            };
        }
    }
}
