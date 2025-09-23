using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiLogica.ModeloDatos
{
    public enum EstadoUsuario
    {
        Inactivo = 0,
        Activo = 1,
        Suspendido = 2
    }
    public class Usuario
    {
        private int id;
        private string nombre;
        private string password;
        private string apellidos;
        private string email;
        private bool suscripcion;
        private int estado;
    }
}
