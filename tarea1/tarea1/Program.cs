using System;
using System.Collections.Generic;

namespace tarea1
{
    class Program
    {
        public static void mostrarDatosIngreso(string rut, string nombre, int edad, int nota1, int nota2, int nota3)
        {
            // Codigo innecesario, solo hecho para hacer que la interfaz en la consola se vea bonita.
            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("\t\t\tINGRESAR NUEVOS DATOS\n");

            Console.Write("\t\tIngrese Rut: " + rut + "\n");
            Console.Write("\t\tIngrese Nombre: " + nombre + "\n");

            if(edad != -1)
            Console.Write("\t\tIngrese Edad: " + edad + "\n");

            if(nota1 != -1)
                Console.Write("\t\t\tIngrese Nota " + (1) + ": " + nota1 + "\n");

            if (nota2 != -1)
                Console.Write("\t\t\tIngrese Nota " + (2) + ": " + nota2 + "\n");

            if (nota3 != -1)
            {
                Console.Write("\t\t\tIngrese Nota " + (3) + ": " + nota3 + "\n");
            }
        }

        static void Main(string[] args)
        {
            List<clases.Persona> personas = new List<clases.Persona>();
            int contadorPersonas = 0;
            do
            {
                bool accionValida = false;
                int accionElejida = 0;

                //Validar que se elija una de las acciones del menu
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("\t\t\t********** MENÚ DE ACCIONES ***********\n");
                    Console.WriteLine("\t\t1. Ingresar Datos");
                    Console.WriteLine("\t\t2. Listar Datos");
                    Console.WriteLine("\t\t3. Buscar Persona por Rut\n");

                    Console.Write("\t\tIngrese el número de la acción: ");
                    string accion = Console.ReadLine();

                    if (accion.Length == 0 | accion.Length > 1)
                    {
                        Console.WriteLine("\t\tIngrese una opción valida\n");
                        Console.Write("\t");
                    }
                    else
                    {
                        bool suceso = int.TryParse(accion, out accionElejida);
                        if (suceso && accionElejida > 0 && accionElejida < 4)
                        {
                            accionValida = true;
                        }
                        else
                        {
                            Console.WriteLine("\t\tIngrese una opción valida\r");
                            Console.Write("\t");
                        }
                    }
                } while (!accionValida);

                clases.Persona nuevaPersona = new clases.Persona();

                // Ejecutar Acciones
                if (accionElejida == 1)
                {
                    Console.Clear();
                    Console.WriteLine("\n");
                    Console.WriteLine("\t\t\tINGRESAR NUEVOS DATOS\n");

                    Console.Write("\t\tIngrese Rut: ");
                    nuevaPersona.Rut = Console.ReadLine();

                    Console.Write("\t\tIngrese Nombre: ");
                    nuevaPersona.Nombre = Console.ReadLine();

                    string edad = "";
 
                    int edadValida = -1,
                        nota1 = -1,
                        nota2 = -1,
                        nota3 = -1;

                    bool sucesoEdad = false;

                    do
                    {
                        mostrarDatosIngreso(nuevaPersona.Rut, nuevaPersona.Nombre, edadValida, nota1, nota2, nota3);
                        Console.Write("\t\tIngrese Edad: ");
                        edad = Console.ReadLine();

                        if (edad.Length > 0)
                        {
                            sucesoEdad = int.TryParse(edad, out edadValida);
                            if (sucesoEdad)
                            {
                                nuevaPersona.Edad = edadValida;
                                sucesoEdad = true;
                            }
                            else
                            {
                                edadValida = -1;
                            }
                        }
                    } while (!sucesoEdad);

  
                    for (int i = 0; i < 3; i++)
                    {
                        string nota = "";
                        int notaValida = 0;
                        bool suceso = false;
                        do
                        {
                            mostrarDatosIngreso(nuevaPersona.Rut, nuevaPersona.Nombre, nuevaPersona.Edad, nota1, nota2, nota3);
                            Console.Write("\t\t\tIngrese Nota " + (i + 1) + ": ");
                            nota = Console.ReadLine();

                            if (nota.Length > 0)
                            {
                                suceso = int.TryParse(nota, out notaValida);
                                if (suceso)
                                {
                                    nuevaPersona.Notas.Add(notaValida);
                                    suceso = true;
                                    if (i == 0) nota1 = notaValida;
                                    if (i == 1) nota2 = notaValida;
                                    if (i == 2) nota3 = notaValida;
                                }
                            }
                        } while (!suceso);
                    }

                    mostrarDatosIngreso(nuevaPersona.Rut, nuevaPersona.Nombre, edadValida, nota1, nota2, nota3);
                    Console.WriteLine("\n");
                    Console.Write("\t\tDATOS GUARDADOS CORRECTAMENTE!");

                    personas.Add(nuevaPersona);

                    contadorPersonas = personas.Count;
                    Console.WriteLine("\n");
                    Console.Write("\tIngrese la tecla m para volver al menu: ");
                    string tecla1 = "";
                    do
                    {
                        tecla1 = Console.ReadLine();
                    } while (tecla1 != "m");
                    Console.Clear();
                }
                
                if(accionElejida == 2)
                {
                    Console.Clear();
                    string tecla2 = " ";
                    if (personas.Count == 0)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\tNo se han ingresado personas aun, ingrese una\n");
                            Console.Write("\tIngrese la tecla m para volver al menu: ");
                            tecla2 = Console.ReadLine();

                        } while (tecla2 != "m");
                        
                    }
                    else
                    {
                        Console.WriteLine("\n");

                        Console.WriteLine("\t\t\tLISTADO DE PERSONAS ENCONTRADAS\n");
                        for (int j = 0; j < contadorPersonas; j++)
                        {
                            Console.WriteLine("\t\tRut: " + personas[j].Rut);
                            Console.WriteLine("\t\tNombre: " + personas[j].Nombre);
                            Console.WriteLine("\t\tEdad: " + personas[j].Edad);
                            Console.WriteLine("\t\tNotas: " + personas[j].Notas[0] + ", " + personas[j].Notas[1] + ", " + +personas[j].Notas[2]);
                            Console.WriteLine("\t\t--------------------------------------------\n");
                        }
                        
                        do
                        {
                            Console.Write("\tIngrese la tecla m para volver al menu: ");
                            tecla2 = Console.ReadLine();
                        } while (tecla2 != "m");

                    }
                }

                if (accionElejida == 3)
                {
                    Console.Clear();
                    bool rutEncontrado = false;
                    int indiceRut = 0;
                    string tecla3 = "";
                    if(personas.Count == 0)
                    {
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\tNo se han ingresado personas aun, ingrese una\n");
                            Console.Write("\tIngrese la tecla m para volver al menu: ");
                            tecla3 = Console.ReadLine();

                        } while (tecla3 != "m");
                    }
                    else
                    {
                        bool temp = false;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\tBUSCAR PERSONA POR RUT");
                            Console.WriteLine("\n");
                            if(temp) Console.WriteLine("\t\tEl rut ingresado no esta registrado, intente con otro\n");
                            Console.Write("\t\tIngrese Rut:");
                            string rut = Console.ReadLine();
                            for (int k = 0; k < contadorPersonas; k++)
                            {
                                if (personas[k].Rut == rut)
                                {
                                    rutEncontrado = true;
                                    indiceRut = k;
                                }
                            }
                            temp = true;

                        } while (!rutEncontrado);
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\n");
                            Console.WriteLine("\t\t\tPERSONA ENCONTRADA");
                            Console.WriteLine("\n");
                            float promedio = personas[indiceRut].promedio();
                            Console.WriteLine("\t\tRut: " + personas[indiceRut].Rut);
                            Console.WriteLine("\t\tNombre: " + personas[indiceRut].Nombre);
                            Console.WriteLine("\t\tPromedio: " + promedio);
                            Console.WriteLine("\n");

                            Console.Write("\tIngrese la tecla m para volver al menu: ");
                            tecla3 = Console.ReadLine();

                        } while (tecla3 != "m");
                    }  
                }
            } while(true);
        }
    }
}


                            
