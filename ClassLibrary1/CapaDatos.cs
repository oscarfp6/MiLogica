using Datos.Database;
using MiLogica.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class CapaDatos : ICapaDatos
    {
        private static List<Usuario> tblUsuarios;
        private static List<Actividad> tblActividades;

        private static int _nextUserId = 1;
        private static int _nextActividadId = 1;


        public void Inicializa()
        {
            tblUsuarios = new List<Usuario>();
            tblActividades = new List<Actividad>();
        }



        bool ICapaDatos.GuardaActividad(Actividad e)
        {
            tblActividades.Add(e);
            return true;
        }

        bool ICapaDatos.GuardaUsuario(Usuario u)
        {
            if(!tblUsuarios.Any(user => user.Email == u.Email))
            {
                tblUsuarios.Add(u);
                return true;
            }
            return false;
        }

        Actividad ICapaDatos.LeeActividad(int idElemento)
        {
            return
        }

        Usuario ICapaDatos.LeeUsuario(string email)
        {
            throw new NotImplementedException();
        }

        int ICapaDatos.NumActividades(int idUsuario)
        {
            throw new NotImplementedException();
        }

        int ICapaDatos.NumUsuarios()
        {
            throw new NotImplementedException();
        }

        int ICapaDatos.NumUsuariosActivos()
        {
            throw new NotImplementedException();
        }

        bool ICapaDatos.ValidaUsuario(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
