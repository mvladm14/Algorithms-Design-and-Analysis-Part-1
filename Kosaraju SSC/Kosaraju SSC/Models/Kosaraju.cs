using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosaraju_SSC.Models
{
    public class Kosaraju
    {
        /// <summary>
        /// No. of nodes processed so far
        /// </summary>
        private int _time;

        /// <summary>
        /// current source vertex
        /// </summary>
        private Node _sourceNode;

        private Dictionary<long, long> _leaderVotes;

        public Kosaraju()
        {
            _leaderVotes = new Dictionary<long, long>();
        }

        public void DFS(Graph graph, Node node)
        {
            node.IsExplored = true;
            node.Leader = _sourceNode;

            foreach(Node neighbor in node.AdjacencyList)
            {
                if (!neighbor.IsExplored)
                {
                    DFS(graph, neighbor);
                }
            }
            _time++;
            node.FinishingTime = _time;
        }

        public void DFSLoop(Graph graph)
        {
            _time = 0;
            _sourceNode = null;

            var reversedNodes = graph.NodesDictionary.OrderByDescending(k => k.Value.Value);

            foreach (KeyValuePair<int,Node> kv in reversedNodes)
            {
                Node node = kv.Value;
                if (!node.IsExplored)
                {
                    _sourceNode = node;
                    DFS(graph, node);
                }
            }
        }

        public void SynchronizeGraphs(Graph graph, Graph reversedGraph)
        {
            for (int i = 0; i <= graph.NodesDictionary.Count; i++)
            {
                Node node, reversedNode;
                if (graph.NodesDictionary.TryGetValue(i, out node))
                {
                    if (reversedGraph.NodesDictionary.TryGetValue(i, out reversedNode))
                    {
                        reversedNode.Value = node.FinishingTime;
                    }
                }
            }
        }

        public List<long> FindLargestSCC(Graph graph, int noOfSCC)
        {
            foreach(Node node in graph.NodesDictionary.Values)
            {
                if (_leaderVotes.Keys.Contains(node.Leader.Value))
                {
                    _leaderVotes[node.Leader.Value]++;
                }
                else
                {
                    _leaderVotes.Add(node.Leader.Value, 1);
                }
            }

            return _leaderVotes.Values.OrderByDescending(v => v)
                                      .Take(noOfSCC)
                                      .ToList<long>();
        }
    }
}
