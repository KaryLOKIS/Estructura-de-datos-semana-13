class Grafo
{
    // Representación de un grafo con lista de adyacencia
    private System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<(string destino, int costo)>> adyacencia;

    public Grafo()
    {
        adyacencia = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<(string destino, int costo)>>();
    }

    public void AgregarVuelo(string origen, string destino, int costo)
    {
        if (!adyacencia.ContainsKey(origen))
            adyacencia[origen] = new System.Collections.Generic.List<(string, int)>();

        adyacencia[origen].Add((destino, costo));

        // Si el destino no existe en el grafo, lo agregamos vacío
        if (!adyacencia.ContainsKey(destino))
            adyacencia[destino] = new System.Collections.Generic.List<(string, int)>();
    }

    // Reportería: mostrar aeropuertos y sus vuelos
    public void MostrarVuelos()
    {
        System.Console.WriteLine(" Lista de vuelos disponibles:");
        foreach (var nodo in adyacencia)
        {
            System.Console.WriteLine($"Desde {nodo.Key}:");
            foreach (var vuelo in nodo.Value)
                System.Console.WriteLine($"   ➝ {vuelo.destino} (${vuelo.costo})");
        }
    }

    // Algoritmo de Dijkstra para encontrar el vuelo más barato
    public void VueloMasBarato(string origen, string destino)
    {
        var distancias = new System.Collections.Generic.Dictionary<string, int>();
        var visitados = new System.Collections.Generic.HashSet<string>();
        var previo = new System.Collections.Generic.Dictionary<string, string>();
        var cola = new System.Collections.Generic.PriorityQueue<string, int>();

        foreach (var nodo in adyacencia.Keys)
            distancias[nodo] = int.MaxValue;

        distancias[origen] = 0;
        cola.Enqueue(origen, 0);

        while (cola.Count > 0)
        {
            var actual = cola.Dequeue();
            if (visitados.Contains(actual)) continue;
            visitados.Add(actual);

            foreach (var vuelo in adyacencia[actual])
            {
                int nuevaDist = distancias[actual] + vuelo.costo;
                if (nuevaDist < distancias[vuelo.destino])
                {
                    distancias[vuelo.destino] = nuevaDist;
                    previo[vuelo.destino] = actual;
                    cola.Enqueue(vuelo.destino, nuevaDist);
                }
            }
        }

        if (distancias[destino] == int.MaxValue)
        {
            System.Console.WriteLine($"No existe vuelo de {origen} a {destino}");
            return;
        }

        // Reconstruir la ruta
        var ruta = new System.Collections.Generic.Stack<string>();
        string actualRuta = destino;
        while (actualRuta != origen)
        {
            ruta.Push(actualRuta);
            actualRuta = previo[actualRuta];
        }
        ruta.Push(origen);

        System.Console.WriteLine($"\n✈️ Vuelo más barato de {origen} a {destino}: ${distancias[destino]}");
        System.Console.Write("Ruta: ");
        System.Console.WriteLine(string.Join(" ➝ ", ruta));
    }
}

class Program
{
    static void Main()
    {
        Grafo vuelos = new Grafo();

        // Base de datos ficticia (simulada dentro del código)
        vuelos.AgregarVuelo("Quito", "Guayaquil", 100);
        vuelos.AgregarVuelo("Quito", "Cuenca", 80);
        vuelos.AgregarVuelo("Cuenca", "Guayaquil", 40);
        vuelos.AgregarVuelo("Guayaquil", "Manta", 50);
        vuelos.AgregarVuelo("Cuenca", "Loja", 60);
        vuelos.AgregarVuelo("Loja", "Manta", 90);

        vuelos.MostrarVuelos();

        System.Console.WriteLine("\n🔍 Consulta de vuelo:");
        vuelos.VueloMasBarato("Quito", "Manta");
    }
}

