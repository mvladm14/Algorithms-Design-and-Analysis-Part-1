using System.Collections.Generic;

namespace Dijkstra.Models
{
    public class Graph
    {
        public Dictionary<int, Node> NodesDictionary { get; set; }

        public Graph()
        {
            NodesDictionary = new Dictionary<int, Node>();
        }
    }
}
