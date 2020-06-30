using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Framework.DataStructures
{
    public abstract class Node
    {
        public Graph Graph { get; internal set; }

        public List<Edge> InboundEdges
        {
            get
            {
                return Graph.Edges.Where(e => e.To == this).ToList();
            }
        }

        public List<Edge> OutboundEdges
        {
            get
            {
                return Graph.Edges.Where(e => e.From == this).ToList();
            }
        }
    }

    public class Node<T> : Node
    {
        public T Payload { get; private set; }

        public Node(T payload)
        {
            this.Payload = payload;
        }

        public override string ToString()
        {
            return this.Payload.ToString();
        }
    }

    public class Edge
    {
        public Node From { get; private set; }

        public Node To { get; private set; }

        public Edge(Node from, Node to)
        {
            this.From = from;
            this.To = to;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", this.From, this.To);
        }
    }

    public class Graph
    {
        public List<Edge> Edges { get; private set; }

        public List<Node> Nodes { get; private set; }

        public Graph(List<Edge> edges, List<Node> nodes)
        {
            this.Edges = edges;
            this.Nodes = nodes;
            foreach (Node node in nodes)
            {
                node.Graph = this;
            }
        }

        public void AddEdge(Edge edge)
        {
            this.Edges.Add(edge);
        }

        public void AddEdge(Node from, Node to)
        {
            this.Edges.Add(new Edge(from, to));
        }

        public void AddNode(Node node)
        {
            this.Nodes.Add(node);
            node.Graph = this;
        }

        public void RemoveEdge(Edge edge)
        {
            this.Edges.Remove(edge);
        }

        public void RemoveNode(Node node)
        {
            this.Edges.RemoveAll(e => e.From == node || e.To == node);
            this.Nodes.Remove(node);
        }
    }



    public class Hello
    {
        static void Test()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(new Node<string>("Node-0"));
            nodes.Add(new Node<string>("Node-1"));
            nodes.Add(new Node<string>("Node-2"));
            nodes.Add(new Node<string>("Node-3"));
            nodes.Add(new Node<string>("Node-4"));
            nodes.Add(new Node<string>("Node-5"));

            List<Edge> edges = new List<Edge>();
            edges.Add(new Edge(nodes[0], nodes[1]));
            edges.Add(new Edge(nodes[0], nodes[2]));
            edges.Add(new Edge(nodes[1], nodes[3]));
            edges.Add(new Edge(nodes[1], nodes[4]));
            edges.Add(new Edge(nodes[4], nodes[5]));

            Graph graph = new Graph(edges, nodes);
        }
    }
    
}
