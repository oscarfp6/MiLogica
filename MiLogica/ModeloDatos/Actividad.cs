using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiLogica.ModeloDatos
{
    public class Actividad
    {
        // Renombramos a "Id" para seguir la convención de EF Core para Primary Key
        public int Id { get; set; }

        // --- Clave Foránea ---
        public int IdUsuario { get; private set; }

        public double Kms { get; private set; }
        public int MetrosDesnivel { get; private set; }
        public TimeSpan Duracion { get; private set; }
        public DateTime Fecha { get; private set; }
        public TipoActividad Tipo { get; private set; }
        public String Descripcion { get; set; }
        public int? FCMedia { get; private set; }

        // --- Propiedad de Navegación ---
        // Esto le dice a EF Core que cada Actividad pertenece a un Usuario.
        public virtual Usuario Usuario { get; set; }

        /// <summary>
        /// Propiedad calculada. Devuelve el ritmo en minutos por kilómetro.
        /// Útil para actividades como Running o Caminata.
        /// </summary>
        public double RitmoMinPorKm
        {
            get
            {
                if (Kms > 0)
                {
                    return Duracion.TotalMinutes / Kms;
                }
                return 0;
            }
        }

        public double VelocidadMediaKmh
        {
            get
            {
                if (Duracion.TotalHours > 0)
                {
                    return Kms / Duracion.TotalHours;
                }
                return 0;
            }
        }

        public void ActualizarMetricas(double kms, int metrosDesnivel, TipoActividad tipo, TimeSpan duracion, string descripcion = "", int? fcMedia = null)
        {
            if (kms < 0) throw new ArgumentException("Los kilómetros no pueden ser negativos.");
            if (duracion.TotalSeconds <= 0) throw new ArgumentException("La duración debe ser positiva.");
            Kms = kms;
            MetrosDesnivel = metrosDesnivel;
            Duracion = duracion;
            Descripcion = descripcion;
            FCMedia = fcMedia;
            Tipo = tipo;
        }

        private Actividad() { }


        // Constructor para ser usado por la lógica de negocio
        public Actividad(int idUsuario, double kms, int metrosDesnivel, TimeSpan duracion, DateTime fecha, TipoActividad tipo, string descripcion = "", int? fcMedia=null)
        {
            // Validación de datos en el constructor para asegurar la integridad del objeto
            if (idUsuario <= 0) throw new ArgumentException("El ID de usuario debe ser válido.");
            if (kms < 0) throw new ArgumentException("Los kilómetros no pueden ser negativos.");
            if (duracion.TotalSeconds <= 0) throw new ArgumentException("La duración debe ser positiva.");

            IdUsuario = idUsuario;
            Kms = kms;
            MetrosDesnivel = metrosDesnivel;
            Duracion = duracion;
            Fecha = fecha;
            Tipo = tipo;
            Descripcion = descripcion;
            FCMedia = fcMedia;
        }

        public Actividad(int idUsuario, double kms, int metrosDesnivel, TimeSpan duracion, DateTime fecha, TipoActividad tipo, string descripcion = "")
        {
            // Validación de datos en el constructor para asegurar la integridad del objeto
            if (idUsuario <= 0) throw new ArgumentException("El ID de usuario debe ser válido.");
            if (kms < 0) throw new ArgumentException("Los kilómetros no pueden ser negativos.");
            if (duracion.TotalSeconds <= 0) throw new ArgumentException("La duración debe ser positiva.");

            IdUsuario = idUsuario;
            Kms = kms;
            MetrosDesnivel = metrosDesnivel;
            Duracion = duracion;
            Fecha = fecha;
            Tipo = tipo;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            if (FCMedia.HasValue)
            {
                return $"{Tipo}: {Kms:F2} km en {Duracion:hh\\:mm\\:ss} el {Fecha:dd/MM/yyyy} con FC media de {FCMedia} bpm";
            }
            else
            {
                return $"{Tipo}: {Kms:F2} km en {Duracion:hh\\:mm\\:ss} el {Fecha:dd/MM/yyyy}";
            }
        }




    }
}