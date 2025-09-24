using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiLogica.ModeloDatos
{
    public class Actividad
    {
        private int idUsuario;
        private int idÁctividad;
        private double kms;
        private int metrosDesnivel;
        private TimeSpan duracion;
        private DateTime fecha;
        private TipoActividad tipo;
        private String descripcion;
        private int fcMedia;


        //Getters y setters
        public int IdUsuario { get; private set; }
        public int IdÁctividad { get; private set; }
        public double Kms { get; private set; }
        public int MetrosDesnivel { get; private set; }
        public TimeSpan Duracion { get; private set; }
        public DateTime Fecha { get; private set; }
        public TipoActividad Tipo { get; private set; }
        public String Descripcion { get; set; }
        public int FCMedia { get; private set; }




    }
}
