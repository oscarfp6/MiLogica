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
        private int Id { get; set; }

        private string Nombre { get; set; }
        private string Apellidos { get; set; }
        public bool Suscripcion { get; set; }

        public string Email { get; private set; }

        private string _passwordHash;

        public DateTime lastLogin { get; private set; }


        //Atributos para la lógica de bloqueo
        private EstadoUsuario Estado { get; private set; }

        private List<DateTime> intentosFallidosTimestamps;

        public Usuario() 
        {
            this.Email = string.Empty;
            this.Nombre = string.Empty;
            this.Apellidos = string.Empty;
            this._passwordHash = string.Empty;
            this.intentosFallidosTimestamps = new List<DateTime>();
        }

        public Usuario(int id, string nombre, string password, string apellidos, string email, bool suscripcion)
        {

            if (!Utils.Email.ValidarEmail(email))
            {
                throw new ArgumentException("El formato del email no es valido");
            }
            if (!Utils.Password.ValidarPassword(password))
            {
                throw new ArgumentException("La contraseña no cumple los requisitos de seguridad.");
            }
            this.Id = id;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Email = email;
            this.suscripcion = suscripcion;

            this.password = Encriptar.EncriptarPasswordSHA256(password);

            this.estado = EstadoUsuario.Activo;
            this.intentosFallidosTimestamps = new List<DateTime>(); // ¡Esta es la línea clave!
            this.lastLogin = DateTime.Now;

        }

        public bool PermitirLogin(string password)
        {
            string passwordEncriptada = Encriptar.EncriptarPasswordSHA256(password);
            VerificarInactividad();
            if (estado == EstadoUsuario.Bloqueado || estado == EstadoUsuario.Inactivo) { return false; }
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
            if (this.email == email && (this.estado == EstadoUsuario.Bloqueado || this.estado == EstadoUsuario.Inactivo) && this.password == passwordEncriptado)
            {
                this.estado = EstadoUsuario.Activo;
                this.intentosFallidosTimestamps.Clear();
                this.lastLogin = DateTime.Now;
                return true;
            } else
            {
                return false;
            }
        }



        
        public void VerificarInactividad()
        {
            if (this.Estado == EstadoUsuario.Activo)
            {
                TimeSpan tiempoSinAcceder = DateTime.Now - this.lastLogin;
                if(tiempoSinAcceder > TimeSpan.FromDays(182)) // Aproximadamente 6 meses
                {
                    this.Estado = EstadoUsuario.Inactivo;
                }
            }
        }

        public override string ToString()
        {
            return $"ID: {id}, Nombre: {nombre}, Apellidos: {apellidos}, Email: {email}, Suscripción: {suscripcion}, Estado: {estado}, Último Login: {lastLogin}";
        }




    }



}
