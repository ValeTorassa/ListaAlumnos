using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaAlumnos
{
    static class MetodosAuxiliares
    {
        // Este método comprueba si una cadena de texto se puede convertir a un número entero positivo y lo devuelve en una variable de salida.
        public static bool ComprobarValor(string num, out int valor)
        {
            valor = 0; // Inicializamos la variable de salida en cero.
            int numero;

            try
            {
                numero = int.Parse(num); // Intentamos convertir la cadena a un número entero.

                if (numero >= 0) // Si el número es mayor o igual que cero, lo guardamos en la variable de salida y devolvemos true.
                {
                    valor = numero;
                    return true;
                }
                else // Si el número es negativo, devolvemos false.
                {
                    return false;
                }
            }
            catch (Exception) // Si no se puede convertir la cadena a un número entero, devolvemos false.
            {
                return false;
            }
        }

        // Este método comprueba si una cadena de texto representa un valor booleano y lo devuelve en una variable de salida.
        public static bool ComprobarActivo(string SN, out bool SNbool)
        {
            SNbool = false; // Inicializamos la variable de salida en false.

            if (SN == "si" || SN == "SI" || SN == "Si") // Si la cadena es "si", "SI" o "Si", asignamos true a la variable de salida y devolvemos true.
            {
                SNbool = true;
                return true;
            }
            else if (SN == "No" || SN == "no" || SN == "NO") // Si la cadena es "No", "no" o "NO", asignamos false a la variable de salida y devolvemos true.
            {
                SNbool = false;
                return true;
            }
            else // Si la cadena no es ninguna de las anteriores, devolvemos false.
            {
                return false;
            }
        }

    }
}
