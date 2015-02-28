using System.Collections.Generic;

namespace Dijkstra.Models
{
    public class Node
    {
        public int Value { get; set; }

        public Dictionary<Node, long> NodeDistance { get; set; }

        public Node(int value)
        {
            Value = value;
            NodeDistance = new Dictionary<Node, long>();
        }
    }
}
