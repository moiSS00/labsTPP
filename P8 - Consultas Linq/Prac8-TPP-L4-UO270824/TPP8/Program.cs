using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace TPP8
{
    public class Program
    {
        private Model modelo = new Model();
        static void Main(string[] args)
        {
            Program consultas = new Program();
            Console.WriteLine("\nConsulta de Ejemplo 1:");
            consultas.Ejemplo1();
            Console.WriteLine("\nConsulta de Ejemplo 2:");
            consultas.Ejemplo2();
            Console.WriteLine("\nConsulta de Ejemplo 3:");
            consultas.Ejemplo3();

            Console.WriteLine("\nConsulta 1:");
            consultas.Consulta1();

            Console.WriteLine("\nConsulta 2:");
            consultas.Consulta2();

            Console.WriteLine("\nConsulta 3:");
            consultas.Consulta3();


            Console.WriteLine("\nConsulta 4:");
            consultas.Consulta4();


            Console.WriteLine("\nConsulta 5:");
            consultas.Consulta5();

            Console.WriteLine("\nConsulta 6:");
            consultas.Consulta6();

            Console.WriteLine("\nObligatoria 1:");
            consultas.Obligatoria1();

            Console.WriteLine("\nObligatoria 2:");
            consultas.Obligatoria2();

            Console.WriteLine("\nObligatoria 3:");
            consultas.Obligatoria3();

            Console.WriteLine("\nObligatoria 4:");
            consultas.Obligatoria4();

            Console.WriteLine("\nObligatoria 5:");
            consultas.Obligatoria5();

            //etc
            Console.ReadLine();
        }


        private void Ejemplo1()
        {
   
            //Llamadas de más de 15 segundos de duración

            // Usando consultas
            var c1 =
                from pc in modelo.PhoneCalls
                where pc.Seconds > 15
                select pc;
            Show(c1);

            Console.WriteLine();

            //Usando métodos
            var c1_m = modelo.PhoneCalls.Where(pc => pc.Seconds > 15);
            Show(c1_m);
           
        }

        private void Ejemplo2()
        {
            //Queremos mostrar las llamadas de cada empleado con el formato: "Nombre_empleado duracion_llamada"

            //El método Join, une dos colecciones a partir de un atributo común:
            var result = modelo.PhoneCalls.Join( //PhoneCalls es el primer IEnumerable
                modelo.Employees, //segundo IEnumerable sobre el que hacer el Join
                llamada => llamada.SourceNumber, //Atributo del primer IEnumerable (PhoneCalls)
                emp => emp.TelephoneNumber, //Atributo del 2º IEnumerable (Employees)
                (llamada, emp) => new // Función que recibe cada par de llamada-empleado y creamos un objeto anónimo.
                {
                    Nombre = emp.Name,
                    Duracion = llamada.Seconds
                }).Select(anonimo => anonimo.Nombre + " " + anonimo.Duracion);

                Show(result);
        }

        private void Ejemplo3()
        {
            //GroupBy: Vamos a mostrar la duración de las llamadas agrupadas por número de teléfono (origen)

            var llamadas = modelo.PhoneCalls;
            var resultado = llamadas.GroupBy(ll => ll.SourceNumber);
            //resultado ahora mismo es un ¡IGrouping! ¡Ooops!
            Console.WriteLine("Imprimiendo el IGrouping directamente:");
            Show(resultado);

            //Para imprimirlo, habría que hacer algo así
            Console.WriteLine("Imprimiendo el IGrouping recorriéndolo:");
            foreach (var actual in resultado)
            {
                //Cada IGrouping tiene una Key:
                Console.Write("\nClave [" + actual.Key+ "] : ");
                //Y tenemos un listado. En este caso, de llamadas:
                foreach(var llamada in actual)
                {
                    Console.Write(llamada.Seconds+" ");
                }
            }

            //Sin embargo GroupBy nos presenta otras opciones, vamos a combinar estas opciones
            //con los objetos anónimos:

            var opcion2 = llamadas.GroupBy(
                ll => ll.SourceNumber, //Agrupamos por número de origen
                (numero, llamadasEncontradas) => new //Vamos emplear una función que cree un objeto anónimo con la info que necesitamos
                {
                    Key = numero,
                    Duraciones = llamadasEncontradas.Select(ll => ll.Seconds)
                }
                );
            Console.WriteLine("\nImprimiendo el Objeto anónimo directamente:");
            Show(opcion2);
            Console.WriteLine("Imprimiendo el objeto anónimo con el Aggregate:");
            var conAggregate = opcion2.Select(a => a.Key + " : " + a.Duraciones.Aggregate("", (inicializador, duracion) => inicializador + " " + duracion));
            Show(conAggregate);
        }

        private void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
            var empleados = modelo.Employees;
            var resultado = empleados.Where(e => e.Name.StartsWith("F")).Select(e => e.Name);
            Show(resultado);

            //El resultado esperado es: Felipe

        }

        private void Consulta2()
        {
            //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria.


            /*El resultado esperado es:
              Alvaro 19/10/1945 0:00:00
              Dario 16/12/1973 0:00:00
            */

            var empleados = modelo.Employees;
            var resultado = empleados.Where(e => e.Province.Equals("Cantabria")).Select(e => e.Name +" "+e.DateOfBirth);
            Show(resultado);
            

        }

        // A partir de aquí, tomad como referencia: http://msdn.microsoft.com/en-us/library/9eekhta0.aspx

        private void Consulta3()
        {
            //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.

            var departamentos = modelo.Departments;
            var resultado = departamentos.Where(d => d.Employees.Count(e=> e.Age >= 18) > 1).Select(d => d.Name);
            Show(resultado);
            
            /*El resultado esperado es:
                Computer Science
                Medicine
            */
            

            //Posteriormente, modifica la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"


            //El resultado esperado es: Medicine
        }

        private void Consulta4()
        {
            // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado con más de 50 años
            //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.

            /*Resultado esperado:
                Alvaro 15
                Alvaro 10
                Eduardo 23
                Eduardo 22
                Felipe 7
            */

            var llamadas = modelo.PhoneCalls.Where(p => p.Seconds > 5);
            var empleados = modelo.Employees.Where(e=>e.Age > 50);
            var result = llamadas.Join( //PhoneCalls es el primer IEnumerable
                            empleados, //segundo IEnumerable sobre el que hacer el Join
                            llamada => llamada.SourceNumber, //Atributo del primer IEnumerable (PhoneCalls)
                            emp => emp.TelephoneNumber, //Atributo del 2º IEnumerable (Employees)
                            (llamada, emp) => new // Función que recibe cada par de llamada-empleado y creamos un objeto anónimo.
                {
                                Nombre = emp.Name,
                                Duracion = llamada.Seconds
                            }).Select(anonimo => anonimo.Nombre + " " + anonimo.Duracion);

            Show(result);
        }

        private void Consulta5()
        {
            //Mostrar la llamada más larga para cada profesor, mostrando por pantalla: Nombre_profesor : duracion_llamada_mas_larga
          
            var empleados = modelo.Employees;

            var resultado = empleados.Join(
                modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (empleado,llamada) => new
                {
                    Nombre = empleado.Name,
                    Tiempo = llamada.Seconds,
                }
                ).GroupBy(
                a => a.Nombre,
                (nombre,duraciones) => new
                {
                    Nombre = nombre,
                    Mayor = duraciones.Max(du => du.Tiempo)
                }
                ).Select(a => a.Nombre + " " + a.Mayor);

            Show(resultado);
        }


        private void Consulta6()
        {
            // Mostrar, agrupados por provincia, el nombre de los empleados
            //Tanto la provincia como los empleados deben estar ordenados alfabéticamente



            /*Resultado esperado:
                Cantabria : Alvaro Dario
                Asturias : Bernardo Felipe
                Alicante : Carlos
                Granada : Eduardo

            */

           

            var empleados = modelo.Employees;
            var resultado = empleados.GroupBy(e => e.Province,
                (provincia, empresas) => new
                {
                    Key = provincia,
                    Nombres = empresas.OrderBy(e => e.Name).Select(e => e.Name).Aggregate("", (cadena, n) => cadena + " , " + n)
                }).OrderBy(a => a.Key).Select(a => a.Key +" : "+a.Nombres);

            Show(resultado);




        }

        private void Obligatoria1()
        {
            
            var empleados = modelo.Employees.Where(e => e.Department.Name.Equals("Computer Science") && e.Office.Building.Equals("Faculty of Science"));
            //Ya tenemos los empleados que cumplen la condición del departamento y la de el despacho.

            var llamadas = modelo.PhoneCalls.Where(pc => pc.Seconds > 60);
            //Ya tenemos las llamadas superiories a un minuto.

            //Juntamos mdiante un join , ambas listas por número de telefono 
            var sol = empleados.Join(
                llamadas,
                e => e.TelephoneNumber,
                pc => pc.SourceNumber,
                (e, pc) => new
                {
                    Nombre = e.Name,
                    Edad = e.Age,
                }
                ).OrderBy(e => e.Edad).Select(e => "Nombre: "+e.Nombre);

            Show(sol);
        }


        private void Obligatoria2()
        {
            var empleados = modelo.Employees.Where(e => e.Department.Name.Equals("Computer Science"));
            //Empleados cuyo departamento es “Computer Science"

            //Agrupamos las llamadas por numero de telefono  
            var llamadas = modelo.PhoneCalls.GroupBy(
                pc => pc.SourceNumber,
                (numero, listaLlamadas) => new //Nos agrupa por numero dandonos una lista de llamadas 
                {
                    Numero = numero,
                    Duracion = listaLlamadas.Select(pc => pc.Seconds) //Sigue siendo una lista 
                }
                );

            //Juntamos a los empleados y a las llamadas con un join 
            var sol = empleados.Join(
                llamadas,
                e => e.TelephoneNumber,
                pc => pc.Numero,
                (e,pc) => new
                {
                    Nombre = e.Name,
                    Duracion = pc.Duracion.Aggregate(0,(inicializador,duracion) => inicializador + duracion)
                }
                ).Select(e => "Nombre: " + e.Nombre + " Duracion de llamadas: " + e.Duracion);

            Show(sol);

        }

        private void Obligatoria3()
        {

            //Agrupamos las llamadas por numero 
            var llamadas = modelo.PhoneCalls.GroupBy(
                           pc => pc.SourceNumber,
                           (numero, listaLlamadas) => new //Nos agrupa por numero dandonos una lista de llamdas 
                {
                               Numero = numero,
                               Duracion = listaLlamadas.Select(pc => pc.Seconds) //Sigue siendo una lista 
                }
                );

            //Juntamos las llamadas con los empleados 
            var departamentos = modelo.Employees.Join(
                llamadas,
                e => e.TelephoneNumber,
                pc => pc.Numero,
                (e, pc) => new
                {
                    Departamento = e.Department.Name,
                    Duracion = pc.Duracion.Aggregate(0, (inicializador, duracion) => inicializador + duracion) //Duracion total de todas las llamadas de un empleado 
                }
                );

            //Agrupamos los departamentos por nombre 
            var sol = departamentos.GroupBy( 
                d => d.Departamento,
                (nombre, duraciones) => new
                {
                    Nombre = nombre,
                    Duracion = duraciones.Aggregate(0, (inicializador, duracion) => inicializador + duracion.Duracion) //Duracion total de las llamadas de todos los empleados de un departamento
                }
                ).Select(d => "Nombre: " + d.Nombre + " Duración: " +d.Duracion);


            Show(sol);
          

        }

        private void Obligatoria4()
        {
            //Agrupamos los empleados por nombre de departamento 
            var sol = modelo.Employees.GroupBy(
                e => e.Department.Name,
                (nombre,empleados) => new
                {
                    NombreDepartamento = nombre,
                    EdadMinima = empleados.Select(e => e.Age).Min() //Cogemos la edad minima 
                }
                ).Select(d => "Departamento: " + d.NombreDepartamento + " Edad mínima: " + d.EdadMinima);

            Show(sol);
        }

        private void Obligatoria5()
        {
            //Juntamos los empleados con las llamadas 
            var empleados = modelo.Employees.Join(
                modelo.PhoneCalls,
                e => e.TelephoneNumber,
                pc => pc.SourceNumber,
                (e,pc) => new
                {
                    Departamento = e.Department.Name,
                    Duracion = pc.Seconds
                }
                );

            //Agrupamos por nombre de departamento 
            var departamentos = empleados.GroupBy(
                e => e.Departamento,
                (departamento, empleadosDuracion) => new
                {
                    NombreDepartamento = departamento,
                    DuracionLlamadas = empleadosDuracion.Aggregate(0,(inicializador,duracion) => inicializador + duracion.Duracion)
                }
                );


            var duracionMax = departamentos.Max(d => d.DuracionLlamadas); //Cogemos la maxima duracion 
            var nombreMax = departamentos.FirstOrDefault(d => d.DuracionLlamadas == duracionMax); //Cogemos el primero que cumple con la maxima duracion
            Console.WriteLine("Departamento: " + nombreMax.NombreDepartamento + " Duración llamadas: " + duracionMax); //Imprimimos solucion 
        }


        //Metodo para mostrar el resultado 
        private static void Show<T>(IEnumerable<T> colección)
        {
            foreach (var item in colección)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Elementos en la colección: {0}.", colección.Count());
        }
    }
}
