# Calculadora Genérica con Delegados y Control de Excepciones

## Propósito del proyecto
Esta aplicación de consola en C# permite gestionar una lista genérica de números y realizar operaciones matemáticas sobre ella. El objetivo es demostrar el uso práctico de tipos genéricos, delegados y control de excepciones. La calculadora soporta distintos tipos de datos numéricos (`int`, `double`, `float`, `decimal`) y ofrece cuatro operaciones: suma, resta, multiplicación y división.

## Instrucciones para ejecutar el programa
1. Clonar el repositorio o copiar el código fuente.
2. Abrir la solución en Visual Studio 2022 o superior.
3. Compilar y ejecutar el proyecto (también puedes usar `dotnet run` desde la terminal).
4. Seguir el menú interactivo en consola para ingresar números y elegir operaciones.

## Detalles sobre el manejo de excepciones
- **InvalidOperationException:** Se lanza cuando se intenta operar con menos de dos números en la lista.
- **DivideByZeroException:** Se captura cuando un número cero en la lista provoca una división por cero.
- **FormatException:** Se maneja cuando el usuario ingresa texto en lugar de un número.
- **OverflowException:** Se captura si un número ingresado excede el rango del tipo de dato.

## Explicación de los métodos utilizados
- **`Calculadora<T>`** – Clase genérica que contiene la lista de números y las operaciones básicas:
  - `AgregarNumero(T numero)`: Agrega un número a la lista interna.
  - `MostrarNumeros()`: Imprime todos los números almacenados en la lista.
  - `AplicarOperacion(OperacionMatematica<T> operacion)`: Aplica una operación binaria a toda la lista de forma secuencial. Lanza `InvalidOperationException` si hay menos de dos elementos.
  - `LimpiarLista()`: Vacía la lista por completo.
- **`Sumar<T>`, `Restar<T>`, `Multiplicar<T>`, `Dividir<T>`** – Métodos estáticos genéricos que realizan la operación aritmética correspondiente usando `dynamic` para mantener la genericidad. `Dividir` lanza `DivideByZeroException` si el divisor es cero.
- **`AgregarNumero` (método de interfaz de usuario)** – Lee la entrada del usuario, convierte a `double` y controla `FormatException` y `OverflowException`.
- **`RealizarOperacion`** – Muestra el submenú de operaciones, captura la elección del usuario, asigna el método adecuado al delegado y ejecuta la operación, manejando las excepciones que puedan surgir.

## Cómo el código utiliza genéricos y delegados
- **Genéricos:** La clase `Calculadora<T>` y el delegado `OperacionMatematica<T>` están parametrizados con un tipo `T` restringido a `struct, IComparable, IConvertible` para garantizar que solo se usen tipos numéricos. Esto permite que la misma clase funcione con `int`, `double`, `float`, etc., sin modificar el código interno. La lista interna `List<T>` almacena los números del tipo elegido, y los métodos de operación (`Sumar<T>`, etc.) utilizan `dynamic` para realizar las operaciones aritméticas sobre cualquier tipo numérico en tiempo de ejecución.
- **Delegados:** El delegado `OperacionMatematica<T>` define la firma de cualquier operación binaria (recibe dos parámetros `T` y devuelve `T`). En el método `RealizarOperacion`, se asigna al delegado la función correspondiente (`Sumar`, `Restar`, etc.) según la opción del usuario. Luego, ese delegado se pasa como argumento a `AplicarOperacion`, que lo invoca iterativamente sobre todos los elementos de la lista. De esta forma, la lógica de la operación se desacopla del código que la aplica, cumpliendo el propósito de los delegados.
