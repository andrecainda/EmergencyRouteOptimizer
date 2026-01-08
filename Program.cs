using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var graph = new Dictionary<string, Dictionary<string, int>>
        {
            { "Hospital", new Dictionary<string, int> { { "A", 5 }, { "B", 10 } } },
            { "A", new Dictionary<string, int> { { "Hospital", 5 }, { "C", 3 } } },
            { "B", new Dictionary<string, int> { { "Hospital", 10 }, { "C", 1 } } },
            { "C", new Dictionary<string, int> { { "A", 3 }, { "B", 1 }, { "Emergency", 2 } } },
            { "Emergency", new Dictionary<string, int>() }
        };

        var result = Dijkstra(graph, "Hospital");

        Console.WriteLine("Fastest routes from Hospital:");
        foreach (var node in result)
        {
            Console.WriteLine($"{node.Key} : {node.Value} minutes");
        }
    }

    static Dictionary<string, int> Dijkstra(
        Dictionary<string, Dictionary<string, int>> graph,
        string start)
    {
        var distances = new Dictionary<string, int>();
        var visited = new HashSet<string>();

        foreach (var node in graph.Keys)
            distances[node] = int.MaxValue;

        distances[start] = 0;

        while (visited.Count < graph.Count)
        {
            string currentNode = null;
            int shortestDistance = int.MaxValue;

            foreach (var node in distances)
            {
                if (!visited.Contains(node.Key) && node.Value < shortestDistance)
                {
                    shortestDistance = node.Value;
                    currentNode = node.Key;
                }
            }

            if (currentNode == null)
                break;

            visited.Add(currentNode);

            foreach (var neighbor in graph[currentNode])
            {
                int newDistance = distances[currentNode] + neighbor.Value;
                if (newDistance < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = newDistance;
                }
            }
        }

        return distances;
    }
}
