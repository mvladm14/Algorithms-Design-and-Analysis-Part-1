using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace kargerMinCutProblem
{
    class Program
    {

        static void Main(string[] args)
        {
            int counter = 0;
            int min = 999;
            int vertex = 0;
            while (counter < 5000)
            {
                Dictionary<int, List<int>> graph = ReadFrom(@"c:\users\vlad\documents\visual studio 2013\Projects\kargerMinCutProblem\kargerMinCutProblem\kargerMinCut.txt");

                int minC = ComputeMinCut(graph, out vertex);
                if (min > minC)
                {
                    min = minC;
                }
                counter++;
            }

            Console.WriteLine(min);
            Console.ReadLine();


        }

        private static int ComputeMinCut(Dictionary<int, List<int>> graph, out int vertex1)
        {
            int v1 = 0, v2;
            int counter = 0;
            while (graph.Keys.Count > 2)
            {

                pickRandomEdge(graph, out v1, out v2);
                mergeAdjList(v1, v2, graph);
                RemoveSelfLoops(v1, v2, graph);
                DeleteV2References(v1, v2, graph);
                //Console.WriteLine(counter.ToString());
                counter++;
            }
            vertex1 = 1;
            return graph[v1].Count;



        }

        private static void DeleteV2References(int v1, int v2, Dictionary<int, List<int>> graph)
        {
            foreach (int i in graph[v1])
            {
                for (int j = 0; j < graph[i].Count; j++)
                {
                    if (graph[i][j] == v2)
                    {
                        graph[i][j] = v1;
                    }


                }
                /*
                List<int>.Enumerator e = graph[i].GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current == v2)
                    {
                        var index = graph[i].FindIndex(x => x == e.Current);
                        graph[i][index] = v2;
                    }
                }     */
            }
            graph.Remove(v2);
        }

        private static void RemoveSelfLoops(int v1, int v2, Dictionary<int, List<int>> graph)
        {
            graph[v1].RemoveAll(x => x == v1 || x == v2);
        }

        private static void mergeAdjList(int v1, int v2, Dictionary<int, List<int>> graph)
        {
            graph[v1].AddRange(graph[v2]);
            //List<int> newList = graph[v1].Distinct().ToList();
            //graph[v1] = newList;
        }

        private static void pickRandomEdge(Dictionary<int, List<int>> graph, out int v1, out int v2)
        {
            Random rnd = new Random();
            int indexKey = rnd.Next(graph.Keys.Count);
            v1 = graph.Keys.ElementAt(indexKey);
            List<int> values = graph[v1];
            int index = rnd.Next(values.Count);
            v2 = values[index];
        }

        private static Dictionary<int, List<int>> ReadFrom(string file)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            string line;
            using (var reader = File.OpenText(file))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    int node;
                    bool keySet = false;
                    int key = 0;
                    for (Match match = Regex.Match(line, @"\d+"); match.Success; match = match.NextMatch())
                    {
                        node = int.Parse(match.Value, NumberFormatInfo.InvariantInfo);
                        if (!keySet)
                        {
                            graph.Add(node, new List<int>());
                            keySet = true;
                            key = node;
                        }
                        else
                        {
                            graph[key].Add(node);
                        }
                    }

                }
            }
            return graph;
        }
    }
}
