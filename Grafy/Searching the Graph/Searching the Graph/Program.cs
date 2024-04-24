using System;
using System.Collections.Generic;

class GraphSearch
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine().Trim());

        for (int graphNum = 1; graphNum <= t; graphNum++)
        {
            int n = int.Parse(Console.ReadLine().Trim());
            List<int>[] adjacencyList = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {
                string[] inputs = Console.ReadLine().Trim().Split();
                int m = int.Parse(inputs[1]);
                adjacencyList[i] = new List<int>();

                for (int j = 0; j < m; j++)
                {
                    adjacencyList[i].Add(int.Parse(inputs[2 + j]));
                }

                adjacencyList[i].Sort();
            }

            Console.WriteLine($"graph {graphNum}");

            while (true)
            {
                string[] query = Console.ReadLine().Trim().Split();
                int v = int.Parse(query[0]);
                int type = int.Parse(query[1]);

                if (v == 0 && type == 0) break;

                if (type == 0)
                {
                    List<int> visited = new List<int>();
                    HashSet<int> seen = new HashSet<int>();
                    DFS(v, adjacencyList, seen, visited);
                    PrintList(visited);
                }
                else if (type == 1)
                {
                    List<int> result = BFS(v, adjacencyList);
                    PrintList(result);
                }
            }
        }
    }

    static void DFS(int v, List<int>[] adjacencyList, HashSet<int> seen, List<int> visited)
    {
        if (seen.Contains(v))
            return;

        seen.Add(v);
        visited.Add(v);

        if (adjacencyList[v] != null)
        {
            foreach (int neighbor in adjacencyList[v])
            {
                if (!seen.Contains(neighbor))
                {
                    DFS(neighbor, adjacencyList, seen, visited);
                }
            }
        }
    }

    static List<int> BFS(int v, List<int>[] adjacencyList)
    {
        Queue<int> queue = new Queue<int>();
        List<int> result = new List<int>();
        HashSet<int> seen = new HashSet<int>();

        queue.Enqueue(v);
        seen.Add(v);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            result.Add(current);

            if (adjacencyList[current] != null)
            {
                foreach (int neighbor in adjacencyList[current])
                {
                    if (!seen.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        seen.Add(neighbor);
                    }
                }
            }
        }

        return result;
    }

    static void PrintList(List<int> list)
    {
        Console.WriteLine(string.Join(" ", list));
    }
}
