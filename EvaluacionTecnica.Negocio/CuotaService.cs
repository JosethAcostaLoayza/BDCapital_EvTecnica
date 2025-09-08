using EvaluacionTecnica.Datos;
using System;
using System.Data;

namespace EvaluacionTecnica.Negocio
{
    public class CuotaService
    {
        private readonly CuotaRepository _repo;

        public CuotaService()
        {
            _repo = new CuotaRepository();
        }

        public DataTable ListarPrestamos()
        {
            return _repo.ListarPrestamos();
        }

        public DataTable BuscarCuotasPendientes(int prestamoId, DateTime fechaRef)
        {
            if (prestamoId <= 0) throw new Exception("El ID del préstamo no es válido.");
            return _repo.BuscarCuotasPendientes(prestamoId, fechaRef);
        }

        public void PagarCuota(int pagoId, int numeroCuota)
        {
            if (pagoId <= 0 || numeroCuota <= 0) throw new Exception("Datos inválidos para pagar cuota.");
            _repo.PagarCuota(pagoId, numeroCuota);
        }
    }
}
