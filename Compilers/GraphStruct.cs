using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers
{
    class GraphStruct
    {
        List<Int32> nodes;
        List<Edge> edges;

        internal List<Edge> Edges { get => edges; set => edges = value; }
        public List<int> Nodes { get => nodes; set => nodes = value; }

        public GraphStruct(List<int> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        /**
         * Remove last node and related edges
         * */
        public void RemoveLastNode()
        {
            edges.RemoveAll(e => e.Dest == nodes.Count);
            nodes.RemoveAt(nodes.Count - 1);
        }

        /**
         * Remove first node and related edges
         * */
        public void RemoveFirstNode()
        {
            edges.RemoveAll(e => e.Origin == 1);
            nodes.RemoveAt(0);
        }
    }

    /**
     * Simple class to save relation between 2 nodes .
     * */
    class Edge
    {
        int origin;
        int dest;
        string label;

        public int Origin { get => origin; set => origin = value; }
        public int Dest { get => dest; set => dest = value; }
        public string Label { get => label; set => label = value; }

        public Edge(int origin, int dest, string label)
        {
            this.origin = origin;
            this.dest = dest;
            this.label = label;
        }
    }
}
