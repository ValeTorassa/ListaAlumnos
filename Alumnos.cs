using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaAlumnos
{
    class Alumnos
    {
        // Propiedades públicas que definen los atributos de los alumnos
        public int Legajo { get => _legajo; set => _legajo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public DateTime FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public DateTime FechaIngreso { get => _fechaNacimiento; set => _fechaIngreso = value; }
        public int Edad { get => _edad; }
        public bool Activo { get => _activo; set => _activo = value; }
        public int MateriasAprobadas { get => _materiasAprobadas; set => _materiasAprobadas = value; }

        // Constructor de la clase Alumnos
        public Alumnos(int legajo, string nombre, string apellido, DateTime fechaNacimiento, DateTime fechaIngreso, bool activo, int cantMaterias)
        {
            Legajo = legajo;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            FechaIngreso = fechaIngreso;
            Activo = activo;
            MateriasAprobadas = cantMaterias;
        }

        // Destructor de la clase Alumnos
        ~Alumnos()
        {
            MessageBox.Show($"El alumno {Nombre} {Apellido} de legajo: {Legajo} ha sido eliminado");
        }

        // Método para obtener la antigüedad del alumno
        public string Antiguedad()
        {
            TimeSpan diferencia = DateTime.Now.Subtract(_fechaIngreso);
            int años = (int)(diferencia.TotalDays / 365); // Aproximación de años completos
            int meses = (int)((diferencia.TotalDays % 365) / 30.5); // Aproximación de meses completos
            int dias = (int)(diferencia.TotalDays % 30.5); // Días restantes

            return $"{años} / {meses} / {dias}";
        }

        // Método para obtener la cantidad de materias no aprobadas por el alumno
        public int MateriasNoAprobadas()
        {
            int noAprobadas;

            noAprobadas = 36 - _materiasAprobadas;

            return noAprobadas;
        }

        // Método para obtener la edad del alumno al momento del ingreso
        public int EdadDeIngreso()
        {
            int edadIngreso;
            TimeSpan diferencia;

            diferencia = _fechaIngreso.Subtract(_fechaNacimiento);
            edadIngreso = Convert.ToInt32(Math.Floor(diferencia.TotalDays / 360));
            return edadIngreso;
        }

        // Método para obtener la edad actual del alumno
        public int EdadActual()
        {
            int edadIngreso;
            TimeSpan diferencia;

            diferencia = DateTime.Now.Subtract(_fechaNacimiento);
            edadIngreso = Convert.ToInt32(Math.Floor(diferencia.TotalDays / 360));
            _edad = edadIngreso;
            return edadIngreso;
        }

        // Atributos privadas de la clase Alumnos
        private int _legajo;
        private string _nombre;
        private string _apellido;
        private DateTime _fechaNacimiento;
        private DateTime _fechaIngreso;
        private int _edad;
        private bool _activo;
        private int _materiasAprobadas;
    }
}
