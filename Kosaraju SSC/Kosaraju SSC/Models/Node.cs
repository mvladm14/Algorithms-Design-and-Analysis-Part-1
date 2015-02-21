using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosaraju_SSC.Models
{
    public class Node
    {
        public int Value { get; set; }

        public HashSet<Node> AdjacencyList { get; set; }

        public bool IsExplored { get; set; }

        public int FinishingTime { get; set; }

        public Node SourceNode { get; set; }

        public Node Leader { get; set; }

        public Node()
        {
            AdjacencyList = new HashSet<Node>();
        }

        public Node(int value) : this()
        {
            Value = value;
        }
    }
}
