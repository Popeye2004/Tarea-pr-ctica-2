namespace CalculadoraGenerica
{
    public delegate T OperacionMatematica<T>(T a, T b);

    public class Calculadora<T> where T : struct, IComparable, IConvertible
    {
        private List<T> _numeros;

        public Calculadora()
        {
            _numeros = new List<T>();
        }

        public void AgregarNumero(T numero)
        {
            _numeros.Add(numero);
            Console.WriteLine($"Número {numero} agregado correctamente.");
        }

        public int CantidadElementos()
        {
            return _numeros.Count;
        }

        public void MostrarNumeros()
        {
            if (_numeros.Count == 0)
            {
                Console.WriteLine("La lista esta vacia.");
                return;
            }

            Console.WriteLine("\nNumeros en la lista:");
            for (int i = 0; i < _numeros.Count; i++)
            {
                Console.WriteLine($"  [{i}] {_numeros[i]}");
            }
        }

        public T AplicarOperacion(OperacionMatematica<T> operacion)
        {
            if (_numeros.Count < 2)
                throw new InvalidOperationException("Se necesitan al menos dos numeros para realizar una operacion.");

            T resultado = _numeros[0];
            for (int i = 1; i < _numeros.Count; i++)
            {
                resultado = operacion(resultado, _numeros[i]);
            }
            return resultado;
        }

        public void LimpiarLista()
        {
            _numeros.Clear();
            Console.WriteLine("Lista limpiada.");
        }
    }

    class Program
    {
        static T Sumar<T>(T a, T b) where T : struct, IComparable, IConvertible
        {
            dynamic x = a;
            dynamic y = b;
            return x + y;
        }

        static T Restar<T>(T a, T b) where T : struct, IComparable, IConvertible
        {
            dynamic x = a;
            dynamic y = b;
            return x - y;
        }

        static T Multiplicar<T>(T a, T b) where T : struct, IComparable, IConvertible
        {
            dynamic x = a;
            dynamic y = b;
            return x * y;
        }

        static T Dividir<T>(T a, T b) where T : struct, IComparable, IConvertible
        {
            dynamic x = a;
            dynamic y = b;
            if (y == 0)
                throw new DivideByZeroException("No se puede dividir entre cero.");
            return x / y;
        }

        static void Main(string[] args)
        {
            Console.Title = "Calculadora Genérica con Delegados";
            Console.WriteLine("=== CALCULADORA GENÉRICA ===");
            Console.WriteLine("Esta aplicación trabaja con numeros de tipo double.\n");

            Calculadora<double> calculadora = new Calculadora<double>();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Agregar numero");
                Console.WriteLine("2. Mostrar lista de numeros");
                Console.WriteLine("3. Realizar operacion");
                Console.WriteLine("4. Limpiar lista");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarNumero(calculadora);
                        break;
                    case "2":
                        calculadora.MostrarNumeros();
                        break;
                    case "3":
                        RealizarOperacion(calculadora);
                        break;
                    case "4":
                        calculadora.LimpiarLista();
                        break;
                    case "5":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void AgregarNumero(Calculadora<double> calculadora)
        {
            Console.Write("\nIngrese un numero: ");
            string entrada = Console.ReadLine();

            try
            {
                double numero = double.Parse(entrada);
                calculadora.AgregarNumero(numero);
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: El valor ingresado no es un numero valido. Intente de nuevo.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("ERROR: El numero ingresado es demasiado grande o demasiado pequeño.");
            }
        }

        static void RealizarOperacion(Calculadora<double> calculadora)
        {
            if (calculadora.CantidadElementos() < 2)
            {
                Console.WriteLine("\nERROR: Se necesitan al menos dos numeros para realizar una operacion.");
                return;
            }

            Console.WriteLine("\n--- SELECCIONE OPERACION ---");
            Console.WriteLine("1. Suma");
            Console.WriteLine("2. Resta");
            Console.WriteLine("3. Multiplicacion");
            Console.WriteLine("4. Division");
            Console.Write("Seleccione una operacion: ");

            string opcion = Console.ReadLine();
            OperacionMatematica<double> operacion = null;
            string nombreOperacion = "";

            switch (opcion)
            {
                case "1":
                    operacion = Sumar;
                    nombreOperacion = "Suma";
                    break;
                case "2":
                    operacion = Restar;
                    nombreOperacion = "Resta";
                    break;
                case "3":
                    operacion = Multiplicar;
                    nombreOperacion = "Multiplicacion";
                    break;
                case "4":
                    operacion = Dividir;
                    nombreOperacion = "Division";
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    return;
            }

            try
            {
                double resultado = calculadora.AplicarOperacion(operacion);
                Console.WriteLine($"\nResultado de la {nombreOperacion}: {resultado}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"\nERROR: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR INESPERADO: {ex.Message}");
            }
        }
    }
}