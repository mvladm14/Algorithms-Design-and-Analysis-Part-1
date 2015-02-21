using Kosaraju_SSC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Kosaraju_SSC
{
    class Program
    {
        private static List<long> SCC;
        static void Main(string[] args)
        {
            int stackSize = 1024 * 1024 * 64;


            Thread th = new Thread(FindLargestSCC, stackSize);

            th.Start();
            th.Join();

            foreach (int scc in SCC)
            {
                Console.Write(scc + ",");
            }

            Console.ReadLine();
        }

        private static void FindLargestSCC()
        {
            Graph graph, reversedGraph;

            CreateGraphsFromFile(@"..\..\TestFiles\SCC.txt", out graph, out reversedGraph);

            Kosaraju kosaraju = new Kosaraju();
            kosaraju.DFSLoop(graph);
            kosaraju.SynchronizeGraphs(graph, reversedGraph);
            kosaraju.DFSLoop(reversedGraph);

            SCC = kosaraju.FindLargestSCC(reversedGraph, 5);            
        }

        private static void CreateGraphsFromFile(string path, out Graph graph, out Graph reversedGraph)
        {
            graph = new Graph();
            reversedGraph = new Graph();
            string line;
            using (var reader = File.OpenText(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = Regex.Match(line, @"\d+");
                    int tail = int.Parse(match.Value, NumberFormatInfo.InvariantInfo);
                    match = match.NextMatch();
                    int head = int.Parse(match.Value, NumberFormatInfo.InvariantInfo);

                    // FOR 1st Graph

                    Node tailNode, headNode;
                    if (!graph.NodesDictionary.TryGetValue(tail, out tailNode))
                    {
                        tailNode = new Node(tail);
                        graph.NodesDictionary.Add(tail, tailNode);
                    }

                    if (!graph.NodesDictionary.TryGetValue(head, out headNode))
                    {
                        headNode = new Node(head);
                        graph.NodesDictionary.Add(head, headNode);
                    }

                    headNode.AdjacencyList.Add(tailNode);

                    // FOR 2nd (reversed Graph
                    Node reversedTailNode, reversedHeadNode;
                    if (!reversedGraph.NodesDictionary.TryGetValue(tail, out reversedTailNode))
                    {
                        reversedTailNode = new Node(tail);
                        reversedGraph.NodesDictionary.Add(tail, reversedTailNode);
                    }

                    if (!reversedGraph.NodesDictionary.TryGetValue(head, out reversedHeadNode))
                    {
                        reversedHeadNode = new Node(head);
                        reversedGraph.NodesDictionary.Add(head, reversedHeadNode);
                    }

                    reversedTailNode.AdjacencyList.Add(reversedHeadNode);
                }
            }
        }
    }
}
