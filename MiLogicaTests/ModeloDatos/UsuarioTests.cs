using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiLogica.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiLogica.ModeloDatos.Tests
{
    [TestClass()]
    public class UsuarioTests
    {
        [TestMethod()]
        public void PermitirLogin()
        {
            Usuario Jose = new Usuario(1, "usuario1", "password123", "Pérez", "jose@gmail.com", false);
            Assert.IsTrue(Jose.PermitirLogin("password123"));
            Console.WriteLine(Jose.Estado);
            Jose.PermitirLogin("wrongpass");
            Jose.PermitirLogin("wrongpass");
            Jose.PermitirLogin("wrongpass");
            Console.WriteLine(Jose.Estado);
            Console.WriteLine("Esperando 5 segundos para desbloquear..., introducimos contraseña correcta");
            Assert.IsFalse(Jose.PermitirLogin("password123"));
        }

        [TestMethod()]

        public void CambiarPasswordUsuarioActivoTest()
        {
            Usuario Maria = new Usuario(2, "Maria", "miPass", "García", "maria@gmail.com", true);
            Console.WriteLine(Maria.Estado);
            Assert.IsFalse(Maria.CambiarPassword("wrongPass", "newPass"));
            Assert.IsTrue(Maria.CambiarPassword("miPass", "@contraseñaSegura123"));


            Assert.IsTrue(Maria.ComprobarPassWord("@contraseñaSegura123"));
            Console.WriteLine("Probamos a cambiar la contraseña a una inválida/insegura");
            if(Maria.CambiarPassword("newPass", "short"))
                Console.WriteLine("La contraseña se ha cambiado a una insegura, hay un error.");
            else
                Console.WriteLine("La contraseña no cumple los requisitos de seguridad, no se ha cambiado.");
            Assert.IsFalse(Maria.ComprobarPassWord("short"));



        }

        [TestMethod()]
        public void CambiarPasswordSimpleTest()
        {
            Usuario oscar = new Usuario(3, "oscar", "pass1234", "Lopez", "oscar@gmail.com", false);
            Assert.IsTrue(oscar.CambiarPassword("pass1234", "@contraseñaSegura123"));
        }


            [TestMethod()]
        public void CambiarPasswordUsuarioBloqueadooTest()
        {
            Usuario Ana = new Usuario(2, "Ana", "miPass", "García", "ana@gmail.com",false);
            Ana.PermitirLogin("wrongpass");
            Ana.PermitirLogin("wrongpass");
            Ana.PermitirLogin("wrongpass");
            Console.WriteLine(Ana.Estado);
            Assert.IsFalse(Ana.CambiarPassword("miPass", "newPass"));


        }

        [TestMethod()]
        public void ComprobarPasswordTest()
        {
            Usuario juan = new Usuario(2, "juan", "passwd", "fernandez", "juan@gmail.com", false);
            Assert.IsTrue(juan.ComprobarPassWord("passwd"));
            Assert.IsFalse(juan.ComprobarPassWord("fake"));
        }
    }
}