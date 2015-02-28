using System;
using System.Collections.Generic;
using System.Linq;
using Dijkstra.Models;

namespace Dijkstra
{
    public class Program
    {
        static void Main(string[] args)
        {
            DijkstraAlgorithm dijkstraAlgorithm = new DijkstraAlgorithm();
            Graph graph = dijkstraAlgorithm.CreateGraphFromFile(@"..\..\TestFiles\dijkstraData.txt");

            dijkstraAlgorithm.ApplyShortestPath(graph, graph.NodesDictionary.Values.First());

            List<int> nodesIndexes = new List<int> { 7, 37, 59, 82, 99, 115, 133, 165, 188, 197 };
            foreach (var nodeIndex in nodesIndexes)
            {
                Node node;
                if (graph.NodesDictionary.TryGetValue(nodeIndex, out node))
                {
                    long value;
                    if (dijkstraAlgorithm.NodeDistance.TryGetValue(node, out value))
                    {
                        Console.Write(value + ",");
                    }
                }
            }
            Console.Read();
        }


    }
}
