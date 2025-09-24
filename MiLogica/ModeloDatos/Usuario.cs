using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MiLogica.ModeloDatos
{

    public class Usuario
    {
        private int id;
        public int Id { get;}
        private string nombre;
        public string Nombre { get; }
        private string password;
        private string apellidos;
        public string Apellidos { get; }
        private string email;
        public string Email { get; }
        private bool suscripcion;
        public bool Suscripcion { get; set; }


        //Atributos para la lógica de bloqueo
        private EstadoUsuario estado;

        private List<DateTime> intentosFallidosTimestamps;

        public Usuario(int id, string nombre, string password, string apellidos, string email, bool suscripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.password = password;
            this.apellidos = apellidos;
            this.email = email;
            this.suscripcion = suscripcion;

            this.estado = EstadoUsuario.Activo;
            this.intentosFallidosTimestamps = new List<DateTime>(); // ¡Esta es la línea clave!

        }


        public EstadoUsuario Estado
        {
            get { return estado; }
            private set { estado = value; }
        }

        public bool ComprobarPassWord(string passwordAComprobar)
        {
            if (estado == EstadoUsuario.Bloqueado)
            {
                return false;
            }

            if (this.password == passwordAComprobar)
            {
                this.intentosFallidosTimestamps.Clear();
                this.estado = EstadoUsuario.Activo;
                return true;
            } else
            {
                this.intentosFallidosTimestamps.Add(DateTime.Now);
                var now = DateTime.Now;
                this.intentosFallidosTimestamps = this.intentosFallidosTimestamps
                    .Where(t => (now - t).TotalMinutes <= 5)
                    .ToList();
                if (this.intentosFallidosTimestamps.Count >= 3)
                {
                    this.estado = EstadoUsuario.Bloqueado;

                }
                return false;
            }
        }
        
        public bool CambiarPassword(string passwordActual, string nuevoPassword)
        {
            if(ComprobarPassWord(passwordActual) && estado==EstadoUsuario.Activo /*&&ValidarPassWord(passwordActual)*/) 
            {
                this.password= nuevoPassword;
                return true;
            } else
            {
                return false;
            }
        }


    }



}
