class Program
{
    // Catálogo de revistas en un arreglo
    static string[] catalogo = new string[10] {
        "National Geographic",
        "Time",
        "Scientific American",
        "Forbes",
        "Nature",
        "Reader's Digest",
        "The Economist",
        "Sports Illustrated",
        "Vogue",
        "Wired"
    };

    static void Main()
    {
        int opcion = 0;

        do
        {
            System.Console.WriteLine("\n===== MENÚ CATÁLOGO DE REVISTAS =====");
            System.Console.WriteLine("1. Buscar revista (recursivo)");
            System.Console.WriteLine("2. Salir");
            System.Console.Write("Seleccione una opción: ");

            opcion = int.Parse(System.Console.ReadLine());

            if (opcion == 1)
            {
                System.Console.Write("\nIngrese el título de la revista a buscar: ");
                string titulo = System.Console.ReadLine();

                bool encontrado = BuscarRecursivo(titulo, 0);

                if (encontrado)
                {
                    System.Console.WriteLine("Encontrado");
                }
                else
                {
                    System.Console.WriteLine("No encontrado");
                }
            }
            else if (opcion == 2)
            {
                System.Console.WriteLine("Saliendo del programa...");
            }
            else
            {
                System.Console.WriteLine("Opción no válida, intente de nuevo.");
            }

        } while (opcion != 2);
    }

    // Método recursivo para buscar en el arreglo
    static bool BuscarRecursivo(string titulo, int indice)
    {
        if (indice >= catalogo.Length)
        {
            return false; // No se encontró
        }

        if (catalogo[indice].ToLower() == titulo.ToLower())
        {
            return true; // Se encontró
        }

        // Llamada recursiva al siguiente índice
        return BuscarRecursivo(titulo, indice + 1);
    }
}
