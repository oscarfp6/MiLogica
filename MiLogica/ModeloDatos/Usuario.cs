using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MiLogica.Utils;

namespace MiLogica.ModeloDatos
{

    public class Usuario
    {
        private int id;
        private int Id { get;}


        private string nombre;
        private string Nombre { get; }

        private string password;
        private string Password { set { this.password = Encriptar.EncriptarPasswordSHA256(value); } }

        private string apellidos;
        private string Apellidos { get; }


        public string email;
        public string Email { get; }

        private bool suscripcion;
        public bool Suscripcion { get; set; }

        public DateTime lastLogin;
        private DateTime LastLogin { get; set; }


        //Atributos para la lógica de bloqueo
        private EstadoUsuario estado;

        private List<DateTime> intentosFallidosTimestamps;

        public Usuario(int id, string nombre, string password, string apellidos, string email, bool suscripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.password = Encriptar.EncriptarPasswordSHA256(password);
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

        public bool PermitirLogin(string password)
        {
            string passwordEncriptada = Encriptar.EncriptarPasswordSHA256(password);
            if (estado == EstadoUsuario.Bloqueado) { return false; }
            if (this.password == passwordEncriptada)
            {
                this.intentosFallidosTimestamps.Clear();
                this.estado = EstadoUsuario.Activo;
                this.lastLogin = DateTime.Now;
                return true;
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta");
                this.intentosFallidosTimestamps.Add(DateTime.Now);
                var now = DateTime.Now;
                this.intentosFallidosTimestamps = this.intentosFallidosTimestamps
                    .Where(t => (now - t).TotalMinutes <= 5)
                    .ToList();
                if (this.intentosFallidosTimestamps.Count >= 3)
                {
                    this.estado = EstadoUsuario.Bloqueado;          
                    Console.WriteLine("Su cuenta ha sido bloqueada temporalmente por motivos de seguridad");

                }
                return false;
            }

        }
        public bool ComprobarPassWord(string passwordAComprobar)
        {
            string passwordEncriptada = Encriptar.EncriptarPasswordSHA256(passwordAComprobar);


            if (this.password == passwordEncriptada){
                return true;
            } else
            {
                return false;
            }
        }
        
        public bool CambiarPassword(string passwordActual, string nuevoPassword)
        {
            if(ComprobarPassWord(passwordActual) && estado==EstadoUsuario.Activo && Utils.Password.ValidarPassword(nuevoPassword)) 
            {
                this.password = Encriptar.EncriptarPasswordSHA256(nuevoPassword);
                return true;
            } else
            {
                return false;
            }
        }



        

        public bool DesbloquearUsuario (string email, string passwordDado )
        {
            string passwordEncriptado = Utils.Encriptar.EncriptarPasswordSHA256(passwordDado);
            if (this.email == email && this.estado == EstadoUsuario.Bloqueado && this.password == passwordEncriptado)
            {
                this.estado = EstadoUsuario.Activo;
                this.intentosFallidosTimestamps.Clear();
                return true;
            } else
            {
                return false;
            }
        }

        public bool verificarInactividad() {
            // Si el usuario ha estado inactivo por más de 6 meses (traducido a 182 días), cambiar su estado a Inactivo y devuelve true.
            if (this.estado == EstadoUsuario.Activo && (DateTime.Now - this.lastLogin).TotalDays > 182 /*medio año*/)
            {
                this.estado = EstadoUsuario.Inactivo;
                return true;
            }
            return false;
        }


    }



}
