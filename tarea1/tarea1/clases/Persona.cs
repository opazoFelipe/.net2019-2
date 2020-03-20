using System;
using System.Collections.Generic;
using System.Text;

namespace tarea1.clases
{
    public class Persona
    {
        // Atributos
        public string Rut, Nombre;
        public int Edad;
        public List<int> Notas = new List<int>();

        // Metodo Promedio
        public float promedio()
        {
            if(Notas.Count == 0)
            {
                return -1;
            }

            float promedio = 0;
            foreach (int nota in this.Notas)
            {
                promedio = promedio + nota;        
            }

            promedio = (promedio / 3);
        
            return promedio;
        }
    }
}
