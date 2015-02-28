using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dijkstra.Models
{
    public class DijkstraAlgorithm
    {
        /// <summary>
        /// Vertices processed so far
        /// </summary>
        private readonly List<Node> _nodesVisitedSoFar;

        /// <summary>
        /// Computed Shortest Path Distances
        /// </summary>
        public Dictionary<Node, long> NodeDistance { get; private set; }

        /// <summary>
        /// Computed shortest paths
        /// </summary>
        private readonly Dictionary<Node, List<Node>> _shortestPaths;

        public DijkstraAlgorithm()
        {
            _nodesVisitedSoFar = new List<Node>();
            NodeDistance = new Dictionary<Node, long>();
            _shortestPaths = new Dictionary<Node, List<Node>>();
        }

        public void ApplyShortestPath(Graph graph, Node startingNode)
        {
            Initialize(startingNode);

            while (_nodesVisitedSoFar.Count != graph.NodesDictionary.Keys.Count)
            {
                long minimum = long.MaxValue;
                Node vStar = null, wStar = null;
                foreach (var v in _nodesVisitedSoFar)
                {
                    foreach (var w in v.NodeDistance.Keys)
                    {
                        if (!_nodesVisitedSoFar.Contains(w))
                        {
                            long distanceToV;
                            NodeDistance.TryGetValue(v, out distanceToV);

                            long distanceVW;
                            v.NodeDistance.TryGetValue(w, out distanceVW);
                            if (minimum > distanceToV + distanceVW)
                            {
                                minimum = distanceToV + distanceVW;
                                vStar = v;
                                wStar = w;
                            }
                        }
                    }
                }
                if (null != wStar)
                {
                    _nodesVisitedSoFar.Add(wStar);
                    NodeDistance.Add(wStar, minimum);
                    List<Node> pathToV;
                    _shortestPaths.TryGetValue(vStar, out pathToV);
                    List<Node> pathToW = pathToV.ToList();
                    pathToW.Add(wStar);
                    _shortestPaths.Add(wStar, pathToW);
                }
            }
        }

        private void Initialize(Node startingNode)
        {
            _nodesVisitedSoFar.Add(startingNode);
            NodeDistance.Add(startingNode, 0);
            _shortestPaths.Add(startingNode, new List<Node>());
        }


        public Graph CreateGraphFromFile(string path)
        {
            var graph = new Graph();
            using (var reader = File.OpenText(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitedNumbers = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    int entryNodeInt = Convert.ToInt32(splitedNumbers[0]);
                    Node entryNode;

                    if (!graph.NodesDictionary.TryGetValue(entryNodeInt, out entryNode))
                    {
                        entryNode = new Node(entryNodeInt);
                        graph.NodesDictionary.Add(entryNodeInt, entryNode);
                    }

                    int totalSplits = splitedNumbers.Count();
                    int counter = 1;

                    while (counter < totalSplits)
                    {
                        string[] nodeAndDistance = splitedNumbers[counter].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        int adjacentNodeInt = Convert.ToInt32(nodeAndDistance[0]);
                        Node adjacentNode;

                        if (!graph.NodesDictionary.TryGetValue(adjacentNodeInt, out adjacentNode))
                        {
                            adjacentNode = new Node(adjacentNodeInt);
                            graph.NodesDictionary.Add(adjacentNodeInt, adjacentNode);
                        }

                        int nodeDistance = Convert.ToInt32(nodeAndDistance[1]);
                        entryNode.NodeDistance.Add(adjacentNode, nodeDistance);
                        counter++;
                    }

                    
                }
            }
            return graph;
        }
    }
}
