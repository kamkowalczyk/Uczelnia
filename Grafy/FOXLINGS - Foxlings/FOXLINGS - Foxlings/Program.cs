using System;
using System.Collections.Generic;

namespace Foxlings
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            int N = int.Parse(input[0]);
            int M = int.Parse(input[1]);

            DisjointSet ds = new DisjointSet(N);

            for (int i = 0; i < M; i++)
            {
                input = Console.ReadLine().Split();
                int A = int.Parse(input[0]);
                int B = int.Parse(input[1]);
                ds.Union(A, B);
            }

            int numberOfComponents = ds.CountComponents();
            Console.WriteLine(numberOfComponents);
        }
    }

    class DisjointSet
    {
        private int[] parent;
        private int[] rank;

        public DisjointSet(int size)
        {
            parent = new int[size + 1];
            rank = new int[size + 1];

            for (int i = 1; i <= size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }

        public int CountComponents()
        {
            HashSet<int> uniqueRoots = new HashSet<int>();
            for (int i = 1; i < parent.Length; i++)
            {
                uniqueRoots.Add(Find(i));
            }
            return uniqueRoots.Count;
        }
    }
}
