using LP.Core.BLCorredores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Core.BLCorredores.Beans;
using LP.Core.ADCorredores.Repositorio;
using LP.Core.BLCorredores.Utils;

namespace LP.Core.BLCorredores.Service
{
    public class ImpComision : IComision
    {
        public BSClientePotencial obtenerClientesPotencials(int CodigoCompania, String CodigoRamo, int CodigoBroker, String CantidadRegistros, String DiasBusqueda)
        {
            var entity = ProxyClientePotencial.Instance.obtenerClientesPotenciales(new ADCorredores.EntityRequest.BEClientePotencialReq()
            {
                cantidadRegistros = CantidadRegistros,
                codigoBroker = CodigoBroker,
                codigoCompania = CodigoCompania,
                codigoRamo = CodigoRamo,
                diasBusqueda = DiasBusqueda,
                filtroBusqueda = Util.evaluarParametros(CodigoCompania, CodigoRamo, 0, CodigoBroker)
            });
            if (null == entity)
                return new BSClientePotencial();

            return new BSClientePotencial()
            {
                fechaActualizacion = entity.fechaActualizacion,
                clientesPotenciales = entity.clientesPotenciales.Select(x => new BSCliente()
                {
                    codigoCliente = x.codigoCliente,
                    comisionSOL = x.comisionSOL,
                    comisionUSD = x.comisionUSD,
                    primaSOL = x.primaSOL,
                    cantidadDocumentos = x.cantidadDocumentos,
                    cantidadPolizas = x.cantidadPolizas,
                    nombre = x.nombre,
                    primaUSD = x.primaUSD
                }).ToList()
            };
        }

        public BSProduccion obtenerComisionPotencial(int CodigoCompania, String CodigoRamo, int CodigoBroker)
        {
            var entity = ProxyComision.Instance.obtenerComisionPotencial(CodigoCompania, CodigoRamo, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, 0, CodigoBroker));

            return new BSProduccion()
            {
                fechaActualizacion = entity.fechaActualizacion,
                comisionPotencial = entity.comisionPotencial.Select(x => new ComisionPotencial()
                {
                    periodo = x.fechaVencimiento,
                    primaSOL = x.primaSol,
                    comisionSOL = x.comisionSOL,
                    comisionUSD = x.comisionUSD,
                    primaUSD = x.primaUSD
                }).ToList()
            };
        }

        public BSComisionRG obtenerComisionPotencialRiesgo(int CodigoCompania, String CodigoRamo, int CodigoBroker, String FiltroBusqueda, String FiltroPeriodo)
        {
            var entity = ProxyComisionRiesgo.Instance.obtenerComisionPotencialRiesgo(CodigoCompania, CodigoRamo, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, 0, CodigoBroker), FiltroBusqueda, FiltroPeriodo);

            return new BSComisionRG()
            {
                fechaActualizacion = entity.fechaActualizacion,
                comisionRiesgo = entity.comisionRiesgo.Select(x => new ComisionPotencialR()
                {
                    periodo = x.periodo,
                    comisionSOL = x.comisionSOL,
                    comisionUSD = x.comisionUSD,
                    primaSOL = x.primaSOL,
                    primaUSD = x.primaUSD
                }).ToList()
            };
        }

        private List<ComisionRiesgo> crearComision(List<LP.Core.ADCorredores.Entity.ComisionRiesgo> listaComision)
        {
            return listaComision.Select(x => new ComisionRiesgo()
            {
                periodo = x.periodo,
                comisionPEN = x.comisionPEN,
                primaPEN = x.primaPEN,
                comisionUSD = x.comisionUSD,
                primaUSD = x.primaUSD,
                nombre = x.nombre
            }).ToList();
        }

        // TEMPORAL RIESGO DETALLE
        private List<ComisionRiesgo> crearComision(List<LP.Core.ADCorredores.Entity.ComisionRiesgo> listaComision, string moneda)
        {
            if (moneda.Equals("PEN"))
            {
                return listaComision.Select(x => new ComisionRiesgo()
                {
                    periodo = x.periodo,
                    comisionPEN = x.comisionPEN,
                    primaPEN = x.primaPEN,
                    nombre = x.nombre // recien agregado 11-10-19, sin publicar
                }).ToList();
            }
            else
            {
                return listaComision.Select(x => new ComisionRiesgo()
                {
                    periodo = x.periodo,
                    comisionUSD = x.comisionUSD,
                    primaUSD = x.primaUSD,
                    nombre = x.nombre // recien agregado 11-10-19, sin publicar
                }).ToList();
            }
        }

        public BSComisionRGDetalle obtenerComisionPotencialRiesgoDetalle(int CodigoCompania, string CodigoRamo, int CodigoBroker, string FiltroBusqueda, string FiltroPeriodo)
        {
            var entity = ProxyComisionRiesgoDetalle.Instance.obtenerComisionPotencialRiesgo(CodigoCompania, CodigoRamo, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, 0, CodigoBroker), FiltroBusqueda, FiltroPeriodo);

            return new BSComisionRGDetalle()
            {
                fechaActualizacion = entity.fechaActualizacion,
                broker = entity.broker,
                compania = entity.compania,
                ramo = entity.ramo,
                comisionRiesgo = crearComision(entity.comisionRiesgo),
                comisionRiesgoBroker = crearComision(entity.comisionRiesgoBroker),
                comisionRiesgoCompania = crearComision(entity.comisionRiesgoCompania),
                comisionRiesgoRamo = crearComision(entity.comisionRiesgoRamo)
            };
        }

        // TEMPORAL
        public BSComisionRGDetalle obtenerComisionPotencialRiesgoDetalleTemporal(int CodigoCompania, string CodigoRamo, int CodigoBroker, string FiltroBusqueda, string FiltroPeriodo, string moneda)
        {
            var entity = ProxyComisionRiesgoDetalle.Instance.obtenerComisionPotencialRiesgo(CodigoCompania, CodigoRamo, CodigoBroker, Util.evaluarParametros(CodigoCompania, CodigoRamo, 0, CodigoBroker), FiltroBusqueda, FiltroPeriodo);

            return new BSComisionRGDetalle()
            {
                fechaActualizacion = entity.fechaActualizacion,
                broker = entity.broker,
                compania = entity.compania,
                ramo = entity.ramo,
                comisionRiesgo = crearComision(entity.comisionRiesgo, moneda),
                comisionRiesgoBroker = crearComision(entity.comisionRiesgoBroker, moneda),
                comisionRiesgoCompania = crearComision(entity.comisionRiesgoCompania, moneda),
                comisionRiesgoRamo = crearComision(entity.comisionRiesgoRamo, moneda)
            };
        }
    }
}
