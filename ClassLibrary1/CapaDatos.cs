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
        List<Usuario> tblUsuarios = new List<Usuario>();
        bool ICapaDatos.GuardaActividad(Actividad e)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
