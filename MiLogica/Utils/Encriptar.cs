using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiLogica.Utils
{
    public static class Encriptar
    {
        public static string EncriptarPassword(string password)
        {
            // Implementación simple de encriptación (no segura, solo para ejemplo)
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }
        public static string DesencriptarPassword(string encryptedPassword)
        {
            var bytes = Convert.FromBase64String(encryptedPassword);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
