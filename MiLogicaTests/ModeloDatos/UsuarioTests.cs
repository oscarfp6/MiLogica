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
        public void ComprobarPassWordTest()
        {
            Usuario Jose = new Usuario(1, "usuario1", "password123", "Pérez","jose@gmail.com",false );
            Assert.IsTrue(Jose.ComprobarPassWord("password123"));
            Console.WriteLine(Jose.Estado);
            Jose.ComprobarPassWord("wrongpass");
            Jose.ComprobarPassWord("wrongpass");
            Jose.ComprobarPassWord("wrongpass");
            Console.WriteLine(Jose.Estado);
        }
    }
}