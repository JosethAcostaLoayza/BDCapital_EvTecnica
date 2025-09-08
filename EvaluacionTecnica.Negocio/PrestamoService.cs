using EvaluacionTecnica.Datos;
using System.Data;

namespace EvaluacionTecnica.Negocio
{
    public class PrestamoService
    {
        private readonly PrestamoRepository _repo;

        public PrestamoService()
        {
            _repo = new PrestamoRepository();
        }

        public void RegistrarPrestamo(string nombre, string tipoDoc, string numDoc,
                                      string direccion, string telefono, string email,
                                      string tipoCliente, decimal monto, int plazoMeses, decimal tasaInteres)
        {
            // 🔹 Aquí podrías meter validaciones de negocio
            if (monto <= 0) throw new System.Exception("El monto debe ser mayor a cero.");
            if (plazoMeses <= 0) throw new System.Exception("El plazo debe ser mayor a cero.");

            _repo.RegistrarPrestamo(nombre, tipoDoc, numDoc, direccion, telefono, email, tipoCliente, monto, plazoMeses, tasaInteres);
        }

        public DataTable ObtenerCronograma(string numDoc)
        {
            return _repo.ObtenerCronograma(numDoc);
        }
    }
}
