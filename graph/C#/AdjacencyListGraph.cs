using System;
using System.Collections.Generic;
using System.Text;

namespace Basic_Data_Structure
{
    // @author : Babatunde Ibukun
    class AdjacencyListGraph<T>
    {
        //This is used to represent a graph and every vertices/nodes within it. 
        //This a dictionary that contains a list, each key in the dictionary represent a vertex/node and each 
        //value is used to represent a vertex's adjacent vertices
        public Dictionary<T, List<T>> Graph { get; set; }

        public AdjacencyListGraph()
        {
            this.Graph = new Dictionary<T, List<T>>();
        }


        public int Size
        {
            get { return this.Graph.Count; }
        }

        public void AddVertex(T vertex)
        {
            if (this.Graph.ContainsKey(vertex))
            {
                return;
            }
            List<T> vertices = new List<T>();
            this.Graph.Add(vertex, vertices);
        }

        public bool HasVertex(T vertex)
        {
            return this.Graph.ContainsKey(vertex);            
        }

        public void AddEdge(T vertex, T adjacentVertex)
        {
            if (!this.Graph.ContainsKey(vertex))
            {
                List<T> vertices = new List<T>();
                this.Graph.Add(vertex, vertices);
            }
            if (!this.Graph.ContainsKey(adjacentVertex))
            {
                List<T> vertices = new List<T>();
                this.Graph.Add(adjacentVertex, vertices);
            }
            this.Graph[vertex].Add(adjacentVertex);
            this.Graph[adjacentVertex].Add(vertex);
        }

        public List<T> FindNeighbours(T vertex)
        {
            return this.Graph[vertex];
        }

        public bool HasEdge(T vertex, T vertexToCheck)
        {
            return this.Graph[vertex].Contains(vertexToCheck);
        }

        static void Main1(string[] args)
        {
            AdjacencyListGraph<int> graph = new AdjacencyListGraph<int>();
            graph.AddVertex(3);
            graph.AddVertex(10);
            graph.AddEdge(3, 20);
            graph.AddVertex(20);
            graph.AddEdge(10, 40);
            graph.AddEdge(3, 40);
            graph.AddVertex(40);
            List<int> vertices = graph.FindNeighbours(3); // 20, 40
            Console.WriteLine(graph.HasVertex(20)); // true
            Console.WriteLine(graph.HasEdge(20, 50)); //false
            foreach (int vertex in vertices)
            {
                Console.WriteLine(vertex);
            }
        }
    }
}
