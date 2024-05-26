using System;
using System.Collections.Generic;
using System.Linq;

namespace InventingTestData
{
    class Program
    {
        static void Main(string[] args)
        {
            int T = int.Parse(Console.ReadLine());
            Console.ReadLine(); // Skip the blank line after the number of test cases

            for (int t = 0; t < T; t++)
            {
                if (t > 0)
                {
                    Console.ReadLine(); // Skip the blank line between test cases
                }

                int N = int.Parse(Console.ReadLine());
                List<Edge> edges = new List<Edge>();

                for (int i = 0; i < N - 1; i++)
                {
                    string[] edgeData = Console.ReadLine().Split();
                    int a = int.Parse(edgeData[0]);
                    int b = int.Parse(edgeData[1]);
                    int w = int.Parse(edgeData[2]);

                    edges.Add(new Edge(a, b, w));
                }

                Console.WriteLine(CalculateMinimumWeight(N, edges));
            }
        }

        private static int CalculateMinimumWeight(int n, List<Edge> edges)
        {
            int totalWeight = 0;
            foreach (var edge in edges)
            {
                totalWeight += edge.Weight;
            }

            int maxWeight = edges.Max(e => e.Weight);

            // Each non-tree edge should be at least maxWeight + 1 to ensure the tree is unique MST
            totalWeight += (maxWeight + 1) * (n * (n - 1) / 2 - (n - 1));

            return totalWeight;
        }
    }

    class Edge
    {
        public int Node1 { get; }
        public int Node2 { get; }
        public int Weight { get; }

        public Edge(int node1, int node2, int weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }
    }
}
