using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosaraju_SSC.Models
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
