using System;
using System.Collections.Generic;

// Main program class
class Program
{
    // Entry point of the application
    static void Main()
    {
        // Graph representation using an adjacency list
        // Each node (string) has a list of neighbors with distances (int)
        var graph = new Dictionary<string, Dictionary<string, int>>
        {
            { "Hospital", new Dictionary<string, int> { { "A", 5 }, { "B", 10 } } },
            { "A", new Dictionary<string, int> { { "Hospital", 5 }, { "C", 3 } } },
            { "B", new Dictionary<string, int> { { "Hospital", 10 }, { "C", 1 } } },
            { "C", new Dictionary<string, int> { { "A", 3 }, { "B", 1 }, { "Emergency", 2 } } },
            { "Emergency", new Dictionary<string, int>() }
        };

        // Execute Dijkstra's algorithm starting from "Hospital"
        var result = Dijkstra(graph, "Hospital");

        // Output the shortest distances from Hospital to all other nodes
        Console.WriteLine("Fastest routes from Hospital:");
        foreach (var node in result)
        {
            Console.WriteLine($"{node.Key} : {node.Value} minutes");
        }
    }

    // Implementation of Dijkstra's algorithm
    // Calculates the shortest path from the start node to all other nodes
    static Dictionary<string, int> Dijkstra(
        Dictionary<string, Dictionary<string, int>> graph,
        string start)
    {
        // Dictionary to store the shortest distance to each node
        var distances = new Dictionary<string, int>();

        // Set to track nodes that have already been visited
        var visited = new HashSet<string>();

        // Initialize all distances as infinity (unreachable)
        foreach (var node in graph.Keys)
            distances[node] = int.MaxValue;

        // Distance to the starting node is zero
        distances[start] = 0;

        // Continue until all nodes are visited
        while (visited.Count < graph.Count)
        {
            string currentNode = null;
            int shortestDistance = int.MaxValue;

            // Select the unvisited node with the smallest known distance
            foreach (var node in distances)
            {
                if (!visited.Contains(node.Key) && node.Value < shortestDistance)
                {
                    shortestDistance = node.Value;
                    currentNode = node.Key;
                }
            }

            // If no reachable unvisited node is found, exit loop
            if (currentNode == null)
                break;

            // Mark the current node as visited
            visited.Add(currentNode);

            // Update distances for neighboring nodes
            foreach (var neighbor in graph[currentNode])
            {
                // Calculate new potential distance
                int newDistance = distances[currentNode] + neighbor.Value;

                // Update distance if a shorter path is found
                if (newDistance < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = newDistance;
                }
            }
        }

        // Return the dictionary with shortest distances
        return distances;
    }
}
