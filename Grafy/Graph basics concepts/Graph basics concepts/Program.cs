using System;
using System.Collections.Generic;
using System.Linq;

public class Graph
{
    private List<Tuple<int, int>>[] adj;
    private bool directed;
    private int numVertices;

    public Graph(int vertices, bool isDirected)
    {
        numVertices = vertices;
        directed = isDirected;
        adj = new List<Tuple<int, int>>[vertices + 1];
        for (int i = 0; i <= vertices; i++)
            adj[i] = new List<Tuple<int, int>>();
    }

    public void AddEdge(int src, int dest, int weight)
    {
        adj[src].Add(Tuple.Create(dest, weight));
        if (!directed)
            adj[dest].Add(Tuple.Create(src, weight));
    }

    public void PrintGraph()
    {
        Console.WriteLine(directed ? "1" : "0");
        Console.WriteLine($"{numVertices} {adj.SelectMany(x => x).Count() / (directed ? 1 : 2)}");
        for (int i = 1; i <= numVertices; i++)
        {
            foreach (var edge in adj[i].OrderBy(e => e.Item1))
                if (directed || i < edge.Item1)  // Avoid printing both directions in undirected graph
                    Console.WriteLine($"{i} {edge.Item1} {edge.Item2}");
        }
    }

    public void BFS(int start)
    {
        var visited = new bool[numVertices + 1];
        var queue = new Queue<int>();
        var parent = new int[numVertices + 1];

        queue.Enqueue(start);
        visited[start] = true;
        parent[start] = -1;

        List<int> order = new List<int>();
        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            order.Add(node);
            foreach (var edge in adj[node])
            {
                if (!visited[edge.Item1])
                {
                    visited[edge.Item1] = true;
                    queue.Enqueue(edge.Item1);
                    parent[edge.Item1] = node;
                }
            }
        }

        Console.WriteLine("BFS:");
        Console.WriteLine(string.Join(" ", order));
        Console.WriteLine("\nBFS Paths:");
        for (int i = 1; i <= numVertices; i++)
        {
            Stack<int> path = new Stack<int>();
            for (int at = i; at != -1; at = parent[at])
                path.Push(at);
            Console.WriteLine(string.Join(" ", path));
        }
    }

    public void DFS(int start)
    {
        var visited = new bool[numVertices + 1];
        var stack = new Stack<int>();
        var parent = new int[numVertices + 1];
        List<int> order = new List<int>();

        stack.Push(start);
        visited[start] = true;
        parent[start] = -1;

        while (stack.Count > 0)
        {
            int node = stack.Pop();
            order.Add(node);
            foreach (var edge in adj[node].OrderByDescending(e => e.Item1))
            {
                if (!visited[edge.Item1])
                {
                    visited[edge.Item1] = true;
                    stack.Push(edge.Item1);
                    parent[edge.Item1] = node;
                }
            }
        }

        Console.WriteLine("\nDFS:");
        Console.WriteLine(string.Join(" ", order));
        Console.WriteLine("\nDFS Paths:");
        for (int i = 1; i <= numVertices; i++)
        {
            Stack<int> path = new Stack<int>();
            for (int at = i; at != -1; at = parent[at])
                path.Push(at);
            Console.WriteLine(string.Join(" ", path));
        }
    }

    public void FindConnectedComponents()
    {
        var visited = new bool[numVertices + 1];
        Console.WriteLine("\nConnected Components:");
        for (int i = 1; i <= numVertices; i++)
        {
            if (!visited[i])
            {
                List<int> component = new List<int>();
                DFSUtil(i, visited, component);
                Console.WriteLine($"C{component[0]}: " + string.Join(" ", component));
            }
        }
    }

    private void DFSUtil(int v, bool[] visited, List<int> component)
    {
        visited[v] = true;
        component.Add(v);
        foreach (var edge in adj[v])
        {
            if (!visited[edge.Item1])
            {
                DFSUtil(edge.Item1, visited, component);
            }
        }
    }

    public void FindArticulationPoints()
    {
        bool[] visited = new bool[numVertices + 1];
        int[] disc = new int[numVertices + 1];
        int[] low = new int[numVertices + 1];
        int[] parent = new int[numVertices + 1];
        bool[] ap = new bool[numVertices + 1];

        for (int i = 1; i <= numVertices; i++)
            if (!visited[i])
                APUtil(i, visited, disc, low, parent, ap);

        Console.WriteLine("\nArticulation Vertices:");
        for (int i = 1; i <= numVertices; i++)
            if (ap[i])
                Console.Write($"{i} ");
    }

    private void APUtil(int u, bool[] visited, int[] disc, int[] low, int[] parent, bool[] ap)
    {
        int time = 0;
        int children = 0;
        visited[u] = true;
        disc[u] = low[u] = ++time;
        foreach (var i in adj[u])
        {
            int v = i.Item1;
            if (!visited[v])
            {
                children++;
                parent[v] = u;
                APUtil(v, visited, disc, low, parent, ap);
                low[u] = Math.Min(low[u], low[v]);
                if (parent[u] == -1 && children > 1)
                    ap[u] = true;
                if (parent[u] != -1 && low[v] >= disc[u])
                    ap[u] = true;
            }
            else if (v != parent[u])
                low[u] = Math.Min(low[u], disc[v]);
        }
    }
}

class Program
{
    static void Main()
    {
        int directed = int.Parse(Console.ReadLine());
        var tokens = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int n = tokens[0], e = tokens[1];

        Graph graph = new Graph(n, directed == 1);
        for (int i = 0; i < e; i++)
        {
            tokens = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            graph.AddEdge(tokens[0], tokens[1], tokens[2]);
        }

        graph.PrintGraph();
        graph.BFS(1);
        graph.DFS(1);
        graph.FindConnectedComponents();
        graph.FindArticulationPoints();
    }
}
